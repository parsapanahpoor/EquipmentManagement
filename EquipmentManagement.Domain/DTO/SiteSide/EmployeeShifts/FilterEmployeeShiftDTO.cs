using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;

public class FilterEmployeeShiftDTO : BasePaging<EmployeeShiftSelected>
{
    #region properties

    public DateOnly Date { get; set; }
    public ulong EmployeeId { get; set; }


    #endregion
}
