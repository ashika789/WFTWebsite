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
using System.Security.Principal;
using System.Configuration;
using System.Web.Security;
using System.IO;

namespace WFTCloud.Admin
{
    public partial class newslettermanagement : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region Page Events
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
                         if (!IsPostBack)
                         {
                            
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

        #region Control Events

      

      
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                    string UserContent = txtContent.Text;
                    string UserHeader = txtHeader.Text;
                    string UserSubject = txtSubject.Text;
                    string UserMessage = txtMessage.Text;
                    string CustomerEmails = string.Empty;
                    var objEmails = objDBContext.NewsLetterSignUps.Where(A => A.ActiveStatus == 1);

                    foreach (var res in objEmails)
                    {
                        CustomerEmails += (res.EmailID.ToString() + ",");
                    }
                    CustomerEmails = CustomerEmails.Remove(CustomerEmails.LastIndexOf(","));

                    string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewsLetterTemplate"]));
                    CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string CustomerContent = CustomerEmailContent.Replace("++UserHeaderContent++", UserContent).Replace("++MainHeader++", UserHeader).Replace("++UserMainContent++", UserMessage);


                    SMTPManager.SendEmail(CustomerContent, UserSubject, CustomerEmails, true, true);
               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

       
        


      
        #endregion

      
       

       
        
    }
}