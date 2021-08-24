using ELReedCinema.Data.Configuration;
using ELReedCinema.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ELReedCinema.Data
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions <CommentsDbContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Comment>(new CommentsConfiguration());
        }
    }
}
