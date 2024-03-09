namespace EquipmentManagement.Domain.Entities.PropertyInquiry;

public sealed class PropertyInquiryDetail : BaseEntities<ulong>
{
    #region properties

    public ulong PropertyInquiryId { get; set; }

    public string RF_Id { get; set; }

    #endregion
}
