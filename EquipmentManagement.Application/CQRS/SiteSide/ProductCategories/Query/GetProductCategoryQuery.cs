using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;

public class GetProductCategoryQuery : IRequest<EditProductCategoryDTO>
{
    #region properties

    public ulong ProductCategoryId { get; set; }

    #endregion
}
