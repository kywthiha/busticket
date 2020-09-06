using System;
using System.Collections.Generic;
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

        public ICollection<RouteDetail> RouteDetails { get; set; }
    }
}
