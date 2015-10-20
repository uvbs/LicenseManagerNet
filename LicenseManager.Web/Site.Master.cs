using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LicenseManager.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        { 
            // TODO: refactor
            if (!(bool)Session["IsLoggedIn"])
            {
                lbtLogin.Visible = true;
                lbtRegister.Visible = true;
                lbtLogout.Visible = false;
                lbtUser.Visible = false;
            }

            if ((bool)Session["IsLoggedIn"])
            {
                lbtLogin.Visible = false;
                lbtRegister.Visible = false;
                lbtLogout.Visible = true;
                lbtUser.Visible = true;
                lbtUser.Text = ((TokenModel)Session["Token"]).Username;
            }
        }



        protected void lbtLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        protected void lbtLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Account/Login.aspx");
        }

        // TEST
        public void SetActiveMenuItem(string menuItem)
        {
            var item = (HtmlGenericControl)FindControl("li" + menuItem);
            if (item != null) {
                item.Attributes["class"] = "active";
            }
        }

        protected void lbtRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Account/Register.aspx");
        }
    }
}