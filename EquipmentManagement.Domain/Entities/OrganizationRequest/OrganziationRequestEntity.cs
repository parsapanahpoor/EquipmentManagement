namespace EquipmentManagement.Domain.Entities.OrganizationRequest;

public class OrganziationRequestEntity : BaseEntities<ulong>
{
    public RequestType RequestType { get; set; }
    public bool IsActive { get; set; }

    public virtual List<RequestDecisionMaker> DecisionMakers { get; set; } = [];
}
