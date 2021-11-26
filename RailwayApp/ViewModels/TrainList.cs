using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayApp.Models;

namespace RailwayApp.ViewModels
{
    public class TrainList
    {
        public IEnumerable<Train> Trains { get; set; }
        public Guid CurrentGuid { get; set; }
        }
    }

