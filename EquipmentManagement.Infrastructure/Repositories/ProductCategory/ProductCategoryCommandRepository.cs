using EquipmentManagement.Domain.IRepositories.ProductCategory;
namespace EquipmentManagement.Infrastructure.Repositories.ProductCategory;

public class ProductCategoryCommandRepository : CommandGenericRepository<Domain.Entities.ProductCategory.ProductCategory>, IProductCategoryCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductCategoryCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}

