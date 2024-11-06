using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EquipmentManagement.Infrastructure.Repositories.Product;

public class ProductQueryRepository : 
    QueryGenericRepository<Domain.Entities.Product.Product>,
    IProductQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side
    
    public async Task<FiltreProductRepairRequestDto> FiltreProductRepairRequest(
        FiltreProductRepairRequestDto filter , 
        CancellationToken cancellationToken)
    {
        var query = _context.RepairRequests
                            .Include(p => p.User)
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete && 
                                        p.ProductId == filter.ProductId)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }
    public async Task<FiltreProductAbolitionRequestDto> FiltreProductAbolitionRequest(
        FiltreProductAbolitionRequestDto filter , 
        CancellationToken cancellationToken)
    {
        var query = _context.AbolitionRequests
                            .Include(p => p.User)
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete && 
                                        p.ProductId == filter.ProductId)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<FiltreOrganizationRequestDocumentDto> FiltreOrganizationRequestDocument(
      FiltreOrganizationRequestDocumentDto filter,
      CancellationToken cancellationToken)
    {
        var query = _context.OrganizationRequestDocuments
                                        .Include(p => p.User)
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete &&
                                               p.OrganizationRequestType == filter.RequestType && 
                                               p.RequestId == filter.OrganizationRequestId)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<FilterProductDTO> FilterProducts(FilterProductDTO filter)
    {
        var query = _context.Products
                            .Include(p => p.Place)
                            .Include(p => p.ProductCategory)
                            .AsNoTracking()
                            .Where(p => !p.IsDelete)
                            .OrderByDescending (p => p.Id)
                            .AsQueryable();

        #region Sorting

        switch (filter.PlaceSorting)
        {
            case PlaceSorting.All:
                break;
            case PlaceSorting.Ascending:
                query = query.OrderBy(p=> p.Place.PlaceTitle);
                break;
            case PlaceSorting.Descending:
                query = query.OrderByDescending(p => p.Place.PlaceTitle);
                break;
        }

        switch (filter.ProductTitleSorting)
        {
            case ProductTitleSorting.All:
                break;
            case ProductTitleSorting.Ascending:
                query = query.OrderBy(p => p.ProductTitle);
                break;
            case ProductTitleSorting.Descending:
                query = query.OrderByDescending(p => p.ProductTitle);
                break;
        }

        switch (filter.BrandSorting)
        {
            case BrandSorting.All:
                break;
            case BrandSorting.Ascending:
                query = query.OrderBy(p => p.Brand);
                break;
            case BrandSorting.Descending:
                query = query.OrderByDescending(p => p.Brand);
                break;
        }

        switch (filter.RepostiroyCodeSorting)
        {
            case RepostiroyCodeSorting.All:
                break;
            case RepostiroyCodeSorting.Ascending:
                query = query.OrderBy(p => p.RepostiroyCode);
                break;
            case RepostiroyCodeSorting.Descending:
                query = query.OrderByDescending(p => p.RepostiroyCode);
                break;
        }

        switch (filter.CategorySorting)
        {
            case CategorySorting.All:
                break;
            case CategorySorting.Ascending:
                query = query.OrderBy(p => p.ProductCategory.CategoryTitle);
                break;
            case CategorySorting.Descending:
                query = query.OrderByDescending(p => p.ProductCategory.CategoryTitle);
                break;
        }

        switch (filter.BarCodeSorting)
        {
            case BarCodeSorting.All:
                break;
            case BarCodeSorting.Ascending:
                query = query.OrderBy(p => p.BarCode);
                break;
            case BarCodeSorting.Descending:
                query = query.OrderByDescending(p => p.BarCode);
                break;
        }

        switch (filter.ProductIdSorting)
        {
            case ProductIdSorting.All:
                break;
            case ProductIdSorting.Ascending:
                query = query.OrderBy(p => p.Id);
                break;
            case ProductIdSorting.Descending:
                query = query.OrderByDescending(p => p.Id);
                break;
        }

        switch (filter.CreateDateSorting)
        {
            case CreateDateSorting.All:
                break;
            case CreateDateSorting.Ascending:
                query = query.OrderBy(p => p.CreateDate);
                break;
            case CreateDateSorting.Descending:
                query = query.OrderByDescending(p => p.CreateDate);
                break;
        }

        #endregion

        #region filter

        if ((!string.IsNullOrEmpty(filter.ProductTitle)))
            query = query.Where(u => u.ProductTitle.Contains(filter.ProductTitle));

        if ((!string.IsNullOrEmpty(filter.Brand)))
            query = query.Where(u => u.Brand!.Contains(filter.Brand));

        if ((!string.IsNullOrEmpty(filter.RepostiroyCode)))
            query = query.Where(u => u.RepostiroyCode.Contains(filter.RepostiroyCode));

        if (filter.PlaceId.HasValue)
            query = query.Where(p=> p.Place!.Id == filter.PlaceId.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.ProductCategory!.Id == filter.CategoryId.Value);

        if ((!string.IsNullOrEmpty(filter.PlaceTitle)))
            query = query.Where(u => u.Place!.PlaceTitle.Contains(filter.PlaceTitle));

        if ((!string.IsNullOrEmpty(filter.CategoryTitle)))
            query = query.Where(u => u.ProductCategory!.CategoryTitle.Contains(filter.CategoryTitle));

        if ((!string.IsNullOrEmpty(filter.BarCode)))
            query = query.Where(u => u.BarCode.Contains(filter.BarCode));

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<List<FilterProductForExcelFilesDTO>> FilterProductsForExcelFiles(FilterProductForExcelFilesDTO filter)
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

        if ((!string.IsNullOrEmpty(filter.RepostiroyCode)))
        {
            query = query.Where(u => u.RepostiroyCode.Contains(filter.RepostiroyCode));
        }

        if (filter.PlaceId.HasValue)
        {
            query = query.Where(p => p.Place.Id == filter.PlaceId.Value);
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

        return await query.Select(p=> new FilterProductForExcelFilesDTO()
        {
            BarCode = p.BarCode,
            CategoryId = p.ProductCategoryId,
            ProductTitle = p.ProductTitle,
            CategoryTitle = p.ProductCategory.CategoryTitle,
            PlaceId = p.PlaceId,
            PlaceTitle = p.Place.PlaceTitle,
            Id = p.Id,
            CreateDate = p.CreateDate,
            Description = p.Description,
            RepostiroyCode = p.RepostiroyCode,
            Brand = p.Brand
        })
        .ToListAsync();
    }

    public async Task<bool> IsExist_Product_ByRfId(string rfId , CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.BarCode == rfId)
                             .AnyAsync();
    }

    public async Task<Domain.Entities.Product.Product?> GetProduct_Product_ByRfId(string rfId, CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.BarCode.Equals(rfId))
                             .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistAny_Product_ByBarCode(string barCode , 
                                                         CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.BarCode == barCode);
         
    }

    public async Task<bool> IsExistAny_Product_ByBarCode(string barCode,
                                                         ulong productId, 
                                                         CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Id != productId &&
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
                                 ProductTitle = p.ProductTitle,
                                 ProductId = productId,
                                 RepositoryCode = p.RepostiroyCode,
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

    public async Task<EditProductDTO?> Fill_EditProductDTO(ulong productId,
                                                           CancellationToken cancellationToken)
    {
        return await _context.Products
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == productId)
                             .Select(p => new EditProductDTO
                             {
                                 BarCode = p.BarCode,
                                 Description = p.Description,
                                 ProductTitle = p.ProductTitle,
                                 ProductId = productId,
                                 RepositoryCode = p.RepostiroyCode,
                                 CategoryId = p.ProductCategoryId,
                                 PlaceId = p.PlaceId,
                                 CategoryName = _context.ProductCategories
                                                        .AsNoTracking()
                                                        .Where(s => !s.IsDelete &&
                                                               s.Id == p.ProductCategoryId)
                                                        .Select(s => s.CategoryTitle)
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
