using LicenseManager.Client.Dialogs;
using LicenseManager.Client.Dtos;
using LicenseManager.Client.Enums;
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

            RefreshData();
        }

        private async void RefreshData()
        {
            SoftwareDetailGrid.Visibility = System.Windows.Visibility.Hidden;
            btnDeleteSoftware.IsEnabled = false;
            btnNewLicense.IsEnabled = false;
            await LoadContent();
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

        private void lst1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst1.SelectedItem != null)
            {
                ShowSoftwareDetails((SoftwareDto)lst1.SelectedItem);
            }
        }

        private async void ShowSoftwareDetails(SoftwareDto sw)
        {
            SoftwareDetailDto software = null;
            using (var client = new SoftwaresClient(Global.Properties.BaseUrl))
            {
                software = await client.GetSoftware(sw.Id);
            }
            if (software == null)
            {
                return;
            }

            SoftwareDetailGrid.Visibility = System.Windows.Visibility.Visible;
            lblTitle.Content = sw.ToString();
            txtManufacturer.Text = software.ManufacturerName;
            txtName.Text = software.Name;
            txtGenre.Text = software.GenreName;
            txtDescription.Text = software.Description;
            DataGrid1.ItemsSource = software.Licenses;
            btnNewLicense.IsEnabled = true;
            btnDeleteSoftware.IsEnabled = true;
        }

        private void btnNewSoftware_Click(object sender, RoutedEventArgs e)
        {
            NewSoftwareDialog dialog = new NewSoftwareDialog();
            dialog.ShowDialog();
            Thread.Sleep(2000);
            RefreshData();
        }

        private void btnNewLicense_Click(object sender, RoutedEventArgs e)
        {
            SoftwareDto sw = (SoftwareDto)lst1.SelectedItem;
            NewLicenseDialog dialog = new NewLicenseDialog(sw.Id);
            dialog.ShowDialog();
            Thread.Sleep(2000);
            ShowSoftwareDetails(sw);
        }

        private async void btnDeleteSoftware_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this software?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            bool success = false;
            using (var client = new SoftwaresClient(Global.Properties.BaseUrl))
            {
                // TODO get software id from anywhere else
                success = await client.DeleteSoftware(((SoftwareDto)lst1.SelectedItem).Id);
            }

            if (!success)
            {
                return;
            }
            Thread.Sleep(1000);
            RefreshData();
        }
    }
}
