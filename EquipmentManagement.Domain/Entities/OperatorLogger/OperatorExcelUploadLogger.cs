namespace EquipmentManagement.Domain.Entities.OperatorLogger;

public sealed class OperatorExcelUploadLogger : BaseEntities<ulong>
{
    #region properties

    public ulong UserId { get; set; }

    public string ExcelFile { get; set; }

    #endregion
}
