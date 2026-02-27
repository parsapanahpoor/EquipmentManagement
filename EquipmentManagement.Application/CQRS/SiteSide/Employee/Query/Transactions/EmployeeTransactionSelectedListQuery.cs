using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeTransaction.Query;

public record EmployeeTransactionSelectedListQuery : IRequest<FilterEmployeeTransaction>
{
    public ulong? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
}
public record DropdownEmployeeTransactionSelectedListQuery : IRequest<List<DropdownItem>>
{
    public ulong EmployeeId { get; set; }

}
