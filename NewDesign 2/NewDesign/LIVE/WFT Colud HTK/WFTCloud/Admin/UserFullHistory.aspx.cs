using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
using System.Web.Security;
using System.Security.Principal;
using System.Text;
using System.IO;

namespace WFTCloud.Admin
{
    public partial class UserFullHistory : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
             
                 HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                 if (cookie != null)
                 {
                     string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                     string Rolename = Roles.GetRolesForUser(UserName).First();
                     if (Rolename == DBKeys.Role_Administrator || Rolename == DBKeys.Role_SuperAdministrator)
                     {
                         if (!IsPostBack)
                         {
                             //Show records based on pagination value and deleted flag.
                            
                             ShowSubscribedRecords();
                         }
                     }
                     else
                     {
                         Response.Redirect("/LoginPage.aspx");
                     }
                 }
                 else
                 {
                     Response.Redirect("/LoginPage.aspx");
                 }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region ControlEvents

        #endregion

        #region Resuable Routines


        public string GetCategoryName(string CID)
        {
            int CategoryID = Convert.ToInt32(CID);
            var services = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
            return services.CategoryName;
        }

        public string ServiceName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.ServiceName;
        }
        public string ShowSubscribedServiceStatus(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
            var usdetails = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UsubId);

            if (usdetails.RecordStatus == 0)
                return "Expired";
            else if (usdetails.RecordStatus == -1)
                return "Cancelled";
            else
            {
                if (usdetails.ExpirationDate == null)
                {
                    return "Expired";
                }
                DateTime ExpDate = Convert.ToDateTime(usdetails.ExpirationDate);
                if (ExpDate < DateTime.Now)
                {
                    return "Expired";
                }
                if (usdetails.AutoProvisioningDone == true)
                    return "Active";
                else
                    return "Provision Pending";
            }

        }
       
        private void ShowSubscribedRecords()
        {
            try
            {
                if (Request.QueryString["viewuserprofileid"].IsValid())
                {
                    
                    string UserMembershipID = Request.QueryString["viewuserprofileid"];
                    Guid ID = new Guid(UserMembershipID);
                    var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                    lblUserNamefor.Text = UserDetails.FirstName + " " + UserDetails.LastName;
                    lblUserName.Text = UserDetails.FirstName + " " + UserDetails.LastName + " " + UserDetails.MiddleName + "(" + UserDetails.EmailID + ")";
                    lblCompanyName.Text = UserDetails.CompanyName;
                    lblContactAddress.Text = UserDetails.MailingAddress;
                    lblContactNumber.Text = UserDetails.ContactNumber + " " + UserDetails.MobileNumber + " " + UserDetails.PhoneNumber;
                    lblLocation.Text = UserDetails.Location;
                    var SubscribedService = objDBContext.vwManageSubscribedServices.Where(cat => cat.UserProfileID == UserDetails.UserProfileID);
                    rptrSubscribedPackages.DataSource = SubscribedService;
                    rptrSubscribedPackages.DataBind();
                    mvSubscriptionDetails.ActiveViewIndex = 1;

                    if (rptrSubscribedPackages.Items.Count == 0)
                    {
                        hidReportType.Value = "";
                        btnExportToExcel.Enabled = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                 string UserMembershipID = Request.QueryString["viewuserprofileid"];
                 Guid ID = new Guid(UserMembershipID);
                 var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                 var ServicePurchased = objDBContext.pr_GetUsersAllSubscriptionDetails(Convert.ToInt32(UserDetails.UserProfileID));

                 DataGrid ServicePayment = new DataGrid();
              
                 ServicePayment.DataSource = ServicePurchased;
                 ServicePayment.DataBind();
                 hidReportType.Value = "Complete Subscription history of " + UserDetails.FirstName + " " + UserDetails.LastName + " " + UserDetails.MiddleName + "(" + UserDetails.EmailID + ")"; 

                    if (hidReportType.Value != "")
                    {
                        Response.ContentType = "application/x-msexcel";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + hidReportType.Value + ".xls");
                        Response.ContentEncoding = Encoding.UTF8;
                        StringWriter tw = new StringWriter();
                        HtmlTextWriter hw = new HtmlTextWriter(tw);

                        ServicePayment.RenderControl(hw);
                      
                        Response.Write(tw.ToString());
                        Response.End();
                    }
                    else
                    {

                    }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

      
       

        
        
    }
}