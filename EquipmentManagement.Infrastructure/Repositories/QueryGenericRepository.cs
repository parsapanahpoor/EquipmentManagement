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
    public async Task<bool> IsExistAnyByIdAsync(ulong id, CancellationToken cancellationToken)
    {

        return await Entities
            .AsNoTracking()
            .AnyAsync(e => EF.Property<ulong>(e, "Id") == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }
    // متد عمومی برای فیلتر و صفحه‌بندی
    public async Task<TFilter> FilterAsync<TFilter>(TFilter filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterQuery)
        where TFilter : class
    {
        var query = Entities.AsNoTracking().AsQueryable();

        // اعمال فیلتر سفارشی
        query = filterQuery(query);

        // فرض می‌کنیم کلاس Filter دارای متد Paging هست
        var pagingMethod = filter.GetType().GetMethod("Paging");
        if (pagingMethod != null)
        {
            var task = (Task)pagingMethod.Invoke(filter, new object[] { query });
            await task;
        }

        return filter;
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
