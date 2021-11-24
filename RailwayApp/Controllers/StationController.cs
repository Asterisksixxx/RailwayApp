using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayApp.Models;
using RailwayApp.Services;

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
            return View(await _stationService.GetAsync());
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _stationService.Delete(id);
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
