using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using AuthorizeNet;
using WFTCloud.DataAccess;
using WFTCloud.AuthSOAP;
using System.Text.RegularExpressions;
using PayPal;
using System.Web.Security;
namespace WFTCloud.ResuableRoutines
{
    public class ReusableRoutines
    {

        public static void LogException(string pageNameOrClassName, string MethodName, string ExMessage, string ExStackTrace, DateTime ExcepDateTime)
        {
            //Log exception into database.
            
            SqlConnection sqlConnection = new SqlConnection();
            MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
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
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = MSU != null?MSU.UserName:"User Not Logged";
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

        public static string AuthorizationCreditCardPayment(string CreditCardNumber, string ExpMonthYear, decimal Amount, string Description, int UserOrderID, int UserProfileID)
        {
            // ConfigurationManager.AppSettings["AuNetAPILogin"]
            // ConfigurationManager.AppSettings["AuNetTransactionKey"]
            cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
            var request = new AuthorizationRequest(CreditCardNumber, ExpMonthYear, Amount, Description);

            //step 2 - create the gateway, sending in your credentials
            var gate = new Gateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"]);

            //step 3 - make some money
            var response = gate.Send(request);

            UserPaymentTransaction NewUPT = new UserPaymentTransaction();
            NewUPT.Amount = response.Amount;
            NewUPT.Approved = response.Approved;
            NewUPT.AuthAuthorizationCode = response.AuthorizationCode;
            NewUPT.AuthCardNumber = response.CardNumber;
            NewUPT.AuthMessage = response.Message;
            NewUPT.AuthResponseCode = response.ResponseCode;
            NewUPT.AuthTransactionID = response.TransactionID;
            NewUPT.PaymentDateTime = DateTime.Now;
            NewUPT.UserProfileID = UserProfileID;
            NewUPT.InvoiceNumber = "WFT" + UserOrderID.ToString();
            objDBContext.UserPaymentTransactions.AddObject(NewUPT);
            objDBContext.SaveChanges();

            //****************** ResponseCode ************************//
            //   1 This transaction has been approved. 
            //   2 This transaction has been declined. 
            //   3 There has been an error processing this transaction. 
            //   4 This transaction is being held for review
            return response.ResponseCode+":"+response.Message;
        }

        public static string EnCryptCreditCardNumber(string MembershipID,string CreditCardNo)
        {
            MembershipID = MembershipID.ToUpper().Replace("-", "");
            string Result =   MembershipID[0].ToString() + "X" 
                            + MembershipID[2].ToString() + "X" 
                            + MembershipID[4].ToString() + "X" 
                            + MembershipID[6].ToString() + "X" 
                            + MembershipID[8].ToString() + "X"
                            + MembershipID[10].ToString() + "X" 
                            + MembershipID[12].ToString() + "X" 
                            + MembershipID[14].ToString() + "X" 
                            + MembershipID[16].ToString() + "X" 
                            + MembershipID[18].ToString() + "X" 
                            + MembershipID[20].ToString() + "X" 
                            + MembershipID[22].ToString() + "X"
                            + MembershipID[24].ToString() + CreditCardNo[12].ToString()
                            + MembershipID[26].ToString() + CreditCardNo[13].ToString()
                            + MembershipID[28].ToString() + CreditCardNo[14].ToString()
                            + MembershipID[30].ToString() + CreditCardNo[15].ToString();
            return Result;
        }
        public static string DeCryptCreditCardNumber(string CreditCardNo)
        {
            string Result =  CreditCardNo[1].ToString() 
                            + CreditCardNo[3].ToString()
                            + CreditCardNo[5].ToString()
                            + CreditCardNo[7].ToString()
                            + CreditCardNo[9].ToString()
                            + CreditCardNo[11].ToString()
                            + CreditCardNo[13].ToString()
                            + CreditCardNo[15].ToString()
                            + CreditCardNo[17].ToString()
                            + CreditCardNo[19].ToString()
                            + CreditCardNo[21].ToString()
                            + CreditCardNo[23].ToString()
                            + CreditCardNo[25].ToString()
                            + CreditCardNo[27].ToString()
                            + CreditCardNo[29].ToString()
                            + CreditCardNo[31].ToString();
            return Result;
        }

        public static string GetCreditCardType(string CreditCardNumber)
        {
            Regex regVisa = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regMaster = new Regex("^5[1-5][0-9]{14}$");
            Regex regExpress = new Regex("^3[47][0-9]{13}$");
            Regex regDiners = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            Regex regDiscover = new Regex("^6(?:011|5[0-9]{2})[0-9]{12}$");
            Regex regJSB = new Regex("^(?:2131|1800|35\\d{3})\\d{11}$");


            if (regVisa.IsMatch(CreditCardNumber))
                return "Visa";
            if (regMaster.IsMatch(CreditCardNumber))
                return "Master Card";
            if (regExpress.IsMatch(CreditCardNumber))
                return "American Express";
            if (regDiners.IsMatch(CreditCardNumber))
                return "Diners Club";
            if (regDiscover.IsMatch(CreditCardNumber))
                return "Discover";
            if (regJSB.IsMatch(CreditCardNumber))
                return "JSB";
            return "Invalid Credit Card";
        }

        public static string AuthorizeCreateCustomerProfile(string Email, string ProfileName, string UserFullName)
        {
            AuthSOAP.ServiceSoapClient AuthWb = new AuthSOAP.ServiceSoapClient();
            MerchantAuthenticationType mt = new MerchantAuthenticationType();
            mt.name = ConfigurationManager.AppSettings["AuNetAPILogin"];
            mt.transactionKey = ConfigurationManager.AppSettings["AuNetTransactionKey"];
            CreateCustomerProfileResponseType Cpres = new CreateCustomerProfileResponseType();
            try{
            CustomerProfileType Cpt  = new CustomerProfileType();
            Cpt.merchantCustomerId =ProfileName;
            Cpt.email =Email;
            Cpt.description = UserFullName.ToUpper();

            ValidationModeEnum VME = new ValidationModeEnum();
            
            Cpres = AuthWb.CreateCustomerProfile(mt, Cpt, VME);

            if (Cpres.resultCode.ToString() == "Ok")
            {
                return Cpres.customerProfileId.ToString();
            }
            else
            {
                return "Failed";
            }

            //AuthWb.
            //bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            //AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
            //AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            //AuthorizeNet.Customer custProfile = new AuthorizeNet.Customer();
            //try
            //{
            //    custProfile = objGW.CreateCustomer(Email, ProfileName);
            //    return custProfile.ProfileID;
            }
            catch (Exception Ex)
            {
                LogException("ReusableRoutines", System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                DeleteCustomerProfileResponseType dcprt =  AuthWb.DeleteCustomerProfile(mt, Cpres.customerProfileId);
                return "Failed";
            }
        }

        public static string AuthorizeAddCreditCardProfile(string ProfileID, string CardNo, int Month, int Year, string CVV,AuthorizeNet.Address BillingAddress)
        {
            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;

            AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            var paymentProfile = objGW.AddCreditCard(ProfileID, CardNo, Month, Year, CVV, BillingAddress);
            return paymentProfile;
        }

        public static string AuthorizeAddBillingAddress(string ProfileID, AuthorizeNet.Address BillingAddress)
        {
            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;

            AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            AuthorizeNet.Customer customer = objGW.GetCustomer(ProfileID);
            customer.BillingAddress = BillingAddress;
            objGW.UpdateCustomer(customer);
            return customer.BillingAddress.ID;
        }

        public static string AuthorizeBillClient(string ProfileID, string PaymentProfileID, decimal BillAmount, string InvoiceNumber, string OrderDescription, int UserProfileID)
        {
            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
            
            cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
            AuthorizeNet.CustomerGateway objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            var customer = objGW.GetCustomer(ProfileID);
            AuthorizeNet.Order objOrder = new AuthorizeNet.Order(ProfileID, PaymentProfileID, customer.PaymentProfiles.FirstOrDefault(s=>s.ProfileID == PaymentProfileID).BillingAddress.ID);
            objOrder.InvoiceNumber = InvoiceNumber;
            objOrder.Amount = BillAmount;
            objOrder.Description = OrderDescription;
            var response = (AuthorizeNet.GatewayResponse)objGW.AuthorizeAndCapture(objOrder);
            UserPaymentTransaction NewUPT = new UserPaymentTransaction();
            NewUPT.Amount = response.Amount;
            NewUPT.Approved = response.Approved;
            NewUPT.AuthAuthorizationCode = response.AuthorizationCode;
            NewUPT.AuthCardNumber = customer.PaymentProfiles.FirstOrDefault(s => s.ProfileID == PaymentProfileID).CardNumber;
            NewUPT.AuthMessage = response.Message;
            NewUPT.AuthResponseCode = response.ResponseCode;
            NewUPT.AuthTransactionID = response.TransactionID;
            NewUPT.PaymentDateTime = DateTime.Now;
            NewUPT.PaymentMethod = "Authorize.net";
            NewUPT.UserProfileID = UserProfileID;
            NewUPT.InvoiceNumber = InvoiceNumber;
            objDBContext.UserPaymentTransactions.AddObject(NewUPT);
            objDBContext.SaveChanges();
            return response.ResponseCode + ":" + response.Message;
        }

        public static string ARBCancelSubscription(long ARBSubscribtionID)
        {
            try
            {
                AuthSOAP.ServiceSoapClient AuthWb = new AuthSOAP.ServiceSoapClient();
                MerchantAuthenticationType mt = new MerchantAuthenticationType();
                mt.name = ConfigurationManager.AppSettings["AuNetAPILogin"];
                mt.transactionKey = ConfigurationManager.AppSettings["AuNetTransactionKey"];
               var response = AuthWb.ARBCancelSubscription(mt, ARBSubscribtionID);
                if(response.resultCode != MessageTypeEnum.Error)
                return "Success";
                else
                    return "Failed";
            }
            catch (Exception Ex)
            {
                LogException("ReusableRoutines", System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Failed";
            }

        }

        // Save paypal response from the all methods
        public static void SavePaypalResponse(PaypalRespons pr)
        {
            cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
            objDBContext.AddToPaypalResponses(pr);
            objDBContext.SaveChanges();
        }
    }
}