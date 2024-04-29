using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public record FilterPropertyInquiryDetail_BadgesCountQueryHandler : IRequestHandler<FilterPropertyInquiryDetail_BadgesCountQuery, FilterPropertiesInquiry_BadgesCountDTO>
{
    #region Ctor

    private readonly IPropertyInquiryQueryRepository _propertyInquiryQueryRepository;

    public FilterPropertyInquiryDetail_BadgesCountQueryHandler(IPropertyInquiryQueryRepository propertyInquiryQueryRepository)
    {
        _propertyInquiryQueryRepository = propertyInquiryQueryRepository;
    }

    #endregion

    public async Task<FilterPropertiesInquiry_BadgesCountDTO> Handle(FilterPropertyInquiryDetail_BadgesCountQuery request, CancellationToken cancellationToken)
    {
        return await _propertyInquiryQueryRepository.FilterInquiryDetail_BadgesCount(request.PlaceId , request.InquiryId , cancellationToken);
    }
}
