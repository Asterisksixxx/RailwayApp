using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayApp.Models;
using RailwayApp.Services;

namespace RailwayApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IRouteService _routeService;

        public OrderController(IOrderService orderService, IUserService userService, IRouteService routeService)
        {
            _orderService = orderService;
            _userService = userService;
            _routeService = routeService;
        }
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            return View(await _orderService.GetAsync(HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value));
        }

        [HttpGet]
        public async Task<IActionResult>   Create()
        {
            ViewBag.OrderUserId= new SelectList( _userService.Get(), "Id", "Name");
            ViewBag.OrderRouteId = new SelectList(await _routeService.GetAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderService.CreateAsync(order);
            return RedirectToAction("Index");
        }
        
    }
}
