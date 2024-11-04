using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

namespace EquipmentManagement.Domain.IRepositories.Product;

public interface IProductCommandRepository
{
    Task AddAsync(Domain.Entities.Product.Product product, CancellationToken cancellationToken);

    void Update(Domain.Entities.Product.Product product);

    Task AddProductLog(Domain.Entities.ProductLog.ProductLog model, CancellationToken cancellationToken);

    Task AddProductLog(List<CreateProductLogDto> model, CancellationToken cancellationToken);
}
