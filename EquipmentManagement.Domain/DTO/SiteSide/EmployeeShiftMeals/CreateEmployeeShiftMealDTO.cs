using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.EmployeeShiftMeals;

public record CreateEmployeeShiftMealDTO
{
    #region properties

    public MealType Meal { get; set; }
    public ulong EmployeeShiftSelectedId { get; set; }

    #endregion
}
