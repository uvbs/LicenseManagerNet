using LicenseManager.Shared;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LicenseManager.Api.Controllers
{
    [RoutePrefix("api/Computers")]
    public class ComputersController : ApiController
    {
        private ILicenseManagerContext _db;

        public ComputersController()
        {
            _db = new LicenseManagerContext();
        }

        public ComputersController(ILicenseManagerContext context)
        {
            this._db = context;
        }

        // GET: api/Computers
        [Route("api/computers")]
        public IQueryable<Computer> GetComputers()
        {
            // TODO should return only the computers of current user?
            return _db.Computers;
        }

        [Route("api/computers/{userId}")]
        public IQueryable<Computer> GetComputers(string userId)
        {
            return _db.Computers.Where(c => c.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
