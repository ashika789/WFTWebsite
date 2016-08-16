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
using System.Text;
using System.Security.Cryptography;

namespace WFTCloud.User
{
    public partial class SSO : System.Web.UI.Page
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
            string url = GetSsoUrl("http://support.wftcloud.com/", //including trailing slash
"7524c3f0231cd08d1f619927f6d1825d", "rkumar@wftus.com", "Apple!123");
            Response.Redirect(url);
        }

        string GetSsoUrl(string baseUrl, string secert, string name, string email)
        {
            return String.Format("{0}login/sso/?name={1}&email={2}&hash={3}", baseUrl, Server.UrlEncode(name),
            Server.UrlEncode(email), GetHash(secert, name, email));
        }
        static string GetHash(string secert, string name, string email)
        {
            string input = name + email + secert;

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                string hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
                sb.Append((hexValue.Length == 1 ? "0" : "") + hexValue);
            }
            return sb.ToString();
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
        
    }
}