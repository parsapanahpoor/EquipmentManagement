using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.IRepositories.OranizationRequest;

public interface IOrganziationRequestCommandRepository
{
    Task AddAsync(OrganziationRequestEntity entity,
        CancellationToken cancellationToken);

    Task Add_RequestDecisionMaker(RequestDecisionMaker model,
        CancellationToken cancellationToken);

    void Update(OrganziationRequestEntity entity);
    void Delete_RequestDecisionMaker(RequestDecisionMaker model);
}
