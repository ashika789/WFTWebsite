using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using WFTCloud.DataAccess;

namespace WFTCloud.ResuableRoutines.SMTPManager
{
    /// <summary>
    /// Class to send emails using templates.
    /// </summary>
    public class SMTPManager
    {
        public static void SendEmail(string messageBody, string subject, string ToMail, bool sendInBCC,bool IsHtml)
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
                
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendEmail", Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }
        public static void SendAdminNotificationEmail(string messageBody, string subject, bool sendInBCC)
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
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendAdminNotificationEmail", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public static void SendSAPBasisNotificationEmail(string messageBody, string subject, bool sendInBCC)
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
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendAdminNotificationEmail", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public static void SendSupportNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Support_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                else
                {
                    mail.Bcc.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
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
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendAdminNotificationEmail", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public static void SendNotificationEmailToSupport(string messageBody, string subject, bool sendInBCC,string ToMail,string CcMail)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
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
                mail.CC.Add(CcMail);
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
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendAdminNotificationEmail", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}