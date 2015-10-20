using LicenseManager.Shared;
using LicenseManager.Shared.Dtos;
using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LicenseManager.Api.Controllers
{
    [Authorize]
    public class GenresController : ApiController
    {
        private ILicenseManagerContext _db = new LicenseManagerContext();

        public GenresController() { }

        public GenresController(ILicenseManagerContext db)
        {
            _db = db;
        }

        // GET: api/Genres
        [AllowAnonymous]
        public IQueryable<GenreDto> GetGenres()
        {
            var genres = from g in _db.Genres
                         select new GenreDto()
                         {
                             Id = g.GenreId,
                             Name = g.Name
                         };

            return genres;
        }
    }
}
