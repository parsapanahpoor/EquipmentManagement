using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;

public class FilterEmployeesQuery : IRequest<FilterEmployeesDto>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
}
public class FilterSelectedEmployeesQuery : IRequest<FilterSelectedEmployeesDto>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
    public int Page { get; set; } = 1;
    public List<ulong>? EmployeeIds = new();
}
