using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

namespace EquipmentManagement.Domain.IRepositories.ProductLog;

public interface IProductLogQueryRepository
{
    Task<FilterProductLogDTO> FilterProductLogs(FilterProductLogDTO filter , CancellationToken cancellationToken);
    Task<List<Domain.Entities.ProductLog.ProductLog>> GetLastestProductLogs(CancellationToken cancellationToken);
}
