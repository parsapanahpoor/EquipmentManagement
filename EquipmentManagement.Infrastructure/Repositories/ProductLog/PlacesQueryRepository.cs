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
}
