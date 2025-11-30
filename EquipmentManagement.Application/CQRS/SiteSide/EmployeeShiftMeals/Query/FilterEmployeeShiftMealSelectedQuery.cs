using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShiftMeals;
using EquipmentManagement.Domain.Entities.MealPricing;
namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Query;

public class FilterEmployeeShiftMealSelectedQuery : IRequest<FilterEmployeeShiftMealDTO>
{
    #region properties

    public ulong MealPricingId { get; set; }
    public ulong EmployeeShiftSelectedId { get; set; }


    #endregion
}
