namespace EquipmentManagement.Domain.Common;

public abstract class BaseEntities<TKey> where TKey : struct, IComparable<TKey>
{
    #region properties

    public TKey Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public bool IsDelete { get; set; }

    public DateTime UpdateDate { get; set; }

    public void Update() => UpdateDate = DateTime.UtcNow;

    #endregion
}