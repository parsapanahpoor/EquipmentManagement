using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.GetUsersSelectedChart;

public record GetUsersSelectedChartQueryHandler(IOrganizationChartQueryRepository _OrganizationChartQueryRepository) :
    IRequestHandler<GetUsersSelectedChartQuery, List<Domain.Entities.Users.User>>
{
    public async Task<List<Domain.Entities.Users.User>> Handle(GetUsersSelectedChartQuery request, CancellationToken cancellationToken)
    => await _OrganizationChartQueryRepository.Get_Users_FromUserSelectedOrganizationChart(request.OrganizationChartId, 
        cancellationToken);
}
