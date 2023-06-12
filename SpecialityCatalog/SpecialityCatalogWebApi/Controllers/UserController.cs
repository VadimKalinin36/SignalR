using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpecialityCatalogWebApi.Data;

namespace SpecialityCatalogWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly StudentsDbContext _studentsDbContext;

        public UserController(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _studentsDbContext.Users.First(x => x.Name == User.Identity.Name);

            return new JsonResult(new { id = user.Id, name = user.Name });
        }
    }
}
