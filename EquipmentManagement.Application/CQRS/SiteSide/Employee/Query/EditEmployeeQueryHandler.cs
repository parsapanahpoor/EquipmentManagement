using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;

public record EditEmployeeQueryHandler : IRequestHandler<EditEmployeeQuery, EditEmployeeDto>
{
    #region Ctor

    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public EditEmployeeQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
    }

    #endregion

    public async Task<EditEmployeeDto?> Handle(EditEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (employee == null)
            return null;

        return new EditEmployeeDto()
        {
            Id = employee.Id,
            PlaceOfServiceId = new List<ulong>() { employee.PlaceOfServiceId },
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Mobile = employee.Mobile,
            PersonnelCode = employee.PersonnelCode
        };
    }
}
