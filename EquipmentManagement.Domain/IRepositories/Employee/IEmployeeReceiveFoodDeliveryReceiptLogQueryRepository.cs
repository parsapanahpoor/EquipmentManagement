using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository
{
    Task<FilterEmployeeReceiveFoodsLogDto> FilterEmployeeReceiveFoodsLog(
        FilterEmployeeReceiveFoodsLogDto filter,
        CancellationToken cancellation);

    Task<Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog?> GetFoodReceiptLogByLogId(
        ulong Id,
        CancellationToken cancellationToken);

    Task<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog?> GetFoodReceiptLogByEmployeeMobile(
        string mobile,
        CancellationToken cancellationToken);
    Task<List<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog?>> GetFoodReceiptLogByEmployeeMobile(
      List<ulong> Ids,
      CancellationToken cancellationToken);
}
