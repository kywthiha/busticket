using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Route
    {
        public int ID { get; set; }

        public int OwnerID { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<RouteDetail> RouteDetails { get; set; }
    }
}
