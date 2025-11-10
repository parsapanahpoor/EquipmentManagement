using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

public class SelectListOfMealPricingDTO
{
    #region properties

    public string? MealType { get; set; }
    public ulong MealPricingId { get; set; }

    #endregion
}
