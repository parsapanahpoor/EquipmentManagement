using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductAbolitionRequest;

public record FiltreProductAbolitionRequestQuery(
    FiltreProductAbolitionRequestDto filter) : 
    IRequest<FiltreProductAbolitionRequestDto>;
