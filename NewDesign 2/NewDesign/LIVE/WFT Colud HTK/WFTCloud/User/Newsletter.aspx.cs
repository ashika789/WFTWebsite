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
using System.Net.Mail;

namespace WFTCloud.User
{
    public partial class Newsletter : System.Web.UI.Page
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
            LoadNewsLetterView();
        }

        protected void btnSubmitReport_Click(object sender, EventArgs e)
        {
            try
            {
                int NewsLetterID = Convert.ToInt32(Request.QueryString["NewsLetterSignupID"]);

               
                string FirstName = txtFirstName.Text.Trim();
                 string LastName = txtLastName.Text.Trim();

                 var NewsLetterReq = objDBContext.pr_UpdateUserNewsLetterInfo(NewsLetterID, FirstName, LastName);

                 divRegisterSuccess.Visible = true;
                 txtFirstName.Text = txtLastName.Text = "";
                
               
               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnViewReport_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void SendSAPCalcEmailToTechnicalTeam(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Admin_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail + ",rkumar@wftus.com");
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
                ReusableRoutines.LogException("SAPCalcEmail", "SendSAPCalcEmailToTechnicalTeam", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
        private  void ClearAllControls()
        {
            try
            {
                //txtFirstName.Text = txtLastName.Text = txtCompanyName.Text =txtEmailID.Text =txtTelephone.Text =txtSAPModulesOthers.Text =txtLandscapeOthers.Text = "";
                //chkSAPModules.ClearSelection();
                //chkSAPLandscape.ClearSelection();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void LoadNewsLetterView()
        {
            try
            {
                int NewsLetterID = Convert.ToInt32(Request.QueryString["NewsLetterSignupID"]);
                if (NewsLetterID != null)
                {
                    var NewsLetter = objDBContext.NewsLetterSignUps.FirstOrDefault(q => q.NewsLetterSignupID == NewsLetterID);
                    txtEmailAddress.Text = NewsLetter.EmailID;
                    
                }
                else
                {
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

    }
}