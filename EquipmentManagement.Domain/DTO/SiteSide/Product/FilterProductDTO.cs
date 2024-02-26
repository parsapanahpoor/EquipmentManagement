using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FilterProductDTO : BasePaging<Domain.Entities.Product.Product>
{
    #region properties

    public string? ProductTitle { get; set; }

    public string? PlaceTitle { get; set; }

    public string? CategoryTitle { get; set; }

    public ulong?  PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string? BarCode { get; set; }

    #endregion
}
