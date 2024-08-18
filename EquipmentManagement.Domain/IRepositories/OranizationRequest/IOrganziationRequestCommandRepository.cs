using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.IRepositories.OranizationRequest;

public interface IOrganziationRequestCommandRepository
{
    Task AddAsync(OrganziationRequestEntity entity,
        CancellationToken cancellationToken);

    Task Add_RequestDecisionMaker(RequestDecisionMaker model,
        CancellationToken cancellationToken);

    Task Add_ExpertVisitorRequest(ExpertVisitorOpinionEntity data,
        CancellationToken cancellationToken);

    Task Add_DecisionRepairRequestRespons(DecisionRepairRequestRespons data,
        CancellationToken cancellationToken);

    Task Add_RepairRequest(RepairRequest data,
        CancellationToken cancellationToken);

    void Update(OrganziationRequestEntity entity);
    void Delete_RequestDecisionMaker(RequestDecisionMaker model);
    void Update_ExpertVisitorResponse(ExpertVisitorOpinionEntity data);
    void Update_DecisionRepairRequestRespons(DecisionRepairRequestRespons data);
    void Update_RepairRequest(RepairRequest data);
}
