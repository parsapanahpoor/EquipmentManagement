using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;

public class FilterProductCatgeoriesQuery : IRequest<FilterProductCategories>
{
    #region properties

    public string? Title { get; set; }

    #endregion
}
