using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

namespace EquipmentManagement.Domain.IRepositories.MealPricing;

public interface IMealPricingQueryRepository
{
    #region General Methods

    Task<Entities.MealPricing.MealPricing> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    Task<FilterMealPricing> FilterMealPricing(FilterMealPricing filter);



    Task<List<FilterMealPricing>> FillSelectListOfMealPricingDTO(FilterMealPricing filter, CancellationToken cancellation);

    #endregion
}
