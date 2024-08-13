using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.Filter;

public record FilterOrganizationRequestsQueryHandler : IRequestHandler<FilterOrganizationRequestsQuery, FilterOrganizationRequestsDto>
{
    #region Ctor 

    private readonly IOrganziationRequestQueryRepository _OrganziationRequestQueryRepository;

    public FilterOrganizationRequestsQueryHandler(IOrganziationRequestQueryRepository OrganziationRequestQueryRepository)
    => _OrganziationRequestQueryRepository = OrganziationRequestQueryRepository;

    #endregion

    public async Task<FilterOrganizationRequestsDto> Handle(FilterOrganizationRequestsQuery request, CancellationToken cancellationToken)
    => await _OrganziationRequestQueryRepository.FilterOrganziationRequest(request.Filter!, cancellationToken);
}
