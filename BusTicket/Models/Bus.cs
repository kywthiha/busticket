using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicket.Models
{
    public class Bus
    {
        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ToDate { get; set; }
   
        public int RouteID {get;set;}
        public Route Route {get;set;}

        public int BusOperatorID { get; set; }
        public BusOperator BusOperator { get; set; }
       
        public int BusTypeID { get; set; }
        public BusType BusType { get; set; }

        public string OwnerID { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<BusDetail> BusDetails {get;set;}
        public ICollection<BusPrice> BusPrices {get;set;}
        public ICollection<BusRecurring> BusRecurrings { get; set; }

    }
}
