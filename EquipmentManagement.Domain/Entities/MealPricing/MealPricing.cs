using EquipmentManagement.Domain.Entities.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagement.Domain.Entities.MealPricing;

public class MealPricing : BaseEntities<ulong>
{
    public string MealType { get; set; }=string.Empty;
    public decimal Price { get; set; }
    public ICollection<EmployeeReceiveFoodDeliveryReceiptLog> EmployeeReceiveFoodDeliveryReceiptLog { get; set; }
}
//public enum MealType
//{
//    [Display(Name = "صبحانه")] Breakfast,
//    [Display(Name = "ناهار")] Lunch,
//    [Display(Name = "شام")] Dinner,
//}