using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;

internal class MealPricingSelectedListQueryHandler : IRequestHandler<MealPricingSelectedListQuery, FilterMealPricing>
{
    #region Ctor

    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;

    public MealPricingSelectedListQueryHandler(IMealPricingQueryRepository MealPricingQueryRepository)
    {
        _MealPricingQueryRepository = MealPricingQueryRepository;
    }

    #endregion

    public async Task<FilterMealPricing> Handle(MealPricingSelectedListQuery request, CancellationToken cancellationToken)
    {
     
        return await _MealPricingQueryRepository.FilterMealPricing(new FilterMealPricing {MealType=request.MealType,Price=request.Price  });
    }
}
internal class DropDownMealPricingSelectedListQueryHandler : IRequestHandler<DropdownMealPricingSelectedListQuery, List<DropdownItem>>
{
    #region Ctor

    private readonly IMealPricingQueryRepository _MealPricingQueryRepository;

    public DropDownMealPricingSelectedListQueryHandler(IMealPricingQueryRepository MealPricingQueryRepository)
    {
        _MealPricingQueryRepository = MealPricingQueryRepository;
    }

    #endregion

    public async Task<List<DropdownItem>> Handle(DropdownMealPricingSelectedListQuery request, CancellationToken cancellationToken)
    {

        return await _MealPricingQueryRepository.GetDropDownMealPricing(new FilterMealPricing { MealType = request.MealType, Price = request.Price },cancellationToken);
    }
}
