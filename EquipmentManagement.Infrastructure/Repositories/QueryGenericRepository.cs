using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Infrastructure.Repositories;

public class QueryGenericRepository<TEntity> where TEntity : class
{
    #region Ctor

    protected readonly EquipmentManagementDbContext _context;

    public DbSet<TEntity> Entities { get; }

    public QueryGenericRepository(EquipmentManagementDbContext context)
    {
        _context = context;
        Entities = _context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(TEntity));
        _context.SaveChangesAsync();
    }

    #endregion

    #region async Method

    public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }

    #endregion

    #region sync Method

    public virtual IQueryable<TEntity> Table => Entities;

    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    #endregion
}
