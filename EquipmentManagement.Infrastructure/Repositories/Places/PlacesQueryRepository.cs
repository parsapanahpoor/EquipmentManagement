using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.DTO.SiteSide.Places;
using EquipmentManagement.Domain.IRepositories.Place;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record;

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

    public async Task<FilterPlacesDTO> FilterPlaces(FilterPlacesDTO filter, CancellationToken cancellationToken)
    {
        var query = _context.Places
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

    public async Task<List<FilterPlacesForExcelFileDTO>> FilterPlacesForExcelFile( CancellationToken cancellationToken)
    {
        var returnModel = new List<FilterPlacesForExcelFileDTO>();

        var parents = await _context.Places
                            .AsNoTracking()
                            .Where(p => !p.IsDelete && !p.ParentId.HasValue)
                            .OrderByDescending(p => p.CreateDate)
                            .Select(p => new FilterPlacesForExcelFileDTO()
                            {
                                PlaceTitle = p.PlaceTitle,
                                Id = p.Id,
                                ParentId = p.ParentId,
                                ParentPlace = null,
                            })
                            .ToListAsync();

        foreach (var p in parents)
        {
            var childs = await _context.Places
                            .AsNoTracking()
                            .Where(s => !s.IsDelete && s.ParentId.HasValue && s.ParentId.Value == p.Id)
                            .OrderByDescending(p => p.CreateDate)
                            .Select(s => new FilterPlacesForExcelFileDTO()
                            {
                                PlaceTitle = s.PlaceTitle,
                                Id = s.Id,
                                ParentId = s.ParentId,
                                ParentPlace = p.PlaceTitle,
                            })
                            .ToListAsync();

            returnModel.Add(p);

            foreach (var item in childs)
            {
                returnModel.Add(item);
            }
        }

        return returnModel;
    }

    public async Task<List<Domain.Entities.Places.Place>> GetSubPlacesByPlaceParentId(ulong parentId,
                                                                                      CancellationToken cancellationToken)
    {
        return await _context.Places
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.ParentId == parentId)
                             .ToListAsync();
    }

    public async Task<bool> IsExistAny_Place_ById(ulong placeId, CancellationToken cancellationToken)
    {
        return await _context.Places
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Id == placeId);
    }

    public async Task<List<SelectListOfPlacesDTO>> FillSelectListOfPlacesDTO(CancellationToken cancellation)
    {
        return await _context.Places
                             .AsNoTracking()
                             .Where(p => !p.IsDelete)
                             .OrderByDescending(p => p.CreateDate)
                             .Select(p => new SelectListOfPlacesDTO()
                             {
                                 ParentId = p.ParentId,
                                 PlaceId = p.Id,
                                 PlaceTitle = p.PlaceTitle
                             })
                             .ToListAsync();
    }

    #endregion
}
