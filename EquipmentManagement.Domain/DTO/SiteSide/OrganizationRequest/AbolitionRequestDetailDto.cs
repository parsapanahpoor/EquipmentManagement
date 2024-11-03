using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public record AbolitionRequestDetailDto
{
    public AbolitionRequest? AbolitionRequest { get; set; }
    public Entities.Users.User? Employee { get; set; }
    public Entities.Users.User? ExpertVisitor { get; set; }
    public Entities.Product.Product? Product { get; set; }
    public ExpertVisitorOpinionForAbolitionRequestEntity? ExpertVisitorOpinion { get; set; }
    public List<DecisionAbolitionRequestDto>? DecisionsRespons { get; set; }
}

public class DecisionAbolitionRequestDto
{
    public Domain.Entities.Users.User? User { get; set; }
    public string? RejectDescription { get; set; }
    public DecisionAbolitionRespons Response { get; set; }
}
