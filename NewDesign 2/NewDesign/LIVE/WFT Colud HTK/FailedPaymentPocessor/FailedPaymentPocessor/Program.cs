
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using AuthorizeNet;
using System.Net.Mail;
using System.Net;
using WFTCloud.DataAccess;
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.PayPalAPIInterfaceService;

namespace FailedPaymentPocessor
{
    class Program
    {
        static SqlConnection sqlConnection = new SqlConnection();
        static string LogFileData = string.Empty;
        static cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        static void Main(string[] args)
        {
            // Get list of all records that are ready for payment for today. These records shouldbe grouped by customer and his credit card
            //On Successful payment,store all the details from Authorize.Net into the data base
            //ON Successful Payment, send out a confirmation email to the user and administrator
            // On any failures, the details should be notified to Administrator as a daily report.
            // Failure messages should be sent to the User too.

            LogExecutionComment("---------------------------------------------------------", false);
            LogExecutionComment("Payment Processing Started...", true);
            LogExecutionComment(string.Empty, true);

            try
            {

                DataTable dtAuthorizePaymentRecords = new DataTable();
                DataTable dtPayPalPaymentRecords = new DataTable();

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand Authorizecommand = new SqlCommand("pr_GetTodaysFailedPaymentItems", sqlConnection);
                Authorizecommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader AuthorizesqlData = Authorizecommand.ExecuteReader();
                dtAuthorizePaymentRecords.Load(AuthorizesqlData);
                AuthorizesqlData.Close();

                SqlCommand PayPalcommand = new SqlCommand("pr_GetTodaysFailedPaymentItems", sqlConnection);
                PayPalcommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader PayPalsqlData = PayPalcommand.ExecuteReader();
                dtPayPalPaymentRecords.Load(PayPalsqlData);
                PayPalsqlData.Close();

                LogExecutionComment("Processing Individual Payments...", true);

                foreach (DataRow dtRow in dtAuthorizePaymentRecords.Rows)
                {
                    string CustomerProfileID = dtRow["CustomerProfileID"].ToString();

                    string PaymentProfileID = dtRow["CustomerPaymentProfileID"].ToString();

                    decimal BillingAmount = Convert.ToDecimal(dtRow["BillingAmount"].ToString());

                    string PaypalBillingAgreementID = dtRow["PaypalBillingAgreementID"].ToString();

                    string PaypalPaymentTransactionID = dtRow["PaypalPaymentTransactionID"].ToString();

                    string PaymentMethod = dtRow["PaymentMethod"].ToString();

                    if (dtRow["PaymentMethod"].ToString() == "Authorize.net" || PaymentMethod.ToString() == "")
                    {
                        CallAuthorizeNetPayment(PaypalBillingAgreementID, PaypalPaymentTransactionID, CustomerProfileID, PaymentProfileID, PaymentMethod, BillingAmount, DateTime.Today);
                    }
                }

                foreach (DataRow dtRow in dtPayPalPaymentRecords.Rows)
                {
                    string CustomerProfileID = dtRow["CustomerProfileID"].ToString();

                    string PaymentProfileID = dtRow["CustomerPaymentProfileID"].ToString();

                    decimal BillingAmount = Convert.ToDecimal(dtRow["BillingAmount"].ToString());

                    //int UserSubscriptionID = Convert.ToInt32(dtRow["UserSubscriptionID"]);

                    string PaypalBillingAgreementID = dtRow["PaypalBillingAgreementID"].ToString();

                    string PaypalPaymentTransactionID = dtRow["PaypalPaymentTransactionID"].ToString();

                    string PaymentMethod = dtRow["PaymentMethod"].ToString();

                    if (dtRow["PaymentMethod"].ToString() == "PayPal")
                    {
                        CallPayPalPayment(PaypalBillingAgreementID, PaypalPaymentTransactionID, CustomerProfileID, PaymentProfileID, PaymentMethod, BillingAmount, DateTime.Today);
                    }
                }



                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("Payment Processing for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("PaymentProcessor", "Processing Individual Payments", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);

            }


            LogExecutionComment("---------------------------------------------------------", false);
            LogExecutionComment("De-Activate ExpiredSubscription Processing Started...", true);
            LogExecutionComment(string.Empty, true);

            try
            {

                DataTable ExpiredSubscription = new DataTable();

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("pr_DeActivateExpiredSubscription", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader sqlData = command.ExecuteReader();
                ExpiredSubscription.Load(sqlData);
                sqlData.Close();

                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("De-Activate ExpiredSubscription Processing for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("PaymentProcessor", "De-Activate ExpiredSubscription", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);
            }

            //Console.ReadLine();
        }
        public static DateTime NextMonth(DateTime date)
        {
            if (date.Day != DateTime.DaysInMonth(date.Year, date.Month))
                return date.AddMonths(1);
            else
                return date.AddDays(1).AddMonths(1).AddDays(-1);
        }
        //New method has been created to get sum of all payments(Authorize.net) for each user and send it as one transaction by the end of the day. If a single user has used multiple cards to buy services at different times, then the sum would be calculated based on the card number as well on 18-Sep-2014
        private static void CallAuthorizeNetPayment(string PaypalBillingAgreementID, string PaypalPaymentTransactionID, string CustomerProfileID, string PaymentProfileId, string PaymentMethod, decimal PaymentAmount, DateTime PaymentDate)
        {
            try
            {

                LogExecutionComment("CallAuthorizeNetPayment...", true);
                //string BillingAddressID = GetBillingAddressID(PaymentProfileId);
                string InvoiceNumber = GetNewInvoiceNumber();
                decimal ReturnAmount = 0M;
                bool ReturnApproved = false;
                string ReturnAuthAuthorizationCode = "";
                string ReturnAuthCardNumber = "";
                string ReturnAuthMessage = "";
                string ReturnAuthResponseCode = "";
                string ReturnAuthTransactionID = "";
                string ReturnInvoiceNumber = "";
                string PaymentDetailsMailContent = "";
                string PaypalPayerMailid = "";
                string PaypalPayerID = "";
                string ResponsePaypalPaymentTransactionID = "";
                string ResponsePaypalBillingAgreementID = "";
                string UserSubscriptionIDs = "";
                int UserProfileID = 0;

                
                if (PaymentMethod.ToString() == "Authorize.net" || PaymentMethod.ToString() == "")
                {
                    var getUserProID = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthCustomerProfileID == CustomerProfileID);// Authorize.net
                    UserProfileID = Convert.ToInt32(getUserProID.UserProfileID);
                    LogExecutionComment("Authorize.net - Started", true);
                    #region Authorize.net
                    bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
                    AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;

                    AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);

                    var customer = objGW.GetCustomer(CustomerProfileID);

                    string BillingAddressID = string.Empty;

                    foreach (var pprofile in customer.PaymentProfiles)
                    {
                        if (pprofile.ProfileID == PaymentProfileId)
                            BillingAddressID = pprofile.BillingAddress.ID;
                    }

                    AuthorizeNet.Order objOrder = new AuthorizeNet.Order(CustomerProfileID, PaymentProfileId, BillingAddressID);
                    objOrder.InvoiceNumber = InvoiceNumber;
                    objOrder.Amount = PaymentAmount;
                    objOrder.Description = "WFT SAP Services"; // Customer services to be added for this field
                    var response = (AuthorizeNet.GatewayResponse)objGW.AuthorizeAndCapture(objOrder);

                    ReturnAmount = response.Amount == null ? PaymentAmount : response.Amount;
                    ReturnApproved = response.Approved == null ? false : response.Approved;
                    ReturnAuthAuthorizationCode = response.AuthorizationCode == null ? "-" : response.AuthorizationCode;
                    ReturnAuthCardNumber = response.CardNumber == null ? "-" : response.CardNumber;
                    ReturnAuthMessage = response.Message == null ? "-" : response.Message;
                    ReturnAuthResponseCode = response.ResponseCode == null ? "-" : response.ResponseCode;
                    ReturnAuthTransactionID = response.TransactionID == null ? "-" : response.TransactionID;
                    ReturnInvoiceNumber = InvoiceNumber;
                    var usersubscriptions = objDBContext.AllAutomatedPayments.Where(A => A.CustomerProfileID == CustomerProfileID && A.CustomerPaymentProfileID == PaymentProfileId && A.PaymentDate == PaymentDate && A.PaymentStatus != "CancelledSubscription");
                    foreach (var res in usersubscriptions)
                    {
                        UserSubscriptionIDs += (res.UserSubscriptionID.ToString() + ",");
                    }
                    var UserInfo = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                    UserSubscriptionIDs = UserSubscriptionIDs.Remove(UserSubscriptionIDs.LastIndexOf(","));
                    PaymentMethod = "Authroize.net";
                    //PaymentDetailsMailContent = "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>Authroize.net</td></tr>"
                    //                            + "<tr valign='top'><td><strong>Card Number</strong></td><td>:</td><td>" + ReturnAuthCardNumber + "</td></tr>"
                    //                            + "<tr valign='top'><td><strong>Auth Message</strong></td><td>:</td><td>" + ReturnAuthMessage + "</td></tr>"
                    //                            + "<tr valign='top'><td><strong>Auth Response Code</strong></td><td>:</td><td>" + ReturnAuthResponseCode + "</td></tr>"
                    //                            + "<tr valign='top'><td><strong>User Subscription ID's</strong></td><td>:</td><td>" + UserSubscriptionIDs + "</td></tr>"
                    //                            + "<tr valign='top'><td><strong>Amount Paid</strong></td><td>:</td><td>" + ReturnAmount + "</td></tr>";

                    //For Email to Admin and User

                    string Content = "";
                    string PaymentStatus = "";
                    string ContactAdmin = "";
                    string CustomerContent = "";

                    string Dear = "";
                    string CustomerName = UserInfo.LastName + " " + UserInfo.FirstName;
                    if (ReturnApproved == true)
                    {
                        Content = "The Recurring payment from our exisitng Customer " + CustomerName + " has been processed successfully thru Authorize.Net for the month of " + DateTime.Now.ToString("MMMM") + "";
                        PaymentStatus = "Payment Successful";
                        CustomerContent = "Your Recurring payment for the below mentioned service has been processed successfully thru Authorize.Net for the month of " + DateTime.Now.ToString("MMMM") + ". Thanks for your subscription and continued support.";
                    }
                    else if (ReturnApproved == false)
                    {
                        Content = "The recurring monthly payment for our existing customer " + CustomerName + " failed for the month of " + DateTime.Now.ToString("MMMM") + ".";
                        PaymentStatus = "Payment Failed";
                        ContactAdmin = " Please contact admin immediately";
                        CustomerContent = "Your recurring payment for the below Subscribed service with WFT Cloud failed for the month of " + DateTime.Now.ToString("MMMM") + " Pls. check with your Credit Card company or update your CC information within 24hrs by logging into your account profile from WFT cloud website to avoid any interuption to your services. For Further assisstance to clear your pending payments contact WFT Cloud account administrator at admin@wftcloud.com";
                    }

                    int Approved = (ReturnApproved ? 1 : 0);
                    // Worklog update
                    objDBContext.pr_GroupOfUpdatePaymentResponse(CustomerProfileID, PaymentProfileId, ReturnAmount, Approved,
                                                           ReturnAuthAuthorizationCode, ReturnAuthCardNumber,
                                                           ReturnAuthMessage, ReturnAuthResponseCode, ReturnAuthTransactionID,
                                                           InvoiceNumber, ResponsePaypalBillingAgreementID,
                                                           ResponsePaypalPaymentTransactionID, PaymentMethod,
                                                           PaypalPayerMailid, PaypalPayerID, PaypalBillingAgreementID,
                                                           PaypalPaymentTransactionID, UserProfileID, UserSubscriptionIDs);


                    foreach (var res in usersubscriptions)
                    {
                        String InstanceNumber = string.Empty;
                        String ApplicationServer = string.Empty;
                        String SID = string.Empty;
                        String DeveloperKey = string.Empty;
                        String ServiceOtherInformation = string.Empty;

                        var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == res.UserSubscriptionID);
                        var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                        var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                        var UserRecurringInfo = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == res.UserSubscriptionID);
                        var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                        DateTime NextBillDate = NextMonth(DateTime.Now);
                        DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                        var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                        var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                        if (ServiceProvisioningCheck.Count() > 0)
                        {
                            InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                            if (InstanceNumber == null || InstanceNumber == "")
                            {
                                InstanceNumber = "N/A";
                            }
                            else
                            {
                                InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                            }
                            ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                            if (ApplicationServer == null || ApplicationServer == "")
                            {
                                ApplicationServer = "N/A";
                            }
                            else
                            {
                                ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                            }
                            SID = ServiceProvisioningDEtails.UIDOnServer;
                            if (SID == null || SID == "")
                            {
                                SID = "N/A";
                            }
                            else
                            {
                                SID = ServiceProvisioningDEtails.UIDOnServer;
                            }
                            DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                            if (DeveloperKey == null || DeveloperKey == "")
                            {
                                DeveloperKey = "To be purchased separately";
                            }
                            else
                            {
                                DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                            }
                            ServiceOtherInformation = ServiceProvisioningDEtails.AdditionalInformation;
                        }
                        else
                        {
                            InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " N/A ");
                            ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " N/A ");
                            SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " N/A "); 
                            DeveloperKey = UserSubInfo.DeveloperKey;
                            if (DeveloperKey == null || DeveloperKey == "")
                            {
                                DeveloperKey = "To be purchased separately";
                            }
                            else
                            {
                                DeveloperKey = UserSubInfo.DeveloperKey;
                            }
                            ServiceOtherInformation = (UserSubInfo.ServiceOtherInformation != null ? UserSubInfo.ServiceOtherInformation : " - "); 
                        }
                       
                        PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                        + "<td><strong>Subscription ID </strong></td><td>" + res.UserSubscriptionID + "<br /></td></tr>"
                                        + "<tr><td><strong>User Name </strong></td><td>" + UserInfo.LastName + " " + UserInfo.FirstName + "<br /></td></tr>"
                                         + "<tr><td><strong>User Email </strong></td><td>" + UserInfo.EmailID +  "<br /></td></tr>"
                                        + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                                        + "<tr><td><strong>Service Category </strong></td><td>" + ServiceDetails.SystemType + "<br /></td></tr>"
                                        + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                        + "<tr><td rowspan='9'><strong>Payment Transaction Information</strong></td><td><strong>Subscription Purchase Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment Date </strong></td><td>" + DateTime.Now.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                        + "<tr><td><strong>Next Payment Date </strong></td><td>" + NextBillDate.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment Mode </strong></td><td>" + "Authroize.net" + "<br /></td></tr>"
                                        + "<tr><td><strong>Invoice Number </strong></td><td>" + ReturnInvoiceNumber + "<br /></td></tr>"
                                        + "<tr><td><strong>Transaction ID </strong></td><td>" + (ReturnAuthTransactionID == "" ? ResponsePaypalPaymentTransactionID : ReturnAuthTransactionID) + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment Amount </strong>"
                                        + "</td><td>" + UserRecurringInfo.CurrentMonthBilling + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment Status </strong></td><td>" + (ReturnAuthResponseCode == "1" ? "Success" : "Failed") + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment Response </strong></td><td>" + ReturnAuthMessage + "<br /></td></tr>"
                                        + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                        + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                        + "</td></tr>"
                                        + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                        + "<tr><td><strong>User Name </strong></td><td>" + UserSubInfo.ServiceUserName + "<br /></td></tr>"
                                        + "<tr><td><strong>Password </strong></td><td>" + UserSubInfo.ServicePassword + "<br /></td></tr>"
                                        + "<tr><td><strong>Developer Key </strong></td><td>" + DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                        + "</td></tr>") : "") + "</table>");

                        PaymentDetailsMailContent += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                        PaymentDetailsMailContent += ("<table><tr><td>" + ServiceOtherInformation + "<br /></td></tr></table>");

                    ContactAdmin = "";
                    Dear = "Admin,";
                    string PaymentResult = (ReturnAuthResponseCode == "1" ? "Successfull" : "Failed");
                    string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string AdminContent = EmailContent.Replace("++dear++", Dear)
                                        .Replace("++Content++", Content).Replace("++PaymentStatus++", PaymentStatus)
                                        .Replace("++ContactAdmin++", ContactAdmin)
                                        .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                    SendAdminNotificationEmail(AdminContent, "" + PaymentResult + " Payment  for " + User.LastName + " " + User.FirstName, false);


                    string UserEmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                    UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string UserContent = UserEmailContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                       .Replace("++Content++", CustomerContent)
                                       .Replace("++PaymentStatus++", PaymentStatus).Replace("++ContactAdmin++", ContactAdmin)
                                       .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                    SendEmail(UserContent, "WFT Cloud : Your Monthly Payment Notification", User.EmailID, false, true);

                    if (ReturnApproved == false)
                    {
                        string Serviceinfo = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                               + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                               + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                               + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                               + "<tr><td><strong>Service Category </strong></td><td>" + ServiceDetails.SystemType + "<br /></td></tr>"
                               + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                               + "<tr><td><strong>Active Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                               + "</table>");

                        string UserEmailInfoContent = File.ReadAllText("./EmailTemplates/CardFailure-Details.html");
                        UserEmailContent = UserEmailInfoContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string UserInfoContent = UserEmailInfoContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                           .Replace("++ServiceDetails++", Serviceinfo)
                                           .Replace("++month++", DateTime.Now.ToString("MMMM"));
                        SendEmail(UserInfoContent, "WFT Cloud subscribe service - Your payment declined for " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year + " ", User.EmailID, false, true);
                    }

                    }

                    LogExecutionComment("Authorize.net - Completed", true);
                    #endregion
                }
                
                
                

                if (PaymentMethod == "PayPal")
                {
                    LogExecutionComment("Billing Agreement ID : " + PaypalBillingAgreementID, true);
                    LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
                    LogExecutionComment("     Message   : " + (ReturnApproved == true ? "SUCCESS" : "FAILED"), true);
                }
                else
                {
                    LogExecutionComment("Customer Profile ID : " + CustomerProfileID, true);
                    LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
                    LogExecutionComment("     Message   : " + ReturnAuthMessage, true);
                }
                LogExecutionComment("CallAuthorizeNetPayment...", false);

                


            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "CallAuthorizeNetPayment", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
            }
        }

       

        // Calling Method For PayPal Method for individual subscription
        private static void CallPayPalPayment(string PaypalBillingAgreementID, string PaypalPaymentTransactionID, string CustomerProfileID, string PaymentProfileId, string PaymentMethod, decimal PaymentAmount, DateTime PaymentDate)
        {
            try
            {

                LogExecutionComment("CallPayPalPayment...", true);
                //string BillingAddressID = GetBillingAddressID(PaymentProfileId);
                string InvoiceNumber = GetNewInvoiceNumber();
                decimal ReturnAmount = 0M;
                bool ReturnApproved = false;
                string ReturnAuthAuthorizationCode = "";
                string ReturnAuthCardNumber = "";
                string ReturnAuthMessage = "";
                string ReturnAuthResponseCode = "";
                string ReturnAuthTransactionID = "";
                string ReturnInvoiceNumber = "";
                string PaymentDetailsMailContent = "";
                string PaypalPayerMailid = "";
                string PaypalPayerID = "";
                string ResponsePaypalPaymentTransactionID = "";
                string ResponsePaypalBillingAgreementID = "";
                string UserSubscriptionIDs = "";
                int UserProfileID;
                //  objDBContext.CustomerPaymentProfiles.Where(a => a.AuthCustomerProfileID == CustomerProfileID).FirstOrDefault().UserProfileID;// Authorize.net
                //var usrSubSer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);
                var UserpaymenTrans = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.PalpalPaymentTransactionID == PaypalPaymentTransactionID && a.PaypalBillingAgreementID == PaypalBillingAgreementID);
                UserProfileID = Convert.ToInt32(UserpaymenTrans.UserProfileID);
                if (PaymentMethod == "PayPal")
                {
                    UserSubscriptionIDs = "";
                    LogExecutionComment("PayPal - Started", true);
                    #region Paypal
                    //var UserpaymenTrans = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.PalpalPaymentTransactionID == PaypalPaymentTransactionID && a.PaypalBillingAgreementID == PaypalBillingAgreementID);
                    var usersubscriptions = objDBContext.AllAutomatedPayments.Where(A => A.PaypalBillingAgreementID == PaypalBillingAgreementID && A.PaypalPaymentTransactionID == PaypalPaymentTransactionID && A.PaymentDate == PaymentDate && A.PaymentStatus != "CancelledSubscription");
                    foreach (var res in usersubscriptions)
                    {
                        UserSubscriptionIDs += (res.UserSubscriptionID.ToString() + ",");
                    }
                    UserSubscriptionIDs = UserSubscriptionIDs.Remove(UserSubscriptionIDs.LastIndexOf(","));
                    if (UserpaymenTrans != null)
                    {
                        ReturnAmount = PaymentAmount;
                        ReturnApproved = false;
                        PaypalPayerMailid = UserpaymenTrans.PaypalPayerMailID == null ? "-" : UserpaymenTrans.PaypalPayerMailID;
                        PaypalPayerID = UserpaymenTrans.PaypalPayerID == null ? "-" : UserpaymenTrans.PaypalPayerID;
                        PaymentMethod = "PayPal";

                        DoReferenceTransactionResponseType DoRfTransResponse = DoReferenceTransactionAPIOperation(PaypalPaymentTransactionID, PaymentAmount.ToString());
                        if (DoRfTransResponse.DoReferenceTransactionResponseDetails != null)
                        {
                            ReturnApproved = DoRfTransResponse.Ack.ToString().ToUpper() == "SUCCESS" ? true : false;
                            ResponsePaypalBillingAgreementID = DoRfTransResponse.DoReferenceTransactionResponseDetails.BillingAgreementID == null ? UserpaymenTrans.PaypalBillingAgreementID : DoRfTransResponse.DoReferenceTransactionResponseDetails.BillingAgreementID;
                            ResponsePaypalPaymentTransactionID = DoRfTransResponse.DoReferenceTransactionResponseDetails.PaymentInfo.TransactionID == null ? "-" : DoRfTransResponse.DoReferenceTransactionResponseDetails.PaymentInfo.TransactionID;
                            ReturnInvoiceNumber = InvoiceNumber;
                            //PaymentDetailsMailContent =
                            //                         "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>PayPal</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Payer Mail ID</strong></td><td>:</td><td>" + PaypalPayerMailid + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Payer ID</strong></td><td>:</td><td>" + PaypalPayerID + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Billing Agreement ID</strong></td><td>:</td><td>" + ResponsePaypalBillingAgreementID + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>User Subscription ID's</strong></td><td>:</td><td>" + UserSubscriptionIDs + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Amount Paid</strong></td><td>:</td><td>" + ReturnAmount + "</td></tr>";

                            int Approved = (ReturnApproved ? 1 : 0);
                            objDBContext.pr_GroupOfUpdatePaymentResponse(CustomerProfileID, PaymentProfileId, ReturnAmount, Approved,
                                                                   ReturnAuthAuthorizationCode, ReturnAuthCardNumber,
                                                                   ReturnAuthMessage, ReturnAuthResponseCode, ReturnAuthTransactionID,
                                                                   InvoiceNumber, ResponsePaypalBillingAgreementID,
                                                                   ResponsePaypalPaymentTransactionID, PaymentMethod,
                                                                   PaypalPayerMailid, PaypalPayerID, PaypalBillingAgreementID,
                                                                   PaypalPaymentTransactionID, UserProfileID, UserSubscriptionIDs);


                            foreach (var res in usersubscriptions)
                            {
                                String InstanceNumber = string.Empty;
                                String ApplicationServer = string.Empty;
                                String SID = string.Empty;
                                String DeveloperKey = string.Empty;
                                String ServiceOtherInformation = string.Empty;

                                var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == res.UserSubscriptionID);
                                var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                                int CategoryID = Convert.ToInt32(ServiceDetails.ServiceCategoryID);
                                var categoryDetails = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
                                var UserRecurringInfo = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == res.UserSubscriptionID);
                                var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                                var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                                DateTime NextBillDate = NextMonth(DateTime.Now);
                                DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                                var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                                var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                                if (ServiceProvisioningCheck.Count() > 0)
                                {
                                    InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                    if (InstanceNumber == null || InstanceNumber == "")
                                    {
                                        InstanceNumber = "N/A";
                                    }
                                    else
                                    {
                                        InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                    }
                                    ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                    if (ApplicationServer == null || ApplicationServer == "")
                                    {
                                        ApplicationServer = "N/A";
                                    }
                                    else
                                    {
                                        ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                    }
                                    SID = ServiceProvisioningDEtails.UIDOnServer;
                                    if (SID == null || SID == "")
                                    {
                                        SID = "N/A";
                                    }
                                    else
                                    {
                                        SID = ServiceProvisioningDEtails.UIDOnServer;
                                    }
                                    DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                    if (DeveloperKey == null || DeveloperKey == "")
                                    {
                                        DeveloperKey = "To be purchased separately";
                                    }
                                    else
                                    {
                                        DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                    }
                                    ServiceOtherInformation = ServiceProvisioningDEtails.AdditionalInformation;
                                }
                                else
                                {

                                    InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " N/A ");
                                    ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " N/A ");
                                    SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " N/A ");
                                    DeveloperKey = UserSubInfo.DeveloperKey;
                                    if (DeveloperKey == null || DeveloperKey == "")
                                    {
                                        DeveloperKey = "To be purchased separately";
                                    }
                                    else
                                    {
                                        DeveloperKey = UserSubInfo.DeveloperKey;
                                    }
                                    ServiceOtherInformation = (UserSubInfo.ServiceOtherInformation != null ? UserSubInfo.ServiceOtherInformation : " - ");
                                }
                                  

                                PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                            + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                                            + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                             + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                            + "<tr><td rowspan='8'><strong>Payment Transaction Information</strong></td><td><strong>Subscription Purchase Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Date </strong></td><td>" + DateTime.Now.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Next Payment Date </strong></td><td>" + NextBillDate.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Mode </strong></td><td>" + "PayPal" + "<br /></td></tr>"
                                            + "<tr><td><strong>Invoice Number </strong></td><td>" + ReturnInvoiceNumber + "<br /></td></tr>"
                                            + "<tr><td><strong>Transaction ID </strong></td><td>" + (ReturnAuthTransactionID == "" ? ResponsePaypalPaymentTransactionID : ReturnAuthTransactionID) + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Amount </strong>"
                                            + "</td><td>" + UserRecurringInfo.CurrentMonthBilling + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Status </strong></td><td>" + (ReturnApproved == true ? "Success" : "Failed") + "<br /></td></tr>"
                                            + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                            + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                            + "</td></tr>"
                                            + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                            + "<tr><td><strong>User Name </strong></td><td>" + UserSubInfo.ServiceUserName + "<br /></td></tr>"
                                            + "<tr><td><strong>Password </strong></td><td>" + UserSubInfo.ServicePassword + "<br /></td></tr>"
                                            + "<tr><td><strong>Developer Key </strong></td><td>" + DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                            + "</td></tr>") : "") + "</table>");

                                PaymentDetailsMailContent += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                PaymentDetailsMailContent += ("<table><tr><td>" + ServiceOtherInformation + "<br /></td></tr></table>");

                                //For Email to Admin and User

                                string Content = "";
                                string PaymentStatus = "";
                                string ContactAdmin = "";
                                string CustomerContent = "";
                                string PaymentResult = (ReturnApproved == true ? "Successfull" : "Failed");
                                string Dear = "";
                                string CustomerName = User.LastName + " " + User.FirstName;
                                if (ReturnApproved == true)
                                {
                                    Content = "The Recurring payment from our exisitng Customer " + CustomerName + " has been processed successfully thru PayPal for the month of " + DateTime.Now.ToString("MMMM") + "";
                                    PaymentStatus = "Payment Successful";
                                    CustomerContent = "Your Recurring payment for the below mentioned service has been processed successfully thru PayPal for the month of " + DateTime.Now.ToString("MMMM") + ". Thanks for your subscription and continued support.";
                                }
                                else if (ReturnApproved == false)
                                {
                                    Content = "The recurring monthly payment for our existing customer " + CustomerName + " failed for the month of " + DateTime.Now.ToString("MMMM") + ".";
                                    PaymentStatus = "Payment Failed";
                                    ContactAdmin = " Please contact admin immediately";
                                    CustomerContent = "Your recurring payment for the below Subscribed service with WFT Cloud failed for the month of " + DateTime.Now.ToString("MMMM") + " Pls. check with your Credit Card company or update your CC information within 24hrs by logging into your account profile from WFT cloud website to avoid any interuption to your services. For Further assisstance to clear your pending payments contact WFT Cloud account administrator at admin@wftcloud.com"; 
                                }
                                ContactAdmin = "";
                                Dear = "Admin,";
                                string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string AdminContent = EmailContent.Replace("++dear++", Dear)
                                                    .Replace("++Content++", Content).Replace("++PaymentStatus++", PaymentStatus)
                                                    .Replace("++ContactAdmin++", ContactAdmin)
                                                    .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
                                                    .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                                SendAdminNotificationEmail(AdminContent, "" + PaymentResult + " Payment for " + User.LastName + " " + User.FirstName, false);


                                string UserEmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                                UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string UserContent = UserEmailContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                                   .Replace("++Content++", CustomerContent)
                                                   .Replace("++PaymentStatus++", PaymentStatus).Replace("++ContactAdmin++", ContactAdmin)
                                                   .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
                                                   .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                                SendEmail(UserContent, "WFT Cloud : Your Monthly Payment Notification", User.EmailID, false, true);

                                if (ReturnApproved == false)
                                {
                                    string Serviceinfo = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                           + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                                           + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                            + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                           + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                                           + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                           + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                           + "<tr><td><strong>Active Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                                           + "</table>");

                                    string UserEmailInfoContent = File.ReadAllText("./EmailTemplates/CardFailure-Details.html");
                                    UserEmailContent = UserEmailInfoContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                    string UserInfoContent = UserEmailInfoContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                                       .Replace("++ServiceDetails++", Serviceinfo)
                                                       .Replace("++month++", DateTime.Now.ToString("MMMM"));
                                    SendEmail(UserInfoContent, "WFT Cloud subscribe service - Your payment declined for " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year + " ", User.EmailID, false, true);
                                }
                            }
                        }
                        else
                        {
                            ResponsePaypalBillingAgreementID = UserpaymenTrans.PaypalBillingAgreementID;
                            ResponsePaypalPaymentTransactionID = "-";
                            ReturnInvoiceNumber = InvoiceNumber;
                            //PaymentDetailsMailContent = "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>PayPal</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Payer Mail ID</strong></td><td>:</td><td>" + PaypalPayerMailid + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Payer ID</strong></td><td>:</td><td>" + PaypalPayerID + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Billing Agreement ID</strong></td><td>:</td><td>" + ResponsePaypalBillingAgreementID + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>User Subscription ID's</strong></td><td>:</td><td>" + UserSubscriptionIDs + "</td></tr>"
                            //                      + "<tr valign='top'><td><strong>Amount Paid</strong></td><td>:</td><td>" + ReturnAmount + "</td></tr>";


                            int Approved = (ReturnApproved ? 1 : 0);
                            objDBContext.pr_GroupOfUpdatePaymentResponse(CustomerProfileID, PaymentProfileId, ReturnAmount, Approved,
                                                                   ReturnAuthAuthorizationCode, ReturnAuthCardNumber,
                                                                   ReturnAuthMessage, ReturnAuthResponseCode, ReturnAuthTransactionID,
                                                                   InvoiceNumber, ResponsePaypalBillingAgreementID,
                                                                   ResponsePaypalPaymentTransactionID, PaymentMethod,
                                                                   PaypalPayerMailid, PaypalPayerID, PaypalBillingAgreementID,
                                                                   PaypalPaymentTransactionID, UserProfileID, UserSubscriptionIDs);


                            foreach (var res in usersubscriptions)
                            {
                                String InstanceNumber = string.Empty;
                                String ApplicationServer = string.Empty;
                                String SID = string.Empty;
                                String DeveloperKey = string.Empty;
                                String ServiceOtherInformation = string.Empty;

                                var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == res.UserSubscriptionID);
                                var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                                int CategoryID = Convert.ToInt32(ServiceDetails.ServiceCategoryID);
                                var UserRecurringInfo = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == res.UserSubscriptionID);
                                var categoryDetails = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
                                var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                                var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                                DateTime NextBillDate = NextMonth(DateTime.Now);
                                DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                                var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                                var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                                if (ServiceProvisioningCheck.Count() > 0)
                                {
                                    InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                    if (InstanceNumber == null || InstanceNumber == "")
                                    {
                                        InstanceNumber = "N/A";
                                    }
                                    else
                                    {
                                        InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                    }
                                    ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                    if (ApplicationServer == null || ApplicationServer == "")
                                    {
                                        ApplicationServer = "N/A";
                                    }
                                    else
                                    {
                                        ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                    }
                                    SID = ServiceProvisioningDEtails.UIDOnServer;
                                    if (SID == null || SID == "")
                                    {
                                        SID = "N/A";
                                    }
                                    else
                                    {
                                        SID = ServiceProvisioningDEtails.UIDOnServer;
                                    }
                                    DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                    if (DeveloperKey == null || DeveloperKey == "")
                                    {
                                        DeveloperKey = "To be purchased separately";
                                    }
                                    else
                                    {
                                        DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                    }
                                    ServiceOtherInformation = ServiceProvisioningDEtails.AdditionalInformation;
                                }
                                else
                                {

                                    InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " N/A ");
                                    ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " N/A ");
                                    SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " - ");
                                    DeveloperKey = UserSubInfo.DeveloperKey;
                                    if (DeveloperKey == null || DeveloperKey == "")
                                    {
                                        DeveloperKey = "To be purchased separately";
                                    }
                                    else
                                    {
                                        DeveloperKey = UserSubInfo.DeveloperKey;
                                    }
                                    ServiceOtherInformation = (UserSubInfo.ServiceOtherInformation != null ? UserSubInfo.ServiceOtherInformation : " N/A ");
                                }

                                PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                            + "<td><strong>Subscription ID </strong></td><td>" + res.UserSubscriptionID + "<br /></td></tr>"
                                            + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                             + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                            + "<tr><td rowspan='8'><strong>Payment Transaction Information</strong></td><td><strong>Subscription Purchase Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Date </strong></td><td>" + DateTime.Now.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Next Payment Date </strong></td><td>" + NextBillDate.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Mode </strong></td><td>" + "PayPal" + "<br /></td></tr>"
                                            + "<tr><td><strong>Invoice Number </strong></td><td>" + ReturnInvoiceNumber + "<br /></td></tr>"
                                            + "<tr><td><strong>Transaction ID </strong></td><td>" + (ReturnAuthTransactionID == "" ? ResponsePaypalPaymentTransactionID : ReturnAuthTransactionID) + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Amount </strong>"
                                            + "</td><td>" + UserRecurringInfo.CurrentMonthBilling + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment Status </strong></td><td>" + (ReturnApproved == true ? "Success" : "Failed") + "<br /></td></tr>"
                                            + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                            + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                            + "</td></tr>"
                                            + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                            + "<tr><td><strong>User Name </strong></td><td>" + UserSubInfo.ServiceUserName + "<br /></td></tr>"
                                            + "<tr><td><strong>Password </strong></td><td>" + UserSubInfo.ServicePassword + "<br /></td></tr>"
                                            + "<tr><td><strong>Developer Key </strong></td><td>" + DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                            + "</td></tr>") : "") + "</table>");

                                PaymentDetailsMailContent += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                PaymentDetailsMailContent += ("<table><tr><td>" + ServiceOtherInformation + "<br /></td></tr></table>");

                                //For Email to Admin and User

                                string Content = "";
                                string PaymentStatus = "";
                                string ContactAdmin = "";
                                string CustomerContent = "";
                                string PaymentResult = (ReturnApproved == true ? "Successfull" : "Failed");
                                string Dear = "";
                                if (ReturnApproved == true)
                                {
                                    Content = "Your payment for the month towards WFTCloud is processed successfully.";
                                    PaymentStatus = "Payment Successful";
                                    CustomerContent = "Your payment for the month towards WFTCloud is processed successfully.";
                                }
                                else if (ReturnApproved == false)
                                {
                                    Content = "The Monthly Payment process for the below user Failed!";
                                    PaymentStatus = "Payment Failed";
                                    ContactAdmin = " Please contact admin immediately";
                                    CustomerContent = "Your monthly automatic recurring payment has failed.  We request that you contact the WFT Cloud administrator at admin@wftcloud.com or call at 1-888-533-3113 immediately to update your payment information on your account and to process the pending payment.<br> To avoid a disruption of your services, you immediate action is required within twenty-four hours.";
                                }
                                ContactAdmin = "";
                                Dear = "Admin,";
                                string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string AdminContent = EmailContent.Replace("++dear++", Dear)
                                                    .Replace("++Content++", Content).Replace("++PaymentStatus++", PaymentStatus)
                                                    .Replace("++ContactAdmin++", ContactAdmin)
                                                    .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
                                                    .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                                SendAdminNotificationEmail(AdminContent, " "+ PaymentResult + " Payment for " + User.LastName + " " + User.FirstName, false);

                                string UserEmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
                                UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string UserContent = UserEmailContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                                   .Replace("++Content++", CustomerContent)
                                                   .Replace("++PaymentStatus++", PaymentStatus).Replace("++ContactAdmin++", ContactAdmin)
                                                   .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
                                                   .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                                SendEmail(UserContent, "WFT Cloud : Your Monthly Payment Notification", User.EmailID, false, true);

                                if (ReturnApproved == false)
                                {
                                    string Serviceinfo = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                           + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                                           + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                            + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                           + "<tr><td><strong>Payment For </strong></td><td>" + "Subscription Renewal" + "<br /></td></tr>"
                                           + "<tr><td><strong>Service Category </strong></td><td>" + categoryDetails.CategoryName + "<br /></td></tr>"
                                           + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                           + "<tr><td><strong>Active Date </strong></td><td>" + Convert.ToDateTime(UserSubInfo.ActiveDate).ToString("dd-MMM-yy") + "<br /></td></tr>"
                                           + "</table>");

                                    string UserEmailInfoContent = File.ReadAllText("./EmailTemplates/CardFailure-Details.html");
                                    UserEmailContent = UserEmailInfoContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                    string UserInfoContent = UserEmailInfoContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                                                       .Replace("++ServiceDetails++", Serviceinfo)
                                                       .Replace("++month++", DateTime.Now.ToString("MMMM"));
                                    SendEmail(UserInfoContent, "WFT Cloud subscribe service - Your payment declined for " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year + " ", User.EmailID, false, true);
                                }
                            }
                        }
                        int i = 1;
                        foreach (var errors in DoRfTransResponse.Errors)
                        {
                            LogExecutionComment("Error " + i + ": ErrorCode" + errors.ErrorCode + "-- Error Message:" + errors.LongMessage + "SeverityCode" + errors.SeverityCode, true);
                            i += 1;
                        }
                    }
                    #endregion
                    LogExecutionComment("PayPal - Completed", true);
                }
                //var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                
                if (PaymentMethod == "PayPal")
                {
                    LogExecutionComment("Billing Agreement ID : " + PaypalBillingAgreementID, true);
                    LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
                    LogExecutionComment("     Message   : " + (ReturnApproved == true ? "SUCCESS" : "FAILED"), true);
                }
                else
                {
                    LogExecutionComment("Customer Profile ID : " + CustomerProfileID, true);
                    LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
                    LogExecutionComment("     Message   : " + ReturnAuthMessage, true);
                }
                LogExecutionComment("CallPayPalPayment...", false);

              


            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "CallPayPalPayment", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
            }
        }

        // Old Payment Calling Method For Authorize.Net and PayPal
        //private static void CallAuthorizeNetPayPalPayment(string PaypalBillingAgreementID ,string PaypalPaymentTransactionID, string CustomerProfileID, string PaymentProfileId, string PaymentMethod, decimal PaymentAmount, int UserSubscriptionID)
        //{
        //    try
        //    {

        //        LogExecutionComment("CallAuthorizeNetPayPalPayment...", true);
        //        //string BillingAddressID = GetBillingAddressID(PaymentProfileId);
        //        string InvoiceNumber = GetNewInvoiceNumber();
        //        decimal ReturnAmount = 0M;
        //        bool ReturnApproved = false;
        //        string ReturnAuthAuthorizationCode = "";
        //        string ReturnAuthCardNumber = "";
        //        string ReturnAuthMessage = "";
        //        string ReturnAuthResponseCode = "";
        //        string ReturnAuthTransactionID = "";
        //        string ReturnInvoiceNumber = "";
        //        string PaymentDetailsMailContent = "";
        //        string PaypalPayerMailid = "";
        //        string PaypalPayerID = "";
        //        string ResponsePaypalPaymentTransactionID = "";
        //        string ResponsePaypalBillingAgreementID = "";
        //        int UserProfileID;
        //       //  objDBContext.CustomerPaymentProfiles.Where(a => a.AuthCustomerProfileID == CustomerProfileID).FirstOrDefault().UserProfileID;// Authorize.net
        //        var usrSubSer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);
        //        UserProfileID = Convert.ToInt32(usrSubSer.UserProfileID);
        //        if (PaymentMethod.ToString() == "Authorize.net" || PaymentMethod.ToString() == "")
        //        {
        //            LogExecutionComment("Authorize.net - Started", true);
        //            #region Authorize.net
        //            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
        //            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;

        //            AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);

        //            var customer = objGW.GetCustomer(CustomerProfileID);

        //            string BillingAddressID = string.Empty;

        //            foreach (var pprofile in customer.PaymentProfiles)
        //            {
        //                if (pprofile.ProfileID == PaymentProfileId)
        //                    BillingAddressID = pprofile.BillingAddress.ID;
        //            }

        //            AuthorizeNet.Order objOrder = new AuthorizeNet.Order(CustomerProfileID, PaymentProfileId, BillingAddressID);
        //            objOrder.InvoiceNumber = InvoiceNumber;
        //            objOrder.Amount = PaymentAmount;
        //            objOrder.Description = "WFT SAP Services"; // Customer services to be added for this field
        //            var response = (AuthorizeNet.GatewayResponse)objGW.AuthorizeAndCapture(objOrder);

        //            ReturnAmount = response.Amount == null ? PaymentAmount : response.Amount;
        //            ReturnApproved = response.Approved == null ? false : response.Approved;
        //            ReturnAuthAuthorizationCode = response.AuthorizationCode == null ? "-" : response.AuthorizationCode;
        //            ReturnAuthCardNumber = response.CardNumber == null ? "-" : response.CardNumber;
        //            ReturnAuthMessage = response.Message == null ? "-" : response.Message;
        //            ReturnAuthResponseCode = response.ResponseCode == null ? "-" : response.ResponseCode;
        //            ReturnAuthTransactionID = response.TransactionID == null ? "-" : response.TransactionID;
        //            ReturnInvoiceNumber = InvoiceNumber;
        //            PaymentMethod = "Authroize.net";
        //            PaymentDetailsMailContent = "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>Authroize.net</td></tr>"
        //                                        + "<tr valign='top'><td><strong>Card Number</strong></td><td>:</td><td>" + ReturnAuthCardNumber + "</td></tr>"
        //                                        + "<tr valign='top'><td><strong>Auth Message</strong></td><td>:</td><td>" + ReturnAuthMessage + "</td></tr>"
        //                                        + "<tr valign='top'><td><strong>Auth Response Code</strong></td><td>:</td><td>" + ReturnAuthResponseCode + "</td></tr>";
        //            LogExecutionComment("Authorize.net - Completed", true);
        //            #endregion
        //        }
        //        else if (PaymentMethod == "PayPal")
        //        {
        //            LogExecutionComment("PayPal - Started", true);
        //            #region Paypal
        //            var UserpaymenTrans = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.PalpalPaymentTransactionID == PaypalPaymentTransactionID && a.PaypalBillingAgreementID == PaypalBillingAgreementID);
        //            if (UserpaymenTrans != null)
        //            {
        //                ReturnAmount = PaymentAmount;
        //                ReturnApproved = false;
        //                PaypalPayerMailid = UserpaymenTrans.PaypalPayerMailID == null ? "-" : UserpaymenTrans.PaypalPayerMailID;
        //                PaypalPayerID = UserpaymenTrans.PaypalPayerID == null ? "-" : UserpaymenTrans.PaypalPayerID;
        //                PaymentMethod = "PayPal";

        //                DoReferenceTransactionResponseType DoRfTransResponse = DoReferenceTransactionAPIOperation(PaypalPaymentTransactionID, PaymentAmount.ToString());
        //                if (DoRfTransResponse.DoReferenceTransactionResponseDetails != null)
        //                {
        //                    ReturnApproved = DoRfTransResponse.Ack.ToString().ToUpper() == "SUCCESS" ? true : false;
        //                    ResponsePaypalBillingAgreementID = DoRfTransResponse.DoReferenceTransactionResponseDetails.BillingAgreementID == null ? UserpaymenTrans.PaypalBillingAgreementID : DoRfTransResponse.DoReferenceTransactionResponseDetails.BillingAgreementID;
        //                    ResponsePaypalPaymentTransactionID = DoRfTransResponse.DoReferenceTransactionResponseDetails.PaymentInfo.TransactionID == null ? "-" : DoRfTransResponse.DoReferenceTransactionResponseDetails.PaymentInfo.TransactionID;
        //                    ReturnInvoiceNumber = InvoiceNumber;
        //                    PaymentDetailsMailContent =
        //                                             "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>PayPal</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Payer Mail ID</strong></td><td>:</td><td>" + PaypalPayerMailid + "</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Payer ID</strong></td><td>:</td><td>" + PaypalPayerID + "</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Billing Agreement ID</strong></td><td>:</td><td>" + ResponsePaypalBillingAgreementID + "</td></tr>";
        //                }
        //                else
        //                {
        //                    ResponsePaypalBillingAgreementID = UserpaymenTrans.PaypalBillingAgreementID;
        //                    ResponsePaypalPaymentTransactionID = "-";
        //                    ReturnInvoiceNumber = InvoiceNumber;
        //                    PaymentDetailsMailContent = "<tr valign='top'><td><strong>Payment Method</strong></td><td>:</td><td>PayPal</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Payer Mail ID</strong></td><td>:</td><td>" + PaypalPayerMailid + "</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Payer ID</strong></td><td>:</td><td>" + PaypalPayerID + "</td></tr>"
        //                                          + "<tr valign='top'><td><strong>Billing Agreement ID</strong></td><td>:</td><td>" + ResponsePaypalBillingAgreementID + "</td></tr>";
        //                }
        //                foreach (var errors in DoRfTransResponse.Errors)
        //                {
        //                    int i = 1;
        //                    LogExecutionComment("Error " + i + ": ErrorCode" + errors.ErrorCode + "-- Error Message:" + errors.LongMessage + "SeverityCode" + errors.SeverityCode, true);
        //                    i += 1;
        //                }
        //            }
        //            #endregion
        //            LogExecutionComment("PayPal - Completed", true);
        //        }
        //        var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
        //        int Approved = (ReturnApproved ? 1 : 0);
        //        objDBContext.pr_UpdatePaymentResponse(CustomerProfileID, PaymentProfileId, ReturnAmount, Approved,
        //                                               ReturnAuthAuthorizationCode, ReturnAuthCardNumber,
        //                                               ReturnAuthMessage, ReturnAuthResponseCode, ReturnAuthTransactionID,
        //                                               InvoiceNumber, ResponsePaypalBillingAgreementID,
        //                                               ResponsePaypalPaymentTransactionID, PaymentMethod,
        //                                               PaypalPayerMailid, PaypalPayerID, PaypalBillingAgreementID,
        //                                               PaypalPaymentTransactionID, UserProfileID);
        //        if (PaymentMethod == "PayPal")
        //        {
        //            LogExecutionComment("Billing Agreement ID : " + PaypalBillingAgreementID, true);
        //            LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
        //            LogExecutionComment("     Message   : " + (ReturnApproved == true ? "SUCCESS" : "FAILED"), true);
        //        }
        //        else
        //        {
        //            LogExecutionComment("Customer Profile ID : " + CustomerProfileID, true);
        //            LogExecutionComment("     Approved? : " + ReturnApproved.ToString(), true);
        //            LogExecutionComment("     Message   : " + ReturnAuthMessage, true);
        //        }
        //        LogExecutionComment("CallAuthorizeNetPayPalPayment...", false);

        //        //For Email to Admin and User

        //        string Content = "";
        //        string PaymentStatus = "";
        //        string ContactAdmin = "";
        //        string CustomerContent = "";

        //        string Dear = "";
        //        if (ReturnApproved == true)
        //        {
        //            Content = "Your payment for the month towards WFTCloud is processed successfully.";
        //            PaymentStatus = "Payment Successful";
        //            CustomerContent = "Your payment for the month towards WFTCloud is processed successfully.";
        //        }
        //        else if (ReturnApproved == false)
        //        {
        //            Content = "The Monthly Payment process for the below user Failed!";
        //            PaymentStatus = "Payment Failed";
        //            ContactAdmin = " Please contact admin immediately";
        //            CustomerContent = "Your monthly payment process got failed and we reqeust you to reach out to WFTCloud Administrator immediately at admin@wftcloud.com. The Administrator is also available at   1-888-533-3113";
        //        }
        //        ContactAdmin = "";
        //        Dear = "Admin,";
        //        string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
        //        EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
        //        string AdminContent = EmailContent.Replace("++dear++", Dear)
        //                            .Replace("++TransactionID++", (ReturnAuthTransactionID == "" ? ResponsePaypalPaymentTransactionID : ReturnAuthTransactionID))
        //                            .Replace("++InvoiceNumber++", ReturnInvoiceNumber)
        //                            .Replace("++Content++", Content).Replace("++PaymentStatus++", PaymentStatus)
        //                            .Replace("++ContactAdmin++", ContactAdmin)
        //                            .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
        //                            .Replace("++PaymentDetails++", PaymentDetailsMailContent);
        //        SendAdminNotificationEmail(AdminContent, "Payments Details of " + User.LastName + " " + User.FirstName, false);


        //        string UserEmailContent = File.ReadAllText("./EmailTemplates/Payment-Receipt.html");
        //        UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
        //        string UserContent = UserEmailContent.Replace("++dear++", User.LastName + "," + User.FirstName)
        //                           .Replace("++TransactionID++", (ReturnAuthTransactionID == "" ? ResponsePaypalPaymentTransactionID : ReturnAuthTransactionID))
        //                           .Replace("++InvoiceNumber++", ReturnInvoiceNumber).Replace("++Content++", CustomerContent)
        //                           .Replace("++PaymentStatus++", PaymentStatus).Replace("++ContactAdmin++", ContactAdmin)
        //                           .Replace("++Username++", User.LastName + "," + User.FirstName + " (" + User.EmailID + ")")
        //                           .Replace("++PaymentDetails++", PaymentDetailsMailContent);
        //        SendEmail(UserContent, "WFT Cloud : Your Monthly Payment Notification", User.EmailID, false, true);


        //    }
        //    catch (Exception ex)
        //    {
        //        LogException("PaymentProcessor", "CallAuthorizeNetPayPalPayment", ex.Message, ex.StackTrace, DateTime.Now);
        //        LogExecutionComment("Error while Processing: " + ex.Message, true);
        //    }
        //}

        public static void LogException(string pageNameOrClassName, string MethodName, string ExMessage, string ExStackTrace, DateTime ExcepDateTime)
        {
            //Log exception into database.

            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("InsertExceptionLog", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ExceptionClassName", SqlDbType.VarChar).Value = pageNameOrClassName;
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar).Value = ExMessage;
                command.Parameters.Add("@ExceptionStackTrace", SqlDbType.VarChar).Value = ExStackTrace;
                command.Parameters.Add("@ExceptionDateTime", SqlDbType.VarChar).Value = ExcepDateTime;
                command.Parameters.Add("@ExceptionMethodName", SqlDbType.VarChar).Value = MethodName;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "Payment Processor";
                command.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (SqlException)
            {
                //Ignore exception

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        private static string GetNewInvoiceNumber()
        {
            try
            {
                var InvoiceNumber = objDBContext.pr_GetNewInvoiceNumber();
                string WFTInvoiceNumber = Convert.ToString(InvoiceNumber.First());
                return WFTInvoiceNumber;
            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "GetNewInvoiceNumber", ex.Message, ex.StackTrace, DateTime.Now);
                return "";
            }
        }

        private static void LogExecutionComment(string Description, bool LogDate)
        {
            string tmpDescription = string.Empty;

            if (LogDate)
                tmpDescription = DateTime.Now.ToString() + " : " + Description;
            else
                tmpDescription = Description;

            Console.WriteLine(tmpDescription);

            LogFileData += tmpDescription + Environment.NewLine;
        }

        private static void LogTransactionDatatoFile(string LogDetails)
        {
            string fileName = String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".Log";
            string Folder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            bool isExists = System.IO.Directory.Exists(Folder);

            if (!isExists)
                System.IO.Directory.CreateDirectory(Folder);

            string FullFilePath = Path.Combine(Folder, fileName);

            TextWriter txtWr = new StreamWriter(FullFilePath, true);
            txtWr.Write(LogDetails);
            txtWr.Close();

        }

        public static void SendEmail(string messageBody, string subject, string ToMail, bool sendInBCC, bool IsHtml)
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
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "SendEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }

        }

        public static void SendAdminNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "ADMIN_EMAIL").SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }

        public static DoReferenceTransactionResponseType DoReferenceTransactionAPIOperation(string strTransID, string AmountToBill)
        {
            DoReferenceTransactionResponseType responseDoReferenceTransactionResponseType = new DoReferenceTransactionResponseType();

            try
            {
                // Create the DoReferenceTransactionReq object
                DoReferenceTransactionReq doReferenceTransaction = new DoReferenceTransactionReq();

                // Information about the payment.
                PaymentDetailsType paymentDetails = new PaymentDetailsType();

                // The total cost of the transaction to the buyer. If shipping cost and
                // tax charges are known, include them in this value. If not, this value
                // should be the current subtotal of the order.

                // If the transaction includes one or more one-time purchases, this field must be equal to
                // the sum of the purchases. Set this field to 0 if the transaction does
                // not include a one-time purchase such as when you set up a billing
                // agreement for a recurring payment that is not immediately charged.
                // When the field is set to 0, purchase-specific fields are ignored
                //
                // * `Currency ID` - You must set the currencyID attribute to one of the
                // 3-character currency codes for any of the supported PayPal
                // currencies.
                // * `Amount`
                BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, AmountToBill);
                paymentDetails.OrderTotal = orderTotal;

                // IPN URL
                // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
                // * The transaction related IPN variables will be received on the call back URL specified in the request       
                // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
                // * PayPal would continuously resend IPN if a wrong IPN is sent        
                paymentDetails.NotifyURL = "http://IPNhost";

                // `DoReferenceTransactionRequestDetails` takes mandatory params:
                //
                // * `Reference Id` - A transaction ID from a previous purchase, such as a
                // credit card charge using the DoDirectPayment API, or a billing
                // agreement ID.
                // * `Payment Action Code` - How you want to obtain payment. It is one of
                // the following values:
                // * Authorization
                // * Sale
                // * Order
                // * None
                // * `Payment Details`
                DoReferenceTransactionRequestDetailsType doReferenceTransactionRequestDetails
                    = new DoReferenceTransactionRequestDetailsType(strTransID, PaymentActionCodeType.SALE, paymentDetails);
                DoReferenceTransactionRequestType doReferenceTransactionRequest = new DoReferenceTransactionRequestType(doReferenceTransactionRequestDetails);
                doReferenceTransaction.DoReferenceTransactionRequest = doReferenceTransactionRequest;

                // Create the service wrapper object to make the API call
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

                // # API call
                // Invoke the DoReferenceTransaction method in service wrapper object
                responseDoReferenceTransactionResponseType = service.DoReferenceTransaction(doReferenceTransaction);

                if (responseDoReferenceTransactionResponseType != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "DoReferenceTransaction API Operation - ";
                    acknowledgement += responseDoReferenceTransactionResponseType.Ack.ToString();
                }
            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "DoReferenceTransactionAPIOperation", ex.Message, ex.StackTrace, DateTime.Now);
                Console.WriteLine("Error Message : " + ex.Message);
            }
            return responseDoReferenceTransactionResponseType;
        }
    }
}
















