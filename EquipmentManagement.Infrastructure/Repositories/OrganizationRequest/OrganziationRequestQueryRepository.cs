using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
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
        p.ResponsType == ExpertVisitorResponsType.WaitingForRespons)
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
        })
        .ToListAsync();
}

