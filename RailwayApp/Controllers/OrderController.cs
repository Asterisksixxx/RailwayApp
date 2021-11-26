using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayApp.Models;
using RailwayApp.Services;
using RailwayApp.ViewModels;

namespace RailwayApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IRouteService _routeService;
        private readonly IStationService _stationService;
        private readonly ITrainService _trainService;

        public OrderController(IOrderService orderService, IUserService userService, IRouteService routeService, IStationService stationService, ITrainService trainService)
        {
            _orderService = orderService;
            _userService = userService;
            _routeService = routeService;
            _stationService = stationService;
            _trainService = trainService;
        }
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            return View(await _orderService.GetAsync(HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderService.CreateAsync(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchStation()
        {
            ViewBag.Station= new SelectList(await _stationService.GetAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchStation(SearchStation searchStation)
        {
            return RedirectToAction("SearchRoute",new{ firstStation = searchStation.FirstStation, secondStation = searchStation.SecondStation });
        }

        [HttpGet]
        public async Task<IActionResult> SearchRoute(Guid firstStation, Guid secondStation)
        { 
            var spisok = _orderService.GetRoute(firstStation, secondStation);
            ViewBag.Routes = new SelectList(spisok, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchRoute(RoutId routId)
        {
            return RedirectToAction("ChangeTrain",new {currentRow=routId.RouteId});
        }

        [HttpGet]
        public async Task<IActionResult> ChangeTrain(Guid currentRow)
        { 
            Order_Train ord=new Order_Train{Route = await _routeService.GetAsy(currentRow),RouteId = currentRow};
            ViewBag.Trains = new SelectList(ord.Route.TrainsList, "Id", "Name");
            return View(ord);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeTrain(Order_Train ord)
        {
             Order order=new Order{Id = Guid.NewGuid(),
                 OrderRoute = await _routeService.GetAsync(ord.RouteId),
                 ChangeTrain = await _trainService.GetAsync(ord.Train),
                 ReserveSeat = "",
                 OrderRouteId = ord.RouteId,
                 OrderUserId =await _userService.GetAsyncId(HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value)};
       //  Order order = new Order{ChangeTrain = train,Id = Guid.NewGuid(),OrderRouteId = train.RouteId, OrderUser = await _userService.GetAsync(HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value)};
       await _orderService.CreateAsync(order);
             return RedirectToAction("Index","Home");
        }
    }
}
