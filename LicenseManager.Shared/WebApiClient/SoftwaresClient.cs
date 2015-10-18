using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.Exceptions;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.WebApiClient
{
    public class SoftwaresClient : AbstractWebApiClient, IDisposable
    {
        public SoftwaresClient(string baseAddress)
            : base(baseAddress)
        {
        }

        public SoftwaresClient(string baseAddress, TokenModel authToken)
            : base (baseAddress, authToken)
        {

        }

        public async Task<List<SoftwareDto>> GetSoftwares()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Softwares");
            List<SoftwareDto> softwares = await response.Content.ReadAsAsync<List<SoftwareDto>>();
            return softwares;
        }

        public async Task<SoftwareDetailDto> GetSoftware(int id)
        {
            if (!CheckAuthorization())
            {
                throw new NotLoggedInException();
            }
            HttpResponseMessage response = await _client.GetAsync("api/Softwares/" + id);
            SoftwareDetailDto software = await response.Content.ReadAsAsync<SoftwareDetailDto>();
            return software;
        }

        public async Task<bool> PostSoftware(SoftwareDetailDto software)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<SoftwareDetailDto>("api/Softwares", software);
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteSoftware(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/Softwares/" + id);
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
