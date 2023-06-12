using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCData.Models;
using SpecialityCatalogWebApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SpecialityCatalogWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly StudentsDbContext _studentsDbContext;

        public GroupController(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        public class GroupFilter
        {
            public int? GroupId { get; set; }
            public string? Name { get; set; }
            public GroupType? Type { get; set; }
        }

        [HttpPost]
        [Route("[action]")]
        public List<Group> Index([FromBody] GroupFilter filter)
        {
            var query = _studentsDbContext.Groups.AsQueryable();

            if (filter.GroupId != null) query = query.Where(x => x.Id == filter.GroupId);

            if (filter.Type != null) query = query.Where(x => x.Type == filter.Type);

            if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(x => x.Name.Contains(filter.Name));

            var groups = query.ToList();

            return groups;

        }


        [HttpGet ("{id}")]
        public Group Get(int id)
        {
            var group = _studentsDbContext.Groups.FirstOrDefault(x => x.Id == id);


            return group;
        }


        [HttpPost("{id}")]
        public IActionResult Post(int id, Group group)
        {
           var existGroup = _studentsDbContext.Groups.FirstOrDefault(x => x.Id ==id);
            if (existGroup == null)
            {
                return NotFound();
            }

            existGroup.Name = group.Name;
            existGroup.Type = group.Type;

            _studentsDbContext.Groups.Update(existGroup);
            _studentsDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Group group)
        {
            _studentsDbContext.Groups.Add(group);
            _studentsDbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existGroup = _studentsDbContext.Groups.FirstOrDefault(x => x.Id == id);
            if (existGroup == null)
            {
                return NotFound();
            }

            _studentsDbContext.Groups.Remove(existGroup);
            _studentsDbContext.SaveChanges();
            
            return Ok();
        }
    }
}
