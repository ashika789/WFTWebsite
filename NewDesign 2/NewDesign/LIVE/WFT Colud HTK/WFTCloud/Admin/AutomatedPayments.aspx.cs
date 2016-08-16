using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
using System.Web.Security;
using System.IO;
using System.Configuration;

namespace WFTCloud.Admin
{
    public partial class AutomatedPayments : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities ObjDBContext = new cgxwftcloudEntities();
        AuthorizeNet.CustomerGateway objGW;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
            objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            lblSuccessMessage.Visible = false;
            lblPaymentsErrorMsg.Visible = false;
            if (!IsPostBack)
            {
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy"); 
                //Show records based on pagination value and deleted flag.
                ShowPaginatedAndDeletedRecords();
                //Check if the screen should load edit category from query string parameter.
                LoadEditPayment();
            }
        }
        #endregion

        #region ControlEvents

        protected void btnGo_Click(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkShowCancelledSubscriptionPayments_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Request.QueryString[QueryStringKeys.EditPaymentDetails].IsValid())
                {
                    int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
                    var selPayments = ObjDBContext.AllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
                    string OldWorkLog = selPayments.Worklog;
                    decimal NewAmount = Convert.ToDecimal(txtAmountToBill.Text);
                    decimal oldAmount =Convert.ToDecimal(selPayments.CurrentMonthBilling);
                    DateTime NewPaymentDate = Convert.ToDateTime(txtPaymentDate.Text).Date;
                    DateTime OldPaymentDate = Convert.ToDateTime(selPayments.PaymentDate).Date;
                    /*
                    if (chkChangePaymentDateToAllFeaturePayments.Checked == true && OldPaymentDate != NewPaymentDate)
                    {
                        long daysToBeAdd = NewPaymentDate.Day;
                        if (daysToBeAdd > 28)
                        {
                            lblPaymentsErrorMsg.Visible = true;
                            lblPaymentsErrorText.Text = "To Change Future Transactions Date the Payment date should be less the 29 of Month.";
                            return;
                        }
                    }
                    */
                    MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                    Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                    var upf = ObjDBContext.vwUsersListWithFullNames.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                    string PaymenDateChangeLog = ((OldPaymentDate == NewPaymentDate) ? "" : "Payment Date Changed From " + OldPaymentDate.ToString("dd-MMM-yyyy") + " To " + NewPaymentDate.ToString("dd-MMM-yyyy") + "\n");
                    string AmountToBillChangeLog = (NewAmount == oldAmount ? "" : "Amount to be billed for this transaction :  Changed From $ " + oldAmount.ToString() + " To $ " + txtAmountToBill.Text) + "\n";
                    string PaymentProfileChangeLog = string.Empty;
                    if (lblMode.Text == "PayPal")
                    {
                        PaymentProfileChangeLog = (selPayments.PaypalPayerMailID == ddlRegisteredPayPalEmails.SelectedItem.Text ? "" : " PayPal Payment Profile ID for this transaction : Changed From " + selPayments.PaypalPayerMailID + " To " + ddlRegisteredPayPalEmails.SelectedItem.Text);
                    }
                    else
                    {
                         PaymentProfileChangeLog = (selPayments.CustomerPaymentProfileID == ddlExistingCards.SelectedValue ? "" : " Authorize.net Payment Profile ID for this transaction : Changed From " + lblCreditCardUsedForBilling.Text + "(" + selPayments.CustomerPaymentProfileID + ")" + " To " + "(" + ddlExistingCards.SelectedItem + ")" + ddlExistingCards.SelectedValue);
                    }
                    string NewWorkLog = txtWorkLog.Text + "\n" + PaymenDateChangeLog + AmountToBillChangeLog + PaymentProfileChangeLog;
                                        //((OldPaymentDate == NewPaymentDate) ? "" : "Payment Date Changed From " + OldPaymentDate.ToString("dd-MMM-yyyy")+" To "+NewPaymentDate.ToString("dd-MMM-yyyy")+"\n")
                                        //+ (NewAmount == oldAmount ? "" : "Amount to be billed for this transaction :  Changed From $ " + oldAmount.ToString() + " To $ " + txtAmountToBill.Text)
                                        //+ (selPayments.CustomerPaymentProfileID == ddlExistingCards.SelectedValue ? "" : " Authorize.net Payment Profile ID for this transaction : Changed From " + lblCreditCardUsedForBilling.Text + "(" + selPayments.CustomerPaymentProfileID + ")" + " To " + "(" + ddlExistingCards.SelectedItem + ")"+ ddlExistingCards.SelectedValue);
                    string curentCustomerPaymentProfileID = selPayments.CustomerPaymentProfileID;
                    string CurrentPayPalEmailId = selPayments.PaypalPayerMailID;
                    selPayments.CustomerPaymentProfileID = ddlExistingCards.SelectedValue;
                    selPayments.PaymentDate = NewPaymentDate;
                    selPayments.CurrentMonthBilling  = NewAmount;
                    selPayments.PaymentStatus = "BillingPlanned";
                    ObjDBContext.SaveChanges();
                    ObjDBContext.pr_UpdateAllPaymentWorklog(PayId, upf.FullName, NewWorkLog);
                    
                    string UserFullName = upf.FullName;
                    
                    if (chkMakeThisCardForFutureTransaction.Checked == true)
                    {
                        ObjDBContext.pr_UpdateSelectedSubscriptionPaymentProfileIDAndWorklog(curentCustomerPaymentProfileID, selPayments.UserSubscriptionID, ddlExistingCards.SelectedValue, UserFullName + "(" + upf.EmailID + ")", (txtWorkLog.Text + PaymentProfileChangeLog));
                        trMakeThisCardForFutureTransaction.Visible = false;
                    }
                    if (chkMakeThisCardForFutureTransaction.Checked == true && lblMode.Text == "PayPal")
                    {
                        ObjDBContext.pr_UpdateSelectedPayPalSubscriptionPaymentProfileIDAndWorklog(CurrentPayPalEmailId, selPayments.UserSubscriptionID, ddlRegisteredPayPalEmails.SelectedItem.Text, UserFullName + "(" + upf.EmailID + ")", (txtWorkLog.Text + PaymentProfileChangeLog));
                        trMakeThisCardForFutureTransaction.Visible = false;
                    }
                    if (chkAmountChangeToAllFeaturePayments.Checked == true && NewAmount != oldAmount)
                    {
                        ObjDBContext.pr_UpdateAllFuturePaymentCurrentMonthBillingAndWorklog(selPayments.UserSubscriptionID,selPayments.AllPaymentID,oldAmount,NewAmount,UserFullName + "(" + upf.EmailID + ")", (txtWorkLog.Text + AmountToBillChangeLog));
                    }
                    //Days difference between the old payment date & new payment date will be added to all future transaction's Payment Date
                    //
                    //Eg: 10-Aug-2014 was changed to 15-Aug-2014 means 5 days will be added to all future transaction's Payment Date
                    if (chkChangePaymentDateToAllFeaturePayments.Checked == true && OldPaymentDate != NewPaymentDate)
                    {
                        
                        //long daysToBeAdd = NewPaymentDate.Subtract(OldPaymentDate).Days;
                        int daysToBeAdd =Convert.ToInt32( NewPaymentDate.Day);
                        var FuturePayments = ObjDBContext.AllAutomatedPayments.Where(a => a.UserSubscriptionID == selPayments.UserSubscriptionID 
                                                && a.AllPaymentID > selPayments.AllPaymentID 
                                                && a.PaymentStatus != "Success" ).ToList();
                        foreach (var res in FuturePayments)
                        {
                            ObjDBContext = new cgxwftcloudEntities();
                            var AutomatedPayment = ObjDBContext.AllAutomatedPayments.FirstOrDefault(A => A.AllPaymentID == res.AllPaymentID);
                            var lastDayOfMonth = DateTime.DaysInMonth(AutomatedPayment.PaymentDate.Year, AutomatedPayment.PaymentDate.Month);
                            DateTime NwDate = new DateTime();
                            if (daysToBeAdd < lastDayOfMonth)
                             NwDate = new DateTime(AutomatedPayment.PaymentDate.Year, AutomatedPayment.PaymentDate.Month, daysToBeAdd);
                            else
                                NwDate = new DateTime(AutomatedPayment.PaymentDate.Year, AutomatedPayment.PaymentDate.Month, lastDayOfMonth);
                            if (NwDate != AutomatedPayment.PaymentDate)
                            {
                                //PaymenDateChangeLog = "Payment Date Changed From " + AutomatedPayment.PaymentDate.ToString("dd-MMM-yyyy") + " To " + AutomatedPayment.PaymentDate.AddDays(daysToBeAdd).ToString("dd-MMM-yyyy") + "\n";
                                PaymenDateChangeLog = "Payment Date Changed From " + AutomatedPayment.PaymentDate.ToString("dd-MMM-yyyy") + " To " + NwDate.ToString("dd-MMM-yyyy") + "\n";
                                //AutomatedPayment.PaymentDate = AutomatedPayment.PaymentDate.AddDays(daysToBeAdd);

                                AutomatedPayment.PaymentDate = NwDate;
                                ObjDBContext.SaveChanges();
                                ObjDBContext.pr_UpdateAllPaymentWorklog(AutomatedPayment.AllPaymentID, upf.FullName, (txtWorkLog.Text + PaymenDateChangeLog));
                            }
                        }
                    }
                    LoadEditPayment();
                    lblSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "EditPaymentDetails", Ex.Message, Ex.StackTrace, DateTime.Now);
                lblPaymentsErrorMsg.Visible = true;
                lblPaymentsErrorText.Text = "Error while editing Payment details.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadEditPayment();
        }

        protected void ddlExistingCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
            var selPayments = ObjDBContext.vwManageAllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
            if (ddlExistingCards.SelectedValue != selPayments.CustomerPaymentProfileID)
            {
                trMakeThisCardForFutureTransaction.Visible = true;
            }
            else
            {
                trMakeThisCardForFutureTransaction.Visible = false;
            }
        }
        protected void ddlRegisteredPayPalEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
            var selPayments = ObjDBContext.vwManageAllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
            if (ddlRegisteredPayPalEmails.SelectedItem.Text != selPayments.PaypalPayerMailID)
            {
                trMakeThisCardForFutureTransaction.Visible = true;
            }
            else
            {
                trMakeThisCardForFutureTransaction.Visible = false;
            }
        }
        #endregion

        #region ReusableRoutines

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                DateTime FromDate = Convert.ToDateTime(txtStartDate.Text).Date;
                DateTime ToDate = Convert.ToDateTime(txtEndDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var aptmts = ObjDBContext.vwManageAllAutomatedPayments.Where(date => date.PaymentDate >= FromDate && date.PaymentDate <= ToDate).OrderByDescending(obj => obj.PaymentDate);
                if (chkShowCancelledSubscriptionPayments.Checked)
                {
                    rptrAutomatedPayments.DataSource = aptmts;
                    rptrAutomatedPayments.DataBind();
                }
                else
                {
                    rptrAutomatedPayments.DataSource = aptmts.Where(p => p.PaymentStatus.Contains("Cancel") == false);
                    rptrAutomatedPayments.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "ShowPaginatedAndDeletedRecords", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditPayment()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditPaymentDetails].IsValid())
                {
                   mvContainer.ActiveViewIndex = 1;
                    int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
                    var selPayments = ObjDBContext.vwManageAllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
                    if (selPayments != null)
                    {
                        dvAuthresponse.Visible = false;
                        var user = ObjDBContext.vwUsersListWithFullNames.FirstOrDefault(selP => selP.UserProfileID == selPayments.UserProfileID);
                        lblusername.Text = user.FullName;
                        hypUserEmailID.Text = user.EmailID;
                        hypUserEmailID.NavigateUrl = "mailto:" + hypUserEmailID.Text;
                        lblCategoryName.Text = selPayments.CategoryName;
                        lblServiceName.Text = selPayments.ServiceName;
                        lblInitialSubscriptionDate.Text = Convert.ToDateTime(selPayments.InitialSubscriptionDate).ToString("dd-MMM-yyyy");
                        DateTime pDate =  Convert.ToDateTime(selPayments.PaymentDate);
                        txtPaymentDate.Text= lblPaymentDate.Text =pDate.ToString("dd-MMM-yyyy");
                        cetxtPaymentDate.SelectedDate = pDate;
                        cetxtPaymentDate.StartDate = new DateTime(pDate.Year, pDate.Month,1);
                        var lastDayOfMonth = DateTime.DaysInMonth(pDate.Year, pDate.Month);
                        cetxtPaymentDate.EndDate = new DateTime(pDate.Year, pDate.Month, lastDayOfMonth);

                        txtAmountToBill.Text = selPayments.CurrentMonthBilling.ToString("0.00");
                        lblMode.Text = selPayments.PaymentMethod.IsValid()?selPayments.PaymentMethod:"Authorize.net";
                        lblPaymentStatus.Text = selPayments.PaymentStatus;
                        txtWorkLogHistory.Text = selPayments.Worklog;
                        lblPaymentMethodType.Text = lblMode.Text == "PayPal" ? "PayPal Response" : "Authroize.net Response";
                        if (lblMode.Text == "PayPal")
                        {
                            ddlRegisteredPayPalEmails.DataSource = ObjDBContext.pr_GetCustomerPayPalMailIDs(user.UserProfileID);
                            ddlRegisteredPayPalEmails.DataTextField = "EmailID";
                            ddlRegisteredPayPalEmails.DataValueField = "UserPaymentTransactionID";
                            ddlRegisteredPayPalEmails.DataBind();

                            pnlAuthResponse.Visible = false;
                            pnlPaypalResponse.Visible = true;
                            trCreditCardUsedForBilling.Visible = false;
                            trExistingCards.Visible = false;
                        }
                        else
                        {
                            var CustomerCardDetails = ObjDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == selPayments.UserProfileID && u.Status == true);
                            if (CustomerCardDetails.Count() != 0)
                            {
                                var CustomerpaymentProfiles = objGW.GetCustomer(CustomerCardDetails.FirstOrDefault().AuthCustomerProfileID);
                                var zz = CustomerCardDetails;

                                var s = from cpp in CustomerpaymentProfiles.PaymentProfiles
                                        join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                                        select new
                                        {
                                            ProfileID = cpp.ProfileID,
                                            CardNumber = "XXXXXXXX" + cpp.CardNumber + (zz1.DefaultPaymentID == true ? " Default Credit Card " : "") + (zz1.Status != true ? " De-Activated by customer" : ""),
                                            DefaultPaymentID = zz1.DefaultPaymentID,
                                            AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                            UserProfileID = zz1.UserProfileID,
                                        };
                                ddlExistingCards.DataSource = s;
                                ddlExistingCards.DataTextField = "CardNumber";
                                ddlExistingCards.DataValueField = "ProfileID";
                                ddlExistingCards.DataBind();
                                ddlExistingCards.SelectedValue = selPayments.CustomerPaymentProfileID;
                                var previouscard = s.FirstOrDefault(a => a.ProfileID == selPayments.CustomerPaymentProfileID);
                                if (previouscard != null)
                                {
                                    lblCreditCardUsedForBilling.Text = previouscard.CardNumber;
                                    pnlAuthResponse.Visible = true;
                                    pnlPaypalResponse.Visible = false;
                                    trCreditCardUsedForBilling.Visible = true;
                                    trExistingCards.Visible = true;
                                }
                            }
                            
                        }
                        if (selPayments.PaymentStatus == "PaymentFailed" || selPayments.PaymentStatus == "BillingPlanned" || selPayments.PaymentStatus == "Planned")
                        {
                           trWorkLogEdit.Visible = trPaymentDateEdit.Visible = trPaymentDetEdit.Visible = true;
                           trPaymentDate.Visible = false;
                           txtAmountToBill.ReadOnly = false;
                        }
                        else if(selPayments.PaymentStatus == "Success")
                        {
                            trWorkLogEdit.Visible = trPaymentDateEdit.Visible = trPaymentDetEdit.Visible = false;
                            trPaymentDate.Visible = true;
                            txtAmountToBill.ReadOnly = true;
                            trExistingCards.Visible = false;
                        }
                        if (selPayments.PaymentStatus == "PaymentFailed" || selPayments.PaymentStatus == "Success")
                        {
                            dvAuthresponse.Visible = true;
                            lblAuthApprovalStatus.Text = selPayments.ApprovalStatus == 1 ? "Approved" : "Not Approved";
                            lblAuthorizationCode.Text = selPayments.AuthorizationCode.IsValid()?selPayments.AuthorizationCode:"-";
                            lblCardNumber.Text = selPayments.CardNumber.IsValid() ? selPayments.CardNumber : "-";
                            lblMessage.Text = selPayments.Message.IsValid() ? selPayments.Message : "-";
                            lblResponseCode.Text = selPayments.ResponseCode.IsValid() ? selPayments.ResponseCode : "-";
                            lblTransactionID.Text = selPayments.TransactionID.IsValid() ? selPayments.TransactionID : "-";
                            lblPayPalPaymentId.Text = selPayments.ResponseBillingAgreementID.IsValid() ? selPayments.ResponseBillingAgreementID : "-";
                            lblPayPalPayerId.Text = selPayments.PaypalPayerID.IsValid() ? selPayments.PaypalPayerID : "-"; ;
                            lblPayPalSalesId.Text = selPayments.ResponsePaymentTransactionID.IsValid() ? selPayments.ResponsePaymentTransactionID : "-";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "EditPaymentDetails", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        #endregion


    }
}