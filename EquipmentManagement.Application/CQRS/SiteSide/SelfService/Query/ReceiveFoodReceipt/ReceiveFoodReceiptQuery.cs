namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;

public record ReceiveFoodReceiptQuery(
    string Mobile) : 
    IRequest<ReceiveFoodReceiptDto?>;
