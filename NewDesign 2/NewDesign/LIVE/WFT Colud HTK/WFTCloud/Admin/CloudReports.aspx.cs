using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class CloudReports : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        #region Pageload

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #endregion

        #region ControlEvents

        protected void lkbtnBakcToOrderDetails_Click(object sender, EventArgs e)
        {
            mvServiceWisePurchasedDetails.ActiveViewIndex = 0;
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
           
            try
            {
                mvContainer.Visible = true; 
                btnExportToExcel.Visible = true;
                hidFromDate.Value = hidToDate.Value = hidReportType.Value = "";
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
               
                if (ddlReportType.SelectedValue == "1" || ddlReportType.SelectedValue == "2" || ddlReportType.SelectedValue == "3")
                {
                    //Show records based on pagination value user order histroy
                    UserOrderHistroy();
                }
               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExportToExcel.Visible = mvContainer.Visible = false;
           
            if (ddlReportType.SelectedValue == "1")
            {
                InitializeDropdowns();
                DivServiceList.Visible = true;
            }
            else
            {
                DivServiceList.Visible = false;
            }
        }
        protected void ddlChooseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelectedCategory = Convert.ToInt32(ddlChooseCategory.SelectedValue);

                var services = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceName).Where(cat => cat.ServiceCategoryID == SelectedCategory && cat.UserSpecific == false && cat.RecordStatus == DBKeys.RecordStatus_Active).ToList();
                services.Insert(0, new ServiceCatalog()
                {
                    ServiceID = 0,
                    ServiceName = "N/A",
                    ServiceDescription = string.Empty,
                    ReleaseVersion = string.Empty,
                    SystemType = string.Empty,
                    WFTCloudPrice = 0.00M
                });

                ddlServices.DataSource = services;
                ddlServices.DataTextField = "ServiceName";
                ddlServices.DataValueField = "ServiceID";
                ddlServices.DataBind();

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
          
            if (hidReportType.Value != "")
            {
                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Purchased List" + ddlChooseCategory.SelectedItem.Text + " - " + ddlServices.SelectedItem.Text + ".xls");
                Response.ContentEncoding = Encoding.UTF8;
                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                if (mvContainer.ActiveViewIndex == 0)
                {
                    //Payments 
                    rptrServiceWisePurchasedDetails.RenderControl(hw);
                }
                
                Response.Write(tw.ToString());
                Response.End();


            }
            else
            {

            }
        }

        #endregion

        #region Resuable Routines

        public string CategoryName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            var category = objDBContext.ServiceCategories.FirstOrDefault(d => d.ServiceCategoryID == services.ServiceCategoryID);
            return category.CategoryName;
        }

        public string ServiceName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.ServiceName;
        }
        private void InitializeDropdowns()
        {
            try
            {

                ddlChooseCategory.DataSource = objDBContext.ServiceCategories.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
                ddlChooseCategory.DataTextField = "CategoryName";
                ddlChooseCategory.DataValueField = "ServiceCategoryID";
                ddlChooseCategory.DataBind();

             

               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        private void UserOrderHistroy()
        {
            try
            {
                mvContainer.ActiveViewIndex = 0;
                int CategoryID = Convert.ToInt32(ddlChooseCategory.SelectedValue);
                int ServiceID = Convert.ToInt32(ddlServices.SelectedValue);
                var ohist = objDBContext.UserSubscribedServices.Where(Usr => Usr.ServiceID == ServiceID && Usr.ServiceCategoryID == CategoryID && Usr.RecordStatus == DBKeys.RecordStatus_Active);
                if (ddlReportType.SelectedValue == "1")
                {
                    rptrServiceWisePurchasedDetails.DataSource = ohist;
                    lblServiceWisePurchasedTitle.Text = " -Service Purchased List";
                    hidReportType.Value = "Purchased List" + ddlChooseCategory.SelectedItem.Text + " - " + ddlServices.SelectedItem.Text;
                }
               

                rptrServiceWisePurchasedDetails.DataBind();
                if (rptrServiceWisePurchasedDetails.Items.Count <= 0)
                {
                    hidReportType.Value = "";
                    mvServiceWisePurchasedDetails.ActiveViewIndex = 1;
                    btnExportToExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message , Ex.StackTrace, DateTime.Now);
            }
        }

        public string UserEmailID(string UserProfileID)
        {
            int userid = Convert.ToInt32(UserProfileID);
            var Users = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == userid);
            return Users.EmailID;
        }


       


       

        #endregion
    }
}