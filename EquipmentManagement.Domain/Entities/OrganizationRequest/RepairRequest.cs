using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class RepairRequest : BaseEntities<ulong>
{
    public ulong EmployeeUserId { get; set; }
    public ulong ProductId { get; set; }
    public RepairRequestState RepairRequestState { get; set; }
    public string? Description { get; set; }
}

public enum RepairRequestState
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}
