using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;

public class FilterProductCategories : BasePaging<Entities.ProductCategory.ProductCategory>
{
    #region properties

    public string? Title { get; set; }

    #endregion
}
