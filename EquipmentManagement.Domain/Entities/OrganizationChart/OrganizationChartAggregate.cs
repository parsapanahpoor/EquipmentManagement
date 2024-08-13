using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.Entities.OrganizationChart;

public class OrganizationChartAggregate : BaseEntities<ulong>
{
    public ulong? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public ICollection<UserSelectedOrganizationChartEntity> UserSelectedOrganizationChartEntities { get; set; } = [];
    public virtual List<RequestDecisionMaker> DecisionMakers { get; set; } = [];
}
