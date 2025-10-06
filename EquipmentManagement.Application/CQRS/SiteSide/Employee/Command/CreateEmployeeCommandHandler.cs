
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;

public record CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
{
    #region Ctor 

    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IEmployeeCommandRepository employeeCommandRepository,
                                    IEmployeeQueryRepository employeeQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (await _employeeQueryRepository.IsExistAnyEmployeeByEmployeeMobile(request.Mobile , cancellationToken))
            return false;

        if (await _employeeQueryRepository.IsExistAnyEmployeeByPersonnalCode(request.PersonnelCode, cancellationToken))
            return false;

        if (request.PlaceOfServiceId == null)
            return false;

        #region Add Employee To The Data Base

        Domain.Entities.Employee.Employee Employee = new Domain.Entities.Employee.Employee()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Mobile  = request.Mobile,
            PlaceOfServiceId = request.PlaceOfServiceId.FirstOrDefault(),
            PersonnelCode = request.PersonnelCode,
        };

        await _employeeCommandRepository.AddAsync(Employee , cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        return true;
    }
}
