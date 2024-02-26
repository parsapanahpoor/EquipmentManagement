using EquipmentManagement.Domain.Entities.Product;

namespace EquipmentManagement.Domain.Entities.Places;

public sealed class Place : BaseEntities<ulong>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string PlaceTitle { get; set; }

    #endregion

    #region Relations

    public List<Domain.Entities.Product.Product> products{ get; set; }

    #endregion
}
