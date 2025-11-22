using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;
namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Query;

public class FilterEmployeeShiftSelectedQuery : IRequest<FilterEmployeeShiftDTO>
{
    #region properties

    public DateOnly Date { get; set; }
    public ulong EmployeeId { get; set; }


    #endregion
}
