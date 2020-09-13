using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Traveller
    {
        public int ID { get; set; }
        public int? OwnerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
 
        public ICollection<Booking> Bookings { get; set; }
    }
    public  enum Gender{
        Male,
        Female,
        MaleGroup,
        FemaleGroup,
        MixedGroup
    }
}
