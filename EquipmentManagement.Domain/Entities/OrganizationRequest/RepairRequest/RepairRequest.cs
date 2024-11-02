using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class RepairRequest : BaseEntities<ulong>
{
    [ForeignKey("EmployeeUserId")]
    public Users.User? User { get; set; }
    public ulong EmployeeUserId { get; set; }

    [ForeignKey("ProductId")]
    public Product.Product? Product { get; set; }
    public ulong ProductId { get; set; }

    public RepairRequestState DesicionMakersRepairRequestState { get; set; }
    public RepairRequestState ExpertVisitorRepairRequestState { get; set; }
    public string? Description { get; set; }
    public bool IsNeedToOutSource { get; set; }
}

public enum RepairRequestState
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}
