using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Customer
{
    public partial class CloudPackages : System.Web.UI.Page
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        cgxwftcloudEntities objDBContext1 = new cgxwftcloudEntities();
        public string UserMembershipID;
        public string CancelServices;
        AuthorizeNet.CustomerGateway objGW;

        #endregion

        #region Pageload

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!Request.IsLocal && !Request.IsSecureConnection)
                //{
                //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
                //    Response.Redirect(redirectUrl, false);
                //    HttpContext.Current.ApplicationInstance.CompleteRequest();
                //}

                bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
                AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
                objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);

                divPaymentSuccess.Visible = divPaymentSuccess1.Visible= divPaymentfailed.Visible= divPaymentfailed1.Visible = divAvaSuccessMessage.Visible = divAvaErrorMessage.Visible = divSubscribedSuccess.Visible =
                divSubscribedFailed.Visible = divMyDeleteCartFailed.Visible = divMyDeleteCartSuccess.Visible = divCouponFailed.Visible = divCouponSuccess.Visible= false;
                UserMembershipID = Request.QueryString["userid"];
                if (UserMembershipID != null && UserMembershipID != "")
                {
                    if (!IsPostBack)
                    {
                        //Show records based on pagination value and active flag for Available Services.
                        ShowCloudAvailablePackages();
                        //Show Subscribed Records
                        ShowSubscribedRecords();
                        //Show Logged User My card details
                        MyCartDetails();
                        //Show the tab controls.
                        LoadTabView();
                        //delete selected service from the cart
                        DeletemyCart();
                        //Add Sevice To My Cart
                        AddToCart();
                        //Cancel Subscribed Service
                        CancelSubscribedService();
                        //Show CreditCard Details.
                        txtCreditCardNumber.Text = txtVerifiCode.Text = "";
                        divInValidCard.Visible = true;
                        divValidCard.Visible = false;
                        //ShowCreditCardDetails();
                        if (Request.QueryString["ShowUserSubscriptionDetails"].IsValid())
                        {
                            int USubID = Convert.ToInt32(Request.QueryString["ShowUserSubscriptionDetails"]);
                            ShowSubscribedServiceDetails(USubID);
                        }

                        if (lblTotalPrice.Text != "")
                        {
                            decimal TotalAmount = Convert.ToDecimal(lblTotalPrice.Text.Replace("Amount Payable : $ ", ""));
                            if (TotalAmount == 0M)
                            {
                                PaypalTable.Visible = false;
                                UpdatePanel1.Visible = false;
                                btnPaymentSubmit.Text = "Start Trial";
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region Control Events

        protected void ibtnPayPal_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCouponCode.Text != "")
                ApplyCoupon();
                if (chkAgree.Checked)
                {
                    UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    UserProfile user = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                    PaypalPayment(user);
                }
                else
                {
                    divRegisterError.Visible = true;
                    divRegisterError.Focus();
                    lblRegisterError.Text = "Please accept Terms and Conditions.";
                }
        }

        protected void ibtnAuthNet_Click(object sender, ImageClickEventArgs e)
        {
            if (pnlPaymentDetails.Visible)
                pnlPaymentDetails.Visible = false;
            else
                pnlPaymentDetails.Visible = true;
        }

        protected void lkbLaunchService_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HiddenField hdnLaunchLink = (HiddenField)item.FindControl("hdnLaunchLink");
            string LaunchServiceLink = hdnLaunchLink.Value;
            Session["LaunchURL"] = LaunchServiceLink;
            UserMembershipID = Request.QueryString["userid"];
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Customer/LaunchService.aspx?userid=" + UserMembershipID + "', '_blank');", true);
            //ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('/Customer/LaunchService.aspx?userid=" + UserMembershipID + "','null','location=no,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,addressbar=0,titlebar=no,directories=no,channelmode=no,status=no'); </script>");
        }

        protected void lkbtnSubscribedPackages_Click(object sender, EventArgs e)
        {
            liSubscribedPackages.Attributes.Add("class", "active");
            SubscribedPackages.Attributes.Add("class", "tab-pane in active");
            liCloudPackages.Attributes.Add("class", "");
            WFTCloudPackages.Attributes.Add("class", "tab-pane");
            liMyCart.Attributes.Add("class", "");
            MyCart.Attributes.Add("class", "tab-pane");
            //liMyCart.Visible = false;
        }

        protected void lkbtnCloudPackages_Click(object sender, EventArgs e)
        {
            liCloudPackages.Attributes.Add("class", "active");
            WFTCloudPackages.Attributes.Add("class", "tab-pane in active");
            liSubscribedPackages.Attributes.Add("class", "");
            SubscribedPackages.Attributes.Add("class", "tab-pane");
            liMyCart.Attributes.Add("class", "");
            MyCart.Attributes.Add("class", "tab-pane");
            //liMyCart.Visible = false;
        }

        protected void lkbtnMyCart_Click(object sender, EventArgs e)
        {
            showCartTab();
        }

        protected void btnShowMyCart_Click(object sender, EventArgs e)
        {
            showCartTab();
        }
        protected void backbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ShowUserSubscriptionDetails"].IsValid())
                        {
                            int USubID = Convert.ToInt32(Request.QueryString["ShowUserSubscriptionDetails"]);
                            var USubServices = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USubID);
                            if (USubServices.RecordStatus == 1)
                            {

                                Response.Redirect("/Customer/CloudPackages.aspx?userid=" + USubServices.UserID.ToString() + "&showview=SubscribedService&status=Active", false);
                            }
                            else if (USubServices.RecordStatus == -1)
                            {
                                Response.Redirect("/Customer/CloudPackages.aspx?userid=" + USubServices.UserID.ToString() + "&showview=SubscribedService&status=Cancelled", false);

                            }
                            else
                            {
                                Response.Redirect("/Customer/CloudPackages.aspx?userid=" + USubServices.UserID.ToString() + "&showview=SubscribedService&status=Expired", false);
                            }
                        }

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void btnPaymentSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCouponCode.Text != "")
                    ApplyCoupon();
                string Services = "";
                int i = 0;
                decimal Amount = Convert.ToDecimal(lblTotalPrice.Text.Replace("Amount Payable : $ ", ""));
                if (chkAgree.Checked)
                {
                    if (Amount > 0M)
                    {
                        UserMembershipID = Request.QueryString["userid"];
                        string EnteredcreditCardNo = txtCreditCardNumber.Text;
                        int length = EnteredcreditCardNo.Length;
                        string CreditCardNo = "";
                        Guid ID = new Guid(UserMembershipID);
                        var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                        string UserFullName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                        int UserProfileID = Convert.ToInt32(user.UserProfileID);
                        string AuthProfileID = "";
                        string AuthBillingAddressID = "";
                        string AuthPaymentProfileID = "";
                        string ProfileName = "WFTUSR-" + user.UserProfileID;
                        AuthorizeNet.Address BillingAddress = new AuthorizeNet.Address();
                        string DefaultCreditCardDetails = "";
                        int ExpMonth = 0;
                        int ExpYear = 0;
                        if (ddlExistingCards.SelectedValue == "New Credit Card")
                        {
                            ExpYear = Convert.ToInt32(ddlExpYear.SelectedValue);
                            ExpMonth = Convert.ToInt32(ddlExpMonth.SelectedValue);

                            if ((ExpMonth < DateTime.Now.Month) && (ExpYear == DateTime.Now.Year))
                            {
                                divPaymentfailed1.Visible = true;
                                divPaymentSuccess1.Visible = false;
                                lblPaymentFailed1.Text = "Credit card is expired. Please submit a new one.";
                                return;
                            }

                            BillingAddress.City = txtCity.Text;
                            BillingAddress.Country = ddlCountry.SelectedItem.Text;
                            BillingAddress.First = txtNameOnCard.Text;
                            BillingAddress.Last = " ";
                            BillingAddress.Phone = txtContactNumberPtDet.Text;
                            BillingAddress.State = ddlStateName.SelectedItem.Text;
                            BillingAddress.Street = txtAddress1.Text + "\n" + txtAddress2.Text;
                            BillingAddress.Zip = txtPostalZipCode.Text;
                        }

                        CreditCardNo = divNewCardDetails.Visible == true ? txtCreditCardNumber.Text.Substring(txtCreditCardNumber.Text.Length - 4, 4) : ddlExistingCards.SelectedItem.Text.Substring(ddlExistingCards.SelectedItem.Text.Length - 4, 4);

                        bool PaymentprofileExist = false;
                        int PaymentProfileIndex;
                        #region Get Authorize profile details
                        var CustPayProf = objDBContext.CustomerPaymentProfiles.Where(cpp => cpp.UserProfileID == user.UserProfileID && cpp.Status == true).ToList();
                        if (ddlExistingCards.SelectedValue == "New Credit Card")
                        {
                            if (CustPayProf.Count() == 0)
                            {
                                AuthProfileID = ReusableRoutines.AuthorizeCreateCustomerProfile(user.EmailID, ProfileName, UserFullName);
                                if (AuthProfileID == "Failed")
                                {
                                    divPaymentfailed.Visible = true;
                                    divPaymentSuccess.Visible = false;
                                    lblPaymentFailed.Text = "Error occurred Please try again.";
                                    return;
                                }
                                AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress);
                            }
                            else
                            {
                                AuthProfileID = CustPayProf.FirstOrDefault().AuthCustomerProfileID;
                                string DefalutPaymentProfileID = CustPayProf.FirstOrDefault(A => A.DefaultPaymentID == true) != null ? CustPayProf.FirstOrDefault(A => A.DefaultPaymentID == true).AuthPaymentProfileID : "";
                                var customer = objGW.GetCustomer(AuthProfileID);
                                if (customer != null)
                                {

                                    if (customer.PaymentProfiles.Count() != 0)
                                    {
                                        var s = customer.PaymentProfiles.FirstOrDefault(A => A.ProfileID == DefalutPaymentProfileID) != null ? customer.PaymentProfiles.FirstOrDefault(A => A.ProfileID == DefalutPaymentProfileID).CardNumber.ToString() : "";
                                        if (s != "")
                                        {
                                            DefaultCreditCardDetails = "XXXXXXXX" + s + "(" + DefalutPaymentProfileID + ")";
                                        }
                                        i = 0;
                                        foreach (var pymtPrf in customer.PaymentProfiles)
                                        {
                                            //if ((customer.PaymentProfiles[i].CardNumber.Contains(CreditCardNo) && customer.PaymentProfiles[i].BillingAddress == BillingAddress) == true)
                                            if ((customer.PaymentProfiles[i].CardNumber.Contains(CreditCardNo)) == true)
                                            {
                                                bool defalutcard = false;

                                                if (divExistingCrdCardList.Visible == true)
                                                {
                                                    //  int CustomerPaymenProfileID =Convert.ToInt32(cpjhgp.FirstOrDefault().CustomerPaymenProfileID);
                                                    var deleteOldestRegistered = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == rblCreditCard.SelectedValue);//CustomerPaymenProfileID == CustomerPaymenProfileID);
                                                    if (deleteOldestRegistered != null)
                                                    {
                                                        defalutcard = Convert.ToBoolean(deleteOldestRegistered.DefaultPaymentID);
                                                        deleteOldestRegistered.DefaultPaymentID = false;
                                                        deleteOldestRegistered.Status = false;
                                                        objDBContext.SaveChanges();
                                                    }
                                                }
                                                PaymentProfileIndex = i;
                                                PaymentprofileExist = true;
                                                AuthPaymentProfileID = customer.PaymentProfiles[i].ProfileID;
                                                var ChangePaymentCardStatus = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == AuthPaymentProfileID);
                                                if (ChangePaymentCardStatus != null)
                                                {
                                                    if (ChangePaymentCardStatus.DefaultPaymentID != true)
                                                    {
                                                        if (defalutcard == true)
                                                            ChangePaymentCardStatus.DefaultPaymentID = true;
                                                    }
                                                    ChangePaymentCardStatus.Status = true;
                                                    objDBContext.SaveChanges();
                                                    if (ChangePaymentCardStatus.DefaultPaymentID == true)
                                                    {
                                                        makeOtherCardsDefaultpaymentcardFalse(UserProfileID, AuthPaymentProfileID);
                                                    }
                                                }
                                                else
                                                {
                                                    CustomerPaymentProfile Cpp = new CustomerPaymentProfile();
                                                    Cpp.AuthBillingAddressID = customer.PaymentProfiles[i].BillingAddress.ID;
                                                    Cpp.AuthCustomerProfileID = AuthProfileID;
                                                    Cpp.AuthPaymentProfileID = AuthPaymentProfileID;
                                                    Cpp.DefaultPaymentID = false;
                                                    if (defalutcard == true)
                                                    {
                                                        Cpp.DefaultPaymentID = true;
                                                    }
                                                    Cpp.Status = true;
                                                    Cpp.UserProfileID = UserProfileID;
                                                    objDBContext.CustomerPaymentProfiles.AddObject(Cpp);
                                                    objDBContext.SaveChanges();
                                                    if (Cpp.DefaultPaymentID == true)
                                                    {
                                                        makeOtherCardsDefaultpaymentcardFalse(UserProfileID, AuthPaymentProfileID);
                                                        DefaultCreditCardDetails = "XXXXXXXXXXXX" + CreditCardNo + "(" + AuthPaymentProfileID + ")";
                                                    }
                                                }
                                            }
                                            i++;
                                        }
                                        //if (PaymentprofileExist == false && i > 2)
                                        //{
                                        //    rblCardList.data
                                        //    mpopUpCardList.Show();
                                        //    divPaymentfailed1.Visible = true;
                                        //    lblPaymentFailed1.Text = "Only 3 credit cards can be used.Please try with the registered credit card.";
                                        //    return;
                                        //}
                                        if (PaymentprofileExist == false)
                                        {
                                            AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress);
                                        }
                                    }
                                    else
                                    {
                                        AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress);
                                    }
                                }
                                else
                                {
                                    AuthProfileID = ReusableRoutines.AuthorizeCreateCustomerProfile(user.EmailID, ProfileName, UserFullName);
                                    if (AuthProfileID == "Failed")
                                    {
                                        divPaymentfailed.Visible = true;
                                        divPaymentSuccess.Visible = false;
                                        lblPaymentFailed.Text = "Error occurred Please try again.";
                                        return;
                                    }
                                    AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress);
                                }
                            }
                        }
                        else
                        {
                            AuthProfileID = CustPayProf.FirstOrDefault().AuthCustomerProfileID;
                            AuthPaymentProfileID = ddlExistingCards.SelectedValue;
                        }
                        #endregion
                        var InvoiceNumber = objDBContext.pr_GetNewInvoiceNumber();
                        string WFTInvoiceNumber = Convert.ToString(InvoiceNumber.First());

                        UserOrder NewOrder = new UserOrder();
                        NewOrder.OrderDateTime = DateTime.Now;
                        NewOrder.OrderStatus = 999; //
                        NewOrder.IsCouponCode = hidCouponCode.Value == "" ? null : hidCouponCode.Value;
                        if (hidDisCountValue.Value != "")
                            NewOrder.IsDiscountValue = Convert.ToDecimal(hidDisCountValue.Value);

                        NewOrder.OrderTotal = Amount;
                        NewOrder.UserProfileID = UserProfileID;
                        NewOrder.AuthCustomerProfileID = AuthProfileID;
                        NewOrder.AuthPaymentProfileID = AuthPaymentProfileID;
                        NewOrder.InvoiceNumber = WFTInvoiceNumber;
                        NewOrder.PaymentMethod = "Authorize.net";
                        objDBContext.UserOrders.AddObject(NewOrder);
                        objDBContext.SaveChanges();
                        int NewUserOrderID = objDBContext.UserOrders.Max(a => a.UserOrderID);
                        foreach (RepeaterItem rItem in rptrMyCart.Items)
                        {
                            i += 1;
                            decimal InitialHoldAmount = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                            int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                            int UserCartID = Convert.ToInt32((rItem.FindControl("hidUserCartID") as HiddenField).Value);
                            int Qty = Convert.ToInt32((rItem.FindControl("lblQty") as TextBox).Text);
                            int selecteddiscountduration = Convert.ToInt32((rItem.FindControl("ddlDiscountType") as DropDownList).SelectedValue);
                            UserOrderDetail NewUserOrderDetail = new UserOrderDetail();
                            NewUserOrderDetail.InitialHoldAmount = InitialHoldAmount;
                            NewUserOrderDetail.ServiceID = ServiceID;
                            NewUserOrderDetail.UserOrderID = NewUserOrderID;
                            NewUserOrderDetail.Quantity = Qty;
                            NewUserOrderDetail.OrderCartID = UserCartID;
                            if (selecteddiscountduration == 0)
                            {
                                NewUserOrderDetail.SelectedDiscountDuration = 0;
                            }
                            else if (selecteddiscountduration == 1)
                            {
                                NewUserOrderDetail.SelectedDiscountDuration = 3;
                            }
                            else if (selecteddiscountduration == 2)
                            {
                                NewUserOrderDetail.SelectedDiscountDuration = 6;
                            }
                            else if (selecteddiscountduration == 3)
                            {
                                NewUserOrderDetail.SelectedDiscountDuration = 9;
                            }
                            else
                            {
                                NewUserOrderDetail.SelectedDiscountDuration = 12;
                            }
                            objDBContext.UserOrderDetails.AddObject(NewUserOrderDetail);
                            objDBContext.SaveChanges();
                            Services += (i + "." + ServiceName((rItem.FindControl("hdnServiceID") as HiddenField).Value) + "  \n");
                        }

                        string PaymentResponse = ReusableRoutines.AuthorizeBillClient(
                            AuthProfileID,
                            AuthPaymentProfileID,
                            Amount,
                            WFTInvoiceNumber,
                            (WFTInvoiceNumber + " - " + user.EmailID + ",\n Services : " + "\n" + Services),
                            UserProfileID);

                        int UserOrderID = NewUserOrderID;
                        var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == UserOrderID);
                        lblOrderCode.Text = "Order" + UserOrderID.ToString();
                        lblOrderDate.Text = Convert.ToDateTime(UserOrder.OrderDateTime).ToString("dd-MMM-yyyy").ToUpper();
                        lblOrdeAmount.Text = "$ " + UserOrder.OrderTotal.ToString();
                        lblCouponUsed.Text = UserOrder.IsCouponCode == null ? "Not Applied" : "Applied";
                        if (lblCouponUsed.Text == "Applied")
                        {
                            trCouponCode.Visible = trDiscountedAmount.Visible = true;
                            lblCouponCode.Text = UserOrder.IsCouponCode;
                            trDiscountedAmount.Visible = UserOrder.IsDiscountValue == null ? false : true;
                            lblDisCountAmount.Text = "$ " + UserOrder.IsDiscountValue.ToString();
                        }
                        else
                        {
                            trCouponCode.Visible = trDiscountedAmount.Visible = false;
                        }
                        rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == UserOrderID);
                        rptrOrderDetails.DataBind();
                        lblInvoiceNumber.Text = WFTInvoiceNumber;
                        var customer2 = objGW.GetCustomer(AuthProfileID);
                        var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == WFTInvoiceNumber);
                        lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
                        lblPaymentAmount.Text = "$ " + PaymentDetails.Amount.ToString();
                        lblPaymentMessage.Text = PaymentDetails.AuthMessage;
                        lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";
                        lblCreditCardnumber.Text = "XXXXXXXX" + (PaymentDetails.AuthCardNumber == null || PaymentDetails.AuthCardNumber == "" ? customer2.PaymentProfiles.FirstOrDefault(a => a.ProfileID == AuthPaymentProfileID).CardNumber : PaymentDetails.AuthCardNumber);
                        zMycart.Visible = false;
                        zOrderDetails.Visible = true;

                        string[] Response = Regex.Split(PaymentResponse, ":");
                        //****************** ResponseCode ************************//
                        //   1 This transaction has been approved. 
                        //   2 This transaction has been declined. 
                        //   3 There has been an error processing this transaction. 
                        //   4 This transaction is being held for review

                        if (Response[0] == "2" || Response[0] == "3" || Response[0] == "4")
                        {
                            string ServiceDEtails = "<table style='width:100%; height:auto;'>" +
                            "<tr><td style='width:49%;'>User Name</td><td style='width:2%;'>:</td><td style='width:49%;'>" + UserFullName + "</td></tr>" +
                            "<tr><td style='width:49%;'>User EmailID</td><td style='width:2%;'>:</td><td style='width:49%;'>" + user.EmailID + "</td></tr>" +
                            "<tr><td style='width:49%;'>Order Number</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrderCode.Text + "</td></tr>" +
                            "<tr><td style='width:49%;'>Order Date</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrderDate.Text + "</td></tr>" +
                            "<tr><td style='width:49%;'>Amount</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrdeAmount.Text + "</td></tr>" +
                            "<tr><td style='width:49%;'>Coupon Used</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblCouponUsed.Text + "</td></tr>" +
                            "</table>" +
                            "<h4 style='color:#0c87c7'>Order Details</h4>" +
                            "<table style='width:100%; height:auto;'>" +
                            "<tr><td><b>S.No</b></td><td><b>Category Name</b></td><td><b>Service Name</b></td><td><b>Quantity</b></td><td><b>Initial hold Amount</b></td></tr>";
                            var mycarts = objDBContext.UserCarts.Where(A => A.UserProfileID == UserOrder.UserProfileID && A.RecordStatus == 999);
                            int s = 0;
                            foreach (var result in mycarts)
                            {
                                s += 1;
                                ServiceDEtails += "<tr><td style='text-align:center;'>" + s + "</td><td>" + CategoryName(result.ServiceID.ToString()) + "</td><td>" + ServiceName(result.ServiceID.ToString()) + "</td><td  style='text-align:center;'>" + result.Quantity + "</td><td style='text-align:center;'> $ " +
                                         ServicePrice(result.ServiceID.ToString(), result.Quantity.ToString(), (result.SelectedDiscount == null ? "0" : result.SelectedDiscount.ToString())) + "</td></tr>";
                            }
                            ServiceDEtails += "</table>" +
                                              "<h4 style='color:#0c87c7'>Payment Transaction Details</h4>" +
                                              "<table style='width:100%; height:auto;'>" +
                                              "<tr><td style='width:49%;'>Invoice Number</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblInvoiceNumber.Text + "</td></tr>" +
                                              "<tr><td style='width:49%;'>Payment Date</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblPaymentDate.Text + "</td></tr>" +
                                              "<tr><td style='width:49%;'>Amount</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblPaymentAmount.Text + "</td></tr>" +
                                              "<tr><td style='width:49%;'>Mode</td><td style='width:2%;'>:</td><td style='width:49%;'>" + (PaymentDetails.PaymentMethod == "PayPal" ? "PayPal" : "Authorize.net") + "</td></tr>" +
                                              "</table>";


                            string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["PaymentFailedOnPurchaseNotificationToAdmin"]));
                            EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            EmailContent = EmailContent.Replace("++name++", " " + UserFullName);
                            string AdminContent = EmailContent.Replace("++AddContentHere++", ServiceDEtails);
                            SMTPManager.SendAdminNotificationEmail(AdminContent, "Payment failure notification of Customer : " + UserFullName, false);
                            
                        }

                        if (Response[0] == "1")
                        {
                            string NewWorkLog = "";
                            if (DefaultCreditCardDetails != "" && rblCreditCard.SelectedItem != null && rblCreditCard.SelectedValue != AuthPaymentProfileID)
                            {
                                if (rblCreditCard.SelectedItem.Text.Contains("Default Credit Card"))
                                {
                                    NewWorkLog = " Authorize.net Credit Card Payment Profile ID for all future transaction : Changed From " + rblCreditCard.SelectedItem.Text + "(" + rblCreditCard.SelectedValue + ") To XXXXXXXXXXXX" + CreditCardNo + "(" + AuthPaymentProfileID + ")";
                                    objDBContext.pr_UpdateAllPaymentProfileIDAndWorklog(rblCreditCard.SelectedValue, UserFullName + "(" + user.EmailID + ")", NewWorkLog, user.UserProfileID);
                                }
                                else
                                {
                                    NewWorkLog = " Authorize.net Credit Card Payment Profile ID for all future transaction : Changed From " + rblCreditCard.SelectedItem.Text + "(" + rblCreditCard.SelectedValue + ") To " + DefaultCreditCardDetails;
                                    objDBContext.pr_UpdateAllPaymentProfileIDAndWorklog(rblCreditCard.SelectedValue, UserFullName + "(" + user.EmailID + ")", NewWorkLog, user.UserProfileID);
                                }
                            }
                            string ServiceNameDEtails = "";
                            //ServiceNameDEtails += "<strong>User Name :</strong> " + UserFullName + "<br />"
                            //                      + "<strong>User EmailID :</strong> " + user.EmailID + "<br /><br />";
                            if (txtCouponCode.Text != "")
                                ApplyCouponForCount();
                            foreach (RepeaterItem rItem in rptrMyCart.Items)
                            {

                                int UserCartID = Convert.ToInt32((rItem.FindControl("hidUserCartID") as HiddenField).Value);
                                var mycarts = objDBContext1.UserCarts.FirstOrDefault(A => A.UserCartID == UserCartID);
                                mycarts.RecordStatus = DBKeys.RecordStatus_Active;
                                objDBContext1.SaveChanges();
                                var serviceDetails = objDBContext1.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == mycarts.ServiceID);
                                var categoryDetails = objDBContext1.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == serviceDetails.ServiceCategoryID);
                                //ServiceNameDEtails += "<strong>Service ID :</strong> " + serviceDetails.ServiceID + "<br />"
                                //               + "<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                //               + "<strong>System Type :</strong> " + serviceDetails.SystemType + "<br />"
                                //               + "<strong>Release Version :</strong> " + serviceDetails.ReleaseVersion + "<br /><br />";


                                ServiceNameDEtails = ("<table border='1'><tr><td rowspan='5'><strong>User Information</strong></td>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>Email ID</strong></td><td>" + user.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Location </strong></td><td>" + user.Location + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td rowspan='4'><strong>Service Information</strong></td><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");


                            }
                            var UpdateStatusApproved = objDBContext1.UserOrders.FirstOrDefault(a => a.UserOrderID == NewUserOrderID);
                            UpdateStatusApproved.OrderStatus = 1;
                            objDBContext1.SaveChanges();
                            objDBContext.Refresh(System.Data.Objects.RefreshMode.ClientWins, objDBContext.UserOrders);
                            divPaymentfailed.Visible = false;
                            divPaymentSuccess.Visible = true;
                            lblPaymentSuccess.Text = Response[1];// "This transaction has been approved. ";

                            //*******
                            //Sent Email Notification to the admin about new order 
                            //******


                            string message = (("<strong>" + user.FirstName + " " + user.MiddleName + " " + user.LastName + "</strong>"));


                            string EmailContent1 = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewServiceRequestToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/new-service-request.html"));
                            EmailContent1 = EmailContent1.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            string AdminContent1 = EmailContent1.Replace("++AddContentHere++", ServiceNameDEtails).Replace("++name++", message);
                            SMTPManager.SendAdminNotificationEmail(AdminContent1, "New Order " + lblInvoiceNumber.Text, false);
                            SMTPManager.SendSAPBasisNotificationEmail(AdminContent1, "New Order " + lblInvoiceNumber.Text, false);

                           

                            //Insert Records Into User Service Provision
                            BuildSubscribedServices(NewUserOrderID);
                            FreshServiceUserCreation();

                        }
                        else if (Response[0] == "2")
                        {
                            var UpdateStatusFailed = objDBContext1.UserOrders.FirstOrDefault(a => a.UserOrderID == NewUserOrderID);
                            UpdateStatusFailed.OrderStatus = 0;
                            objDBContext1.SaveChanges();
                            divPaymentfailed.Visible = true;
                            divPaymentSuccess.Visible = false;
                            lblPaymentFailed.Text = Response[1]; //"This transaction has been declined. ";
                        }
                        else if (Response[0] == "3")
                        {
                            var UpdateStatusFailed = objDBContext1.UserOrders.FirstOrDefault(a => a.UserOrderID == NewUserOrderID);
                            UpdateStatusFailed.OrderStatus = 0;
                            objDBContext1.SaveChanges();
                            divPaymentfailed.Visible = true;
                            divPaymentSuccess.Visible = false;
                            lblPaymentFailed.Text = Response[1]; //"There has been an error processing this transaction. ";
                        }
                        else if (Response[0] == "4")
                        {
                            var UpdateStatusFailed = objDBContext1.UserOrders.FirstOrDefault(a => a.UserOrderID == NewUserOrderID);
                            UpdateStatusFailed.OrderStatus = 0;
                            objDBContext1.SaveChanges();
                            divPaymentfailed.Visible = true;
                            divPaymentSuccess.Visible = false;
                            lblPaymentFailed.Text = Response[1];//"This transaction is being held for review";
                        }
                        MyCartDetails();
                    }
                    else if (Amount == 0M)
                    {
                        UserMembershipID = Request.QueryString["userid"];
                        string EnteredcreditCardNo = txtCreditCardNumber.Text;
                        int length = EnteredcreditCardNo.Length;
                        //string CreditCardNo = "";
                        Guid ID = new Guid(UserMembershipID);
                        var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                        string UserFullName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                        int UserProfileID = Convert.ToInt32(user.UserProfileID);
                        string AuthProfileID = "";
                        //string AuthBillingAddressID = "";
                        string AuthPaymentProfileID = "";
                        string ProfileName = "WFTUSR-" + user.UserProfileID;
                        AuthorizeNet.Address BillingAddress = new AuthorizeNet.Address();
                        //string DefaultCreditCardDetails = "";
                        //int ExpMonth = 0;
                        //int ExpYear = 0;




                        var InvoiceNumber = objDBContext.pr_GetNewInvoiceNumber();
                        string WFTInvoiceNumber = Convert.ToString(InvoiceNumber.First());

                        UserOrder NewOrder = new UserOrder();
                        NewOrder.OrderDateTime = DateTime.Now;
                        NewOrder.OrderStatus = 999; //
                        NewOrder.IsCouponCode = hidCouponCode.Value == "" ? null : hidCouponCode.Value;
                        if (hidDisCountValue.Value != "")
                            NewOrder.IsDiscountValue = Convert.ToDecimal(hidDisCountValue.Value);

                        NewOrder.OrderTotal = Amount;
                        NewOrder.UserProfileID = UserProfileID;
                        NewOrder.AuthCustomerProfileID = AuthProfileID;
                        NewOrder.AuthPaymentProfileID = AuthPaymentProfileID;
                        NewOrder.InvoiceNumber = WFTInvoiceNumber;
                        NewOrder.PaymentMethod = "NULL";
                        objDBContext.UserOrders.AddObject(NewOrder);
                        objDBContext.SaveChanges();
                        int NewUserOrderID = objDBContext.UserOrders.Max(a => a.UserOrderID);
                        foreach (RepeaterItem rItem in rptrMyCart.Items)
                        {
                            i += 1;
                            decimal InitialHoldAmount = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                            int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                            int UserCartID = Convert.ToInt32((rItem.FindControl("hidUserCartID") as HiddenField).Value);
                            int Qty = Convert.ToInt32((rItem.FindControl("lblQty") as TextBox).Text);
                            int selecteddiscountduration = Convert.ToInt32((rItem.FindControl("ddlDiscountType") as DropDownList).SelectedValue);
                            UserOrderDetail NewUserOrderDetail = new UserOrderDetail();
                            NewUserOrderDetail.InitialHoldAmount = InitialHoldAmount;
                            NewUserOrderDetail.ServiceID = ServiceID;
                            NewUserOrderDetail.UserOrderID = NewUserOrderID;
                            NewUserOrderDetail.Quantity = Qty;
                            NewUserOrderDetail.OrderCartID = UserCartID;
                            NewUserOrderDetail.SelectedDiscountDuration = selecteddiscountduration;
                            objDBContext.UserOrderDetails.AddObject(NewUserOrderDetail);
                            objDBContext.SaveChanges();
                            Services += (i + "." + ServiceName((rItem.FindControl("hdnServiceID") as HiddenField).Value) + "  \n");
                        }



                        int UserOrderID = NewUserOrderID;
                        var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == UserOrderID);
                        lblOrderCode.Text = "Order" + UserOrderID.ToString();
                        lblOrderDate.Text = Convert.ToDateTime(UserOrder.OrderDateTime).ToString("dd-MMM-yyyy").ToUpper();
                        lblOrdeAmount.Text = "$ " + UserOrder.OrderTotal.ToString();
                        lblCouponUsed.Text = UserOrder.IsCouponCode == null ? "Not Applied" : "Applied";
                        if (lblCouponUsed.Text == "Applied")
                        {
                            trCouponCode.Visible = trDiscountedAmount.Visible = true;
                            lblCouponCode.Text = UserOrder.IsCouponCode;
                            trDiscountedAmount.Visible = UserOrder.IsDiscountValue == null ? false : true;
                            lblDisCountAmount.Text = "$ " + UserOrder.IsDiscountValue.ToString();
                        }
                        else
                        {
                            trCouponCode.Visible = trDiscountedAmount.Visible = false;
                        }
                        rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == UserOrderID);
                        rptrOrderDetails.DataBind();
                        lblInvoiceNumber.Text = WFTInvoiceNumber;
                        //var customer2 = objGW.GetCustomer(AuthProfileID);
                        //var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == WFTInvoiceNumber);
                        lblPaymentDate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy").ToUpper();
                        lblPaymentAmount.Text = "$ " + Amount;
                        lblPaymentMessage.Text = "Success";
                        lblAmountPaid.Text = "Paid";
                        lblCreditCardnumber.Text = "XXXXXXXX";
                        zMycart.Visible = false;
                        zOrderDetails.Visible = true;


                        if (NewUserOrderID > 0)
                        {
                            string ServiceNameDEtails = "";
                            //ServiceNameDEtails += "<strong>User Name :</strong> " + UserFullName + "<br />"
                            //                      + "<strong>User EmailID :</strong> " + user.EmailID + "<br /><br />";
                            if (txtCouponCode.Text != "")
                                ApplyCouponForCount();
                            foreach (RepeaterItem rItem in rptrMyCart.Items)
                            {

                                int UserCartID = Convert.ToInt32((rItem.FindControl("hidUserCartID") as HiddenField).Value);
                                var mycarts = objDBContext1.UserCarts.FirstOrDefault(A => A.UserCartID == UserCartID);
                                mycarts.RecordStatus = DBKeys.RecordStatus_Active;
                                objDBContext1.SaveChanges();
                                var serviceDetails = objDBContext1.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == mycarts.ServiceID);
                                var categoryDetails = objDBContext1.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == serviceDetails.ServiceCategoryID);
                                //ServiceNameDEtails += "<strong>Service ID :</strong> " + serviceDetails.ServiceID + "<br />"
                                //               + "<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                //               + "<strong>System Type :</strong> " + serviceDetails.SystemType + "<br />"
                                //               + "<strong>Release Version :</strong> " + serviceDetails.ReleaseVersion + "<br /><br />";

                                ServiceNameDEtails = ("<table border='1'><tr><td rowspan='5'><strong>User Information</strong></td>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>Email ID</strong></td><td>" + user.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Location </strong></td><td>" + user.Location + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td rowspan='4'><strong>Service Information</strong></td><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");

                            }
                            var UpdateStatusApproved = objDBContext1.UserOrders.FirstOrDefault(a => a.UserOrderID == NewUserOrderID);
                            UpdateStatusApproved.OrderStatus = 1;
                            objDBContext1.SaveChanges();
                            objDBContext.Refresh(System.Data.Objects.RefreshMode.ClientWins, objDBContext.UserOrders);
                            divPaymentfailed.Visible = false;
                            divPaymentSuccess.Visible = true;
                            lblPaymentSuccess.Text = "This transaction has been approved. ";

                            //*******
                            //Sent Email Notification to the admin about new order 
                            //******


                            string message = (("<strong>" + user.FirstName + " " + user.MiddleName + " " + user.LastName + "</strong>"));


                            string EmailContent1 = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewServiceRequestToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/new-service-request.html"));
                            EmailContent1 = EmailContent1.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            string AdminContent1 = EmailContent1.Replace("++AddContentHere++", ServiceNameDEtails).Replace("++name++", message);
                            SMTPManager.SendAdminNotificationEmail(AdminContent1, "New Order " + lblInvoiceNumber.Text, false);
                            SMTPManager.SendSAPBasisNotificationEmail(AdminContent1, "New Order " + lblInvoiceNumber.Text, false);

                            //Insert Records Into User Service Provision
                            BuildSubscribedServices(NewUserOrderID);

                        }

                        MyCartDetails();
                    }
                    else
                    {
                        divPaymentfailed1.Visible = true;
                        lblPaymentFailed1.Text = "Invalid Amount";
                    }
                }
                else
                {
                    divRegisterError.Visible = true;
                    lblRegisterError.Text = "Please accept Terms and Conditions.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divPaymentfailed.Visible = true;
                divPaymentSuccess.Visible = false;
                divPaymentfailed1.Visible = true;
                divPaymentSuccess1.Visible = false;
                lblPaymentFailed1.Text = Ex.Message;
                lblPaymentFailed.Text = "Error occurred Please try again.";
            }
        }
        private void SendEmail(string messageBody, string subject, string ToMail, bool sendInBCC, bool IsHtml)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = IsHtml;

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }
        private void FreshServiceUserCreation()
        {
            try
            {
                string FreshServiceurl = WebConfigurationManager.AppSettings["TicketToolUserCreationwebURL"].ToString();
                string TicketToolAdminInfo = WebConfigurationManager.AppSettings["TicketToolAdminInfo"].ToString();
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                String UserName = user.FirstName + user.LastName;
                string UserEmail = user.EmailID;
                string data = "{\"user\":{ \"name\":\"" + UserName + "\", \"email\":\"" + UserEmail + "\" }}";

                WebRequest myReq = WebRequest.Create(FreshServiceurl);
                myReq.Method = "POST";
                myReq.ContentLength = data.Length;
                myReq.ContentType = "application/json";//x-www-form-urlencoded";
                UTF8Encoding enc = new UTF8Encoding();
                myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(TicketToolAdminInfo)));
                //myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes("BCaVcz5WwEv7CNluNLKB")));

                using (Stream ds = myReq.GetRequestStream())
                {
                    ds.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
                }

                System.Net.ServicePointManager.Expect100Continue = false;
                //myReq.CookieContainer = cookieContainer; 

                WebResponse wr = myReq.GetResponse();

                //Stream receiveStream = wr.GetResponseStream();
                //StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                //string content = reader.ReadToEnd();

                //Response.Write(content.ToString());
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegister_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCouponCode.Text != "")
                    ApplyCoupon();
                else
                {
                    hidCouponCode.Value = "";
                    hidDisCountValue.Value = "";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divCouponFailed.Visible = true;
                divCouponSuccess.Visible = false;
                lblCouponFailed.Text = "Error occurred while applying Coupon code";
            }
        }

        protected void btnClearDiscount_Click(object sender, EventArgs e)
        {
            hidDisCountValue.Value = hidCouponCode.Value = txtCouponCode.Text = "";
            lblTotalPrice.Text = "Amount Payable : $ " + TotalPrice();
        }

        protected void txtCouponCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCouponCode.Text = txtCouponCode.Text.Trim()[txtCouponCode.Text.Trim().Length - 1] != ',' ? txtCouponCode.Text.Trim() : txtCouponCode.Text.Trim().Remove(txtCouponCode.Text.Trim().Length - 1);
                if (txtCouponCode.Text != "")
                    ApplyCoupon();
                else
                {
                    hidCouponCode.Value = "";
                    hidDisCountValue.Value = "";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbtnBakcToOrderDetails_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
            {
                
                string View = (Request.QueryString[QueryStringKeys.ShowView]);
                if (divPaymentSuccess.Visible==true)
                {
                    Response.Redirect("/Customer/CloudPackages.aspx?userid=" + Request.QueryString["userid"] + "&showview=SubscribedService", false);
                }
                else
                {
                    Response.Redirect("/Customer/CloudPackages.aspx?userid=" + Request.QueryString["userid"] + "&showview=ShowMyCart", false);
                }
            }
        }

        protected void rptrWFTCloudPackagesCategoryName_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                int UserProfileID = Convert.ToInt32(user.UserProfileID);

                var myHidden = (HiddenField)e.Item.FindControl("hdnServiceCategoryID");
                int CatID = Convert.ToInt32(myHidden.Value);
                Repeater rptrWFTCloudPackages = (Repeater)e.Item.FindControl("rptrWFTCloudPackages");
                Repeater rptrWFTServicesWithOutPayAsYouGo = (Repeater)e.Item.FindControl("rptrWFTCloudNonPayAsYouGo");
                List<ServiceCatalog> ServiceCatalogs = new List<ServiceCatalog>();
                
                var CommonServices = objDBContext.ServiceCatalogs.Where(cs => cs.UserSpecific == false && cs.ServiceCategoryID == CatID && cs.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.Priority);
                var LoggedUserSpecificService = objDBContext.ServiceCatalogs.Where(cs => cs.UserSpecific == true && cs.ServiceCategoryID == CatID && cs.RecordStatus == DBKeys.RecordStatus_Active && cs.UserProfileID == UserProfileID).OrderBy(svc => svc.Priority);
                ServiceCatalogs.AddRange(LoggedUserSpecificService.ToArray());
                ServiceCatalogs.AddRange(CommonServices.ToArray());

                bool PayAsYouGoModel = Convert.ToBoolean(objDBContext.ServiceCategories.FirstOrDefault(ct => ct.ServiceCategoryID == CatID).IsPayAsYouGo);
                if (PayAsYouGoModel == true)
                {
                    rptrWFTServicesWithOutPayAsYouGo.Visible = false;
                    rptrWFTCloudPackages.Visible = true;
                    rptrWFTCloudPackages.DataSource = ServiceCatalogs;
                    rptrWFTCloudPackages.DataBind();
                }
                else
                {
                    rptrWFTServicesWithOutPayAsYouGo.Visible = true;
                    rptrWFTCloudPackages.Visible = false;
                    rptrWFTServicesWithOutPayAsYouGo.DataSource = ServiceCatalogs;
                    rptrWFTServicesWithOutPayAsYouGo.DataBind();
                }
            }
        }

        protected void lblQty_TextChanged(object sender, EventArgs e)
        {
            TextBox lblQuantity = ((TextBox)(sender));

            updateMyCartRowData(lblQuantity);
        }

        protected void ddlExistingCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            divExistingCardDetails.Visible = true;
            if (ddlExistingCards.SelectedValue != "New Credit Card")
            {
                FilCreditCardDetails();
                divNewCardDetails.Visible = false;
            }
            else
            {
                FilCreditCardDetails();
                divNewCardDetails.Visible = true;
            }

        }

        protected void btnCancelServices_Click(object sender, EventArgs e)
        {
            try
            {
                string UserMembershipID = Convert.ToString(Session["UserId"]);
                Guid ID = new Guid(UserMembershipID);
                int UserSubscripID = Convert.ToInt32(Session["CancelService"]);
                var UserServ = objDBContext.UserSubscribedServices.FirstOrDefault(ot => ot.UserSubscriptionID == UserSubscripID && ot.UserID == ID && ot.RecordStatus == DBKeys.RecordStatus_Active);
                var UserProfile = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                if (UserServ != null)
                {
                    if (UserServ.ARBSubscriptionId != "0" && UserServ.ARBSubscriptionId.IsValid())
                    {
                        long ARBSubID = Convert.ToInt64(UserServ.ARBSubscriptionId);
                        string response = ReusableRoutines.ARBCancelSubscription(ARBSubID);
                        if (response == "Failed")
                        {
                            divSubscribedFailed.Visible = true;
                            divSubscribedSuccess.Visible = false;
                            lblSubscribedfailed.Text = "Cancelling Subscribed service has failed .Please contact admin to Unsubscribe the service.";
                            return;
                        }
                    }
                    var UnSubscribe = objDBContext.pr_CancelUserSubscription(UserServ.UserSubscriptionID, UserProfile.EmailID, txtReasons.Text, txtFeedbacks.Text, rblServiceRating.SelectedValue);
                    var orderDetails = objDBContext.UserOrders.FirstOrDefault(a => a.UserOrderID == UserServ.UserOrderID);
                    var upt = objDBContext.UserPaymentTransactions.FirstOrDefault(o => o.InvoiceNumber == orderDetails.InvoiceNumber);
                    if (upt.AuthMessage == "PayPal")
                    {
                        string reasons1 = ((txtReasons.Text != null ? (txtReasons.Text != "" ? txtReasons.Text : " - ") : " - " + "<br />"));
                        string feedback1 = ((txtFeedbacks.Text != null ? (txtFeedbacks.Text != "" ? txtFeedbacks.Text : " - ") : " - " + "<br />"));
                        string Username1 = UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName;
                        string EmailID = UserProfile.EmailID;
                        string CanceledDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                        string Category = objDBContext.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == UserServ.ServiceCategoryID).CategoryName;
                        string Service = ServiceName(UserServ.ServiceID.ToString());
                        string EmailContent1 = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceCancelledBuyViaPayPal"]));
                        EmailContent1 = EmailContent1.Replace("++UserName++", Username1).Replace("++UserEmailID++", EmailID).Replace("++Category++", Category).Replace("++Service++", Service).Replace("++CancelledDate++", CanceledDate).Replace("++CancelledReason++", reasons1).Replace("++Feedback++", feedback1).Replace("++RecurringPaymentsProfileID++", upt.AuthResponseCode).Replace("++TransactionID++",upt.AuthTransactionID);
                        EmailContent1 = EmailContent1.Replace("++SubscriptionID++", UserServ.UserSubscriptionID.ToString()).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        SMTPManager.SendAdminNotificationEmail(EmailContent1, "Attention!!! Requires immediate action on a cancelled Service", false);
                        SMTPManager.SendSAPBasisNotificationEmail(EmailContent1, "Attention!!! Requires immediate action on a cancelled Service", false);
                        SMTPManager.SendSupportNotificationEmail(EmailContent1, "Attention!!! Requires immediate action on a cancelled Service", false);
                    }
                    ShowSubscribedRecords();
                    divSubscribedFailed.Visible = false;
                    divSubscribedSuccess.Visible = true;
                    lblSubscribedSuccess.Text = "Subscribed Service cancelled successfully.";

                    //string UnsubscribeDetails = "";

                    int serv = UserServ.ServiceID;
                    int Ctgry = UserServ.ServiceCategoryID;
                    var service = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serv);
                    var category = objDBContext.ServiceCategories.FirstOrDefault(c => c.ServiceCategoryID == Ctgry);
                    var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == Ctgry && sp.ServiceID == serv);
                    string message = ((UserProfile.LastName + "   " + UserProfile.FirstName));
                    string UserID = ((UserProfile.UserProfileID + "<br />"));
                    string Username = ((UserProfile.EmailID + "<br />"));
                    string servicetype = ((service.UsageUnit + "<br />"));
                    string servicename = ((service.ServiceName + "<br />"));
                    string InstanceNumber = string.Empty;
                    string ApplicationServer = string.Empty;
                    string SID = string.Empty;

                    string servicecategoryname = ((category.CategoryName + "<br />"));
                    string serviceusername = ((UserServ.ServiceUserName != null ? (UserServ.ServiceUserName != "" ? UserServ.ServiceUserName : " - ") : " - " + "<br />"));
                    string serviceurl = (UserServ.ServiceUrl != null ? (UserServ.ServiceUrl != "" ? ("<a target='_blank' href='" + UserServ.ServiceUrl + "'>" + UserServ.ServiceUrl + "</a>") : " N/A ") : " N/A ");
                    string serviceotherinformation = ((UserServ.ServiceOtherInformation != null ? (UserServ.ServiceOtherInformation != "" ? UserServ.ServiceOtherInformation : " N/A ") : " N/A " + "<br />"));
                    string UIDOnServer = UserServ.UIDOnServer != null ? (UserServ.UIDOnServer != "" ? UserServ.UIDOnServer : " N/A ") : " N/A ";
                    string ActivatedDate = Convert.ToDateTime(UserServ.ActiveDate).ToString("dd-MMM-yyyy");

                    string reasons = ((txtReasons.Text != null ? (txtReasons.Text != "" ? txtReasons.Text : " - ") : " - " + "<br />"));
                    string feedback = ((txtFeedbacks.Text != null ? (txtFeedbacks.Text != "" ? txtFeedbacks.Text : " - ") : " - " + "<br />"));


                    var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == serv);
                    if (ServiceProvisioningCheck.Count() > 0)
                    {
                        InstanceNumber = ((ServiceProvisioningDEtails.InstanceNumber != null ? (ServiceProvisioningDEtails.InstanceNumber != "" ? ServiceProvisioningDEtails.InstanceNumber : " N/A ") : " N/A "));
                        ApplicationServer = ((ServiceProvisioningDEtails.ApplicationServer != null ? (ServiceProvisioningDEtails.ApplicationServer != "" ? ServiceProvisioningDEtails.ApplicationServer : " N/A ") : " N/A "));
                        SID = ((ServiceProvisioningDEtails.UIDOnServer != null ? (ServiceProvisioningDEtails.UIDOnServer != "" ? ServiceProvisioningDEtails.UIDOnServer : " N/A ") : " N/A "));
                    }
                    else
                    {
                        InstanceNumber = ((UserServ.InstanceNumber != null ? (UserServ.InstanceNumber != "" ? UserServ.InstanceNumber : " N/A ") : " N/A "));
                        ApplicationServer = ((UserServ.ApplicationServer != null ? (UserServ.ApplicationServer != "" ? UserServ.ApplicationServer : " N/A ") : " N/A "));
                        SID = ((UserServ.UIDOnServer != null ? (UserServ.UIDOnServer != "" ? UserServ.UIDOnServer : " N/A ") : " N/A "));
                    }
                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToAdminCustomer"]));
                    


                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    
                    
                    string FullContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + UserServ.UserSubscriptionID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName + "<br /></td></tr>"
                                    + "<tr><td><strong>User Email </strong></td><td>" + UserProfile.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + servicecategoryname + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + servicename + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + service.ReleaseVersion + "<br /></td></tr>"
                                    + "<tr><td rowspan='7'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                    + "</td></tr>"
                                    + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                    + "<tr><td><strong>UserName </strong></td><td>" + UserServ.ServiceUserName + "<br /></td></tr>"
                                    + "<tr><td><strong>Activated Date </strong></td><td>" + ActivatedDate + "<br /></td></tr>"
                                    + "<tr><td><strong>Service URL </strong></td>"
                                    + "<td>" + serviceurl + "</td></tr></table><br />");

                    FullContent += ("<table><tr><td><strong>Reasons:</strong></td></tr>"
                                    + "<tr><td>" + reasons + "<br /></td></tr></table>");

                    FullContent += ("<table><tr><td><strong>Feedback:</strong></td></tr>"
                                    + "<tr><td>" + feedback + "<br /></td></tr></table>");

                    FullContent += ("<table><tr><td><strong>Service Rating:</strong></td></tr>"
                                    + "<tr><td>" + rblServiceRating.SelectedValue
                                    + "<br /></td></tr></table>");

                    string AdminContent = EmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                    string UserEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToUser"]));
                    UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string UserContent = UserEmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                    //AdminContent = EmailContent.
                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    SMTPManager.SendEmail(UserContent, "Un-Subscribe request from " + " " + UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName, UserProfile.EmailID, false, true);
                    SMTPManager.SendSupportNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);

                }
                Session["CancelService"] = null;
                Session["UserId"] = null;
                Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserProfile.UserMembershipID.ToString() + "&showview=SubscribedService", false);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbAddCart_Click(object sender, EventArgs e)
        {
            LinkButton lkbAddCart = ((LinkButton)(sender));

            RepeaterItem rItem = ((RepeaterItem)(lkbAddCart.NamingContainer));

            HiddenField hdnServiceID = (rItem.FindControl("hdnNewServicesID") as HiddenField);
            int ServiceID = int.Parse(hdnServiceID.Value);
            DropDownList dropDownList = (rItem.FindControl("ddlPriceModel") as DropDownList);

            var services = objDBContext.ServiceCatalogs.FirstOrDefault(cat => cat.ServiceID == ServiceID);

            decimal ServiceAmount = Convert.ToDecimal(services.InitialHoldAmount);

            //DropDownList DiscountType = (rItem.FindControl("ddlDiscountType") as DropDownList);
            //if(DiscountType != null)
            //    DiscountType = (rItem.FindControl("ddlDiscountType1stRepeater") as DropDownList);
            int SelDiscount = 0;
            //if(DiscountType!=null)
            //    SelDiscount= Convert.ToInt32(DiscountType.SelectedValue);
            bool PriceModelValue = false;
            if (dropDownList != null)
            {
                PriceModelValue = dropDownList.SelectedValue == "1" ? true : false;
            }

            string UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
            int UserProfileID = Convert.ToInt32(user.UserProfileID);
            UserCart objusercart = new UserCart();
            var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID && ot.IsPayAsYouGo == PriceModelValue && ot.SelectedDiscount == SelDiscount);

            var UserService = objDBContext.UserSubscribedServices.FirstOrDefault(usr => usr.UserProfileID == UserProfileID && usr.ServiceID == ServiceID && usr.InitialHoldAmount == 0);

            if (ServiceAmount == 0 && UserService != null)
            {
                divAvaErrorMessage.Visible = false;
                divAvaSuccessMessage.Visible = true;
                lblAvaSuccessmsg.Text = "You have already availed this trial service";
            }
            else
            {
                if (UserServ != null)
                {
                    if (ServiceAmount > 0)
                    {
                        int QunatityCount = UserServ.Quantity;
                        UserServ.Quantity = QunatityCount + 1;
                        UserServ.ModifiedOn = DateTime.Now;
                        UserServ.ModifiedBy = ID;
                        divAvaErrorMessage.Visible = false;
                        divAvaSuccessMessage.Visible = true;

                        lblAvaSuccessmsg.Text = "Service added to Your cart";
                    }
                    else
                    {
                        divAvaErrorMessage.Visible = false;
                        divAvaSuccessMessage.Visible = true;
                        lblAvaSuccessmsg.Text = "Only 1 free trail service can be added to Your cart";
                    }
                }
                else
                {
                    objusercart.Quantity = 1;
                    objusercart.ServiceID = ServiceID;
                    objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                    objusercart.CreatedOn = DateTime.Now;
                    objusercart.CreatedBy = ID;
                    objusercart.IsPayAsYouGo = PriceModelValue;
                    objusercart.SelectedDiscount = SelDiscount;
                    objusercart.RecordStatus = 999;
                    objDBContext.UserCarts.AddObject(objusercart);
                    divAvaErrorMessage.Visible = false;
                    divAvaSuccessMessage.Visible = true;

                    lblAvaSuccessmsg.Text = "Service added to Your cart";
                }
            }
                objDBContext.SaveChanges();
                MyCartDetails();
            
            
        }

        protected void ddlDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DiscountDropDown = ((DropDownList)(sender));
            updateMyCartRowData(DiscountDropDown);
        }

        protected void rptrMyCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList DropDownID = (DropDownList)e.Item.FindControl("ddlDiscountType");
                HiddenField hidUserCartID = (HiddenField)e.Item.FindControl("hidUserCartID");
                HiddenField ServiceID = (HiddenField)e.Item.FindControl("hdnServiceID");
                Label lblUnitPrice = (Label)e.Item.FindControl("lblUnitPrice");
                TextBox lblQty=(TextBox)e.Item.FindControl("lblQty");
                int userCartID = Convert.ToInt32(hidUserCartID.Value);
                int SerID = Convert.ToInt32(ServiceID.Value);
                var cart = objDBContext.UserCarts.FirstOrDefault(a => a.UserCartID == userCartID);
                var ServiceDiscount = objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == SerID);
                int index = Convert.ToInt32(cart.SelectedDiscount);
                DropDownID.Items[index].Selected = true;
                if (lblUnitPrice.Text == "0.00")
                {
                    lblQty.Enabled = false;
                    lblQty.Text = "1";
                }
                if (ServiceDiscount.ThreeMonthsSaving == 0)
                {
                    DropDownID.Enabled = false;
                }
                if (ServiceDiscount.SixMonthsSaving == 0)
                {
                    DropDownID.Enabled = false;
                }
                if (ServiceDiscount.NineMonthsSaving == 0)
                {
                    DropDownID.Enabled = false;
                }
                if (ServiceDiscount.TwelveMonthsSaving == 0)
                {
                    DropDownID.Enabled = false;
                }
              
            }
        }

        protected void txtCreditCardNumber_TextChanged(object sender, EventArgs e)
        {
            string a = ReusableRoutines.GetCreditCardType(txtCreditCardNumber.Text);
            if (a.Contains("Invalid Credit Card"))
            {
                trCardType.Visible = false;
                divValidCard.Visible = false;
                divInValidCard.Visible = true;
                lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
            }
            else
            {
                trCardType.Visible = true;
                divValidCard.Visible = true;
                divInValidCard.Visible = false;
                lblCardType.Text = a;
            }
        }

        protected void btnProceedToDeactiveAccount_Click(object sender, EventArgs e)
        {
            UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
            var CustPayProf = objDBContext.CustomerPaymentProfiles.Where(cpp => cpp.UserProfileID == user.UserProfileID && cpp.Status == true).ToList();
            //if(cu
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeStatenames(ddlCountry.SelectedValue);
            string Password = txtVerifiCode.Text;
            txtVerifiCode.Attributes.Add("value", Password);  
        }

        #endregion

        #region Reusable Routines

        private void ShowCloudAvailablePackages()
        {
            try
            {
                string View = (Request.QueryString[QueryStringKeys.ShowView]);
                if (View == "AvailableService")
                {
                    //rptrWFTCloudPackages.DataSource = objDBContext.ServiceCatalogs.OrderBy(obj => obj.ServiceID).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active);
                    //rptrWFTCloudPackages.DataBind();
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                    List<ServiceCategory> sercat = new List<ServiceCategory>();
                    var serviceCateg = objDBContext.ServiceCategories.Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
                    foreach (var result in serviceCateg)
                    {
                        if (objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == result.ServiceCategoryID && cat.UserSpecific == true && cat.UserProfileID == UserDetails.UserProfileID && cat.RecordStatus == DBKeys.RecordStatus_Active).Count() > 0)
                        {
                            sercat.Add(result);
                        }
                        else if (objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == result.ServiceCategoryID && cat.RecordStatus == DBKeys.RecordStatus_Active).Count() > 0)
                        {
                            sercat.Add(result);
                        }
                    }
                    rptrWFTCloudPackagesCategoryName.DataSource = sercat;
                    rptrWFTCloudPackagesCategoryName.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowSubscribedSelectiveRecords();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
       
        private void ShowSubscribedRecords()
        {
            try
            {
                string View = (Request.QueryString[QueryStringKeys.ShowView]);
                if (View == "SubscribedService")
                {
                    string Userid = Request.QueryString[QueryStringKeys.CheckRepeater];
                    Guid ID = new Guid(Userid);
                    //var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID);
                    string SubStatus = string.Empty;
                    if (Request.QueryString["status"].IsValid())
                    {
                         SubStatus = Request.QueryString["status"];
                    }

                    if (ddlStatus.SelectedItem.Text == "Active" || SubStatus == "Active")
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && cat.RecordStatus == 1);
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    else if (ddlStatus.SelectedItem.Text == "Cancelled" || SubStatus == "Cancelled")
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && cat.RecordStatus == -1);
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    else
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && (cat.RecordStatus == 0 || cat.ExpirationDate < DateTime.Now || cat.ExpirationDate ==null));
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowSubscribedSelectiveRecords()
        {
            try
            {
                string View = (Request.QueryString[QueryStringKeys.ShowView]);
                if (View == "SubscribedService")
                {
                    string Userid = Request.QueryString[QueryStringKeys.CheckRepeater];
                    Guid ID = new Guid(Userid);
                    //var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID);
                   

                    if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && cat.RecordStatus == 1);
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    else if (ddlStatus.SelectedItem.Text == "Cancelled")
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && cat.RecordStatus == -1);
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    else if (ddlStatus.SelectedItem.Text == "Select")
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && cat.RecordStatus == 1);
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }
                    else
                    {
                        var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserID == ID && (cat.RecordStatus == 0 || cat.ExpirationDate < DateTime.Now || cat.ExpirationDate == null));
                        rptrSubscribedPackages.DataSource = SubscribedService;
                        rptrSubscribedPackages.DataBind();
                    }

                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        public string ShowSubscribedServiceStatus(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
            var usdetails = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UsubId);
            
                if (usdetails.RecordStatus == 0)
                    return "Expired";
                else if (usdetails.RecordStatus == -1)
                    return "Cancelled";
                else
                {
                     if (usdetails.ExpirationDate == null)
                        {
                           return "Expired";
                         }
                 DateTime ExpDate = Convert.ToDateTime(usdetails.ExpirationDate);
                 if (ExpDate < DateTime.Now)
                 {
                     return "Expired";
                 }
                    if (usdetails.AutoProvisioningDone == true)
                        return "Active";
                    else
                        return "Provision Pending";
                }

        }

        public bool LaunchServiceStatus(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
            var usdetails = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UsubId);

            if (usdetails.RecordStatus == 0 || usdetails.RecordStatus == -1)
                return false;
            else
            {
                if (usdetails.ExpirationDate == null)
                {
                    return false;
                }
                DateTime ExpDate = Convert.ToDateTime(usdetails.ExpirationDate);
                if (ExpDate < DateTime.Now)
                {
                    return false;
                }
                else
                    return true;
            }

        }

        private void MyCartDetails()
        {
            try
            {
                string View = (Request.QueryString[QueryStringKeys.ShowView]);
                if (View == "ShowMyCart")
                {
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    int UserProfileID = Convert.ToInt32(user.UserProfileID);
                    var mycarts = objDBContext.UserCarts.Where(A => A.UserProfileID == UserProfileID && A.RecordStatus == 999);
                    rptrMyCart.DataSource = mycarts;
                    rptrMyCart.DataBind();
                    if (mycarts.Count() > 0)
                    {
                        divAppyCouponCode.Visible = btnShowMyCart.Visible = btnShowMyCart0.Visible = btnShowMyCart3.Visible = divPaymentDetails.Visible = true;
                        lblTotalPrice.Text = "Amount Payable : $ " + TotalPrice();
                        //pnlPaymentDetails.Visible = false;
                    }
                    else
                    {
                        divAppyCouponCode.Visible = btnShowMyCart.Visible = btnShowMyCart0.Visible = btnShowMyCart3.Visible = divPaymentDetails.Visible = false;
                        lblTotalPrice.Text = "";
                    }
                    int Year = DateTime.Now.Year;
                    ddlExpYear.DataSource = Enumerable.Range(Year, 20);
                    ddlExpYear.DataBind();
                    ddlExpYear.Items.Insert(0, new ListItem("Select", "Select"));
                    ddlCountry.DataSource = objDBContext.Countries.Where(a => a.RecordStatus == 1).OrderBy(a => a.CountryNames);
                    ddlCountry.DataTextField = "CountryNames";
                    ddlCountry.DataValueField = "CC_ISO";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                    ChangeStatenames(ddlCountry.SelectedValue);
                    var CustomerCardDetails = objDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == UserProfileID && u.Status == true);
                    if (CustomerCardDetails.Count() > 0)
                    {
                        divExistingCardDetails.Visible = true;
                        divNewCardDetails.Visible = false;

                        trExistingCards.Visible = true;
                        List<CardDetails> lstCardDetails = new List<CardDetails>();
                        var CustomerpaymentProfiles = objGW.GetCustomer(CustomerCardDetails.FirstOrDefault().AuthCustomerProfileID);
                        var zz = objDBContext.CustomerPaymentProfiles.Where(A => A.UserProfileID == user.UserProfileID && A.Status == true);

                        var s = from cpp in CustomerpaymentProfiles.PaymentProfiles
                                join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                                select new
                                {
                                    ProfileID = cpp.ProfileID,
                                    CardNumber = "XXXXXXXX" + cpp.CardNumber,
                                    DefaultPaymentID = zz1.DefaultPaymentID,
                                    AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                    UserProfileID = zz1.UserProfileID,
                                };
                        ddlExistingCards.DataSource = s;
                        ddlExistingCards.DataTextField = "CardNumber";
                        ddlExistingCards.DataValueField = "ProfileID";
                        ddlExistingCards.DataBind();
                        ddlExistingCards.Items.Insert(0, new ListItem("New Credit Card", "New Credit Card"));
                        FilCreditCardDetails();
                        var z1 = s != null ? s.FirstOrDefault(z => z.DefaultPaymentID == true) : null;
                        if (z1 != null)
                        {
                            ddlExistingCards.SelectedValue = z1.ProfileID;
                            FilCreditCardDetails();
                        }
                        else
                        {
                            divNewCardDetails.Visible = true;
                        }
                        if (s.Count() == 3)
                        {
                            var s1 = from cpp in CustomerpaymentProfiles.PaymentProfiles
                                     join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                                     select new
                                     {
                                         ProfileID = cpp.ProfileID,
                                         CardNumber = "XXXXXXXX" + cpp.CardNumber + "" + (zz1.DefaultPaymentID == true ? "&nbsp;&nbsp;&nbsp;Default Credit Card" : ""),
                                         DefaultPaymentID = zz1.DefaultPaymentID,
                                         AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                         UserProfileID = zz1.UserProfileID,
                                     };
                            divExistingCrdCardList.Visible = true;
                            //rptrCrediCardList.DataSource = s1;
                            //rptrCrediCardList.DataBind();
                            rblCreditCard.DataSource = s1;
                            rblCreditCard.DataTextField = "CardNumber";
                            rblCreditCard.DataValueField = "ProfileID";
                            rblCreditCard.DataBind();
                        }
                        else
                        {
                            divExistingCrdCardList.Visible = false;
                        }

                    }
                    else
                    {
                        divExistingCardDetails.Visible = true;
                        divExistingCrdCardList.Visible = false;
                        ddlExistingCards.Items.Insert(ddlExistingCards.Items.Count, new ListItem("New Credit Card", "New Credit Card"));
                        FilCreditCardDetails();
                        divNewCardDetails.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(),(System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        
        private void LoadTabView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    string View = (Request.QueryString[QueryStringKeys.ShowView]);
                    if (View == "AvailableService")
                    {
                        liCloudPackages.Attributes.Add("class", "active");
                        WFTCloudPackages.Attributes.Add("class", "tab-pane in active");
                        liSubscribedPackages.Attributes.Add("class", "");
                        SubscribedPackages.Attributes.Add("class", "tab-pane");
                        liMyCart.Attributes.Add("class", "");
                        MyCart.Attributes.Add("class", "tab-pane");
                        //liMyCart.Visible = false;
                    }
                    else if (View == "SubscribedService")
                    {
                        liSubscribedPackages.Attributes.Add("class", "active");
                        SubscribedPackages.Attributes.Add("class", "tab-pane in active");
                        liCloudPackages.Attributes.Add("class", "");
                        WFTCloudPackages.Attributes.Add("class", "tab-pane");
                        liMyCart.Attributes.Add("class", "");
                        MyCart.Attributes.Add("class", "tab-pane");
                        //liMyCart.Visible = false;
                    }
                    else if (View == "ShowMyCart")
                    {
                        liSubscribedPackages.Attributes.Add("class", "");
                        SubscribedPackages.Attributes.Add("class", "tab-pane");
                        liCloudPackages.Attributes.Add("class", "");
                        WFTCloudPackages.Attributes.Add("class", "tab-pane");
                        liMyCart.Attributes.Add("class", "active");
                        MyCart.Attributes.Add("class", "tab-pane in active");
                        //liMyCart.Visible = false;
                    }
                    else
                    {
                        liCloudPackages.Attributes.Add("class", "active");
                        WFTCloudPackages.Attributes.Add("class", "tab-pane in active");
                        liSubscribedPackages.Attributes.Add("class", "");
                        SubscribedPackages.Attributes.Add("class", "tab-pane");
                        liMyCart.Attributes.Add("class", "");
                        MyCart.Attributes.Add("class", "tab-pane");
                        //liMyCart.Visible = false;
                    }
                }
                else
                {
                    liCloudPackages.Attributes.Add("class", "active");
                    WFTCloudPackages.Attributes.Add("class", "tab-pane in active");
                    liSubscribedPackages.Attributes.Add("class", "");
                    SubscribedPackages.Attributes.Add("class", "tab-pane");
                    liMyCart.Attributes.Add("class", "");
                    MyCart.Attributes.Add("class", "tab-pane");
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void showCartTab()
        {
            string UserMembershipID = Request.QueryString["userid"];
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?userid=" + UserMembershipID + "&showview=ShowMyCart");
            //liSubscribedPackages.Attributes.Add("class", "");
            //SubscribedPackages.Attributes.Add("class", "tab-pane");
            //liCloudPackages.Attributes.Add("class", "");
            //WFTCloudPackages.Attributes.Add("class", "tab-pane");
            //liMyCart.Attributes.Add("class", "active");
            //MyCart.Attributes.Add("class", "tab-pane in active");
            //liMyCart.Visible = true;
        }

        private void AddToCart()
        {
            try
            {
                string ModelID = Convert.ToString(Session["PriceModelValue"]);
                if (Request.QueryString["AddToCart"].IsValid())
                {
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    int UserProfileID = Convert.ToInt32(user.UserProfileID);
                    UserCart objusercart = new UserCart();
                    int ServiceID = int.Parse(Request.QueryString["AddToCart"]);
                    var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID);
                    if (UserServ != null)
                    {
                        int QunatityCount = UserServ.Quantity;
                        UserServ.Quantity = QunatityCount + 1;
                        UserServ.ModifiedOn = DateTime.Now;
                        UserServ.ModifiedBy = ID;
                    }
                    else
                    {
                        objusercart.Quantity = 1;
                        objusercart.ServiceID = ServiceID;
                        objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                        objusercart.CreatedOn = DateTime.Now;
                        objusercart.CreatedBy = ID;
                        objusercart.RecordStatus = 999;
                        objDBContext.UserCarts.AddObject(objusercart);
                    }
                    objDBContext.SaveChanges();
                    MyCartDetails();
                    divAvaErrorMessage.Visible = false;
                    divAvaSuccessMessage.Visible = true;
                    lblAvaSuccessmsg.Text = "New Service(s) added to your cart successfully.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void CancelSubscribedService()
        {
            try
            {
                if (Request.QueryString["CancelSubscribedService"].IsValid())
                {
                    mpopupCanCelServices.Show();
                    Session["CancelService"] = Convert.ToString(Request.QueryString["CancelSubscribedService"]);
                    Session["UserId"] = Convert.ToString(Request.QueryString["userid"]);
                }
             
                
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void DeletemyCart()
        {
            try
            {
                if (Request.QueryString["deleteMycart"].IsValid())
                {
                    string usercart = Request.QueryString["deleteMycart"];
                    int UserCartID = Convert.ToInt32(usercart);
                        var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.UserCartID == UserCartID);
                        objDBContext.UserCarts.DeleteObject(UserServ);
                        objDBContext.SaveChanges();
                        MyCartDetails();
                        divMyDeleteCartFailed.Visible = false;
                        divMyDeleteCartSuccess.Visible = true;
                        lblMyDeleteCartSuccess.Text = "Purchased service deleted from your cart.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public string CategoryName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            var category = objDBContext.ServiceCategories.FirstOrDefault(d => d.ServiceCategoryID == services.ServiceCategoryID);
            return category.CategoryName;
        }
      
        public string ServiceName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.ServiceName;
        }

        public string ServiceDuration(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.PackageLengthInMonths.ToString();
        }


        public string ServiceUnitPrice(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.InitialHoldAmount.ToString();
        }

        public string GetCategoryName(string CID)
        {
            int CategoryID = Convert.ToInt32(CID);
            var services = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
            return services.CategoryName;
        }        

        public string ServicePrice(string ServiceID, string Qty,string Discount)
        {
            int serviceID = Convert.ToInt32(ServiceID);
            int Quantity = Convert.ToInt32(Qty);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            decimal DiscountPrice = 0;
            int DiscountPeriod = 0;
            if (Discount == "0")
            {
                DiscountPrice = 0;
                DiscountPeriod = 1;
            }
            else if (Discount == "1")
            {
                DiscountPrice = Convert.ToDecimal(services.ThreeMonthsSaving);
                DiscountPeriod = 3;
            }
            else if (Discount == "2")
            {
                DiscountPrice = Convert.ToDecimal(services.SixMonthsSaving);
                DiscountPeriod = 6;
            }
            else if (Discount == "3")
            {
                DiscountPrice = Convert.ToDecimal(services.NineMonthsSaving);
                DiscountPeriod = 9;
            }
            else if (Discount == "4")
            {
                DiscountPrice = Convert.ToDecimal(services.TwelveMonthsSaving);
                DiscountPeriod = 12;
            }
            //decimal ReducedPrice = ((DiscountPrice / 100) * Convert.ToDecimal(services.InitialHoldAmount))* Quantity;
            decimal ReducedPrice=0;
            decimal OriginalPrice=0;
            if (DiscountPrice > 0)
            {
                ReducedPrice = ((DiscountPrice / 100) * Convert.ToDecimal(services.InitialHoldAmount) * DiscountPeriod);
                ReducedPrice = Convert.ToDecimal(services.InitialHoldAmount) * DiscountPeriod - ReducedPrice;
                OriginalPrice = Convert.ToDecimal((ReducedPrice * Quantity));
            }
            else
            {
                 ReducedPrice = ((DiscountPrice / 100) * Convert.ToDecimal(services.InitialHoldAmount)) * Quantity;
                 OriginalPrice = Convert.ToDecimal((services.InitialHoldAmount * Quantity) - ReducedPrice);
            }
           
            return OriginalPrice.ToString("0.00");
        }
      
        private string TotalPrice()
        {
            decimal TotalPrice = 0M;
            foreach (RepeaterItem rItem in rptrMyCart.Items)
            {
                decimal lblPrice = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                TotalPrice += lblPrice;
            }
            return TotalPrice.ToString("0.00");
        }

        private void CouponValidation(bool ValidCoupon, bool InValidCoupon, string SuccessText, string failedSuccess, string AmountPayable)
        {
            divCouponFailed.Visible = InValidCoupon;
            divCouponSuccess.Visible = ValidCoupon;
            lblCouponFailed.Text = failedSuccess;
            lblCouponSuccess.Text = SuccessText;
            lblTotalPrice.Text = AmountPayable;
            if (InValidCoupon == true)
            {
                lblTotalPrice.Text = "Amount Payable : $ " + TotalPrice();
                hidCouponCode.Value ="";
                hidDisCountValue.Value = "";
            }
            else if(ValidCoupon == true)
            {
                decimal B4Discount = Convert.ToDecimal(TotalPrice());
                decimal AftrDiscount = Convert.ToDecimal(lblTotalPrice.Text.Replace("Amount Payable : $ ", ""));
                decimal DiscountedValue = B4Discount - AftrDiscount;
                hidCouponCode.Value = txtCouponCode.Text;
                hidDisCountValue.Value = DiscountedValue.ToString();
            }
        }

        public string updatePaymentProfileID(UserProfile user, string AuthProfileID, string AuthBillingAddressID, int ExpMonth, int ExpYear, AuthorizeNet.Address BillingAddress)
        {
            string AuthPaymentProfileID;
            var s = objDBContext.CustomerPaymentProfiles.Where(cpp => cpp.UserProfileID == user.UserProfileID).ToList();
            int upid = Convert.ToInt32(user.UserProfileID);
            string BillingID = user.UserProfileID.ToString() + "-" + Convert.ToString(s.Count() > 0 ? (s.Count() + 1) : 1);
            //BillingAddress.ID = BillingID;
            AuthPaymentProfileID = ReusableRoutines.AuthorizeAddCreditCardProfile(AuthProfileID, txtCreditCardNumber.Text, ExpMonth, ExpYear, txtVerifiCode.Text, BillingAddress);

            var customer = objGW.GetCustomer(AuthProfileID);
            var cpjhgp = objDBContext.CustomerPaymentProfiles.Where(zMycart=>zMycart.UserProfileID == upid && zMycart.Status == true).ToList();
            bool defalutcard = false;

            if (divExistingCrdCardList.Visible== true)
            {
                var deleteOldestRegistered = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == rblCreditCard.SelectedValue);//CustomerPaymenProfileID == CustomerPaymenProfileID);
                defalutcard = Convert.ToBoolean(deleteOldestRegistered.DefaultPaymentID);
                deleteOldestRegistered.DefaultPaymentID = false;
                deleteOldestRegistered.Status = false;
                objDBContext.SaveChanges();
            }
            CustomerPaymentProfile nW = new CustomerPaymentProfile();
            nW.AuthCustomerProfileID = AuthProfileID;
            nW.AuthPaymentProfileID = AuthPaymentProfileID;
            nW.UserProfileID = user.UserProfileID;
            nW.DefaultPaymentID = cpjhgp.Count()==0?true:false;
            if (defalutcard == true)
            {
                nW.DefaultPaymentID = defalutcard;
            }
            nW.AuthBillingAddressID = BillingID;
            nW.Status = true;
            objDBContext.CustomerPaymentProfiles.AddObject(nW);            
            objDBContext.SaveChanges();            
            return AuthPaymentProfileID;
        }

        private void ApplyCoupon()
        {
            //string[] Coupons = null;
            string Coupons = string.Empty;
            //Coupons = Regex.Split(txtCouponCode.Text.Trim(), ",");
            Coupons = txtCouponCode.Text.Trim();
            lblTotalPrice.Text = "Amount Payable : $ " + TotalPrice();
            string AmountPayable = lblTotalPrice.Text;
            decimal TotalPrice1 = Convert.ToDecimal(TotalPrice()); 
            decimal ReducePercentage = 0M;
            decimal ReducedAmount = 0M;
            bool CouponApplied = false;
            UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            //var Couponvalid1 = objDBContext.Coupons.FirstOrDefault(a => a.CouponCode == txtCouponCode.Text && a.IsUsed == false  && a.RecordStatus == DBKeys.RecordStatus_Active && (a.ValidityDate >= DateTime.Today || a.ValidityDate == null) && (a.CouponCount > 0 || a.CouponCount == null));
            decimal TotalAfterCouponApld = 0M;
            decimal ReducedAmountAfterCouponApld = 0M;
            int i = 0;
            int SameCouponCodeEnteredCount = 0;
            List<Coupon> couponslst = new List<Coupon>();
            //foreach (string reslt in Coupons)
            //{
            //    SameCouponCodeEnteredCount = 0;
            //    foreach (string str in Coupons)
            //    {
            //        if (reslt == str)
            //            SameCouponCodeEnteredCount += 1;
            //    }
            //    if (SameCouponCodeEnteredCount > 1)
            //    {
            //        CouponValidation(false, true, "", "Same Coupon Code Entered Multiple Times", AmountPayable);
            //        return;
            //    }
            //    else
            //    {
            Coupon cpn = objDBContext.Coupons.Where(a => a.CouponCode == Coupons.Trim() && a.IsUsed == false && a.RecordStatus == DBKeys.RecordStatus_Active).Where(a => a.ValidityDate >= DateTime.Today || a.ValidityDate == null).FirstOrDefault(a => a.CouponCount > 0 || a.CouponCount == null);
                    if (cpn != null)
                    { couponslst.Add(cpn); }
                    else
                    {
                        CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                        return;
                    }
            //    }

            //}
            if(couponslst.Count() >0)
            {
                couponslst = couponslst.OrderByDescending(a=>a.ForServiceID).ToList();
            }
            foreach (var reslt in couponslst)
            {
                CouponApplied = false;
                ReducedAmountAfterCouponApld = 0;
                if (i == 0)
                {
                    TotalAfterCouponApld = TotalPrice1;
                }

                var Couponvalid = reslt;//objDBContext.Coupons.Where(a => a.CouponCode == reslt.Trim() && a.IsUsed == false && a.RecordStatus == DBKeys.RecordStatus_Active).Where(a => a.ValidityDate >= DateTime.Today || a.ValidityDate == null).FirstOrDefault(a => a.CouponCount > 0 || a.CouponCount == null);
                if (Couponvalid != null)
                {
                    if (Couponvalid.IsUsed == false)
                    {
                        if (Couponvalid.ForUser != null && Couponvalid.ForServiceID != null)
                        {
                            if (Couponvalid.ForUser == ID)
                            {
                                foreach (RepeaterItem rItem in rptrMyCart.Items)
                                {
                                    int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                                    decimal lblPrice = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                                    if (Couponvalid.ForServiceID == ServiceID)
                                    {
                                        CouponApplied = true;
                                        ReducePercentage = Couponvalid.Discount;
                                        decimal amoutReducedforthisService = lblPrice * ((100 - ReducePercentage) / 100);
                                        ReducedAmountAfterCouponApld += (lblPrice - amoutReducedforthisService);
                                    }
                                }
                                //if (CouponApplied == true)
                                //{
                                //    decimal result = TotalPrice1 - ReducedAmount;
                                //    CouponValidation(true, false, "Coupon accepted successfully.", "", "Amount Payable : $ " + result.ToString("0.00"));
                                //    return;
                                //}

                                //if (CouponApplied == false)
                                //{
                                //    CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                                //    return;
                                //}
                            }
                            //else
                            //{
                            //    CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                            //    return;
                            //}
                        }
                        else if (Couponvalid.ForUser != null && Couponvalid.ForServiceID == null)
                        {
                            if (Couponvalid.ForUser == ID)
                            {
                                CouponApplied = true;
                                hidCouponCode.Value = txtCouponCode.Text;
                                ReducePercentage = Couponvalid.Discount;
                                decimal result = TotalAfterCouponApld * ((100 - ReducePercentage) / 100);
                                ReducedAmountAfterCouponApld += (TotalAfterCouponApld - result);
                                //CouponValidation(true, false, "Coupon accepted successfully.", "", "Amount Payable : $ " + result.ToString("0.00"));
                               // return;
                            }
                            //else
                            //{
                            //    CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                            //    return;
                            //}
                        }
                        else if (Couponvalid.ForUser == null && Couponvalid.ForServiceID != null)
                        {
                            foreach (RepeaterItem rItem in rptrMyCart.Items)
                            {
                                int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                                decimal lblPrice = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                                if (Couponvalid.ForServiceID == ServiceID)
                                {
                                    CouponApplied = true;
                                    ReducePercentage = Couponvalid.Discount;
                                    decimal amoutReducedforthisService = lblPrice * ((100 - ReducePercentage) / 100);
                                    ReducedAmountAfterCouponApld += (lblPrice - amoutReducedforthisService);
                                }
                            }
                            //if (CouponApplied == true)
                            //{
                            //    decimal result = TotalPrice1 - ReducedAmount;
                            //    CouponValidation(true, false, "Coupon accepted successfully.", "", "Amount Payable : $ " + result.ToString("0.00"));
                            //    return;
                            //}
                            //if (CouponApplied == false)
                            //{
                            //    CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                            //    return;
                            //}
                        }
                        else if (Couponvalid.ForUser == null && Couponvalid.ForServiceID == null)
                        {
                            CouponApplied = true;
                            ReducePercentage = Couponvalid.Discount;
                            decimal result = TotalAfterCouponApld * ((100 - ReducePercentage) / 100);
                            ReducedAmountAfterCouponApld += (TotalAfterCouponApld - result);
                            //CouponValidation(true, false, "Coupon accepted successfully.", "", "Amount Payable : $ " + result.ToString("0.00"));
                           // return;
                        }
                    }
                    //else
                    //{
                    //    CouponValidation(false, true, "", "The allowed limit for this coupon is already read, please enter a vaid coupon and proceed.", AmountPayable);
                    //    return;
                    //}
                }
                //else
                //{
                //    CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                //   // return;
                //}

                TotalAfterCouponApld = TotalAfterCouponApld - ReducedAmountAfterCouponApld;
                ReducedAmount +=  ReducedAmountAfterCouponApld;
                i++;
            }
            if (CouponApplied == true)
            {
                decimal result = TotalPrice1 - ReducedAmount;
                hidCouponCode.Value = txtCouponCode.Text.Trim();
                CouponValidation(true, false, "Coupon accepted successfully.", "", "Amount Payable : $ " + result.ToString("0.00"));
                return;
            }
            if (CouponApplied == false)
            {
                CouponValidation(false, true, "", "Please enter a valid coupon code.", AmountPayable);
                return;
            }
        }

        private void ApplyCouponForCount()
        {
            UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            string[] Coupons = null;
            Coupons = Regex.Split(txtCouponCode.Text.Trim(), ",");
            
            foreach (string reslt in Coupons)
            {
                
                var Couponvalid = objDBContext.Coupons.FirstOrDefault(a => a.CouponCode == reslt && a.IsUsed == false && a.RecordStatus == DBKeys.RecordStatus_Active && (a.ValidityDate >= DateTime.Today || a.ValidityDate == null) && (a.CouponCount > 0 || a.CouponCount == null));
                if (Couponvalid != null)
                {
                    var CouponTypeDetails = objDBContext.CouponTypes.FirstOrDefault(c => c.CouponTypeID == Couponvalid.CouponTypeID);
                    if (Couponvalid.ForUser != null && Couponvalid.ForServiceID != null)
                    {
                        if (Couponvalid.ForUser == ID)
                        {
                            if (CouponTypeDetails.IsCouponUnlimited != true)
                            {
                                foreach (RepeaterItem rItem in rptrMyCart.Items)
                                {
                                    int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                                    if (Couponvalid.ForServiceID == ServiceID)
                                    {
                                        int Qty = Convert.ToInt32((rItem.FindControl("lblQty") as TextBox).Text);
                                        Couponvalid.CouponCount -= Qty;
                                        objDBContext.SaveChanges();
                                        //return;
                                    }
                                }
                            }
                        }
                    }
                    else if (Couponvalid.ForUser != null && Couponvalid.ForServiceID == null)
                    {
                        if (Couponvalid.ForUser == ID)
                        {
                            if (CouponTypeDetails.IsCouponUnlimited != true)
                            {
                                Couponvalid.CouponCount -= 1;
                                objDBContext.SaveChanges();
                                //return;
                            }
                        }
                    }
                    else if (Couponvalid.ForUser == null && Couponvalid.ForServiceID != null)
                    {
                        foreach (RepeaterItem rItem in rptrMyCart.Items)
                        {
                            int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                            if (Couponvalid.ForServiceID == ServiceID)
                            {
                                if (CouponTypeDetails.IsCouponUnlimited != true)
                                {
                                    int Qty = Convert.ToInt32((rItem.FindControl("lblQty") as TextBox).Text);
                                    Couponvalid.CouponCount -= Qty;
                                    objDBContext.SaveChanges();
                                    //return;
                                }
                            }
                        }
                    }
                    else if (Couponvalid.ForUser == null && Couponvalid.ForServiceID == null)
                    {
                        if (CouponTypeDetails.IsCouponUnlimited != true)
                        {
                            Couponvalid.CouponCount -= 1;
                            objDBContext.SaveChanges();
                            //return;
                        }
                    }
                }
            }
        }

        private void FilCreditCardDetails()
        {
            try
            {

                if (ddlExistingCards.SelectedValue != "New Credit Card")
                {
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    var CustomerCardDetails = objDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == user.UserProfileID);
                    if (CustomerCardDetails.Count() > 0)
                    {

                        var s1 = CustomerCardDetails.FirstOrDefault(s => s.AuthPaymentProfileID == ddlExistingCards.SelectedValue);
                        var customerPaymentProfile = objGW.GetCustomer(s1.AuthCustomerProfileID).PaymentProfiles.First(a => a.ProfileID == ddlExistingCards.SelectedValue);
                        //rblTypeOfCard.ClearSelection(); 
                        ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                        txtAddress1.Text = customerPaymentProfile.BillingAddress.Street;
                        txtCity.Text = customerPaymentProfile.BillingAddress.City;
                        txtContactNumberPtDet.Text = customerPaymentProfile.BillingAddress.Phone;
                        txtCreditCardNumber.Text = "XXXXXXXX" + customerPaymentProfile.CardNumber;
                        txtPostalZipCode.Text = customerPaymentProfile.BillingAddress.Zip;

                        txtNameOnCard.Text = customerPaymentProfile.BillingAddress.First + " " + customerPaymentProfile.BillingAddress.Last;
                        divInValidCard.Visible = true;
                        divValidCard.Visible = false;
                        divNewCardDetails.Visible = false;

                        var Coun = objDBContext.Countries.FirstOrDefault(a => a.CountryNames.Contains(customerPaymentProfile.BillingAddress.Country));
                        if (Coun != null)
                        {
                            if (ddlCountry.SelectedValue != Coun.CC_ISO)
                            {
                                ddlCountry.SelectedValue = Coun.CC_ISO;
                                ChangeStatenames(Coun.CC_ISO);
                            }
                            var state = objDBContext.StateNames.FirstOrDefault(s => s.StateName1.Contains(customerPaymentProfile.BillingAddress.State) && s.CC_ISO == Coun.CC_ISO);
                            if (state != null)
                            { ddlStateName.SelectedValue = state.StateName1; }
                            else
                            {
                                ddlStateName.SelectedValue = "0";
                                if (ddlStateName.Items.Count == 2 && ddlStateName.Items[1].Text == ddlCountry.SelectedItem.Text)
                                {
                                    ddlStateName.SelectedIndex = 1;
                                }
                            }
                        }
                        else
                        {
                            ddlStateName.Items.Clear();
                            ddlStateName.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                            ddlCountry.SelectedValue = "0";
                        }
                    }
                }
                else
                {
                    //rblTypeOfCard.ClearSelection();
                    trCardType.Visible = false;
                    divNewCardDetails.Visible = true;
                    ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                    txtAddress2.Text = txtAddress1.Text = txtCity.Text = txtContactNumberPtDet.Text = txtCreditCardNumber.Text = txtPostalZipCode.Text = txtNameOnCard.Text = "";
                    ddlCountry.SelectedValue = "0";
                    ddlStateName.Items.Clear();
                    ddlStateName.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ChangeStatenames(string CC_ISO)
        {
            try
            {
                ddlStateName.Items.Clear();
                if (CC_ISO != "0")
                {
                    var states = objDBContext.StateNames.Where(s => s.CC_ISO == CC_ISO && s.RecordStatus == 1).OrderBy(A => A.StateName1);
                    ddlStateName.DataSource = states;
                    ddlStateName.DataTextField = "StateName1";
                    ddlStateName.DataValueField = "StateName1";
                    ddlStateName.DataBind();
                    if (ddlStateName.Items.Count == 0)
                    {
                        ddlStateName.Items.Insert(0, new ListItem(ddlCountry.SelectedItem.Text, ddlCountry.SelectedItem.Text));
                    }
                    ddlStateName.Items.Insert(0, new ListItem("<--Select State-->", "0"));
                }
                else
                {
                    ddlStateName.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                }
            }
            catch (Exception Ex)
            {
                 ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public void ShowSubscribedServiceDetails(int USubscribtionID)
        {
            try
            {
                mvSubservice.ActiveViewIndex = 1;
                var USubService = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USubscribtionID);
                lblUserSubServiceIDHead.Text = "User Subscription ID - " + USubService.UserSubscriptionID;
                lblShowActivatedDate.Text = Convert.ToDateTime(USubService.ActiveDate).ToString("dd-MMM-yyyy");
                //lblShowExpirationDate.Text = Convert.ToDateTime(USubService.ExpirationDate).ToString("dd-MMM-yyyy");
                lblCanceledDate.Text = USubService.CanceledDate != null ? Convert.ToDateTime(USubService.CanceledDate).ToString("dd-MMM-yyyy") : "";
                lblShowInitialHoldAmount.Text = "$ " + USubService.InitialHoldAmount.ToString();
                lblShowOtherServiceInfomation.Text = USubService.ServiceOtherInformation;
                lblShowServiceCategory.Text = CategoryName(USubService.ServiceID.ToString());
                lblShowServiceName.Text = ServiceName(USubService.ServiceID.ToString());
                lblShowServiceUserName.Text = USubService.ServiceUserName;
                trShowLanchService.Visible = trShowServiceUserName.Visible = Convert.ToBoolean(USubService.AutoProvisioningDone);
                lblShowOtherServiceInfomation.Text = lblShowOtherServiceInfomation.Text == "" ? "-" : lblShowOtherServiceInfomation.Text;
                hypShowLanchService.NavigateUrl = USubService.ServiceUrl;

                if (USubService.InitialHoldAmount == 0)
                {
                       lbl.Text = "This is a Free service";
                }
                    
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }

        public void SendEmailAttachment(string messageBody, string subject, string ToMail, bool sendInBCC, bool IsHtml, string attachmentPath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = IsHtml;
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                }

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }

        private void updateMyCartRowData(Control ControlObj)
        {
            RepeaterItem rItem = ((RepeaterItem)(ControlObj.NamingContainer));

            HiddenField hdnServiceID = rItem.FindControl("hdnServiceID") as HiddenField;
            HiddenField hidUserCartID = rItem.FindControl("hidUserCartID") as HiddenField;
            int usercartid = Convert.ToInt32(hidUserCartID.Value);
            DropDownList DiscountPrice = rItem.FindControl("ddlDiscountType") as DropDownList;
            Label lblPrice = rItem.FindControl("lblPrice") as Label;
            TextBox lblQuantity = rItem.FindControl("lblQty") as TextBox;
            decimal Price = Convert.ToDecimal(ServicePrice(hdnServiceID.Value, lblQuantity.Text, DiscountPrice.SelectedValue));
            lblPrice.Text = Price.ToString("0.00");
            lblTotalPrice.Text = "Amount Payable : $ " + TotalPrice();


            //Update value in database
            string UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
            int UserProfileID = Convert.ToInt32(user.UserProfileID);
            UserCart objusercart = new UserCart();
            int ServiceID = int.Parse(hdnServiceID.Value);
            var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.UserCartID == usercartid);
            if (UserServ != null)
            {
                int slectedDisType = Convert.ToInt32(DiscountPrice.SelectedValue);
                UserServ.SelectedDiscount = slectedDisType;
                UserServ.Quantity = Convert.ToInt32(lblQuantity.Text);
                UserServ.ModifiedOn = DateTime.Now;
                UserServ.ModifiedBy = ID;
                objDBContext.SaveChanges();
            }
            if (txtCouponCode.Text != "")
                ApplyCoupon();
            else
            {
                hidCouponCode.Value = "";
                hidDisCountValue.Value = "";
            }
            //Update Label for DiscountType
            Label lblDiscountPerc = rItem.FindControl("lblDiscountPercentage") as Label;
            string Discount = DiscountPrice.SelectedValue;
            string ReducedPrice = GetDiscountValue(ServiceID.ToString(), Discount);
            lblDiscountPerc.Text = ReducedPrice;

            if (DiscountPrice.SelectedValue == "0")
            {
                divDiscountTypeSuccess.Visible = false;
            }
            else
            {
                divDiscountTypeSuccess.Visible = true;
            }
        }

        public string GetDiscountValue(string ServiceID, string Discount)
        {
            Discount = Discount == "" ? "0" : Discount;
            int sid = Convert.ToInt32(ServiceID);
            var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == sid);
            decimal DiscountPrice1 = 0m;
            if (Discount == "0")
                DiscountPrice1 = 0;
            else if (Discount == "1")
                DiscountPrice1 = Convert.ToDecimal(ServiceDetails.ThreeMonthsSaving);
            else if (Discount == "2")
                DiscountPrice1 = Convert.ToDecimal(ServiceDetails.SixMonthsSaving);
            else if (Discount == "3")
                DiscountPrice1 = Convert.ToDecimal(ServiceDetails.NineMonthsSaving);
            else if (Discount == "4")
                DiscountPrice1 = Convert.ToDecimal(ServiceDetails.TwelveMonthsSaving);
            decimal ReducedPrice = ((DiscountPrice1 / 100) * Convert.ToDecimal(ServiceDetails.InitialHoldAmount));
            return ReducedPrice == 0 ? "-" : ("$ " + ReducedPrice.ToString("0.00"));
        }

        public void SendAdminNotificationAttachmentEmail(string messageBody, string subject, bool sendInBCC, string attachmentPath)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Admin_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                else
                {
                    mail.Bcc.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                }
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public void SendSAPBasisNotificationAttachmentEmail(string messageBody, string subject, bool sendInBCC, string attachmentPath)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.SAPBasis_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                else
                {
                    mail.Bcc.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                }
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public void makeOtherCardsDefaultpaymentcardFalse(int userprofileid, string AuthPaymentProfileID)
        {
            var makeOtherCardsFirst = objDBContext.CustomerPaymentProfiles.Where(uid => uid.UserProfileID == userprofileid && uid.AuthPaymentProfileID != AuthPaymentProfileID && uid.Status == true).ToList();
            if (makeOtherCardsFirst != null)
            {
                foreach (var result in makeOtherCardsFirst)
                {
                    var updateToFalse = objDBContext.CustomerPaymentProfiles.FirstOrDefault(r => r.CustomerPaymenProfileID == result.CustomerPaymenProfileID);
                    updateToFalse.DefaultPaymentID = false;
                    objDBContext.SaveChanges();
                }
            }
        }

        private void BuildSubscribedServices(int UserOrderID)
        {
            try
            {
                var userOrder = objDBContext.UserOrders.FirstOrDefault(order => order.UserOrderID == UserOrderID && order.OrderStatus == DBKeys.RecordStatus_Active);
                if ((userOrder != null))
                {
                    UserProfile userProf = objDBContext.UserProfiles.FirstOrDefault(upf => upf.UserProfileID == userOrder.UserProfileID);
                    string UserFullName = userProf.FirstName + " " + userProf.MiddleName + " " + userProf.LastName;
                    List<UserOrderDetail> orderDtls = objDBContext.UserOrderDetails.Where(ordr => ordr.UserOrderID == UserOrderID).ToList();
                    foreach (var uod in orderDtls.ToList())
                    {
                        for (int idx = 0; idx < uod.Quantity; idx++)
                        {
                            string ServiceUserName = "";
                            string ServicePassword = "";
                            string ServiceURL = "";
                            string ServiceOtherInformation = "";
                            bool ServiceProvisioningUpdated = false;
                            int ServiceID = uod.ServiceID;
                            ServiceCatalog serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(scat => scat.ServiceID == uod.ServiceID);
                           
                            int CategoryID = Convert.ToInt32(serviceDetails.ServiceCategoryID);
                            var categoryDetails = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
                            var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == CategoryID && sp.ServiceID == ServiceID);
                            if (ServiceProvisioningDEtails != null && (categoryDetails.IsDedicated == false || categoryDetails.IsDedicated != null))
                            {
                                if ((ServiceProvisioningDEtails.UserMin + ServiceProvisioningDEtails.CurrentUserCounter) <= ServiceProvisioningDEtails.UserMax)
                                {
                                    var serviceZUsername = objDBContext.GetServiceProvisionUserName(ServiceProvisioningDEtails.ServiceProvisionID);

                                    ServiceUserName = Convert.ToString(serviceZUsername.First());
                                    //ServiceProvisioningDEtails.CurrentUserCounter += 1;
                                    //var serviceProvisonDetails = objDBContext1.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == CategoryID && sp.ServiceID == ServiceID);
                                    //serviceProvisonDetails.CurrentUserCounter += 1;
                                    //objDBContext1.SaveChanges();
                                    //ServiceUserName = ServiceProvisioningDEtails.UserName + (ServiceProvisioningDEtails.UserMin + ServiceProvisioningDEtails.CurrentUserCounter);
                                    ServiceProvisioningUpdated = true;

                                    ServicePassword = ServiceProvisioningDEtails.ServicePassword;
                                    ServiceURL = ServiceProvisioningDEtails.ServiceUrl;
                                    ServiceOtherInformation = ServiceProvisioningDEtails.AdditionalInformation;
                                }
                                int notification = ServiceProvisioningDEtails.UserMax - (ServiceProvisioningDEtails.UserMin + ServiceProvisioningDEtails.CurrentUserCounter);
                                try
                                {
                                    if (notification <= ServiceProvisioningDEtails.NotificationRequiredAt)
                                    {
                                        string ServiceDEtails = ("<strong>Category Name :</strong> " + CategoryName(serviceDetails.ServiceID.ToString()) + "<br />"
                                                                + "<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                                                + "<strong>System Type :</strong> " + serviceDetails.SystemType + "<br />"
                                                                + "<strong>Release Version :</strong> " + serviceDetails.ReleaseVersion + "<br />"
                                                                + "<strong>Remaining User Count :</strong> " + notification + "<br />"
                                                   );

                                        string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NeedtoUpdateServiceToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/Need to update service provisioning User count.html"));
                                        EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                        string AdminContent = EmailContent.Replace("++AddContentHere++", ServiceDEtails);
                                        SMTPManager.SendAdminNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName , false);
                                        SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName , false);
                                        SMTPManager.SendSupportNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName , false);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                                }
                            }

                            UserSubscribedService uss = new UserSubscribedService();
                            uss.UserID = userProf.UserMembershipID;
                            uss.ServiceID = uod.ServiceID;
                            //uss.Credit = uod.InitialHoldAmount;
                            //uss.ARBPeriod = 0;
                            //uss.Usage = 0;
                            uss.ARBSubscriptionId = "0";
                            uss.Status = DBKeys.RecordStatus_Active.ToString();
                            uss.RecordStatus = DBKeys.RecordStatus_Active;
                            uss.CreatedOn = DateTime.Now;
                            uss.UserOrderID = userOrder.UserOrderID;

                            uss.UserProfileID = userProf.UserProfileID;
                            uss.ActiveDate = DateTime.Now;
                            uss.AddedBy = userProf.UserMembershipID;

                            uss.ServiceCategoryID = CategoryID;
                            uss.UserCart_Id = uod.OrderCartID;
                            uss.ServiceUserName = ServiceUserName;
                            uss.ServicePassword = ServicePassword;
                            uss.ServiceUrl = ServiceURL;
                            uss.ServiceOtherInformation = ServiceOtherInformation;
                            int PackageMonthLength = Convert.ToInt32(serviceDetails.PackageLengthInMonths);
                            uss.ExpirationDate = DateTime.Now.AddMonths(PackageMonthLength + 1);//.AddHours(23).AddMinutes(59);
                            uss.IsPayAsYouGo = uod.IsPayAsYouGo;
                            uss.InitialHoldAmount = serviceDetails.InitialHoldAmount;
                            if (uss.IsPayAsYouGo == true)
                            {
                                uss.WFTCloudPrice = serviceDetails.WFTCloudPrice;
                            }
                            uss.UsageUnit = serviceDetails.UsageUnit;
                            uss.SelectedDiscountDuration = uod.SelectedDiscountDuration;
                            uss.AutoProvisioningDone = ServiceProvisioningUpdated;
                            /*SelectedDiscountDuration
                             * 1- 3 Months
                             * 2- 6 Months
                             * 3- 9 Months
                             * 4- 12 Months
                             */
                            decimal disPer = 0M;
                            if (uod.SelectedDiscountDuration == 0 || uod.SelectedDiscountDuration == null)
                            {
                                disPer = 0M;
                            }
                            else if (uod.SelectedDiscountDuration == 1)
                            {
                                disPer = Convert.ToDecimal(serviceDetails.ThreeMonthsSaving);
                            }
                            else if (uod.SelectedDiscountDuration == 2)
                            {
                                disPer = Convert.ToDecimal(serviceDetails.SixMonthsSaving);
                            }
                            else if (uod.SelectedDiscountDuration == 3)
                            {
                                disPer = Convert.ToDecimal(serviceDetails.NineMonthsSaving);
                            }
                            else if (uod.SelectedDiscountDuration == 4)
                            {
                                disPer = Convert.ToDecimal(serviceDetails.TwelveMonthsSaving);
                            }

                            uss.DiscountPercentage = disPer;

                            objDBContext1.UserSubscribedServices.AddObject(uss);
                            objDBContext1.SaveChanges();
                            
                            if (uss.IsPayAsYouGo == true)
                            {
                                uss.SelectedDiscountDuration = 0;
                                disPer = 0;
                            }
                            int USerSubID = Convert.ToInt32(objDBContext.UserSubscribedServices.Max(z => z.UserSubscriptionID));
                            var SubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                            objDBContext.pr_GenerateAllPaymentEntries(userOrder.AuthCustomerProfileID, userOrder.AuthPaymentProfileID, USerSubID, PackageMonthLength, uss.InitialHoldAmount, uss.SelectedDiscountDuration, disPer, "Authorize.net", "", "", "", "");
                            try
                            {
                                var ussinfo = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.InvoiceNumber == userOrder.InvoiceNumber);
                                if (ServiceProvisioningUpdated == false && ServiceProvisioningDEtails != null)
                                {

                                    string ServiceDEtails = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                 + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                 + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                 + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                 + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                 + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                 + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                 + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                 + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");


                                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NeedServiceProvisioningToAdminOnOrderPurchased"]));//"/wftcloud/UploadedContents/EmailTemplates/Need Service Provisioning.html"));
                                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                    string AdminContent = EmailContent.Replace("++AddContentHere++", ServiceDEtails);
                                    AdminContent = AdminContent.Replace("++UpdateOfManualProvisioning++", " This is to notify you that Auto-Provisioning for one of our customer has failed and requires manual intervention to provide the Provisioning Details. Please provide the necessary details to the customers.");
                                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Service Count Update" + serviceDetails.ServiceName + " Service Provisioning Details for " + UserFullName, false);

                                    SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName + " for " + UserFullName, false);
                                    SMTPManager.SendSupportNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName + " for " + UserFullName, false);
                                }
                                else if (ServiceProvisioningUpdated == false && ServiceProvisioningDEtails == null)
                                {
                                    //Service Provision for the service does not have records
                                    string ServiceDEtails = string.Empty;
                                    if (SubInfo.SelectedDiscountDuration > 0)
                                    {
                                         ServiceDEtails = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                         + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                         + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                         + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                         + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                         + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                         + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                         + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                         + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                         + "<tr><td rowspan='2'><strong>Offer Information</strong></td><td><strong>Offer Period </strong></td><td>" + SubInfo.SelectedDiscountDuration + " Months" + "<br /></td></tr>"
                                         + "<tr><td><strong>Order Status </strong></td><td>" + "Pre-Paid" + "<br /></td></tr></table>");

                                    }
                                    else
                                    {
                                          ServiceDEtails = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                         + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                         + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                         + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                         + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                         + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                         + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                         + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                         + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");
                                    }
                                   

                                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NeedServiceProvisioningToAdminOnOrderPurchased"]));//"/wftcloud/UploadedContents/EmailTemplates/Need Service Provisioning.html"));
                                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                    string AdminContent = EmailContent.Replace("++AddContentHere++", ServiceDEtails);
                                    AdminContent = AdminContent.Replace("++UpdateOfManualProvisioning++", "This is to notify that our customer who purchased a service did not receive their  service provisioned details. This requires manual intervention to provide the provisioning details.Please provide the necessary service provisioned details to the customer.");
                                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);

                                    SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);
                                    SMTPManager.SendSupportNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);
                                }

                                string ServiceDetails1 = "";
                                string ServiceDetailstoSupport = "";
                                if (ServiceProvisioningUpdated == true)
                                {

                                    var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");

                                    var UserNextPaymentDate = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                                    string TransactionID =string.Empty ;
                                    string PaymentMethod = string.Empty;
                                    string NextBillDate = string.Empty;
                                    if (serviceDetails.InitialHoldAmount > 0M)
                                    {
                                        if (userOrder.PaymentMethod == "Authorize.net")
                                        {
                                            TransactionID = ussinfo.AuthTransactionID;
                                            PaymentMethod = "Authorize.net";
                                        }
                                        else
                                        {
                                            TransactionID = ussinfo.PalpalPaymentTransactionID;
                                            PaymentMethod = "PayPal";
                                        }
                                    }
                                    else
                                    {
                                        TransactionID = "-";
                                        PaymentMethod = "-";
                                    }

                                    if (UserNextPaymentDate != null)
                                    {
                                        NextBillDate = UserNextPaymentDate.PaymentDate.ToString("dd-MMM-yy");
                                    }
                                    else
                                    {
                                        NextBillDate = "NA";
                                    }

                                    ServiceDetails1 = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>User Email </strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                    + "<tr><td rowspan='9'><strong>Payment Transaction Information</strong></td><td><strong>Subscription Purchased Date </strong></td><td>" + userOrder.OrderDateTime.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                    + "<tr><td><strong>Initial Payment Date </strong></td><td>" + userOrder.OrderDateTime.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                    + "<tr><td><strong>Recurring Billing Date </strong></td><td>" + NextBillDate + "<br /></td></tr>"
                                    + "<tr><td><strong>Cancel Date </strong></td><td>" + "Prior to Recurring Billing Date" + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment Mode </strong></td><td>" + PaymentMethod + "<br /></td></tr>"
                                    + "<tr><td><strong>Invoice Number </strong></td><td>" + userOrder.InvoiceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Transaction ID </strong></td><td>" + TransactionID + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Amount </strong>"
                                    + "</td><td>" + serviceDetails.InitialHoldAmount + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment Status </strong></td><td>" + "Success" + "<br /></td></tr>"
                                    + "<tr><td rowspan='7'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + ServiceProvisioningDEtails.InstanceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Application Server </strong></td><td>" + (ServiceProvisioningDEtails.ApplicationServer != null ? ServiceProvisioningDEtails.ApplicationServer : " ") + "<br />"
                                    + "</td></tr>"
                                    + "<tr><td><strong>SID </strong></td><td>" + ServiceProvisioningDEtails.UIDOnServer + "<br /></td></tr>"
                                    + "<tr><td><strong>UserName </strong></td><td>" + ServiceUserName + "<br /></td></tr>"
                                    + "<tr><td><strong>Password </strong></td><td>" + ServicePassword + "<br /></td></tr>"
                                    + "<tr><td><strong>Developer Key </strong></td><td>" + "To be purchased separately" + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                    + "</td></tr>") : "") + "</table><br />");

                                    ServiceDetails1 += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                    ServiceDetails1 += ("<table><tr><td>" + ServiceProvisioningDEtails.AdditionalInformation + "<br /></td></tr></table>");

                                    ServiceDetailstoSupport = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>User Email </strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                    + "<tr><td rowspan='7'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + ServiceProvisioningDEtails.InstanceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Application Server </strong></td><td>" + (ServiceProvisioningDEtails.ApplicationServer != null ? ServiceProvisioningDEtails.ApplicationServer : " ") + "<br />"
                                    + "</td></tr>"
                                    + "<tr><td><strong>SID </strong></td><td>" + ServiceProvisioningDEtails.UIDOnServer + "<br /></td></tr>"
                                    + "<tr><td><strong>UserName </strong></td><td>" + ServiceUserName + "<br /></td></tr>"
                                    + "<tr><td><strong>Password </strong></td><td>" + ServicePassword + "<br /></td></tr>"
                                    + "<tr><td><strong>Developer Key </strong></td><td>" + "To be purchased separately" + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                    + "</td></tr>") : "") + "</table><br />");

                                    ServiceDetailstoSupport += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                    ServiceDetailstoSupport += ("<table><tr><td>" + ServiceProvisioningDEtails.AdditionalInformation + "<br /></td></tr></table>");
                                }
                                else if (ServiceProvisioningDEtails != null)
                                {
                                    ServiceDetails1 = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>"

                                        + "<strong>This Service cannot be provisioned at the moment. Please Contact WFT Admin <a href='mailto:admin@wftcloud.com'>admin@wftcloud.com</a> to get your Service provision details.</strong> ");
                                }
                                else
                                {
                                    if (SubInfo.SelectedDiscountDuration > 0)
                                    {
                                        ServiceDetails1 = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                   + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                   + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                   + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                   + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                   + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                   + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                   + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                   + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                   + "<tr><td rowspan='2'><strong>Offer Information</strong></td><td><strong>Offer Period </strong></td><td>" + SubInfo.SelectedDiscountDuration + " Months" + "<br /></td></tr>"
                                   + "<tr><td><strong>Order Status </strong></td><td>" + "Pre-Paid" + "<br /></td></tr></table>");

                                        ServiceDetails1 += "<br><strong>After your chosen discount subscription period is over, the following month you will be charged the regular monthly subscription rate as listed in our website.</strong><br>";

                                    }
                                    else
                                    {
                                        ServiceDetails1 = ("<table border='1'><tr><td rowspan='4'><strong>User Information</strong></td>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>Email ID</strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td rowspan='5'><strong>Service Information</strong></td><td><strong>User Subscription ID </strong></td><td>" + USerSubID + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");
                                    }

                                    ServiceDetails1 += "<br><strong>Service Provisioning details will be sent to you shortly by the Administrator. Please Contact WFT Admin <a href='mailto:admin@wftcloud.com'>admin@wftcloud.com</a> if you need any clarification.</strong>";
                                
                                }
                                //*******
                                //Sent Email Notification to the Customer 
                                //******
                                string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceReadyToCustomer"]));//"/wftcloud/UploadedContents/EmailTemplates/Service-Ready.html"));
                                CustomerEmailContent = CustomerEmailContent.Replace("++UserName++", UserFullName);
                                CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string CustomerContent = CustomerEmailContent.Replace("++AddContentHere++", ServiceDetails1).Replace("++name++", UserFullName);
                                CustomerContent = CustomerContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                string path = "";
                                string img = "";
                                string AttachmentPath = "";
                                if (ServiceProvisioningDEtails != null)
                                {
                                    if (ServiceProvisioningDEtails.AttachmentImgPath.IsValid())
                                    {
                                        path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + ServiceProvisioningDEtails.AttachmentImgPath);
                                        if (File.Exists(path))
                                        {
                                            string oldFilename = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + ServiceProvisioningDEtails.AttachmentImgPath);
                                            string temp = ServiceProvisioningDEtails.AttachmentImgPath.Substring(ServiceProvisioningDEtails.AttachmentImgPath.LastIndexOf('/') + 1).ToLower();
                                            string NewFileLoc = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + USerSubID + "/");
                                            if (!Directory.Exists(NewFileLoc))
                                            {
                                                Directory.CreateDirectory(NewFileLoc);
                                            }
                                            string NewFileName = DateTime.Now.ToString("ddMMyyyymmssfff") + temp;
                                            string NewFilePath = NewFileLoc + NewFileName;
                                            if (!File.Exists(NewFilePath))
                                            {
                                                File.Copy(oldFilename, NewFilePath);
                                            }
                                            var ussimg = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                                            ussimg.ImagePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + USerSubID + "/" + NewFileName;
                                            objDBContext.SaveChanges();
                                            path = ConfigurationManager.AppSettings["DomainNameForImageBindInMail"] + ussimg.ImagePath;
                                            img = " <img alt='Logo' src='" + path + "' border='0' vspace='0' hspace='0' style='display:block; max-width:100%; height:auto !important;' /><br /><br />";
                                        }
                                        CustomerContent = CustomerContent.Replace("++AttachmentURL++", img);
                                    }
                                    else
                                    {
                                        CustomerContent = CustomerContent.Replace("++AttachmentURL++", "");
                                    }
                                }
                                else
                                {
                                    CustomerContent = CustomerContent.Replace("++AttachmentURL++", "");
                                }
                                if (ServiceProvisioningDEtails != null)
                                {
                                    if (ServiceProvisioningDEtails.AttachmentPath.IsValid())
                                    {
                                        path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + ServiceProvisioningDEtails.AttachmentPath);
                                        if (File.Exists(path))
                                        {
                                            string oldFilename = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + ServiceProvisioningDEtails.AttachmentPath);
                                            string temp = ServiceProvisioningDEtails.AttachmentPath.Substring(ServiceProvisioningDEtails.AttachmentPath.LastIndexOf('/') + 1).ToLower();
                                            string NewFileLoc = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + USerSubID + "/");
                                            if (!Directory.Exists(NewFileLoc))
                                            {
                                                Directory.CreateDirectory(NewFileLoc);
                                            }
                                            string NewFileName = DateTime.Now.ToString("ddMMyyyymmssfff") + temp;
                                            string NewFilePath = NewFileLoc + NewFileName;
                                            if (!File.Exists(NewFilePath))
                                            {
                                                File.Copy(oldFilename, NewFilePath);
                                            }
                                            var ussimg = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                                            ussimg.FilePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + USerSubID + "/" + NewFileName;
                                            objDBContext.SaveChanges();
                                        }
                                        AttachmentPath = ServiceProvisioningDEtails.AttachmentPath;
                                        // SMTPManager.SendEmail(CustomerContent, "Your order " + lblInvoiceNumber.Text + " on WFTCloud.com", userProf.EmailID, false, true);
                                        SendEmailAttachment(CustomerContent, "Your order " + lblInvoiceNumber.Text + " on WFTCloud.com", userProf.EmailID, false, true, AttachmentPath);
                                    }
                                    else
                                    {
                                        SMTPManager.SendEmail(CustomerContent, "Your order " + lblInvoiceNumber.Text + " on WFTCloud.com", userProf.EmailID, false, true);
                                    }
                                }
                                else
                                {
                                    SMTPManager.SendEmail(CustomerContent, "Your order " + lblInvoiceNumber.Text + " on WFTCloud.com", userProf.EmailID, false, true);
                                }


                                //*******
                                //Sent Email Notification to the admin about ServiceProvision details provided to customer 
                                //******
                                if (ServiceProvisioningUpdated == true)
                                {
                                    string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceProvisionSuccessNotificationToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/ServiceProvisionSuccessNotificationToAdmin.html"));
                                    AdminEmailContent = AdminEmailContent.Replace("++UserName++", UserFullName + " (" + userProf.EmailID + ") ");
                                    AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                    string AdminContent = AdminEmailContent.Replace("++AddContentHere++", ServiceDetails1);
                                    AdminContent = AdminContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                    AdminContent = AdminContent.Replace("++AttachmentURL++", img);


                                    string SupportContent = AdminEmailContent.Replace("++AddContentHere++", ServiceDetailstoSupport);
                                    SupportContent = SupportContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                    SupportContent = SupportContent.Replace("++AttachmentURL++", img);

                                    if (AttachmentPath == "")
                                    {
                                        SMTPManager.SendAdminNotificationEmail(AdminContent, "Auto Provisioning Completed for - " + UserFullName, false);

                                        SMTPManager.SendSAPBasisNotificationEmail(SupportContent, "Auto Provisioning Completed for - " + UserFullName, false);
                                    }
                                    else
                                    {
                                        SendAdminNotificationAttachmentEmail(AdminContent, "Auto Provisioning Completed for - " + UserFullName, false, AttachmentPath);
                                        SendSAPBasisNotificationAttachmentEmail(SupportContent, "Auto Provisioning Completed for - " + UserFullName, false, AttachmentPath);
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        
        public void PaypalPayment(UserProfile User)
        {
            var InvoiceNumber = objDBContext.pr_GetNewInvoiceNumber();
            string WFTInvoiceNumber = Convert.ToString(InvoiceNumber.First());
            decimal Amount = Convert.ToDecimal(lblTotalPrice.Text.Replace("Amount Payable : $ ", ""));
            string Services = "";
            int i = 0;
            UserOrder NewOrder = new UserOrder();
            NewOrder.OrderDateTime = DateTime.Now;
            NewOrder.OrderStatus = 999; //
            NewOrder.IsCouponCode = hidCouponCode.Value == "" ? null : hidCouponCode.Value;
            if (hidDisCountValue.Value != "")
                NewOrder.IsDiscountValue = Convert.ToDecimal(hidDisCountValue.Value);

            NewOrder.OrderTotal = Amount;
            NewOrder.UserProfileID =Convert.ToInt32(User.UserProfileID);
      /*    NewOrder.AuthCustomerProfileID = AuthProfileID;
            NewOrder.AuthPaymentProfileID = AuthPaymentProfileID;
      */
            NewOrder.PaymentMethod = "PayPal";
            NewOrder.InvoiceNumber = WFTInvoiceNumber;
            objDBContext.UserOrders.AddObject(NewOrder);
            objDBContext.SaveChanges();
            int NewUserOrderID = objDBContext.UserOrders.Max(a => a.UserOrderID);
            int BillingPeriod = 1; 
            foreach (RepeaterItem rItem in rptrMyCart.Items)
            {
                i += 1;
                decimal InitialHoldAmount = Convert.ToDecimal((rItem.FindControl("lblPrice") as Label).Text);
                int ServiceID = Convert.ToInt32((rItem.FindControl("hdnServiceID") as HiddenField).Value);
                int UserCartID = Convert.ToInt32((rItem.FindControl("hidUserCartID") as HiddenField).Value);
                int Qty = Convert.ToInt32((rItem.FindControl("lblQty") as TextBox).Text);
                int selecteddiscountduration = Convert.ToInt32((rItem.FindControl("ddlDiscountType") as DropDownList).SelectedValue);
                UserOrderDetail NewUserOrderDetail = new UserOrderDetail();
                NewUserOrderDetail.InitialHoldAmount = InitialHoldAmount;
                NewUserOrderDetail.ServiceID = ServiceID;
                NewUserOrderDetail.UserOrderID = NewUserOrderID;
                NewUserOrderDetail.Quantity = Qty;
                NewUserOrderDetail.OrderCartID = UserCartID;
                NewUserOrderDetail.SelectedDiscountDuration = selecteddiscountduration;
                objDBContext.UserOrderDetails.AddObject(NewUserOrderDetail);
                objDBContext.SaveChanges();
                Services += (i + "." + ServiceName((rItem.FindControl("hdnServiceID") as HiddenField).Value) + "  \n");
                BillingPeriod = Convert.ToInt32(objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == ServiceID).PackageLengthInMonths)-1;
            }
            
                
            string returnUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Customer/Success.aspx?payment=successl&userid=" + User.UserMembershipID.ToString().ToLower() + "&InvoiceNumber=" + WFTInvoiceNumber +"&BP="+BillingPeriod;
            string cancelUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Customer/Failed.aspx?unpaid=cancel&userid=" + User.UserMembershipID.ToString().ToLower() + "&InvoiceNumber=" + WFTInvoiceNumber;
            if (txtCouponCode.Text != "")
                returnUrl += ("&CouponCode=" + txtCouponCode.Text);
            string AmountToBill = lblTotalPrice.Text.Replace("Amount Payable : $ ", "");
            string paymentDescription = (WFTInvoiceNumber + " - " + User.EmailID + ",\n Service : " + "\n" + Services + "\n Amount: $ " + lblTotalPrice.Text.Replace("Amount Payable : $ ", ""));
            Session["paymentDescription"] = paymentDescription;
            
            string UserMembershipID = Request.QueryString["userid"];
            Guid UserMemID = new Guid(UserMembershipID);

            SetExpressCheckoutCommand obj = new SetExpressCheckoutCommand();
            var trans1Response = obj.SetExpressCheckoutAPIOperation(returnUrl
                                                                    , cancelUrl
                                                                    , Amount
                                                                    , ConfigurationManager.AppSettings["PayPalMerchantEmailID"]
                                                                    , WFTInvoiceNumber
                                                                    , paymentDescription

                );
            PaypalRespons Pres = new PaypalRespons();
            Pres.InvoiceNumber = WFTInvoiceNumber;
            Pres.JsonReponseFormPaypal = JsonConvert.SerializeObject(trans1Response);
            Pres.MethodName = "SetExpressCheckoutAPIOperation";
            Pres.UpdatedDateTime = DateTime.Now;
            Pres.UserMembershipID = UserMemID;
            Pres.UserOrderID = NewUserOrderID;
            ReusableRoutines.SavePaypalResponse(Pres);

            Session["strToken"] = trans1Response.Token;
            string RedirectPayPalURL = ConfigurationManager.AppSettings["RedirectPayPalURL"];
            RedirectPayPalURL += "&token={0}";
            //Redirect to Paypal for Payment
            Response.Redirect(string.Format(RedirectPayPalURL, trans1Response.Token));
        }

        #endregion

    }
    public class CardDetails
    {
        public string ProfileID { get; set; }
        public string CardNumber { get; set; }
    }
}