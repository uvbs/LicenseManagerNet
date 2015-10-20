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
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((bool)Session["IsLoggedIn"] == true)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                WritePageText();
            }
        }

        private void WritePageText()
        {
            //var item = (HtmlGenericControl)Master.FindControl("liLogin");
            //item.Attributes["class"] = "active";

            var master = Master as SiteMaster;
            if (master != null)
            {
                master.SetActiveMenuItem("Login");
            }

            this.Title = "Login";
            pageHeader.InnerText = "Login";
            lblEmail.Text = "E-Mail";
            lblPassword.Text = "Password";
            btnDoLogin.Text = "Login";
            btnAdminLogin.Text = "Admin Login";
            btnTwLogin.Text = "tw@to-wer.de";
        }

        protected void btnDoLogin_Click(object sender, EventArgs e)
        {
                var email = tbEmail.Text;
                var password = tbPassword.Text;
                DoLogin(email, password);
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
            Session.Add("Token", token);
            Response.Redirect("~/Default.aspx");
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            DoLogin("admin@to-wer.de", "Admin123;");
        }

        protected void btnTwLogin_Click(object sender, EventArgs e)
        {
            DoLogin("tw@to-wer.de", "Test123;");
        }
    }
}