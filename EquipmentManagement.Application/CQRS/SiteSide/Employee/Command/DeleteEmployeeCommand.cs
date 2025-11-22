namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;

public record DeleteEmployeeCommand(
    ulong EmployeeId) : 
    IRequest<bool>;
