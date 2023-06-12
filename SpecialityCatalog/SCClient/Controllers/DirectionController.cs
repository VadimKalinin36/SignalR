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
    public class DirectionController : Controller
    {
        private readonly ILogger<DirectionController> _logger;

        private readonly SpecialityCatalogService _specialityCatalogService;



        public DirectionController(ILogger<DirectionController> logger, SpecialityCatalogService specialityCatalogService)
        {
            _logger = logger;
            _specialityCatalogService= specialityCatalogService;
        }

        public async Task <IActionResult> Index()
        {
            List<Direction> directions = await _specialityCatalogService.GetDirections();
            
            return View(directions);
        }

        public async Task<IActionResult> Remove(int id)
        {

            await _specialityCatalogService.RemoveDirection(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Direction direction)
        {

            bool result = await _specialityCatalogService.EditDirection(direction);
            
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(direction);
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(int id)
        {
            var direction = await _specialityCatalogService.GetDirection(id);

            if (direction == null)
            {
                return RedirectToAction("Index");
            }


            return View(direction);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Direction direction)
        {
           bool result = await _specialityCatalogService.AddDirection(direction);
           
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