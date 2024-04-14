namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command;

public class CreateProductCommand : IRequest<bool>
{
    #region properties

    public ulong? PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string ProductTitle { get; set; }

    public string RepositoryCode { get; set; }

    public string BarCode { get; set; }

    public int EntityCount { get; set; }

    public string? Description { get; set; }

    #endregion
}
