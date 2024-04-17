using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.EditProduct;

public record EditProductCommand : IRequest<bool>
{
    public EditProductDTO EditProductDTO { get; set; }
}
