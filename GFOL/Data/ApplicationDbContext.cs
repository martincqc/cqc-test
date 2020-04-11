using System;
using System.Collections.Generic;
using System.Text;
using GFOL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GFOL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Schema> Schema { get; set; }
        public DbSet<Submission> Submission { get; set; }
    }
}
