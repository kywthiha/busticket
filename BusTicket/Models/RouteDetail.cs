using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class RouteDetail
    {
        public int ID { get; set; }

        [Required]
        public int Order { get; set; }

        public int RouteID { get; set; }
        public Route Route { get; set; }

        public int CityID { get; set; }
        public City City { get; set; }
    }
}
