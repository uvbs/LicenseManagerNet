using LicenseManager.Shared.Models;
using LicenseManager.Shared.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LicenseManager.Web.Pages.Account
{
    public partial class Register : System.Web.UI.Page
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
            WritePageText();
        }

        private void WritePageText()
        {
            var master = Master as SiteMaster;
            if(master != null) { master.SetActiveMenuItem("Register"); }
            this.Title = "Login";
            pageHeader.InnerText = "Login";
            lblEmail.Text = "E-Mail";
            lblPassword.Text = "Password";
            lblConfirmPassword.Text = "Confirm Password";
            btnRegister.Text = "Register";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterUser(tbEmail.Text, tbPassword.Text, tbConfirmPassword.Text);
        }

        private async void RegisterUser(string username, string password, string confirmPassword)
        {
            using (var client = new AccountClient(GlobalProperties.BaseUrl))
            {
                var result = await client.Register(username, password, confirmPassword);
                if (result)
                {
                    Response.Redirect("~/Pages/Account/Login.aspx");
                }
            }
        }
    }
}