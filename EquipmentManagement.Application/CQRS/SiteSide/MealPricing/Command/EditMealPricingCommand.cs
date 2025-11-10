using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public class EditMealPricingCommand : IRequest<bool>
{
    #region properties

    public string MealType { get; set; }=string.Empty;
    public decimal Price { get; set; }
    public ulong Id { get; set; }

    #endregion
}
