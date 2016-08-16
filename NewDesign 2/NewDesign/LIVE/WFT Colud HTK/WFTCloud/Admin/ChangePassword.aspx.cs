using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Customer
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;
        #endregion

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = "Change Password";
                if (Request.QueryString["userid"].IsValid())
                {
                    UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var User = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID && a.TempPasswordProvided == true);
                    if (User != null)
                    {
                        if (!IsPostBack)
                        {
                            mpopupCanCelServices.Show();
                            Session["UserId"] = Convert.ToString(Request.QueryString["userid"]);
                        }
                    }
                    else
                    {
                            Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=SubscribedService", false);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }
        #endregion

        #region Control Events
        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["userid"].IsValid())
                {
                    Guid ID = new Guid(Request.QueryString["userid"]);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    if (user != null)
                    {
                        if (user.TempPasswordProvided == true)
                        {
                            MembershipUser MSU = Membership.GetUser(user.EmailID);
                            MSU.ChangePassword(MSU.GetPassword(), txtConformPassword.Text);
                            Membership.UpdateUser(MSU);
                            user.TempPasswordProvided = false;
                            objDBContext.SaveChanges();
                            Response.Redirect("/Customer/CloudPackages.aspx?userid=" + ID.ToString() + "&showview=SubscribedService", false);
                        }
                        else
                        {
                            Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=SubscribedService", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("/Loginpage.aspx", false);
                    }
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