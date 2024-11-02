using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.AbolitionRequestDetail;

public record AbolitionRequestDetailQuery(
    ulong UserId , 
    ulong abolitionRequestId) : 
    IRequest<AbolitionRequestDetailDto?>;