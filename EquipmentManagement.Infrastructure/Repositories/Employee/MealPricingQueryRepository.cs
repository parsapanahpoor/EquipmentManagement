using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeTransaction;

public class EmployeeTransactionQueryRepository : QueryGenericRepository<EquipmentManagement.Domain.Entities.Employee.EmployeeTransaction>, IEmployeeTransactionQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeTransactionQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }


    #endregion

    #region Admin Side

    public async Task<FilterEmployeeTransaction> FilterEmployeeTransaction(FilterEmployeeTransaction filter)
    {
        var query = _context.EmployeeTransaction
                                        .AsNoTracking()
                                        .Include(x=>x.Employee)
                                        .Where(p => !p.IsDelete)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region filter

        if (filter.EmployeeId != null)
        {
            query = query.Where(u => u.EmployeeId == (filter.EmployeeId));
        }

        if (filter.EmployeeName != null)
        {
            query = query.Where(u => u.Employee.FirstName!.Contains(filter.EmployeeName)
            || u.Employee.LastName!.Contains(filter.EmployeeName));
        }
        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }




    #endregion
}

