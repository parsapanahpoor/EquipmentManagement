using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;

public record FilterProductCatgeoriesQueryHandler : IRequestHandler<FilterProductCatgeoriesQuery, FilterProductCategories>
{
    #region Ctor 

    private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;

    public FilterProductCatgeoriesQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository)
    {
        _productCategoryQueryRepository = productCategoryQueryRepository;
    }

    #endregion
    public async Task<FilterProductCategories> Handle(FilterProductCatgeoriesQuery request, CancellationToken cancellationToken)
    {
        FilterProductCategories model = new()
        {
            Title = request.Title,
        };

        return await _productCategoryQueryRepository.FilterProductCategories(model);
    }
}
