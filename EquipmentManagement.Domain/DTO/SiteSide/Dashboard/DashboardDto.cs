using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Dashboard;

public record  DashboardDto
{
    public List<RepairRequestDto>? RepairRequest { get; set; }
    public List<AbolitionRequestDto>? AbolitionRequest { get; set; }
}

public record RepairRequestDto
{
    public ulong RepairRequestId { get; set; }
    public string? RequesterUsername { get; set; }
    public DateTime CreateDate { get; set; }
    public string? ProductName { get; set; }
    public VisitorRole VisitorRole { get; set; }
    public string? Description { get; set; }
    public RepairRequestState DesicionMakersRepairRequestState { get; set; }
    public RepairRequestState ExpertVisitorRepairRequestState { get; set; }
}

public record AbolitionRequestDto
{
    public ulong AbolitionRequestId { get; set; }
    public string? RequesterUsername { get; set; }
    public DateTime CreateDate { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public ExpertAbolitionRequestState ExpertVisitorAbolitionRequestState { get; set; }
}

public enum VisitorRole
{
    [Display(Name ="ناظر به عنوان کارشناس")]ExpertVisitor,
    [Display(Name ="ناظر به عنوان مسئول ")]DesicionMaker
}