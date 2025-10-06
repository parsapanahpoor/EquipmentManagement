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
}
