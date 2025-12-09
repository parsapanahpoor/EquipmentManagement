using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.Entities.Employee;

public sealed class EmployeeReceiveFoodDeliveryReceiptLog : 
    BaseEntities<ulong>
{
    public ulong EmployeeId { get; set; }

    public Employee? Employee { get; set; }
    public ulong? MealPricingId { get; set; }
    public MealPricing.MealPricing? MealPricing { get; set; }

}
