using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfAndTurf.Models;

namespace SurfAndTurf.Data
{
    public class SurfAndTurfContext : IdentityDbContext 
    {
        public SurfAndTurfContext (DbContextOptions<SurfAndTurfContext> options)
            : base(options)
        {
        }

        public DbSet<SurfBoard> SurfBoard { get; set; } = default!;
        public DbSet<Bookings> Bookings { get; set; } = default!;
    }


}
