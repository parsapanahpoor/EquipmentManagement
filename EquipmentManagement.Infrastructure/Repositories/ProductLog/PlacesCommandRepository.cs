using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using EquipmentManagement.Domain.Entities.ProductLog;
using EquipmentManagement.Domain.IRepositories.ProductLog;

namespace EquipmentManagement.Infrastructure.Repositories.ProductLog;

public class ProductLogCommandRepository : CommandGenericRepository<Domain.Entities.ProductLog.ProductLog> , IProductLogCommandRepository
{
	#region Ctor

	private readonly EquipmentManagementDbContext _context;

    public ProductLogCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task AddProductLog(Domain.Entities.ProductLog.ProductLog model , CancellationToken cancellationToken)
    {
        await AddAsync(model , cancellationToken);
    }
}
