using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FilterAbolitionProducts;

public class FilterAbolitionProductsQuery : IRequest<FilterProductDTO>
{
    #region properties

    public FilterProductDTO filter { get; set; }

    #endregion
}
