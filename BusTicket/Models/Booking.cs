using Microsoft.AspNetCore.Identity;
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

        [EnumDataType(typeof(BookingStatus))]
        public BookingStatus BookingStatus { get; set; }
      
        public string TravellerID { get; set; }
        public Traveller Traveller { get; set; }


        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
    }

    public enum BookingStatus
    {
        Pending = 1,
        Confirmed =2,
        Canceled =3,
        TimeOuted =4
    }
    
}
