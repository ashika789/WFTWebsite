using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFTCloud
{
    public partial class SampleSAPQuote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CloudAnalyticsQuote = System.Configuration.ConfigurationManager.AppSettings["CloudAnalyticsQuote"];
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=SampleWFTQuote.pdf");
            Response.TransmitFile(Server.MapPath(CloudAnalyticsQuote));
            Response.End();
        }
    }
}