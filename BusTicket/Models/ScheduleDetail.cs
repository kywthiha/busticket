using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class ScheduleDetail
    {
        public int ID { get; set; }
        public int ScheduleID { get; set; }
        [Required]
        public int WeekDay { get; set; }
        public Schedule Schedule { get; set; }
    }
    
}
