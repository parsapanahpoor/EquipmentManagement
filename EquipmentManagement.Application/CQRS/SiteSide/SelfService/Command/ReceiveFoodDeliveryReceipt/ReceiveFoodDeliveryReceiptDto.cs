namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;

public record ReceiveFoodDeliveryReceiptDto(
    string Mobile,ulong MealPricingId);
