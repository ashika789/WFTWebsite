using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using System.Configuration;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class download_SAP_cloud_computing_brochures : System.Web.UI.Page
    {
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
    }
}