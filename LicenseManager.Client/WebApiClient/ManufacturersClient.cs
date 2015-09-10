using LicenseManager.Client.Dtos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.WebApiClient
{
    public class ManufacturersClient : IDisposable
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        private HttpClient _client;

        public ManufacturersClient(string baseAddress) 
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ManufacturerDto>> GetManufacturers()
        {
            HttpResponseMessage response = await _client.GetAsync("api/manufacturers", HttpCompletionOption.ResponseContentRead);
            List<ManufacturerDto> manufacturers = await response.Content.ReadAsAsync<List<ManufacturerDto>>();
            return manufacturers;
        }

        public async Task<ManufacturerDetailDto> GetManufacturer(int id)
        {
            HttpResponseMessage response = await _client.GetAsync("api/manufacturers/" + id);
            ManufacturerDetailDto manufacturer = await response.Content.ReadAsAsync<ManufacturerDetailDto>();

            return manufacturer;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
