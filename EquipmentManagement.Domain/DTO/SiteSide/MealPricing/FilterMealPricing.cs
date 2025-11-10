using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

public class FilterMealPricing : BasePaging<Entities.MealPricing.MealPricing>
{
    #region properties

    public string? MealType { get; set; }
    public decimal? Price { get; set; }

    #endregion
}
