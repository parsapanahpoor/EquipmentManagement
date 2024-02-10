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

    #endregion
}

