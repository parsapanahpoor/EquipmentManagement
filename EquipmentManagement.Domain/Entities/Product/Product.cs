using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.Places;

namespace EquipmentManagement.Domain.Entities.Product;

public sealed class Product : BaseEntities<ulong>
{
    #region properties

    public ulong PlaceId { get; set; }

    public ulong ProductCategoryId { get; set; }

    public string? ProductTitle { get; set; }

    public string? RepostiroyCode { get; set; }

    public string? BarCode { get; set; }

    public int EntityCount { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public bool IsAbolition { get; set; }

    public string? InvoiceImage { get; set; }

    public DateTime? InvoiceDateTime { get; set; }

    public ulong? InvoiceNumber { get; set; }

    public string? InvoiceUniqueNumber { get; set; } 

    #endregion

    #region Relations

    public Place? Place { get; set; }
    public ProductCategory.ProductCategory? ProductCategory { get; set; }
    public ICollection<RepairRequest>? RepairRequests { get; set; }
    public ICollection<ProductLog.ProductLog>? ProductLogs { get; set; }

    #endregion
}
