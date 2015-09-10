using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.WebApiClient
{
    public abstract class AbstractWebApiClient
    {
        protected HttpClient _client;
        protected Logger _logger;

        public AbstractWebApiClient(string baseAddress)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (Global.Properties.AuthenticationToken != null)
            {
                _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", Global.Properties.AuthenticationToken.AccessToken));
            }
        }

        protected bool CheckAuthorization()
        {
            if (Global.Properties.AuthenticationToken == null)
            {
                return false;
            }

            return true;
        }
    }
}
