using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.EditProduct;

public class EditProductQuery : IRequest<EditProductDTO>
{
    public ulong ProductId { get; set; }
}
