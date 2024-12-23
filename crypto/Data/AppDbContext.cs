using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crypto.Models; // Namespace where your models are located

namespace crypto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define DbSets for your models
        public DbSet<User> Users { get; set; }
        // Add other DbSet properties for additional entities, e.g.:
        // public DbSet<Product> Products { get; set; }
    }
}