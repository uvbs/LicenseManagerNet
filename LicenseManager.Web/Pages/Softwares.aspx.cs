using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LicenseManager.Web.Pages
{
    public partial class Softwares : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var item = (HtmlGenericControl)Master.FindControl("liSoftwares");
            if (item != null) { item.Attributes["class"] = "active"; }
            GetSoftwares();
            WritePageText();
        }

        #region page methods

        private void WritePageText()
        {
            this.Title = "Softwares";
            thcId.Text = "#";
            thcManufacturer.Text = "Manufacturer";
            thcName.Text = "Name";
            thcButtons.Text = "&nbsp;";
        }

        #endregion page methods

        private async void GetSoftwares()
        {
            using (var client = new SoftwaresClient(GlobalProperties.BaseUrl))
            {
                var softwares = await client.GetSoftwares();
                foreach (var item in softwares)
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
                    tr.Cells.Add(CreateLinks(item));
                    tblSoftwares.Rows.Add(tr);
                }
            }
        }

        private TableCell CreateLinks(SoftwareDto software)
        {
            var tc = new TableCell();
            var btnDetail = new Button
            {
                Text = "Details",
                CssClass = "btn btn-info",
                ID = "btnDetail_" + software.Id,
                PostBackUrl = "SoftwareDetails.aspx?id=" + software.Id + "&pageAction=Detail"
            };

            var btnEdit = new Button { ID = "btnEdit_" + software.Id, Text = "Edit", CssClass = "btn btn-warning" };
            var btnDelete = new Button { Text = "Delete", CssClass="btn btn-danger" };
            
            tc.Controls.Add(btnDetail);
            tc.Controls.Add(new Label() { Text = " " });
            tc.Controls.Add(btnEdit);
            tc.Controls.Add(new Label() { Text = " " });
            tc.Controls.Add(btnDelete);
            return tc;
        }
    }
}