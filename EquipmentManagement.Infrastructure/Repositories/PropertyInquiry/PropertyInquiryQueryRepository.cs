using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

    public async Task<FilterSystemPropertyInquiriesDTO> FilterSystemPropertyInquiries(FilterSystemPropertyInquiriesDTO filter,
                                                                                      CancellationToken cancellation)
    {
        var inquiries = _context.PropertyInquiries
        .Where(p => !p.IsDelete)
        .OrderByDescending(s => s.CreateDate)
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
                        InquiryId = q.Id
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

        #endregion

        await filter.Paging(query);

        return filter;
    }

    public async Task<FilterInquiryDetailDTO> FilterInquiryDetail(FilterInquiryDetailDTO filter,
                                                                  CancellationToken cancellation)
    {
        var query = from q in _context.PropertyInquiryDetails
                                      .AsNoTracking()
                                      .Where(p => !p.IsDelete &&
                                             p.PropertyInquiryId == filter.PropertyInquiryId)
                                      .AsQueryable()

                    join p in _context.Products
                                      .Include(p => p.ProductCategory)
                                      .Include(p => p.Place)
                                      .Where(p => !p.IsDelete)
                                      .AsQueryable()
                    on q.RF_Id equals p.BarCode
                    select new FilterInquiryDetail
                    {
                        CategoryTitle = p.ProductCategory.CategoryTitle,
                        InquiryDetailId = q.Id,
                        PlaceTitle = p.Place.PlaceTitle,
                        PropertyTitle = p.ProductTitle,
                        PropertyId = p.Id,
                        RfId = p.BarCode
                    };

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

        await filter.Paging(query);

        return filter;
    }
}
