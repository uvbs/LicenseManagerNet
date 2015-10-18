using LicenseManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.WebApiClient
{
    public class LicensesClient : AbstractWebApiClient, IDisposable
    {
        public LicensesClient(string baseUrl)
            : base(baseUrl)
        {
        }

        public async Task<bool> PostLicense(LicenseDto license)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<LicenseDto>("api/licenses", license);
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> PutLicense(int id, LicenseDetailDto license)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteLicense(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/licenses/" + id);
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
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
