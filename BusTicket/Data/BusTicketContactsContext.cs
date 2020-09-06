using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusTicket.Models;

namespace BusTicket.Data
{
    public class BusTicketContactsContext : DbContext
    {
        public BusTicketContactsContext (DbContextOptions<BusTicketContactsContext> options)
            : base(options)
        {
        }

        public DbSet<BusTicket.Models.Contact> Contact { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<RouteDetail> RouteDetail { get; set; }
        public DbSet<BusTicket.Models.BusOperator> BusOperator { get; set; }
        public DbSet<BusTicket.Models.Bus> Bus { get; set; }
    }
}
