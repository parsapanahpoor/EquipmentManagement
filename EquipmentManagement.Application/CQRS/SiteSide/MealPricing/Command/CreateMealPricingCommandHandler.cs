
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public record CreateMealPricingCommandHandler : IRequestHandler<CreateMealPricingCommand, bool>
{
    #region Ctor 

    private readonly IMealPricingCommandRepository _MealPricingCommandRepository;
    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMealPricingCommandHandler(IMealPricingCommandRepository MealPricingCommandRepository,
                                    IMealPricingQueryRepository MealPricingQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _MealPricingCommandRepository = MealPricingCommandRepository;
        _MealPricingQueryRepository = MealPricingQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateMealPricingCommand request, CancellationToken cancellationToken)
    {
       

        #region Add MealPricing To The Data Base

      Domain.Entities.MealPricing.  MealPricing MealPricing =new Domain.Entities.MealPricing.MealPricing()
        {
            MealType = request.MealType,
            Price = request.Price,
        };

        await _MealPricingCommandRepository.AddAsync(MealPricing , cancellationToken);
        await _unitOfWork.SaveChangesAsync();


        await _unitOfWork.SaveChangesAsync();

        #endregion

        return true;
    }
}
