using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.MealPricing;

public interface IMealPricingQueryRepository:IQueryGenericRepository<Domain.Entities.MealPricing.MealPricing>
{
    #region General Methods

    Task<Entities.MealPricing.MealPricing> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    Task<FilterMealPricing> FilterMealPricing(FilterMealPricing filter);


    Task<List<DropdownItem>> GetDropDownMealPricing(FilterMealPricing filter, CancellationToken cancellationToken);
    Task<List<FilterMealPricing>> FillSelectListOfMealPricingDTO(FilterMealPricing filter, CancellationToken cancellation);

    #endregion
}
