using CMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St", Password = "John@123", PasswordKey = "John@123" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "0987654321", Address = "456 Elm St", Password = "Jane@123", PasswordKey = "Jane@123" },
                new Customer { CustomerId = 3, FirstName = "Leanne", LastName = "Graham", Email = "Sincere@example.com", Phone = "17707368031", Address = "Kulas Light", Password = "Leanne@123", PasswordKey = "Leanne@123" },
                new Customer { CustomerId = 4, FirstName = "Dennis", LastName = "Schulist", Email = "dennis@example.com", Phone = "098-765-4321", Address = "Suite 879", Password = "Dennis@123", PasswordKey = "Dennis@123" },
                new Customer { CustomerId = 5, FirstName = "Glenna", LastName = "Reichert", Email = "glenna@example.com", Phone = "123-456-7890", Address = "Proactive didactic contingency", Password = "Glenna@123", PasswordKey = "Glenna@123" },
                new Customer { CustomerId = 6, FirstName = "Ervin", LastName = "Howell", Email = "ervin@example.com", Phone = "0987654321", Address = "Wisokyburgh", Password = "Ervin@123", PasswordKey = "Ervin@123" }
            );
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Email).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
