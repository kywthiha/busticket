using System;
using System.ComponentModel.DataAnnotations;

namespace BusTicket.Models
{
    public class BusDetail
    {
        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DeparatureTime { get; set; }

        public int BusID { get; set; }
        public Bus Bus { get; set; }

        public int RouteDetailID { get; set; }
        public RouteDetail RouteDetail { get; set; }
    }
}