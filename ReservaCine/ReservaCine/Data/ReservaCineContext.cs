using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservaCine.Models;

namespace ReservaCine.Data
{
    public class ReservaCineContext : DbContext
    {
        public ReservaCineContext (DbContextOptions<ReservaCineContext> options)
            : base(options)
        {
        }

        public DbSet<ReservaCine.Models.Pelicula> Pelicula { get; set; }

        public DbSet<ReservaCine.Models.Usuario> Usuario { get; set; }
    }
}
