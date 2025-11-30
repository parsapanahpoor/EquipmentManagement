using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;

public class ReceiveFoodListReceiptQueryHandler(
    IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository repository,IEmployeeQueryRepository employeeQueryRepository) : 
    IRequestHandler<ReceiveFoodListReceiptQuery, List<ReceiveFoodListReceiptDto>?>
{
    public async Task<List<ReceiveFoodListReceiptDto>?> Handle(
        ReceiveFoodListReceiptQuery request, 
        CancellationToken cancellationToken)
    {
        var employeelogData = await employeeQueryRepository.GetEmployeeByIds(request.Ids, cancellationToken);
        if (employeelogData == null)
            return null;

        return employeelogData.Select(p=> new ReceiveFoodListReceiptDto(
            LogId: p.Id,
            FirstName: p.FirstName , 
            LastName: p.LastName , 
            Mobile: p.Mobile ,
            PersonnelCode: p.PersonnelCode, 
            CreatedDate: p.CreateDate)).ToList();
    }
}