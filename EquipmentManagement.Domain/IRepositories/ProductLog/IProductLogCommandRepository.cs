using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

namespace EquipmentManagement.Domain.IRepositories.ProductLog;

public interface IProductLogCommandRepository
{
    Task AddAsync(Domain.Entities.ProductLog.ProductLog productLog, CancellationToken cancellationToken);

    void Update(Entities.ProductLog.ProductLog productLog);
}
