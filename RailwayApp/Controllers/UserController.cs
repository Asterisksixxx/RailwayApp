using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RailwayApp.Models;
using RailwayApp.Services;

namespace RailwayApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            if (!ModelState.IsValid) return View();
            if (!_userService.Check(user.Name, user.EmailUser, user.PassportNumber))
            {
                await _userService.CreateAsync(user);
              await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "The user already exists");
            return View(user);
        }
        public async Task Authenticate(User user)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(_userService.Authenticate(user)));
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User use)
        {
            var user = _userService.Get().FirstOrDefault(u => u.EmailUser == use.EmailUser && u.Password == use.Password);
            if (user != null)
            {
                Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(use);
        }

        public async Task<IActionResult>  Logout()
        { 
           await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(string email)
        {
            return View(await _userService.GetAsync(email));

        }
    }
}
