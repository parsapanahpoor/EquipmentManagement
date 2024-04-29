using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public record FilterPropertyInquiryDetailQueryHandler : IRequestHandler<FilterPropertyInquiryDetailQuery, List<FilterInquiryDetail>>
{
    #region Ctor

    private readonly IPropertyInquiryQueryRepository _propertyInquiryQueryRepository;

    public FilterPropertyInquiryDetailQueryHandler(IPropertyInquiryQueryRepository propertyInquiryQueryRepository)
    {
        _propertyInquiryQueryRepository = propertyInquiryQueryRepository;
    }

    #endregion

    public async Task<List<FilterInquiryDetail>> Handle(FilterPropertyInquiryDetailQuery request, CancellationToken cancellationToken = default)
    {
        var res = await _propertyInquiryQueryRepository.FilterInquiryDetail(request.FilterInquiryDetailDTO, cancellationToken);

        return res;
    }
}
