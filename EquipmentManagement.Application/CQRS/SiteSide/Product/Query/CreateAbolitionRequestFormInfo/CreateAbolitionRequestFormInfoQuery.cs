using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateAbolitionRequestFormInfo;

public record CreateAbolitionRequestFormInfoQuery(
    ulong ProductId , 
    ulong UserId) : 
    IRequest<CreateAbolitionRequestFormInfoDto>;
