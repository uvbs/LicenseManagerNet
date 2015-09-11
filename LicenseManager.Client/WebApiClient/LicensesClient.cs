using LicenseManager.Client.Dtos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.WebApiClient
{
    public class LicensesClient : AbstractWebApiClient, IDisposable
    {
        public LicensesClient()
            : base(Global.Properties.BaseUrl)
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<bool> PostLicense(LicenseDto license)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<LicenseDto>("api/licenses", license);
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                _logger.Error(responseMessage);
                return false;
            }

            return true;
        }


        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
