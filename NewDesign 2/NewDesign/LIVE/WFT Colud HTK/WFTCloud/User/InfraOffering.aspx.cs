using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.User
{
    public partial class InfraOffering : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string selectedLanguage = "English";
            if (Request.Cookies["LanguageCookie"] != null)
            {
                selectedLanguage = Request.Cookies["LanguageCookie"].Value;
            }
            var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "InfraOffering.aspx" && c.Language == selectedLanguage);
            if (Content != null)
            {
                //Literal1.Text = Content.HTMLContent;
            }
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "InfraOffering.aspx" && c.Language == "English");
                //Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;
        }

        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                string UserCompanyName = txtCompanyName.Text;
                string UserName = txtFirstName.Text + " " + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtContactNumber.Text;
                string Location = ddlCountry.SelectedValue;
                string Message = txtMessage.Text;
                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["InfraOfferingRegister"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++GeographicLocation++", Location).Replace("++UserCompanyName++", UserCompanyName).Replace("++Message++", Message);
                SMTPManager.SendAdminNotificationEmail(AdminContent, "New Infrastructure Migration Query from " + UserName, false);
                txtMessage.Text = txtCompanyName.Text = txtContactNumber.Text = txtEmailID.Text = txtFirstName.Text = txtLastName.Text = string.Empty;
                divRegisterSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegisterCode_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}