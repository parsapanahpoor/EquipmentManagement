using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;
using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.EmployeeShiftMeal;

public class EmployeeShiftMealSelectedQueryRepository : QueryGenericRepository<Domain.Entities.Employee.EmployeeShiftMealSelected>, IEmployeeShiftMealSelectedQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeShiftMealSelectedQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
    public async Task<FilterEmployeeShiftDTO> Filter(FilterEmployeeShiftDTO filter)
    {
        var query = _context.EmployeeShiftSelected
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region filter

   

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }
}
