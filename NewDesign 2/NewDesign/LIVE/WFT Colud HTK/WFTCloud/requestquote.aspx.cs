using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud 
{
    public partial class requestquote : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion 

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Request.IsLocal && Request.IsSecureConnection)
            //{
            //    string redirectUrl = Request.Url.ToString().Replace("https:", "http:");
            //    Response.Redirect(redirectUrl, false);
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}
                if (!IsPostBack)
                {
                    
                    try
                    {
                        clearCookie();
                       
                    }
                    catch (Exception Ex)
                    {
                        ReusableRoutines.LogException(this.GetType().ToString(), "Page_Load", Ex.Message, Ex.StackTrace, DateTime.Now);
                    }
                }
        }

        private void clearCookie()
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {

                Session.Clear();
                FormsAuthentication.SignOut();
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            }
        }

        #endregion

        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {


                string UserName = txtName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtContactNumber.Text;
                string Location = "City : " + txtCity.Text + ", State : " + txtState.Text + ", Country : " + txtCountry.Text;
                string Company = txtCompany.Text;
                string Decscription = txtDecscription.Text;

                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewRequestQuote"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++GeographicLocation++", Location).Replace("++UserCompanyName++", Company).Replace("++Message++", Decscription);
                SMTPManager.SendAdminNotificationEmail(AdminContent, "New Quote request from " + UserName, false);
                txtName.Text = txtName.Text = txtContactNumber.Text = txtEmailID.Text = txtCity.Text = txtState.Text = txtCountry.Text = txtCompany.Text = txtDecscription.Text = string.Empty;
                divRegisterSuccess.Visible = true;

                
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegisterCode_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        
    }
}