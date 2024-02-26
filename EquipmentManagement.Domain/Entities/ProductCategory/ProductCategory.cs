namespace EquipmentManagement.Domain.Entities.ProductCategory;

public sealed class ProductCategory : BaseEntities<ulong>
{
    #region properties

    public string BusinessKey { get; set; }

    public string CategoryTitle { get; set; }

    #endregion

    #region Relations

    public List<Domain.Entities.Product.Product> products { get; set; }

    #endregion
}
