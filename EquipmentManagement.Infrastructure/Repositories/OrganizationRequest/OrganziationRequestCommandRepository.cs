using EquipmentManagement.Domain.Entities.OrganizationRequest;
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

    public async Task Add_ExpertVisitorRequest(ExpertVisitorOpinionEntity data , 
        CancellationToken cancellationToken)
        => await _context.ExpertVisitorOpinions.AddAsync(data, cancellationToken);

    public async Task Add_RepairRequest(RepairRequest data,
        CancellationToken cancellationToken)
        => await _context.RepairRequests.AddAsync(data, cancellationToken);
}

