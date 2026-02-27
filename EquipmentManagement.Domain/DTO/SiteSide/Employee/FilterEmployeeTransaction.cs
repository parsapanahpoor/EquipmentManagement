using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.Employee;

namespace EquipmentManagement.Domain.DTO.SiteSide.Employee;

public class FilterEmployeeTransaction : BasePaging<EmployeeTransaction>
{
    #region properties

    public ulong? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    #endregion
}

public class FilterEmployeeTransactionResponse : BaseEntities<ulong>
{

    public ulong EmployeeId { get; set; }

    public string? Description { get; set; }
    public long Amount { get; set; }
    public bool Paid { get; set; } = false;

    

}