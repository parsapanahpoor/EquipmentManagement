namespace EquipmentManagement.Domain.IRepositories.Product;

public interface IProductCommandRepository
{
    Task AddAsync(Domain.Entities.Product.Product product, CancellationToken cancellationToken);

    void Update(Domain.Entities.Product.Product product);
}
