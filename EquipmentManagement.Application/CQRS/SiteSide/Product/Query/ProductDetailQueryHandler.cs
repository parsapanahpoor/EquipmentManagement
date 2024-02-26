using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, ProductDetailDTO>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;

    public ProductDetailQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    #endregion

    public async Task<ProductDetailDTO?> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.FillProductDetailDTO(request.ProductId , cancellationToken);
    }
}
