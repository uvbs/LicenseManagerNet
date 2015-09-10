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
            
            Global.Properties.BaseUrl = Global.Properties.DevUrlSsl;
            await Login();
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


        //TODO Testcode
        private async Task Login()
        {
            using (var client = new AccountClient())
            {
                TokenModel token = await client.Login("test@to-wer.de", "Test123;");
                if(token != null)
                {
                    Global.Properties.AuthenticationToken = token;
                    _logger.Debug(token.ExpiresAt);
                    _logger.Debug(Global.Properties.AuthenticationToken.AccessToken);
                    _logger.Debug(Global.Properties.AuthenticationToken.ExpiresIn);
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
                DataGrid1.ItemsSource = software.Licenses;
            }
        }
    }
}
