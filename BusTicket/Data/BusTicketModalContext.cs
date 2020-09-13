using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusTicket.Models;

namespace BusTicket.Data
{
    public class BusTicketModalContext : DbContext
    {
        public BusTicketModalContext (DbContextOptions<BusTicketModalContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<RouteDetail> RouteDetail { get; set; }
        public DbSet<BusOperator> BusOperator { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<BusSeat> BusSeat { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleDetail> ScheduleDetail { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingDetail> BookingDetail { get; set; }
        public DbSet<Traveller> Traveller { get; set; }
        
        

    }
}
