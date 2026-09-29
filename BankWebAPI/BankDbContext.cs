using Microsoft.EntityFrameworkCore;

namespace BankWebAPI
{
    public class BankDbContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Pointing directly to your live verified local PostgreSQL server engine
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=0787242wrA/$;Database=postgres";
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Re-mapping our inheritance tracking rule exactly like our console app did!
            modelBuilder.Entity<BankAccount>()
                .HasDiscriminator<string>("accounttype")
                .HasValue<SavingsAccount>("Savings");
        }
    }
}
