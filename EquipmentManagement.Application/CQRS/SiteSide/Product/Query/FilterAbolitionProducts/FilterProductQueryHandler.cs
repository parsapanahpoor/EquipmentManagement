using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FilterAbolitionProducts;

public record FilterAbolitionProductsQueryHandler : IRequestHandler<FilterAbolitionProductsQuery, FilterProductDTO>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;

    public FilterAbolitionProductsQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    #endregion

    public async Task<FilterProductDTO> Handle(FilterAbolitionProductsQuery request, CancellationToken cancellationToken = default)
    {
        return await _productQueryRepository.FilterAbolitionProducts(request.filter);
    }
}
