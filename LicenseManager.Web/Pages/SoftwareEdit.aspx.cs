using LicenseManager.Shared.Dtos;
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
    public partial class SoftwareEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WritePageText();
            fillDdlManufacturer();
            fillDdlGenres();
        }

        private void WritePageText()
        {
            var master = Master as SiteMaster;
            if (master != null)
            {
                master.SetActiveMenuItem("Softwares");
            }
            lblManufacturer.Text = "Manufacturer";
            lblName.Text = "Name";
            lblGenre.Text = "Genre";
            lblDescription.Text = "Description";
        }

        private async void fillDdlManufacturer()
        {
            var manufacturers = await GetManufacturers();
            ddlManufacturer.DataSource = manufacturers;
            ddlManufacturer.DataBind();
        }

        private async void fillDdlGenres()
        {
            var genres = await GetGenres();
            ddlGenre.DataSource = genres;
            ddlGenre.DataBind();
        }

        private async Task<List<ManufacturerDto>> GetManufacturers()
        {
            using (var client = new ManufacturersClient(GlobalProperties.BaseUrl))
            {
                var result = await client.GetManufacturers();
                return result;
            }
        }

        private async Task<List<GenreDto>> GetGenres()
        {
            using (var client = new GenresClient(GlobalProperties.BaseUrl))
            {
                var result = await client.GetGenres();
                return result;
            }
        }
    }
}