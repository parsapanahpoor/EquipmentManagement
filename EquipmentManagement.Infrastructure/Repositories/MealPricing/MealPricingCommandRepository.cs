using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
namespace EquipmentManagement.Infrastructure.Repositories.ProductCategory;

public class MealPricingCommandRepository : CommandGenericRepository<Domain.Entities.MealPricing. MealPricing>, IMealPricingCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public MealPricingCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}

