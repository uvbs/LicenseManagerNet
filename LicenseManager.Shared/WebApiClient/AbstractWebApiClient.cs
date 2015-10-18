
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.WebApiClient
{
    public abstract class AbstractWebApiClient
    {
        protected HttpClient _client;
        private TokenModel _authToken;

        public AbstractWebApiClient(string baseAddress, TokenModel authToken = null)
        {
            _authToken = authToken;
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (authToken != null)
            {
                _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", authToken.AccessToken));
            }
        }

        protected bool CheckAuthorization()
        {
            if (_authToken == null)
            {
                return false;
            }

            // TODO implement check if token is valid

            return true;
        }
    }
}
