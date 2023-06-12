using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCData.Models;
using SpecialityCatalogWebApi.Data;
using static SpecialityCatalogWebApi.Controllers.DirectionController;

namespace SpecialityCatalogWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsDbContext _studentsDbContext;

        public StudentController(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        public class StudentFilter
        {
            public int? StudentId { get; set; }
            public string? LastName { get; set; }
            public string? FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? GroupName { get; set; }
            public string? DirectionName { get; set; }
            public string? CountryName { get; set; }


        }

        [HttpPost]
        [Route("[action]")]

        public List<Student> Index([FromBody] StudentFilter filter)
        {
            var query = _studentsDbContext.Students.AsQueryable();

            if (filter.StudentId != null) query = query.Where(x => x.Id == filter.StudentId);

            if (!string.IsNullOrEmpty(filter.LastName)) query = query.Where(x => x.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.FirstName)) query = query.Where(x => x.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.MiddleName)) query = query.Where(x => x.MiddleName.Contains(filter.MiddleName));

            if (!string.IsNullOrEmpty(filter.GroupName)) query = query.Where(x => x.Group.Name.Contains(filter.GroupName));

            if (!string.IsNullOrEmpty(filter.DirectionName)) query = query.Where(x => x.Direction.Name.Contains(filter.DirectionName));

            if (!string.IsNullOrEmpty(filter.CountryName)) query = query.Where(x => x.Country.Name.Contains(filter.CountryName));






            var students = query
                .Include(x => x.Group)
                .Include(x => x.Direction)
                .Include(x => x.Country)
                .ToList();

            return students;

        }


        [HttpGet("{id}")]
        public Student Get(int id)
        {
            var student = _studentsDbContext.Students.FirstOrDefault(x => x.Id == id);

            return student;
        }


        [HttpPost("{id}")]
        public IActionResult Post(int id, Student student)
        {
            var existStudent = _studentsDbContext.Students.FirstOrDefault(x => x.Id == id);

            if (existStudent == null)
            {
                return NotFound();
            }

            existStudent.LastName = student.LastName;
            existStudent.FirstName = student.FirstName;
            existStudent.MiddleName = student.MiddleName;
            existStudent.GroupId = student.GroupId;
            existStudent.DirectionId = student.DirectionId;
            existStudent.CountryId = student.CountryId;


            _studentsDbContext.Students.Update(existStudent);
            _studentsDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Student student)
        {
            _studentsDbContext.Students.Add(student);
            _studentsDbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existStudent = _studentsDbContext.Students.FirstOrDefault(x => x.Id == id);

            if (existStudent == null)
            {
                return NotFound();
            }

            _studentsDbContext.Students.Remove(existStudent);
            _studentsDbContext.SaveChanges();

            return Ok();
        }
    }
}