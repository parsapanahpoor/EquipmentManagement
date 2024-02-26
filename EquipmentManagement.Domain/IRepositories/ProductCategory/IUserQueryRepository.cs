using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;

namespace EquipmentManagement.Domain.IRepositories.ProductCategory;

public interface IProductCategoryQueryRepository
{
    #region General Methods

    Task<Entities.ProductCategory.ProductCategory> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<FilterProductCategories> FilterProductCategories(FilterProductCategories filter);

    Task<EditProductCategoryDTO?> FillEditProductCategoryDTO(ulong categoryId, CancellationToken cancellation);

    Task<bool> IsExistAny_Category_ByCategoryId(ulong categoryId, CancellationToken cancellationToken);

    Task<List<SelectListOfCategoriesDTO>> FillSelectListOfCategoriesDTO(CancellationToken cancellation);

    #endregion
}
