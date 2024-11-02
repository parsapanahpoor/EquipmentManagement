using EquipmentManagement.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

public class DecisionAbolitionRequestRespons : BaseEntities<ulong>
{
    public ulong AbolitionRequestId { get; set; }
    public ulong OrganizationChartId { get; set; }
    public ulong EmployeeUserId { get; set; }
    public string? RejectDescription { get; set; }
    public DecisionAbolitionRespons Response { get; set; }
    public int Sign { get; set; }
}

public enum DecisionAbolitionRespons
{
    [Display(Name = "درانتظار بررسی")] WaitingForRespons,
    [Display(Name = "تایید شده")] Accepted,
    [Display(Name = "رد شده")] Reject
}