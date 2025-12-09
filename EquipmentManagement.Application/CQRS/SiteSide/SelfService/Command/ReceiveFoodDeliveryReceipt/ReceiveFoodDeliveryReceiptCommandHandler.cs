using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.CQRS.Event.Employee.AddEmployeeReciveFoodLog;
using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;

namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;

public record ReceiveFoodDeliveryReceiptCommandHandler(
    IEmployeeQueryRepository employeeQueryRepository,
   
         IEmployeeShiftMealSelectedQueryRepository employeeShiftSelectedQuery,
    IMediator mediator,
    IUnitOfWork uow) :
    IRequestHandler<ReceiveFoodDeliveryReceiptCommand, bool>
{
    public async Task<bool> Handle(ReceiveFoodDeliveryReceiptCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeQueryRepository.GetEmployeeByMobile(request.model.Mobile, cancellationToken);
        if (employee is null)
            throw new Exception("کارمند با این شماره موبایل یافت نشد");

        await mediator.Publish(new AddEmployeeReceiveFoodLogEvent(employee.Id, request.model.MealPricingId), cancellationToken);

        return true;
    }
}
