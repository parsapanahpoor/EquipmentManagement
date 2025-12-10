using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeQueryRepository
{
    Task<FilterEmployeesDto> FilterEmployees(
        FilterEmployeesDto filter, 
        CancellationToken cancellation);
    Task<Domain.Entities.Employee.Employee?> GetEmployeeByRFID(
    string rfid,
    CancellationToken cancellationToken);
    Task<FilterSelectedEmployeesDto> FilterEmployees(
        FilterSelectedEmployeesDto filter,
        CancellationToken cancellation);
    Task<Entities.Employee.Employee> GetByIdAsync(
        CancellationToken cancellationToken, 
        params object[] ids);

    Task<bool> IsExistAnyEmployeeByEmployeeMobile(
        string mobile,
        CancellationToken cancellationToken);
    Task<bool> IsExistAnyEmployeeById(
       ulong mobile,
       CancellationToken cancellationToken);

    Task<bool> IsExistAnyEmployeeByPersonnalCode(
        string personnalCode,
        CancellationToken cancellationToken);

    Task<Entities.Employee.Employee?> GetEmployeeByMobile(
        string mobile,
        CancellationToken cancellationToken);

    Task<List<Domain.Entities.Employee.Employee?>> GetEmployeeByIds(
       List<ulong> Ids,
       CancellationToken cancellationToken);
}
