using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime DepatureTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ArrivalTime { get; set; }
        public int BusOperatorID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int BusID { get; set; }
        public int RouteID { get; set; }
        public Bus Bus { get; set; }
        public Route Route { get; set; }

        public ICollection<ScheduleDetail> ScheduleDetails { get; set; }
    }

}
