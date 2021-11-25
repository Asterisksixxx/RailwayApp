using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayApp.ViewModels
{
    public class Route_View
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> TrainsList { get; set; }=new List<Guid>();
        public List<Guid> StationList { get; set; }=new List<Guid>();
        public DateTime StartRouteDateTime { get; set; }
        public DateTime EndRouteDateTime { get; set; }
    }
}
