using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class ContactUs : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = string.Empty;
            string selectedLanguage = "English";
            if (Request.Cookies["LanguageCookie"] != null)
            {
                selectedLanguage = Request.Cookies["LanguageCookie"].Value;
            }
            var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "contactus.aspx" && c.Language == selectedLanguage);
            if (Content != null)
            {
                Literal1.Text = Content.HTMLContent;
            }
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "contactus.aspx" && c.Language == "English");
                Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

                ContactRequest cr = new ContactRequest();
                cr.ContactName = txtContactName.Value;
                cr.ContactEmailAddress = txtEmailAddress.Value;
                cr.DateEntered = DateTime.Now;
                cr.Message = txtMessage.Value;
                cr.Subject = txtSubject.Value;

                objDBContext.ContactRequests.AddObject(cr);
                objDBContext.SaveChanges();

                lblSuccess.Text = "Thank you for contacting us. Someone will be in touch with you shortly.";

                txtContactName.Value = txtEmailAddress.Value = txtMessage.Value = txtSubject.Value = string.Empty;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}