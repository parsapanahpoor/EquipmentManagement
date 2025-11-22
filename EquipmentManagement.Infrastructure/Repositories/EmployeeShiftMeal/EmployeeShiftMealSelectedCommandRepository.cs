using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeShiftMeal;

public class EmployeeShiftMealSelectedCommandRepository : CommandGenericRepository<Domain.Entities.Employee.EmployeeShiftMealSelected>, IEmployeeShiftMealSelectedCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeShiftMealSelectedCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side


    #endregion
}
