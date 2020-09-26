using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class BusSeat
    {
        public int ID { get; set; }

        [Required]
        public int SeatNo { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int BusTypeID { get; set; }
        public BusType BusType { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
    }
   
}
