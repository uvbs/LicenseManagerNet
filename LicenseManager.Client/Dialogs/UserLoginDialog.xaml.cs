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
    /// Interaktionslogik für UserLoginDialog.xaml
    /// </summary>
    public partial class UserLoginDialog : Window
    {
        public UserLoginDialog()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            TokenModel token = null;
            using (var client = new AccountClient())
            {
                token = await client.Login(txtUsername.Text, txtPassword.Text);
            }
            if(token == null)
            {
                return;
            }
            if (String.IsNullOrWhiteSpace(token.AccessToken))
            {
                return;
            }
            Global.Properties.AuthenticationToken = token;
            this.Close();
        }
    }
}
