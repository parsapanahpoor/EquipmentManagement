using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.EmployeeShifts;

public interface IEmployeeShiftSelectedCommandRepository:ICommandGenericRepository<EmployeeShiftSelected>
{
    Task AddAsync(EmployeeShiftSelected model, CancellationToken cancellationToken);
}
