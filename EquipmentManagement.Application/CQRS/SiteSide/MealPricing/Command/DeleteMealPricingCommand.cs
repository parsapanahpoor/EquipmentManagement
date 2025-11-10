namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public record DeleteMealPricingCommand(ulong MealPricingId) : IRequest<bool>;
