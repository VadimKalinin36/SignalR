using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SCClient.Models;
using SCClient.Models.Config;
using SCData.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using SCClient.Services;

namespace SCClient.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;

        private readonly SpecialityCatalogService _specialityCatalogService;



        public GroupController(ILogger<GroupController> logger, SpecialityCatalogService specialityCatalogService)
        {
            _logger = logger;
            _specialityCatalogService= specialityCatalogService;
        }

        public async Task <IActionResult> Index()
        {
            List<Group> groups = await _specialityCatalogService.GetGroups();
            
            return View(groups);
        }

        public async Task<IActionResult> Remove(int id)
        {

            await _specialityCatalogService.RemoveGroup(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {

            bool result = await _specialityCatalogService.EditGroup(group);
            
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(group);
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _specialityCatalogService.GetGroup(id);

            return View(group);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Group group)
        {
           bool result = await _specialityCatalogService.AddGroup(group);
           
           if (result)
           {
                return RedirectToAction(nameof(Index));
           }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            return View();
        }



       

    }
}