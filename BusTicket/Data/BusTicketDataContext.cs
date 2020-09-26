using Microsoft.EntityFrameworkCore;
using BusTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BusTicket.Data
{
    public class BusTicketDataContext : IdentityDbContext<IdentityUser>
    {
        public BusTicketDataContext (DbContextOptions<BusTicketDataContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteDetail> RouteDetails { get; set; }
        public DbSet<BusOperator> BusOperators { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusType> BusTypes { get; set; }
        public DbSet<BusSeat> BusSeats { get; set; }
        public DbSet<BusDetail> BusDetails { get; set; }
        public DbSet<BusRecurring> BusRecurrings { get; set; }
        public DbSet<BusPrice> BusPrices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Traveller> Travellers { get; set; }

    }


}
