﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;

namespace WFTCloud.User
{
    public partial class CancelationPolicy : System.Web.UI.Page
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
           
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "TermsAndConditions.aspx" && c.Language == "English");
                Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;
        }
    }
}