using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.Entities.Employee;
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
                           .Include(p => p.Employee)
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

    public async Task<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog?> GetFoodReceiptLogByLogId(
        ulong Id,
        CancellationToken cancellationToken)
        => await _context.EmployeeReceiveFoodDeliveryReceiptLogs
                         .Include(p => p.Employee)
                         .AsNoTracking()
                         .Where(p => !p.IsDelete)
                         .FirstOrDefaultAsync(p => p.Id == Id);

    public async Task<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog?> GetFoodReceiptLogByEmployeeMobile(
        string mobile,
        CancellationToken cancellationToken)
        => await _context.EmployeeReceiveFoodDeliveryReceiptLogs
                         .Include(p => p.Employee)
                         .AsNoTracking()
                         .Where(p => !p.IsDelete)
                         .OrderByDescending(p => p.CreateDate)
                         .FirstOrDefaultAsync(p => p.Employee.Mobile == mobile);


    public async Task<List<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog>>
        GetFoodReceiptLogByEmployeeMobile(List<ulong> ids, CancellationToken cancellationToken)
    {

  
        return await _context.Employees
       
            .AsNoTracking()
            .Where(p => !p.IsDelete && ids.Contains(p.Id)).Select(_=>new EmployeeReceiveFoodDeliveryReceiptLog
            {
                Employee=_,
                EmployeeId=_.Id
            })
            .OrderByDescending(p => p.CreateDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(ulong MealPricingId, ulong EmployeeId,CancellationToken ct)
    {
        var condition = await _context.EmployeeReceiveFoodDeliveryReceiptLogs.Where(x => x.EmployeeId == EmployeeId && x.MealPricingId == MealPricingId && x.CreateDate.Date == DateTime.UtcNow.Date).CountAsync();
      return condition>1;
    }

}
