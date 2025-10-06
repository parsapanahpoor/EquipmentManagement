using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;

public record EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, EditEmployeeResult>
{
    #region Ctor

    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditEmployeeCommandHandler(IEmployeeCommandRepository employeeCommandRepository,
                                  IEmployeeQueryRepository employeeQueryRepository,
                                  IUnitOfWork unitOfWork)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<EditEmployeeResult> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request.PlaceOfServiceId == null)
            return EditEmployeeResult.Faild;

        var employee = await _employeeQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (employee == null)
            return EditEmployeeResult.EmployeeNotFound;

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Mobile = request.Mobile;
        employee.PersonnelCode = request.PersonnelCode;
        employee.PlaceOfServiceId = request.PlaceOfServiceId.FirstOrDefault();

        _employeeCommandRepository.Update(employee);
        await _unitOfWork.SaveChangesAsync();

        return EditEmployeeResult.Success;
    }
}
