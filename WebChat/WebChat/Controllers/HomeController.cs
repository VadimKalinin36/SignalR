using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebChat.Models;
using AutoMapper;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var historyModel = _context.HistoryModels.ToList();
            var MaphistoryModel = _mapper.Map<List<MapHistoryModel>>(historyModel);
            //var mapSendModel = _mapper.Map<List<MapSendModel>>(sendModel);

            //var sendModel = _context.SendModels.FirstOrDefault();

            //var mapSendModel = _mapper.Map<MapSendModel>(sendModel);


            return View(MaphistoryModel);


        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult DataInputAdd(HistoryModel DataInput)
        //{
        //    _context.HistoryModels.Add(DataInput);
        //    _context.SaveChanges();

        //    return Ok();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}