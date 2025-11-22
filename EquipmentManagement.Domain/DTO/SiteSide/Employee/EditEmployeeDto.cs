using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Employee;

public record EditEmployeeDto
{
    public ulong Id { get; set; }
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

public enum EditEmployeeResult
{
    Success,
    EmployeeNotFound,
    UniqueNameExists,
    Faild
}