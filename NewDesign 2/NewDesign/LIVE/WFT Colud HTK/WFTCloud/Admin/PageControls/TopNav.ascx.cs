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

namespace WFTCloud.Admin.PageControls
{
    public partial class TopNav : System.Web.UI.UserControl
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        #region page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                    string Name = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == UserName).FirstName;
                    //lblUserName.Text = Name;
                    //string Rolename = Roles.GetRolesForUser(UserName).First();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "Page_Load", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region ControlEvents

        protected void lkbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Response.Redirect("/Index.aspx");
        }

        #endregion
    }
}