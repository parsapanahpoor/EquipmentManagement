namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public class CreateProductCategoryCommand : IRequest<bool>
{
    #region properties

    public string Title { get; set; }

    #endregion
}
