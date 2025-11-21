
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;

public record CreateEmployeeShiftSelectedCommandHandler : IRequestHandler<CreateEmployeeShiftSelectedCommand, bool>
{
    #region Ctor 

    private readonly IEmployeeShiftSelectedCommandRepository _EmployeeShiftSelectedCommandRepository;
    private readonly IEmployeeShiftSelectedQueryRepository _EmployeeShiftSelectedQueryRepository;
    private readonly IEmployeeQueryRepository _EmployeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeShiftSelectedCommandHandler(IEmployeeShiftSelectedCommandRepository EmployeeShiftSelectedCommandRepository,
                                    IEmployeeShiftSelectedQueryRepository EmployeeShiftSelectedQueryRepository , IEmployeeQueryRepository EmployeeQueryRepository,
                                    IUnitOfWork unitOfWork)
    {
        _EmployeeShiftSelectedCommandRepository = EmployeeShiftSelectedCommandRepository;
        _EmployeeShiftSelectedQueryRepository = EmployeeShiftSelectedQueryRepository;
        _EmployeeQueryRepository = EmployeeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateEmployeeShiftSelectedCommand request, CancellationToken cancellationToken)
    {
        #region Check For Title Comming Being Unique

        if (!await _EmployeeQueryRepository.IsExistAnyEmployeeById(request.EmployeeId , cancellationToken))
        {
            return false;
        }

        #endregion

        #region Add EmployeeShiftSelected To The Data Base

        EquipmentManagement.Domain.Entities.Employee.EmployeeShiftSelected EmployeeShiftSelected = new EquipmentManagement.Domain.Entities.Employee.EmployeeShiftSelected()
        {
           EmployeeId = request.EmployeeId,
           Date=request.Date,
           Employee= await _EmployeeQueryRepository.GetByIdAsync(cancellationToken,request.EmployeeId)
        };

        await _EmployeeShiftSelectedCommandRepository.AddAsync(EmployeeShiftSelected , cancellationToken);
        await _unitOfWork.SaveChangesAsync();



        #endregion

        return true;
    }
}
