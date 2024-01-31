using EquipmentManagement.Application.Common.IUnitOfWork;
namespace EquipmentManagement.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    #region Using

    private readonly EquipmentManagementDbContext _context;

    public UnitOfWork(EquipmentManagementDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Save Changes

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    #endregion
}
