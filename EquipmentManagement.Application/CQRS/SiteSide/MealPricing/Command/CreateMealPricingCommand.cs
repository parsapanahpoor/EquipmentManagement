using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public class CreateMealPricingCommand : IRequest<bool>
{
    #region properties

    public MealType MealType { get; set; }
    public decimal Price { get; set; }

    #endregion
}
