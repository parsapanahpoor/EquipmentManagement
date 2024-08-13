using EquipmentManagement.Domain.Entities.OrganizationChart;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class RequestDecisionMaker : BaseEntities<ulong>
{
    public virtual OrganziationRequestEntity? OrganziationRequestEntity { get; set; }
    public ulong OrganziationRequestId { get; set; }

    public virtual OrganizationChartAggregate? OrganizationChartAggregate { get; set; }
    public ulong OrganizationChartId { get; set; }
}
