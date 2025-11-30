using EquipmentManagement.Domain.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Employee;

public class FilterEmployeesDto :
    BasePaging<Domain.Entities.Employee.Employee>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
    public bool CanReceiveFood { get; set; }
    public string? RFId { get; set; }
  
}
public class FilterSelectedEmployeesDto :
    BasePaging<Domain.Entities.Employee.Employee>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
    public bool CanReceiveFood { get; set; }
    public string? RFId { get; set; }
    public List<ulong>? EmployeeIds = new();

}
