
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;

public record DeleteEmployeeShiftSelectedCommandHandler : IRequestHandler<DeleteEmployeeShiftSelectedCommand, bool>
{
    #region Ctor

    private readonly IEmployeeShiftSelectedCommandRepository _EmployeeShiftSelectedCommandRepository;
    private readonly IEmployeeShiftSelectedQueryRepository _EmployeeShiftSelectedQueryRepository;
    private readonly IUnitOfWork _unitOfWork;   

    public DeleteEmployeeShiftSelectedCommandHandler(IEmployeeShiftSelectedCommandRepository EmployeeShiftSelectedCommandRepository,
                                    IEmployeeShiftSelectedQueryRepository EmployeeShiftSelectedQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _EmployeeShiftSelectedCommandRepository = EmployeeShiftSelectedCommandRepository;
        _EmployeeShiftSelectedQueryRepository = EmployeeShiftSelectedQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(DeleteEmployeeShiftSelectedCommand request, CancellationToken cancellationToken)
    {
        //Get EmployeeShiftSelected By Id
        var EmployeeShiftSelected = await _EmployeeShiftSelectedQueryRepository.GetByIdAsync(cancellationToken, request.EmployeeShiftSelectedId);
        if (EmployeeShiftSelected == null) return false;

        EmployeeShiftSelected.IsDelete = true;
        _EmployeeShiftSelectedCommandRepository.Update(EmployeeShiftSelected);


            await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
