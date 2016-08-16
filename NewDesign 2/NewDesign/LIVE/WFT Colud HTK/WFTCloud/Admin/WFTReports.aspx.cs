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
    public partial class Reports1 : System.Web.UI.Page
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
            mvOrderDetails.ActiveViewIndex = 0;
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            /*
             * All Payments = 1
             * Successful Payments Report = 2
             * Failed Payments Report = 3
             * New User Signup Report = 4
             * User with no services Report = 5
             * User Report by Expiry Date = 6
             * User Report - Trial Services = 7
             * User Report - Dedicated Service = 8
             */
            try
            {
                mvContainer.Visible = true; rptrUsersList.Visible = rptrWithNoSubScribedServices.Visible = true;
                btnExportToExcel.Visible = true;
                hidFromDate.Value = hidToDate.Value = hidReportType.Value = "";
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                if (dvDate.Visible == true)
                {
                    FromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                    ToDate = Convert.ToDateTime(txtToDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                if (ddlReportType.SelectedValue == "1" || ddlReportType.SelectedValue == "2" || ddlReportType.SelectedValue == "3")
                {
                    //Show records based on pagination value user order histroy
                    UserOrderHistroy();
                }
                else if (ddlReportType.SelectedValue == "4")
                {
                    rptrWithNoSubScribedServices.Visible = false;
                    lblUserDEtails.Text = " - New User Signup";
                    mvContainer.ActiveViewIndex = 1;
                    var NewUser= objDBContext.pr_NewSignupUserReport(FromDate,ToDate);
                    rptrUsersList.DataSource = NewUser;
                    rptrUsersList.DataBind();
                    hidReportType.Value = "New_User_Signup_" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    mvUserDetails.ActiveViewIndex=0;
                    if(rptrUsersList.Items.Count == 0)
                    {
                        mvUserDetails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                    
                }
                else if (ddlReportType.SelectedValue == "5")
                {
                    rptrUsersList.Visible = false;
                    lblUserDEtails.Text = " - User with no Services";
                    mvContainer.ActiveViewIndex = 1;
                    var UserDoesnotHaveService = objDBContext.pr_UserwithnoservicesReport();
                    rptrWithNoSubScribedServices.DataSource = UserDoesnotHaveService;
                    rptrWithNoSubScribedServices.DataBind();
                    hidReportType.Value = "User_with_No_Services_on_" + DateTime.Now.ToString("dd-MMM-yyyy");
                    mvUserDetails.ActiveViewIndex = 0;
                    if (rptrWithNoSubScribedServices.Items.Count == 0)
                    {
                        mvUserDetails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "6")
                {
                    lblServiceUserNameTitle.Text = " - Service Expiry Date";
                    mvContainer.ActiveViewIndex = 2;
                    mvServiceUserDEtails.ActiveViewIndex = 0;
                    var DEdicatedServiceUser = objDBContext.pr_UserReportDedicatedService(FromDate, ToDate);
                    rptrServiceUserDetails.DataSource = DEdicatedServiceUser;
                    rptrServiceUserDetails.DataBind();
                    hidReportType.Value = "Users_Service_Expired_" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrServiceUserDetails.Items.Count == 0)
                    {
                        mvServiceUserDEtails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "7")
                {
                    lblServiceUserNameTitle.Text = " - Purchased Trial Services";
                    mvContainer.ActiveViewIndex = 2;
                    mvServiceUserDEtails.ActiveViewIndex = 0;
                    var ServiceExpDateUser = objDBContext.pr_UserReportbyExpiryDate(FromDate, ToDate);
                    rptrServiceUserDetails.DataSource = ServiceExpDateUser;
                    rptrServiceUserDetails.DataBind();
                    hidReportType.Value = "User_Purchased_Trial_Services_" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrServiceUserDetails.Items.Count == 0)
                    {
                        mvServiceUserDEtails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if(ddlReportType.SelectedValue=="8")
                {
                    lblServiceUserNameTitle.Text = " - Purchased Dedicated Services";
                    mvContainer.ActiveViewIndex = 2;
                    mvServiceUserDEtails.ActiveViewIndex = 0;
                    var TrailServiceUser = objDBContext.pr_UserReportTrialServices(FromDate, ToDate);
                    rptrServiceUserDetails.DataSource = TrailServiceUser;
                    rptrServiceUserDetails.DataBind();
                    hidReportType.Value = "User_Purchased_Dedicated_Services_" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrServiceUserDetails.Items.Count == 0)
                    {
                        mvServiceUserDEtails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "9")
                {
                    rptrWithNoSubScribedServices.Visible = false;
                    mvContainer.ActiveViewIndex = 3;
                    var NewUser = objDBContext.pr_UsersHowDidYouHearAboutUs(FromDate, ToDate);
                    rptrHearAboutUs.DataSource = NewUser;
                    rptrHearAboutUs.DataBind();
                    hidReportType.Value = "User_How_did_you_hear_about_us" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    mvHearAboutUs.ActiveViewIndex = 0;
                    if (rptrHearAboutUs.Items.Count == 0)
                    {
                        mvHearAboutUs.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "10")
                {
                    rptrWithNoSubScribedServices.Visible = false;
                    mvContainer.ActiveViewIndex = 4;
                    rptrOrderDetailsFailedOnPurchase.DataSource = UserOrderFailedDetails();
                    rptrOrderDetailsFailedOnPurchase.DataBind();

                    hidReportType.Value = "User_Order_Details_Failed_On_Purchase_" + DateTime.Now.ToString("dd-MMM-yyyy");
                    mvOrderDetailsFailedOnPurchase.ActiveViewIndex = 0;
                    if (rptrOrderDetailsFailedOnPurchase.Items.Count == 0)
                    {
                        mvOrderDetailsFailedOnPurchase.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }

                else if (ddlReportType.SelectedValue == "11" )
                {

                    
                    mvContainer.ActiveViewIndex = 5;
                    
                        rptrSubscriberList.DataSource = objDBContext.NewsLetterSignUps.OrderBy(a => a.EmailID);
                        rptrSubscriberList.DataBind();

                    hidReportType.Value = "WFTCloud.com_NewsLetter_Subscribers_List_" + DateTime.Now.ToString("dd-MMM-yyyy");
                    mvNewsLetterInnverMV.ActiveViewIndex = 0;
                    if (rptrSubscriberList.Items.Count == 0)
                    {
                        mvNewsLetterInnverMV.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "12")
                {

                   
                    mvContainer.ActiveViewIndex = 6;
                    
                        rptrOrgSubscriberList.DataSource = objDBContext.VisitorsDetails.Where(a=>a.Status == 1).OrderBy(a => a.Email);
                        rptrOrgSubscriberList.DataBind();
                   
                    hidReportType.Value = "WFTCloudOrg_NewsLetter_Subscribers_List_" + DateTime.Now.ToString("dd-MMM-yyyy");
                    mvOrgNewsLetterInnverMV.ActiveViewIndex = 0;
                    if (rptrOrgSubscriberList.Items.Count == 0)
                    {
                        mvOrgNewsLetterInnverMV.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "13")
                {
                    lblNewServicePurchasedTitle.Text = FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy");
                    mvContainer.ActiveViewIndex = 7;
                    mvNewServicePurchasedDetails.ActiveViewIndex = 0;
                    var ServicePurchased = objDBContext.pr_NewServicePurchasedReport(FromDate, ToDate);
                    rptrNewServicePurchasedDetails.DataSource = ServicePurchased;
                    rptrNewServicePurchasedDetails.DataBind();
                    hidReportType.Value = "User_Purchased_New_Services_" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrNewServicePurchasedDetails.Items.Count == 0)
                    {
                        mvNewServicePurchasedDetails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "14")
                {
                    lblTrainingRequestTitle.Text = FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy");
                    mvContainer.ActiveViewIndex = 8;
                    mvNewServicePurchasedDetails.ActiveViewIndex = 0;
                    var ServicePurchased = objDBContext.pr_TrainingRequestReport(FromDate, ToDate);
                    rptrTrainingRequestDetails.DataSource = ServicePurchased;
                    rptrTrainingRequestDetails.DataBind();
                    hidReportType.Value = "Training_Request from" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrTrainingRequestDetails.Items.Count == 0)
                    {
                        mvTrainingRequestDetails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
                }
                else if (ddlReportType.SelectedValue == "15")
                {
                    lblFailedPaymentsReport.Text = FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy");
                    mvContainer.ActiveViewIndex = 9;
                    mvFailedPaymentsDetails.ActiveViewIndex = 0;
                    var ServicePurchased = objDBContext.pr_MonthlyFailedPaymentReport(FromDate, ToDate);
                    rptrFailedPaymentsDetails.DataSource = ServicePurchased;
                    rptrFailedPaymentsDetails.DataBind();
                    hidReportType.Value = "Failed_Payments from" + FromDate.ToString("dd-MMM-yyyy") + "_To_" + ToDate.ToString("dd-MMM-yyyy");
                    if (rptrFailedPaymentsDetails.Items.Count == 0)
                    {
                        mvFailedPaymentsDetails.ActiveViewIndex = 1;
                        hidReportType.Value = "";
                        btnExportToExcel.Visible = false;
                    }
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
            txtFromDate.Text = txtToDate.Text = hidReportType.Value = "";
            if (ddlReportType.SelectedValue == "5" || ddlReportType.SelectedValue == "10" || ddlReportType.SelectedValue == "11" || ddlReportType.SelectedValue == "12")
                dvDate.Visible = false;
            else
                dvDate.Visible = true;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            /*
            * All Payments = 1
            * Successful Payments Report = 2
            * Failed Payments Report = 3
            * New User Signup Report = 4
            * User with no services Report = 5
            * User Report by Expiry Date = 6
            * User Report - Trial Services = 7
            * User Report - Dedicated Service = 8
             * WFTCloud.com Newsletter Subscribers List = 11
             * WFTCloud Org Newsletter Subscribers List = 12
             * WFTCloud New Service Purchased List = 13
            */
            if (hidReportType.Value != "")
            {
                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + hidReportType.Value + ".xls");
                Response.ContentEncoding = Encoding.UTF8;
                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                if (mvContainer.ActiveViewIndex == 0)
                {
                    //Payments 
                    rptrPaymentHistroy.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 1)
                {
                    //New Signup user & User With no Service 
                    if (rptrUsersList.Visible == true)
                        rptrUsersList.RenderControl(hw);
                    else
                        rptrWithNoSubScribedServices.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 2)
                {
                    //User Details - How did they hear about wft
                    rptrServiceUserDetails.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 3)
                {
                    //User list With Corresponding service
                    rptrHearAboutUs.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 4)
                {
                    //User Order Details Failed On Purchase
                    rptrOrderDetailsFailedOnPurchase.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 5)
                {
                    //WFTCloud com Subscribers LIst

                    rptrSubscriberList.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 6)
                {
                    //WFTCloud Org Subscribers LIst

                    rptrOrgSubscriberList.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 7)
                {
                    //WFTCloud New Service Purchased List

                    rptrNewServicePurchasedDetails.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 8)
                {
                    //WFTCloud New Service Purchased List

                    rptrTrainingRequestDetails.RenderControl(hw);
                }
                else if (mvContainer.ActiveViewIndex == 9)
                {
                    //WFTCloud New Service Purchased List

                    rptrFailedPaymentsDetails.RenderControl(hw);
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

        private void UserOrderHistroy()
        {
            try
            {
                mvContainer.ActiveViewIndex = 0;
                DateTime startDate = Convert.ToDateTime(txtFromDate.Text); 
                DateTime Enddate = Convert.ToDateTime(txtToDate.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
                var ohist = objDBContext.UserPaymentTransactions.Where(ord => ord.PaymentDateTime >= startDate && ord.PaymentDateTime<= Enddate);
                if (ddlReportType.SelectedValue == "1")
                {
                    rptrPaymentHistroy.DataSource = ohist;
                    lblPaymentLabel.Text = " - All Payments";
                    hidReportType.Value = "All_Payments_From_" + startDate.ToString("dd-MMM-yyyy") +"_To_"+Enddate.ToString("dd-MMM-yyyy");
                }
                else if (ddlReportType.SelectedValue == "2")
                {
                    rptrPaymentHistroy.DataSource = ohist.Where(o => o.Approved == true);
                    lblPaymentLabel.Text = " - Successful Payments";
                    hidReportType.Value = "Successful_Payments_From_" + startDate.ToString("dd-MMM-yyyy") + "_To_" + Enddate.ToString("dd-MMM-yyyy");
                }
                else if (ddlReportType.SelectedValue == "3")
                {
                    rptrPaymentHistroy.DataSource = ohist.Where(o => o.Approved == false);
                    lblPaymentLabel.Text = " - Failed Payments";
                    hidReportType.Value = "Failed_Payments_From_" + startDate.ToString("dd-MMM-yyyy") + "_To_" + Enddate.ToString("dd-MMM-yyyy");
                }

                rptrPaymentHistroy.DataBind();
                if (rptrPaymentHistroy.Items.Count <= 0)
                {
                    hidReportType.Value = "";
                    mvOrderDetails.ActiveViewIndex = 1;
                    btnExportToExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message + "FromDate= " + txtFromDate.Text + "ToDate= " + txtToDate.Text, Ex.StackTrace, DateTime.Now);
            }
        }

        public string UserEmailID(string UserProfileID)
        {
            int userid = Convert.ToInt32(UserProfileID);
            var Users = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == userid);
            return Users.EmailID;
        }

        public List<vwUserOrderDetailsTransactionFailedOnPurchase> UserOrderFailedDetails()
        {
            List<vwUserOrderDetailsTransactionFailedOnPurchase> UODTFailed = new List<vwUserOrderDetailsTransactionFailedOnPurchase>();
            var a = objDBContext.vwUserOrderDetailsTransactionFailedOnPurchases;
            foreach (var result in a)
            {
                var UODTSuccess = objDBContext.vwUserOrderDetailsTransactionSucceedOnPurchases.FirstOrDefault(z => z.EmailID == result.EmailID && z.ServiceID == result.ServiceID && z.OrderDateTime >= result.OrderDateTime);
                if (UODTSuccess == null)
                {
                    var a1 = UODTFailed.Where(a2 => a2.ServiceID == result.ServiceID && a2.UserProfileID == result.UserProfileID);
                    if (a1.Count() == 0)
                        UODTFailed.Add(result);
                }
            }
            return UODTFailed.ToList();
        }

        #endregion
    }
}