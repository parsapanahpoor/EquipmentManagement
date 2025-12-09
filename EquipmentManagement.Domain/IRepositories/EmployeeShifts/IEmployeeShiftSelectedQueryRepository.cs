using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.EmployeeShifts;

public interface IEmployeeShiftSelectedQueryRepository: IQueryGenericRepository<EmployeeShiftSelected>
{
    Task<bool> VerifyByEmployeeAsync( ulong EmployeeId,CancellationToken ct);
    //Task<ulong> GetEmployeeShiftIdByEmployeeIdAsync(ulong EmployeeId, CancellationToken ct);
}
