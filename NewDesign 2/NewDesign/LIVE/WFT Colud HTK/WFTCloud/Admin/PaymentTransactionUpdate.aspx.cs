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
using System.IO;
using System.Configuration;

namespace WFTCloud.Admin
{
    public partial class PaymentTransactionUpdate : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities ObjDBContext = new cgxwftcloudEntities();
        AuthorizeNet.CustomerGateway objGW;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
            AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
            objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
            lblSuccessMessage.Visible = false;
            lblPaymentsErrorMsg.Visible = false;
            if (!IsPostBack)
            {
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy"); 
                //Show records based on pagination value and deleted flag.
                ShowPaginatedAndDeletedRecords();
                //Check if the screen should load edit category from query string parameter.
                LoadEditPayment();
            }
        }
        #endregion

        #region ControlEvents

        protected void btnGo_Click(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkShowCancelledSubscriptionPayments_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Request.QueryString[QueryStringKeys.EditPaymentDetails].IsValid())
                {
                    int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
                    var selPayments = ObjDBContext.AllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
                    string OldWorkLog = selPayments.Worklog;
                    decimal oldAmount =Convert.ToDecimal(selPayments.CurrentMonthBilling);
                    DateTime OldPaymentDate = Convert.ToDateTime(selPayments.PaymentDate).Date;
                    
                    MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                    Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                    var upf = ObjDBContext.vwUsersListWithFullNames.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                    string TransactionID = txtTransactionID.Text.Trim();
                    string PaymentMode = ddlPaymentType.SelectedItem.Text;
                    ObjDBContext.pr_UpdateRecurringPaymentMannual(PayId, upf.FullName, "MannualUpdate", TransactionID, PaymentMode );
                   
                    LoadEditPayment();
                    lblSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "EditPaymentDetails", Ex.Message, Ex.StackTrace, DateTime.Now);
                lblPaymentsErrorMsg.Visible = true;
                lblPaymentsErrorText.Text = "Error while editing Payment details.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadEditPayment();
        }

       
        #endregion

        #region ReusableRoutines

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                DateTime FromDate = Convert.ToDateTime(txtStartDate.Text).Date;
                DateTime ToDate = Convert.ToDateTime(txtEndDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var aptmts = ObjDBContext.vwManageAllAutomatedPayments.Where(date => date.PaymentDate >= FromDate && date.PaymentDate <= ToDate).OrderByDescending(obj => obj.PaymentDate);
                if (chkShowCancelledSubscriptionPayments.Checked)
                {
                    rptrAutomatedPayments.DataSource = aptmts;
                    rptrAutomatedPayments.DataBind();
                }
                else
                {
                    rptrAutomatedPayments.DataSource = aptmts.Where(p => p.PaymentStatus.Contains("Cancel") == false);
                    rptrAutomatedPayments.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "ShowPaginatedAndDeletedRecords", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditPayment()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditPaymentDetails].IsValid())
                {
                   mvContainer.ActiveViewIndex = 1;
                    int PayId = int.Parse(Request.QueryString[QueryStringKeys.EditPaymentDetails]);
                    var selPayments = ObjDBContext.vwManageAllAutomatedPayments.FirstOrDefault(cat => cat.AllPaymentID == PayId);
                    if (selPayments != null)
                    {
                        
                        var user = ObjDBContext.vwUsersListWithFullNames.FirstOrDefault(selP => selP.UserProfileID == selPayments.UserProfileID);
                        lblusername.Text = user.FullName;
                        hypUserEmailID.Text = user.EmailID;
                        hypUserEmailID.NavigateUrl = "mailto:" + hypUserEmailID.Text;
                        lblCategoryName.Text = selPayments.CategoryName;
                        lblServiceName.Text = selPayments.ServiceName;
                        lblInitialSubscriptionDate.Text = Convert.ToDateTime(selPayments.InitialSubscriptionDate).ToString("dd-MMM-yyyy");
                        lblPaymentDate.Text = Convert.ToDateTime(selPayments.PaymentDate).ToString("dd-MMM-yyyy");
                        DateTime pDate =  Convert.ToDateTime(selPayments.PaymentDate);
                       
                        var lastDayOfMonth = DateTime.DaysInMonth(pDate.Year, pDate.Month);
                       
                        lblMode.Text = selPayments.PaymentMethod.IsValid()?selPayments.PaymentMethod:"Authorize.net";
                        
                        
                        if (selPayments.PaymentStatus == "PaymentFailed")
                        {
                            btnSave.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false;
                        }
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "EditPaymentDetails", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        #endregion


    }
}