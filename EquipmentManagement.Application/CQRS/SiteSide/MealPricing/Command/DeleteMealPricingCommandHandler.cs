
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public record DeleteMealPricingCommandHandler : IRequestHandler<DeleteMealPricingCommand, bool>
{
    #region Ctor

    private readonly IMealPricingCommandRepository _MealPricingCommandRepository;
    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;
    private readonly IUnitOfWork _unitOfWork;   

    public DeleteMealPricingCommandHandler(IMealPricingCommandRepository MealPricingCommandRepository,
                                    IMealPricingQueryRepository MealPricingQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _MealPricingCommandRepository = MealPricingCommandRepository;
        _MealPricingQueryRepository = MealPricingQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(DeleteMealPricingCommand request, CancellationToken cancellationToken)
    {
        //Get MealPricing By Id
        var MealPricing = await _MealPricingQueryRepository.GetByIdAsync(cancellationToken, request.MealPricingId);
        if (MealPricing == null) return false;

        MealPricing.IsDelete = true;
        _MealPricingCommandRepository.Update(MealPricing);

   

            await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
