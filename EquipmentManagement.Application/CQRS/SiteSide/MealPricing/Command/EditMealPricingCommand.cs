using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public class EditMealPricingCommand : IRequest<bool>
{
    #region properties

    public MealType MealType { get; set; }
    public decimal Price { get; set; }
    public ulong Id { get; set; }

    #endregion
}
