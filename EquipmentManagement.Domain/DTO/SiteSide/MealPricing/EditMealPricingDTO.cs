using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

public record EditMealPricingDTO 
{
    #region properties

    public ulong MealPricingId { get; set; }

    public string MealType { get; set; } = string.Empty;
    public decimal Price { get; set; }

    #endregion
}
