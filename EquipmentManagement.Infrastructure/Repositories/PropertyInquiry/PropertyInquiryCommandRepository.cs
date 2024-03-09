using EquipmentManagement.Domain.Entities.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
namespace EquipmentManagement.Infrastructure.Repositories.PropertyInquiry;

public class PropertyInquiryCommandRepository : CommandGenericRepository<Domain.Entities.PropertyInquiry.PropertyInquiry>, IPropertyInquiryCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public PropertyInquiryCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task AddRangeInquiryDetailAsync(IEnumerable<PropertyInquiryDetail> inquiryDetails, CancellationToken cancellationToken)
    {
        await _context.PropertyInquiryDetails.AddRangeAsync(inquiryDetails);
    }
}
