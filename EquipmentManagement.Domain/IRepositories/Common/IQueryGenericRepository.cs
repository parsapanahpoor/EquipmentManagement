

namespace EquipmentManagement.Domain.IRepositories.Common;

public interface IQueryGenericRepository<TEntity> 
{
    // متد async برای گرفتن موجودیت بر اساس کلید
    Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    // جدول اصلی (با Tracking)
    IQueryable<TEntity> Table { get; }

    // جدول بدون Tracking (برای کوئری‌های فقط خواندنی)
    IQueryable<TEntity> TableNoTracking { get; }

    // متد sync برای گرفتن موجودیت بر اساس کلید
    TEntity GetById(params object[] ids);

    Task<bool> IsExistAnyByIdAsync(ulong Id, CancellationToken cancellationToken);
    // متد عمومی برای فیلتر و صفحه‌بندی
    Task<TFilter> FilterAsync<TFilter>(TFilter filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterQuery)
        where TFilter : class;
}
