using EquipmentManagement.Domain.Entities.MealPricing;

namespace EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;

public record CreateEmployeeShiftDTO
{
    #region properties

    public string Date { get; set; }
    public ulong EmployeeId { get; set; }

    #endregion
}
