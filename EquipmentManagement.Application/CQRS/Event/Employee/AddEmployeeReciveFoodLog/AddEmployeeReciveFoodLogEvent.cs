namespace EquipmentManagement.Application.CQRS.Event.Employee.AddEmployeeReciveFoodLog;

public record AddEmployeeReceiveFoodLogEvent(
    ulong EmployeeId) : 
    INotification;
