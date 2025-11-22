using EquipmentManagement.Domain.Entities.MealPricing;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.Employee;

public class Employee : BaseEntities<ulong>
{
    public ulong PlaceOfServiceId { get; set; }
    public PlaceOfService.PlaceOfService? PlaceOfService { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
    public string? RFId { get; set; }
    public bool? CanReceiveFood { get; set; }

    public ICollection<EmployeeReceiveFoodDeliveryReceiptLog> EmployeeReceiveFoodDeliveryReceiptLogs { get; set; } = [];
    public ICollection<EmployeeShiftSelected> EmployeeShiftSelected { get; set; } = [];
}
public class EmployeeShiftSelected : BaseEntities<ulong>
{
    public DateOnly Date { get; set; }
    public ulong EmployeeId { get; set; }
    public Employee Employee { get; set; } = new();
    public ICollection<EmployeeShiftMealSelected> EmployeeShiftMealFSelected { get; set; } = [];
}
public class EmployeeShiftMealSelected : BaseEntities<ulong>
{
    public MealType Meal { get; set; }
    public ulong EmployeeShiftSelectedId { get; set; }
    public EmployeeShiftSelected EmployeeShiftSelected { get; set; } = new();
}