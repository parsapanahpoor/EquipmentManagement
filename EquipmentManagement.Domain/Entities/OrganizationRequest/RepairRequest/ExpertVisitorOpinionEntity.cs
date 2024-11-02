using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class ExpertVisitorOpinionEntity : BaseEntities<ulong>
{
    public ulong RepairRequestId { get; set; }
    public ulong ExpertUserId { get; set; }
    public ExpertVisitorResponsType ResponsType { get; set; }
    public string? Description { get; set; }
}

public enum ExpertVisitorResponsType
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}