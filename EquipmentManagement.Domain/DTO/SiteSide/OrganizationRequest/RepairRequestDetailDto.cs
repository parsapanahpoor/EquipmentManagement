using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public record RepairRequestDetailDto
{
    public RepairRequest? RepairRequest { get; set; }
    public Entities.Users.User? Employee { get; set; }
    public Entities.Users.User? ExpertVisitor { get; set; }
    public Entities.Product.Product? Product { get; set; }
    public ExpertVisitorOpinionEntity? ExpertVisitorOpinion { get; set; }
    public List<DecisionRepairRequestDto>? DecisionsRespons { get; set; }
}
