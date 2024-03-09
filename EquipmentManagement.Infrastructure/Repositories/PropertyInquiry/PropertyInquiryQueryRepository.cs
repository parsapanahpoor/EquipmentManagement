using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using System.Globalization;

namespace EquipmentManagement.Infrastructure.Repositories.PropertyInquiry;

public class PropertyInquiryQueryRepository : QueryGenericRepository<Domain.Entities.PropertyInquiry.PropertyInquiry>  , IPropertyInquiryQueryRepository
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
        var query = _context.PropertyInquiries
        .Where(p => !p.IsDelete )
        .OrderByDescending(s => s.CreateDate)
        .AsQueryable();

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
}
