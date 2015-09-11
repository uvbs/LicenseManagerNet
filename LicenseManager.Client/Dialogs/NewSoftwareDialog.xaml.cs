using LicenseManager.Client.Dtos;
using LicenseManager.Client.Enums;
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
    /// Interaction logic for NewSoftwareDialog.xaml
    /// </summary>
    public partial class NewSoftwareDialog : Window
    {
        public NewSoftwareDialog()
        {
            InitializeComponent();
            cmbGenre.ItemsSource = GetGenres();
            cmbManufacturer.ItemsSource = Global.Content.Manufacturers;
        }

        private IEnumerable<Genre> GetGenres()
        {
            var values = Enum.GetValues(typeof(Genre)).Cast<Genre>();
            return values;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SoftwareDetailDto software = new SoftwareDetailDto()
            {
                Name = txtName.Text,
                ManufacturerId = ((ManufacturerDto)cmbManufacturer.SelectedItem).Id,
                GenreId = (int)cmbGenre.SelectedItem,
                Description = null
            };

            bool? success;
            using (var client = new SoftwaresClient(Global.Properties.BaseUrl))
            {
                success = await client.PostSoftware(software);
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
