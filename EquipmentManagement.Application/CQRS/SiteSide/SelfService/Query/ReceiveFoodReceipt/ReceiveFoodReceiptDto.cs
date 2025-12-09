namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;

public record ReceiveFoodReceiptDto(
    ulong LogId , 
    string FirstName , 
    string LastName , 
    string Mobile , 
    string PersonnelCode , 
    bool IsFree , 
    bool IsDeny, 
    DateTime CreatedDate);
