namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public class DeleteProductCategoryCommand : IRequest<bool>
{
    #region properties

    public ulong ProductCategoryId { get; set; }

    #endregion
}
