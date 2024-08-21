using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductRepairRequest;

public record FiltreProductRepairRequestQuery(
    FiltreProductRepairRequestDto filter) : 
    IRequest<FiltreProductRepairRequestDto>;
