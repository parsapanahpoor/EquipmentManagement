using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public class FilterSystemPropertyInquiriesQuery : IRequest<List<PropertyInquiryDTO>>
{
    public FilterSystemPropertyInquiriesDTO Filter { get; set; }
}
