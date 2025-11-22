using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record EditMealPricingQuery(ulong Id) : IRequest<EditMealPricingDTO>;
