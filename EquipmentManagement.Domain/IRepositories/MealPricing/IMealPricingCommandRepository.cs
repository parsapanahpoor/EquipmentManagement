using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.MealPricing;

public interface IMealPricingCommandRepository:ICommandGenericRepository<Domain.Entities.MealPricing.MealPricing>
{
    #region General Methods

    Task AddAsync(Domain.Entities.MealPricing.MealPricing MealPricing, CancellationToken cancellationToken);

    void Update(Entities.MealPricing.MealPricing MealPricing);

    #endregion
}
