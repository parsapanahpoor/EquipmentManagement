using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeShift;

public class EmployeeShiftSelectedQueryRepository : QueryGenericRepository<Domain.Entities.Employee.EmployeeShiftSelected>, IEmployeeShiftSelectedQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeShiftSelectedQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> VerifyByEmployeeAsync(ulong EmployeeId,CancellationToken ct)
    {
        DateTime utcNow = DateTime.UtcNow;
        DateOnly dateOnly = DateOnly.FromDateTime(utcNow);
        return await _context.EmployeeShiftSelected.AnyAsync(x => x.Date == dateOnly && x.EmployeeId == EmployeeId,ct);
    }

    #endregion

}
