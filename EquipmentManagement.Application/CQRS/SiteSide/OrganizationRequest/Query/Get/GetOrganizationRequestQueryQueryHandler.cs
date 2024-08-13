using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.Get;

public record GetGetOrganizationRequestQueryHandler : 
    IRequestHandler<GetOrganizationRequestQuery, OrganizationRequestEntryModel>
{
    #region Ctor

    private readonly IOrganziationRequestQueryRepository _organizationRequestQueryRepository;

    public GetGetOrganizationRequestQueryHandler(IOrganziationRequestQueryRepository organizationRequestQueryRepository)
    => _organizationRequestQueryRepository = organizationRequestQueryRepository;

    #endregion

    public async Task<OrganizationRequestEntryModel?> Handle(GetOrganizationRequestQuery request, CancellationToken cancellationToken)
    => await _organizationRequestQueryRepository.FillOrganizationRequestEntryModel(request.OrganizationRequestId, 
            cancellationToken);
}
