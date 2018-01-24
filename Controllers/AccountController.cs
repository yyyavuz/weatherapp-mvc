using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kontrolmatik.Data;
using Kontrolmatik.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Kontrolmatik.Controllers
{
    public class AccountController : Controller
    {
        private readonly WeatherDbContext dbContext;
        public AccountController(WeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Login()
        {
            // TODO: Is User Logged Control            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // TODO: Is User Logged Control            
            if (ModelState.IsValid)
            {
                string hashedPassword = model.Password.Hash();
                var user = dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı adınız veya şifreniz yanlış");
                    return View(model);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim("Username", user.Username),
                        new Claim("Id",user.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(
    claims,
    CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        public IActionResult Register()
        {
            // TODO: Is User Logged Control
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            // TODO: Is User Logged Control
            if (ModelState.IsValid)
            {
                var isHasUser = dbContext.Users.Any(x => x.EmailAddress == model.EmailAddress | x.Username == model.Username);
                if (isHasUser)
                {
                    ModelState.AddModelError("", "Bu kullanıcı zaten var");
                    return View(model);
                }
                else
                {
                    dbContext.Users.Add(new Data.Tables.User()
                    {
                        Username = model.Username,
                        Password = model.Password.Hash(),
                        EmailAddress = model.EmailAddress,
                        FullName = model.FullName
                    });
                    try
                    {
                        dbContext.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Kayıt olurken hata oluştu");
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // TODO: Is User Logged Control
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}