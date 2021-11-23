using System;
using System.Collections.Generic;

namespace RailwayApp.Models
{
    public class Route
    {
        public Guid Id { get; set; }
        public List<Train> TrainsList { get; set; }
        public List<Station> StationList { get; set; }
        public DateTime StartRouteDateTime { get; set; }
        public DateTime EndRouteDateTime { get; set; }
    }
}