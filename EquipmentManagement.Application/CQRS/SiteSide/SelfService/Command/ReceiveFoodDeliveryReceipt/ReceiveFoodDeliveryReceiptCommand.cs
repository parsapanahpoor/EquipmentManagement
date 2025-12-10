namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;

public record ReceiveFoodDeliveryReceiptCommand(
    ReceiveFoodDeliveryReceiptDto model) :
    IRequest<ReceiveFoodDeliveryReceiptCommandResult>;
