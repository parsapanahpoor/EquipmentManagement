using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.Employee;

public class FilterEmployeeReceiveFoodsLogDto :
    BasePaging<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
}
