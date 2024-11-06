using EquipmentManagement.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public sealed class OrganizationRequestDocumentEntity : BaseEntities<ulong>
{
    public ulong RequestId { get; set; }
    public RequestType OrganizationRequestType { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }

    [ForeignKey(nameof(EmployeeUserId))]
    public User? User { get; set; }
    public ulong EmployeeUserId { get; set; }
}
