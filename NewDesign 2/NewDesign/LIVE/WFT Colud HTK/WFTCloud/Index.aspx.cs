using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using System.Configuration;
using System.Web.Security;
using WFTCloud.ResuableRoutines;
using System.Web.UI.HtmlControls;
using System.Text;
namespace WFTCloud
{
    public partial class Index : System.Web.UI.Page
    {
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Request.IsLocal && Request.IsSecureConnection)
                {
                    string redirectUrl = Request.Url.ToString().Replace("https:", "http:");
                    Response.Redirect(redirectUrl, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                if (!IsPostBack)
                {
                    //Add items to cart.
                    AddItemToCart();
                    //bind twitter feeds.
                    rptrTwitterFeeds.DataSource = objDBContext.SocialTweets.OrderByDescending(o => o.CreatedDate).Take(5);
                    rptrTwitterFeeds.DataBind();

                    var TabContent = objDBContext.ServiceCategories.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
                    List<ServiceCategory> lstServiceCategory = new List<ServiceCategory>();
                    foreach(ServiceCategory item in TabContent)
                    {
                        int CategoryID = item.ServiceCategoryID;
                        var CategoryContent = objDBContext.ServiceCatalogs.Where(a => a.ServiceCategoryID == CategoryID && a.UserSpecific == false && a.RecordStatus == DBKeys.RecordStatus_Active);
                        int count = CategoryContent.Count();
                        if (count > 0)
                        {
                            var MainTabContent = objDBContext.ServiceCategories.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active && svc.ServiceCategoryID == CategoryID).OrderBy(svc => svc.CategoryPriority);
                            lstServiceCategory.AddRange(MainTabContent.ToArray());
                        }
                    }
                    rptrServiceCategoryTab.DataSource = lstServiceCategory;
                    rptrServiceCategoryTab.DataBind();

                    rptrServiceCategoryTabContent.DataSource = objDBContext.ServiceCategories.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
                    rptrServiceCategoryTabContent.DataBind();
                    var videos = objDBContext.IndexDatas.Where(Idx => Idx.RecordStatus == 1 && Idx.IndexDataTypeID == 2).OrderBy(Idx => Idx.Priority);
                    rptrPhoneVideos.DataSource = rptrDesktopVideos.DataSource = videos;
                    rptrDesktopVideos.DataBind();

                    rptrPhoneVideos.DataBind();

                    var brochures = objDBContext.IndexDatas.Where(Idx => (Idx.IndexDataTypeID == 5 || Idx.IndexDataTypeID == 6) && Idx.RecordStatus == DBKeys.RecordStatus_Active);
                    
                    if (brochures.Count() > 0)
                    {
                        int BrochureSliderCount = int.Parse(ConfigurationManager.AppSettings["BrochureSliderCount"]);
                        List<PageNumber> lstBrochurePages = new List<PageNumber>();
                        int brochurePageCount = brochures.Count() / BrochureSliderCount;
                        brochurePageCount = brochures.Count() % BrochureSliderCount == 0 ? brochurePageCount : brochurePageCount + 1;
                        for (int idx = 0; idx < brochurePageCount; idx++)
                        {
                            lstBrochurePages.Add(new PageNumber() { PageNo = idx });
                        }

                        rptrBrochureCarousel.DataSource = lstBrochurePages;
                        rptrBrochureCarousel.DataBind();
                    }

                    var testimonials = objDBContext.pr_GetRandomTestimonialForIndex().ToList();

                    if (testimonials[0] != null)
                    {
                        ltTestimonial1.Text = testimonials[0].HomePageVersion.Length > 100 ? testimonials[0].HomePageVersion.Substring(0, 100) + "..." : testimonials[0].HomePageVersion + "...";
                        hlTestimonial1.Text = "- " + testimonials[0].CustomerName == null || testimonials[0].CustomerName == "" ? testimonials[0].Designation : testimonials[0].CustomerName + ",";
                        hlTestimonialOrg1.Text = testimonials[0].CustOrg;
                        hlTestimonial1.NavigateUrl = hyptest1.NavigateUrl = "/User/Testimonials.aspx#testimonial" + testimonials[0].TestimonialID;

                    }

                    if (testimonials[1] != null)
                    {
                        ltTestimonial2.Text = testimonials[1].HomePageVersion.Length > 100 ? testimonials[1].HomePageVersion.Substring(0, 100) + "..." : testimonials[1].HomePageVersion + "...";
                        hlTestimonial2.Text = "- " + testimonials[1].CustomerName == null || testimonials[1].CustomerName == "" ? testimonials[1].Designation : testimonials[1].CustomerName + ",";
                        hlTestimonialOrg2.Text = testimonials[1].CustOrg;
                        hlTestimonial2.NavigateUrl = hyptest2.NavigateUrl = "/User/Testimonials.aspx#testimonial" + testimonials[1].TestimonialID;
                    }

                    hlTestimonial2.NavigateUrl = hyptest2.NavigateUrl;
                    hlTestimonial1.NavigateUrl = hyptest1.NavigateUrl;

                    
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void AddItemToCart()
        {
            try
            {
                if (Request.QueryString["AddToCart"].IsValid())
                {

                         HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];

                         if (HttpContext.Current.User.Identity.IsAuthenticated && cookie != null)
                         {
                              MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                                 Guid ID = Guid.Parse(MSU.ProviderUserKey.ToString());
                                 var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID && a.UserRole.Contains("User"));
                                 if (user != null)
                                 {

                                     int UserProfileID = Convert.ToInt32(user.UserProfileID);
                                     UserCart objusercart = new UserCart();
                                     int ServiceID = int.Parse(Request.QueryString["AddToCart"]);
                                     bool PriceModel = Convert.ToBoolean(objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == ServiceID).IsPayAsYouGo);
                                     var UserServ = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID && ot.IsPayAsYouGo == PriceModel);
                                     if (UserServ != null)
                                     {
                                         int QunatityCount = UserServ.Quantity;
                                         UserServ.Quantity = QunatityCount + 1;
                                         UserServ.ModifiedOn = DateTime.Now;
                                         UserServ.ModifiedBy = ID;
                                     }
                                     else
                                     {
                                         objusercart.Quantity = 1;
                                         objusercart.ServiceID = ServiceID;
                                         objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                                         objusercart.CreatedOn = DateTime.Now;
                                         objusercart.CreatedBy = ID;
                                         objusercart.RecordStatus = 999;
                                         objusercart.IsPayAsYouGo = PriceModel;
                                         objusercart.SelectedDiscount = 0;
                                         objDBContext.UserCarts.AddObject(objusercart);
                                     }
                                     objDBContext.SaveChanges();
                                     string UserMembershipID = MSU.ProviderUserKey.ToString();
                                     Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=ShowMyCart"); 
                                 }
                                 else
                                 { Response.Redirect("/LoginPage.aspx", false); }
                         }
                         else
                         { Response.Redirect("/LoginPage.aspx", false); }
              }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrServiceCategoryTabContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                RepeaterItem item = e.Item;
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rptrChild = (Repeater)item.FindControl("rptrCategoryServices");
                    HiddenField hdnCategoryID = (HiddenField)item.FindControl("hdnCategoryID");
                    int categoryID = int.Parse(hdnCategoryID.Value);
                    rptrChild.DataSource = objDBContext.ServiceCatalogs.Where(svc => svc.ServiceCategoryID == categoryID && svc.UserSpecific == false && svc.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.Priority);
                    rptrChild.DataBind();
                   
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrBrochureCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                RepeaterItem item = e.Item;
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    Repeater rptrChild = (Repeater)item.FindControl("rptrBrochureInnerGroupItems");
                    HiddenField hdnPageNumber = (HiddenField)item.FindControl("hdnPageNumber");
                    int PageNumber = int.Parse(hdnPageNumber.Value);
                    int BrochureSliderCount = int.Parse(ConfigurationManager.AppSettings["BrochureSliderCount"]);
                    var brochures = objDBContext.IndexDatas.Where(Idx => (Idx.IndexDataTypeID == 5 || Idx.IndexDataTypeID == 6) && Idx.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(brch => brch.Priority);
                    rptrChild.DataSource = brochures.Skip(PageNumber * BrochureSliderCount).Take(BrochureSliderCount);
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

        public string GetBlinkClass(string ServiceID)
        {
            int servID = int.Parse(ServiceID);
            var service = objDBContext.ServiceCatalogs.FirstOrDefault(srv => srv.ServiceID == servID);
            if (service != null)
            {
                if (service.DisplayBlinking.HasValue && service.DisplayBlinking.Value)
                    return "blink_me";
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        public string GetActiveClassForCategoryTab(string categoryID)
        {
            int catID = int.Parse(categoryID);
            var category = objDBContext.ServiceCategories.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority).First();

            if (category.ServiceCategoryID == catID) 
                return "active"; 
            else 
                return string.Empty;
        }
       
    }

    public class PageNumber
    {
        public int PageNo { get; set; }
    }
}