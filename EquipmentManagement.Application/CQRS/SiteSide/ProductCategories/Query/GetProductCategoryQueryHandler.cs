using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;

public record GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, EditProductCategoryDTO>
{
    #region Ctor

    private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;

    public GetProductCategoryQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository)
    {
        _productCategoryQueryRepository = productCategoryQueryRepository;
    }

    #endregion

    public async Task<EditProductCategoryDTO?> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
    {
        //Get Product Category By Id 
        return await _productCategoryQueryRepository.FillEditProductCategoryDTO(request.ProductCategoryId , cancellationToken);
    }
}
