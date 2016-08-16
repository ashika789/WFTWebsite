using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using System.Configuration;
using System.Web.Security;
using System.IO;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud
{
    public partial class FrontEnd : System.Web.UI.MasterPage
    {
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string pageName1 = this.Page.ToString();
                var setting = objDBContext.WftSettings.FirstOrDefault(st=>st.SettingKey=="SITE_LOCKED" && st.SettingValue=="1");
                if (setting == null || pageName1.Contains("loginpage_aspx"))
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (HttpContext.Current.User.Identity.IsAuthenticated && cookie != null)
                    {
                        //liregister.Visible = false;
                        MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                        UserMembershipID = MSU.ProviderUserKey.ToString();
                        Guid MuID = Guid.Parse(UserMembershipID);
                        licart.Visible = true;
                        UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                        loginUser.InnerText = string.Format("  Welcome {0}", upf.FirstName);
                        string RoleName = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name).First();
                        if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                        {
                            hrefloggedInLink.HRef = "/Admin/userlist.aspx";
                            licart.Visible = false;
                        }
                        else
                        {
                            licart.Visible = true;
                            hrefloggedInLink.HRef = "/Customer/CloudPackages.aspx?userid=" + MuID.ToString() + "&showview=SubscribedService";
                        }
                        
                        lilogout.Visible = true;
                        liSignUp.Visible = false;
                    }

                    ShowRandomBannerImages();

                    if (!IsPostBack)
                    {
                        string selectedLanguage = "English";
                        string pageName = Path.GetFileName(Request.Url.AbsolutePath).ToLower();
                        //string selectedLanguage = Convert.ToString(Session["Language"]);
                        if (Request.Cookies["LanguageCookie"] != null)
                        {
                            selectedLanguage = Request.Cookies["LanguageCookie"].Value;
                        }
                        else
                        {
                            Response.Cookies["LanguageCookie"].Value = hdnLanguage.Value != null && hdnLanguage.Value != "" ? hdnLanguage.Value : "English";
                            Response.Redirect(Request.RawUrl,false);
                        }
                        var seoContents = objDBContext.SitePagesAndContents.FirstOrDefault(spc => spc.PageRelativeUrl == pageName && spc.Language == selectedLanguage);
                        if (seoContents != null)
                        {
                            this.Page.Title = seoContents.PageTitle;
                            this.Page.MetaKeywords = seoContents.MetaKeywords;
                            this.Page.MetaDescription = seoContents.MetaDescription;
                        }
                        else
                        {
                            var seoEnglishContents = objDBContext.SitePagesAndContents.FirstOrDefault(spc => spc.PageRelativeUrl == pageName && spc.Language == "English");
                            if (seoEnglishContents != null)
                            {
                                this.Page.Title = seoEnglishContents.PageTitle;
                                this.Page.MetaKeywords = seoEnglishContents.MetaKeywords;
                                this.Page.MetaDescription = seoEnglishContents.MetaDescription;
                            }
                            else
                            {
                                if (Request.QueryString[QueryStringKeys.PreviewID].IsValid())
                                {
                                    int PreviewID = int.Parse(Request.QueryString[QueryStringKeys.PreviewID]);
                                    var Title = objDBContext.SitePagesAndContents.FirstOrDefault(spc => spc.PageID == PreviewID);
                                    if (Title != null)
                                    {
                                        this.Page.Title = Title.PageTitle;
                                        this.Page.MetaKeywords = Title.MetaKeywords;
                                        this.Page.MetaDescription = Title.MetaDescription;
                                    }
                                }
                            }
                        }
                        ddlLanguage.SelectedValue = selectedLanguage;

                        var clientLogos = objDBContext.IndexDatas.Where(Idx => Idx.IndexDataTypeID == 3 && Idx.RecordStatus == DBKeys.RecordStatus_Active);

                        if (clientLogos.Count() > 0)
                        {
                            int ClientLogoSliderCount = int.Parse(ConfigurationManager.AppSettings["PartnerLogoSliderCount"]);
                            List<PageNumber> lstClientLogoPages = new List<PageNumber>();
                            int clientLogoPageCount = clientLogos.Count() / ClientLogoSliderCount;
                            clientLogoPageCount = clientLogos.Count() % ClientLogoSliderCount == 0 ? clientLogoPageCount : clientLogoPageCount + 1;
                            for (int idx = 0; idx < clientLogoPageCount; idx++)
                            {
                                lstClientLogoPages.Add(new PageNumber() { PageNo = idx });
                            }

                            rptrPartnerLogoCarousel.DataSource = lstClientLogoPages;
                            rptrPartnerLogoCarousel.DataBind();
                        }

                        //For Certificate
                        /*
                         *           */
                        var CertificateLogos = objDBContext.IndexDatas.Where(Idx => Idx.IndexDataTypeID == 1 && Idx.RecordStatus == DBKeys.RecordStatus_Active);

                        if (CertificateLogos.Count() > 0)
                        {
                            int CertificateLogoSliderCount = int.Parse(ConfigurationManager.AppSettings["CertificateLogoSliderCount"]);
                            List<PageNumber> lstCertificateLogoPages = new List<PageNumber>();
                            int CertificateLogoPageCount = CertificateLogos.Count() / CertificateLogoSliderCount;
                            CertificateLogoPageCount = CertificateLogos.Count() % CertificateLogoSliderCount == 0 ? CertificateLogoPageCount : CertificateLogoPageCount + 1;
                            for (int idx = 0; idx < CertificateLogoPageCount; idx++)
                            {
                                lstCertificateLogoPages.Add(new PageNumber() { PageNo = idx });
                            }

                            rptrCertificate.DataSource = lstCertificateLogoPages;
                            rptrCertificate.DataBind();
                        }
                        RandomClientLogo();
                    }
                }
                else
                {
                    Response.Redirect("/SiteDownForMaintenance.html", false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public void RandomClientLogo()
        {
            try
            {
                DateTime srt = DateTime.Now;
                var clientLogosList = objDBContext.pr_GetIndexData(4).ToList();
                rptrClientLogoCarousel.DataSource = clientLogosList;
                rptrClientLogoCarousel.DataBind();
                DateTime End = DateTime.Now;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void ShowRandomBannerImages()
        {
            try
            {
                if (Request.Url.ToString().ToLower().Contains("index.aspx"))
                {
                    sliderHomePage.Visible = true;
                    List<pr_GetRandomBannerImages_Result> bannersList = new List<pr_GetRandomBannerImages_Result>();
                    bannersList = objDBContext.pr_GetRandomBannerImages(true).OrderBy(a=>a.ImgPriority).ToList();
                    int bannerCount = int.Parse(ConfigurationManager.AppSettings["BannerCount"]);

                    if (bannersList.Count() < bannerCount)
                    {
                        int nonmand = bannerCount - bannersList.Count();
                        List<pr_GetRandomBannerImages_Result> bannersNonMandatory = new List<pr_GetRandomBannerImages_Result>();
                        bannersNonMandatory = objDBContext.pr_GetRandomBannerImages(false).Take(nonmand).ToList();
                        for (int i = 0; i < nonmand; i++)
                        { bannersList.Add(bannersNonMandatory[i]); }
                    }
                    rptrCanvasImages.DataSource = bannersList.OrderBy(bnr => bnr.ImgPriority);
                    rptrCanvasImages.DataBind();
                    rptrPaginationImages.DataSource = bannersList.OrderBy(bnr => bnr.ImgPriority);
                    rptrPaginationImages.DataBind();
                }
                else
                {
                    sliderHomePage.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrPartnerLogoCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                RepeaterItem item = e.Item;
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rptrChild = (Repeater)item.FindControl("rptrPartnerLogoInnerItems");
                    HiddenField hdnPageNumber = (HiddenField)item.FindControl("hdnPageNumber");
                    int PageNumber = int.Parse(hdnPageNumber.Value);
                    int ClientLogoSliderCount = int.Parse(ConfigurationManager.AppSettings["PartnerLogoSliderCount"]);
                    var clientLogos = objDBContext.IndexDatas.Where(Idx => Idx.IndexDataTypeID == 3 && Idx.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(idx => idx.Priority);
                    rptrChild.DataSource = clientLogos.Skip(PageNumber * ClientLogoSliderCount).Take(ClientLogoSliderCount);
                    rptrChild.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrCertificate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                RepeaterItem item = e.Item;
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rptrChild = (Repeater)item.FindControl("rptrCertificateInnerItems");
                    HiddenField hdnPageNumber = (HiddenField)item.FindControl("hdnCertificatePageNumber");
                    int PageNumber = int.Parse(hdnPageNumber.Value);
                    int CertificateLogoSliderCount = int.Parse(ConfigurationManager.AppSettings["CertificateLogoSliderCount"]);
                    var clientLogos = objDBContext.IndexDatas.Where(Idx => Idx.IndexDataTypeID == 1 && Idx.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(idx => idx.Priority);
                    rptrChild.DataSource = clientLogos.Skip(PageNumber * CertificateLogoSliderCount).Take(CertificateLogoSliderCount);
                    rptrChild.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string GetActiveClassForPageNumber(string pageNumber)
        {
            return pageNumber == "0" ? "active" : string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Membership.ValidateUser(txtLoginEmail.Value, txtLoginPassword.Value))
                {
                    FormsAuthentication.SetAuthCookie(txtLoginEmail.Value, true);
                    MembershipUser MSU = Membership.GetUser(txtLoginEmail.Value);
                    Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                    UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                    loginUser.InnerText = string.Format("Welcome {0}", upf.FirstName);
                    string RoleName = Roles.GetRolesForUser(txtLoginEmail.Value).First();
                    if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                    {
                        hrefloggedInLink.HRef = "/Admin/Dashboard.aspx?userid=" + MuID.ToString();
                        Response.Redirect("/Admin/Dashboard.aspx?userid=" + MuID.ToString(),false);
                    }
                    else
                    {
                        hrefloggedInLink.HRef = "/Customer/CloudPackages.aspx?userid=" + MuID.ToString() + "&showview=AvailableService";
                        Response.Redirect("/Customer/CloudPackages.aspx?userid=" + MuID.ToString() + "&showview=AvailableService",false);
                    }
                    //FormsAuthentication.RedirectFromLoginPage(txtLoginEmail.Value, true);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser mUser = Membership.CreateUser(txtEmailID.Text, txtPassword.Text);

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

                objDBContext.UserProfiles.AddObject(upf);

                UserSurveyRespons objsr = new UserSurveyRespons();
                objsr.CreatedBy = Guid.Parse(mUser.ProviderUserKey.ToString());
                objsr.CreatedOn = DateTime.Now;
                objsr.SurveyAnswer = ddlHearAboutUs.SelectedValue;
                objsr.SurveyQuestionID = 1;

                objDBContext.UserSurveyResponses.AddObject(objsr);

                objDBContext.SaveChanges();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnNewsLetter_Click(object sender, EventArgs e)
        {
            try
            {
                string CustomerEmails = string.Empty;
                NewsLetterSignUp objnls = new NewsLetterSignUp();

                var objEmails = objDBContext.NewsLetterSignUps.Where(A => A.ActiveStatus == 1);

                foreach (var res in objEmails)
                {
                    CustomerEmails += (res.EmailID.ToString() + ",");
                }
                CustomerEmails = CustomerEmails.Remove(CustomerEmails.LastIndexOf(","));

                if (CustomerEmails.Contains(newsletteremail.Value))
                {
                    newsletteremail.Value = "Email already exists";
                }
                else
                {
                    objnls.DateSignedUp = DateTime.Now;
                    objnls.EmailID = newsletteremail.Value;
              

                    objDBContext.NewsLetterSignUps.AddObject(objnls);
                    objDBContext.SaveChanges();

                    string UserEmailID = newsletteremail.Value;

                    int NewsSignupID = Convert.ToInt32(objDBContext.NewsLetterSignUps.Max(z => z.NewsLetterSignupID));
                    string NewsLetterLinkURL = "<a href=" + "http://wftcloud.com/user/newsletter.aspx?NewsLetterSignupID=" + NewsSignupID + ">Yes, subscribe me to this list.</a>";
                    string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewsLetterToAdmin"]));
                    AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string AdminContent = AdminEmailContent.Replace("++UserEmailID++", UserEmailID);

                    string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["NewsLetter"]));
                    CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string CustomerContent = CustomerEmailContent.Replace("++UserEmailID++", UserEmailID).Replace("++NewsLetterLink++", NewsLetterLinkURL);


                    SMTPManager.SendAdminNotificationEmail(AdminContent, "NewsLetter sign up request", false);
                    SMTPManager.SendEmail(CustomerContent, "WFTCloud Newsletter: Subscription Confirmed", UserEmailID, false, true);

                    newsletteremail.Value = "Thank you for signing up.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbtnMyCart_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                if (MSU != null)
                {
                    if (MSU.IsOnline)
                    {
                        Response.Redirect("/Customer/CloudPackages.aspx?userid=" + MSU.ProviderUserKey.ToString() + "&showview=ShowMyCart",false);
                    }
                    else
                    {
                        Response.Redirect("/LoginPage.aspx",false);
                    }
                }
                else
                {
                    Response.Redirect("/LoginPage.aspx",false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbtnLogOut_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
            }
            Response.Redirect("/Index.aspx", false);
        }
         
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedLanguage = ddlLanguage.SelectedValue;
            //Session["Language"] = SelectedLanguage;
            hdnLanguage.Value = SelectedLanguage;
            Response.Cookies["LanguageCookie"].Value = SelectedLanguage;
            Response.Redirect(Request.RawUrl,false);
        }

    }
}