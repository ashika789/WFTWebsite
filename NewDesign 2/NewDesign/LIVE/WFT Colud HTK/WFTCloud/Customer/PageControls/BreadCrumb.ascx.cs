using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;

namespace WFTCloud.Customer.PageControls
{
    public partial class BreadCrumb : System.Web.UI.UserControl
    {    
        #region Global Variables and Properties
        public string UserMembershipID; 
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            UserMembershipID = Request.QueryString["userid"];
            if (UserMembershipID != null && UserMembershipID != "")
            {
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                lblUserName.Text = "Welcome " + user.LastName.ToUpper() + ", " + user.FirstName.ToUpper();
            }
        }
        #endregion

    }
}