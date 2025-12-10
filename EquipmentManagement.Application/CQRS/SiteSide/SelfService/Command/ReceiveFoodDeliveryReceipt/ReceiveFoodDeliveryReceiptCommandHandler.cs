using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.CQRS.Event.Employee.AddEmployeeReciveFoodLog;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using EquipmentManagement.Domain.IRepositories.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;

public record ReceiveFoodDeliveryReceiptCommandResult(bool Status, string Mobile);
public record ReceiveFoodDeliveryReceiptCommandHandler(
    IEmployeeQueryRepository employeeQueryRepository,
    IMealPricingQueryRepository MealPricingQueryRepository,

         IEmployeeShiftMealSelectedQueryRepository employeeShiftSelectedQuery,
    IMediator mediator,
    IUnitOfWork uow) :
    IRequestHandler<ReceiveFoodDeliveryReceiptCommand, ReceiveFoodDeliveryReceiptCommandResult>
{
    public async Task<ReceiveFoodDeliveryReceiptCommandResult> Handle(ReceiveFoodDeliveryReceiptCommand request, CancellationToken cancellationToken)
    {

        if (!await MealPricingQueryRepository.IsExistAnyByIdAsync(request.model.MealPricingId, cancellationToken))
            throw new Exception("وعده مورد نظر را انتخاب کنید");


        EquipmentManagement.Domain.Entities.Employee.Employee? employee;
        if (request.model.RFId is not null)
            employee = await employeeQueryRepository.GetEmployeeByRFID(request.model.RFId, cancellationToken);
        else
            employee = await employeeQueryRepository.GetEmployeeByMobile(request.model.Mobile, cancellationToken);
        if (employee is null)
            throw new Exception("کارمند با این شماره موبایل یافت نشد");

        await mediator.Publish(new AddEmployeeReceiveFoodLogEvent(employee.Id, request.model.MealPricingId), cancellationToken);

        return new ReceiveFoodDeliveryReceiptCommandResult(true, employee.Mobile!);
    }
}
