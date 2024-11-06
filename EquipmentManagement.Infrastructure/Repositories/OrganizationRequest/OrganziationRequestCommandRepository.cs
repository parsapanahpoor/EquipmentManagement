using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationRequest;

public class OrganziationRequestCommandRepository :
    CommandGenericRepository<OrganziationRequestEntity>,
    IOrganziationRequestCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;
    public OrganziationRequestCommandRepository(EquipmentManagementDbContext context) : base(context)
        => _context = context;

    #endregion

    public void Delete_RequestDecisionMaker(RequestDecisionMaker model)
       => _context.RequestDecisionMakers.Remove(model);

    public async Task Add_RequestDecisionMaker(RequestDecisionMaker model,
    CancellationToken cancellationToken)
        => await _context.RequestDecisionMakers.AddAsync(model, cancellationToken);

    public async Task Add_DecisionRepairRequestRespons(DecisionRepairRequestRespons data,
        CancellationToken cancellationToken)
        => await _context.DecisionRepairRequestRespons.AddAsync(data, cancellationToken);

    public async Task Add_DecisionAbolitionRequestRespons(DecisionAbolitionRequestRespons data,
      CancellationToken cancellationToken)
      => await _context.DecisionAbolitionRequestRespons.AddAsync(data, cancellationToken);

    public async Task Add_ExpertVisitorRequest(ExpertVisitorOpinionEntity data,
        CancellationToken cancellationToken)
        => await _context.ExpertVisitorOpinions.AddAsync(data, cancellationToken);

    public async Task Add_ExpertVisitorRequest(ExpertVisitorOpinionForAbolitionRequestEntity data,
    CancellationToken cancellationToken)
        => await _context.ExpertVisitorOpinionForAbolitionRequestEntities.AddAsync(data, cancellationToken);

    public async Task Add_RepairRequest(RepairRequest data,
        CancellationToken cancellationToken)
        => await _context.RepairRequests.AddAsync(data, cancellationToken);

    public async Task Add_AbolitionRequest(AbolitionRequest data,
       CancellationToken cancellationToken)
       => await _context.AbolitionRequests.AddAsync(data, cancellationToken);

    public async Task Add_OrganizationRequestDocument(OrganizationRequestDocumentEntity data,
       CancellationToken cancellationToken)
       => await _context.OrganizationRequestDocuments.AddAsync(data, cancellationToken);

    public void Update_ExpertVisitorResponse(ExpertVisitorOpinionEntity data)
        => _context.ExpertVisitorOpinions.Update(data);

    public void Update_ExpertVisitorResponse(ExpertVisitorOpinionForAbolitionRequestEntity data)
        => _context.ExpertVisitorOpinionForAbolitionRequestEntities.Update(data);

    public void Update_DecisionRepairRequestRespons(DecisionRepairRequestRespons data)
            => _context.DecisionRepairRequestRespons.Update(data);

    public void Update_DecisionAbolitionRequestRespons(DecisionAbolitionRequestRespons data)
        => _context.DecisionAbolitionRequestRespons.Update(data);

    public void Update_RepairRequest(RepairRequest data)
         => _context.RepairRequests.Update(data);

    public void Update_AbolitionRequest(AbolitionRequest data)
        => _context.AbolitionRequests.Update(data);
}

