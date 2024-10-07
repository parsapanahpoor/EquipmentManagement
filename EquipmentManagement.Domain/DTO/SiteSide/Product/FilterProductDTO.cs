using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FilterProductDTO : BasePaging<Domain.Entities.Product.Product>
{
    #region properties

    public string? ProductTitle { get; set; }

    public string? PlaceTitle { get; set; }

    public string? CategoryTitle { get; set; }

    public string RepostiroyCode { get; set; }

    public ulong?  PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string? BarCode { get; set; }

    #endregion
}

public record FilterProductForExcelFilesDTO
{
    #region properties

    public ulong Id { get; set; }
    public string? ProductTitle { get; init; }

    public string? PlaceTitle { get; init; }

    public string? CategoryTitle { get; init; }

    public string? RepostiroyCode { get; set; }

    public string? Description { get; set; }

    public ulong? PlaceId { get; init; }

    public ulong? CategoryId { get; init; }

    public string? BarCode { get; init; }

    public DateTime CreateDate { get; set; }

    #endregion
}
