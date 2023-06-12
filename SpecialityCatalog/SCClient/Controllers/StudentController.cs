using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCClient.Models;
using SCClient.Services;
using SCData.Models;
using System.Diagnostics;

namespace SCClient.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;

        private readonly SpecialityCatalogService _specialityCatalogService;

        public StudentController(ILogger<StudentController> logger, SpecialityCatalogService specialityCatalogService)
        {
            _logger = logger;

            _specialityCatalogService = specialityCatalogService;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = await _specialityCatalogService.GetStudents();

            return View(students);
        }

        public async Task<IActionResult> Remove(int id)
        {

            await _specialityCatalogService.RemoveStudent(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {

            bool result = await _specialityCatalogService.EditStudent(student);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View((student, await GetGroupsSelectList(), await GetDirectionsSelectList()));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _specialityCatalogService.GetStudent(id);
            var groups = await GetGroupsSelectList();
            var directions = await GetDirectionsSelectList();

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            return View((student, groups, directions)); 
        }


        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            bool result = await _specialityCatalogService.AddStudent(student);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View((await GetGroupsSelectList(), await GetDirectionsSelectList()));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            
            return View((await GetGroupsSelectList(), await GetDirectionsSelectList()));
        }

        private async Task<SelectList> GetGroupsSelectList()
        {
            List<Group> groups = await _specialityCatalogService.GetGroups();

            return new SelectList(groups, "Id", "Name");
        }

        private async Task<SelectList> GetDirectionsSelectList()
        {
            List<Direction> directions = await _specialityCatalogService.GetDirections();

            return new SelectList(directions, "Id", "Name");
        }
    }
}