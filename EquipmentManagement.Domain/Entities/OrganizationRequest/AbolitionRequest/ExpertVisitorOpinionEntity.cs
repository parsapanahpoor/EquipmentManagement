using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

public class ExpertVisitorOpinionForAbolitionRequestEntity : BaseEntities<ulong>
{
    public ulong AbolitionRequestId { get; set; }
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