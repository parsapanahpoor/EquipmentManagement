using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

namespace EquipmentManagement.Domain.IRepositories.ProductLog;

public interface IProductLogQueryRepository
{
    Task<FilterProductLogDTO> FilterProductLogs(FilterProductLogDTO filter , CancellationToken cancellationToken);
}
