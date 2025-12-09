using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;

namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;

public class ReceiveFoodReceiptQueryHandler(
    IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository employeeReceiveFoodDeliveryReceiptLogQueryRepository,
    IEmployeeShiftSelectedQueryRepository employeeShiftSelectedQuery
    ) : 
    IRequestHandler<ReceiveFoodReceiptQuery, ReceiveFoodReceiptDto?>
{
    public async Task<ReceiveFoodReceiptDto?> Handle(
        ReceiveFoodReceiptQuery request, 
        CancellationToken cancellationToken)
    {
    
        var employeelogData = await employeeReceiveFoodDeliveryReceiptLogQueryRepository.GetFoodReceiptLogByEmployeeMobile(request.Mobile , cancellationToken);
        if (employeelogData == null)
            return null;


        var isDeny= await employeeReceiveFoodDeliveryReceiptLogQueryRepository.ExistsAsync(request.MealPricingId,employeelogData.EmployeeId, cancellationToken);
        var isFree = await employeeShiftSelectedQuery.VerifyByEmployeeAsync(employeelogData.EmployeeId, cancellationToken);
        return new ReceiveFoodReceiptDto(
            LogId: employeelogData.Id,
            FirstName: employeelogData.Employee.FirstName , 
            LastName: employeelogData.Employee.LastName , 
            Mobile: employeelogData.Employee.Mobile ,
            PersonnelCode: employeelogData.Employee.PersonnelCode,
            IsFree: isFree,
            IsDeny: isDeny,
            CreatedDate: employeelogData.CreateDate);
    }
}