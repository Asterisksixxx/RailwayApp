using System;
using System.Collections.Generic;

namespace RailwayApp.Models
{
    public class Station
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<Route> RouteList { get; set;}
    }
}