using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class PreviewContent : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.PreviewID].IsValid())
                {
                    int PreviewID = int.Parse(Request.QueryString[QueryStringKeys.PreviewID]);
                    var Content = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageID == PreviewID);
                    Literal1.Text = Content.PreviewHTMLContent;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}