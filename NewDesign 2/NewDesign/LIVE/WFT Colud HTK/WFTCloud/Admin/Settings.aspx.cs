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
    public partial class Settings : System.Web.UI.Page
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

        protected void btnNewSave_Click(object sender, EventArgs e)
        {
            try
            {
                var NewAdminSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.Admin_Email);
                var NewSalesSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.Sales_Email);
                var NewTechnicalSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.Technical_Email);
                var NewHoursSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.HOURS_PER_MONTH);
                var NewDaysSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.DAYS_PER_MONTH);
                var NewMaintenanceSetting = objDBContext.WftSettings.FirstOrDefault(set => set.SettingsID == DBKeys.Maintenance_Email);
                var NewAppURL = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "APP_URL");
                var SiteDownForMaintenance = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "SITE_LOCKED");
                var NewSupportTeamMails = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "SUPPORT_MAIL");
                var NewSAPBasisTeamMails = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "SAPBASIS_MAIL");
                txtSupportTeam.Text = txtSupportTeam.Text.Replace(" ", "");
                txtSupportTeam.Text = txtSupportTeam.Text[txtSupportTeam.Text.Length - 1] != ',' ? txtSupportTeam.Text : txtSupportTeam.Text.Remove(txtSupportTeam.Text.Length - 1);
                txtAdminEmail.Text = txtAdminEmail.Text.Replace(" ", "");
                txtAdminEmail.Text = txtAdminEmail.Text[txtAdminEmail.Text.Length - 1] != ',' ? txtAdminEmail.Text : txtAdminEmail.Text.Remove(txtAdminEmail.Text.Length - 1);
                txtSalesEmail.Text = txtSalesEmail.Text.Replace(" ", "");
                txtSalesEmail.Text = txtSalesEmail.Text[txtSalesEmail.Text.Length - 1] != ',' ? txtSalesEmail.Text : txtSalesEmail.Text.Remove(txtSalesEmail.Text.Length - 1);
                txtTechnicalEmail.Text = txtTechnicalEmail.Text.Replace(" ", "");
                txtTechnicalEmail.Text = txtTechnicalEmail.Text[txtTechnicalEmail.Text.Length - 1] != ',' ? txtTechnicalEmail.Text : txtTechnicalEmail.Text.Remove(txtTechnicalEmail.Text.Length - 1);
                txtMaintenanceEmail.Text = txtMaintenanceEmail.Text.Replace(" ", "");
                txtMaintenanceEmail.Text = txtMaintenanceEmail.Text[txtMaintenanceEmail.Text.Length - 1] != ',' ? txtMaintenanceEmail.Text : txtMaintenanceEmail.Text.Remove(txtMaintenanceEmail.Text.Length - 1);
                txtSAPBasisEmail.Text = txtSAPBasisEmail.Text.Replace(" ", "");
                txtSAPBasisEmail.Text = txtSAPBasisEmail.Text[txtSAPBasisEmail.Text.Length - 1] != ',' ? txtSAPBasisEmail.Text : txtSAPBasisEmail.Text.Remove(txtSAPBasisEmail.Text.Length - 1);
                if (SiteDownForMaintenance == null)
                {
                    WftSetting APPURL = new WftSetting();
                    APPURL.SettingKey = "SITE_LOCKED";
                    APPURL.SettingValue = chkSiteDownForMaintenance.Checked ? "1" : "0";
                    objDBContext.WftSettings.AddObject(APPURL);
                }
                else
                {
                    SiteDownForMaintenance.SettingValue = chkSiteDownForMaintenance.Checked ? "1" : "0";
                }
                if (NewAppURL == null)
                {
                    WftSetting APPURL = new WftSetting();
                    APPURL.SettingKey = "APP_URL";
                    APPURL.SettingValue = txtAppURL.Text;
                    objDBContext.WftSettings.AddObject(APPURL);
                }
                else
                {
                    NewAppURL.SettingValue = txtAppURL.Text;
                }
                if (NewSupportTeamMails == null)
                {
                    WftSetting APPURL = new WftSetting();
                    APPURL.SettingKey = "SUPPORT_MAIL";
                    APPURL.SettingValue = txtSupportTeam.Text;
                    objDBContext.WftSettings.AddObject(APPURL);
                }
                else
                {
                    NewSupportTeamMails.SettingValue = txtSupportTeam.Text;
                }
                
                NewAdminSetting.SettingValue = txtAdminEmail.Text;
                NewSalesSetting.SettingValue = txtSalesEmail.Text;
                NewTechnicalSetting.SettingValue = txtTechnicalEmail.Text;
                NewHoursSetting.SettingValue = txtHoursPerMonth.Text;
                NewDaysSetting.SettingValue = txtDaysPerMonth.Text;
                NewSupportTeamMails.SettingValue = txtSupportTeam.Text;
                NewMaintenanceSetting.SettingValue = txtMaintenanceEmail.Text;
                NewSAPBasisTeamMails.SettingValue = txtSAPBasisEmail.Text;
                objDBContext.SaveChanges();
                divEmailSuccess.Visible = true;
                divEmailError.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            divEmailSuccess.Visible = false;
            divEmailError.Visible = false;
            LoadEmailIDS();
        }

        #endregion

        #region ReusableRoutines

        private void LoadEmailIDS()
        {
            var AdminEmail = objDBContext.WftSettings.FirstOrDefault(email=>email.SettingsID==DBKeys.Admin_Email);
            var SalesEmail = objDBContext.WftSettings.FirstOrDefault(email=>email.SettingsID==DBKeys.Sales_Email);
            var TechnicalEmail = objDBContext.WftSettings.FirstOrDefault(email=>email.SettingsID==DBKeys.Technical_Email);
            var Hours = objDBContext.WftSettings.FirstOrDefault(email => email.SettingsID == DBKeys.HOURS_PER_MONTH);
            var Days = objDBContext.WftSettings.FirstOrDefault(email => email.SettingsID == DBKeys.DAYS_PER_MONTH);
            var AppURL = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "APP_URL");
            var SiteDown = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "SITE_LOCKED");
            var SupportTeamMails = objDBContext.WftSettings.FirstOrDefault(set => set.SettingKey == "SUPPORT_MAIL");
            var MaintenanceEmail = objDBContext.WftSettings.FirstOrDefault(email => email.SettingsID == DBKeys.Maintenance_Email);
            var SAPBasisEmail = objDBContext.WftSettings.FirstOrDefault(email => email.SettingsID == DBKeys.SAPBasis_Email);
            if (SupportTeamMails != null)
                txtSupportTeam.Text = SupportTeamMails.SettingValue;
            if(SiteDown!= null)
                chkSiteDownForMaintenance.Checked=SiteDown.SettingValue=="1"?true:false;
            if(AppURL != null)
                txtAppURL.Text = AppURL.SettingValue;
            if(AdminEmail!= null)
                txtAdminEmail.Text = AdminEmail.SettingValue;
            if (SalesEmail != null)
                txtSalesEmail.Text = SalesEmail.SettingValue;
            if (TechnicalEmail != null)
                txtTechnicalEmail.Text = TechnicalEmail.SettingValue;
            if (Hours != null)
                txtHoursPerMonth.Text = Hours.SettingValue;
            if (Days != null)
                txtDaysPerMonth.Text = Days.SettingValue;
            if (MaintenanceEmail != null)
                txtMaintenanceEmail.Text = MaintenanceEmail.SettingValue;
            if (SAPBasisEmail != null)
                txtSAPBasisEmail.Text = SAPBasisEmail.SettingValue;

        }

        #endregion
    }
}