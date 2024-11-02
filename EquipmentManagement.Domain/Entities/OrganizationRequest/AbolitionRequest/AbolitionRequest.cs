using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

public class AbolitionRequest : BaseEntities<ulong>
{
    [ForeignKey("EmployeeUserId")]
    public Users.User? User { get; set; }
    public ulong EmployeeUserId { get; set; }

    [ForeignKey("ProductId")]
    public Product.Product? Product { get; set; }
    public ulong ProductId { get; set; }

    public AbolitionRequestState ExpertVisitorAbolitionRequestState { get; set; }
    public string? Description { get; set; }
    public bool IsFinally { get; set; }
}

public enum AbolitionRequestState
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}
