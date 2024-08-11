using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.ListOfOrganizationCharts;

public record ListOfOrganizationChartTitleQueryHandler : IRequestHandler<ListOfOrganizationWithTitleQuery, List<OrganizationChartSelectedForUserDto>?>
{
    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;

    public ListOfOrganizationChartTitleQueryHandler(IOrganizationChartQueryRepository organizationChartQueryRepository)
    => _organizationChartQueryRepository = organizationChartQueryRepository;

    public async Task<List<OrganizationChartSelectedForUserDto>?> Handle(ListOfOrganizationWithTitleQuery request, CancellationToken cancellationToken)
       => await _organizationChartQueryRepository.FillOrganizationChartSelectedForUserDto(request.title, cancellationToken);

}
