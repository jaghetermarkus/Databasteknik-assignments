using Console_dbApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Console_dbApp.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // Define DbSet properties for all entities in the database
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ContactInformationEntity> ContactInformations { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ModelYearEntity> ModelYears { get; set; }
    public DbSet<ColorEntity> Colors { get; set; }
    public DbSet<ManufacturerEntity> Manufacturers { get; set; }
    public DbSet<EngineEntity> Engines { get; set; }
    public DbSet<CustomerOrderEntity> CustomerOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerOrderEntity>()
        .HasKey(x => new { x.CustomerId, x.OrderId });

        modelBuilder.Entity<CustomerEntity>()
            .HasMany(c => c.CustomerOrders)
            .WithOne(co => co.Customer)
            .HasForeignKey(co => co.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}