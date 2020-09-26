using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicket.Models
{
    public class BusPrice
    {
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [ForeignKey("FromCity")]
        public int FromCityID { get; set; }
        [ForeignKey("FromCityID")]
        public City FromCity { get; set; }

        [ForeignKey("ToCity")]
        public int ToCityID { get; set; }
        [ForeignKey("ToCityID")]
        public City ToCity { get; set; }

        public int BusID { get; set; }
        public Bus Bus { get; set; }

        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<BookingDetail> BookingDetails {get;set;}

    }
}