namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public record EditProductDTO
{
    #region proeprties

    public ulong ProductId { get; set; }

    public ulong PlaceId { get; set; }

    public string PlaceName { get; set; }

    public string? BrandName { get; set; }

    public ulong  CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string ProductTitle { get; set; }

    public string RepositoryCode { get; set; }

    public string BarCode { get; set; }

    public string? Description { get; set; }

    #endregion
}
