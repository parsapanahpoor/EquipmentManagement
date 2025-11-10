using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public class CreateMealPricingCommand : IRequest<bool>
{
    #region properties

    public string? MealType { get; set; }
    public decimal Price { get; set; }

    #endregion
}
