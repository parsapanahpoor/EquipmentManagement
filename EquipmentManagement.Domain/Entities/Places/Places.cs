namespace EquipmentManagement.Domain.Entities.Places;

public sealed class Place : BaseEntities<ulong>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string PlaceTitle { get; set; }

    #endregion
}
