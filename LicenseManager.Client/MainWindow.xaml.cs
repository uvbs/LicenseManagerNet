using LicenseManager.Client.Dtos;
using LicenseManager.Client.WebApiClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Global.Properties.BaseUrl = Global.Properties.DevUrl;

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
    }
}
