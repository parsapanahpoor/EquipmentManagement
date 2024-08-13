using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Domain.IRepositories.OranizationRequest;

public interface IOrganziationRequestQueryRepository
{
    Task<FilterOrganizationRequestsDto> FilterOrganziationRequest(FilterOrganizationRequestsDto filter,
        CancellationToken cancellationToken);
}
