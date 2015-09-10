using LicenseManager.Client.Dtos;
using LicenseManager.Client.WebApiClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LicenseManager.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            Startup();
        }

        public async void Startup()
        {
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            
            //TEST
            Global.Properties.AuthenticationToken = new TokenModel
            {
                AccessToken = "le691iwe1aOphYj-1ECKS0r6eNr91teHcb6gkOCBG1bqdfDUpCXvQFf1aBnBYUjdflmZFYJHaA9yH2QaoFYmM7YXHpdVDskazOXT9_v-PsdpEnUJHNnmG4UBQTcyFz8rMOMFbb9i_dJ2Q9kGfWwV2t3YgsWVc_lv6ZqVQxc7asDBaZWhD0YjxQEbTNefCTU6DcvJOp66ngT_kxo_Ppoui6WCJB92MRO1c-xe7TLfiMHi_kHyJoXOVSSIkRaqW7CgYv6n6AxFj5QQ0KhHDMYY-wkoLOGgU_vqwG3yXaj8uzHryvuVNEL4htSvLs7X5nN-8pFRhGjEkozqVVuqn2zCbOIV_9Ir-wZ9nQEAdLVr4xY1pfY6UcVYfm3em7J2ePyp0VnVShZ1a5d6ckTvCJ1erjRzWT7_4sGSDKX4UOVO6oKtrXd5RPM0U0-y4afD8Lrouks9qTTGhELeJV0nGD-ipoVOiDNWTjWDVFQFUZAba90"
            };

            Global.Properties.BaseUrl = Global.Properties.DevUrlSsl;

            await LoadContent();

            lst1.Items.Add("Test");

            RefreshData();
        }

        private void RefreshData()
        {
            if (Global.Content.Softwares.Count > 0)
            {
                lst1.Items.Clear();
                foreach (var item in Global.Content.Softwares)
                {
                    lst1.Items.Add(item);
                }
            }
        }

        private async Task LoadContent()
        {
            using (var client = new ManufacturersClient(Global.Properties.BaseUrl))
            {
                Global.Content.Manufacturers = await client.GetManufacturers();
            }

            using (var client = new SoftwaresClient(Global.Properties.BaseUrl))
            {
                Global.Content.Softwares = await client.GetSoftwares();
            }
        }

        private async void lst1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst1.SelectedItem != null)
            {
                SoftwareDetailDto software = null;
                using(var client = new SoftwaresClient(Global.Properties.BaseUrl))
                {
                    software = await client.GetSoftware(((SoftwareDto)lst1.SelectedItem).Id);
                }
                SoftwareDetailGrid.Visibility = System.Windows.Visibility.Visible;
                lblTitle.Content = lst1.SelectedItem.ToString();
                txtManufacturer.Text = software.ManufacturerName;
                txtName.Text = software.Name;
                txtGenre.Text = software.GenreName;
                txtDescription.Text = software.Description;
            }
        }
    }
}
