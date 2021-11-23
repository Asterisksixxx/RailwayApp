using System;
using System.Collections.Generic;

namespace RailwayApp.Models
{
    public class Train
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal TicketCost { get; set; }
        public string AvailableSeats { get; set; }
        public string Type { get; set; }
    }
}
