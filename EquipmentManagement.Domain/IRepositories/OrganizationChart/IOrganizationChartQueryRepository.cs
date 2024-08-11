using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.DTO.SiteSide.Places;
using EquipmentManagement.Domain.Entities.OrganizationChart;

namespace EquipmentManagement.Domain.IRepositories.OrganizationChart;

public interface IOrganizationChartQueryRepository
{
    Task<FilterOrganizationChartsDto> FilterOrganizationChart(FilterOrganizationChartsDto organizationChartAggregate, 
        CancellationToken cancellationToken);

    Task<OrganizationChartAggregate> GetByIdAsync(CancellationToken cancellationToken, 
        params object[] ids);

    Task<OrganizationChartEntryModel?> FillOrganizationChartEntryModel(ulong organizationChartId,
        CancellationToken cancellation);
}
