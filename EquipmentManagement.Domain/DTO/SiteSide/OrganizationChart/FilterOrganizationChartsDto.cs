using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.OrganizationChart;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

public class FilterOrganizationChartsDto : BasePaging<OrganizationChartAggregate>
{
    #region properties

    public string? Title { get; set; }

    public ulong? ParentId { get; set; }

    #endregion
}