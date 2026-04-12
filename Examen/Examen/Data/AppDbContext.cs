using Examen.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Port=3306;Database=examen_db;User=root;Password=;",
                ServerVersion.Parse("8.4.3-mysql")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Username = "admin", Email = "admin@admin.com", Password = "admin123" },
                new User { Id = 2, Name = "Peanut", Username = "peanut", Email = "peanut@yum.com", Password = "peanut123" }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, Date = DateTime.Today, Title = "Meeting", Description = "Team sync" },
                new Appointment { Id = 2, Date = DateTime.Today.AddDays(1), Title = "Doctor", Description = "Checkup" }
            );
        }
    }
}