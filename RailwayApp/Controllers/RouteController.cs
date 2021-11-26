using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayApp.Services;
using RailwayApp.ViewModels;

namespace RailwayApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly ITrainService _trainService;
        private readonly IStationService _stationService;

        public RouteController(IRouteService routeService, ITrainService trainService, IStationService stationService)
        {
            _routeService = routeService;
            _trainService = trainService;
            _stationService = stationService;
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Route_View routeView)
        {
            await _routeService.CreateAsync(routeView);
            return RedirectToAction("Index");
        }
    }
}
