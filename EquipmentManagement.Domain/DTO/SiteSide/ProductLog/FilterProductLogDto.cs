using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

public class FilterProductLogDTO : BasePaging<Entities.ProductLog.ProductLog>
{
    public string? ProductTitle { get; set; }

    public ulong? ProductId { get; set; }

    public string? Brand { get; set; }

    public string? PlaceTitle { get; set; }

    public string? RepostiroyCode { get; set; }

    public ulong? PlaceId { get; set; }

    public string? BarCode { get; set; }
}
