using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.Models;
using LicenseManager.Shared.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LicenseManager.Web.Pages
{
    public partial class SoftwareDetails : System.Web.UI.Page
    {
        private int _id = -1;
        private string _pageAction = "";
        private SoftwareDetailDto _software = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session["IsLoggedIn"])
            {
                // TODO fehler anzeigen oder auf Login Seite weiterleiten
            }
            var id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    _id = int.Parse(id);
                }
                catch (InvalidCastException ex)
                {

                }
            }
            _pageAction = Request.QueryString["pageAction"];
            GetSoftwareDetails(_id);
        }

        private async void GetSoftwareDetails(int id)
        {
            using (var client = new SoftwaresClient(GlobalProperties.BaseUrl,
                (TokenModel)Session["Token"]))
            {
                _software = await client.GetSoftware(id);
            }
            if (_software == null)
            {
                return;
            }
            lblSoftwareTitle.Text = _software.ToString();
        }
    }
}