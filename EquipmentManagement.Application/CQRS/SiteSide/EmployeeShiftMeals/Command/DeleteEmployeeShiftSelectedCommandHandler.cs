
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;

public record DeleteEmployeeShiftMealSelectedCommandHandler : IRequestHandler<DeleteEmployeeShiftMealSelectedCommand, bool>
{
    #region Ctor

    private readonly IEmployeeShiftMealSelectedCommandRepository _EmployeeShiftMealSelectedCommandRepository;
    private readonly IEmployeeShiftMealSelectedQueryRepository _EmployeeShiftMealSelectedQueryRepository;
    private readonly IUnitOfWork _unitOfWork;   

    public DeleteEmployeeShiftMealSelectedCommandHandler(IEmployeeShiftMealSelectedCommandRepository EmployeeShiftMealSelectedCommandRepository,
                                    IEmployeeShiftMealSelectedQueryRepository EmployeeShiftMealSelectedQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _EmployeeShiftMealSelectedCommandRepository = EmployeeShiftMealSelectedCommandRepository;
        _EmployeeShiftMealSelectedQueryRepository = EmployeeShiftMealSelectedQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(DeleteEmployeeShiftMealSelectedCommand request, CancellationToken cancellationToken)
    {
        //Get EmployeeShiftMealSelected By Id
        var EmployeeShiftMealSelected = await _EmployeeShiftMealSelectedQueryRepository.GetByIdAsync(cancellationToken, request.EmployeeShiftMealSelectedId);
        if (EmployeeShiftMealSelected == null) return false;

        EmployeeShiftMealSelected.IsDelete = true;
        _EmployeeShiftMealSelectedCommandRepository.Update(EmployeeShiftMealSelected);


            await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
