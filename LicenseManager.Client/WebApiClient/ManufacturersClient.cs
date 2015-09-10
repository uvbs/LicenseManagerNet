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

        public async Task GetManufacturers()
        {
            LOGGER.Debug("Try getting manufacturers");
            HttpResponseMessage response = await _client.GetAsync("api/manufacturers", HttpCompletionOption.ResponseContentRead);
            LOGGER.Debug("reponse status code: " + response.StatusCode);

            Global.Content.Manufacturers = await response.Content.ReadAsAsync<List<ManufacturerDto>>();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
