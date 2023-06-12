using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCData.Models;
using SpecialityCatalogWebApi.Data;
using static SpecialityCatalogWebApi.Controllers.GroupController;

namespace SpecialityCatalogWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly StudentsDbContext _studentsDbContext;

        public CountryController(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        public class CountryFilter
        {
            public int? CountryId { get; set; }
            public string? Name { get; set; }

        }

        [HttpPost]
        [Route("[action]")]
        public List<Country> Index([FromBody] CountryFilter filter)
        {
            var query = _studentsDbContext.Countries.AsQueryable();

            if (filter.CountryId != null) query = query.Where(x => x.Id == filter.CountryId);

            if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(x => x.Name.Contains(filter.Name));

            var countries = query.ToList();

            return countries;
        }

        [HttpGet("{id}")]
        public Country Get(int id)
        {
            var country = _studentsDbContext.Countries.FirstOrDefault(x => x.Id == id);

            return country;
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, Country country)
        {
            var existCountry = _studentsDbContext.Countries.FirstOrDefault(x => x.Id == id);
            if (existCountry == null)
            {
                return NotFound();
            }

            existCountry.Name = country.Name;

            _studentsDbContext.Countries.Update(existCountry);
            _studentsDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Country country)
        {
            _studentsDbContext.Countries.Add(country);
            _studentsDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existCountry = _studentsDbContext.Countries.FirstOrDefault(x => x.Id == id);
            if (existCountry == null)
            {
                return NotFound();
            }

            _studentsDbContext.Countries.Remove(existCountry);
            _studentsDbContext.SaveChanges();

            return Ok();
        }




    }
}
