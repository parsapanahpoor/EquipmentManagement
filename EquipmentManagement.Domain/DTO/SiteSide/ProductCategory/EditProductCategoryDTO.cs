namespace EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;

public record EditProductCategoryDTO 
{
    #region properties

    public ulong CategoryId { get; set; }

    public string Title { get; set; }

    #endregion
}
