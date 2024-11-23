using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flora.Models;

namespace Flora.Data
{
    public class FloraContext : DbContext
    {
        public FloraContext (DbContextOptions<FloraContext> options)
            : base(options)
        {
        }

        public DbSet<Flora.Models.Product> Product { get; set; } = default!;
    }
}
