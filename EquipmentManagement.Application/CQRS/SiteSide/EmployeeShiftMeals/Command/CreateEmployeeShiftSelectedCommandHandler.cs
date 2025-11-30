
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;

public record CreateEmployeeShiftMealSelectedCommandHandler : IRequestHandler<CreateEmployeeShiftMealSelectedCommand, bool>
{
    #region Ctor 

    private readonly IEmployeeShiftMealSelectedCommandRepository _EmployeeShiftMealSelectedCommandRepository;
    private readonly IEmployeeShiftMealSelectedQueryRepository _EmployeeShiftMealSelectedQueryRepository;
    private readonly IEmployeeShiftSelectedQueryRepository _EmployeeShiftQueryRepository;
    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeShiftMealSelectedCommandHandler(IEmployeeShiftMealSelectedCommandRepository EmployeeShiftMealSelectedCommandRepository,
                                    IEmployeeShiftMealSelectedQueryRepository EmployeeShiftMealSelectedQueryRepository, IEmployeeShiftSelectedQueryRepository EmployeeShiftQueryRepository,
                                    IUnitOfWork unitOfWork, IMealPricingQueryRepository mealPricingQueryRepository)
    {
        _EmployeeShiftMealSelectedCommandRepository = EmployeeShiftMealSelectedCommandRepository;
        _EmployeeShiftMealSelectedQueryRepository = EmployeeShiftMealSelectedQueryRepository;
        _EmployeeShiftQueryRepository = EmployeeShiftQueryRepository;
        _unitOfWork = unitOfWork;
        _MealPricingQueryRepository = mealPricingQueryRepository;
    }

    #endregion

    public async Task<bool> Handle(CreateEmployeeShiftMealSelectedCommand request, CancellationToken cancellationToken)
    {
        #region Check For Title Comming Being Unique

        if (!await _EmployeeShiftQueryRepository.IsExistAnyByIdAsync(request.EmployeeShiftSelectedId, cancellationToken))
        {
            return false;
        }

        #endregion

        #region Add EmployeeShiftMealSelected To The Data Base

        EquipmentManagement.Domain.Entities.Employee.EmployeeShiftMealSelected EmployeeShiftMealSelected = new EquipmentManagement.Domain.Entities.Employee.EmployeeShiftMealSelected()
        {
            EmployeeShiftSelectedId = request.EmployeeShiftSelectedId,
            MealPricingId = request.MealPricingId,
            MealPricing = await _MealPricingQueryRepository.GetByIdAsync(cancellationToken, request.MealPricingId),
            EmployeeShiftSelected = await _EmployeeShiftQueryRepository.GetByIdAsync(cancellationToken, request.EmployeeShiftSelectedId)
        };

        await _EmployeeShiftMealSelectedCommandRepository.AddAsync(EmployeeShiftMealSelected, cancellationToken);
        await _unitOfWork.SaveChangesAsync();



        await _unitOfWork.SaveChangesAsync();

        #endregion

        return true;
    }
}
