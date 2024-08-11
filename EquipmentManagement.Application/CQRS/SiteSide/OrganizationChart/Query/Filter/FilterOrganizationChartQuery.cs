using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;

public class FilterOrganizationChartQuery : IRequest<FilterOrganizationChartsDto>
{
    #region properties

    public FilterOrganizationChartsDto Filter { get; set; }

    #endregion
}
