namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;

public class CreateEmployeeShiftSelectedCommand : IRequest<ulong?>
{
    #region properties
    public DateOnly Date { get; set; }
    public ulong EmployeeId { get; set; }

    #endregion
}
