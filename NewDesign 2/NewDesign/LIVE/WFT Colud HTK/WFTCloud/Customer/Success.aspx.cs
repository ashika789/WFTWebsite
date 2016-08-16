
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
namespace WFTCloud.Customer
{
    public partial class Success : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;
        public string InvoiceNumber;
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Page.Title = "WFTCloud - Thank You! We have received your payment";
                if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["payment"]) && !string.IsNullOrEmpty(Request.QueryString["userid"]) && !string.IsNullOrEmpty(Request.QueryString["InvoiceNumber"]) && !string.IsNullOrEmpty(Request.QueryString["PayerID"]))
                {
                    UserMembershipID = Request.QueryString["userid"];
                      Guid UserMemID = new Guid(UserMembershipID);
                    InvoiceNumber = Request.QueryString["InvoiceNumber"];
                    var OrderDetail = objDBContext.UserOrders.FirstOrDefault(a => a.InvoiceNumber == InvoiceNumber && a.OrderStatus == 999);
                    if (OrderDetail != null)
                    {
                        #region GetExpressCheckOut
                        GetExpressCheckoutDetailsCommand obj = new GetExpressCheckoutDetailsCommand();
                        var trans1Response = obj.GetExpressCheckoutDetailsAPIOperation(Session["strToken"].ToString());
                        Session["strPayerID"] = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                        
                        PaypalRespons Pres = new PaypalRespons();
                        Pres.InvoiceNumber = InvoiceNumber;
                        Pres.JsonReponseFormPaypal = JsonConvert.SerializeObject(trans1Response);
                        Pres.MethodName = "GetExpressCheckoutDetailsAPIOperation";
                        Pres.UpdatedDateTime = DateTime.Now;
                        Pres.UserMembershipID = UserMemID;
                        Pres.UserOrderID = OrderDetail.UserOrderID;
                        ReusableRoutines.SavePaypalResponse(Pres);
                        #endregion

                        #region Do Express Checkout Payment
                        DoExpressCheckoutPaymentCommand obj2 = new DoExpressCheckoutPaymentCommand();
                        var trans2Response = obj2.DoExpressCheckoutPaymentAPIOperation(Session["strToken"].ToString()
                                                                                    , Session["strPayerID"].ToString()
                                                                                    , OrderDetail.OrderTotal
                                                                                    , ConfigurationManager.AppSettings["PayPalMerchantEmailID"]
                                                                                    , InvoiceNumber
                                                                                    );

                        PaypalRespons Pres1 = new PaypalRespons();
                        Pres1.InvoiceNumber = InvoiceNumber;
                        Pres1.JsonReponseFormPaypal = JsonConvert.SerializeObject(trans2Response);
                        Pres1.MethodName = "DoExpressCheckoutPaymentAPIOperation";
                        Pres1.UpdatedDateTime = DateTime.Now;
                        Pres1.UserMembershipID = UserMemID;
                        Pres1.UserOrderID = OrderDetail.UserOrderID;
                        ReusableRoutines.SavePaypalResponse(Pres1);
                        #endregion
                        

                        if (trans2Response.Ack == PayPal.PayPalAPIInterfaceService.Model.AckCodeType.SUCCESS)
                        {
                            UserPaymentTransaction NewUPT = new UserPaymentTransaction();
                            NewUPT.Amount = Convert.ToDecimal(trans2Response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo.First().GrossAmount.value);
                            NewUPT.Approved = trans2Response.Ack == PayPal.PayPalAPIInterfaceService.Model.AckCodeType.SUCCESS ? true : false;
                            //Paypal Payer User name  
                            NewUPT.PaypalPayerMailID = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Payer;
                            // Payment Payer Id  
                            NewUPT.PaypalPayerID = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                            //Billing Agreement Id
                            OrderDetail.PaypalBillingAgreementID = trans2Response.DoExpressCheckoutPaymentResponseDetails.BillingAgreementID;
                            NewUPT.PaypalBillingAgreementID = trans2Response.DoExpressCheckoutPaymentResponseDetails.BillingAgreementID;
                            // Payment TransactionID  used for the Refund option
                            NewUPT.PalpalPaymentTransactionID = trans2Response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo.First().TransactionID;
                            NewUPT.PaymentMethod = "PayPal";
                            OrderDetail.PaypalPaymentTransactionID = trans2Response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo.First().TransactionID;
                            NewUPT.PaymentDateTime = DateTime.Now;
                            NewUPT.UserProfileID = OrderDetail.UserProfileID;
                            NewUPT.InvoiceNumber = InvoiceNumber;
                            NewUPT.AuthAuthorizationCode = "";
                            NewUPT.AuthCardNumber = "";
                            NewUPT.AuthMessage = "";
                            NewUPT.AuthResponseCode = "";
                            NewUPT.AuthTransactionID = "";
                            objDBContext.UserPaymentTransactions.AddObject(NewUPT);
                            objDBContext.SaveChanges();
                            if (Request.QueryString["CouponCode"].IsValid())
                                ApplyCouponForCount(Request.QueryString["CouponCode"].ToString(), OrderDetail.UserProfileID);
                            var user = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == OrderDetail.UserProfileID);
                            string ServiceNameDEtails = "";
                            //ServiceNameDEtails += "<strong>User Name :</strong> " + user.FirstName + " " + user.MiddleName + " " + user.LastName + "<br />"
                            //                      + "<strong>User EmailID :</strong> " + user.EmailID + "<br /><br />";
                            var UserCartDetail = objDBContext.UserCarts.Where(a => a.UserProfileID == OrderDetail.UserProfileID && a.RecordStatus == 999).ToList();
                            foreach (var ORDResult in UserCartDetail)
                            {
                                var mycarts = objDBContext.UserCarts.FirstOrDefault(A => A.UserCartID == ORDResult.UserCartID);
                                mycarts.RecordStatus = DBKeys.RecordStatus_Active;
                                objDBContext.SaveChanges();
                                var serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == mycarts.ServiceID);
                                var categoryDetails = objDBContext.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == serviceDetails.ServiceCategoryID);
                                //ServiceNameDEtails += "<strong>Service ID :</strong> " + serviceDetails.ServiceID + "<br />"
                                //               + "<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                //               + "<strong>System Type :</strong> " + serviceDetails.SystemType + "<br />"
                                //               + "<strong>Release Version :</strong> " + serviceDetails.ReleaseVersion + "<br /><br />";

                                ServiceNameDEtails = ("<table border='1'><tr><td rowspan='5'><strong>User Information</strong></td>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + user.FirstName + " " + user.MiddleName + " " + user.LastName + "<br /></td></tr>"
                                    + "<tr><td><strong>Email ID</strong></td><td>" + user.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Location </strong></td><td>" + user.Location + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For  </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td rowspan='4'><strong>Service Information</strong></td><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                    + "<tr><td><strong>System Type </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr></table>");
                            }
                            var UpDateOrderStatus = objDBContext.UserOrders.FirstOrDefault(a => a.InvoiceNumber == InvoiceNumber && a.OrderStatus == 999);
                            UpDateOrderStatus.OrderStatus = 1;
                            objDBContext.SaveChanges();
                            //*******
                            //Sent Email Notification to the admin about new order 
                            //******
                            string message = (("<strong>" + user.FirstName + " " + user.MiddleName + " " + user.LastName + "</strong>"));
                            string EmailContent1 = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewServiceRequestToAdmin"]));
                            EmailContent1 = EmailContent1.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            string AdminContent1 = EmailContent1.Replace("++AddContentHere++", ServiceNameDEtails).Replace("++name++", message);
                            SMTPManager.SendAdminNotificationEmail(AdminContent1, "New Order " + InvoiceNumber, false);
                            DisplayOrderStatusDetail(OrderDetail, OrderDetail.InvoiceNumber);
                            //CreateSubscribed Service and send notification to customer and admin
                            BuildSubscribedServices(OrderDetail.UserOrderID, NewUPT);
                        }
                        else
                        {
                            UserPaymentTransaction NewUPT1 = new UserPaymentTransaction();
                            NewUPT1.Amount = OrderDetail.OrderTotal;
                            NewUPT1.Approved = false;
                            NewUPT1.AuthAuthorizationCode = Session["paymentid"] != null ? Session["paymentid"].ToString() : "paymentidNull ";//PaymentID
                            NewUPT1.AuthCardNumber = Session["strPayerID"] != null ? Session["strPayerID"].ToString() : "PayerIDisNull"; //Request.QueryString["PayerID"];//PayerID
                            NewUPT1.AuthMessage = "PayPal";
                            NewUPT1.AuthResponseCode = "Recurring Payments Profile not created.";
                            NewUPT1.AuthTransactionID = "PayPal";
                            NewUPT1.PaymentDateTime = DateTime.Now;
                            NewUPT1.UserProfileID = OrderDetail.UserProfileID;
                            NewUPT1.InvoiceNumber = Request.QueryString["InvoiceNumber"];
                            objDBContext.UserPaymentTransactions.AddObject(NewUPT1);
                            objDBContext.SaveChanges();
                            DisplayOrderStatusDetail(OrderDetail, OrderDetail.InvoiceNumber);
                        }
                    }
                    else
                    {
                        Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=ShowMyCart", false);
                    }
                }
                else
                {
                    Response.Redirect("/Loginpage.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
             //   Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority + "/Customer/Success.aspx?payment=successl&userid=" + Request.QueryString["userid"] + "&InvoiceNumber=" + InvoiceNumber, false);
            }
        }

        #endregion

        #region Reusable Routines

        private void DisplayOrderStatusDetail(UserOrder OrderDetail,string InvoiceNumber)
        {
            var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == OrderDetail.UserOrderID);
            lblOrderCode.Text = "Order" + OrderDetail.UserOrderID.ToString();
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
            rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == OrderDetail.UserOrderID);
            rptrOrderDetails.DataBind();
            lblInvoiceNumber.Text = OrderDetail.InvoiceNumber;
            var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == OrderDetail.InvoiceNumber);
            lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
            lblPaymentAmount.Text = "$ " + PaymentDetails.Amount.ToString();
            lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";
            lblPayPalPaymentId.Text = PaymentDetails.PaypalBillingAgreementID.IsValid()?PaymentDetails.PaypalBillingAgreementID:"-";
            lblPayPalPayerId.Text = PaymentDetails.PaypalPayerID.IsValid() ? PaymentDetails.PaypalPayerID : "-"; ;
            lblPayPalSalesId.Text = PaymentDetails.PalpalPaymentTransactionID.IsValid()?PaymentDetails.PalpalPaymentTransactionID:"-";
        }

        private void BuildSubscribedServices(int UserOrderID, UserPaymentTransaction NewUPT)
        {
            try
            {
                var userOrder = objDBContext.UserOrders.FirstOrDefault(order => order.UserOrderID == UserOrderID);
                if ((userOrder != null) && (userOrder.OrderStatus == DBKeys.RecordStatus_Active))
                {
                    var userProf = objDBContext.UserProfiles.FirstOrDefault(upf => upf.UserProfileID == userOrder.UserProfileID);
                    string UserFullName = userProf.FirstName + " " + userProf.MiddleName + " " + userProf.LastName;
                    var orderDtls = objDBContext.UserOrderDetails.Where(ordr => ordr.UserOrderID == UserOrderID);
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
                            var serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(scat => scat.ServiceID == uod.ServiceID);

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
                            objDBContext.UserSubscribedServices.AddObject(uss);
                            objDBContext.SaveChanges();
                            if (uss.IsPayAsYouGo == true)
                            {
                                uss.SelectedDiscountDuration = 0;
                                disPer = 0;
                            }
                            int USerSubID = Convert.ToInt32(objDBContext.UserSubscribedServices.Max(z => z.UserSubscriptionID));
                            var SubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                            //Auto-Generated bill payment//  
                            //objDBContext.pr_GenerateAllPaymentEntries(userOrder.AuthCustomerProfileID, userOrder.AuthPaymentProfileID, USerSubID, PackageMonthLength, uss.InitialHoldAmount, uss.SelectedDiscountDuration, disPer);
                            objDBContext.pr_GenerateAllPaymentEntries("", "", USerSubID, PackageMonthLength, uss.InitialHoldAmount, uss.SelectedDiscountDuration, disPer,"PayPal",NewUPT.PaypalPayerMailID,NewUPT.PaypalPayerID,NewUPT.PaypalBillingAgreementID,NewUPT.PalpalPaymentTransactionID);
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
                                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName + " for " + UserFullName, false);

                                    SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName + " for " + UserFullName, false);
                                    SMTPManager.SendSupportNotificationEmail(AdminContent, "Service Count Update - " + serviceDetails.ServiceName + " for " + UserFullName, false);
                                }
                                else if (ServiceProvisioningUpdated == false && ServiceProvisioningDEtails == null)
                                {
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
                                    AdminContent = AdminContent.Replace("++UpdateOfManualProvisioning++", "This is to notify you that one of our customer does not get their provision details. Requires manual intervention to provide the Provisioning Details. Please provide the necessary details to the customers.");
                                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);

                                    SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);
                                    SMTPManager.SendSupportNotificationEmail(AdminContent, "Request to Manually Provide Service Provisioning Details for " + UserFullName, false);
                                }

                                string ServiceDetails1 = "";
                                string ServiceDetailstoSupport = "";
                                if (ServiceProvisioningUpdated == true)
                                {
                                    //var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                                    //ServiceDetails1 = ("<table><tr><td><strong>Service Name :</strong> </td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    //                  + "<tr><td><strong>Service/SAP UserName :</strong></td><td>" + ServiceUserName + "<br /></td></tr>"
                                    //                  + "<tr><td><strong>Service/SAP Password :</strong></td><td>" + ServicePassword + "<br /></td></tr>"
                                    //                  + (APP_URL != null ? ("<tr><td><strong>Service/SAP URL :</strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a></td></tr>") : "")
                                    //                  + "<tr><td><strong>Service Other Information :</strong></td><td>" + ServiceProvisioningDEtails.AdditionalInformation + "</td></tr></table>");

                                    var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");

                                    var UserNextPaymentDate = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);
                                    string TransactionID = string.Empty;
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
                                    + "</td></tr>") : "") + "</table>");

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
                                    + "</td></tr>") : "") + "</table>");

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
                                        SendEmailAttachment(CustomerContent, "Your order " + userOrder.InvoiceNumber + " on WFTCloud.com", userProf.EmailID, false, true, AttachmentPath);
                                    }
                                    else
                                    {
                                        SMTPManager.SendEmail(CustomerContent, "Your order " + userOrder.InvoiceNumber + " on WFTCloud.com", userProf.EmailID, false, true);
                                    }
                                }
                                else
                                {
                                    SMTPManager.SendEmail(CustomerContent, "Your order " + userOrder.InvoiceNumber + " on WFTCloud.com", userProf.EmailID, false, true);
                                }
                                /*
                                //*******
                                //Sent Email Notification to the admin about new order 
                                //******
                                string ServiceNameDEtails = "";
                                var UserProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == userProf.EmailID);
                                string message = (("<strong></strong> " + UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName));
                                ServiceNameDEtails = ("<strong>Service ID :</strong> " + ServiceID + "<br />"
                                                   + "<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                                   + "<strong>System Type :</strong> " + serviceDetails.SystemType + "<br />"
                                                   + "<strong>Release Version :</strong> " + serviceDetails.ReleaseVersion + "<br />"
                                                   + "<strong>User Name :</strong> " + UserFullName + "<br />"
                                                   + "<strong>User EmailID :</strong> " + userProf.EmailID + "<br /><br />");

                                string EmailContent1 = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewServiceRequestToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/new-service-request.html"));
                                EmailContent1 = EmailContent1.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string AdminContent1 = EmailContent1.Replace("++AddContentHere++", ServiceNameDEtails).Replace("++name++", message);
                                SMTPManager.SendAdminNotificationEmail(AdminContent1, "New Order " + userOrder.InvoiceNumber, false);
                                */
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
        private void ApplyCouponForCount(string couponcode,int userprofileid)
        {
            UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            var Couponvalid = objDBContext.Coupons.FirstOrDefault(a => a.CouponCode == couponcode && a.IsUsed == false && a.RecordStatus == DBKeys.RecordStatus_Active && (a.ValidityDate >= DateTime.Today || a.ValidityDate == null) && (a.CouponCount > 0 || a.CouponCount == null));

            if (Couponvalid != null)
            {
                var CouponTypeDetails = objDBContext.CouponTypes.FirstOrDefault(c => c.CouponTypeID == Couponvalid.CouponTypeID);
                if (Couponvalid.ForUser != null && Couponvalid.ForServiceID != null)
                {
                    if (Couponvalid.ForUser == ID)
                    {
                        if (CouponTypeDetails.IsCouponUnlimited != true)
                        {
                            var UserCartDetail = objDBContext.UserCarts.Where(a => a.UserProfileID == userprofileid && a.RecordStatus == 999).ToList();
                            foreach (var ORDResult in UserCartDetail)
                            {
                                long ServiceID = ORDResult.ServiceID;
                                if (Couponvalid.ForServiceID == ServiceID)
                                {
                                    Couponvalid.CouponCount -= 1;
                                    objDBContext.SaveChanges();
                                    return;
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
                            return;
                        }
                    }
                }
                else if (Couponvalid.ForUser == null && Couponvalid.ForServiceID != null)
                {
                    var UserCartDetail = objDBContext.UserCarts.Where(a => a.UserProfileID == userprofileid && a.RecordStatus == 999).ToList();
                    foreach (var ORDResult in UserCartDetail)
                    {
                        long ServiceID = ORDResult.ServiceID;
                        if (Couponvalid.ForServiceID == ServiceID)
                        {
                            if (CouponTypeDetails.IsCouponUnlimited != true)
                            {
                                Couponvalid.CouponCount -= 1;
                                objDBContext.SaveChanges();
                                return;
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
                        return;
                    }
                }
            }
        }
        #endregion
    }

}