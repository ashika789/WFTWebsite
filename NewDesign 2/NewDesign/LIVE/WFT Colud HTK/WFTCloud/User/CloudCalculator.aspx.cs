using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Net.Mail;

namespace WFTCloud.User
{
    public partial class CloudCalculator : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FirstGridViewRow();
                SecondGridViewRow();

                if (Request.QueryString["QuoteReferenceID"] != null)
                {
                    GetComputeData();
                    GetStorageData();
                    LoadEditQuoteView();
                }
               
                if (Request.QueryString["FirstName"] != null)
                {
                    txtFirstName.Text = Request.QueryString["FirstName"];
                    txtLastName.Text = Request.QueryString["LastName"];
                    txtCompanyName.Text = Request.QueryString["CompanyName"];
                    txtEmailID.Text = Request.QueryString["EmailID"];
                    txtTelephone.Text = Request.QueryString["Telephone"];
                }
            }

           
            string selectedLanguage = "English";
            if (Request.Cookies["LanguageCookie"] != null)
            {
                selectedLanguage = Request.Cookies["LanguageCookie"].Value;
            }
            var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "InfraOffering.aspx" && c.Language == selectedLanguage);
            if (Content != null)
            {
                //Literal1.Text = Content.HTMLContent;
            }
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "InfraOffering.aspx" && c.Language == "English");
                //Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;

           
        }



        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("ServerName", typeof(string)));
            dt.Columns.Add(new DataColumn("OS", typeof(string)));
            dt.Columns.Add(new DataColumn("CPU", typeof(string)));
            dt.Columns.Add(new DataColumn("Memory", typeof(string)));
            dt.Columns.Add(new DataColumn("SAPS", typeof(string)));
            dt.Columns.Add(new DataColumn("Storage", typeof(string)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("HA", typeof(string)));
            dt.Columns.Add(new DataColumn("SR", typeof(string)));
            dt.Columns.Add(new DataColumn("BK", typeof(string)));
            dr = dt.NewRow();
            dr["ServerName"] = string.Empty;
            dr["OS"] = string.Empty;
            dr["CPU"] = string.Empty;
            dr["Memory"] = string.Empty;
            dr["SAPS"] = string.Empty;
            dr["Storage"] = string.Empty;
            dr["Type"] = string.Empty;
            dr["HA"] = string.Empty;
            dr["SR"] = string.Empty;
            dr["BK"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;


            grvComputDetails.DataSource = dt;
            grvComputDetails.DataBind();



        }
        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox ServerName = (TextBox)grvComputDetails.Rows[rowIndex].Cells[1].FindControl("txtServerName");
                        DropDownList OS = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[2].FindControl("drpOS");
                        DropDownList CPU = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[3].FindControl("drpCPU");
                        DropDownList Memory = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[4].FindControl("drpMemory");
                        TextBox SAPS = (TextBox)grvComputDetails.Rows[rowIndex].Cells[5].FindControl("txtSAPS");
                        TextBox Storage = (TextBox)grvComputDetails.Rows[rowIndex].Cells[6].FindControl("txtStorage");
                        DropDownList Type = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[7].FindControl("drpType");
                        CheckBox HA = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[8].FindControl("chkHA");
                        CheckBox SR = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[9].FindControl("chkSR");
                        CheckBox BK = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[10].FindControl("chkBK");
                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ServerName"] = ServerName.Text;
                        dtCurrentTable.Rows[i - 1]["OS"] = OS.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["CPU"] = CPU.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Memory"] = Memory.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["SAPS"] = SAPS.Text;
                        dtCurrentTable.Rows[i - 1]["Storage"] = Storage.Text;
                        dtCurrentTable.Rows[i - 1]["Type"] = Type.SelectedValue;

                       
                        dtCurrentTable.Rows[i - 1]["HA"] = HA.Checked;
                        dtCurrentTable.Rows[i - 1]["SR"] = SR.Checked;
                        dtCurrentTable.Rows[i - 1]["BK"] = BK.Checked;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvComputDetails.DataSource = dtCurrentTable;
                    grvComputDetails.DataBind();

                   
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox ServerName = (TextBox)grvComputDetails.Rows[rowIndex].Cells[1].FindControl("txtServerName");
                        DropDownList OS = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[2].FindControl("drpOS");
                        DropDownList CPU = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[3].FindControl("drpCPU");
                        DropDownList Memory = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[4].FindControl("drpMemory");
                        TextBox SAPS = (TextBox)grvComputDetails.Rows[rowIndex].Cells[5].FindControl("txtSAPS");
                        TextBox Storage = (TextBox)grvComputDetails.Rows[rowIndex].Cells[6].FindControl("txtStorage");
                        DropDownList Type = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[7].FindControl("drpType");
                        CheckBox HA = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[8].FindControl("chkHA");
                        CheckBox SR = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[9].FindControl("chkSR");
                        CheckBox BK = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[10].FindControl("chkBK");
                        // drCurrentRow["RowNumber"] = i + 1;

                        //grvComputDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        ServerName.Text = dt.Rows[i]["ServerName"].ToString();
                        OS.SelectedValue = dt.Rows[i]["OS"].ToString();
                        CPU.SelectedValue = dt.Rows[i]["CPU"].ToString();
                        Memory.SelectedValue = dt.Rows[i]["Memory"].ToString();
                        SAPS.Text = dt.Rows[i]["SAPS"].ToString();
                        Storage.Text = dt.Rows[i]["Storage"].ToString();
                        Type.SelectedValue = dt.Rows[i]["Type"].ToString();

                        if (dt.Rows[i]["HA"].ToString() == "True")
                        {
                            HA.Checked = true;
                        }
                        else
                        {
                            HA.Checked = false;
                        }
                        if (dt.Rows[i]["SR"].ToString() == "True")
                        {
                            SR.Checked = true;
                        }
                        else
                        {
                            SR.Checked = false;
                        }
                        if (dt.Rows[i]["BK"].ToString() == "True")
                        {
                            BK.Checked = true;
                        }
                        else
                        {
                            BK.Checked = false;
                        }

                        rowIndex++;
                    }
                }
            }
        }
        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox ServerName = (TextBox)grvComputDetails.Rows[rowIndex].Cells[1].FindControl("txtServerName");
                        DropDownList OS = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[2].FindControl("drpOS");
                        DropDownList CPU = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[3].FindControl("drpCPU");
                        DropDownList Memory = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[4].FindControl("drpMemory");
                        TextBox SAPS = (TextBox)grvComputDetails.Rows[rowIndex].Cells[5].FindControl("txtSAPS");
                        TextBox Storage = (TextBox)grvComputDetails.Rows[rowIndex].Cells[6].FindControl("txtStorage");
                        DropDownList Type = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[7].FindControl("drpType");
                        CheckBox HA = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[8].FindControl("chkHA");
                        CheckBox SR = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[9].FindControl("chkSR");
                        CheckBox BK = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[10].FindControl("chkBK");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["ServerName"] = ServerName.Text;
                        dtCurrentTable.Rows[i - 1]["OS"] = OS.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["CPU"] = CPU.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Memory"] = Memory.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["SAPS"] = SAPS.Text;
                        dtCurrentTable.Rows[i - 1]["Storage"] = Storage.Text;
                        dtCurrentTable.Rows[i - 1]["Type"] = Type.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["HA"] = HA.Checked;
                        dtCurrentTable.Rows[i - 1]["SR"] = SR.Checked;
                        dtCurrentTable.Rows[i - 1]["BK"] = BK.Checked;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }


        private void SecondGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
          
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("VolumeType", typeof(string)));
            dt.Columns.Add(new DataColumn("Volumes", typeof(string)));
            dt.Columns.Add(new DataColumn("Storage", typeof(string)));
            dt.Columns.Add(new DataColumn("IOPS", typeof(string)));
            dt.Columns.Add(new DataColumn("Snapshot", typeof(string)));
           
            dr = dt.NewRow();
           
            dr["Description"] = string.Empty;
            dr["VolumeType"] = string.Empty;
            dr["Volumes"] = string.Empty;
            dr["Storage"] = string.Empty;
            dr["IOPS"] = string.Empty;
            dr["Snapshot"] = string.Empty;
           
            dt.Rows.Add(dr);

            ViewState["SecondTable"] = dt;


            grvDescriptionDetails.DataSource = dt;
            grvDescriptionDetails.DataBind();


        }
        private void AddSecondNewRow()
        {
            int rowIndex = 0;

            if (ViewState["SecondTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SecondTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox Description = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)grvDescriptionDetails.Rows[rowIndex].Cells[2].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[3].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[4].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[5].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[6].FindControl("txtSnapshot");
                        drCurrentRow = dtCurrentTable.NewRow();
                     
                        dtCurrentTable.Rows[i - 1]["Description"] = Description.Text;
                        dtCurrentTable.Rows[i - 1]["VolumeType"] = VolumeType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Volumes"] = Volumes.Text;
                        dtCurrentTable.Rows[i - 1]["Storage"] = ServerStorage.Text;
                        dtCurrentTable.Rows[i - 1]["IOPS"] = IOPS.Text;
                        dtCurrentTable.Rows[i - 1]["Snapshot"] = Snapshot.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["SecondTable"] = dtCurrentTable;

                    grvDescriptionDetails.DataSource = dtCurrentTable;
                    grvDescriptionDetails.DataBind();

                }
            }
            else
            {
                Response.Write("No data");
            }
            SetSecondPreviousData();
        }
        private void SetSecondPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["SecondTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["SecondTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox Description = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)grvDescriptionDetails.Rows[rowIndex].Cells[2].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[3].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[4].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[5].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[6].FindControl("txtSnapshot");
                       
                        Description.Text = dt.Rows[i]["Description"].ToString();
                        VolumeType.SelectedValue = dt.Rows[i]["VolumeType"].ToString();
                        Volumes.Text = dt.Rows[i]["Volumes"].ToString();
                        ServerStorage.Text = dt.Rows[i]["Storage"].ToString();
                        IOPS.Text = dt.Rows[i]["IOPS"].ToString();
                        Snapshot.Text = dt.Rows[i]["Snapshot"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

       
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnDescAddition_Click(object sender, EventArgs e)
        {
            AddSecondNewRow();
        }

        protected void grvComputDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    grvComputDetails.DataSource = dt;
                    grvComputDetails.DataBind();

                    SetPreviousData();
                }
            }
        }
        
        protected void grvDescriptionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetSecondRowData();
            if (ViewState["SecondTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["SecondTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["SecondTable"] = dt;
                    grvDescriptionDetails.DataSource = dt;
                    grvDescriptionDetails.DataBind();

                    SetSecondPreviousData();
                }
            }
        }

        private void SetSecondRowData()
        {
            int rowIndex = 0;

            if (ViewState["SecondTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SecondTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox Description = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)grvDescriptionDetails.Rows[rowIndex].Cells[2].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[3].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[4].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[5].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[6].FindControl("txtSnapshot");
                        drCurrentRow = dtCurrentTable.NewRow();
                       
                        dtCurrentTable.Rows[i - 1]["Description"] = Description.Text;
                        dtCurrentTable.Rows[i - 1]["VolumeType"] = VolumeType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Volumes"] = Volumes.Text;
                        dtCurrentTable.Rows[i - 1]["Storage"] = ServerStorage.Text;
                        dtCurrentTable.Rows[i - 1]["IOPS"] = IOPS.Text;
                        dtCurrentTable.Rows[i - 1]["Snapshot"] = Snapshot.Text;
                        rowIndex++;
                    }

                    ViewState["SecondTable"] = dtCurrentTable;
                    
                }
            }
            else
            {
                Response.Write("No data available");
            }
            //SetPreviousData();
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string QuoteReferenceURL= string.Empty;
                string QuoteRefID = Request.QueryString["QuoteReferenceID"];
                SetRowData();
                SetSecondRowData();
                DataTable table = ViewState["CurrentTable"] as DataTable;
                DataTable Descriptiontable = ViewState["SecondTable"] as DataTable;

                string UserCompanyName = txtCompanyName.Text;
                string UserName = txtFirstName.Text + " " + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtTelephone.Text;


                if (QuoteRefID != null)
                {
                    QuoteReferenceURL = QuoteRefID;
                    var QuoteRegisters = objDBContext.QuoteRegisters.FirstOrDefault(q => q.QuoteReferenceID == QuoteRefID);

                    QuoteRegister objQuoteRegister = new QuoteRegister();
                    objQuoteRegister.FirstName = txtFirstName.Text;
                    objQuoteRegister.LastName = txtLastName.Text;
                    objQuoteRegister.PhoneNumber = txtTelephone.Text;
                    objQuoteRegister.Email = txtEmailID.Text;
                    objQuoteRegister.CompanyName = txtCompanyName.Text;
                    objQuoteRegister.EstimatedBandwidth = txtEstimatedBandwidth.Text;
                    objQuoteRegister.BasisSupport = rdbSupportType.SelectedValue;
                    objQuoteRegister.CreatedOn = DateTime.Now;
                    objQuoteRegister.QuoteReferenceID = QuoteRefID;
                    objDBContext.SaveChanges();
                    objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.QuoteRegisters);

                    objDBContext.pr_DeleteExistingQuoteRegisterDetails(QuoteRegisters.QuoteRegisterID);

                    

                    var objCompute = grvComputDetails;
                    for (int i = 0; i < grvComputDetails.Rows.Count; i++)
                    {

                        QuoteComputeDetail objQuoteCompute = new QuoteComputeDetail();
                        TextBox ServerName = (TextBox)objCompute.Rows[i].FindControl("txtServerName");
                        DropDownList OS = (DropDownList)objCompute.Rows[i].FindControl("drpOS");
                        DropDownList CPU = (DropDownList)objCompute.Rows[i].FindControl("drpCPU");
                        DropDownList Memory = (DropDownList)objCompute.Rows[i].FindControl("drpMemory");
                        TextBox SAPS = (TextBox)objCompute.Rows[i].FindControl("txtSAPS");
                        TextBox Storage = (TextBox)objCompute.Rows[i].FindControl("txtStorage");
                        DropDownList Type = (DropDownList)objCompute.Rows[i].FindControl("drpType");
                        CheckBox HA = (CheckBox)objCompute.Rows[i].FindControl("chkHA");
                        CheckBox SR = (CheckBox)objCompute.Rows[i].FindControl("chkSR");
                        CheckBox BK = (CheckBox)objCompute.Rows[i].FindControl("chkBK");


                        objQuoteCompute.QuoteRegisterID = QuoteRegisters.QuoteRegisterID;
                        objQuoteCompute.ServerName = ServerName.Text;
                        objQuoteCompute.OS = OS.SelectedValue;
                        objQuoteCompute.CPU = CPU.SelectedValue;
                        objQuoteCompute.Memory = Memory.SelectedValue;
                        objQuoteCompute.SAPS = SAPS.Text;
                        objQuoteCompute.Storage = Storage.Text;
                        objQuoteCompute.Type = Type.SelectedValue;
                        objQuoteCompute.HA = HA.Checked;
                        objQuoteCompute.SR = SR.Checked;
                        objQuoteCompute.BK = BK.Checked;
                        objQuoteCompute.QuoteReferenceID = QuoteRefID;
                        objQuoteCompute.CreatedOn = DateTime.Now;
                        objDBContext.QuoteComputeDetails.AddObject(objQuoteCompute);
                        objDBContext.SaveChanges();
                    }


                    var objDescription = grvDescriptionDetails;
                    for (int i = 0; i < grvDescriptionDetails.Rows.Count; i++)
                    {
                        QuoteStorageDetail objStorage = new QuoteStorageDetail();
                        TextBox Description = (TextBox)objDescription.Rows[i].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)objDescription.Rows[i].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)objDescription.Rows[i].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)objDescription.Rows[i].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)objDescription.Rows[i].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)objDescription.Rows[i].FindControl("txtSnapshot");

                        objStorage.QuoteRegisterID = QuoteRegisters.QuoteRegisterID;
                        objStorage.Description = Description.Text;
                        objStorage.VolumeType = VolumeType.SelectedValue;
                        objStorage.Volumes = Volumes.Text;
                        objStorage.Storage = ServerStorage.Text;
                        objStorage.IOPS = IOPS.Text;
                        objStorage.Snapshot = Snapshot.Text;
                        objStorage.QuoteReferenceID = QuoteRefID;
                        objStorage.CreatedOn = DateTime.Now;
                        objDBContext.QuoteStorageDetails.AddObject(objStorage);
                        objDBContext.SaveChanges();
                    }
                }
                else
                {
                    QuoteRegister objQuoteRegister = new QuoteRegister();


                    int QuoteRegisterID = 0;

                    Guid QuoteReferenceID = Guid.NewGuid();

                    QuoteReferenceURL = QuoteReferenceID.ToString();

                    objQuoteRegister.FirstName = txtFirstName.Text;
                    objQuoteRegister.LastName = txtLastName.Text;
                    objQuoteRegister.PhoneNumber = txtTelephone.Text;
                    objQuoteRegister.Email = txtEmailID.Text;
                    objQuoteRegister.CompanyName = txtCompanyName.Text;
                    objQuoteRegister.EstimatedBandwidth = txtEstimatedBandwidth.Text;
                    objQuoteRegister.BasisSupport = rdbSupportType.SelectedValue;
                    objQuoteRegister.CreatedOn = DateTime.Now;
                    objQuoteRegister.QuoteReferenceID = QuoteReferenceID.ToString();
                    objDBContext.QuoteRegisters.AddObject(objQuoteRegister);
                    objDBContext.SaveChanges();

                    QuoteRegisterID = objQuoteRegister.QuoteRegisterID;

                    var objCompute = grvComputDetails;
                    for (int i = 0; i < grvComputDetails.Rows.Count; i++)
                    {

                        QuoteComputeDetail objQuoteCompute = new QuoteComputeDetail();
                        TextBox ServerName = (TextBox)objCompute.Rows[i].FindControl("txtServerName");
                        DropDownList OS = (DropDownList)objCompute.Rows[i].FindControl("drpOS");
                        DropDownList CPU = (DropDownList)objCompute.Rows[i].FindControl("drpCPU");
                        DropDownList Memory = (DropDownList)objCompute.Rows[i].FindControl("drpMemory");
                        TextBox SAPS = (TextBox)objCompute.Rows[i].FindControl("txtSAPS");
                        TextBox Storage = (TextBox)objCompute.Rows[i].FindControl("txtStorage");
                        DropDownList Type = (DropDownList)objCompute.Rows[i].FindControl("drpType");
                        CheckBox HA = (CheckBox)objCompute.Rows[i].FindControl("chkHA");
                        CheckBox SR = (CheckBox)objCompute.Rows[i].FindControl("chkSR");
                        CheckBox BK = (CheckBox)objCompute.Rows[i].FindControl("chkBK");


                        objQuoteCompute.QuoteRegisterID = QuoteRegisterID;
                        objQuoteCompute.ServerName = ServerName.Text;
                        objQuoteCompute.OS = OS.SelectedValue;
                        objQuoteCompute.CPU = CPU.SelectedValue;
                        objQuoteCompute.Memory = Memory.SelectedValue;
                        objQuoteCompute.SAPS = SAPS.Text;
                        objQuoteCompute.Storage = Storage.Text;
                        objQuoteCompute.Type = Type.SelectedValue;
                        objQuoteCompute.HA = HA.Checked;
                        objQuoteCompute.SR = SR.Checked;
                        objQuoteCompute.BK = BK.Checked;
                        objQuoteCompute.QuoteReferenceID = QuoteReferenceID.ToString();
                        objQuoteCompute.CreatedOn = DateTime.Now;
                        objDBContext.QuoteComputeDetails.AddObject(objQuoteCompute);
                        objDBContext.SaveChanges();
                    }


                    var objDescription = grvDescriptionDetails;
                    for (int i = 0; i < grvDescriptionDetails.Rows.Count; i++)
                    {
                        QuoteStorageDetail objStorage = new QuoteStorageDetail();
                        TextBox Description = (TextBox)objDescription.Rows[i].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)objDescription.Rows[i].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)objDescription.Rows[i].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)objDescription.Rows[i].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)objDescription.Rows[i].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)objDescription.Rows[i].FindControl("txtSnapshot");

                        objStorage.QuoteRegisterID = QuoteRegisterID;
                        objStorage.Description = Description.Text;
                        objStorage.VolumeType = VolumeType.SelectedValue;
                        objStorage.Volumes = Volumes.Text;
                        objStorage.Storage = ServerStorage.Text;
                        objStorage.IOPS = IOPS.Text;
                        objStorage.Snapshot = Snapshot.Text;
                        objStorage.QuoteReferenceID = QuoteReferenceID.ToString();
                        objStorage.CreatedOn = DateTime.Now;
                        objDBContext.QuoteStorageDetails.AddObject(objStorage);
                        objDBContext.SaveChanges();
                    }
                }
                


                StringBuilder strBuilder = new StringBuilder();

                strBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' width='100%' ");
                strBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");

                strBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myColumn in table.Columns)
                {
                    strBuilder.Append("<td align='left' valign='top'>");
                    strBuilder.Append(myColumn.ColumnName);
                    strBuilder.Append("</td>");
                }

                strBuilder.Append("</tr>");

                foreach (DataRow myRow in table.Rows)
                {
                    strBuilder.Append("<tr align='left' valign='top'>");

                    foreach (DataColumn myColumn in table.Columns)
                    {
                        strBuilder.Append("<td align='left' valign='top'>");
                        strBuilder.Append(myRow[myColumn.ColumnName].ToString());
                        strBuilder.Append("</td>");
                    }

                    strBuilder.Append("</tr>");
                }

                strBuilder.Append("</table>");

                strBuilder.Replace("True", "Yes").Replace("False", "No");

                StringBuilder DescstrBuilder = new StringBuilder();

                DescstrBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' width='100%' ");
                DescstrBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");

                DescstrBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myDescColumn in Descriptiontable.Columns)
                {
                    DescstrBuilder.Append("<td align='left' valign='top'>");
                    DescstrBuilder.Append(myDescColumn.ColumnName);
                    DescstrBuilder.Append("</td>");
                }

                DescstrBuilder.Append("</tr>");

                foreach (DataRow myDescRow in Descriptiontable.Rows)
                {
                    DescstrBuilder.Append("<tr align='left' valign='top'>");

                    foreach (DataColumn myDescColumn in Descriptiontable.Columns)
                    {
                        DescstrBuilder.Append("<td align='left' valign='top'>");
                        DescstrBuilder.Append(myDescRow[myDescColumn.ColumnName].ToString());
                        DescstrBuilder.Append("</td>");
                    }

                    DescstrBuilder.Append("</tr>");
                }

                DescstrBuilder.Append("</table>");

                string QuoteURL = "<a href=" + "http://wftcloud.com/user/CloudCalculator.aspx?QuoteReferenceID=" + QuoteReferenceURL + ">Click on this Link</a>";

                string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudCalculatorToCustomer"]));
                CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);

                CustomerEmailContent = CustomerEmailContent.Replace("++UserName++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++UserCompanyName++", UserCompanyName).Replace("++CalculatorResults++", strBuilder.ToString()).Replace("++DescriptionResults++", DescstrBuilder.ToString()).Replace("++EstimatedBandwidth++", txtEstimatedBandwidth.Text).Replace("++SupportType++", rdbSupportType.SelectedValue).Replace("++QuoteURL++", QuoteURL);
                SMTPManager.SendEmail(CustomerEmailContent, "SAP Landscape Information Received - WFT Cloud", UserEmailID, false, true);
                    
                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudCalculator"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++UserCompanyName++", UserCompanyName).Replace("++CalculatorResults++", strBuilder.ToString()).Replace("++DescriptionResults++", DescstrBuilder.ToString()).Replace("++EstimatedBandwidth++", txtEstimatedBandwidth.Text).Replace("++SupportType++", rdbSupportType.SelectedValue).Replace("++QuoteURL++", QuoteURL);
                SendSAPCalcEmailToTechnicalTeam(AdminContent, "WFT Cloud SAP Calc request from " + UserName, false);

                divRegisterSuccess.Visible = true;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnViewSample_Click(object sender, EventArgs e)
        {
            try
            {
                QuoteRegister objQuoteRegister = new QuoteRegister();
                string UserCompanyName = txtCompanyName.Text;
                string UserName = txtFirstName.Text + " " + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string ContactNumber = txtTelephone.Text;

                

                objQuoteRegister.FirstName = txtFirstName.Text;
                objQuoteRegister.LastName = txtLastName.Text;
                objQuoteRegister.PhoneNumber = txtTelephone.Text;
                objQuoteRegister.Email = txtEmailID.Text;
                objQuoteRegister.CompanyName = txtCompanyName.Text;
                objDBContext.QuoteRegisters.AddObject(objQuoteRegister);
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.QuoteRegisters);

                string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudAnalyticsToCustomer"]));
                CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);

                CustomerEmailContent = CustomerEmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++SAPModules++", "").Replace("++UserCompanyName++", UserCompanyName).Replace("++Landscape++", "");
                SMTPManager.SendEmail(CustomerEmailContent, "SAP Landscape Information Received - WFT Cloud", UserEmailID, false, true);

                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CloudAnalytics"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++contactnumber++", ContactNumber).Replace("++SAPModules++", "").Replace("++UserCompanyName++", UserCompanyName).Replace("++Landscape++", "");
                SendSAPCalcEmailToTechnicalTeam(AdminContent, "SAP to Cloud migration request from " + UserName, false);
               divRegisterSuccess.Visible = true;

               

                string CloudAnalyticsQuote = System.Configuration.ConfigurationManager.AppSettings["CloudAnalyticsQuote"];
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=SampleWFTQuote.pdf");
                Response.TransmitFile(Server.MapPath(CloudAnalyticsQuote));
                Response.End();
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/user/CloudAnalytics.aspx', '_new');", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void DeleteLastRowFromComputDetails()
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                Int32 index = grvComputDetails.Rows.Count - 1;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[index]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    grvComputDetails.DataSource = dt;
                    grvComputDetails.DataBind();

                    SetPreviousData();
                }
            }
        }
        private void DeleteLastRowFromDescriptionDetails()
        {
            SetSecondRowData();
            if (ViewState["SecondTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["SecondTable"];
                DataRow drCurrentRow = null;
                Int32 index = grvDescriptionDetails.Rows.Count - 1;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[index]);
                    drCurrentRow = dt.NewRow();
                    ViewState["SecondTable"] = dt;
                    grvDescriptionDetails.DataSource = dt;
                    grvDescriptionDetails.DataBind();

                    SetSecondPreviousData();
                }
            }
        }


        private void GetComputeData()
        {
            int rowIndex = 0;

            string QuoteRefID = Request.QueryString["QuoteReferenceID"];
            if (QuoteRefID != null)
            {
                var QuoteRegisters = objDBContext.QuoteRegisters.FirstOrDefault(q => q.QuoteReferenceID == QuoteRefID);
                string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                string ComputeDetailQuery = "select ServerName,OS,CPU,Memory,SAPS,Storage,Type,HA,SR,BK from QuoteComputeDetails where QuoteRegisterID=" + QuoteRegisters.QuoteRegisterID;
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter ComputeDetailDataAdapter = new SqlDataAdapter(ComputeDetailQuery, connection);
                DataSet Computeds = new DataSet();
                DataSet Descriptionds = new DataSet();
                connection.Open();
                ComputeDetailDataAdapter.Fill(Computeds, "Compute_table");
                connection.Close();


                DataTable dt = Computeds.Tables[0];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AddNewRow();
                            TextBox ServerName = (TextBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("txtServerName");
                            DropDownList OS = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("drpOS");
                            DropDownList CPU = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("drpCPU");
                            DropDownList Memory = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("drpMemory");
                            TextBox SAPS = (TextBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("txtSAPS");
                            TextBox Storage = (TextBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("txtStorage");
                            DropDownList Type = (DropDownList)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("drpType");
                            CheckBox HA = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("chkHA");
                            CheckBox SR = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("chkSR");
                            CheckBox BK = (CheckBox)grvComputDetails.Rows[rowIndex].Cells[i].FindControl("chkBK");

                            ServerName.Text = dt.Rows[i]["ServerName"].ToString();
                            OS.SelectedValue = dt.Rows[i]["OS"].ToString();
                            CPU.SelectedValue = dt.Rows[i]["CPU"].ToString();
                            Memory.SelectedValue = dt.Rows[i]["Memory"].ToString();
                            SAPS.Text = dt.Rows[i]["SAPS"].ToString();
                            Storage.Text = dt.Rows[i]["Storage"].ToString();
                            Type.SelectedValue = dt.Rows[i]["Type"].ToString();

                            if (dt.Rows[i]["HA"].ToString() == "True")
                            {
                                HA.Checked = true;
                            }
                            else
                            {
                                HA.Checked = false;
                            }
                            if (dt.Rows[i]["SR"].ToString() == "True")
                            {
                                SR.Checked = true;
                            }
                            else
                            {
                                SR.Checked = false;
                            }
                            if (dt.Rows[i]["BK"].ToString() == "True")
                            {
                                BK.Checked = true;
                            }
                            else
                            {
                                BK.Checked = false;
                            }

                            rowIndex++;
                        }
                    }
                }
            }
            DeleteLastRowFromComputDetails();
        }

        
        private void GetStorageData()
        {
            string QuoteRefID = Request.QueryString["QuoteReferenceID"];
            if (QuoteRefID != null)
            {
                var QuoteRegisters = objDBContext.QuoteRegisters.FirstOrDefault(q => q.QuoteReferenceID == QuoteRefID);
                string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                string QuoteStorageQuery = "select Description,VolumeType,Volumes,Storage,IOPS,Snapshot from QuoteStorageDetails where QuoteRegisterID=" + QuoteRegisters.QuoteRegisterID;
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter QuoteStorageDataAdapter = new SqlDataAdapter(QuoteStorageQuery, connection);
                DataSet Descriptionds = new DataSet();
                connection.Open();
                QuoteStorageDataAdapter.Fill(Descriptionds, "Description_table");
                connection.Close();

                int rowIndex = 0;

                DataTable Desdt = Descriptionds.Tables[0];
                if (Desdt != null)
                {
                if (Desdt.Rows.Count > 0)
                {
                    for (int i = 0; i < Desdt.Rows.Count; i++)
                    {
                        AddSecondNewRow();
                        TextBox Description = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("txtDescription");
                        DropDownList VolumeType = (DropDownList)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("drpVolumeType");
                        TextBox Volumes = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("txtVolumes");
                        TextBox ServerStorage = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("txtServerStorage");
                        TextBox IOPS = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("txtIOPS");
                        TextBox Snapshot = (TextBox)grvDescriptionDetails.Rows[rowIndex].Cells[i].FindControl("txtSnapshot");

                        Description.Text = Desdt.Rows[i]["Description"].ToString();
                        VolumeType.SelectedValue = Desdt.Rows[i]["VolumeType"].ToString();
                        Volumes.Text = Desdt.Rows[i]["Volumes"].ToString();
                        ServerStorage.Text = Desdt.Rows[i]["Storage"].ToString();
                        IOPS.Text = Desdt.Rows[i]["IOPS"].ToString();
                        Snapshot.Text = Desdt.Rows[i]["Snapshot"].ToString();
                        rowIndex++;
                    }
                    }
                }
            }
            DeleteLastRowFromDescriptionDetails();
        }


        private void LoadEditQuoteView()
        {
            try
            {
                string QuoteReferenceID = Request.QueryString["QuoteReferenceID"];
                if (QuoteReferenceID != null)
                {
                    var QuoteRegisters = objDBContext.QuoteRegisters.FirstOrDefault(q => q.QuoteReferenceID == QuoteReferenceID);
                    QuoteRegister ObjMain = objDBContext.QuoteRegisters.FirstOrDefault(obj => obj.QuoteReferenceID == QuoteRegisters.QuoteReferenceID);
                    LoadFieldsFromData(ObjMain);
                }
                else
                {
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtCompanyName.Text = "";
                    txtEmailID.Text = "";
                    txtTelephone.Text = "";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadFieldsFromData(QuoteRegister Quote)
        {
            try
            {
                txtFirstName.Text = Quote.FirstName;
                txtLastName.Text = Quote.LastName;
                txtCompanyName.Text = Quote.CompanyName.ToString();
                txtEmailID.Text = Quote.Email;
                txtTelephone.Text = Quote.PhoneNumber;
                txtEstimatedBandwidth.Text = Quote.EstimatedBandwidth;
                rdbSupportType.SelectedValue = Quote.BasisSupport;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void SendSAPCalcEmailToTechnicalTeam(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Admin_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail + ",rkumar@wftus.com");
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ReusableRoutines.LogException("SAPCalcEmail", "SendSAPCalcEmailToTechnicalTeam", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
        protected void grvComputDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    
    }
}