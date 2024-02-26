using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Infrastructure.Repositories.ProductCategory;

public class ProductCategoryQueryRepository : QueryGenericRepository<Domain.Entities.ProductCategory.ProductCategory>, IProductCategoryQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductCategoryQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Admin Side

    public async Task<FilterProductCategories> FilterProductCategories(FilterProductCategories filter)
    {
        var query = _context.ProductCategories
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.Title)))
        {
            query = query.Where(u => u.CategoryTitle.Contains(filter.Title));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<EditProductCategoryDTO?> FillEditProductCategoryDTO(ulong categoryId, CancellationToken cancellation)
    {
        return await _context.ProductCategories
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == categoryId)
                             .Select(p => new EditProductCategoryDTO()
                             {
                                 CategoryId = p.Id,
                                 Title = p.CategoryTitle
                             })
                             .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistAny_Category_ByCategoryId(ulong categoryId , CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Id == categoryId);
    }

    public async Task<List<SelectListOfCategoriesDTO>> FillSelectListOfCategoriesDTO(CancellationToken cancellation)
    {
        return await _context.ProductCategories
                             .AsNoTracking()
                             .Where(p => !p.IsDelete)
                             .Select(p=> new SelectListOfCategoriesDTO()
                             {
                                 CategoryId = p.Id,
                                 CategoryTitle = p.CategoryTitle
                             })
                             .ToListAsync();
    }

    #endregion
}

