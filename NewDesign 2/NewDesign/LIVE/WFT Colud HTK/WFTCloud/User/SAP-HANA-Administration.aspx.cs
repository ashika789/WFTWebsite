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

namespace WFTCloud.User
{
    public partial class SAP_HANA_Administration : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string selectedLanguage = "English";
            if (Request.Cookies["LanguageCookie"] != null)
            {
                selectedLanguage = Request.Cookies["LanguageCookie"].Value;
            }
            var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "SAP-HANA-Administration.aspx" && c.Language == selectedLanguage);
            if (Content != null)
            {
                Literal1.Text = Content.HTMLContent;
            }
            else
            {
                var EnglishContent = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == "SAP-HANA-Administration.aspx" && c.Language == "English");
                Literal1.Text = EnglishContent.HTMLContent;
            }
            Response.Cookies["LanguageCookie"].Value = selectedLanguage;
            if (!IsPostBack)
            {
                FillCourseDetails();
            }
        }

        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                TrainingRequestDetail objTrainingRegister = new TrainingRequestDetail();
                string UserName = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;
                string UserEmailID = txtEmailID.Text;
                string Course = ddlCourse.SelectedItem.Text;
                string Module = ddlModule.SelectedItem.Text;
                string ContactNumber = txtContactNumber.Text;
                string Location = "City : " + txtCity.Text + ", State : " + txtState.Text + ", Country : " + txtCountry.Text;
                string Know = ddlHearAboutUs.SelectedItem.Text;
                string Comments = txtComments.Text;

                objTrainingRegister.FirstName = txtFirstName.Text;
                objTrainingRegister.MiddleName = txtMiddleName.Text;
                objTrainingRegister.LastName = txtLastName.Text;
                objTrainingRegister.PhoneNumber = txtContactNumber.Text;
                objTrainingRegister.Email = txtEmailID.Text;
                objTrainingRegister.Course = ddlCourse.SelectedItem.Text;
                objTrainingRegister.Module = ddlModule.SelectedItem.Text;
                objTrainingRegister.Location = "City : " + txtCity.Text + ", State : " + txtState.Text + ", Country : " + txtCountry.Text;
                objTrainingRegister.SurveyAnswer = ddlHearAboutUs.SelectedValue;
                objTrainingRegister.Comments = txtComments.Text;
                objTrainingRegister.CreatedOn = DateTime.Now;
                objDBContext.TrainingRequestDetails.AddObject(objTrainingRegister);
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.TrainingRequestDetails);
                
                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["TrainingRegister"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                string AdminContent = EmailContent.Replace("++Username++", UserName).Replace("++UserEmailID++", UserEmailID).Replace("++UserCourse++", Course).Replace("++UserModule++", Module).Replace("++contactnumber++", ContactNumber).Replace("++GeographicLocation++", Location).Replace("++Know++", Know).Replace("++Comments++", Comments);
                SMTPManager.SendAdminNotificationEmail(AdminContent, "Training request from " + UserName, false);
                txtComments.Text = txtContactNumber.Text = txtEmailID.Text = txtFirstName.Text = txtLastName.Text = txtMiddleName.Text = string.Empty;
                ddlHearAboutUs.SelectedIndex = ddlCourse.SelectedIndex = ddlModule.SelectedIndex = 0;
                divRegisterSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegisterCode_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeModulenames(Convert.ToInt32(ddlCourse.SelectedValue));

        }
        private void FillCourseDetails()
        {
            ddlCourse.DataSource = objDBContext.TrainingCourses.Where(a => a.RecordStatus == 1).OrderBy(a => a.CourseName);
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "CourseID";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new ListItem("<--Select Course-->", "0"));
        }

        private void ChangeModulenames(int CourseID)
        {
            try
            {
                ddlModule.Items.Clear();
                if (CourseID != 0)
                {
                    var states = objDBContext.TrainingModules.Where(t => t.CourseID == CourseID && t.RecordStatus == 1).OrderBy(A => A.ModuleName);
                    ddlModule.DataSource = states;
                    ddlModule.DataTextField = "ModuleName";
                    ddlModule.DataValueField = "ModuleID";
                    ddlModule.DataBind();

                    if (ddlModule.Items.Count == 0)
                    {
                        ddlModule.Items.Insert(0, new ListItem("N/A", "0"));
                    }

                }
                else
                {
                    ddlModule.Items.Insert(0, new ListItem("N/A", "0"));
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}