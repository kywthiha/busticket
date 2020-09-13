using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public int? OwnerID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SeatedDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ConfirmedDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CanceledDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? TimeoutDate { get; set; }

        public int TravellerID { get; set; }
        public int ScheduleID { get; set; }

        public Traveller Traveller { get; set; }
        public Schedule Schedule { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
    }
    
}
