using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFTCloud.Customer
{
    public partial class LaunchService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string LaunchUrl = Convert.ToString(Session["LaunchURL"]);
            iframe.Attributes["src"] = LaunchUrl;
            Session["LaunchURL"] = null;
        }
    }
}