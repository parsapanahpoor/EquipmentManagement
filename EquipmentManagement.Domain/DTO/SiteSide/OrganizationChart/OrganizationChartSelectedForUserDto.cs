using EquipmentManagement.Domain.Entities.OrganizationChart;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

public record OrganizationChartSelectedForUserDto
{
    public OrganizationChartAggregate? OrganizationChart { get; set; }
    public List<OrganizationChartAggregate>? OrganizationChartChildren { get; set; }
}
