using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.Get;

public record GetOrganizationRequestQuery(ulong OrganizationRequestId) : 
    IRequest<OrganizationRequestEntryModel>;
