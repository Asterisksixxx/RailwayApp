using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayApp.Models
{
    public class Train
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxPeopleCount { get; set; }
        public int NowPeopleCount { get; set; }
        public decimal TicketCount { get; set; }
    }
}
