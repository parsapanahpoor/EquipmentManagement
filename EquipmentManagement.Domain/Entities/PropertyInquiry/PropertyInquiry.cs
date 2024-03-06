namespace EquipmentManagement.Domain.Entities.PropertyInquiry;

public sealed class PropertyInquiry : BaseEntities<ulong>
{
    #region properties

    public ulong UserId { get; set; }

    public string ExcelFile { get; set; }

    #endregion
}
