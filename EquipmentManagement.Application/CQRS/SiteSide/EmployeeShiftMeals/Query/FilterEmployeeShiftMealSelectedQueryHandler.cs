using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShiftMeals;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Query;

public record FilterEmployeeShiftMealSelectedQueryHandler : IRequestHandler<FilterEmployeeShiftMealSelectedQuery, FilterEmployeeShiftMealDTO>
{
    #region Ctor

    private readonly IEmployeeShiftMealSelectedQueryRepository _EmployeeShiftMealSelectedQueryRepository;

    public FilterEmployeeShiftMealSelectedQueryHandler(IEmployeeShiftMealSelectedQueryRepository EmployeeShiftMealSelectedQueryRepository)
    {
        _EmployeeShiftMealSelectedQueryRepository = EmployeeShiftMealSelectedQueryRepository;
    }

    #endregion



    public async Task<FilterEmployeeShiftMealDTO> Handle(FilterEmployeeShiftMealSelectedQuery request, CancellationToken cancellationToken)
    {
        var result = await _EmployeeShiftMealSelectedQueryRepository.FilterAsync(
            new FilterEmployeeShiftMealDTO() { EmployeeShiftSelectedId=request.EmployeeShiftSelectedId}, // DTO ورودی
            query => query.Include(_=>_.MealPricing)
                .Where(p => !p.IsDelete&&p.EmployeeShiftSelectedId==request.EmployeeShiftSelectedId)                // شرط حذف نشده
                .OrderByDescending(p => p.CreateDate) // مرتب‌سازی
            
        );

        return result;
    }

}
