using EquipmentManagement.Domain.IRepositories.EmployeeShifts;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeShift;

public class EmployeeShiftSelectedQueryRepository : QueryGenericRepository<Domain.Entities.Employee.EmployeeShiftSelected>, IEmployeeShiftSelectedQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeShiftSelectedQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

}
