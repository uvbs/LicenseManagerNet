using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LicenseManager.Api.Controllers
{
    public class SoftwaresController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();

        public SoftwaresController() { }

        public SoftwaresController(ILicenseManagerContext context)
        {
            _db = context;
        }

        // GET api/Softwares
        public IQueryable<Software> GetSoftwares()
        {
            return _db.Softwares;
        }

        // GET api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult GetSoftware(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Softwares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSoftware(int id, Software manufacturer)
        {
            throw new NotImplementedException();
        }

        // POST: api/Softwares
        [ResponseType(typeof(Software))]
        public IHttpActionResult PostSoftware(Software software)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult DeleteSoftware(int id)
        {
            throw new NotImplementedException();
        }
    }
}
