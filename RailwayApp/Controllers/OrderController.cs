using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RailwayApp.Services;

namespace RailwayApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
