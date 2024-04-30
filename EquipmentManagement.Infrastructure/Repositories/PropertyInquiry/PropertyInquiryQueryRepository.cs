using EquipmentManagement.Application.CQRS.SiteSide.Account.Query;
using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace EquipmentManagement.Infrastructure.Repositories.PropertyInquiry;

public class PropertyInquiryQueryRepository : QueryGenericRepository<Domain.Entities.PropertyInquiry.PropertyInquiry>, IPropertyInquiryQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public PropertyInquiryQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<List<PropertyInquiryDTO>> FilterSystemPropertyInquiries(FilterSystemPropertyInquiriesDTO filter,
                                                                                      CancellationToken cancellation)
    {
        var inquiries = _context.PropertyInquiries
        .Where(p => !p.IsDelete)
        .OrderBy(s => s.CreateDate)
        .AsQueryable();

        var query = from q in inquiries
                    join u in _context.Users
                                      .AsNoTracking()
                                      .Where(p => !p.IsDelete)
                                      .AsQueryable()
                    on q.UserId equals u.Id
                    select new PropertyInquiryDTO()
                    {
                        CreateDate = q.CreateDate,
                        ExcelFile = q.ExcelFile,
                        UserId = u.Id,
                        Username = u.Username,
                        InquiryId = q.Id,
                        PlaceId = q.PlaceId,
                        PlaceTitle = _context.Places
                                             .AsNoTracking()
                                             .Where(p => !p.IsDelete &&
                                                    p.Id == q.PlaceId)
                                             .Select(p => p.PlaceTitle)
                                             .FirstOrDefault()
                    };

        #region Filter

        if (!string.IsNullOrEmpty(filter.StartTime))
        {
            var spliteDate = filter.StartTime.Split('/');
            int year = int.Parse(spliteDate[0]);
            int month = int.Parse(spliteDate[1]);
            int day = int.Parse(spliteDate[2]);
            DateTime fromDate = new DateTime(year, month, day, new PersianCalendar());

            query = query.Where(s => s.CreateDate >= fromDate);
        }

        if (!string.IsNullOrEmpty(filter.EndTime))
        {
            var spliteDate = filter.EndTime.Split('/');
            int year = int.Parse(spliteDate[0]);
            int month = int.Parse(spliteDate[1]);
            int day = int.Parse(spliteDate[2]);
            DateTime toDate = new DateTime(year, month, day, new PersianCalendar());

            query = query.Where(s => s.CreateDate <= toDate);
        }

        if (!string.IsNullOrEmpty(filter.PlaceTitle))
        {
            query = query.Where(p => p.PlaceTitle.Contains(filter.PlaceTitle));
        }

        #endregion

        return await query.ToListAsync();
    }
    public async Task<FilterPropertiesInquiry_BadgesCountDTO> FilterInquiryDetail_BadgesCount(ulong placeId ,
                                                                                              ulong inquiryId ,
                                                                                              CancellationToken cancellation = default)
    {
        var returnModel = new FilterPropertiesInquiry_BadgesCountDTO();

        var placeProducts = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.PlaceId == placeId)
                                    .AsQueryable();

        var inqueryProducts = _context.PropertyInquiryDetails
                                      .AsNoTracking()
                                      .Where(p => !p.IsDelete &&
                                             p.PropertyInquiryId == inquiryId)
                                      .AsQueryable();

        //Existing Products in inquiry
        var products = from q in inqueryProducts
                       join p in placeProducts
                       on q.RF_Id equals p.BarCode
                       select new FilterInquiryDetail
                       {
                           CategoryTitle = p.ProductCategory.CategoryTitle,
                           InquiryDetailId = q.Id,
                           PlaceTitle = p.Place.PlaceTitle,
                           PropertyTitle = p.ProductTitle,
                           PropertyId = p.Id,
                           RfId = p.BarCode,
                           IsExistInInquiey = IsExistInInquiey.FoundInInquiry
                       };

        returnModel.FoundInInquiry = products.Count();

        //Not Found Products in inquiry
        var notFoundProducts_BarCode = placeProducts.Select(p => p.BarCode).Except(inqueryProducts.Select(p => p.RF_Id));
        foreach (var notFoundProduct_BarCode in notFoundProducts_BarCode)
        {
            var notFoundProduct = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.BarCode == notFoundProduct_BarCode)
                                    .Select(p => new FilterInquiryDetail()
                                    {
                                        CategoryTitle = p.ProductCategory.CategoryTitle,
                                        InquiryDetailId = inquiryId,
                                        PlaceTitle = p.Place.PlaceTitle,
                                        PropertyTitle = p.ProductTitle,
                                        PropertyId = p.Id,
                                        RfId = p.BarCode,
                                        IsExistInInquiey = IsExistInInquiey.NotFoundInInquiry
                                    })
                                    .FirstOrDefault();

            if (notFoundProduct != null)
            {
                returnModel.NotFoundInInquiry ++;
            }
        }

        //New Products From Another Places
        var NewProductsFromAnotherPlaces_BarCode = inqueryProducts.Select(p => p.RF_Id).Except(placeProducts.Select(p => p.BarCode));
        foreach (var NewProductsFromAnotherPlace_BarCode in NewProductsFromAnotherPlaces_BarCode)
        {
            var NewProducts = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.BarCode == NewProductsFromAnotherPlace_BarCode)
                                    .Select(p => new FilterInquiryDetail()
                                    {
                                        CategoryTitle = p.ProductCategory.CategoryTitle,
                                        InquiryDetailId = inquiryId,
                                        PlaceTitle = p.Place.PlaceTitle,
                                        PropertyTitle = p.ProductTitle,
                                        PropertyId = p.Id,
                                        RfId = p.BarCode,
                                        IsExistInInquiey = IsExistInInquiey.NewProductsFromAnotherPlaces
                                    })
                                    .FirstOrDefault();

            if (NewProducts != null)
            {
                returnModel.NewProductsFromAnotherPlaces++;
            }
        }

        return returnModel;
    }

    public async Task<List<FilterInquiryDetail>> FilterInquiryDetail(FilterInquiryDetailDTO filter,
                                                                  CancellationToken cancellation = default)
    {
        var list = new List<FilterInquiryDetail>();

        var placeProducts = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.PlaceId == filter.PlaceId)
                                    .AsQueryable();

        var inqueryProducts = _context.PropertyInquiryDetails
                                      .AsNoTracking()
                                      .Where(p => !p.IsDelete &&
                                             p.PropertyInquiryId == filter.PropertyInquiryId)
                                      .AsQueryable();
        //Existing Products in inquiry
        var products = from q in inqueryProducts
                       join p in placeProducts
                       on q.RF_Id equals p.BarCode
                       select new FilterInquiryDetail
                       {
                           CategoryTitle = p.ProductCategory.CategoryTitle,
                           InquiryDetailId = q.Id,
                           PlaceTitle = p.Place.PlaceTitle,
                           PropertyTitle = p.ProductTitle,
                           PropertyId = p.Id,
                           RfId = p.BarCode,
                           IsExistInInquiey = IsExistInInquiey.FoundInInquiry
                       };

         list = products.ToList();

        //Not Found Products in inquiry
        var notFoundProducts_BarCode = placeProducts.Select(p => p.BarCode).Except(inqueryProducts.Select(p => p.RF_Id));
        foreach (var notFoundProduct_BarCode in notFoundProducts_BarCode)
        {
            var notFoundProduct = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.BarCode == notFoundProduct_BarCode)
                                    .Select(p => new FilterInquiryDetail()
                                    {
                                        CategoryTitle = p.ProductCategory.CategoryTitle,
                                        InquiryDetailId = filter.PropertyInquiryId,
                                        PlaceTitle = p.Place.PlaceTitle,
                                        PropertyTitle = p.ProductTitle,
                                        PropertyId = p.Id,
                                        RfId = p.BarCode,
                                        IsExistInInquiey = IsExistInInquiey.NotFoundInInquiry
                                    })
                                    .FirstOrDefault();

            if (notFoundProduct != null)
            {
                list.Add(notFoundProduct);
            }
        }

        //New Products From Another Places
        var NewProductsFromAnotherPlaces_BarCode = inqueryProducts.Select(p => p.RF_Id).Except(placeProducts.Select(p => p.BarCode));
        foreach (var NewProductsFromAnotherPlace_BarCode in NewProductsFromAnotherPlaces_BarCode)
        {
            var NewProducts = _context.Products
                                    .Include(p => p.ProductCategory)
                                    .Include(p => p.Place)
                                    .Where(p => !p.IsDelete &&
                                           p.BarCode == NewProductsFromAnotherPlace_BarCode)
                                    .Select(p => new FilterInquiryDetail()
                                    {
                                        CategoryTitle = p.ProductCategory.CategoryTitle,
                                        InquiryDetailId = filter.PropertyInquiryId,
                                        PlaceTitle = p.Place.PlaceTitle,
                                        PropertyTitle = p.ProductTitle,
                                        PropertyId = p.Id,
                                        RfId = p.BarCode,
                                        IsExistInInquiey = IsExistInInquiey.NewProductsFromAnotherPlaces
                                    })
                                    .FirstOrDefault();

            if (NewProducts != null)
            {
                list.Add(NewProducts);
            }
        }

        IQueryable<FilterInquiryDetail> query = list.AsQueryable<FilterInquiryDetail>();


        #region IsExistInInquiey_Inquiry

        switch (filter.IsExistInInquiey_Inquiry)
        {
            case IsExistInInquiey_Inquiry.All:
                break;

            case IsExistInInquiey_Inquiry.NotFoundInInquiry:
                query = query.Where(p => p.IsExistInInquiey == IsExistInInquiey.NotFoundInInquiry);
                break;

            case IsExistInInquiey_Inquiry.FoundInInquiry:
                query = query.Where(p => p.IsExistInInquiey == IsExistInInquiey.FoundInInquiry);
                break;

            case IsExistInInquiey_Inquiry.NewProductsFromAnotherPlaces:
                query = query.Where(p => p.IsExistInInquiey == IsExistInInquiey.NewProductsFromAnotherPlaces);
                break;
        }

        #endregion

        #region Filter

        if (!string.IsNullOrEmpty(filter.PlaceName))
        {
            query = query.Where(p => p.PlaceTitle.Contains(filter.PlaceName));
        }

        if (!string.IsNullOrEmpty(filter.CategoryName))
        {
            query = query.Where(p => p.CategoryTitle.Contains(filter.CategoryName));
        }

        #endregion

        return query.ToList();
    }
}
