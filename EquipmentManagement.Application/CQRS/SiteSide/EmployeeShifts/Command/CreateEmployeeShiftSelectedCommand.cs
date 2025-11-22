namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;

public class CreateEmployeeShiftSelectedCommand : IRequest<bool>
{
    #region properties
    public DateOnly Date { get; set; }
    public ulong EmployeeId { get; set; }

    #endregion
}
