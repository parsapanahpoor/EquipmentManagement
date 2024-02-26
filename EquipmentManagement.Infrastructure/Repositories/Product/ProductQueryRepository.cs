using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EquipmentManagement.Infrastructure.Repositories.Product;

public class ProductQueryRepository : QueryGenericRepository<Domain.Entities.Product.Product>, IProductQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side

    public async Task<FilterProductDTO> FilterProducts(FilterProductDTO filter)
    {
        var query = _context.Products
                            .Include(p => p.Place)
                            .Include(p => p.ProductCategory)
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.ProductTitle)))
        {
            query = query.Where(u => u.ProductTitle.Contains(filter.ProductTitle));
        }

        if (filter.PlaceId.HasValue)
        {
            query = query.Where(p=> p.Place.Id == filter.PlaceId.Value);
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(p => p.ProductCategory.Id == filter.CategoryId.Value);
        }

        if ((!string.IsNullOrEmpty(filter.PlaceTitle)))
        {
            query = query.Where(u => u.Place.PlaceTitle.Contains(filter.PlaceTitle));
        }

        if ((!string.IsNullOrEmpty(filter.CategoryTitle)))
        {
            query = query.Where(u => u.ProductCategory.CategoryTitle.Contains(filter.CategoryTitle));
        }

        if ((!string.IsNullOrEmpty(filter.BarCode)))
        {
            query = query.Where(u => u.BarCode.Contains(filter.BarCode));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<bool> IsExistAny_Product_ByBarCode(string barCode , 
                                                         CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.BarCode == barCode);
         
    }

    public async Task<ProductDetailDTO?> FillProductDetailDTO(ulong productId , CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && 
                                    p.Id == productId)
                             .Select(p => new ProductDetailDTO
                             {
                                 BarCode = p.BarCode,
                                 Description = p.Description,
                                 EntityCount = p.EntityCount,
                                 ProductTitle = p.ProductTitle,
                                 ProductId = productId,
                                 CategoryName = _context.ProductCategories  
                                                        .AsNoTracking()
                                                        .Where(s=> !s.IsDelete &&
                                                               s.Id == p.ProductCategoryId)
                                                        .Select(s=> s.CategoryTitle)
                                                        .FirstOrDefault(),
                                 PlaceName = _context.Places
                                                        .AsNoTracking()
                                                        .Where(s => !s.IsDelete &&
                                                               s.Id == p.PlaceId)
                                                        .Select(s => s.PlaceTitle)
                                                        .FirstOrDefault(),
                             })
                             .FirstOrDefaultAsync();
    }

    #endregion
}
