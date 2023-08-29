using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RestaurantAPI.Configurations;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<LocalGovernment> LocalGovernments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<MenuType> MenuTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Vote> Votes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasMany(s => s.LocalGovernments)
                .WithOne(lg => lg.State)
                .HasForeignKey(lg => lg.StateId)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Reviews)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(r => r.RestaurantId)
                .IsRequired();
            modelBuilder.Entity<MenuType>()
                .HasMany(m => m.MenuItems)
                .WithOne(m => m.MenuType)
                .HasForeignKey(m => m.MenuTypeId)
                .IsRequired();
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.MenuTypes)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(r => r.RestaurantId)
                .IsRequired();
            modelBuilder.Entity<LocalGovernment>()
                .HasMany(lg => lg.Restaurants)
                .WithOne(r => r.LocalGovernment)
                .HasForeignKey(r => r.LocalGovernmentId);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(18, 0);
            modelBuilder.Entity<Delivery>()
                .Property(d => d.DeliveryFee)
                .HasPrecision(18, 0);

            StatesSeeder.SeedStates(modelBuilder);
            LocalGovernmentsSeeder.SeedLocalGovernment(modelBuilder);

        }
    }
}
