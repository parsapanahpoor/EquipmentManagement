using EquipmentManagement.Domain.Entities.ProductCategory;

namespace EquipmentManagement.Domain.IRepositories.ProductCategory;

public interface IProductCategoryCommandRepository
{
    #region General Methods

    Task AddAsync(Domain.Entities.ProductCategory.ProductCategory productCategory, CancellationToken cancellationToken);

    void Update(Entities.ProductCategory.ProductCategory productCategory);

    #endregion
}
