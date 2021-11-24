using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RailwayApp.Data;
using RailwayApp.Models;
using RailwayApp.Services;
using RailwayApp.ViewModels;

namespace RailwayApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly ITrainService _trainService;
        private readonly IStationService _stationService;
        private readonly AppDataContext _appDataContext;

        public RouteController(IRouteService routeService, ITrainService trainService, IStationService stationService, AppDataContext appDataContext)
        {
            _routeService = routeService;
            _trainService = trainService;
            _stationService = stationService;
            _appDataContext = appDataContext;
        }

        [HttpGet]
        public async Task<IActionResult>  Index()
        {

            return View(await _routeService.GetAsync());
        }

        [HttpGet]
        public async Task<IActionResult>  Create()
        {

            ViewBag.ListTrain=new SelectList(await _trainService.GetAsync(),"Id","Name");
            ViewBag.ListStation=new SelectList(await _stationService.GetAsync(),"Id","Name");
            return View(new Route_View(){TrainsList = new List<Guid>(), StationList = new List<Guid>()});
        }
        [HttpPost]
        public async Task<IActionResult> Create(Route_View routeView)
        {
            await _routeService.CreateAsync(routeView);
            return RedirectToAction("Index");
        }
    }
}
