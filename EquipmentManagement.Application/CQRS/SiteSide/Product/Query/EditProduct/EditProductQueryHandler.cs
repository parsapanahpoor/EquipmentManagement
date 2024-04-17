using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.EditProduct;

public class EditProductQueryHandler : IRequestHandler<EditProductQuery, EditProductDTO>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;

    public EditProductQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    #endregion

    public async Task<EditProductDTO?> Handle(EditProductQuery request, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.Fill_EditProductDTO(request.ProductId , cancellationToken);
    }
}
