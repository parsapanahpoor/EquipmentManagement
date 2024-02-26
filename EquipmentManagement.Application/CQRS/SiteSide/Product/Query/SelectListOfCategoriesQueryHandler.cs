using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public record SelectListOfCategoriesQueryHandler : IRequestHandler<SelectListOfCategoriesQuery, List<SelectListOfCategoriesDTO>>
{
    #region Ctor

    private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;

    public SelectListOfCategoriesQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository)
    {
        _productCategoryQueryRepository = productCategoryQueryRepository;
    }

    #endregion

    public async Task<List<SelectListOfCategoriesDTO>> Handle(SelectListOfCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _productCategoryQueryRepository.FillSelectListOfCategoriesDTO(cancellationToken);
    }
}
