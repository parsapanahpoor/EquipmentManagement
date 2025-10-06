using EquipmentManagement.Application.CQRS.SiteSide.Account.Query;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.Employee;

public class EmployeeQueryRepository : 
    QueryGenericRepository<Domain.Entities.Employee.Employee>, 
    IEmployeeQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<FilterEmployeesDto> FilterEmployees(FilterEmployeesDto filter, CancellationToken cancellation)
    {
        var query = _context.Employees
                           .AsNoTracking()
                           .Where(p => !p.IsDelete)
                           .OrderByDescending(p => p.CreateDate)
                           .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.FirstName)))
            query = query.Where(u => u.FirstName.Contains(filter.FirstName));

        if ((!string.IsNullOrEmpty(filter.LastName)))
            query = query.Where(u => u.LastName.Contains(filter.LastName));

        if ((!string.IsNullOrEmpty(filter.PersonnelCode)))
            query = query.Where(u => u.PersonnelCode.Contains(filter.PersonnelCode));

        if ((!string.IsNullOrEmpty(filter.Mobile)))
            query = query.Where(u => u.Mobile.Contains(filter.Mobile));

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<bool> IsExistAnyEmployeeByEmployeeMobile(
        string mobile, 
        CancellationToken cancellationToken)
    {
        return await _context.Employees
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Mobile == mobile);
    }

    public async Task<bool> IsExistAnyEmployeeByPersonnalCode(
        string personnalCode,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.PersonnelCode == personnalCode);
    }
}
