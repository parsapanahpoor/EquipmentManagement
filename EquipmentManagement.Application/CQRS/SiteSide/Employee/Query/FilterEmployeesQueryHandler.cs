using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;

public record FilterEmployeesQueryHandler : IRequestHandler<FilterEmployeesQuery, FilterEmployeesDto>
{
    #region Ctor

    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public FilterEmployeesQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
    }

    #endregion

    public async Task<FilterEmployeesDto> Handle(FilterEmployeesQuery request, CancellationToken cancellationToken)
        => await _employeeQueryRepository.FilterEmployees(new FilterEmployeesDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mobile = request.Mobile,
                PersonnelCode = request.PersonnelCode,
            },
            cancellationToken);
}
public record FilterSelectedEmployeesQueryHandler : IRequestHandler<FilterSelectedEmployeesQuery, FilterSelectedEmployeesDto>
{
    #region Ctor

    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public FilterSelectedEmployeesQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
    }

    #endregion

    public async Task<FilterSelectedEmployeesDto> Handle(FilterSelectedEmployeesQuery request, CancellationToken cancellationToken)
        => await _employeeQueryRepository.FilterEmployees(new FilterSelectedEmployeesDto()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Mobile = request.Mobile,
            PersonnelCode = request.PersonnelCode,
            EmployeeIds=request.EmployeeIds,
        },
            cancellationToken);
}
