using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationChart;

public class OrganizationChartQueryRepository :
    QueryGenericRepository<OrganizationChartAggregate>,
    IOrganizationChartQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public OrganizationChartQueryRepository(EquipmentManagementDbContext context) : base(context)
    => _context = context;

    #endregion

    public async Task<FilterOrganizationChartsDto> FilterOrganizationChart(FilterOrganizationChartsDto organizationChartAggregate,
            CancellationToken cancellationToken)
    {
        var query = _context.OrganizationCharts
                                            .AsNoTracking()
                                            .Include(p => p.UserSelectedOrganizationChartEntities)
                                            .Where(p => !p.IsDelete &&
                                            !p.ParentId.HasValue)
                                            .OrderByDescending(p => p.CreateDate)
                                            .AsQueryable();

        if (organizationChartAggregate.ParentId.HasValue)
            query = _context.OrganizationCharts
                                             .AsNoTracking()
                                             .Include(p => p.UserSelectedOrganizationChartEntities)
                                             .Where(p => !p.IsDelete &&
                                             p.ParentId == organizationChartAggregate.ParentId)
                                             .OrderByDescending(p => p.CreateDate)
                                             .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(organizationChartAggregate.Title)))
            query = query.Where(u => u.Title!.Contains(organizationChartAggregate.Title));

        #endregion

        #region paging

        await organizationChartAggregate.Paging(query);

        #endregion

        return organizationChartAggregate;
    }

    public async Task<OrganizationChartEntryModel?> FillOrganizationChartEntryModel(ulong organizationChartId,
        CancellationToken cancellation)
    {
        return await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == organizationChartId)
                             .Select(p => new OrganizationChartEntryModel()
                             {
                                 Description = p.Description,
                                 OrganizationChartId = organizationChartId,
                                 ParentId = p.ParentId,
                                 Title = p.Title,
                             })
                             .FirstOrDefaultAsync();
    }

    public async Task<List<ulong>?> Get_OrganizationChartsIds_ByUserId(ulong userId,
        CancellationToken cancellation)
    => await _context.UserSelectedOrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.UserId == userId)
                             .Select(p => p.OrganizationChartAggregateId)
                             .ToListAsync();

    public async Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(CancellationToken cancellation)
    => await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && !p.ParentId.HasValue)
                             .OrderByDescending(p => p.CreateDate)
                             .Select(p => new OrganizationChartSelectedForUserDto()
                             {
                                 OrganizationChart = p,
                                 OrganizationChartChildren = _context.OrganizationCharts
                                 .AsNoTracking()
                                 .Where(c => !c.IsDelete &&
                                 c.ParentId == p.Id)
                                 .Select(c => new OrganizationChartAggregate()
                                 {
                                     Id = c.Id,
                                     ParentId = c.ParentId,
                                     IsDelete = c.IsDelete,
                                     CreateDate = c.CreateDate,
                                     Description = c.Description,
                                     Title = c.Title,
                                     UpdateDate = c.UpdateDate,
                                 })
                                 .ToList()
                             })
                             .ToListAsync();

    public async Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(string brandTitle,
        CancellationToken cancellation)
    => await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete)
                             .OrderByDescending(p => p.CreateDate)
                             .Select(p => new OrganizationChartSelectedForUserDto()
                             {
                                 OrganizationChart = p,
                                 OrganizationChartChildren = _context.OrganizationCharts
                                 .AsNoTracking()
                                 .Where(c => !c.IsDelete &&
                                 c.ParentId == p.Id &&
                                 c.Title.Contains(brandTitle))
                                 .Select(c => new OrganizationChartAggregate()
                                 {
                                     Id = c.Id,
                                     ParentId = c.ParentId,
                                     IsDelete = c.IsDelete,
                                     CreateDate = c.CreateDate,
                                     Description = c.Description,
                                     Title = c.Title,
                                     UpdateDate = c.UpdateDate,
                                 })
                                 .ToList()
                             })
                             .ToListAsync();

    public async Task<List<UserSelectedOrganizationChartEntity>> Get_UserSelectedOrganizationCharts_ByUserId(ulong userId,
        CancellationToken cancellationToken)
        => await _context.UserSelectedOrganizationCharts
            .Where(p => !p.IsDelete &&
               p.UserId == userId)
            .ToListAsync();

    public async Task<List<Domain.Entities.Users.User>> Get_Users_FromUserSelectedOrganizationChart(ulong organizationChartId,
        CancellationToken cancellationToken)
        => await _context.UserSelectedOrganizationCharts
        .AsNoTracking()
        .Include(p => p.User)
        .Where(p => !p.IsDelete &&
              p.OrganizationChartAggregateId == organizationChartId)
        .OrderByDescending(p => p.CreateDate)
        .Select(p => p.User)
        .ToListAsync();

    public async Task<List<UserSelectedOrganizationChartEntity>> Get_RepairRequestDesiciners(CancellationToken cancellationToken)
    {
        var userSelectedOrganizationCharts = new List<UserSelectedOrganizationChartEntity>();

        var repairRequesttId = await _context.OrganziationRequests
            .AsNoTracking()
            .Where(p => !p.IsDelete &&
               p.RequestType == RequestType.Repair)
            .Select(p => p.Id)
            .FirstOrDefaultAsync();

        var organizationCharts = await _context.RequestDecisionMakers
            .AsNoTracking()
            .Where(p => !p.IsDelete &&
               p.OrganziationRequestId == repairRequesttId)
            .Select(p => p.OrganizationChartId)
            .ToListAsync();

        foreach (var organizationChartId in organizationCharts)
        {
            var userSelectedOrganizationChart = await _context.UserSelectedOrganizationCharts
                .AsNoTracking()
                .Where(p => !p.IsDelete &&
                     p.OrganizationChartAggregateId == organizationChartId)
                .ToListAsync();

            if (userSelectedOrganizationChart is not null &&
                userSelectedOrganizationChart.Any())
            {
                foreach (var item in userSelectedOrganizationChart)
                {
                    if (!userSelectedOrganizationCharts.Contains(item))
                     userSelectedOrganizationCharts.Add(item);
                }
            }
        }

        return userSelectedOrganizationCharts;
    }

    public async Task<List<UserSelectedOrganizationChartEntity>> Get_AbolitionRequestDesiciners(CancellationToken cancellationToken)
    {
        var userSelectedOrganizationCharts = new List<UserSelectedOrganizationChartEntity>();

        var abilitionRequesttId = await _context.OrganziationRequests
            .AsNoTracking()
            .Where(p => !p.IsDelete &&
               p.RequestType == RequestType.Abolition)
            .Select(p => p.Id)
            .FirstOrDefaultAsync();

        var organizationCharts = await _context.RequestDecisionMakers
            .AsNoTracking()
            .Where(p => !p.IsDelete &&
               p.OrganziationRequestId == abilitionRequesttId)
            .Select(p => p.OrganizationChartId)
            .ToListAsync();

        foreach (var organizationChartId in organizationCharts)
        {
            var userSelectedOrganizationChart = await _context.UserSelectedOrganizationCharts
                .AsNoTracking()
                .Where(p => !p.IsDelete &&
                     p.OrganizationChartAggregateId == organizationChartId)
                .ToListAsync();

            if (userSelectedOrganizationChart is not null &&
                userSelectedOrganizationChart.Any())
            {
                foreach (var item in userSelectedOrganizationChart)
                {
                    if (!userSelectedOrganizationCharts.Contains(item))
                        userSelectedOrganizationCharts.Add(item);
                }
            }
        }

        return userSelectedOrganizationCharts;
    }
}