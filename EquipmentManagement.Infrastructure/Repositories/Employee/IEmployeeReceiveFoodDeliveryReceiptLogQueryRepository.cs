using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.Employee;

public class EmployeeReceiveFoodDeliveryReceiptLogQueryRepository : 
    QueryGenericRepository<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog>,
    IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeReceiveFoodDeliveryReceiptLogQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<FilterEmployeeReceiveFoodsLogDto> FilterEmployeeReceiveFoodsLog(
        FilterEmployeeReceiveFoodsLogDto filter, 
        CancellationToken cancellation)
    {
        var query = _context.EmployeeReceiveFoodDeliveryReceiptLogs
                           .Include(p=> p.Employee)
                           .AsNoTracking()
                           .Where(p => !p.IsDelete)
                           .OrderByDescending(p => p.CreateDate)
                           .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.FirstName)))
            query = query.Where(u => u.Employee.FirstName.Contains(filter.FirstName));

        if ((!string.IsNullOrEmpty(filter.LastName)))
            query = query.Where(u => u.Employee.LastName.Contains(filter.LastName));

        if ((!string.IsNullOrEmpty(filter.PersonnelCode)))
            query = query.Where(u => u.Employee.PersonnelCode.Contains(filter.PersonnelCode));

        if ((!string.IsNullOrEmpty(filter.Mobile)))
            query = query.Where(u => u.Employee.Mobile.Contains(filter.Mobile));

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }
}
