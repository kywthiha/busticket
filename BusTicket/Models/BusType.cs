using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class BusType
    {
        public int ID { get; set; }
       
        [Required]
        public string Name { get; set; }

        [Required]
        public int Seats { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }

        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<Bus> Buses { get; set; }
        public ICollection<BusSeat> BusSeats { get; set; }
    }
}
