using System;
using System.Collections.Generic;

namespace RailwayApp.Models
{
    public class Route
    {
        public Guid Id { get; set; }
        public List<Train> TrainsList { get; set; }
        public Station StartStation { get; set; }
        public Guid StartStationId { get; set; }
        public Station LastStation { get; set; }
        public Guid LastStationId { get; set; }
        public DateTime StartRouteDateTime { get; set; }
        public DateTime EndRouteDateTime { get; set; }
    }
}