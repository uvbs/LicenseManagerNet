using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LicenseManager.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var item = (HtmlGenericControl)Master.FindControl("liHome");
            if (item != null) { item.Attributes["class"] = "active"; }
        }
    }
}