using EquipmentManagement.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class DecisionRepairRequestRespons : BaseEntities<ulong>
{
    public ulong RepariRequestId { get; set; }
    public ulong OrganizationChartId { get; set; }
    public ulong EmployeeUserId { get; set; }
    public string? RejectDescription { get; set; }
    public DecisionRepairRespons Response { get; set; }
    public int Sign { get; set; }
}

public enum DecisionRepairRespons
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}