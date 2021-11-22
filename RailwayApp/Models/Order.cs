using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User OrderUser { get; set; }
        public Guid OrderUserId { get; set; }
        public Route OrderRoute { get; set; }
        public Guid OrderRouteId { get; set; }
        public string ReserveSeat { get; set; } 
    }
}
