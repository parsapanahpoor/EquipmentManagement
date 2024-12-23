﻿using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public record FilterSystemPropertyInquiriesQueryHandler : IRequestHandler<FilterSystemPropertyInquiriesQuery, List<PropertyInquiryDTO>>
{
    #region Ctor

    private readonly IPropertyInquiryQueryRepository _propertyInquiryQueryRepository;

    public FilterSystemPropertyInquiriesQueryHandler(IPropertyInquiryQueryRepository propertyInquiryQueryRepository)
    {
        _propertyInquiryQueryRepository = propertyInquiryQueryRepository;
    }

    #endregion

    public async Task<List<PropertyInquiryDTO>> Handle(FilterSystemPropertyInquiriesQuery request, CancellationToken cancellationToken)
    {
        return await _propertyInquiryQueryRepository.FilterSystemPropertyInquiries(request.Filter , cancellationToken);
    }
}
