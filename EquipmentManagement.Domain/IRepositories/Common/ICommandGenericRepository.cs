

namespace EquipmentManagement.Domain.IRepositories.Common;

public interface ICommandGenericRepository<TEntity> 
{
    // افزودن یک موجودیت به صورت async
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    // افزودن چند موجودیت به صورت async
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    // افزودن یک موجودیت به صورت sync
    void Add(TEntity entity);

    // افزودن چند موجودیت به صورت sync
    void AddRange(IEnumerable<TEntity> entities);

    // به‌روزرسانی موجودیت
    void Update(TEntity entity);

    // حذف یک موجودیت
    void Delete(TEntity entity);

    // حذف چند موجودیت
    void DeleteRange(IEnumerable<TEntity> entities);
}
