namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class ProductDetailDTO
{
    #region proeprties

    public ulong ProductId { get; set; }

    public string? PlaceName { get; set; }

    public string? CategoryName { get; set; }

    public string? ProductTitle { get; set; }

    public string? BarCode { get; set; }

    public string? Description { get; set; }

    #endregion
}
