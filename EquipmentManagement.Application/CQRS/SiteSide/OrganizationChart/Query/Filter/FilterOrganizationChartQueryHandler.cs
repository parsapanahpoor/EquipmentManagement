using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;

public record FilterOrganizationChartQueryHandler : IRequestHandler<FilterOrganizationChartQuery, FilterOrganizationChartsDto>
{
    #region Ctor 

    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;

    public FilterOrganizationChartQueryHandler(IOrganizationChartQueryRepository organizationChartQueryRepository)
    =>  _organizationChartQueryRepository = organizationChartQueryRepository;

    #endregion

    public async Task<FilterOrganizationChartsDto> Handle(FilterOrganizationChartQuery request, CancellationToken cancellationToken)
    => await _organizationChartQueryRepository.FilterOrganizationChart(request.Filter , cancellationToken);
}
