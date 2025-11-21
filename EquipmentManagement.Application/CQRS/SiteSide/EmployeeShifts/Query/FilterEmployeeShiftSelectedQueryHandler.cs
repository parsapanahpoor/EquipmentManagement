using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using System.Web.Mvc;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Query;

public record FilterEmployeeShiftSelectedQueryHandler : IRequestHandler<FilterEmployeeShiftSelectedQuery, FilterEmployeeShiftDTO>
{
    #region Ctor

    private readonly IEmployeeShiftSelectedQueryRepository _EmployeeShiftSelectedQueryRepository;

    public FilterEmployeeShiftSelectedQueryHandler(IEmployeeShiftSelectedQueryRepository EmployeeShiftSelectedQueryRepository)
    {
        _EmployeeShiftSelectedQueryRepository = EmployeeShiftSelectedQueryRepository;
    }

    #endregion



    public async Task<FilterEmployeeShiftDTO> Handle(FilterEmployeeShiftSelectedQuery request, CancellationToken cancellationToken)
    {
        var result = await _EmployeeShiftSelectedQueryRepository.FilterAsync(
            new FilterEmployeeShiftDTO() { EmployeeId =request.EmployeeId}, // DTO ورودی
            query => query
                .Where(p => !p.IsDelete&&request.EmployeeId==p.EmployeeId)                // شرط حذف نشده
                .OrderByDescending(p => p.CreateDate) // مرتب‌سازی
            
        );

        return result;
    }

}
