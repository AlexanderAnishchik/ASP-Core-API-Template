using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Angular4.Models
{
    public class UtilitiesContext : DbContext
    {
        public UtilitiesContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }

    }
    
}
