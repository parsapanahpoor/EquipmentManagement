using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

public record CreateMealPricingDTO
{
    #region properties

    public string MealType { get; set; } = string.Empty;
    public decimal Price { get; set; }

    #endregion
}
