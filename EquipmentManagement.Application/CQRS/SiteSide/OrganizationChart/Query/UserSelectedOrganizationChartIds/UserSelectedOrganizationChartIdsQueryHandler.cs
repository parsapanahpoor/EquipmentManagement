
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.UserSelectedOrganizationChartIds;

public record UserSelectedOrganizationChartIdsQueryHandler : IRequestHandler<UserSelectedOrganizationChartIdsQuery, List<ulong>>
{
    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;

    public UserSelectedOrganizationChartIdsQueryHandler(IOrganizationChartQueryRepository organizationChartQueryRepository)
    {
        _organizationChartQueryRepository = organizationChartQueryRepository;
    }

    public async Task<List<ulong>?> Handle(UserSelectedOrganizationChartIdsQuery request, CancellationToken cancellationToken)
    => await _organizationChartQueryRepository.Get_OrganizationChartsIds_ByUserId(request.userId , cancellationToken);
}
