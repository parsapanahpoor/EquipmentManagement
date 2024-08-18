using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RepairRequestDetail;

public record RepairRequestDetailQuery(
    ulong UserId , 
    ulong RepairRequestId) : 
    IRequest<RepairRequestDetailDto?>;