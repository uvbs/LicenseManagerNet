using LicenseManager.Client.Dtos;
using LicenseManager.Client.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LicenseManager.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for NewLicenseDialog.xaml
    /// </summary>
    public partial class NewLicenseDialog : Window
    {
        public int SoftwareId { get; set; }

        public NewLicenseDialog(int softwareId)
        {
            InitializeComponent();
            this.SoftwareId = softwareId;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LicenseDetailDto license = new LicenseDetailDto
            {
                SoftwareId = SoftwareId,
                Edition = txtEdition.Text,
                ActivationKey = txtActivationKey.Text,
                VolumeLicense = false
            };

            bool? success;
            using (var client = new LicensesClient())
            {
                success = await client.PostLicense(license);
            }
            if (success == null)
            {
                throw new NotImplementedException();
            }
            if (!success.HasValue) { throw new NotImplementedException(); }
            if (!success.Value) { throw new NotImplementedException(); }
            this.Close();
        }
    }
}
