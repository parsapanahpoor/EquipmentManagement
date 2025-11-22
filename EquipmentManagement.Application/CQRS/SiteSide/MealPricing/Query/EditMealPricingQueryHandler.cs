using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;

public record EditMealPricingQueryHandler : IRequestHandler<EditMealPricingQuery, EditMealPricingDTO>
{
    #region Ctor

    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;

    public EditMealPricingQueryHandler(IMealPricingQueryRepository MealPricingQueryRepository)
    {
        _MealPricingQueryRepository = MealPricingQueryRepository;
    }

    #endregion

    public async Task<EditMealPricingDTO?> Handle(EditMealPricingQuery request, CancellationToken cancellationToken)
    {
        //get MealPricing By MealPricing 
        var MealPricing = await _MealPricingQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (MealPricing == null) return null;

        return new EditMealPricingDTO()
        {
            MealType = MealPricing.MealType,
            Price = MealPricing.Price,
            MealPricingId = MealPricing.Id,
        };
    }
}
