using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Bus
    {
        public int ID { get; set; }

        [Required]
        public int NoOfSeat { get; set; }
        [Required]
        public BusType Type { get; set; }
        public int BusOperatorID { get; set; }
        public BusOperator BusOperator { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
    public  enum BusType{
        Standard,
        VIP
    }
}
