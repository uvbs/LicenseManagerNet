using LicenseManager.Client.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.WebApiClient
{
    public class SoftwaresClient : AbstractWebApiClient, IDisposable
    {
        public SoftwaresClient(string baseAddress)
            : base(baseAddress)
        { }

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
                throw new NotImplementedException();
            }
            HttpResponseMessage response = await _client.GetAsync("api/Softwares/" + id);
            SoftwareDetailDto software = await response.Content.ReadAsAsync<SoftwareDetailDto>();
            return software;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
