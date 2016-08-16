using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
namespace WFTCloud.Customer
{
    public partial class CustomerPages : System.Web.UI.MasterPage
    {       
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                                var setting = objDBContext.WftSettings.FirstOrDefault(st=>st.SettingKey=="SITE_LOCKED" && st.SettingValue=="1");
                                if (setting == null)
                                {
                                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];

                                    if (cookie != null && Request.QueryString["userid"].IsValid())
                                    {
                                        UserMembershipID = Request.QueryString["userid"];
                                        string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                                        Guid iD = new Guid(UserMembershipID);
                                        var user = objDBContext.UserProfiles.FirstOrDefault(a => a.RecordStatus == DBKeys.RecordStatus_Active && a.UserMembershipID == iD);
                                        var Admin = objDBContext.UserProfiles.FirstOrDefault(a => a.EmailID == UserName && a.RecordStatus == DBKeys.RecordStatus_Active);
                                        if (user != null)
                                        {
                                            MembershipUser MSU = Membership.GetUser(user.EmailID);
                                            if (MSU != null)
                                            {

                                                if (user.UserRole == DBKeys.Role_BusinessUser || user.UserRole == DBKeys.Role_EnterpriseUser || user.UserRole == DBKeys.Role_PersonalUser || user.UserRole == DBKeys.Role_Administrator || user.UserRole == DBKeys.Role_SuperAdministrator)
                                                {
                                                    if (MSU.IsApproved)
                                                    {
                                                        if (user.TempPasswordProvided == true && !this.Page.ToString().Contains("_changepassword_aspx"))
                                                        {
                                                            Response.Redirect("/Customer/ChangePassword.aspx?userid=" + iD.ToString(), false);
                                                        }
                                                    }
                                                    else if (!MSU.IsApproved)
                                                    {
                                                        Response.Redirect("/Login.aspx", false);
                                                    }
                                                    else if ((Admin.UserRole == DBKeys.Role_Administrator || Admin.UserRole == DBKeys.Role_Administrator) && Admin != null)
                                                    {
                                                        bool permission = true;
                                                        permission = (objDBContext.AdminPageAccesses.Where(a => a.AdminUserID == Admin.UserProfileID && a.WebPageID == 1)).Count() > 0 ? true : false;
                                                        if (permission == false)
                                                        {
                                                            Response.Redirect("/Admin/RestrictedPageAccess.aspx", false);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Response.Redirect("/Login.aspx", false);
                                                }
                                            }
                                            else
                                            {
                                                Response.Redirect("/Login.aspx", false);
                                            }
                                        }
                                        else
                                        {
                                            Response.Redirect("/Login.aspx", false);
                                        }
                                    }
                                    else
                                    {
                                        Response.Redirect("/Login.aspx", false);
                                    }
                                }
                                else
                                {
                                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                                    if (cookie != null)
                                    {

                                        Session.Clear();
                                        FormsAuthentication.SignOut();
                                        HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                                    }
                                    Response.Redirect("/SiteDownForMaintenance.html", false);
                                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion
    }
}