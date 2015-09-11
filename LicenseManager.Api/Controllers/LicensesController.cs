using LicenseManager.Api.ViewModels;
using LicenseManager.Shared;
using LicenseManager.Shared.Models;
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
    [Authorize]
    [RoutePrefix("api/Licenses")]
    public class LicensesController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();

        public LicensesController() { }

        public LicensesController(ILicenseManagerContext context)
        {
            this._db = context;
        }

        // GET: api/Licenses
        public IQueryable<LicenseViewModel> GetLicenses()
        {
            var licenses = _db.Licenses
                .Select(l => new LicenseViewModel()
                {
                    Id = l.LicenseId,
                    //Software = l.Software.Manufacturer.Name + " " + l.Software.Name,
                    Edition = l.Edition,
                    ActivationKey = l.ActivationKey,
                    VolumeLicense = (l.VolumeLicense == 1)
                });
            return licenses;
        }

        // GET: api/softwares/{softwareId}/licenses
        [HttpGet, Route("~/api/softwares/{softwareId}/licenses")]
        public IQueryable<LicenseViewModel> GetLicensesBySoftwareId(int softwareId)
        {
            if (_db.Softwares.Find(softwareId) == null)
            {
                return null;
            }
            var licenses = _db.Licenses.Where(l => l.SoftwareId == softwareId)
                .Select(l => new LicenseViewModel()
                {
                    Id = l.LicenseId,
                    //Software = l.Software.Manufacturer.Name + " " + l.Software.Name,
                    Edition = l.Edition,
                    ActivationKey = l.ActivationKey,
                    VolumeLicense = (l.VolumeLicense == 1)
                });

            return licenses;
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

        // PUT: api/Licenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLicense(int id, License license)
        {
            license.ModificationDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != license.LicenseId)
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
            license.CreationDate = DateTime.Now;
            _db.Licenses.Add(license);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = license.LicenseId }, license);
        }

        // DELETE: api/Licenses/5
        [ResponseType(typeof(License))]
        [Route("{id:int}")]
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
            return _db.Licenses.Count(e => e.LicenseId == id) > 0;
        }
    }
}
