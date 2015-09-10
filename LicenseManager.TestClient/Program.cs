using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Test().Wait();
            Console.WriteLine("Test finished");
            Console.ReadKey();
        }

        private static async Task Test()
        {
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44300");
                client.BaseAddress = new Uri("https://lm.twerner.info");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine(client.BaseAddress);

                var response = client.GetAsync("api/softwares/1");
                Console.WriteLine(response.Result.StatusCode);
                Console.WriteLine(await response.Result.Content.ReadAsStringAsync());
            }
        }
    }
}
