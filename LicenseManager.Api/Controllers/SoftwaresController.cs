using System.Data.Entity.Infrastructure;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;

namespace LicenseManager.Api.Controllers
{
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
        public IQueryable<Software> GetSoftwares()
        {
            return _db.Softwares;
        }

        // GET api/Softwares/5
        [ResponseType(typeof(Software))]
        public IHttpActionResult GetSoftware(int id)
        {
            Software software = _db.Softwares.Find(id);
            if (null == software)
            {
                return NotFound();
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

            if (id != software.Id)
            {
                return BadRequest();
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

            _db.Softwares.Add(software);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = software.Id}, software);
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
    }
}
