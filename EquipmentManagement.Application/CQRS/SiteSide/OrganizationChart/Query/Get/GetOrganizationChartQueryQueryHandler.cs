using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Get;

public record GetGetOrganizationChartQueryHandler : 
    IRequestHandler<GetOrganizationChartQuery, OrganizationChartEntryModel>
{
    #region Ctor

    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;

    public GetGetOrganizationChartQueryHandler(IOrganizationChartQueryRepository organizationChartQueryRepository)
    => _organizationChartQueryRepository = organizationChartQueryRepository;

    #endregion

    public async Task<OrganizationChartEntryModel?> Handle(GetOrganizationChartQuery request, CancellationToken cancellationToken)
    => await _organizationChartQueryRepository.FillOrganizationChartEntryModel(request.OrganizationChartId, 
            cancellationToken);
}
