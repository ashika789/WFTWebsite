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
    public partial class SAP_training : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //protected void btn2SignUp_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/LoginPage.aspx?ShowView=Register");
        //}

        //protected void btn3SignUp_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/LoginPage.aspx?ShowView=Register");
        //}

        //protected void btn1SignUp_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/LoginPage.aspx?ShowView=Register");
        //}

        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAgree.Checked)
                {
                    var IsUserNameAlreadyExist = Membership.GetUser(txtEmailID.Text);
                    if (IsUserNameAlreadyExist == null)
                    {
                        MembershipUser mUser = Membership.CreateUser(txtEmailID.Text, txtRegPassword.Text,txtEmailID.Text);
                        mUser.IsApproved = true;
                        Membership.UpdateUser(mUser);
                        Roles.AddUserToRole(txtEmailID.Text, "Personal User");
                        UserProfile upf = new UserProfile();
                        upf.FirstName = txtFirstName.Text;
                        upf.MiddleName = txtMiddleName.Text;
                        upf.LastName = txtLastName.Text;
                        upf.UserMembershipID = Guid.Parse(mUser.ProviderUserKey.ToString());
                        upf.RecordStatus = DBKeys.RecordStatus_Active;
                        upf.EmailID = txtEmailID.Text;
                        upf.CreatedOn = DateTime.Now;
                        upf.UserRole = "Personal User";
                        upf.Location = ddlCountry.SelectedValue;
                        objDBContext.UserProfiles.AddObject(upf);

                        UserSurveyRespons objsr = new UserSurveyRespons();
                        objsr.CreatedBy = Guid.Parse(mUser.ProviderUserKey.ToString());
                        objsr.CreatedOn = DateTime.Now;
                        if (ddlHearAboutUs.SelectedValue == "Others")
                        {
                            objsr.SurveyAnswer = txtOthers.Text;
                        }
                        else
                        {
                            objsr.SurveyAnswer = ddlHearAboutUs.SelectedValue;
                        }
                        objsr.SurveyQuestionID = 1;
                        objDBContext.UserSurveyResponses.AddObject(objsr);

                        objDBContext.SaveChanges();
                        txtFirstName.Text = txtMiddleName.Text = txtLastName.Text = txtRegPassword.Text = txtEmailID.Text = txtConfPassword.Text = txtOthers.Text = string.Empty;
                        divRegisterError.Visible = false;
                        lblRegisterSuccess.Text = "Successfully Registered.";
                        divRegisterSuccess.Visible = true;
                    }
                    else
                    {
                        divRegisterError.Visible = true;
                        lblRegisterError.Text = "User Name already exists. Please register with an alternate email address.";
                        divRegisterSuccess.Visible = false;
                    }
                }
                else
                {
                    divRegisterError.Visible = true;
                    lblRegisterError.Text = "Please accept Terms and Conditions.";
                    divRegisterSuccess.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegister_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}