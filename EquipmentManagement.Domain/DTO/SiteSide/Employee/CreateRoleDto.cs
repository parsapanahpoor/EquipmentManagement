using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Employee;

public record CreateEmployeeDto
{
    [Required]
    public List<ulong> PlaceOfServiceId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? PersonnelCode { get; set; }
    [Required]
    public string? Mobile { get; set; }
    public bool CanReceiveFood { get; set; }

    [Required]
    public string? RFId { get; set; }
}
public record CreateEmployeeTransactionDto
{
    public string? SerialId { get; set; }
    public string? RRN { get; set; }
    public ulong EmployeeId { get; set; }
    public string? Description { get; set; }
    public long Amount { get; set; }
    public bool Paid { get; set; } = false;
}
