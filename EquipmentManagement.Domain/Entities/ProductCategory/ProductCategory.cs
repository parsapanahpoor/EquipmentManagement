namespace EquipmentManagement.Domain.Entities.ProductCategory;

public sealed class ProductCategory : BaseEntities<ulong>
{
    #region properties

    public string BusinessKey { get; set; }

    public string CategoryTitle { get; set; }

    #endregion
}
