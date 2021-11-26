using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayApp.Models;

namespace RailwayApp.ViewModels
{
    public class Order_Train
    {
        public Order Order { get; set; }
        public Guid Train { get; set; }
        public Route Route { get; set; }
        public Guid RouteId { get; set; }
    }
}
