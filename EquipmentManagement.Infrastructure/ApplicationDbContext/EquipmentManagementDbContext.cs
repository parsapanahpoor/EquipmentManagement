#region properties

using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.Entities.ProductCategory;
using EquipmentManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Infrastructure.ApplicationDbContext;

#endregion

public class EquipmentManagementDbContext : DbContext
{
    #region Ctor

    public EquipmentManagementDbContext(DbContextOptions<EquipmentManagementDbContext> options)
           : base(options)
    {

    }

    #endregion

    #region Entity

    #region User

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<UserRole> UserRole { get; set; }

    #endregion

    #region Product Category

    public DbSet<ProductCategory> ProductCategories { get; set; }

    #endregion

    #endregion

    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
