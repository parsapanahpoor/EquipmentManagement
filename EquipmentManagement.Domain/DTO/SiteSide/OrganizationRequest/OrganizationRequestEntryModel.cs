using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public record OrganizationRequestEntryModel
{
    public ulong? Id { get; set; }
    public RequestType RequestType { get; set; }
    public bool IsActive { get; set; }
}
