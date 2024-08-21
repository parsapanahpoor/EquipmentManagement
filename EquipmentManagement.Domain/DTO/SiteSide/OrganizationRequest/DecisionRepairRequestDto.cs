using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public class DecisionRepairRequestDto
{
    public Domain.Entities.Users.User? User { get; set; }
    public string? RejectDescription { get; set; }
    public DecisionRepairRespons Response { get; set; }
}
