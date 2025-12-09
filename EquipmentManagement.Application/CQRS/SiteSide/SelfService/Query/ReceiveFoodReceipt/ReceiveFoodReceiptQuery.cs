namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;

public record ReceiveFoodReceiptQuery(
    string Mobile, ulong MealPricingId) : 
    IRequest<ReceiveFoodReceiptDto?>;
