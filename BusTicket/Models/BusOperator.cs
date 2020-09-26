using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class BusOperator
    {
        public int ID { get; set; }
       
        [Required]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phoneno { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<Bus> Buses { get; set; }
    }
}
