using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public class ProductDetailQuery : IRequest<ProductDetailDTO>
{
    public ulong ProductId { get; set; }
}
