using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Customer.PageControls
{
    public partial class LeftNavigation : System.Web.UI.UserControl
    {
        #region Global Variables and Properties
        public string UserMembershipID; 
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserMembershipID = Request.QueryString["userid"];
                string pageName = this.Page.ToString();
                liuserProfiles.Attributes.Add("class", pageName.Contains("_userprofiles_aspx") == true ? "active" : "");
                liWftCloudPackages.Attributes.Add("class", (pageName.Contains("_cloudpackages_aspx") || pageName.Contains("_userdashboard_aspx") || pageName.Contains("_success_aspx") || pageName.Contains("_failed_aspx")) == true ? "active" : "");
                liCRMRequest.Attributes.Add("class", pageName.Contains("_crmrequests_aspx") == true ? "active" : "");
                liUserOrderDetails.Attributes.Add("class", pageName.Contains("_userorderdetails_aspx") == true ? "active" : "");
                liDownloads.Attributes.Add("class", pageName.Contains("_downloads_aspx") == true ? "active" : "");
                liManageCRM.Attributes.Add("class", pageName.Contains("_managecrmrequests_aspx") == true ? "active" : "");
                string UserType = "";
                string UserName = "";
                HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.EmailID == UserName);
                    var techmails = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "TECH_EMAIL");
                    if (techmails != null)
                    {
                        UserType = techmails.SettingValue.ToLower().Contains(UserName.ToLower()) ? "Technical" : "";
                    }
                    var sales = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "SALES_EMAIL");
                    if (sales != null)
                    {
                        UserType = sales.SettingValue.ToLower().Contains(UserName.ToLower()) ? "Sales" : UserType;
                    }
                    if (UserType != "")
                        liManageCRM.Visible = true;
                    else
                        liManageCRM.Visible = false;
                }
                //UserMembershipID = Request.QueryString["userid"];
                //if (UserMembershipID != null && UserMembershipID != "")
                //{
                //    Guid ID = new Guid(UserMembershipID);
                //    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                //    lblUserName.Text = "Welcome, " + user.LastName + " " + user.FirstName;
                //}
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

    }
}