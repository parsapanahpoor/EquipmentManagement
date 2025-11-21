using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;

public class CreateEmployeeShiftMealSelectedCommand : IRequest<bool>
{
    #region properties
    public MealType Meal { get; set; }
    public ulong EmployeeShiftSelectedId { get; set; }

    #endregion
}
