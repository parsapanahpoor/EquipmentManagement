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

    public ExpertAbolitionRequestState ExpertVisitorAbolitionRequestState { get; set; }
    public string? Description { get; set; }
    public AbolitionRequestState RequestState { get; set; }
}

public enum ExpertAbolitionRequestState
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}
public enum AbolitionRequestState
{
    [Display(Name = "درانتظار بررسی مدیر")] WaitingForManagerRespons,
    [Display(Name = "درانتظار بررسی جمع دار اموال")] WaitingForProductsCollectorRespons,
    [Display(Name = "رد شده")] Reject,
    [Display(Name = "بسته شده")] Finally,
}
