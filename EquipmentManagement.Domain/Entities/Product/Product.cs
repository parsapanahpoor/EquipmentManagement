using EquipmentManagement.Domain.Entities.Places;
using System.Runtime.CompilerServices;

namespace EquipmentManagement.Domain.Entities.Product;

public sealed class Product : BaseEntities<ulong>
{
    #region properties

    public ulong PlaceId { get; set; }

    public ulong ProductCategoryId { get; set; }

    public string ProductTitle { get; set; }

    public string BarCode { get; set; }

    public int EntityCount { get; set; }

    public string? Description { get; set; }

    #endregion

    #region Relations

    public Place Place { get; set; }

    public Domain.Entities.ProductCategory.ProductCategory ProductCategory { get; set; }

    #endregion
}
