namespace EquipmentManagement.Domain.Entities.Employee;

public class Employee : BaseEntities<ulong>
{
    public ulong PlaceOfServiceId { get; set; }
    public PlaceOfService.PlaceOfService? PlaceOfService { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonnelCode { get; set; }
    public string? Mobile { get; set; }
}
