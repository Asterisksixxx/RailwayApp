using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RailwayApp.Models;
using RailwayApp.Services;

namespace RailwayApp.Controllers
{
    public class TrainController : Controller
    {
        private readonly ITrainService _trainService;

        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _trainService.GetAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Train train)
        {
            await _trainService.CreateAsync(train);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _trainService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _trainService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(Train train)
        {
            await _trainService.UpdateAsync(train);
            return RedirectToAction("Index");
        }
    }
}
