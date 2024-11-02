using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductAbolitionRequest;

public record FiltreProductAbolitionRequestQueryHandler (
    IProductQueryRepository productQueryRepository) : 
    IRequestHandler<FiltreProductAbolitionRequestQuery, FiltreProductAbolitionRequestDto>
{
    public async Task<FiltreProductAbolitionRequestDto> Handle(
        FiltreProductAbolitionRequestQuery request,
        CancellationToken cancellationToken)
        => await productQueryRepository.FiltreProductAbolitionRequest(request.filter , 
            cancellationToken);
}