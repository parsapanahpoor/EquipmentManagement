using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.ListOfOrganizationCharts;

public record ListOfOrganizationChartQueryHandler : IRequestHandler<ListOfOrganizationQuery, List<OrganizationChartSelectedForUserDto>>
{
    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;

    public ListOfOrganizationChartQueryHandler(IOrganizationChartQueryRepository organizationChartQueryRepository)
    => _organizationChartQueryRepository = organizationChartQueryRepository;

    public async Task<List<OrganizationChartSelectedForUserDto>?> Handle(ListOfOrganizationQuery request, CancellationToken cancellationToken)
    => await _organizationChartQueryRepository.FillOrganizationChartSelectedForUserDto(cancellationToken);
}
