﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.User
{
    public partial class AboutUs : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //string selectedLanguage = Convert.ToString(Session["Language"]);
            string selectedLanguage = "English";
            if (Request.Cookies["LanguageCookie"] != null)
            {
                selectedLanguage = Request.Cookies["LanguageCookie"].Value;
            }
            var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "aboutus.aspx" && c.Language == selectedLanguage);
            if (Content != null)
            {
                Literal1.Text = Content.HTMLContent;
            }
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "aboutus.aspx" && c.Language == "English");
                Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;
        }
    }
}