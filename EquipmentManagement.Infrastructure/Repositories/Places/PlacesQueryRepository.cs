using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.IRepositories.Place;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.Places;

public class PlacesQueryRepository : QueryGenericRepository<Domain.Entities.Places.Place>, IPlacesQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public PlacesQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Side Side

    public async Task<FilterPlacesDTO> FilterPlaces(FilterPlacesDTO filter , CancellationToken cancellationToken)
    {
        var query = _context.Places
                            .AsNoTracking()
                            .Where(p => !p.IsDelete )
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

        if ((!string.IsNullOrEmpty(filter.PlaceTitle)))
        {
            query = query.Where(u => u.PlaceTitle.Contains(filter.PlaceTitle));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<List<Domain.Entities.Places.Place>> GetSubPlacesByPlaceParentId(ulong parentId , 
                                                                                      CancellationToken cancellationToken)
    {
        return await _context.Places
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.ParentId == parentId)
                             .ToListAsync();
    }

    #endregion
}
