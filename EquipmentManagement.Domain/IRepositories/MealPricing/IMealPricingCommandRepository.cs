using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.IRepositories.MealPricing;

public interface IMealPricingCommandRepository
{
    #region General Methods

    Task AddAsync(Domain.Entities.MealPricing.MealPricing MealPricing, CancellationToken cancellationToken);

    void Update(Entities.MealPricing.MealPricing MealPricing);

    #endregion
}
