using EquipmentManagement.Domain.Entities.OperatorLogger;

namespace EquipmentManagement.Domain.IRepositories.PropertyInquiry;

public interface IPropertyInquiryCommandRepository
{
    Task AddAsync(Domain.Entities.PropertyInquiry.PropertyInquiry inquiry, CancellationToken cancellationToken);

    Task AddRangeAsync(IEnumerable<Entities.PropertyInquiry.PropertyInquiry> inquiries, CancellationToken cancellationToken);

    Task AddRangeInquiryDetailAsync(IEnumerable<Entities.PropertyInquiry.PropertyInquiryDetail> inquiryDetails, CancellationToken cancellationToken);

    Task AddAsync(OperatorExcelUploadLogger excelUploadLogger, CancellationToken cancellationToken);
}
