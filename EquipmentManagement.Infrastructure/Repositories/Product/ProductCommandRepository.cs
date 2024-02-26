using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Infrastructure.Repositories.Product;

public class ProductCommandRepository : CommandGenericRepository<Domain.Entities.Product.Product> , IProductCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side

    #endregion
}
