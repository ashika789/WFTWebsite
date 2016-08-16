using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud 
{
    public partial class ExpressCloud : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
         public string UserMembershipID ="";
        #endregion 

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Request.IsLocal && Request.IsSecureConnection)
            //{
            //    string redirectUrl = Request.Url.ToString().Replace("https:", "http:");
            //    Response.Redirect(redirectUrl, false);
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}
            string pageName1 = this.Page.ToString();
                var setting = objDBContext.WftSettings.FirstOrDefault(st=>st.SettingKey=="SITE_LOCKED" && st.SettingValue=="1");
                if (setting == null || pageName1.Contains("login_aspx"))
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (HttpContext.Current.User.Identity.IsAuthenticated && cookie != null)
                    {
                        //liregister.Visible = false;
                        MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                        UserMembershipID = MSU.ProviderUserKey.ToString();
                        Guid MuID = Guid.Parse(UserMembershipID);

                        UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                        loginUser.InnerText = string.Format("  Welcome {0}", upf.FirstName);
                        string RoleName = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name).First();
                        if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                        {
                            hrefloggedInLink.HRef = "/Admin/userlist.aspx";
                            //licart.Visible = false;
                        }
                        else
                        {
                            //licart.Visible = true;
                            hrefloggedInLink.HRef = "/Customer/CloudPackages.aspx?userid=" + MuID.ToString() + "&showview=SubscribedService";
                        }

                        lilogout.Visible = true;
                        //liSignUp.Visible = false;
                    }
                }

                   

                if (!IsPostBack)
                {
                   
                    try
                    {
                        
                        //Add items to cart.
                        AddItemToCart();
                    }
                    catch (Exception Ex)
                    {
                        ReusableRoutines.LogException(this.GetType().ToString(), "Page_Load", Ex.Message, Ex.StackTrace, DateTime.Now);
                    }
                }
        }

        private void clearCookie()
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {

                Session.Clear();
                FormsAuthentication.SignOut();
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            }
        }

        #endregion

        private void AddItemToCart()
        {
            try
            {
                if (Request.QueryString["AddToCart"].IsValid())
                {

                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (HttpContext.Current.User.Identity.IsAuthenticated && cookie != null)
                    {
                        MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                        Guid ID = Guid.Parse(MSU.ProviderUserKey.ToString());
                        var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID && a.UserRole.Contains("User"));
                        if (user != null)
                        {

                            int UserProfileID = Convert.ToInt32(user.UserProfileID);
                            UserCart objusercart = new UserCart();
                            int ServiceID = int.Parse(Request.QueryString["AddToCart"]);
                            bool PriceModel = Convert.ToBoolean(objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == ServiceID).IsPayAsYouGo);
                            var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID && ot.IsPayAsYouGo == PriceModel);
                            if (UserServ != null)
                            {
                                int QunatityCount = UserServ.Quantity;
                                UserServ.Quantity = QunatityCount + 1;
                                UserServ.ModifiedOn = DateTime.Now;
                                UserServ.ModifiedBy = ID;
                            }
                            else
                            {
                                objusercart.Quantity = 1;
                                objusercart.ServiceID = ServiceID;
                                objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                                objusercart.CreatedOn = DateTime.Now;
                                objusercart.CreatedBy = ID;
                                objusercart.RecordStatus = 999;
                                objusercart.IsPayAsYouGo = PriceModel;
                                objusercart.SelectedDiscount = 0;
                                objDBContext.UserCarts.AddObject(objusercart);
                            }
                            objDBContext.SaveChanges();
                            string UserMembershipID = MSU.ProviderUserKey.ToString();
                            Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=ShowMyCart");
                        }
                        else
                        { Response.Redirect("/Login.aspx", false); }
                    }
                    else
                    { Response.Redirect("/Login.aspx", false); }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void lkbtnLogOut_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            Response.Redirect("/Home.aspx", false);
        }
        
    }
}