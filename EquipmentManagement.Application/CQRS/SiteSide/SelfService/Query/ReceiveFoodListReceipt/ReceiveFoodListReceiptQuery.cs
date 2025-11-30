namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;

public record ReceiveFoodListReceiptQuery(
    List<ulong> Ids) : 
    IRequest<List<ReceiveFoodListReceiptDto>?>;
