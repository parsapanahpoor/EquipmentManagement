#region properties

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
