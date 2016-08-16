using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using System.Web.Security;
using System.IO;

namespace WFTCloud.Admin
{
    public partial class CategoriesList : System.Web.UI.Page
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

                        if (!IsPostBack)
                        {
                            //Check if the screen should load edit category from query string parameter.
                            LoadEditCategory();
                            //Check if the screen should delete any category from query string parameter.
                            UpdateCategoryStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any category from query string parameter.
                            UpdateCategoryStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any category from query string parameter.
                            UpdateCategoryStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                            //Show records based on pagination value and deleted flag.
                            ShowPaginatedAndDeletedRecords();
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

        private void ClearLabels()
        {
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            divCatErrorMessage.Visible = false;
            divCatSuccessMessage.Visible = false;
        }

        #endregion

        #region Control Events

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryID = int.Parse(lblCategoryID.Text);
                var selCategory = GetCategoryFromID(categoryID);
                //If new category then add to dbcontext
                if (selCategory == null)
                {
                    ServiceCategory objCategory = new ServiceCategory();
                    LoadDataFromFields(objCategory);
                    //if (objDBContext.ServiceCategories.Any(ot => ot.CategoryPriority == objCategory.CategoryPriority && objCategory.RecordStatus == DBKeys.RecordStatus_Active))
                    //{
                    //    lblErrorMessage.Visible = true;
                    //    lblErrorMessageText.Text = "Category Priority Already assigned.";
                    //    return;
                    //}
                    //else
                    //{
                        objDBContext.ServiceCategories.AddObject(objCategory);
                        objDBContext.SaveChanges();
                        lblCategoryID.Text = objCategory.ServiceCategoryID.ToString();
                }
                else
                {
                    //LoadDataFromFields(selCategory);
                    if (ddlCategoryPriority.SelectedValue == null || ddlCategoryPriority.SelectedValue == "0")
                    {
                        selCategory.CategoryPriority = null;
                    }
                    else
                    {
                        selCategory.CategoryPriority = Convert.ToInt32(ddlCategoryPriority.SelectedValue);
                    }
                    selCategory.CategoryName = txtCategoryName.Text;
                    selCategory.CategoryPriority = int.Parse(ddlCategoryPriority.SelectedValue.ToString());
                    selCategory.RecordStatus = int.Parse(ddlRecordStatus.SelectedValue);
                    selCategory.IsPayAsYouGo = chkIsPayAsYouGo.Checked;
                    selCategory.IsDedicated = Convert.ToBoolean(rblDedicatedServices.SelectedValue);
                    //if (objDBContext.ServiceCategories.Any(ot => ot.CategoryPriority == selCategory.CategoryPriority && selCategory.RecordStatus == DBKeys.RecordStatus_Active))
                    //{
                    //    lblErrorMessage.Visible = true;
                    //    lblErrorMessageText.Text = "Category Priority Already assigned.";
                    //    return;
                    //}
                    if (chkIsPayAsYouGo.Checked == false)
                    {
                        int CategoryID = Convert.ToInt32(lblCategoryID.Text);
                        var ServiceDetails = objDBContext.ServiceCatalogs.Where(c => c.ServiceCategoryID == CategoryID);
                        if (ServiceDetails != null)
                        {
                            foreach (ServiceCatalog Cat in ServiceDetails)
                            {
                                var Service = objDBContext.ServiceCatalogs.FirstOrDefault(ser => ser.ServiceID == Cat.ServiceID);
                                Service.IsPayAsYouGo = false;
                            }
                        }
                    }

                    objDBContext.SaveChanges();
                }

                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.ServiceCategories);

                lblSuccessMessage.Visible = true;

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblErrorMessage.Visible = true;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            int CategoryID=int.Parse(lblCategoryID.Text);
            if (CategoryID == 0)
            {
                txtCategoryName.Text = string.Empty;
                ddlCategoryPriority.SelectedValue = "1";
                ddlRecordStatus.SelectedValue = "1";
                chkIsPayAsYouGo.Checked = false;
            }
            else
            {
                LoadFieldsFromData(GetCategoryFromID(CategoryID));
            }
            lblSuccessMessage.Visible = lblErrorMessage.Visible = false;
           
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriesList.aspx?editcategoryid=0");
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void showDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        private void LoadEditCategory()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCategoryID].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int categoryID = int.Parse(Request.QueryString[QueryStringKeys.EditCategoryID]);
                    var selCategory = objDBContext.ServiceCategories.FirstOrDefault(cat => cat.ServiceCategoryID == categoryID);
                    if (selCategory != null)
                    {
                        int CategoriesCount = objDBContext.ServiceCategories.Count();
                        ddlCategoryPriority.DataSource = Enumerable.Range(0, CategoriesCount + 1);
                        ddlCategoryPriority.DataBind();
                        if (selCategory.CategoryPriority != null || selCategory.CategoryPriority != 0)
                        {
                            ddlCategoryPriority.SelectedValue = Convert.ToString(selCategory.CategoryPriority);
                        }
                        else
                        {
                            ddlCategoryPriority.SelectedValue = "0";
                        }
                        lblHeaderCategory.Text = "Edit Category Details";
                        lblCategoryID.Text = selCategory.ServiceCategoryID.ToString();
                        hdnCategoryID.Value = selCategory.ServiceCategoryID.ToString();
                        txtCategoryName.Text = selCategory.CategoryName;
                        ddlCategoryPriority.SelectedValue = selCategory.CategoryPriority.ToString();
                        ddlRecordStatus.SelectedValue = selCategory.RecordStatus.ToString();
                        chkIsPayAsYouGo.Checked = selCategory.IsPayAsYouGo ?? false;
                        rblDedicatedServices.SelectedValue = selCategory.IsDedicated == true ? "true" : "false";
                    }
                    else
                    {
                        int CategoriesCount = objDBContext.ServiceCategories.Count();
                        ddlCategoryPriority.DataSource = Enumerable.Range(1, CategoriesCount + 1);
                        ddlCategoryPriority.DataBind();
                        lblHeaderCategory.Text = "New Category Details";
                        lblCategoryID.Text = "0";
                        txtCategoryName.Text = string.Empty;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region Resuable Routines

        private void ActivateDeactivateCategories(int categoryStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCategory.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnCategoryID = (rItem.FindControl("hdnCategoryID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCategory = GetCategoryFromID(int.Parse(hdnCategoryID.Value));
                        if (selCategory.IsNotNull())
                        {
                            selCategory.RecordStatus = categoryStatus;
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

        private void UpdateCategoryStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int categoryID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetCategoryFromID(categoryID);
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

        private void LoadDataFromFields(ServiceCategory objCategory)
        {
            objCategory.CategoryName = txtCategoryName.Text;
            objCategory.CategoryPriority = int.Parse(ddlCategoryPriority.SelectedValue.ToString());
            objCategory.RecordStatus = int.Parse(ddlRecordStatus.SelectedValue);
            objCategory.IsPayAsYouGo = chkIsPayAsYouGo.Checked;
            objCategory.IsDedicated = Convert.ToBoolean(rblDedicatedServices.SelectedValue);
        }

        private void LoadFieldsFromData(ServiceCategory objCategory)
        {
            txtCategoryName.Text = objCategory.CategoryName;
            ddlCategoryPriority.SelectedValue = objCategory.CategoryPriority.ToString();
            ddlRecordStatus.SelectedValue = objCategory.RecordStatus.ToString();
            chkIsPayAsYouGo.Checked = objCategory.IsPayAsYouGo ?? false;
        }

        private ServiceCategory GetCategoryFromID(int categoryID)
        {
            return objDBContext.ServiceCategories.FirstOrDefault(cat => cat.ServiceCategoryID == categoryID);
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    var boundCategories = objDBContext.ServiceCategories.OrderBy(obj => obj.ServiceCategoryID);
                    rptrCategory.DataSource = boundCategories;
                    rptrCategory.DataBind();
                }
                else
                {
                    var boundCategories = objDBContext.ServiceCategories.OrderBy(obj => obj.ServiceCategoryID).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrCategory.DataSource = boundCategories;
                    rptrCategory.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

    }
}