namespace EquipmentManagement.Domain.Entities.Employee;

public sealed class EmployeeReceiveFoodDeliveryReceiptLog : 
    BaseEntities<ulong>
{
    public ulong EmployeeId { get; set; }
    public Employee? Employee { get; set; } 
}
