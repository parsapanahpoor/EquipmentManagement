using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateRepairRequestFormInfo;

public record CreateRepairRequestFormInfoQuery(
    ulong ProductId , 
    ulong UserId) : 
    IRequest<CreateRepairRequestFormInfoDto>;
