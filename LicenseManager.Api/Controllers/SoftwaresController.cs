﻿using System.Data.Entity.Infrastructure;
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
    [Authorize]
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
        [Route("")]
        [AllowAnonymous]
        public IQueryable<SoftwareViewModel> GetSoftwares()
        {
            var softwares = from s in _db.Softwares
                            select new SoftwareViewModel()
                            {
                                Id = s.SoftwareId,
                                Name = s.Name,
                                ManufacturerName = s.Manufacturer.Name,
                                GenreName = s.Genre.Name,
                            };
            return softwares;
        }

        // GET api/Manufacturers/{manufacturerId}/Softwares
        [Route("~/api/Manufacturers/{manufacturerId}/Softwares")]
        [AllowAnonymous]
        public IQueryable<SoftwareViewModel> GetSoftwaresByManufacturer(int manufacturerId)
        {
            var softwares = from s in _db.Softwares
                            where s.ManufacturerId == manufacturerId
                            select new SoftwareViewModel
                            {
                                Id = s.SoftwareId,
                                Name = s.Name,
                                ManufacturerName = s.Manufacturer.Name,
                                GenreName = s.Genre.Name
                            };
            return softwares;
        }

        // GET api/Softwares/5
        [ResponseType(typeof(SoftwareViewModel))]
        [Route("{id:int}")]
        public IHttpActionResult GetSoftware(int id)
        {
            return GetSoftware(-1, id);
        }

        // GET api/Manufacturers/{manufacturerId}/Softwares/5
        [ResponseType(typeof(SoftwareViewModel))]
        [Route("~/api/manufacturers/{manufacturerId}/softwares/{id:int}")]
        public IHttpActionResult GetSoftware(int manufacturerId, int id)
        {
            Logger.Debug("ManufacturerId: " + manufacturerId);

            Software item = null;
            if (manufacturerId > 0)
            {
                item = _db.Softwares.FirstOrDefault(s => s.SoftwareId == id && s.ManufacturerId == manufacturerId);
            }

            if (manufacturerId == -1)
            {
                item = _db.Softwares.Find(id);
            }
            
            if (null == item)
            {
                return NotFound();
            }

            var software = new SoftwareDetailViewModel()
            {
                Id = item.SoftwareId,
                Name = item.Name,
                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.Manufacturer.Name,
                GenreId = item.GenreId,
                GenreName = item.Genre.Name,
                Description = item.Description
            };

            if (item.Licenses.Count > 0)
            {
                software.Licenses = item.Licenses.Select(l => new LicenseViewModel
                {
                    Id = l.LicenseId,
                    ActivationKey = l.ActivationKey,
                    Edition = l.Edition,
                    VolumeLicense = (l.VolumeLicense == 1)
                });
            }

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
        [Route("")]
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

            //TODO checked created at route
            //return CreatedAtRoute("DefaultApi", new {id = software.SoftwareId}, software);
            return Created("api/softwares/" + new { id = software.SoftwareId }, software);
        }

        // DELETE: api/Softwares/5
        [ResponseType(typeof(Software))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteSoftware(int id)
        {
            Software software = _db.Softwares.Find(id);
            if (null == software)
            {
                return NotFound();
            }

            _db.Licenses.RemoveRange(_db.Licenses.Where(l => l.SoftwareId == id));

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
