using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;

public class ReceiveFoodReceiptQueryHandler(
    IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository repository) : 
    IRequestHandler<ReceiveFoodReceiptQuery, ReceiveFoodReceiptDto?>
{
    public async Task<ReceiveFoodReceiptDto?> Handle(
        ReceiveFoodReceiptQuery request, 
        CancellationToken cancellationToken)
    {
        var employeelogData = await repository.GetFoodReceiptLogByEmployeeMobile(request.Mobile , cancellationToken);
        if (employeelogData == null)
            return null;

        return new ReceiveFoodReceiptDto(
            LogId: employeelogData.Id,
            FirstName: employeelogData.Employee.FirstName , 
            LastName: employeelogData.Employee.LastName , 
            Mobile: employeelogData.Employee.Mobile ,
            PersonnelCode: employeelogData.Employee.PersonnelCode, 
            CreatedDate: employeelogData.CreateDate);
    }
}