using EquipmentManagement.Domain.Entities.Product;
using EquipmentManagement.Domain.Entities.ProductLog;

namespace EquipmentManagement.Domain.Entities.Places;

public sealed class Place : BaseEntities<ulong>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string PlaceTitle { get; set; }

    #endregion

    #region Relations

    public ICollection<Product.Product>? products{ get; set; }
    public ICollection<ProductLog.ProductLog>? ProductLogs{ get; set; }

    #endregion
}
