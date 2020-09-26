using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class BookingDetail
    {
        public int ID { get; set; }

        public int BookingID { get; set; }
        public Booking Booking { get; set; }

        public int BusSeatID { get; set; }
        public BusSeat BusSeat { get; set; }

        public int BusPriceID { get; set; }
        public BusPrice BusPrice { get; set; }
       
    }
   
}
