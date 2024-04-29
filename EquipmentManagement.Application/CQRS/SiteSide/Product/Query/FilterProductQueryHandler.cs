using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public record FilterProductQueryHandler : IRequestHandler<FilterProductQuery, FilterProductDTO>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;

    public FilterProductQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    #endregion

    public async Task<FilterProductDTO> Handle(FilterProductQuery request, CancellationToken cancellationToken = default)
    {
        return await _productQueryRepository.FilterProducts(request.filter);
    }
}
