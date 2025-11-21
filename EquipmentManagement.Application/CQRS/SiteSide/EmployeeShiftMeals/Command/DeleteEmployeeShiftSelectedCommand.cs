namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;

public record DeleteEmployeeShiftMealSelectedCommand(ulong EmployeeShiftMealSelectedId) : IRequest<bool>;
