namespace EquipmentManagement.Domain.Entities.ProductLog;

public sealed class ProductLog : BaseEntities<ulong>
{
    public ulong ProductId { get; set; }
    public ulong? PlaceId { get; set; }
    public string? Description { get; set; }

    public Users.User? User { get; set; }
    public ulong UserId { get; set; }

    public Product.Product? Product { get; set; }
    public Places.Place? Place { get; set; }
}
