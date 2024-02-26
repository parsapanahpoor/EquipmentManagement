using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public class FilterProductQuery : IRequest<FilterProductDTO>
{
    #region properties

    public string? ProductTitle { get; set; }

    public string? PlaceTitle { get; set; }

    public string? CategoryTitle { get; set; }

    public ulong? PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    #endregion
}
