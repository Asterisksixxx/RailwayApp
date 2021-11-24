using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayApp.Models;

namespace RailwayApp.ViewModels
{
    public class Route_Train_Station
    {
        public Train Trains { get; set; }
        public Station Stations { get; set; }
        public Route Routes { get; set; }
    }
}
