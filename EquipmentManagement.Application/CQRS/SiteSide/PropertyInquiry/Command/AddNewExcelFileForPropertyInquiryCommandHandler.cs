
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.Entities.PropertyInquiry;
using EquipmentManagement.Domain.Entities.Users;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using OfficeOpenXml;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Command;

public record AddNewExcelFileForPropertyInquiryCommandHandler : IRequestHandler<AddNewExcelFileForPropertyInquiryCommand, AddNewExcelFileForPropertyInquiryResult>
{
    #region Ctor

    private readonly IPropertyInquiryQueryRepository _propertyInquiryQueryRepository;
    private readonly IPropertyInquiryCommandRepository _propertyInquiryCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddNewExcelFileForPropertyInquiryCommandHandler(IPropertyInquiryQueryRepository propertyInquiryQueryRepository,
                                                           IPropertyInquiryCommandRepository propertyInquiryCommandRepository,
                                                           IUnitOfWork unitOfWork)
    {
        _propertyInquiryCommandRepository = propertyInquiryCommandRepository;
        _propertyInquiryQueryRepository = propertyInquiryQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<AddNewExcelFileForPropertyInquiryResult> Handle(AddNewExcelFileForPropertyInquiryCommand request, CancellationToken cancellationToken)
    {
        #region Fill Order Model

        Domain.Entities.PropertyInquiry.PropertyInquiry propertyInquiry = new()
        {
            UserId = request.UserId,
        };

        #endregion

        #region Add File To Server

        if (request.Model.ExcelFile != null)
        {
            var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.Model.ExcelFile.FileName);
            await request.Model.ExcelFile.AddFilesToServer(imageName, FilePaths.PropertyInquiryExcelFile);
            propertyInquiry.ExcelFile = imageName;
        }

        #endregion

        #region Fill Inquiry Detail 

        using (var stream = new MemoryStream())
        {
            if (request.Model.ExcelFile == null) return new AddNewExcelFileForPropertyInquiryResult()
            {
                PropertyInquiryId = null,
                ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
            };

            await request.Model.ExcelFile.CopyToAsync(stream);

            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                var rowCount = workSheet.Dimension.Rows;

                //Add Iqnuiry 
                await _propertyInquiryCommandRepository.AddAsync(propertyInquiry, cancellationToken);
                await _unitOfWork.SaveChangesAsync();

                if (rowCount >= 1)
                {
                    List<PropertyInquiryDetail> detail = new();

                    for (int row = 1; row <= rowCount; row++)
                    {
                        if (!string.IsNullOrEmpty(workSheet.Cells[row, 1].Value.ToString()))
                        {
                            detail.Add(new PropertyInquiryDetail()
                            {
                                RF_Id = workSheet.Cells[row, 1].Value.ToString(),
                                PropertyInquiryId = propertyInquiry.Id
                            });
                        }
                    }

                    //Add Inquiry Detail
                    await _propertyInquiryCommandRepository.AddRangeInquiryDetailAsync(detail, cancellationToken);
                    await _unitOfWork.SaveChangesAsync();

                    return new AddNewExcelFileForPropertyInquiryResult()
                    {
                        PropertyInquiryId = propertyInquiry.Id,
                        ResState = AddNewExcelFileForPropertyInquiryResultState.Success
                    };
                }
                else
                {
                    return new AddNewExcelFileForPropertyInquiryResult()
                    {
                        PropertyInquiryId = null,
                        ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
                    };
                }
            }
        }

        #endregion

        return new AddNewExcelFileForPropertyInquiryResult()
        {
            PropertyInquiryId = null,
            ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
        };
    }
}
