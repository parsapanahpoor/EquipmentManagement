namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;

public record DeleteEmployeeShiftSelectedCommand(ulong EmployeeShiftSelectedId) : IRequest<bool>;
