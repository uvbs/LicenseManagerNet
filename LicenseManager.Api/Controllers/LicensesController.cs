using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LicenseManager.Api.Controllers
{
    public class LicensesController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();

        public LicensesController() { }

        public LicensesController(ILicenseManagerContext context)
        {
            this._db = context;
        }
        
    }
}
