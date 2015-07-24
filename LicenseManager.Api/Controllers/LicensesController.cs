﻿using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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

        // GET: api/Licenses
        public IQueryable<License> GetLicenses()
        {
            return _db.Licenses;
        }

        // GET: api/Licenses/5
        [ResponseType(typeof(License))]
        public IHttpActionResult GetLicense(int id)
        {
            License license = _db.Licenses.Find(id);
            if(null == license)
            {
                return NotFound();
            }

            return Ok(license);
        }

        // GET: api/Licenses/BySoftwareId/5
        public IQueryable<License> GetLicensesBySoftwareId(int softwareId)
        {
            // TODO check software id
            return _db.Licenses.Where(l => l.SoftwareId == softwareId);
        }

        // PUT: api/Licenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLicense(int id, License license)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != license.Id)
            {
                return BadRequest();
            }

            _db.MarkAsModified(license);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Licenses
        [ResponseType(typeof(License))]
        public IHttpActionResult PostLicense(License license)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO implement check if license already exists (Key and SoftwareId)

            _db.Licenses.Add(license);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = license.Id }, license);
        }

        // DELETE: api/Licenses/5
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult DeleteLicense(int id)
        {
            License license = _db.Licenses.Find(id);
            if (license == null)
            {
                return NotFound();
            }

            _db.Licenses.Remove(license);
            _db.SaveChanges();

            return Ok(license);
        }

        private bool LicenseExists(int id)
        {
            return _db.Licenses.Count(e => e.Id == id) > 0;
        }
    }
}
