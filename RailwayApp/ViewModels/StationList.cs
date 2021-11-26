using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayApp.Models;

namespace RailwayApp.ViewModels
{
    public class StationList
    {
        public IEnumerable<Station> Stations { get; set; }
        public Guid CurrentStation { get; set; }
    }
}
