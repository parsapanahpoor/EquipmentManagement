using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeQueryRepository
{
    Task<FilterEmployeesDto> FilterEmployees(
        FilterEmployeesDto filter, 
        CancellationToken cancellation);
    Task<Entities.Employee.Employee> GetByIdAsync(
        CancellationToken cancellationToken, 
        params object[] ids);

    Task<bool> IsExistAnyEmployeeByEmployeeMobile(
        string mobile,
        CancellationToken cancellationToken);

    Task<bool> IsExistAnyEmployeeByPersonnalCode(
        string personnalCode,
        CancellationToken cancellationToken);
    }
