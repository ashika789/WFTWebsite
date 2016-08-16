using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class CouponManagement : System.Web.UI.Page
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
                        //Clear All Labels
                        ClearLabels();
                        CalendarExtender2.StartDate = DateTime.Now;

                        if (!IsPostBack)
                        {
                            //Show appropriate view
                            ShowRequestedView();
                            //Check if the screen should delete any category from query string parameter.
                            UpdateCouponStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any category from query string parameter.
                            UpdateCouponStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any category from query string parameter.
                            UpdateCouponStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);

                            //Show records
                            ShowPaginatedAndDeletedRecords();
                        }
                    }
                    else
                    {
                        Response.Redirect("/LoginPage.aspx",false);
                    }
                }
                else
                {
                    Response.Redirect("/LoginPage.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowRequestedView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    switch (Request.QueryString[QueryStringKeys.ShowView])
                    {
                        case QueryStringKeys.EditCoupon:
                            mvContainer.ActiveViewIndex = 1;
                            hdnCouponID.Value = Request.QueryString[QueryStringKeys.CouponID];
                            LoadEditCouponView();
                            break;
                        default:
                            mvContainer.ActiveViewIndex = 0;
                            break;
                    }
                }
                else
                {
                    mvContainer.ActiveViewIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditCouponView()
        {
            try
            {
                int CouponID = int.Parse(hdnCouponID.Value);
                
                if (CouponID > 0)
                {
                    txtCouponCode.ReadOnly = true;
                    Coupon ObjMain=objDBContext.Coupons.FirstOrDefault(obj => obj.CouponID == CouponID);

                    LoadFieldsFromData(ObjMain);
                    lblTableHeader.Text = "Edit Coupon Details";
                }
                else
                {
                    ClearFormForNewEntry();
                    lblTableHeader.Text = "New Coupon Details";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ClearFormForNewEntry()
        {
            try
            {
                InitializeDropdowns();
                int CouponTypeID = Convert.ToInt32(ddlCouponTypes.SelectedValue);
                var CouponTpeDetails = objDBContext.CouponTypes.FirstOrDefault(c => c.CouponTypeID == CouponTypeID);
                if (CouponTpeDetails.IsCouponUnlimited == true)
                    pnlCouponCount.Visible = false;
                else
                    pnlCouponCount.Visible = true;
                txtCouponName.Text = txtValidityDate.Text = string.Empty;
                txtDiscount.Text = "0.00";
                txtUserCount.Text = "0";
                txtValidityDate.Text = string.Empty;
                //txtValidityDays.Text = "0";

                txtCouponCode.Text = Guid.NewGuid().ToString().Substring(0, 8);
                
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadFieldsFromData(Coupon coupon)
        {
            try
            {
                InitializeDropdowns();

                txtCouponName.Text = coupon.CouponName;
                txtCouponCode.Text = coupon.CouponCode;
                txtDiscount.Text = coupon.Discount.ToString();
                txtUserCount.Text = (coupon.UserCount ?? 0).ToString();
                txtValidityDate.Text = coupon.ValidityDate.HasValue ? coupon.ValidityDate.Value.ToString("dd-MMM-yyyy") : string.Empty;
                //txtValidityDays.Text = (coupon.ValidityInDays ?? 0).ToString();

                //chkIsUsed.Checked = coupon.IsUsed ?? false;
                if (coupon.CouponCount == 0)
                    chkIsUsed.Checked = true;
                else
                    chkIsUsed.Checked = false;
                txtCouponCount.Text = Convert.ToString(coupon.CouponCount);
                //txtCouponType.Text = coupon.CouponType;

                ddlRecordStatus.SelectedValue = (coupon.RecordStatus).ToString();
                ddlServices.SelectedValue = (coupon.ForServiceID ?? 0).ToString();
                int ServiceID = Convert.ToInt32((coupon.ForServiceID ?? 0).ToString());
                GetServiceCategoryID(ServiceID);
                ddlCouponTypes.SelectedValue = coupon.CouponTypeID.ToString();
                int CouponTypeID = Convert.ToInt32(ddlCouponTypes.SelectedValue);
                var CouponTpeDetails = objDBContext.CouponTypes.FirstOrDefault(c => c.CouponTypeID == CouponTypeID);
                if (CouponTpeDetails.IsCouponUnlimited == true)
                    pnlCouponCount.Visible = false;
                else
                    pnlCouponCount.Visible = true;
                ddlUsers.SelectedValue = coupon.ForUser.HasValue ? coupon.ForUser.ToString() : "0";
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void InitializeDropdowns()
        {
            try
            {

                ddlChooseCategory.DataSource = objDBContext.ServiceCategories.OrderBy(cat => cat.CategoryName).Where(st => st.RecordStatus != DBKeys.RecordStatus_Delete && st.RecordStatus != DBKeys.RecordStatus_Inactive);
                ddlChooseCategory.DataTextField = "CategoryName";
                ddlChooseCategory.DataValueField = "ServiceCategoryID";
                ddlChooseCategory.DataBind();

                int SelectedCategory = Convert.ToInt32(ddlChooseCategory.SelectedValue);

                var services = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceName).Where(cat => cat.ServiceCategoryID == SelectedCategory).ToList();
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

                //var services = objDBContext.ServiceCatalogs.ToList();

                //services.Insert(0, new ServiceCatalog()
                //{
                //    ServiceID = 0,
                //    ServiceName = "N/A",
                //    ServiceDescription = string.Empty,
                //    ReleaseVersion = string.Empty,
                //    SystemType = string.Empty,
                //    WFTCloudPrice = 0.00M
                //});

                //ddlServices.DataSource = services;
                //ddlServices.DataTextField = "ServiceName";
                //ddlServices.DataValueField = "ServiceID";
                //ddlServices.DataBind();

                ddlCouponTypes.DataSource = objDBContext.CouponTypes.OrderBy(A=>A.CouponType1);
                ddlCouponTypes.DataTextField = "CouponType1";
                ddlCouponTypes.DataValueField = "CouponTypeID";
                ddlCouponTypes.DataBind();

                var AppUsers = objDBContext.vwUsersListWithFullNames.Where(obj => obj.UserRole == DBKeys.Role_PersonalUser || obj.UserRole == DBKeys.Role_BusinessUser || obj.UserRole == DBKeys.Role_EnterpriseUser).OrderBy(a => a.FullName).ToList();
                AppUsers.Insert(0, new vwUsersListWithFullName() { UserProfileID = 0, UserMembershipID = Guid.Empty, FirstName = "N/A", LastName = string.Empty, FullName = "N/A", RecordStatus = 1 });
                ddlUsers.DataSource = AppUsers;
                ddlUsers.DataTextField = "FullName";
                ddlUsers.DataValueField = "UserMembershipID";
                ddlUsers.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void UpdateCouponStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int serviceID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetCouponFromID(serviceID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                    }

                    divCatSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrCoupons.DataSource = objDBContext.vwCouponsLists;
                    rptrCoupons.DataBind();
                }
                else
                {
                    rptrCoupons.DataSource = objDBContext.vwCouponsLists.Where(obj => obj.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrCoupons.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        private void ClearLabels()
        {
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            divCatErrorMessage.Visible = false;
            divCatSuccessMessage.Visible = false;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CouponManagement.aspx?showview=editcoupon&couponid=0");
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCoupons(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        private void ActivateDeactivateCoupons(int recStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCoupons.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnCouponID = (rItem.FindControl("hdnCouponID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCoupon = GetCouponFromID(int.Parse(hdnCouponID.Value));
                        if (selCoupon.IsNotNull())
                        {
                            selCoupon.RecordStatus = recStatus;
                            divCatSuccessMessage.Visible = true;
                        }
                    }
                }
                objDBContext.SaveChanges();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private Coupon GetCouponFromID(int couponID)
        {
            return objDBContext.Coupons.FirstOrDefault(obj => obj.CouponID == couponID);
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCoupons(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCoupons(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int CouponID = int.Parse(hdnCouponID.Value);
                if (CouponID > 0)
                {
                    LoadDataFromFields(objDBContext.Coupons.FirstOrDefault(obj => obj.CouponID == CouponID));
                }
                else
                {
                    Coupon objCoupon = new Coupon();
                    LoadDataFromFields(objCoupon);
                    objDBContext.Coupons.AddObject(objCoupon);
                    
                }

                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.Coupons);
                if (CouponID < 1)
                {
                    hdnCouponID.Value = objDBContext.Coupons.Max(A => A.CouponID).ToString();
                    LoadEditCouponView();
                }
                lblSuccessMessage.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblErrorMessage.Visible = true;
            }
        }

        private void LoadDataFromFields(Coupon objCoupon)
        {
            try
            {
                objCoupon.CouponCode = txtCouponCode.Text;
                objCoupon.CouponName = txtCouponName.Text;
                objCoupon.CouponTypeID = int.Parse(ddlCouponTypes.SelectedValue);
                objCoupon.Discount = decimal.Parse(txtDiscount.Text);
                objCoupon.ForServiceID = ddlServices.SelectedValue == "0" ? (int?)null : int.Parse(ddlServices.SelectedValue);
                if (pnlCouponCount.Visible == true)
                   objCoupon.CouponCount = Convert.ToInt32(txtCouponCount.Text); 
                else
                    objCoupon.CouponCount = null;
                //objCoupon.CouponType = txtCouponType.Text;

                if (ddlUsers.SelectedValue != Guid.Empty.ToString())
                    objCoupon.ForUser = Guid.Parse(ddlUsers.SelectedValue);
                else
                    objCoupon.ForUser = null;

                objCoupon.IsUsed = chkIsUsed.Checked;
                objCoupon.RecordStatus = int.Parse(ddlRecordStatus.SelectedValue);
                objCoupon.UserCount = txtUserCount.Text.IsValid() ? int.Parse(txtUserCount.Text) : 0;
                if (txtValidityDate.Text.IsValid())
                    objCoupon.ValidityDate = DateTime.Parse(txtValidityDate.Text);
                else
                    objCoupon.ValidityDate = null;

                //objCoupon.ValidityInDays = txtValidityDays.Text.IsValid() ? int.Parse(txtValidityDays.Text) : 0;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void GetServiceCategoryID(int ServiceID)
        {
            try
            {
                //var Category = objDBContext.ServiceCatalogs.Where(cat => cat.ServiceID == ServiceID).ToList();
               var Category = objDBContext.ServiceCatalogs.FirstOrDefault(cat => cat.ServiceID == ServiceID);
               if (Category != null)
               {
                   ddlChooseCategory.SelectedValue = Category.ServiceCategoryID.ToString();
                   ddlChooseCategory_SelectedIndexChanged(null, null);
               }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                int couponID = int.Parse(hdnCouponID.Value);
                if (couponID > 0)
                {
                    LoadFieldsFromData(objDBContext.Coupons.First(obj => obj.CouponID == couponID));
                }
                else
                {
                    ClearFormForNewEntry();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void ddlChooseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelectedCategory = Convert.ToInt32(ddlChooseCategory.SelectedValue);

                var services = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceName).Where(cat => cat.ServiceCategoryID == SelectedCategory).ToList();
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

                //ddlServices.DataSource = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceID).Where(cat => cat.ServiceCategoryID == SelectedCategory);
                //ddlServices.DataTextField = "ServiceName";
                //ddlServices.DataValueField = "ServiceID";
                //ddlServices.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void ddlCouponTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CouponTypeID = Convert.ToInt32(ddlCouponTypes.SelectedValue);
            var CouponTpeDetails = objDBContext.CouponTypes.FirstOrDefault(c => c.CouponTypeID == CouponTypeID);
            if (CouponTpeDetails.IsCouponUnlimited == true)
                pnlCouponCount.Visible = false;
            else
                pnlCouponCount.Visible = true;
        }
    }
}