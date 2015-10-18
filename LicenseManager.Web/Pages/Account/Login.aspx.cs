using LicenseManager.Shared.Models;
using LicenseManager.Shared.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LicenseManager.Web.Pages.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Init(object sener, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((bool)Session["IsLoggedIn"] == true)
            {
                Response.Redirect("Default.aspx");
            }
            WritePageText();
            var item = (HtmlGenericControl)Master.FindControl("liLogin");
            item.Attributes["class"] = "active";
        }

        private void WritePageText()
        {
            this.Title = "Login";
            pageHeader.InnerText = "Login";
            lblEmail.Text = "E-Mail";
            lblPassword.Text = "Password";
            btnDoLogin.Text = "Login";
        }

        protected void btnDoLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var email = tbEmail.Text;
                var password = tbPassword.Text;
                DoLogin(email, password);
            }
            catch (Exception ex)
            {

            }
        }

        private async void DoLogin(string email, string password)
        {
            TokenModel token = null;
            using (var client = new AccountClient(GlobalProperties.BaseUrl))
            {
                token = await client.Login(email, password);
            }
            if (token == null)
            {
                return;
            }
            Session["IsLoggedIn"] = true;
            Session["Username"] = email;
            Session.Add("Token", token);
            Response.Redirect("~/Default.aspx");
        }
    }
}