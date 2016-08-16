using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;

namespace WFTCloud.User
{
    public partial class express_cloud : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add items to cart.
            AddItemToCart();
            // Bind categories data
            ExpressCloud();
        }
        #endregion
        #region Control Events

        protected void rptrCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var myHidden = (HiddenField)e.Item.FindControl("hdnServiceCategoryID");
                int CatID = Convert.ToInt32(myHidden.Value);
                Repeater rptrWFTCloudPackages = (Repeater)e.Item.FindControl("rptrServices");
                    rptrWFTCloudPackages.DataSource = objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == CatID && cat.UserSpecific == false && cat.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrWFTCloudPackages.DataBind();
            }
        }

        #endregion
        #region Reusable Routines
        private void ExpressCloud()
        {
            /*List<ServiceCategory> Categories = new List<ServiceCategory>();
            var ExpressCategories = Regex.Split(ConfigurationManager.AppSettings["ExpressCategories"],",");
            foreach (var result in ExpressCategories)
            {
                int Categoryid = Convert.ToInt32(result);
                var category = objDBContext.ServiceCategories.FirstOrDefault(cat => cat.RecordStatus == DBKeys.RecordStatus_Active && cat.ServiceCategoryID == Categoryid);
                if (category != null)
                    Categories.Add(category);
            }
            */
            List<ServiceCategory> sercat = new List<ServiceCategory>();
            var serviceCateg = objDBContext.ServiceCategories.Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
            foreach (var result in serviceCateg)
            {
                if (objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == result.ServiceCategoryID && cat.UserSpecific == false && cat.RecordStatus == DBKeys.RecordStatus_Active).Count() > 0)
                {
                    sercat.Add(result);
                }
            }
            rptrCategories.DataSource = sercat;
            rptrCategories.DataBind();
        }

        private void AddItemToCart()
        {
            if (Request.QueryString["AddToCart"].IsValid())
            {
                MembershipUser MSU = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                if (MSU != null)
                {
                    if (MSU.IsOnline)
                    {
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
                        }
                        else
                        { Response.Redirect("/LoginPage.aspx",false); }
                    }
                    else
                    { Response.Redirect("/LoginPage.aspx", false); }
                }
                else
                {
                    Response.Redirect("/LoginPage.aspx", false);
                }
            }
        }
        #endregion
    }
}