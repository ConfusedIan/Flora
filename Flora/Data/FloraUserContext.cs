using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flora.Models;

namespace Flora.Data
{
    public class FloraUserContext : DbContext
    {
        public FloraUserContext (DbContextOptions<FloraUserContext> options)
            : base(options)
        {
        }

        public DbSet<Flora.Models.User> User { get; set; } = default!;
    }
}
