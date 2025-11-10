using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;

public record MealPricingSelectedListQuery : IRequest<FilterMealPricing>
{
    public string? MealType { get; set; }
    public decimal? Price { get; set; }
}
