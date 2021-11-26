using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using RailwayApp.Models;
using RailwayApp.Services;
using RailwayApp.ViewModels;

namespace RailwayApp.Controllers
{
    public class StationController : Controller
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        public async Task<IActionResult> Index()
        {
            StationList stationList = new StationList {Stations = await _stationService.GetAsync() };
            return View(stationList);
        }

        [HttpPost]
        public IActionResult Delete(StationList stationList)
        {
            _stationService.Delete(stationList.CurrentStation);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Station station)
        {
            await _stationService.CreateAsync(station);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _stationService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(Station station)
        {
            await _stationService.UpdateAsync(station);
            return RedirectToAction("Index");
        }
    }
}
