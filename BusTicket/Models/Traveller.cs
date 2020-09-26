using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Traveller
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }


        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
    public  enum Gender{
        Male=1,
        Female=2,
        MaleGroup=3,
        FemaleGroup=4,
        MixedGroup=5
    }
}
