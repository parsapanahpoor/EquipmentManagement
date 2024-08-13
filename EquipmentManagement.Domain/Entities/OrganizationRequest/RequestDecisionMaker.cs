using EquipmentManagement.Domain.Entities.OrganizationChart;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class RequestDecisionMaker : BaseEntities<ulong>
{
    [ForeignKey("OrganziationRequestId")]
    public virtual OrganziationRequestEntity? OrganziationRequestEntity { get; set; }
    public ulong OrganziationRequestId { get; set; }

    [ForeignKey("OrganizationChartId")]
    public virtual OrganizationChartAggregate? OrganizationChartAggregate { get; set; }
    public ulong OrganizationChartId { get; set; }
}
