using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductRepairRequest;

public record FiltreProductRepairRequestQueryHandler (
    IProductQueryRepository productQueryRepository) : 
    IRequestHandler<FiltreProductRepairRequestQuery, FiltreProductRepairRequestDto>
{
    public async Task<FiltreProductRepairRequestDto> Handle(
        FiltreProductRepairRequestQuery request,
        CancellationToken cancellationToken)
        => await productQueryRepository.FiltreProductRepairRequest(request.filter , 
            cancellationToken);
}