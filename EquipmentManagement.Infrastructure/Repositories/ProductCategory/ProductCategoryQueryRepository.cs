using EquipmentManagement.Domain.IRepositories.ProductCategory;
namespace EquipmentManagement.Infrastructure.Repositories.ProductCategory;

public class ProductCategoryQueryRepository : QueryGenericRepository<Domain.Entities.ProductCategory.ProductCategory>, IProductCategoryQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductCategoryQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Admin Side

    public async Task<>

    #endregion
}

