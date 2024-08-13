using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
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
}

