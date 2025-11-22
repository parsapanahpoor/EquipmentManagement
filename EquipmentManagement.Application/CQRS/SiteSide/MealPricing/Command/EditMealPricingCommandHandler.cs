using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;

public record EditMealPricingCommandHandler : IRequestHandler<EditMealPricingCommand, bool>
{
    #region Ctor

    private readonly IMealPricingCommandRepository _MealPricingCommandRepository;
    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditMealPricingCommandHandler(IMealPricingCommandRepository MealPricingCommandRepository,
                                  IMealPricingQueryRepository MealPricingQueryRepository,
                                  IUnitOfWork unitOfWork)
    {
        _MealPricingCommandRepository = MealPricingCommandRepository;
        _MealPricingQueryRepository = MealPricingQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(EditMealPricingCommand request, CancellationToken cancellationToken)
    {
        //Get MealPricing By Id
        var MealPricing = await _MealPricingQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (MealPricing == null) return false;

        MealPricing.Price = request.Price;
        MealPricing.MealType = request.MealType;

        _MealPricingCommandRepository.Update(MealPricing);


        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
