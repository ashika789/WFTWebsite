using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin.PageControls
{
    public partial class LeftNav : System.Web.UI.UserControl
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                var User = objDBContext.UserProfiles.FirstOrDefault(a => a.EmailID == UserName && a.RecordStatus == DBKeys.RecordStatus_Active);
                if (User != null)
                {
                    string pageName = this.Page.ToString();
                    liUserManagement.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 1)).Count() > 0 ? true : false;
                    liUserManagement.Attributes.Add("class", pageName.Contains("_userlist_aspx") == true || pageName.Contains("_userservices_aspx") == true ? "active" : "");

                    liAdminList.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 2)).Count() > 0 ? true : false;
                    liAdminList.Attributes.Add("class", pageName.Contains("_administratorslist_aspx") == true || pageName.Contains("_usersubscriptiondetails_aspx") == true || pageName.Contains("_usersservicehistory_aspx") == true || pageName.Contains("_usersubscriptionhistory_aspx") == true || pageName.Contains("_userfullhistory_aspx") == true || pageName.Contains("_userpaymentinformation_aspx") == true || pageName.Contains("_newslettermanagement_aspx") == true || pageName.Contains("_admindashboard_aspx") == true ? "active" : "");

                    liServiceList.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 3)).Count() > 0 ? true : false;
                    liServiceList.Attributes.Add("class", (pageName.Contains("_categorieslist_aspx") == true || pageName.Contains("_wftservices_aspx") == true || pageName.Contains("_serviceprovisioningdetails_aspx") == true || pageName.Contains("_customserviceprovisioningdetails_aspx") == true || pageName.Contains("_cloudresourcemanagement_aspx") == true) ? "active" : "");

                    liCoupon.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 4)).Count() > 0 ? true : false;
                    liCoupon.Attributes.Add("class", pageName.Contains("_couponmanagement_aspx") == true ? "active" : "");

                    liContentManagment.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 5)).Count() > 0 ? true : false;
                    liContentManagment.Attributes.Add("class", (pageName.Contains("_contentmanagementscreen_aspx") == true || pageName.Contains("_manageindexdata_aspx") == true || pageName.Contains("_managefaq_aspx") == true || pageName.Contains("_managebanners_aspx") || pageName.Contains("_managetestimonials_aspx") == true || pageName.Contains("managepressrelease_aspx") == true) ? "active" : "");

                    liFinancialAccounting.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 6)).Count() > 0 ? true : false;
                    liFinancialAccounting.Attributes.Add("class", (pageName.Contains("_processedpayments_aspx") == true || pageName.Contains("_automatedpayments_aspx") == true || pageName.Contains("_paymenttransactionupdate_aspx") == true) ? "active" : "");

                    liTrainningAndVisitors.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 7)).Count() > 0 ? true : false;
                    liTrainningAndVisitors.Attributes.Add("class", (pageName.Contains("_managetrainingdetails_aspx") == true || pageName.Contains("_managecourses_aspx") == true || pageName.Contains("_managevisitors_aspx") == true) ? "active" : "");

                    liGenerateReports.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 34)).Count() > 0 ? true : false;
                    liGenerateReports.Attributes.Add("class", pageName.Contains("_wftreports_aspx") == true ? "active" : "");

                    liCloudGenerateReports.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 34)).Count() > 0 ? true : false;
                    liCloudGenerateReports.Attributes.Add("class", pageName.Contains("_cloudreports_aspx") == true ? "active" : "");

                    liSettings.Visible = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 35)).Count() > 0 ? true : false;
                    liSettings.Attributes.Add("class", pageName.Contains("_settings_aspx") == true ? "active" : "");
                    liManageCRMRequests.Attributes.Add("class", pageName.Contains("_managecrmrequests_aspx") == true ? "active" : "");
                }

                //if (cookie != null)
                //{
                //    var Name = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == UserName);
                //    lblUserName.Text = "Welcome, " + Name.LastName + " " + Name.FirstName;
                //}
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}