using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SCData.Models;
using SpecialityCatalogWebApi.Data;
using SpecialityCatalogWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpecialityCatalogWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthController : ControllerBase
    {
        private readonly StudentsDbContext _studentsDbContext;

        public AuthController(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginModel data)
        {

            var user = _studentsDbContext.Users.FirstOrDefault(x => x.Name == data.Login && x.Password == data.Password);

            if (user == null)
            {
                return new JsonResult(new { status = 1, token = " " });
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, data.Login)
            };

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2000)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonResult(new { status = 0, token });
        }

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Создание нового пользователя
        //    var user = new User { Name = model.Login, Password = model.Password };
        //    var result = await _studentsDbContext.CreateAsync(user, model.Password);

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    // Создание JWT Token для нового пользователя
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Id, user.Id),
        //        new Claim(JwtRegisteredClaimNames.Name, user.Name),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(30),
        //      signingCredentials: creds);

        //    // Возвращение токена клиенту
        //    return Ok(new
        //    {
        //        token = new JwtSecurityTokenHandler().WriteToken(token),
        //        userId = user.Id
        //    });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await _studentsDbContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
        //        if (user == null)
        //        {
        //            // добавляем пользователя в бд
        //            _studentsDbContext.Users.Add(new User { Login = model.Login, Password = model.Password });
        //            await _studentsDbContext.SaveChangesAsync();

        //            await Authenticate(model.Login); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

    }
}
