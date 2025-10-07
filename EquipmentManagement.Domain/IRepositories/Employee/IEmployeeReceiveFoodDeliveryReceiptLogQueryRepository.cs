using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository
{
    Task<FilterEmployeeReceiveFoodsLogDto> FilterEmployeeReceiveFoodsLog(
        FilterEmployeeReceiveFoodsLogDto filter,
        CancellationToken cancellation);
}
