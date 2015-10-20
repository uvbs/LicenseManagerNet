using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.Models;
using LicenseManager.Shared.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // TODO set menu item active
            if(null == Session["IsLoggedIn"]) { Response.Redirect("~/Default.aspx"); }
            if (!(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Pages/Account/Login.aspx");
            }
            var id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    _id = int.Parse(id);
                }
                catch (InvalidCastException ex)
                {

                }
            }
            WritePageText();
            ShowSoftware();
        }

        private void WritePageText()
        {
            btnBack.Text = "Back";
            btnEdit.Text = "Edit";

            tcManufacturer.Text = "Manufacturer";
            tcName.Text = "Name";
            tcGenre.Text = "Genre";
            tcDescription.Text = "Description";

            thcLicensesId.Text = "#";
            thcLicensesEdition.Text = "Edition";
            thcLicensesKey.Text = "Key";
            thcLicensesVolume.Text = "VL";
        }

        private async void ShowSoftware()
        {
            await GetSoftwareDetails(_id);
            if (_software == null)
            {
                return;
            }
            tcManufacturerValue.Text = _software.ManufacturerName;
            tcNameValue.Text = _software.Name;
            tcGenreValue.Text = _software.GenreName;
            tcDescriptionValue.Text = _software.Description;
            tblSoftwareDetails.Visible = true;

            if(_software.Licenses == null)
            {
                return;
            }
            tblLicenses.Visible = true;
            foreach (var item in _software.Licenses)
            {
                var tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = item.Id.ToString() });
                tr.Cells.Add(new TableCell() { Text = item.Edition });
                tr.Cells.Add(new TableCell() { Text = item.ActivationKey });
                tr.Cells.Add(new TableCell() { Text = item.VolumeLicense.ToString() });
                tblLicenses.Rows.Add(tr);
            }
        }

        private async Task GetSoftwareDetails(int id)
        {
            if (Session["Token"] == null)
            {
                Session["IsLoggedIn"] = false;
                Response.Redirect("~/Default.aspx");
            }
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