using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusTicket.Models;

namespace BusTicket.Data
{
    public class BusTicketContext : DbContext
    {
        public BusTicketContext (DbContextOptions<BusTicketContext> options)
            : base(options)
        {
        }

        public DbSet<BusTicket.Models.BusType> BusType { get; set; }
    }
}
