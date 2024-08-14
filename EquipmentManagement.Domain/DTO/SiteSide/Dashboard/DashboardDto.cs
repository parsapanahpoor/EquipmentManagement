using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Dashboard;

public record  DashboardDto
{
    public List<RepairRequestDto>? RepairRequest { get; set; }
}

public record RepairRequestDto
{
    public ulong RepairRequestId { get; set; }
    public string? RequesterUsername { get; set; }
    public DateTime CreateDate { get; set; }
    public string? ProductName { get; set; }
    public VisitorRole VisitorRole { get; set; }
    public string? Description { get; set; }
}

public enum VisitorRole
{
    [Display(Name ="ناظر به عنوان کارشناس")]ExpertVisitor,
    [Display(Name ="ناظر به عنوان مسئول ")]DesicionMaker
}