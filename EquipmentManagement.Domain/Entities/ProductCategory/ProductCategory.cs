namespace EquipmentManagement.Domain.Entities.ProductCategory;

public sealed class ProductCategory : BaseEntities<ulong>
{
    #region properties

    public ulong BusinessKey { get; set; }

    public string CategoryTitle { get; set; }

    #endregion
}
