namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class CreateProductDTO
{
    #region properties

    public ulong? PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string ProductTitle { get; set; }

    public string RepositoryCode { get; set; }

    public string BarCode { get; set; }

    public string? Description { get; set; }

    #endregion
}
