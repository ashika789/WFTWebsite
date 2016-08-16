using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;

namespace WFTCloud.Admin.PageControls
{
    public partial class BreadCrumb : System.Web.UI.UserControl
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                var Name = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == UserName);
                lblUserName.Text = "Welcome " + Name.LastName.ToUpper() + ", " + Name.FirstName.ToUpper();
            }
        }
    }
}