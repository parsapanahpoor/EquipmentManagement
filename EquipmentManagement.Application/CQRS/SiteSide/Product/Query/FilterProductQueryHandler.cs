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

    public async Task<FilterProductDTO> Handle(FilterProductQuery request, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.FilterProducts(new FilterProductDTO()
        {
            CategoryTitle = request.CategoryTitle,
            CategoryId = request.CategoryId,
            PlaceId = request.PlaceId,
            PlaceTitle = request.PlaceTitle,
            ProductTitle = request.ProductTitle,
        });
    }
}
