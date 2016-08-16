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
    public partial class BWHANARegistration : System.Web.UI.Page
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
        protected void ddlHearAboutUs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHearAboutUs.SelectedValue == "Others")
            {
                trOthers.Visible = true;
            }
            else
            {
                trOthers.Visible = false;
            }
        }
        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                HANARegistration objHanaRegistration = new HANARegistration();
                string UserCompanyName = txtCompanyName.Text;
                string UserName = txtFirstName.Text + " " + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtContactNumber.Text;
                string Location = ddlCountry.SelectedValue;
                string SurveyAnswer = string.Empty;
                if (ddlHearAboutUs.SelectedValue == "Others")
                {
                    SurveyAnswer = txtOthers.Text;
                }
                else
                {
                   SurveyAnswer = ddlHearAboutUs.SelectedValue;
                }
                string Message = txtSubject.Text;

                objHanaRegistration.FirstName = txtFirstName.Text;
                objHanaRegistration.LastName = txtLastName.Text;
                objHanaRegistration.PhoneNumber = txtContactNumber.Text;
                objHanaRegistration.Email = txtEmailID.Text;
                objHanaRegistration.CompanyName = txtCompanyName.Text;
                objHanaRegistration.Location = ddlCountry.SelectedValue;
                if (ddlHearAboutUs.SelectedValue == "Others")
                {
                    objHanaRegistration.SurveyAnswer = txtOthers.Text;
                }
                else
                {
                    objHanaRegistration.SurveyAnswer = ddlHearAboutUs.SelectedValue;
                }
                objHanaRegistration.Subject = txtSubject.Text;
                objHanaRegistration.CreatedOn = DateTime.Now;
                //  objDBContext.AddObject("QuoteRegister",objQuoteRegister);
                objDBContext.HANARegistrations.AddObject(objHanaRegistration);
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.HANARegistrations);


                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["BWHANARegister"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++GeographicLocation++", Location).Replace("++UserCompanyName++", UserCompanyName).Replace("++SurveyAnswer++", SurveyAnswer).Replace("++Message++", Message);
                SMTPManager.SendAdminNotificationEmail(AdminContent, "New SAP BW HANA request from " + UserName, false);
                txtOthers.Text = txtCompanyName.Text = txtContactNumber.Text = txtEmailID.Text = txtFirstName.Text = txtLastName.Text = string.Empty;
                divRegisterSuccess.Visible = true;

                ClearAllControls();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegisterCode_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private  void ClearAllControls()
        {
            try
            {
                txtFirstName.Text = txtLastName.Text = txtCompanyName.Text = txtEmailID.Text = txtContactNumber.Text = txtOthers.Text = txtSubject.Text = "";
                ddlHearAboutUs.ClearSelection();
                ddlCountry.ClearSelection();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}