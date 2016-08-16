using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class Checkout : System.Web.UI.Page
    {
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HttpCookie objCartCookie = Request.Cookies["WFTCart"];

                    if (Request.QueryString["RemoveService"].IsValid())
                    {
                        objCartCookie.Values.Remove(Request.QueryString["RemoveService"]);
                        Response.Cookies.Add(objCartCookie);
                    }

                    if (objCartCookie == null)
                        rptrCartItems.Visible = false;
                    else
                    {
                        objCartCookie = Request.Cookies["WFTCart"];
                        List<int> serviceIds = new List<int>();
                        foreach (var keyID in objCartCookie.Values)
                        {
                            if (keyID != null)
                                serviceIds.Add(int.Parse(keyID.ToString()));
                        }

                        List<ServiceCatalog> servicesToBuy = new List<ServiceCatalog>();
                        decimal serviceTotal = 0.00M;

                        foreach (int serviceID in serviceIds)
                        {
                            var selService = objDBContext.ServiceCatalogs.FirstOrDefault(serv => serv.ServiceID == serviceID);
                            if (selService != null)
                            {
                                servicesToBuy.Add(selService);
                                serviceTotal += selService.InitialHoldAmount.Value;
                            }
                        }

                        rptrCartItems.DataSource = servicesToBuy;
                        rptrCartItems.DataBind();

                        lblTotal.Text = serviceTotal.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}