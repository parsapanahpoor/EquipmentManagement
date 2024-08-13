using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RequestSelectedChartIds;

public record RequestSelectedChartIdsQueryHandler : IRequestHandler<RequestSelectedChartIdsQuery, List<ulong>>
{
    private readonly IOrganziationRequestQueryRepository _organziationRequestQueryRepository;

    public RequestSelectedChartIdsQueryHandler(IOrganziationRequestQueryRepository organizationChartQueryRepository)
    {
        _organziationRequestQueryRepository = organizationChartQueryRepository;
    }

    public async Task<List<ulong>?> Handle(RequestSelectedChartIdsQuery request, CancellationToken cancellationToken)
    => await _organziationRequestQueryRepository.Get_OrganizationChartsIds_ByRequestId(request.requestId, cancellationToken);
}
