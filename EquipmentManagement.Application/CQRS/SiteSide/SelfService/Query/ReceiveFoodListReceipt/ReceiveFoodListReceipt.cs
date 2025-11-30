namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;

public record ReceiveFoodListReceiptDto(
    ulong LogId , 
    string FirstName , 
    string LastName , 
    string Mobile , 
    string PersonnelCode , 
    DateTime CreatedDate);
