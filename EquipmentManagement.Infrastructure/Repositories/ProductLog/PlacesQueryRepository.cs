using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.ProductLog;

public class ProductLogQueryRepository : QueryGenericRepository<Domain.Entities.ProductLog.ProductLog>, IProductLogQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductLogQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<FilterProductLogDTO> FilterProductLogs(FilterProductLogDTO filter, CancellationToken cancellationToken)
    {
        var query = _context.ProductLogs
                            .Include(p => p.Place)
                            .Include(p => p.User)
                            .Include(p => p.Product)
                            .AsNoTracking()
                            .Where(p => !p.IsDelete)
                            .OrderByDescending(p => p.Id)
                            .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.ProductTitle)))
            query = query.Where(u => u.Product!.ProductTitle.Contains(filter.ProductTitle));

        if ((!string.IsNullOrEmpty(filter.Brand)))
            query = query.Where(u => u.Product!.Brand!.Contains(filter.Brand));

        if ((!string.IsNullOrEmpty(filter.RepostiroyCode)))
            query = query.Where(u => u.Product!.RepostiroyCode.Contains(filter.RepostiroyCode));

        if (filter.PlaceId.HasValue)
            query = query.Where(p => p.Place!.Id == filter.PlaceId.Value);

        if (filter.ProductId.HasValue)
            query = query.Where(p => p.ProductId == filter.ProductId.Value);

        if ((!string.IsNullOrEmpty(filter.PlaceTitle)))
            query = query.Where(u => u.Place!.PlaceTitle.Contains(filter.PlaceTitle));

        if ((!string.IsNullOrEmpty(filter.BarCode)))
            query = query.Where(u => u.Product!.BarCode.Contains(filter.BarCode));

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }
}
