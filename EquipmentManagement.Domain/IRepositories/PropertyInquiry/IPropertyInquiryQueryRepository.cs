using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

namespace EquipmentManagement.Domain.IRepositories.PropertyInquiry;

public interface IPropertyInquiryQueryRepository
{
    Task<FilterSystemPropertyInquiriesDTO> FilterSystemPropertyInquiries(FilterSystemPropertyInquiriesDTO filter,
                                                                         CancellationToken cancellation);

    Task<FilterInquiryDetailDTO> FilterInquiryDetail(FilterInquiryDetailDTO filter,
                                                                  CancellationToken cancellation);
}
