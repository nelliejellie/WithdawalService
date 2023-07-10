using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithdrawalService.Domain;
using WithdrawalService.Entities;

namespace WithdrawalService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AppUser> AspNetUsers { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<SuccessfulTransfers> SuccessfulTransfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("Default");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
