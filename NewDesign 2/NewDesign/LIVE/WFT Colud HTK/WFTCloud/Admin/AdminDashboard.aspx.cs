using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region Pageload

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadEmailIDS();
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

        #region ReusableRoutines

        private void LoadEmailIDS()
        {
            int USerSubID = Convert.ToInt32(objDBContext.UserSubscribedServices.Max(z => z.UserSubscriptionID));
            var SubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == USerSubID);

            UserProfile userProf = objDBContext.UserProfiles.FirstOrDefault(upf => upf.UserProfileID == SubInfo.UserProfileID);
            string UserFullName = userProf.FirstName + " " + userProf.MiddleName + " " + userProf.LastName;

            ServiceCatalog serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(scat => scat.ServiceID == SubInfo.ServiceID);

            var categoryDetails = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == SubInfo.ServiceCategoryID);

            lblrecentCustomer.Text = UserFullName;
            lblrecentService.Text = serviceDetails.ServiceName;
            lblrecentCategory.Text = categoryDetails.CategoryName;

            var MonthlySubscribedHsitory = objDBContext.vwGetMonthlySubscriptionHistories.OrderBy(a => a.CategoryName);
            if (MonthlySubscribedHsitory.Count() > 0)
            {
                rptrMonthlySubscriptionHistoryDetails.DataSource = MonthlySubscribedHsitory;
                rptrMonthlySubscriptionHistoryDetails.DataBind();
            }
        }

        #endregion
    }
}