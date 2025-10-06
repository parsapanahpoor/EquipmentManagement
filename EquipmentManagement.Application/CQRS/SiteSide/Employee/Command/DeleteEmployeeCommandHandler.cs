using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;

public record DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    #region Ctor

    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;   

    public DeleteEmployeeCommandHandler(IEmployeeCommandRepository employeeCommandRepository,
                                    IEmployeeQueryRepository employeeQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeQueryRepository.GetByIdAsync(cancellationToken, request.EmployeeId);
        if (employee == null) 
            return false;

        employee.IsDelete = true;

        _employeeCommandRepository.Update(employee);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
