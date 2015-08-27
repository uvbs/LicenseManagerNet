using System.Data.Entity.Infrastructure;
using LicenseManager.Shared;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using LicenseManager.Api.ViewModels;

namespace LicenseManager.Api.Controllers
{
    [RoutePrefix("api/Softwares")]
    public class SoftwaresController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SoftwaresController() { }

        public SoftwaresController(ILicenseManagerContext context)
        {
            _db = context;
        }

        // GET api/Softwares
        public IQueryable<SoftwareViewModel> GetSoftwares()
        {
            var softwares = from s in _db.Softwares
                            select new SoftwareViewModel()
                            {
                                Id = s.SoftwareId,
                                Name = s.Name,
                                ManufacturerName = s.Manufacturer.Name,
                                GenreName = s.Genre.Name,
                                Description = s.Description
                            };
            return softwares;
        }

        // GET api/Softwares/5
        [ResponseType(typeof(SoftwareViewModel))]
        public IHttpActionResult GetSoftware(int id)
        {
            Software item = _db.Softwares.Find(id);
            if (null == item)
            {
                return NotFound();
            }

            var software = new SoftwareViewModel()
            {
                Id = item.SoftwareId,
                Name = item.Name,
                ManufacturerName = item.Manufacturer.Name,
                GenreName = item.Genre.Name,
                Description = item.Description
            };

            return Ok(software);
        }

        // PUT: api/Softwares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSoftware(int id, Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != software.SoftwareId)
            {
                return BadRequest();
            }

            if (!ManufacturerExists(software.ManufacturerId))
            {
                return BadRequest("Manufacturer not exists");
            }

            _db.MarkAsModified(software);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Logger.Error(ex);
                //TODO check if software exits
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Softwares
        [ResponseType(typeof(Software))]
        public IHttpActionResult PostSoftware(Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ManufacturerExists(software.ManufacturerId))
            {
                return BadRequest("Manufacturer not exists");
            }

            _db.Softwares.Add(software);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = software.SoftwareId}, software);
        }

        // DELETE: api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult DeleteSoftware(int id)
        {
            Software software = _db.Softwares.Find(id);
            if (null == software)
            {
                return NotFound();
            }

            _db.Softwares.Remove(software);
            _db.SaveChanges();

            return Ok(software);
        }

        private bool ManufacturerExists(int id)
        {
            return new ManufacturersController(_db).ManufacturerExists(id);
        }
    }
}
