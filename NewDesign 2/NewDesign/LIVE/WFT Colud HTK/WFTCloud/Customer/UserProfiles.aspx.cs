﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Customer
{
    public partial class UserProfiles : System.Web.UI.Page
    {
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;
        AuthorizeNet.CustomerGateway objGW;
        #endregion

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool AuNetFlag = bool.Parse(ConfigurationManager.AppSettings["AuNetLiveMode"]);
                AuthorizeNet.ServiceMode objServiceMode = AuNetFlag ? AuthorizeNet.ServiceMode.Live : AuthorizeNet.ServiceMode.Test;
                objGW = new AuthorizeNet.CustomerGateway(ConfigurationManager.AppSettings["AuNetAPILogin"], ConfigurationManager.AppSettings["AuNetTransactionKey"], objServiceMode);
                
                divPasswordErrorMessage.Visible = divPasswordSuccessMessage.Visible = divPaymentErrorMessage.Visible =
                    divPaymentSuccessMessage.Visible = divUserErrorMessage.Visible = divUserSuccessMessage.Visible = false;
                    //divCancelErrorMessage.Visible = divCancelSuccessMessage.Visible = false;
                UserMembershipID = Request.QueryString["userid"];
                if (UserMembershipID != null && UserMembershipID != "")
                {
                    if (!IsPostBack)
                    {
                        //Show Loaded View
                        LoadTabView();
                        int Year = DateTime.Now.Year;
                        ddlExpYear.DataSource = Enumerable.Range(Year, 20);
                        ddlExpYear.DataBind();
                        ddlExpYear.Items.Insert(0, new ListItem("Select", "Select"));
                        Guid ID = new Guid(UserMembershipID);
                        BindPaymentCustomerDetails(ID);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        #endregion
         
        #region Control Events

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                UserMembershipID = Request.QueryString["userid"];
                if (UserMembershipID != null)
                {
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    user.FirstName = txtFirstName.Text;
                    user.MiddleName = txtMiddleName.Text;
                    user.LastName = txtLastName.Text;
                    user.ContactNumber = txtContactNumber.Text;
                    user.PhoneNumber = txtPhoneNumberOff.Text;
                    user.MobileNumber = txtMobileNumberPer.Text;
                    user.MailingAddress = txtMailingAddress.Text;
                    user.CompanyName = txtCompanyName.Text;
                    user.Location = ddlCountry2.SelectedValue;
                    objDBContext.SaveChanges();
                    divUserSuccessMessage.Visible = true;
                    divUserErrorMessage.Visible = false;
                    lblUserSuccess.Text = "User details saved successfully.";
                }
                else
                {
                    divUserSuccessMessage.Visible = false;
                    divUserErrorMessage.Visible = true;
                    lblUserSuccess.Text = "Please Log In and Try again";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divUserSuccessMessage.Visible = false;
                divUserErrorMessage.Visible = true;
                lblUserSuccess.Text = "Error occured while saving user details";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtConformPassword.Text = txtOldPassword.Text = txtNewPassword.Text = "";
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                UserMembershipID = Request.QueryString["userid"];
                if (UserMembershipID != null)
                {
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    MembershipUser MSU = Membership.GetUser(user.EmailID);
                    if (MSU.GetPassword() == txtOldPassword.Text)
                    {
                        MSU.ChangePassword(txtOldPassword.Text, txtConformPassword.Text);
                        Membership.UpdateUser(MSU);
                        divPasswordErrorMessage.Visible = false;
                        divPasswordSuccessMessage.Visible = true;
                        lblPasswordSuccessmsg.Text = "User Password changed successfully.";
                    }
                    else
                    {
                        divPasswordErrorMessage.Visible = true;
                        divPasswordSuccessMessage.Visible = false;
                        lblPasswordErrormsg.Text = "Please Enter Current Password Correctly.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divPasswordErrorMessage.Visible = true;
                divPasswordSuccessMessage.Visible = false;
                lblPasswordErrormsg.Text = "Error occured while saving Password";
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            try
            {
                
                UserMembershipID = Request.QueryString["userid"];
                string CreditCardNo = "";
                string AuthProfileID = "";
                string AuthBillingAddressID = "";
                string AuthPaymentProfileID = "";
                Guid ID = new Guid(UserMembershipID);
                CreditCardNo = dvCardDetails.Visible == true ? txtCreditCardNumber.Text.Substring(txtCreditCardNumber.Text.Length - 4, 4) : ddlExistingCards.SelectedItem.Text.Substring(ddlExistingCards.SelectedItem.Text.Length - 4, 4);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);

                    string UserFullName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                    int UserProfileID = Convert.ToInt32(user.UserProfileID);
                    string ProfileName = "WFTUSR-" + user.UserProfileID;
                    int ExpMonth = Convert.ToInt32(ddlExpMonth.SelectedValue);
                    int ExpYear = Convert.ToInt32(ddlExpYear.SelectedValue);
                    AuthorizeNet.Address BillingAddress = new AuthorizeNet.Address();

                    BillingAddress.City = txtCity.Text;
                    BillingAddress.Country = ddlCountry.SelectedItem.Text;
                    BillingAddress.First = txtNameOnCard.Text;
                    BillingAddress.Last = " ";
                    BillingAddress.Phone = txtContactNumberPtDet.Text;
                    BillingAddress.State = ddlState.SelectedItem.Text;
                    BillingAddress.Street = txtAddress1.Text + "\n" + txtAddress2.Text;

                    BillingAddress.Zip = txtPostalZipCode.Text;
                string DefaultCreditCardDetails ="";
                    bool PaymentprofileExist = false;
                    int PaymentProfileIndex;

                    var CustPayProf = objDBContext.CustomerPaymentProfiles.Where(cpp => cpp.UserProfileID == user.UserProfileID && cpp.Status==true).ToList();

                    if (CustPayProf.Count() == 0)
                    {
                        AuthProfileID = ReusableRoutines.AuthorizeCreateCustomerProfile(user.EmailID, ProfileName, UserFullName);
                        if (AuthProfileID == "Failed")
                        {
                            divPaymentSuccessMessage.Visible = false;
                            divPaymentErrorMessage.Visible = true;
                            lblPaymentErrormsg.Text = "Error occurred while creating payment profile.";
                            return;
                        }
                        AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress, chkDefalutCreditCard.Checked);
                    }
                    else
                    {
                        AuthProfileID = CustPayProf.FirstOrDefault().AuthCustomerProfileID;
                        string DefalutPaymentProfileID = CustPayProf.FirstOrDefault(A=>A.DefaultPaymentID == true) != null ?CustPayProf.FirstOrDefault(A=>A.DefaultPaymentID == true).AuthPaymentProfileID:"";
                        var customer = objGW.GetCustomer(AuthProfileID);
                        if (customer != null && DefalutPaymentProfileID != "")
                        {
                            var s = customer.PaymentProfiles.FirstOrDefault(A=>A.ProfileID == DefalutPaymentProfileID);
                            if(s!= null)
                            {
                                DefaultCreditCardDetails = "XXXXXXXX"+s.CardNumber+"("+DefalutPaymentProfileID+")";
                            }
                            if (customer.PaymentProfiles.Count() != 0)
                            {
                                int i = 0;
                                foreach (var pymtPrf in customer.PaymentProfiles)
                                {
                                    //if ((customer.PaymentProfiles[i].CardNumber.Contains(CreditCardNo) && customer.PaymentProfiles[i].BillingAddress == BillingAddress) == true)
                                    if ((customer.PaymentProfiles[i].CardNumber.Contains(CreditCardNo)) == true)
                                    {
                                        bool defalutcard = false;

                                        if (divExistingCrdCardList.Visible == true)
                                        {
                                            //  int CustomerPaymenProfileID =Convert.ToInt32(cpjhgp.FirstOrDefault().CustomerPaymenProfileID);
                                            var deleteOldestRegistered = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == rblCreditCard.SelectedValue);//CustomerPaymenProfileID == CustomerPaymenProfileID);
                                            if (deleteOldestRegistered != null)
                                            {
                                                defalutcard = Convert.ToBoolean(deleteOldestRegistered.DefaultPaymentID);
                                                deleteOldestRegistered.DefaultPaymentID = false;
                                                deleteOldestRegistered.Status = false;
                                                objDBContext.SaveChanges();
                                            }
                                        }
                                        PaymentProfileIndex = i;
                                        PaymentprofileExist = true;
                                        AuthPaymentProfileID = customer.PaymentProfiles[i].ProfileID;
                                        var ChangePaymentCardStatus = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == AuthPaymentProfileID);
                                        if (ChangePaymentCardStatus != null)
                                        {
                                            if (ChangePaymentCardStatus.DefaultPaymentID != true)
                                            {
                                                ChangePaymentCardStatus.DefaultPaymentID = chkDefalutCreditCard.Checked;
                                                if (defalutcard == true)
                                                    ChangePaymentCardStatus.DefaultPaymentID = true;
                                            }
                                            ChangePaymentCardStatus.Status = true;
                                            objDBContext.SaveChanges();
                                            if (ChangePaymentCardStatus.DefaultPaymentID == true)
                                            {
                                                makeOtherCardsDefaultpaymentcardFalse(UserProfileID, AuthPaymentProfileID);
                                            }
                                        }
                                        else
                                        {
                                            CustomerPaymentProfile Cpp = new CustomerPaymentProfile();
                                            Cpp.AuthBillingAddressID = customer.PaymentProfiles[i].BillingAddress.ID;
                                            Cpp.AuthCustomerProfileID = AuthProfileID;
                                            Cpp.AuthPaymentProfileID = AuthPaymentProfileID;
                                            Cpp.DefaultPaymentID = chkDefalutCreditCard.Checked;
                                            if (defalutcard == true)
                                            {
                                                Cpp.DefaultPaymentID = true;
                                            }
                                            Cpp.Status = true;
                                            Cpp.UserProfileID = UserProfileID;
                                            objDBContext.CustomerPaymentProfiles.AddObject(Cpp);
                                            objDBContext.SaveChanges();
                                            if(Cpp.DefaultPaymentID  == true)
                                            {
                                            makeOtherCardsDefaultpaymentcardFalse(UserProfileID, AuthPaymentProfileID);
                                            DefaultCreditCardDetails = "XXXXXXXXXXXX" + CreditCardNo + "(" + AuthPaymentProfileID + ")";
                                            }
                                        }
                                        // objGW.DeletePaymentProfile(user.AuthProfileID, customer.PaymentProfiles[0].ProfileID);
                                        //updatePaymentProfileID(user, user.AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress);
                                    }
                                    i++;
                                }
                                //if (PaymentprofileExist == false && i > 2)
                                //{
                                //    divPaymentErrorMessage.Visible = true;
                                //    lblPaymentErrormsg.Text = "Only 3 credit cards can be used.Please try with the registered credit card.";
                                //    return;
                                //}
                                if (PaymentprofileExist == false)
                                {
                                    AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress, chkDefalutCreditCard.Checked);
                                }
                            }
                            else
                            {
                                AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress, chkDefalutCreditCard.Checked);
                            }
                        }
                        else
                        {
                            AuthProfileID = ReusableRoutines.AuthorizeCreateCustomerProfile(user.EmailID, ProfileName, UserFullName);
                            if (AuthProfileID == "Failed")
                            {
                                divPaymentSuccessMessage.Visible = false;
                                divPaymentErrorMessage.Visible = true;
                                lblPaymentErrormsg.Text = "Error occurred while creating payment profile.";
                                return;
                            }
                            AuthPaymentProfileID = updatePaymentProfileID(user, AuthProfileID, AuthBillingAddressID, ExpMonth, ExpYear, BillingAddress, chkDefalutCreditCard.Checked);
                        }
                    }

                
                divPaymentSuccessMessage.Visible = true;
                divPaymentErrorMessage.Visible = false;
                lblPaymentSuccessmsg.Text = "Payment details saved successfully.";
                string NewWorkLog = "";

                //divExistingCrdCardList.Visible == true && rblCreditCard.SelectedItem != null && rblCreditCard.SelectedValue != AuthPaymentProfileID
                if (chkDefalutCreditCard.Checked == true)
                {
                    //if (rblCreditCard.SelectedItem.Text.Contains("Default Credit Card"))
                    //{
                    //    NewWorkLog = " Authorize.net Credit Card Payment Profile ID for all future transaction : Changed From " + rblCreditCard.SelectedItem.Text + "(" + rblCreditCard.SelectedValue + ") To XXXXXXXXXXXX" + CreditCardNo + "(" + AuthPaymentProfileID + ")";
                    //    objDBContext.pr_UpdateUserPaymentProfileIDAndWorklog(rblCreditCard.SelectedValue, UserFullName + "(" + user.EmailID + ")", NewWorkLog, user.UserProfileID);
                    //}
                    //else
                    //{
                        NewWorkLog = " Authorize.net Credit Card Payment Profile ID for all future transaction : Changed to " +  DefaultCreditCardDetails;
                        objDBContext.pr_UpdateUserPaymentProfileIDAndWorklog(AuthPaymentProfileID, UserFullName + "(" + user.EmailID + ")", NewWorkLog, user.UserProfileID);
                    //}
                }
                BindPaymentCustomerDetails(user.UserMembershipID);
                ddlExistingCards.SelectedValue = AuthPaymentProfileID;
                dvCardDetails.Visible = false;
                FilCreditCardDetails();
                /*
                var CustomerCardDetails = objDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == user.UserProfileID && u.Status==true);
                //if (CustomerCardDetails.Count() == 3)
                //{
                //    trSaveCrediCard.Visible = false;
                //}
                
                if (CustomerCardDetails.Count() != 0)
                {
                    var CustomerpaymentProfiles = objGW.GetCustomer(CustomerCardDetails.FirstOrDefault().AuthCustomerProfileID);
                    var zz = objDBContext.CustomerPaymentProfiles.Where(A => A.UserProfileID == user.UserProfileID && A.Status == true);

                    var s = from cpp in CustomerpaymentProfiles.PaymentProfiles
                            join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                            select new
                            {
                                ProfileID = cpp.ProfileID,
                                CardNumber = "XXXXXXXX" + cpp.CardNumber,
                                DefaultPaymentID = zz1.DefaultPaymentID
                            };
                    ddlExistingCards.DataSource = s;
                    ddlExistingCards.DataTextField = "CardNumber";
                    ddlExistingCards.DataValueField = "ProfileID";
                    ddlExistingCards.DataBind();
                    ddlExistingCards.Items.Insert(0, new ListItem("New Credit Card", "New Credit Card"));
                    //var z1 = s != null ? s.FirstOrDefault(z => z.DefaultPaymentID == true) : null;
                    //if (z1 != null)
                    //{
                        //ddlExistingCards.SelectedValue = AuthPaymentProfileID;
                        //   FilCreditCardDetails();
                    //}
                    if (s.Count() == 3)
                    {
                        divExistingCrdCardList.Visible = true;
                        var s1 = from cpp in CustomerpaymentProfiles.PaymentProfiles
                                 join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                                 select new
                                 {
                                     ProfileID = cpp.ProfileID,
                                     CardNumber = "XXXXXXXX" + cpp.CardNumber + "" + (zz1.DefaultPaymentID == true ? "&nbsp;&nbsp;&nbsp;Default Credit Card" : ""),
                                     DefaultPaymentID = zz1.DefaultPaymentID,
                                     AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                     UserProfileID = zz1.UserProfileID,
                                 };
                        rblCreditCard.DataSource = s1;
                        rblCreditCard.DataTextField = "CardNumber";
                        rblCreditCard.DataValueField = "ProfileID";
                        rblCreditCard.DataBind();
                    }
                    else
                    {
                        divExistingCrdCardList.Visible = false;
                    }
                    ddlExistingCards.SelectedValue = AuthPaymentProfileID;
                    FilCreditCardDetails();
                   // ddlExistingCards.Items.Insert(0, new ListItem("<- Select Registered Card ->", "<- Select Registered Card ->"));
                    //ddlExistingCards_SelectedIndexChanged(sender, e);
                    ddlExistingCards.Visible = true;
                    //btnExistingcard.Visible = false;
                    //btnNewCard.Visible = true;
                    btnUpdate.Visible = true;
                    dvCardDetails.Visible = false;
                    trExistingCards.Visible = true;
                   // btnNewCard.Visible = s.Count() < 3 ? true : false;
                }
                 */
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divPaymentSuccessMessage.Visible = false;
                divPaymentErrorMessage.Visible = true;
                lblPaymentErrormsg.Text = "Error occurred while saving payment details.";
            }
        }

        //protected void lkbtnUnSubscribe_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        UserMembershipID = Request.QueryString["userid"];
        //        if (UserMembershipID != null)
        //        {
        //            Guid ID = new Guid(UserMembershipID);
        //            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
        //            MembershipUser MSU = Membership.GetUser(user.EmailID);
        //            MSU.IsApproved = false;
        //            Membership.UpdateUser(MSU);
        //            user.RecordStatus = DBKeys.RecordStatus_Delete;
        //            objDBContext.SaveChanges();
        //            SMTPManager.SendEmail("Your are successfully Unsubscribed from the wft cloud services.", "Unsubscribed from the wft cloud services.", user.EmailID, false,false);
        //            SMTPManager.SendAdminNotificationEmail("Dear WFT Admin.\n\nThe user " + user.FirstName + " " + user.LastName + " whose emailid is " + user.EmailID + " cancelled all WFT subscriptions.\n\nRegards,\n\nAdmin Notification.", "User " + user.EmailID + " cancelled subscription", false);
        //            divCancelErrorMessage.Visible = false;
        //            divCancelSuccessMessage.Visible = true;
        //            lblCancelSuccessmsg.Text = "Your are successfully Unsubscribed from the wft cloud services.";
        //            Session.Clear();
        //            FormsAuthentication.SignOut();
        //            Response.Redirect("/index.aspx");
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ReusableRoutines.LogException(this.GetType().ToString(), "lkbtnUnSubscribe_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
        //    }
        //}
        #endregion

        #region Reusable Routines
        private void BindPaymentCustomerDetails(Guid ID)
        {
            try
            {

                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                txtFirstName.Text = user.FirstName;
                txtMiddleName.Text = user.MiddleName != null ? (user.MiddleName != "" ? user.MiddleName : " - ") : user.MiddleName = " - ";
                txtLastName.Text = user.LastName;
                txtEmailId.Text = user.EmailID;
                txtContactNumber.Text = user.ContactNumber;
                txtPhoneNumberOff.Text = user.PhoneNumber;
                txtMobileNumberPer.Text = user.MobileNumber;
                txtMailingAddress.Text = user.MailingAddress;
                txtCompanyName.Text = user.CompanyName;
                ddlCountry2.SelectedValue = user.Location != null ? (user.Location != "" ? user.Location : "0") : user.Location = "0";
                var CustomerCardDetails = objDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == user.UserProfileID && u.Status == true);
                if (CustomerCardDetails.Count() == 3)
                {
                    trSaveCrediCard.Visible = false;
                }

                if (CustomerCardDetails.Count() != 0)
                {
                    var CustomerpaymentProfiles = objGW.GetCustomer(CustomerCardDetails.FirstOrDefault().AuthCustomerProfileID);
                    var zz = objDBContext.CustomerPaymentProfiles.Where(A => A.UserProfileID == user.UserProfileID && A.Status == true);

                    var s = from cpp in CustomerpaymentProfiles.PaymentProfiles
                            join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                            select new
                            {
                                ProfileID = cpp.ProfileID,
                                CardNumber = "XXXXXXXX" + cpp.CardNumber,
                                DefaultPaymentID = zz1.DefaultPaymentID,
                                AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                UserProfileID = zz1.UserProfileID,
                            };
                    ddlExistingCards.DataSource = s;
                    ddlExistingCards.DataTextField = "CardNumber";
                    ddlExistingCards.DataValueField = "ProfileID";
                    ddlExistingCards.DataBind();
                    ddlExistingCards.Items.Insert(0, new ListItem("New Credit Card", "New Credit Card"));
                    var z1 = s != null ? s.FirstOrDefault(z => z.DefaultPaymentID == true) : null;
                    if (z1 != null)
                    {
                        ddlExistingCards.SelectedValue = z1.ProfileID;
                        FilCreditCardDetails();
                    }
                    if (s.Count() == 3)
                    {
                        divExistingCrdCardList.Visible = true;
                        var s1 = from cpp in CustomerpaymentProfiles.PaymentProfiles
                                 join zz1 in zz on cpp.ProfileID equals zz1.AuthPaymentProfileID
                                 select new
                                 {
                                     ProfileID = cpp.ProfileID,
                                     CardNumber = "XXXXXXXX" + cpp.CardNumber + "" + (zz1.DefaultPaymentID == true ? "&nbsp;&nbsp;&nbsp;Default Credit Card" : ""),
                                     DefaultPaymentID = zz1.DefaultPaymentID,
                                     AuthCustomerProfileID = zz1.AuthCustomerProfileID,
                                     UserProfileID = zz1.UserProfileID,
                                 };
                        rblCreditCard.DataSource = s1;
                        rblCreditCard.DataTextField = "CardNumber";
                        rblCreditCard.DataValueField = "ProfileID";
                        rblCreditCard.DataBind();
                    }
                    else
                    {
                        divExistingCrdCardList.Visible = false;
                    }
                    // ddlExistingCards.Items.Insert(0, new ListItem("<- Select Registered Card ->", "<- Select Registered Card ->"));
                    trExistingCards.Visible = true;
                    btnUpdate.Visible = true;
                    FilCreditCardDetails();
                    //   btnNewCard.Visible = s.Count() < 3 ? true : false;
                }
                else
                {
                    //btnExistingcard.Visible = true;
                    //btnNewCard.Visible = true;
                    dvCardDetails.Visible = true;
                    btnUpdate.Visible = false;

                    //rblTypeOfCard.ClearSelection();
                    ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                    txtAddress2.Text = txtAddress1.Text = txtCity.Text = txtContactNumberPtDet.Text = txtCreditCardNumber.Text = txtPostalZipCode.Text = txtNameOnCard.Text = "";
                    txtAddress1.ReadOnly = txtAddress2.ReadOnly = txtCity.ReadOnly = txtContactNumberPtDet.ReadOnly = false;
                    txtCreditCardNumber.ReadOnly = txtNameOnCard.ReadOnly = txtPostalZipCode.ReadOnly = txtVerifiCode1.ReadOnly = false;
                    ddlCountry.SelectedIndex = 0;
                    ChangeStatenames(ddlCountry.SelectedValue);
                    ddlCountry.Enabled = true;
                    ddlState.Enabled = true;
                    ddlExpMonth.Enabled = ddlExpYear.Enabled = true;
                    trSaveCrediCard.Visible = true;
                    divValidCard.Visible = false;
                    divInValidCard.Visible = true;
                    lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
                    trReadOnlyDefaultMod.Visible = false;
                }
                //Show CreditCard Details.
               // ShowCreditCardDetails();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void LoadTabView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    string View = (Request.QueryString[QueryStringKeys.ShowView]);
                    if (View == "UserDetails")
                    {
                        liUserDetails.Attributes.Add("class", "active");
                        UserProfilesDetails.Attributes.Add("class", "tab-pane in active");
                        liChangePaymentDetails.Attributes.Add("class", "");
                        ChangePaymentDetails.Attributes.Add("class", "tab-pane");
                        //liCancelSubscription.Attributes.Add("class", "");
                        //CancelSubscription.Attributes.Add("class", "tab-pane");
                        liChangePassword.Attributes.Add("class", "");
                        ChangePassword.Attributes.Add("class", "tab-pane");
                    }
                    else if (View == "ChangePassword")
                    {
                        liUserDetails.Attributes.Add("class", "");
                        UserProfilesDetails.Attributes.Add("class", "tab-pane");
                        liChangePaymentDetails.Attributes.Add("class", "");
                        ChangePaymentDetails.Attributes.Add("class", "tab-pane");
                        //liCancelSubscription.Attributes.Add("class", "");
                        //CancelSubscription.Attributes.Add("class", "tab-pane");
                        liChangePassword.Attributes.Add("class", "active");
                        ChangePassword.Attributes.Add("class", "tab-pane in active");
                    }
                    else if (View == "ChangePaymentDetails")
                    {
                        liUserDetails.Attributes.Add("class", "");
                        UserProfilesDetails.Attributes.Add("class", "tab-pane");
                        liChangePaymentDetails.Attributes.Add("class", "active");
                        ChangePaymentDetails.Attributes.Add("class", "tab-pane in active");
                        //liCancelSubscription.Attributes.Add("class", "");
                        //CancelSubscription.Attributes.Add("class", "tab-pane");
                        liChangePassword.Attributes.Add("class", "");
                        ChangePassword.Attributes.Add("class", "tab-pane");
                        ddlCountry.DataSource = objDBContext.Countries.OrderBy(a => a.CountryNames);
                        ddlCountry.DataValueField = "CC_ISO";
                        ddlCountry.DataTextField = "CountryNames";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("<--Select Country-->","0"));
                        ddlState.Items.Clear();
                        ddlState.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                    }
                    else if (View == "CancelSubscription")
                    {
                        liUserDetails.Attributes.Add("class", "");
                        UserProfilesDetails.Attributes.Add("class", "tab-pane");
                        liChangePaymentDetails.Attributes.Add("class", "");
                        ChangePaymentDetails.Attributes.Add("class", "tab-pane");
                        //liCancelSubscription.Attributes.Add("class", "active");
                        //CancelSubscription.Attributes.Add("class", "tab-pane in active");
                        liChangePassword.Attributes.Add("class", "");
                        ChangePassword.Attributes.Add("class", "tab-pane");
                    }
                    else
                    {
                        liUserDetails.Attributes.Add("class", "active");
                        UserProfilesDetails.Attributes.Add("class", "tab-pane in active");
                        liChangePaymentDetails.Attributes.Add("class", "");
                        ChangePaymentDetails.Attributes.Add("class", "tab-pane");
                        //liCancelSubscription.Attributes.Add("class", "");
                        //CancelSubscription.Attributes.Add("class", "tab-pane");
                        liChangePassword.Attributes.Add("class", "");
                        ChangePassword.Attributes.Add("class", "tab-pane");
                    }
                }
                else
                {
                    liUserDetails.Attributes.Add("class", "active");
                    UserProfilesDetails.Attributes.Add("class", "tab-pane in active");
                    liChangePaymentDetails.Attributes.Add("class", "");
                    ChangePaymentDetails.Attributes.Add("class", "tab-pane");
                    //liCancelSubscription.Attributes.Add("class", "");
                    //CancelSubscription.Attributes.Add("class", "tab-pane");
                    liChangePassword.Attributes.Add("class", "");
                    ChangePassword.Attributes.Add("class", "tab-pane");
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ChangeStatenames(string CC_ISO)
        {
            ddlState.Items.Clear();
            if (CC_ISO != "0")
            {
                var states = objDBContext.StateNames.Where(s => s.CC_ISO == CC_ISO).OrderBy(A => A.StateName1);
                ddlState.DataSource = states;
                ddlState.DataTextField = "StateName1";
                ddlState.DataValueField = "StateName1";
                ddlState.DataBind();
                if (ddlState.Items.Count == 0)
                {
                    ddlState.Items.Insert(0, new ListItem(ddlCountry.SelectedItem.Text, ddlCountry.SelectedItem.Text));
                }
                ddlState.Items.Insert(0, new ListItem("<--Select State-->", "0"));
            }
            else
            {
                ddlState.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
            }
        }

        public string updatePaymentProfileID(UserProfile user, string AuthProfileID, string AuthBillingAddressID, int ExpMonth, int ExpYear, AuthorizeNet.Address BillingAddress,bool DefaultCreditCard)
        {
            string AuthPaymentProfileID;
            var s = objDBContext.CustomerPaymentProfiles.Where(cpp => cpp.UserProfileID == user.UserProfileID).ToList();
            int upid = Convert.ToInt32(user.UserProfileID);
            string BillingID = user.UserProfileID.ToString() + "-" + Convert.ToString(s.Count() > 0 ? (s.Count() + 1) : 1);
            //BillingAddress.ID = BillingID;
            AuthPaymentProfileID = ReusableRoutines.AuthorizeAddCreditCardProfile(AuthProfileID, txtCreditCardNumber.Text, ExpMonth, ExpYear, txtVerifiCode1.Text, BillingAddress);

            var customer = objGW.GetCustomer(AuthProfileID);
            var cpjhgp = objDBContext.CustomerPaymentProfiles.Where(zMycart => zMycart.UserProfileID == upid && zMycart.Status == true).ToList();
            bool defalutcard = false;

            if (divExistingCrdCardList.Visible == true)
            {
                //  int CustomerPaymenProfileID =Convert.ToInt32(cpjhgp.FirstOrDefault().CustomerPaymenProfileID);
                var deleteOldestRegistered = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthPaymentProfileID == rblCreditCard.SelectedValue);//CustomerPaymenProfileID == CustomerPaymenProfileID);
                defalutcard = Convert.ToBoolean(deleteOldestRegistered.DefaultPaymentID);
                deleteOldestRegistered.DefaultPaymentID = false;
                deleteOldestRegistered.Status = false;
                objDBContext.SaveChanges();
            }
            int UserProfID = Convert.ToInt32(user.UserProfileID);
            if (trUpdateDefaultMod.Visible == true && defalutcard==false)
            {
                defalutcard = chkDefalutCreditCard.Checked;
            }
            CustomerPaymentProfile nW = new CustomerPaymentProfile();
            nW.AuthCustomerProfileID = AuthProfileID;
            nW.AuthPaymentProfileID = AuthPaymentProfileID;
            nW.UserProfileID = user.UserProfileID;
            nW.DefaultPaymentID = cpjhgp.Count() == 0 ? true : false;
            if (defalutcard == true)
            {
                nW.DefaultPaymentID = defalutcard;
            }
            nW.AuthBillingAddressID = BillingID;
            nW.Status = true;
            objDBContext.CustomerPaymentProfiles.AddObject(nW);
            objDBContext.SaveChanges();
            if (nW.DefaultPaymentID == true)
            { makeOtherCardsDefaultpaymentcardFalse(UserProfID, AuthPaymentProfileID); }
            return AuthPaymentProfileID;
        }
        private void ShowCreditCardDetails()
        {
            Guid ID = new Guid(UserMembershipID);
            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
            if (user.AuthProfileID != null)
            {
                var customer = objGW.GetCustomer(user.AuthProfileID);
                txtAddress1.Text = customer.PaymentProfiles[0].BillingAddress.Street;
                txtCity.Text = customer.PaymentProfiles[0].BillingAddress.City;
                ddlCountry.SelectedValue = customer.PaymentProfiles[0].BillingAddress.Country;
                txtContactNumberPtDet.Text = customer.PaymentProfiles[0].BillingAddress.Phone;
                txtCreditCardNumber.Text = "XXXXXXXX" + customer.PaymentProfiles[0].CardNumber;
                ddlExpMonth.SelectedValue = customer.PaymentProfiles[0].CardExpiration[0].ToString() + customer.PaymentProfiles[0].CardExpiration[1].ToString();
                ddlExpYear.SelectedValue = customer.PaymentProfiles[0].CardExpiration[2].ToString() + customer.PaymentProfiles[0].CardExpiration[3].ToString();
               // rblTypeOfCard.SelectedValue = customer.PaymentProfiles[0].CardType;
                txtPostalZipCode.Text = customer.PaymentProfiles[0].BillingAddress.Zip;
                ddlState.SelectedValue = customer.PaymentProfiles[0].BillingAddress.State;
                txtNameOnCard.Text = customer.PaymentProfiles[0].BillingAddress.First + " " + customer.PaymentProfiles[0].BillingAddress.Last;
                divValidCard.Visible = false;
                divInValidCard.Visible = true;
                lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
            }
        }

        private void FilCreditCardDetails()
        {
            try
            {
                if (ddlExistingCards.SelectedValue != "New Credit Card")
                {
                    trSaveCrediCard.Visible = false;
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    var CustomerCardDetails = objDBContext.CustomerPaymentProfiles.Where(u => u.UserProfileID == user.UserProfileID && u.Status == true);
                    if (CustomerCardDetails.Count() > 0)
                    {
                        var s1 = CustomerCardDetails.FirstOrDefault(s => s.AuthPaymentProfileID == ddlExistingCards.SelectedValue);
                        var customerPaymentProfile = objGW.GetCustomer(s1.AuthCustomerProfileID).PaymentProfiles.First(a => a.ProfileID == ddlExistingCards.SelectedValue);
                        //rblTypeOfCard.ClearSelection();
                        ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                        txtAddress1.Text = customerPaymentProfile.BillingAddress.Street;
                        txtCity.Text = customerPaymentProfile.BillingAddress.City;
                        txtContactNumberPtDet.Text = customerPaymentProfile.BillingAddress.Phone;
                        txtCreditCardNumber.Text = "XXXXXXXX" + customerPaymentProfile.CardNumber;
                        txtPostalZipCode.Text = customerPaymentProfile.BillingAddress.Zip;
                        
                        txtNameOnCard.Text = customerPaymentProfile.BillingAddress.First + " " + customerPaymentProfile.BillingAddress.Last;
                        txtAddress1.ReadOnly = txtAddress2.ReadOnly = txtCity.ReadOnly = txtContactNumberPtDet.ReadOnly = true;
                        txtCreditCardNumber.ReadOnly = txtNameOnCard.ReadOnly = txtPostalZipCode.ReadOnly = txtVerifiCode1.ReadOnly = true;
                        ddlState.Enabled = ddlCountry.Enabled = false;
                        ddlExpMonth.Enabled = ddlExpYear.Enabled = false;
                        chkDefalutCreditCard.Checked = Convert.ToBoolean(s1.DefaultPaymentID);
                        //chkDefalutCreditCard.Text = chkDefalutCreditCard.Checked == true ? "&nbsp;&nbsp;&nbsp;Yes" : "&nbsp;&nbsp;&nbsp;No";
                        divInValidCard.Visible = false;
                        if (s1.DefaultPaymentID == true)
                        {
                            btnUpdate.Visible = false;
                            trUpdateDefaultMod.Visible = false;
                            trReadOnlyDefaultMod.Visible = true;
                        }
                        else
                        {
                            btnUpdate.Visible = true;
                            trUpdateDefaultMod.Visible = true;
                            trReadOnlyDefaultMod.Visible = false;
                        }

                        var Coun = objDBContext.Countries.FirstOrDefault(a => a.CountryNames.Contains(customerPaymentProfile.BillingAddress.Country));
                        if (Coun != null)
                        {
                            if (ddlCountry.SelectedValue != Coun.CC_ISO)
                            {
                                ddlCountry.SelectedValue = Coun.CC_ISO;
                                ChangeStatenames(Coun.CC_ISO);
                            }
                            
                            var state = objDBContext.StateNames.FirstOrDefault(s => s.StateName1.Contains(customerPaymentProfile.BillingAddress.State) && s.CC_ISO == Coun.CC_ISO);
                            if (state != null)
                            { ddlState.SelectedValue = state.StateName1; }
                            else
                            {
                                ddlState.SelectedValue = "0";
                                if (ddlState.Items.Count == 2 && ddlState.Items[1].Text == ddlCountry.SelectedItem.Text)
                                {
                                    ddlState.SelectedIndex = 1;
                                }
                            }
                        }
                        else
                        {
                            ddlState.Items.Clear();
                            ddlState.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                            ddlCountry.SelectedValue = "0";
                        }
                        
                    }
                    else
                    {
                        //btnExistingcard.Visible = true;
                        //btnNewCard.Visible = true;
                        dvCardDetails.Visible = true;
                        trUpdateDefaultMod.Visible = true;
                        chkDefalutCreditCard.Checked = false;
                        //rblTypeOfCard.ClearSelection();
                        ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                        txtAddress2.Text = txtAddress1.Text = txtCity.Text = txtContactNumberPtDet.Text = txtCreditCardNumber.Text = txtPostalZipCode.Text = txtNameOnCard.Text = "";
                        txtAddress1.ReadOnly = txtAddress2.ReadOnly = txtCity.ReadOnly = txtContactNumberPtDet.ReadOnly = false;
                        txtCreditCardNumber.ReadOnly = txtNameOnCard.ReadOnly = txtPostalZipCode.ReadOnly = txtVerifiCode1.ReadOnly = false;
                        ddlCountry.SelectedIndex = 0;
                        ChangeStatenames(ddlCountry.SelectedValue);
                        ddlState.Enabled = ddlCountry.Enabled = true;
                        ddlExpMonth.Enabled = ddlExpYear.Enabled = true;
                        trSaveCrediCard.Visible = true;
                        divValidCard.Visible = false;
                        divInValidCard.Visible = true;
                        lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
                        trReadOnlyDefaultMod.Visible = false;
                    }
                }
                else
                {
                    //btnExistingcard.Visible = true;
                    //btnNewCard.Visible = true;
                    dvCardDetails.Visible = true;
                    btnUpdate.Visible = false;
                    trUpdateDefaultMod.Visible = true;
                    chkDefalutCreditCard.Checked = false;
                    trReadOnlyDefaultMod.Visible = false;
                    trSaveCrediCard.Visible = true;
                    //rblTypeOfCard.ClearSelection();
                    ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
                    txtAddress2.Text = txtAddress1.Text = txtCity.Text = txtContactNumberPtDet.Text = txtCreditCardNumber.Text = txtPostalZipCode.Text =txtNameOnCard.Text = "";
                    txtAddress1.ReadOnly = txtAddress2.ReadOnly = txtCity.ReadOnly = txtContactNumberPtDet.ReadOnly = false;
                    txtCreditCardNumber.ReadOnly = txtNameOnCard.ReadOnly = txtPostalZipCode.ReadOnly = txtVerifiCode1.ReadOnly = false;
                    ddlCountry.SelectedValue = "0";
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
                    ddlCountry.Enabled = true;
                    ddlState.Enabled = true;
                    ddlExpMonth.Enabled = ddlExpYear.Enabled = true;
                    trSaveCrediCard.Visible = true;
                    trCardType.Visible = divValidCard.Visible = false;
                    divInValidCard.Visible = true;
                    lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        protected void ddlExistingCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilCreditCardDetails();
            if (ddlExistingCards.SelectedIndex == 0)
            {
                ddlState.Enabled = ddlCountry.Enabled = ddlExpYear.Enabled = ddlExpMonth.Enabled = true; 
                chkDefalutCreditCard.Enabled = true;
            }
            else
            {
                dvCardDetails.Visible = false;
                ddlState.Enabled = ddlCountry.Enabled = ddlExpYear.Enabled = ddlExpMonth.Enabled = false;
            }
        }

        //protected void btnNewCard_Click(object sender, EventArgs e)
        //{
        //    ddlExistingCards.SelectedIndex = 0;
        //    ddlExistingCards_SelectedIndexChanged(sender, e);
        //    ddlExistingCards.Visible = false;
        //    btnExistingcard.Visible = true;
        //    btnNewCard.Visible = true;
        //    dvCardDetails.Visible = true;
        //    btnUpdate.Visible = false;
            
        //        //rblTypeOfCard.ClearSelection();
        //        ddlExpMonth.SelectedIndex = ddlExpYear.SelectedIndex = 0;
        //        txtAddress2.Text = txtAddress1.Text = txtCity.Text =  txtContactNumberPtDet.Text = txtCreditCardNumber.Text = txtPostalZipCode.Text = txtState.Text = txtNameOnCard.Text = "";
        //        txtAddress1.ReadOnly = txtAddress2.ReadOnly = txtCity.ReadOnly = ddlCountry.Enabled= txtContactNumberPtDet.ReadOnly = false;
        //        txtCreditCardNumber.ReadOnly = txtNameOnCard.ReadOnly = txtPostalZipCode.ReadOnly = txtState.ReadOnly = txtVerifiCode1.ReadOnly = false;
        //        ddlCountry.SelectedValue = "";
        //        ddlCountry.Enabled = true;
        //         ddlExpMonth.Enabled = ddlExpYear.Enabled = true;
        //        trSaveCrediCard.Visible = true;
        //        divValidCard.Visible = false;
        //        divInValidCard.Visible = true;
        //        lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
        //}

        //protected void btnExistingcard_Click(object sender, EventArgs e)
        //{
        //    ddlExistingCards.SelectedIndex = 0;
        //    ddlExistingCards_SelectedIndexChanged(sender, e);
        //    ddlExistingCards.Visible = true;
        //    btnExistingcard.Visible = false;
        //    btnNewCard.Visible = true;
        //    btnUpdate.Visible = true;
        //    dvCardDetails.Visible = false;
        //}

        protected void txtCreditCardNumber_TextChanged(object sender, EventArgs e)
        {
            string a = ReusableRoutines.GetCreditCardType(txtCreditCardNumber.Text);
            if (a.Contains("Invalid Credit Card"))
            {
                trCardType.Visible = false;
                divValidCard.Visible = false;
                divInValidCard.Visible = true;
                lblInvalidCard.Text = "Invalid Credit Card. Please Enter Valid Credit Card Number.";
            }
            else
            {
                trCardType.Visible = true;
                divValidCard.Visible = true;
                divInValidCard.Visible = false;
                lblCardType.Text = a;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
                    if (trExistingCards.Visible)
                    {
                        var ExistingCustpayment = objDBContext.CustomerPaymentProfiles.FirstOrDefault(d => d.AuthPaymentProfileID == ddlExistingCards.SelectedValue);
                        if(ExistingCustpayment!= null)
                        {
                            ExistingCustpayment.DefaultPaymentID =chkDefalutCreditCard.Checked;
                            objDBContext.SaveChanges();
                            if (ExistingCustpayment.DefaultPaymentID == true)
                            {
                                int id = Convert.ToInt32(ExistingCustpayment.UserProfileID);
                                makeOtherCardsDefaultpaymentcardFalse(id, ExistingCustpayment.AuthPaymentProfileID);
                            }
                        }
                    }
                    //ShowCreditCardDetails();
                    FilCreditCardDetails();
                    divPaymentSuccessMessage.Visible = true;
                    divPaymentErrorMessage.Visible = false;
                    lblPaymentSuccessmsg.Text = "Payment details saved successfully.";
        }

        public void makeOtherCardsDefaultpaymentcardFalse(int userprofileid, string AuthPaymentProfileID)
        {
            var makeOtherCardsFirst = objDBContext.CustomerPaymentProfiles.Where(uid => uid.UserProfileID == userprofileid && uid.AuthPaymentProfileID != AuthPaymentProfileID && uid.Status == true).ToList();
            if (makeOtherCardsFirst != null)
            {
                foreach (var result in makeOtherCardsFirst)
                {
                    var updateToFalse = objDBContext.CustomerPaymentProfiles.FirstOrDefault(r => r.CustomerPaymenProfileID == result.CustomerPaymenProfileID);
                    updateToFalse.DefaultPaymentID = false;
                    objDBContext.SaveChanges();
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeStatenames(ddlCountry.SelectedValue);
        }

        protected void rblCreditCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblCreditCard.SelectedItem.Text.Contains("Default Credit Card"))
                {
                    chkDefalutCreditCard.Checked = true;
                    chkDefalutCreditCard.Enabled = false;
                }
                else
                {
                     chkDefalutCreditCard.Checked = false;
                     chkDefalutCreditCard.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}