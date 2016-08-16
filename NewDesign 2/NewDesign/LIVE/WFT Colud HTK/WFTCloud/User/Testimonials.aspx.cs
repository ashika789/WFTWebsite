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
    public partial class Testimonials : System.Web.UI.Page
    {
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var testimonials = objDBContext.Testimonials.Where(tst => tst.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(p=>p.Priority);
                    rptrTestimonial.DataSource = testimonials;
                    rptrTestimonial.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}