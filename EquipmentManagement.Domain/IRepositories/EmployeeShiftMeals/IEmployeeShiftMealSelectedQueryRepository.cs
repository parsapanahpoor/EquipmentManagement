using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;

public interface IEmployeeShiftMealSelectedQueryRepository: IQueryGenericRepository<EmployeeShiftMealSelected>
{
    Task<bool> ExistsAsync(ulong EmployeeId, ulong MealPricingId, CancellationToken ct);
}
