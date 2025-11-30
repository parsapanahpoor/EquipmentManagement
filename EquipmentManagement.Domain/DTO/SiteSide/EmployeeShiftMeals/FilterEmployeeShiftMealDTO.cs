using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.Entities.MealPricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagement.Domain.DTO.SiteSide.EmployeeShiftMeals;

public class FilterEmployeeShiftMealDTO : BasePaging<EmployeeShiftMealSelected>
{
    #region properties

    public ulong MealPricingId { get; set; }
    public ulong MealPricingName { get; set; }
    public ulong EmployeeShiftSelectedId { get; set; }

    #endregion
}
