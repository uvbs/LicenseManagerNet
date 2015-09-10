using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LicenseManager.Shared;
using LicenseManager.Shared.Models;
using LicenseManager.Api.ViewModels;

namespace LicenseManager.Api.Controllers
{
    public class ManufacturersController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();

        public ManufacturersController() { }

        public ManufacturersController(ILicenseManagerContext context)
        {
            this._db = context;
        }

        // GET: api/Manufacturers
        public IQueryable<ManufacturerViewModel> GetManufacturers()
        {
            var manufacturers = from m in _db.Manufacturers
                                select new ManufacturerViewModel()
                                {
                                    Id = m.ManufacturerId,
                                    Name = m.Name,
                                };
            return manufacturers;
        }

        // GET: api/Manufacturers/5
        [ResponseType(typeof(ManufacturerDetailViewModel))]
        public IHttpActionResult GetManufacturer(int id)
        {
            var item = _db.Manufacturers.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var manufacturer = new ManufacturerDetailViewModel()
            {
                Id = item.ManufacturerId,
                Name = item.Name,
                Homepage = item.Homepage
            };

            return Ok(manufacturer);
        }

        // PUT: api/Manufacturers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }

            _db.MarkAsModified(manufacturer);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult PostManufacturer(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ManufacturerExists(manufacturer.Name))
            {
                return BadRequest("Manufacturer already exists");
            }

            _db.Manufacturers.Add(manufacturer);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = _db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _db.Manufacturers.Remove(manufacturer);
            _db.SaveChanges();

            return Ok(manufacturer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool ManufacturerExists(int id)
        {
            return _db.Manufacturers.Count(e => e.ManufacturerId == id) > 0;
        }

        private bool ManufacturerExists(string name)
        {
            return _db.Manufacturers.Count(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) > 0;
        }
    }
}