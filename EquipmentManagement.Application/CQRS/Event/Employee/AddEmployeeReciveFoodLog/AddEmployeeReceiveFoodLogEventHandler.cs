using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.Event.Employee.AddEmployeeReciveFoodLog;

public class AddEmployeeReceiveFoodLogEventHandler(
    IEmployeeReceiveFoodDeliveryReceiptLogCommandRepository repository , 
    IUnitOfWork unitOfWork) :
    INotificationHandler<AddEmployeeReceiveFoodLogEvent>
{
    public async Task Handle(
        AddEmployeeReceiveFoodLogEvent notification,
        CancellationToken cancellationToken)
    {
        await repository.AddAsync(new Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog()
        {
            EmployeeId = notification.EmployeeId,
            MealPricingId=notification.MealPricingId,
        }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);   
    }
}