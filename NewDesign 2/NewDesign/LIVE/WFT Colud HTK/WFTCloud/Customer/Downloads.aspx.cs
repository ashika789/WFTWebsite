using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Customer
{
    public partial class Downloads : System.Web.UI.Page
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
                UserMembershipID = Request.QueryString["userid"];
                if (!IsPostBack && UserMembershipID != null && UserMembershipID != "")
                {
                    //Show records based on pagination value for WFT Cloud Resources.
                    ShowWFTCloudResources();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ReusableRoutines

        private void ShowWFTCloudResources()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var WFTResourceCount = objDBContext.WftCloudResources.OrderBy(obj => obj.ResourceID).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active);
                if (WFTResourceCount.Count() > 0)
                {
                    if (UserMembershipID != null)
                    {
                        rptrWFTCloudResource.DataSource = WFTResourceCount;
                        rptrWFTCloudResource.DataBind();
                    }
                }
                else
                {
                    mvContainer.ActiveViewIndex = 1;
                    lblNoWFT.Text = "There are no WFT Cloud Resources.";
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