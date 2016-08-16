using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;

namespace WFTCloud
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        cgxwftcloudEntities obc = new cgxwftcloudEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView gv = new GridView();
            gv.DataSource = obc.UserProfiles;
            gv.DataBind();
            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment; filename=1.xls");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            gv.RenderControl(hw);

            Response.Write(tw.ToString());
            Response.End();
        }
    }
}