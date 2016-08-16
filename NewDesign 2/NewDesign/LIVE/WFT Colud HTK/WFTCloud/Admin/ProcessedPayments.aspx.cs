using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using System.Web.Security;
using System.IO;
using System.Configuration;
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.PayPalAPIInterfaceService;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Admin
{
    public partial class ProcessedPayments : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                    string Rolename = Roles.GetRolesForUser(UserName).First();
                    if (Rolename == DBKeys.Role_Administrator || Rolename == DBKeys.Role_SuperAdministrator)
                    {
                        divRefundedSuccessly.Visible = divRefundedFailed.Visible = false;
                        if (!IsPostBack)
                        {
                            //get The InvoiceNumber From the AutomatedPayments.aspx and reload page with new url
                            LoadRefundFromAutomatedPayments();
                            txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                            txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");                            
                            //Check if the screen should load edit processed payment from query string parameter.
                            LoadRefund();
                            //Show records based on pagination value and deleted flag.
                            ShowPaginatedAndDeletedRecords();
                        }
                    }
                    else
                    {
                        Response.Redirect("/LoginPage.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/LoginPage.aspx");
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion 

        #region ControlEvents

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkShowRefunded.Checked == true)
                    ShowRefundedRecordsAlso();
                else
                    ShowPaginatedAndDeletedRecords();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void chkShowRefunded_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowRefunded.Checked == true)
                ShowRefundedRecordsAlso();
            else
                ShowPaginatedAndDeletedRecords();
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["UserPaymentTransactionID"].IsValid())
                {
                    decimal AmountToRefund = Convert.ToDecimal(txtAmountToRefund.Text);
                    decimal AmountToBilled = Convert.ToDecimal(txtBilledAmount.Text);
                    int UserPaymentTransactionID = int.Parse(Request.QueryString["UserPaymentTransactionID"]);
                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(i => i.UserPaymentTransactionID == UserPaymentTransactionID);
                    var Refundhistory = objDBContext.UserRefundPayments.Where(r => r.UserPaymentTransactionID == UserPaymentTransactionID);
                    decimal RefundedAmount = 0m;
                    if (Refundhistory.Count() > 0)
                    {
                        foreach (var result in Refundhistory)
                        {
                            if (result.AuthResponseApproved == true)
                            {
                                RefundedAmount += Convert.ToDecimal(result.AuthResponseAmount);
                            }
                        }
                    }
                    if (AmountToBilled >= AmountToRefund &&( RefundedAmount== 0m || AmountToRefund<=(AmountToBilled - RefundedAmount )))
                    {
                        lblAmounSholubeLessthanBilledAmount.Visible = false;
                        string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["RefundNotificationToAdmin"]));
                        AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["RefundNotificationToCustomer"]));
                        CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        var CustomerDetails = objDBContext.UserProfiles.FirstOrDefault(a => a.UserProfileID == PaymentDetails.UserProfileID);

                        bool RefundStatus = false;
                        string InvoiceNumber = PaymentDetails.InvoiceNumber;
                        string PayedAmount = string.Format("{0:0.00}", PaymentDetails.Amount); 
                        string RefundAmount = string.Format("{0:0.00}", AmountToRefund);
                        string UserFullName = CustomerDetails.FirstName + " " + CustomerDetails.MiddleName + " " + CustomerDetails.LastName;

                        if (PaymentDetails.PaymentMethod != "PayPal")
                        {
                            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
                            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
                            AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
                            if (PaymentDetails != null && objGW != null)
                            {
                                mvPayments.ActiveViewIndex = 1;
                                string CustomerProfileID = "";
                                string PaymentProfileID = "";
                                string AuthorizeTransactionID = PaymentDetails.AuthTransactionID;
                                string AuthorizeApprovalCode = PaymentDetails.AuthAuthorizationCode;
                                var UPaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.UserPaymentTransactionID == UserPaymentTransactionID);
                                var OrderDetails = objDBContext.UserOrders.FirstOrDefault(uo => uo.InvoiceNumber == UPaymentDetails.InvoiceNumber);
                                if (OrderDetails == null)
                                {
                                    var CustAuthProfId = objDBContext.CustomerPaymentProfiles.FirstOrDefault(A => A.UserProfileID == UPaymentDetails.UserProfileID);
                                    var automatedPayment = objDBContext.AllAutomatedPayments.FirstOrDefault(A => A.CustomerProfileID == CustAuthProfId.AuthCustomerProfileID);
                                    CustomerProfileID = CustAuthProfId.AuthCustomerProfileID;
                                    PaymentProfileID = automatedPayment.CustomerPaymentProfileID;
                                }
                                else
                                {
                                    CustomerProfileID = OrderDetails.AuthCustomerProfileID;
                                    PaymentProfileID = OrderDetails.AuthPaymentProfileID;
                                }
                                var refundresonse = objGW.Refund(CustomerProfileID, PaymentProfileID, AuthorizeTransactionID, AuthorizeApprovalCode, AmountToRefund);
                                
                                UPaymentDetails.IsRefunded = refundresonse.Approved;
                                objDBContext.SaveChanges();
                                UserRefundPayment URP = new UserRefundPayment();
                                URP.AuthResponseAmount = refundresonse.Amount;
                                URP.AuthResponseApproved = refundresonse.Approved;
                                URP.AuthResponseAuthorizationCode = refundresonse.AuthorizationCode;
                                URP.AuthResponseCardNumber = refundresonse.CardNumber;
                                URP.AuthResponseCode = refundresonse.ResponseCode;
                                URP.AuthResponseInvoiceNumber = refundresonse.InvoiceNumber;
                                URP.AuthResponseMessage = refundresonse.Message;
                                URP.AuthResponseTransactionID = refundresonse.TransactionID;
                                URP.PaymentMethod = "Authroize.net";
                                /* logged User Profile ID */
                                MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                                Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                                UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                                
                                URP.DoneByUserProfileID = upf.UserProfileID;
                                URP.PaymentDateTime = DateTime.Now;
                                URP.UserPaymentInvoiceNumber = PaymentDetails.InvoiceNumber;
                                URP.UserPaymentTransactionID = PaymentDetails.UserPaymentTransactionID;

                                objDBContext.UserRefundPayments.AddObject(URP);

                                objDBContext.SaveChanges();
                                if (URP.AuthResponseApproved == true)
                                {
                                    RefundStatus = true;
                                    divRefundedFailed.Visible = false;
                                    divRefundedSuccessly.Visible = true;
                                    lblRefundedSuccess.Text = "Payment refunded successfully";
                                }
                                else
                                {
                                    RefundStatus = false;
                                    divRefundedSuccessly.Visible = false;
                                    divRefundedFailed.Visible = true;
                                    lblRefunedFailed.Text = "Payment refund failed.Because, " + refundresonse.Message;
                                }
                                LoadRefund();
                            }
                            else
                            {
                                divRefundedSuccessly.Visible = false;
                                divRefundedFailed.Visible = true;
                                lblRefunedFailed.Text = "Payment Transaction Details Not Found";
                            }                            
                        }
                        else
                        {

                            RefundTransactionResponseType refund1 = RefundTransactionAmount(PaymentDetails.PalpalPaymentTransactionID, PaymentDetails.Amount, AmountToRefund, PaymentDetails.InvoiceNumber);

                            //Refund refund = Refund.Get(ReusableRoutines.GetAPIContext(), PaymentDetails.AuthTransactionID);//AuthCardNumber- PayPal Payment Id
                            var UPaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.UserPaymentTransactionID == UserPaymentTransactionID);
                            if (refund1 != null)
                            { UPaymentDetails.IsRefunded = refund1.Ack.ToString().ToUpper().Trim() == "SUCCESS" ? true : false; }
                            else
                            { UPaymentDetails.IsRefunded = false; }
                             objDBContext.SaveChanges();

                            UserRefundPayment URP = new UserRefundPayment();
                            URP.AuthResponseAmount = AmountToRefund;
                            if (refund1 != null)
                            {
                                URP.AuthResponseApproved = refund1.Ack.ToString().ToUpper().Trim() == "SUCCESS" ? true : false;
                            }
                            else
                            {
                                URP.AuthResponseApproved = false;
                            }
                            URP.PaypalPayerMailID = PaymentDetails.PaypalPayerMailID;
                            URP.PaypalPayerID = PaymentDetails.PaypalPayerID;
                            URP.PaypalRefundTransaction = refund1.RefundTransactionID;
                            URP.PaymentMethod = "PayPal";
                            URP.AuthResponseInvoiceNumber = PaymentDetails.InvoiceNumber;
                            URP.AuthResponseMessage = refund1 != null ? (refund1.RefundTransactionID != null ? "SUCCESS" : "Refund Error Code :" + refund1.Errors.First().ErrorCode) : "Refund Error Code :" + refund1.Errors.First().ErrorCode; ;

                            /* logged User Profile ID */
                            MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                            Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                            UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);

                            URP.DoneByUserProfileID = upf.UserProfileID;
                            URP.PaymentDateTime = DateTime.Now;
                            URP.UserPaymentInvoiceNumber = PaymentDetails.InvoiceNumber;
                            URP.UserPaymentTransactionID = PaymentDetails.UserPaymentTransactionID;

                            objDBContext.UserRefundPayments.AddObject(URP);

                            objDBContext.SaveChanges();
                            if (URP.AuthResponseApproved == true)
                            {
                                RefundStatus = true;
                                divRefundedFailed.Visible = false;
                                divRefundedSuccessly.Visible = true;
                                lblRefundedSuccess.Text = "Payment refunded successfully";
                            }
                            else
                            {
                                RefundStatus = false;
                                divRefundedSuccessly.Visible = false;
                                divRefundedFailed.Visible = true;
                                lblRefunedFailed.Text = "Payment refund failed";
                            }
                            LoadRefund();
                        }
                        if (RefundStatus == true)
                        {

                            string CommonDetails = "<tr><td style='width:30%;'><strong>Invoice Number</strong></td><td style='width:2%;'>:</td><td style='width:68%;'> " + InvoiceNumber + "</td></tr>"
                                                  + "<tr><td style='width:30%;'><strong>Payed Amount </strong></td><td style='width:2%;'>:</td><td style='width:68%;'> $ " + PayedAmount + "</td></tr>"
                                                  + "<tr><td style='width:30%;'><strong>Refund Amount</strong></td><td style='width:2%;'>:</td><td style='width:68%;'> $ " + RefundAmount + "</td></tr>";
                            if (RefundedAmount > 0m)
                            {
                                string totalRefundedAmt = string.Format("{0:0.00}", (RefundedAmount + AmountToRefund)); 
                                CommonDetails += "<tr><td style='width:30%;'><strong>Previous Refunded Amount</strong></td><td style='width:2%;'>:</td><td style='width:68%;'> $ " + RefundedAmount + "</td></tr>";
                                CommonDetails += "<tr><td style='width:30%;'><strong>Total Refunded Amount</strong></td><td style='width:2%;'>:</td><td style='width:68%;'> $ " + totalRefundedAmt + "</td></tr>";
                            }
                            string AdminMainContent = "<table style='width:70%;'>"
                                                    + "<tr><td style='width:30%;'><strong>Customer Name</strong></td><td style='width:2%;'>:</td><td style='width:68%;'>" + UserFullName +"</td></tr>"
                                                    + CommonDetails
                                                    + "</table>";

                            string CustomerMainContent = "<table style='width:70%;'>"
                                                       + CommonDetails
                                                       + "</table>";
                            AdminEmailContent = AdminEmailContent.Replace("++UserName++", UserFullName + " (" + CustomerDetails.EmailID + ")");
                            AdminEmailContent = AdminEmailContent.Replace("++AddContentHere++", AdminMainContent);
                            SMTPManager.SendAdminNotificationEmail(AdminEmailContent, "Customer Refund Notification for " + CustomerDetails.EmailID, false);

                            CustomerEmailContent = CustomerEmailContent.Replace("++UserName++", UserFullName);
                            CustomerEmailContent = CustomerEmailContent.Replace("++AddContentHere++", CustomerMainContent);
                            SMTPManager.SendEmail(CustomerEmailContent, "WFTCloud Refund Notification for Invoice Number " + PaymentDetails.InvoiceNumber, CustomerDetails.EmailID, false, true);
                        }
                    }
                    else
                    {
                        lblAmounSholubeLessthanBilledAmount.Visible = true;
                    }
                   
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divRefundedSuccessly.Visible = false;
                divRefundedFailed.Visible = true;
                lblRefunedFailed.Text = "Error occured while refunding";
            }
        }

        #endregion

        #region ReusableRoutines
        
        private void LoadRefundFromAutomatedPayments()
        {
             try
            {
                if (Request.QueryString["InvoiceNumber"].IsValid())
                {
                    string InvoiceNumber = Request.QueryString["InvoiceNumber"];
                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(i => i.InvoiceNumber == InvoiceNumber);
                    if (PaymentDetails != null)
                    {
                        Response.Redirect("/Admin/ProcessedPayments.aspx?UserProfileID="+PaymentDetails.UserProfileID+"&UserPaymentTransactionID="+PaymentDetails.UserPaymentTransactionID, false);
                    }
                    else
                    {
                        mvPayments.ActiveViewIndex = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divRefundSuccess.Visible = false;
                divPaymentError.Visible = true;
                lblPaymentError.Text = "Error occured while showing Refund History.";
            }
        }

        private void LoadRefund()
        {
            try
            {
                if (Request.QueryString["UserPaymentTransactionID"].IsValid())
                {
                    int UserPaymentTransactionID = int.Parse(Request.QueryString["UserPaymentTransactionID"]);

                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(i => i.UserPaymentTransactionID == UserPaymentTransactionID);
                    var UserProf = objDBContext.UserProfiles.FirstOrDefault(s => s.UserProfileID == PaymentDetails.UserProfileID);
                    if (PaymentDetails != null)
                    {
                        mvPayments.ActiveViewIndex = 1;
                        decimal BilledAmount = PaymentDetails.Amount;
                        decimal RefundedAmount = 0m;
                        lblMode.Text = PaymentDetails.PaymentMethod.IsValid() ? PaymentDetails.PaymentMethod : "Authroize.net";
                        //tramountToRefund.Visible = PaymentDetails.AuthMessage == "PayPal" ? false : true;
                        lblPaymentInVoiceNumber.Text = lblInvoiceNumber.Text = PaymentDetails.InvoiceNumber;
                        lblIsRefuned.Text = PaymentDetails.IsRefunded == true ? "Yes" : "No";
                        lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy");
                        lbluserFullName.Text = UserProf.FirstName + " " + UserProf.MiddleName + " " + UserProf.LastName;
                        lblUserMailID.Text = UserProf.EmailID;
                        txtBilledAmount.Text = PaymentDetails.Amount.ToString("0.00");
                        txtAmountToRefund.Text = "";
                        var Refundhistory = objDBContext.UserRefundPayments.Where(r => r.UserPaymentTransactionID == UserPaymentTransactionID);
                        if (Refundhistory.Count() > 0)
                        {
                            rptrRefundHistory.DataSource = Refundhistory;
                            rptrRefundHistory.DataBind();
                            foreach (var result in Refundhistory)
                            {
                                if(result.AuthResponseApproved== true)
                                {
                                    RefundedAmount += Convert.ToDecimal(result.AuthResponseAmount);
                                }
                            }
                            if (RefundedAmount >= BilledAmount)
                            {
                                tramountToRefund.Visible = trBtnRefund.Visible = false;
                                divRefundHistroy.Visible = true;
                            }
                            else
                            {
                                tramountToRefund.Visible = trBtnRefund.Visible = true;
                                divRefundHistroy.Visible = true;
                                //tramountToRefund.Visible = PaymentDetails.AuthMessage == "PayPal" ? false : true;
                            }
                        }
                        else
                        {
                            tramountToRefund.Visible = trBtnRefund.Visible = true;
                            divRefundHistroy.Visible = false;
                            //tramountToRefund.Visible = PaymentDetails.AuthMessage == "PayPal" ? false : true;
                        }
                    }
                    else
                    {
                        mvPayments.ActiveViewIndex = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divRefundSuccess.Visible = false;
                divPaymentError.Visible = true;
                lblPaymentError.Text = "Error occured while showing Refund History.";
            }
        }
        public string Username(string UserProfileID)
        {
            int UserProfID = Convert.ToInt32(UserProfileID);
            var Usr = objDBContext.UserProfiles.FirstOrDefault(a => a.UserProfileID == UserProfID);
            return (Usr.FirstName + " " + Usr.MiddleName + " " + Usr.LastName);
        }
        public string UserEmailID(string UserProfileID)
        {
            int UserProfID = Convert.ToInt32(UserProfileID);
            var Usr = objDBContext.UserProfiles.FirstOrDefault(a => a.UserProfileID == UserProfID);
            return Usr.EmailID;
        }
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                DateTime FromDate = Convert.ToDateTime(txtStartDate.Text).Date;
                DateTime ToDate = Convert.ToDateTime(txtEndDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var ProcessedPayments = objDBContext.UserPaymentTransactions.Where(date => date.Approved == true && date.PaymentDateTime>=FromDate && date.PaymentDateTime <= ToDate && (date.IsRefunded == false || date.IsRefunded == null));

                    rptrProcessedPayment.DataSource = ProcessedPayments;
                    rptrProcessedPayment.DataBind();
                    if (ProcessedPayments.Count() > 0)
                    {
                        divPaymentError.Visible = false;
                    }
                    else
                    {
                        divPaymentError.Visible = true;
                        lblPaymentError.Text = "There are no Payments for the selected date.";
                    }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public RefundTransactionResponseType RefundTransactionAmount(string transactionId, decimal SucceedAmount, decimal amountToRefund, string InvoiceNumber)
        {
            try
            {
                // Create request object
                RefundTransactionRequestType request = new RefundTransactionRequestType();
                // (Required) Unique identifier of the transaction to be refunded.
                // Note: Either the transaction ID or the payer ID must be specified.

                request.TransactionID = transactionId;
                // Type of refund you are making. It is one of the following values:
                // * Full – Full refund (default).
                // * Partial – Partial refund.
                // * ExternalDispute – External dispute. (Value available since version 82.0)
                // * Other – Other type of refund. (Value available since version 82.0)
                CurrencyCodeType currency = CurrencyCodeType.USD;
                if (SucceedAmount == amountToRefund)
                {
                    request.RefundType = RefundType.FULL;
                }
                else
                {
                    request.RefundType = RefundType.PARTIAL;
                    request.Amount = new BasicAmountType(currency, amountToRefund.ToString());
                }

                request.Memo = "Refund - " + InvoiceNumber;

                // (Optional)Type of PayPal funding source (balance or eCheck) that can be used for auto refund. It is one of the following values:
                // * any – The merchant does not have a preference. Use any available funding source.
                // * default – Use the merchant's preferred funding source, as configured in the merchant's profile.
                // * instant – Use the merchant's balance as the funding source.
                // * eCheck – The merchant prefers using the eCheck funding source. If the merchant's PayPal balance can cover the refund amount, use the PayPal balance.
                // Note: This field does not apply to point-of-sale transactions.

                request.RefundSource = RefundSourceCodeType.DEFAULT;



                // Invoke the API
                RefundTransactionReq wrapper = new RefundTransactionReq();
                wrapper.RefundTransactionRequest = request;

                // Create the PayPalAPIInterfaceServiceService service object to make the API call
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

                // # API call 
                // Invoke the RefundTransaction method in service wrapper object  
                RefundTransactionResponseType refundTransactionResponse = service.RefundTransaction(wrapper);
                return refundTransactionResponse;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }
     
        private void ShowRefundedRecordsAlso()
        {
            try
            {
                DateTime FromDate = Convert.ToDateTime(txtStartDate.Text).Date;
                DateTime ToDate = Convert.ToDateTime(txtEndDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var ProcessedPayments = objDBContext.UserPaymentTransactions.Where(date => date.Approved == true && date.PaymentDateTime >= FromDate && date.PaymentDateTime <= ToDate);
                    rptrProcessedPayment.DataSource = ProcessedPayments;
                    rptrProcessedPayment.DataBind();
                    if (ProcessedPayments.Count() > 0)
                    {
                        divPaymentError.Visible = false;
                    }
                    else
                    {
                        divPaymentError.Visible = true;
                        lblPaymentError.Text = "There are no Payments for the selected date.";
                    }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

    }
}