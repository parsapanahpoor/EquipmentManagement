using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record;

namespace EquipmentManagement.Infrastructure.Repositories.PlaceOfServices;

public class PlaceOfServicesQueryRepository :
    QueryGenericRepository<Domain.Entities.PlaceOfService.PlaceOfService>, 
    IPlaceOfServicesQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public PlaceOfServicesQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Side Side

    public async Task<List<Domain.Entities.PlaceOfService.PlaceOfService>> GetAllOfOlaceOfServices(CancellationToken cancellationToken)
        => await _context.PlaceOfServices
                         .AsNoTracking()
                         .Where(p => !p.IsDelete)
                         .OrderByDescending(p => p.CreateDate)
                         .ToListAsync(cancellationToken);

    public async Task<FilterPlaceOfServicesDTO> FilterPlaceOfServices(FilterPlaceOfServicesDTO filter, CancellationToken cancellationToken)
    {
        var query = _context.PlaceOfServices
                            .AsNoTracking()
                            .Where(p => !p.IsDelete)
                            .OrderByDescending(p => p.CreateDate)
                            .AsQueryable();

        #region filter

        if (filter.ParentId.HasValue)
        {
            query = query.Where(p => p.ParentId == filter.ParentId.Value);
        }
        else
        {
            query = query.Where(p => p.ParentId == null);
        }

        if ((!string.IsNullOrEmpty(filter.PlaceOfServiceTitle)))
        {
            query = query.Where(u => u.Title.Contains(filter.PlaceOfServiceTitle));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<List<Domain.Entities.PlaceOfService.PlaceOfService>> GetSubPlaceOfServicesByPlaceOfServiceParentId(ulong parentId,
                                                                                      CancellationToken cancellationToken)
    {
        return await _context.PlaceOfServices
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.ParentId == parentId)
                             .ToListAsync();
    }

    public async Task<bool> IsExistAny_PlaceOfService_ById(ulong PlaceOfServiceId, CancellationToken cancellationToken)
    {
        return await _context.PlaceOfServices
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Id == PlaceOfServiceId);
    }

    public async Task<List<SelectListOfPlaceOfServicesDTO>> FillSelectListOfPlaceOfServicesDTO(CancellationToken cancellation)
    {
        return await _context.PlaceOfServices
                             .AsNoTracking()
                             .Where(p => !p.IsDelete)
                             .OrderByDescending(p => p.CreateDate)
                             .Select(p => new SelectListOfPlaceOfServicesDTO()
                             {
                                 ParentId = p.ParentId,
                                 PlaceOfServiceId = p.Id,
                                 PlaceOfServiceTitle = p.Title
                             })
                             .ToListAsync();
    }

    #endregion
}
