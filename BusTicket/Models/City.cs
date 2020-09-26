using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class City
    {
        public int ID { get; set; }
       
        [Required]
        public string Name { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }

        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<RouteDetail> RouteDetails { get; set; }
    }
}
