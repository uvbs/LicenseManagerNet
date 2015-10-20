using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.WebApiClient
{
    public class GenresClient : AbstractWebApiClient, IDisposable
    {

        public GenresClient(string baseUrl, TokenModel token = null)
            : base(baseUrl)
        {
           
        }

        public async Task<List<GenreDto>> GetGenres()
        {
            var response = await _client.GetAsync("api/Genres");
            var result = await response.Content.ReadAsAsync<List<GenreDto>>();
            return result;
        }


        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
