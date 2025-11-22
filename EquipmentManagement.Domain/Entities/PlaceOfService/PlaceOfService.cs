namespace EquipmentManagement.Domain.Entities.PlaceOfService;

public sealed class PlaceOfService : BaseEntities<ulong>
{
    public ulong? ParentId { get; set; }
    public string? Title { get; set; }

    public ICollection<Employee.Employee> Employees { get; set; } = [];
}
