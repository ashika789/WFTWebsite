using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
namespace WFTCloud.Customer.PageControls
{
    public partial class TopNavigation : System.Web.UI.UserControl
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
                UserMembershipID = Request.QueryString["userid"];
                if (UserMembershipID != null && UserMembershipID != "")
                {
                    //Guid ID = new Guid(UserMembershipID);
                    //var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    //lblUserName.Text = "Welcome, " + user.FirstName;
                   
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    int UserProfileID = Convert.ToInt32(user.UserProfileID);
                    var UserServ = objDBContext.UserSubscribedServices.Where(ot => ot.UserProfileID == UserProfileID && ot.RecordStatus == DBKeys.RecordStatus_Active);
                    if (UserServ.Count() > 0)
                    {
                        //SupportButton.Visible = true;
                    }
                    else
                    {
                        //SupportButton.Visible = false;
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
        protected void lkbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Response.Redirect("/Login.aspx");
        }
        #endregion

        protected void SupportButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string FreshServiceurl = WebConfigurationManager.AppSettings["TicketToolwebURL"].ToString();
                string TicketToolSecretKey = WebConfigurationManager.AppSettings["TicketToolSecretKey"].ToString();
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                String UserName = user.FirstName;
                string UserEmail = user.EmailID;
                string url = GetSsoUrl(FreshServiceurl, //including trailing slash
                                TicketToolSecretKey, UserName, UserEmail);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('"+ url +"','_newtab');", true);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

     
       
        string GetSsoUrl(string baseUrl, string secert, string name, string email)
        {
            return String.Format("{0}login/sso/?name={1}&email={2}&hash={3}", baseUrl, Server.UrlEncode(name),
            Server.UrlEncode(email), GetHash(secert, name, email));
        }
        static string GetHash(string secert, string name, string email)
        {
            string input = name + email + secert;

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                string hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
                sb.Append((hexValue.Length == 1 ? "0" : "") + hexValue);
            }
            return sb.ToString();
        }
    }
}