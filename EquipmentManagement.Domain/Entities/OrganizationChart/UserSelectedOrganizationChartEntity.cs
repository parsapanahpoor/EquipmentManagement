using EquipmentManagement.Domain.Entities.Users;

namespace EquipmentManagement.Domain.Entities.OrganizationChart;

public sealed class UserSelectedOrganizationChartEntity : BaseEntities<ulong>
{
    public ulong OrganizationChartId { get; set; }
    public ulong UserId { get; set; }

    public User User { get; set; }
    public OrganizationChartAggregate OrganizationChartAggregate { get; set; }
}
