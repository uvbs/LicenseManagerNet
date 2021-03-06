﻿using LicenseManager.Shared;
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
    [Authorize]
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
        [Route("")]
        public IQueryable<Computer> GetComputers()
        {
            return _db.Computers;
        }

        // GET: api/{userId}/Computers}
        [Route("~/api/{userId}/Computers")]
        public IQueryable<Computer> GetComputers(string userId)
        {
            //var computers = _db.Computers.Where(c => c.UserId.Equals(userId, StringComparison.CurrentCultureIgnoreCase));
            var computers = _db.Computers.Where(c => c.UserId == userId);
            if (computers == null)
            {
                throw new ArgumentNullException();
            }
            return computers;
        }

        // GET: api/Computers/{id}
        [Route("{id:int}")]
        [ResponseType(typeof(Computer))]
        public IHttpActionResult GetComputer(int id)
        {
            Computer computer = _db.Computers.Find(id);
            if (computer == null)
            {
                return NotFound();
            }
            return Ok(computer);
        }

        // GET: api/Computers/{id}/Installed
        [Route("{id}/Installed")]
        public string GetInstalledLicenses(int id)
        {
            var usedons = _db.UsedOns.Where(u => u.ComputerId == id);
            return usedons.Count().ToString();
        }

        // TODO PUT install license on host
    }
}
