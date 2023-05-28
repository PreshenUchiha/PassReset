using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PassReset.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

}
