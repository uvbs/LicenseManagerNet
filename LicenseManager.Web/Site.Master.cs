using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LicenseManager.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                Session.Add("IsLoggedIn", false);
                Session.Add("CurrentSoftware", -1);
                Session.Add("PageAction", "");
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session["IsLoggedIn"])
            {
                hlLogin2.Visible = true;
            }
        }
    }
}