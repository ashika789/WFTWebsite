using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class AdminPages : System.Web.UI.MasterPage
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var setting = objDBContext.WftSettings.FirstOrDefault(st => st.SettingKey == "SITE_LOCKED" && st.SettingValue == "1");
                //if (setting == null)
                //{
                                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                                    if (cookie != null)
                                    {
                                        string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                                        var User = objDBContext.UserProfiles.FirstOrDefault(a => a.EmailID == UserName && a.RecordStatus == DBKeys.RecordStatus_Active);
                                        MembershipUser MSU = Membership.GetUser(User.EmailID);
                                        //string Rolename = Roles.GetRolesForUser(UserName).First();
                                        if (User.UserRole == DBKeys.Role_Administrator || User.UserRole == DBKeys.Role_SuperAdministrator)
                                        {
                                            if (!MSU.IsApproved)
                                            {
                                                Response.Redirect("/LoginPage.aspx", false);
                                            }
                                            if (User.TempPasswordProvided == true && !this.Page.ToString().Contains("_changepassword_aspx"))
                                            {
                                                Response.Redirect("/Admin/ChangePassword.aspx?userid=" + User.UserMembershipID, false);
                                            }
                                            string pageName = this.Page.ToString();
                                            bool permission = true;
                                            if (pageName.Contains("_userlist_aspx") || pageName.Contains("_userservices_aspx"))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 1)).Count() > 0 ? true : false;
                                            }
                                            else if (pageName.Contains("_administratorslist_aspx"))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 2)).Count() > 0 ? true : false;
                                            }
                                            else if ((pageName.Contains("_categorieslist_aspx") == true || pageName.Contains("_wftservices_aspx") == true || pageName.Contains("_serviceprovisioningdetails_aspx") == true || pageName.Contains("_cloudresourcemanagement_aspx") == true))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 3)).Count() > 0 ? true : false;
                                            }
                                            else if (pageName.Contains("_couponmanagement_aspx"))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 4)).Count() > 0 ? true : false;
                                            }
                                            else if ((pageName.Contains("_contentmanagementscreen_aspx") == true || pageName.Contains("_manageindexdata_aspx") == true || pageName.Contains("_managefaq_aspx") == true || pageName.Contains("_managebanners_aspx") || pageName.Contains("_managetestimonials_aspx") == true))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 5)).Count() > 0 ? true : false;
                                            }
                                            else if ((pageName.Contains("_processedpayments_aspx") == true || pageName.Contains("_automatedpayments_aspx") == true))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 6)).Count() > 0 ? true : false;
                                            }
                                            else if ((pageName.Contains("_managetrainingdetails_aspx") == true || pageName.Contains("_managecourses_aspx") == true || pageName.Contains("_managevisitors_aspx") == true))
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 7)).Count() > 0 ? true : false;
                                            }
                                            else if (pageName.Contains("_wftreports_aspx") == true)
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 34)).Count() > 0 ? true : false;
                                            }
                                            else if (pageName.Contains("_settings_aspx") == true)
                                            {
                                                permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == User.UserProfileID && a.WebPageID == 35)).Count() > 0 ? true : false;
                                            }

                                            if (permission == false && pageName.Contains("_restrictedpageaccess_aspx") == false)
                                            {
                                                Response.Redirect("/Admin/RestrictedPageAccess.aspx", false);
                                            }
                                        }
                                        else
                                        {
                                            Response.Redirect("/LoginPage.aspx", false);
                                        }
                                    }
                                    else
                                    {
                                        Response.Redirect("/LoginPage.aspx", false);
                                    }
                //}
                //else
                //{
                //    Response.Redirect("/SiteDownForMaintenance.aspx", false);
                //}
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}