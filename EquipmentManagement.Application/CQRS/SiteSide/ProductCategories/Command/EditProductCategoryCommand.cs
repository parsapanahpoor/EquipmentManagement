namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public class EditProductCategoryCommand : IRequest<bool>
{
    #region properties

    public ulong ProductCategoryId { get; set; }

    public string Title { get; set; }

    #endregion
}
