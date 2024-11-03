using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationRequest;

public class OrganziationRequestQueryRepository : QueryGenericRepository<OrganziationRequestEntity>, IOrganziationRequestQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public OrganziationRequestQueryRepository(EquipmentManagementDbContext context) : base(context)
    => _context = context;

    #endregion

    public async Task<FilterOrganizationRequestsDto> FilterOrganziationRequest(FilterOrganizationRequestsDto filter,
        CancellationToken cancellationToken)
    {
        var query = _context.OrganziationRequests
                                            .AsNoTracking()
                                            .Include(p => p.DecisionMakers)
                                            .Where(p => !p.IsDelete)
                                            .OrderByDescending(p => p.CreateDate)
                                            .AsQueryable();

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<bool> IsExistAnyOrganizationRequestByRequestType(RequestType requestType,
        CancellationToken cancellationToken)
    => await _context.OrganziationRequests
        .AsNoTracking()
        .AnyAsync(p => !p.IsDelete &&
        p.RequestType == requestType);

    public async Task<OrganizationRequestEntryModel?> FillOrganizationRequestEntryModel(ulong organizationRequestId,
      CancellationToken cancellation)
    => await _context.OrganziationRequests
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == organizationRequestId)
                             .Select(p => new OrganizationRequestEntryModel()
                             {
                                 Id = p.Id,
                                 IsActive = p.IsActive,
                                 RequestType = p.RequestType
                             })
                             .FirstOrDefaultAsync();

    public async Task<List<RequestDecisionMaker>> Get_RequestDecisionMaker_ByRequestId(ulong requestId,
     CancellationToken cancellationToken)
     => await _context.RequestDecisionMakers
         .Where(p => !p.IsDelete &&
         p.OrganziationRequestId == requestId)
         .ToListAsync();

    public async Task<List<DecisionRepairRequestDto>> Get_DecisionRepairRequestDto_ByRequestId(ulong requestId,
       CancellationToken cancellationToken)
       => await _context.DecisionRepairRequestRespons
           .Where(p => !p.IsDelete &&
           p.RepariRequestId == requestId)
        .Select(p => new DecisionRepairRequestDto()
        {
            RejectDescription = p.RejectDescription,
            Response = p.Response,
            User = _context.Users
            .AsNoTracking()
            .Where(c => !c.IsDelete &&
            c.Id == p.EmployeeUserId)
            .FirstOrDefault(),
        })
           .ToListAsync();

    public async Task<List<DecisionAbolitionRequestDto>> Get_DecisionAbolitionRequestDto_ByRequestId(ulong requestId,
       CancellationToken cancellationToken)
       => await _context.DecisionAbolitionRequestRespons
           .Where(p => !p.IsDelete &&
           p.AbolitionRequestId == requestId)
        .Select(p => new DecisionAbolitionRequestDto()
        {
            RejectDescription = p.RejectDescription,
            Response = p.Response,
            User = _context.Users
            .AsNoTracking()
            .Where(c => !c.IsDelete &&
            c.Id == p.EmployeeUserId)
            .FirstOrDefault(),
        })
           .ToListAsync();

    public async Task<List<ulong>?> Get_OrganizationChartsIds_ByRequestId(ulong requestId,
       CancellationToken cancellation)
    => await _context.RequestDecisionMakers
                            .AsNoTracking()
                            .Where(p => !p.IsDelete &&
                                   p.OrganziationRequestId == requestId)
                            .Select(p => p.OrganizationChartId)
                            .ToListAsync();

    public async Task<bool> IsExistAnyConfiguration_ForRepairRequest(CancellationToken cancellationToken)
        => await _context.OrganziationRequests
        .AnyAsync(p => !p.IsDelete &&
        p.RequestType == RequestType.Repair);

    public async Task<OrganziationRequestEntity?> GetFirstConfiguration_ForRepairRequest(CancellationToken cancellationToken)
       => await _context.OrganziationRequests
       .FirstOrDefaultAsync(p => !p.IsDelete &&
       p.RequestType == RequestType.Repair);

    public async Task<OrganziationRequestEntity?> GetFirstConfiguration_ForAbolitionRequest(CancellationToken cancellationToken)
       => await _context.OrganziationRequests
       .FirstOrDefaultAsync(p => !p.IsDelete &&
       p.RequestType == RequestType.Abolition);

    public async Task<bool> IExistAny_DesicionMaker_ForRequest(ulong organziationRequestId,
        CancellationToken cancellationToken)
        => await _context.RequestDecisionMakers
        .AnyAsync(p => !p.IsDelete &&
        p.OrganziationRequestId == organziationRequestId);

    public async Task<List<RepairRequestDto>> FillRepairRequestDto(ulong userId,
        CancellationToken cancellationToken)
    => await _context.ExpertVisitorOpinions
        .AsNoTracking()
        .Where(p => !p.IsDelete &&
        p.ResponsType == Domain.Entities.OrganizationRequest.ExpertVisitorResponsType.WaitingForRespons)
        .Select(p => new RepairRequestDto()
        {
            CreateDate = p.CreateDate,
            ProductName = _context.RepairRequests
            .Include(rq => rq.Product)
            .Where(rq => !rq.IsDelete &&
                  rq.Id == p.RepairRequestId)
            .Select(rq => rq.Product!.ProductTitle)
            .FirstOrDefault(),

            RepairRequestId = p.RepairRequestId,
            VisitorRole = VisitorRole.ExpertVisitor,
            RequesterUsername = _context.RepairRequests
           .Include(rq => rq.User)
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepairRequestId)
            .Select(rq => rq.User.Username)
            .FirstOrDefault(),

            Description = _context.RepairRequests
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepairRequestId)
            .Select(rq => rq.Description)
            .FirstOrDefault(),

            DesicionMakersRepairRequestState = _context.RepairRequests
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepairRequestId)
            .Select(rq => rq.DesicionMakersRepairRequestState)
            .FirstOrDefault(),

            ExpertVisitorRepairRequestState = _context.RepairRequests
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepairRequestId)
            .Select(rq => rq.ExpertVisitorRepairRequestState)
            .FirstOrDefault(),
        })
        .ToListAsync();

    public async Task<List<AbolitionRequestDto>> FillAbolitionRequestDto(ulong userId,
      CancellationToken cancellationToken)
      => await _context.ExpertVisitorOpinionForAbolitionRequestEntities
          .AsNoTracking()
          .Where(p => !p.IsDelete &&
             p.ExpertUserId == userId &&
             p.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.WaitingForRespons)
          .Select(p => new AbolitionRequestDto()
          {
              CreateDate = p.CreateDate,
              ProductName = _context.AbolitionRequests
              .Include(rq => rq.Product)
              .Where(rq => !rq.IsDelete &&
                    rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.Product!.ProductTitle)
              .FirstOrDefault(),

              AbolitionRequestId = p.AbolitionRequestId,
              RequesterUsername = _context.AbolitionRequests
             .Include(rq => rq.User)
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.User.Username)
              .FirstOrDefault(),

              Description = _context.AbolitionRequests
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.Description)
              .FirstOrDefault(),

              ExpertVisitorAbolitionRequestState = _context.AbolitionRequests
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.ExpertVisitorAbolitionRequestState)
              .FirstOrDefault(),
          })
          .ToListAsync();

    public async Task<List<RepairRequestDto>> GetLastestNewRequestAsDecisinorsForCurrentUser(ulong userId,
      CancellationToken cancellationToken)
      => await _context.DecisionRepairRequestRespons
          .AsNoTracking()
          .Where(p => !p.IsDelete &&
          p.Response == DecisionRepairRespons.WaitingForRespons)
          .Select(p => new RepairRequestDto()
          {
              CreateDate = p.CreateDate,
              ProductName = _context.RepairRequests
              .Include(rq => rq.Product)
              .Where(rq => !rq.IsDelete &&
              rq.Id == p.RepariRequestId)
              .Select(rq => rq.Product!.ProductTitle)
              .FirstOrDefault(),

              RepairRequestId = p.RepariRequestId,
              VisitorRole = VisitorRole.DesicionMaker,

              RequesterUsername = _context.RepairRequests
             .Include(rq => rq.User)
              .Where(rq => !rq.IsDelete &&
              rq.Id == p.RepariRequestId)
              .Select(rq => rq.User.Username)
              .FirstOrDefault(),

              Description = _context.RepairRequests
              .Where(rq => !rq.IsDelete &&
              rq.Id == p.RepariRequestId)
              .Select(rq => rq.Description)
              .FirstOrDefault(),

              DesicionMakersRepairRequestState = _context.RepairRequests
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepariRequestId)
            .Select(rq => rq.DesicionMakersRepairRequestState)
            .FirstOrDefault(),

              ExpertVisitorRepairRequestState = _context.RepairRequests
            .Where(rq => !rq.IsDelete &&
                   rq.Id == p.RepariRequestId)
            .Select(rq => rq.ExpertVisitorRepairRequestState)
            .FirstOrDefault(),
          })
          .ToListAsync();

    public async Task<List<AbolitionRequestDto>> GetNotifForDecisinor(ulong userId,
      CancellationToken cancellationToken)
      => await _context.DecisionAbolitionRequestRespons
          .AsNoTracking()
          .Where(p => !p.IsDelete &&
          p.Response == DecisionAbolitionRespons.WaitingForRespons)
          .Select(p => new AbolitionRequestDto()
          {
              CreateDate = p.CreateDate,

              ProductName = _context.AbolitionRequests
              .Include(rq => rq.Product)
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.Product!.ProductTitle)
              .FirstOrDefault(),

              AbolitionRequestId = p.AbolitionRequestId,

              RequesterUsername = _context.AbolitionRequests
              .Include(rq => rq.User)
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.User.Username)
              .FirstOrDefault(),

              Description = _context.AbolitionRequests
              .Where(rq => !rq.IsDelete &&
                     rq.Id == p.AbolitionRequestId)
              .Select(rq => rq.Description)
              .FirstOrDefault(),

              ExpertVisitorAbolitionRequestState = _context.AbolitionRequests
                .Where(rq => !rq.IsDelete &&
                       rq.Id == p.AbolitionRequestId)
                .Select(rq => rq.ExpertVisitorAbolitionRequestState)
                .FirstOrDefault(),
          })
          .ToListAsync();

    public async Task<RepairRequest?> GetRepairRequestById(ulong repairReuqestId,
        CancellationToken cancellationToken)
        => await _context.RepairRequests
        .AsNoTracking()
        .Where(p => !p.IsDelete &&
        p.Id == repairReuqestId)
        .FirstOrDefaultAsync();

    public async Task<AbolitionRequest?> GetAbolitionRequestById(ulong abolitionReuqestId,
        CancellationToken cancellationToken)
        => await _context.AbolitionRequests
        .AsNoTracking()
        .Where(p => !p.IsDelete &&
            p.Id == abolitionReuqestId)
        .FirstOrDefaultAsync();

    public async Task<ExpertVisitorOpinionEntity?> Get_ExpertOpinion_ByRepairRequestId(ulong repairRequestId,
        CancellationToken cancellationToken)
        => await _context.ExpertVisitorOpinions
        .AsNoTracking()
        .Where(p => !p.IsDelete &&
        p.RepairRequestId == repairRequestId)
        .FirstOrDefaultAsync();

    public async Task<ExpertVisitorOpinionForAbolitionRequestEntity?> Get_ExpertOpinion_ByAbolitionRequestId(ulong abolitionRequestId,
      CancellationToken cancellationToken)
      => await _context.ExpertVisitorOpinionForAbolitionRequestEntities
      .AsNoTracking()
      .Where(p => !p.IsDelete &&
        p.AbolitionRequestId == abolitionRequestId)
      .FirstOrDefaultAsync();

    public async Task<List<DecisionRepairRequestRespons>?> Get_DecisionRepairRequestRespons_ByRequestIdAndUserId(
        ulong requestId,
        ulong userId,
        CancellationToken cancellationToken)
        => await _context.DecisionRepairRequestRespons
        .AsNoTracking()
        .Where(p => !p.IsDelete &&
        p.RepariRequestId == requestId &&
        p.EmployeeUserId == userId)
        .ToListAsync();

    public async Task<List<DecisionAbolitionRequestRespons>?> Get_DecisionAbolitionRequestRespons_ByRequestIdAndUserId(
      ulong requestId,
      ulong userId,
      CancellationToken cancellationToken)
      => await _context.DecisionAbolitionRequestRespons
      .AsNoTracking()
      .Where(p => !p.IsDelete &&
      p.AbolitionRequestId == requestId &&
      p.EmployeeUserId == userId)
      .ToListAsync();

    public async Task<bool> IsRequestNotBeFinished(ulong repairRequestId,
        CancellationToken cancellationToken)
        => await _context.DecisionRepairRequestRespons.AnyAsync(p => !p.IsDelete &&
        p.RepariRequestId == repairRequestId && (
        p.Response == DecisionRepairRespons.Reject ||
        p.Response == DecisionRepairRespons.WaitingForRespons));

    public async Task<bool> IsAbolitionRequestNotBeFinished(ulong abolitionRequestId,
      CancellationToken cancellationToken)
      => await _context.DecisionAbolitionRequestRespons.AnyAsync(p => !p.IsDelete &&
          p.AbolitionRequestId == abolitionRequestId && (
          p.Response == DecisionAbolitionRespons.Reject ||
          p.Response == DecisionAbolitionRespons.WaitingForRespons));

}

