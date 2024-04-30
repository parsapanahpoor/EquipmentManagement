using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

namespace EquipmentManagement.Domain.IRepositories.PropertyInquiry;

public interface IPropertyInquiryQueryRepository
{
    Task<List<PropertyInquiryDTO>> FilterSystemPropertyInquiries(FilterSystemPropertyInquiriesDTO filter,
                                                                         CancellationToken cancellation);

    Task<List<FilterInquiryDetail>> FilterInquiryDetail(FilterInquiryDetailDTO filter,
                                                                  CancellationToken cancellation);

    Task<FilterPropertiesInquiry_BadgesCountDTO> FilterInquiryDetail_BadgesCount(ulong placeId,
                                                                                 ulong inquiryId,
                                                                                 CancellationToken cancellation = default);
}
