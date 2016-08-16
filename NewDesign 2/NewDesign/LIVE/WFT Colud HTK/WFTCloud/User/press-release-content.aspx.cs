using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class press_release_content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    WFTCloud.DataAccess.cgxwftcloudEntities objDBContext = new DataAccess.cgxwftcloudEntities();
                    rptrPressRelease.DataSource = objDBContext.PressReleases.Where(pr => pr.RecordStatus == DBKeys.RecordStatus_Active).OrderByDescending(date => date.PressReleaseDate);
                    rptrPressRelease.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}