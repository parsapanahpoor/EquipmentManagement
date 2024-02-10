using EquipmentManagement.Domain.DTOs.Common;

namespace EquipmentManagement.Domain.DTO.AdminSide.ProductCategory;

public class FilterProductCategories : BasePaging<Entities.ProductCategory.ProductCategory>
{
    #region properties

    public string? Title { get; set; }

    #endregion
}
