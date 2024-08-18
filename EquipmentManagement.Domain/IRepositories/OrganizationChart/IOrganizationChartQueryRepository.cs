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

    Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(string brandTitle,
        CancellationToken cancellation);

    Task<List<ulong>?> Get_OrganizationChartsIds_ByUserId(ulong userId,
        CancellationToken cancellation);

    Task<List<UserSelectedOrganizationChartEntity>> Get_UserSelectedOrganizationCharts_ByUserId(ulong userId,
        CancellationToken cancellationToken);

    Task<List<Entities.Users.User>> Get_Users_FromUserSelectedOrganizationChart(ulong organizationChartId,
        CancellationToken cancellationToken);

    Task<List<UserSelectedOrganizationChartEntity>> Get_RepairRequestDesiciners(CancellationToken cancellationToken);
    Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(CancellationToken cancellation);
}
