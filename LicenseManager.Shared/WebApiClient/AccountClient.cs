using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.WebApiClient
{
    public class AccountClient : AbstractWebApiClient, IDisposable
    {
        public AccountClient(string baseUrl)
            : base(baseUrl)
        {
        }

        public async Task<bool> Register(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterBindingModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            var response = await _client.PostAsJsonAsync("api/Account/Register", registerModel);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<TokenModel> Login(string username, string password)
        {
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
            HttpResponseMessage response = await _client.PostAsync("Token", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            }));
            if (!response.IsSuccessStatusCode)
            {
                string responseError = await response.Content.ReadAsStringAsync();
                return null;
            }

            TokenModel token = await response.Content.ReadAsAsync<TokenModel>();
            return token;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
