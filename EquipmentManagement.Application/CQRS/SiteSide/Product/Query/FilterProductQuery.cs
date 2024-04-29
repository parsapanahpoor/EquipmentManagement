using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public class FilterProductQuery : IRequest<FilterProductDTO>
{
    #region properties

    public FilterProductDTO filter { get; set; }

    #endregion
}
