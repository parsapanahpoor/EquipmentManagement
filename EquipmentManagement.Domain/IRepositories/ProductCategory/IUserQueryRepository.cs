using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;

namespace EquipmentManagement.Domain.IRepositories.ProductCategory;

public interface IProductCategoryQueryRepository
{
    #region General Methods

    Task<FilterProductCategories> FilterProductCategories(FilterProductCategories filter);

    #endregion
}
