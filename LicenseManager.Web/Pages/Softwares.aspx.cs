using LicenseManager.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LicenseManager.Web.Pages
{
    public partial class Softwares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WritePageText();
            GetSoftwares();
        }

        #region page methods

        private void WritePageText()
        {
            thcId.Text = "#";
            thcManufacturer.Text = "Manufacturer";
            thcName.Text = "Name";
        }

        #endregion page methods

        private async void GetSoftwares()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://lmapi.local/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));

                //lblTest.Text = client.BaseAddress.ToString();

                var response = await client.GetAsync("api/softwares");

                var softwaresList = await response.Content.ReadAsAsync<List<SoftwareDto>>();
                foreach (var item in softwaresList)
                {
                    var tr = new TableRow();
                    tr.Cells.Add(new TableCell()
                    {
                        Text = item.Id.ToString()
                    });
                    tr.Cells.Add(new TableCell()
                    {
                        Text = item.ManufacturerName
                    });
                    tr.Cells.Add(new TableCell()
                    {
                        Text = item.Name
                    });
                    tblSoftwares.Rows.Add(tr);
                }
            }
        }

        private bool SoftwareDto()
        {
            throw new NotImplementedException();
        }
    }
}