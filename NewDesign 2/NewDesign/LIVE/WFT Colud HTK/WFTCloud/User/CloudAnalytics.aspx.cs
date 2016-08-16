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
    public partial class CloudAnalytics : System.Web.UI.Page
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

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                QuoteRegister objQuoteRegister = new QuoteRegister();
                string SAPModules = string.Empty;
                string SAPLandscape = string.Empty;
                string UserCompanyName = txtCompanyName.Text;
                string UserName = txtFirstName.Text + " " + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtTelephone.Text != "" ? txtTelephone.Text : "-" ;
                string Description = txtDescription.Text != "" ? txtDescription.Text : "-";
                foreach (ListItem item in chkSAPModules.Items)
                {
                    if (item.Selected)
                    {
                        SAPModules += (item.Value + ",");
                    }
                }

                if (txtSAPModulesOthers.Text != "")
                {
                    SAPModules += txtSAPModulesOthers.Text;
                }
                else if (SAPModules != "")
                {
                    SAPModules = SAPModules.Remove(SAPModules.LastIndexOf(","));
                }
                else
                {
                    SAPModules = "";
                }
               
                foreach (ListItem Landscapeitem in chkSAPLandscape.Items)
                {
                    if (Landscapeitem.Selected)
                    {
                        SAPLandscape += (Landscapeitem.Value + ",");
                    }
                }
                if (txtLandscapeOthers.Text != "")
                {
                    SAPLandscape += txtLandscapeOthers.Text;
                }
                else if (SAPLandscape != "")
                {
                    SAPLandscape = SAPLandscape.Remove(SAPLandscape.LastIndexOf(","));
                }
                else
                {
                    SAPLandscape = "";
                }


                objQuoteRegister.FirstName = txtFirstName.Text;
                objQuoteRegister.LastName = txtLastName.Text;
                objQuoteRegister.PhoneNumber = txtTelephone.Text;
                objQuoteRegister.Email = txtEmailID.Text;
                objQuoteRegister.CompanyName = txtCompanyName.Text;
                objQuoteRegister.SAPModules = SAPModules.ToString();
                objQuoteRegister.Landscapes = SAPLandscape.ToString();
                objQuoteRegister.Descriptions = Description.ToString();
                objDBContext.QuoteRegisters.AddObject(objQuoteRegister);
              //  objDBContext.AddObject("QuoteRegister",objQuoteRegister);
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.QuoteRegisters);

                string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudAnalyticsToCustomer"]));
                CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);

                CustomerEmailContent = CustomerEmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++SAPModules++", SAPModules).Replace("++UserCompanyName++", UserCompanyName).Replace("++Landscape++", SAPLandscape).Replace("++Description++", Description);
                SMTPManager.SendEmail(CustomerEmailContent, "SAP Landscape Information Received - WFT Cloud", UserEmailID, false, true);

                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudAnalytics"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++SAPModules++", SAPModules).Replace("++UserCompanyName++", UserCompanyName).Replace("++Landscape++", SAPLandscape).Replace("++Description++", Description);
                SendSAPCalcEmailToTechnicalTeam(AdminContent, "SAP to Cloud migration request from " + UserName, false);
                divRegisterSuccess.Visible = true;


                string CloudAnalyticsQuote = System.Configuration.ConfigurationManager.AppSettings["CloudAnalyticsQuote"];
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=SampleWFTQuote.pdf");
                Response.TransmitFile(Server.MapPath(CloudAnalyticsQuote));
              
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "clickLink", "ClickLink();", true);

                Response.AddHeader("Refresh", "2;URL=http://wftcloud.com/user/CloudCalculator.aspx?FirstName=" + txtFirstName.Text + "&LastName=" + txtLastName.Text + "&CompanyName=" + txtCompanyName.Text + "&EmailID=" + txtEmailID.Text + "&Telephone=" + txtTelephone.Text);
                //Response.End();

                
               
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/user/CloudCalculator.aspx?FirstName=" + txtFirstName.Text + "&LastName=" + txtLastName.Text + "&CompanyName=" + txtCompanyName.Text + "&EmailID=" + txtEmailID.Text + "&Telephone=" + txtTelephone.Text + "', '_blank');", true);

               
               
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
                txtFirstName.Text = txtLastName.Text = txtCompanyName.Text =txtEmailID.Text =txtTelephone.Text =txtSAPModulesOthers.Text =txtLandscapeOthers.Text = "";
                chkSAPModules.ClearSelection();
                chkSAPLandscape.ClearSelection();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}