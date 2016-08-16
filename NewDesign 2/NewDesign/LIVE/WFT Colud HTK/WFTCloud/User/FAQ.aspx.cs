using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class FAQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    WFTCloud.DataAccess.cgxwftcloudEntities objDBContext = new DataAccess.cgxwftcloudEntities();
                    rptrGeneralFAQ.DataSource = objDBContext.FAQs.Where(faq => faq.FAQCategoryTypeID == 1 && faq.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrGeneralFAQ.DataBind();
                    rptrTechnical.DataSource = objDBContext.FAQs.Where(faq => faq.FAQCategoryTypeID == 2 && faq.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrTechnical.DataBind();
                    rptrSales.DataSource = objDBContext.FAQs.Where(faq => faq.FAQCategoryTypeID == 3 && faq.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrSales.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}