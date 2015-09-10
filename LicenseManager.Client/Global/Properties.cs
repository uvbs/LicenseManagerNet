using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.Global
{
    public class Properties
    {
        public static string BaseUrl;
        public static readonly string DevUrl = "http://localhost:2453/";
        public static readonly string DevUrlSsl = "https://localhost:44300/";
        public static readonly string LiveUrl = "https://lm.twerner.info";
    }
}
