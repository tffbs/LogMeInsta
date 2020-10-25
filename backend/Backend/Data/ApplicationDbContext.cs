using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Model;



namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Picture>(entity =>
            {
                entity.HasOne(t => t.User)
                .WithMany(t => t.Pictures)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Picture> Pictures { get; set; }

    }
}