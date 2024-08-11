namespace EquipmentManagement.Domain.Entities.OrganizationChart;

public sealed class OrganizationChartAggregate : BaseEntities<ulong>
{
    public ulong? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public ICollection<UserSelectedOrganizationChartEntity> UserSelectedOrganizationChartEntities { get; set; }
}
