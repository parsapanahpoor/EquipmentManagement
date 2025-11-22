using EquipmentManagement.Domain.IRepositories.EmployeeShifts;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeShift;

public class EmployeeShiftSelectedCommandRepository : CommandGenericRepository<Domain.Entities.Employee.EmployeeShiftSelected>, IEmployeeShiftSelectedCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeShiftSelectedCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side


    #endregion
}
