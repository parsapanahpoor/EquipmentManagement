using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.Entities.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Command;

public record AddNewExcelFileForPropertyInquiryCommandHandler : IRequestHandler<AddNewExcelFileForPropertyInquiryCommand, AddNewExcelFileForPropertyInquiryResult>
{
    #region Ctor

    private readonly IPropertyInquiryQueryRepository _propertyInquiryQueryRepository;
    private readonly IPropertyInquiryCommandRepository _propertyInquiryCommandRepository;
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IPlacesQueryRepository _placesQueryRepository;
    private readonly IProductLogCommandRepository _productLogCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddNewExcelFileForPropertyInquiryCommandHandler(IPropertyInquiryQueryRepository propertyInquiryQueryRepository,
                                                           IPropertyInquiryCommandRepository propertyInquiryCommandRepository,
                                                           IPlacesQueryRepository placesQueryRepository,
                                                           IProductLogCommandRepository productLogCommandRepository,
                                                           IUnitOfWork unitOfWork,
                                                           IProductQueryRepository productQueryRepository)
    {
        _propertyInquiryCommandRepository = propertyInquiryCommandRepository;
        _propertyInquiryQueryRepository = propertyInquiryQueryRepository;
        _placesQueryRepository = placesQueryRepository;
        _productQueryRepository = productQueryRepository;
        _productLogCommandRepository = productLogCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<AddNewExcelFileForPropertyInquiryResult> Handle(AddNewExcelFileForPropertyInquiryCommand request, CancellationToken cancellationToken)
    {
        #region Check Place

        if (!await _placesQueryRepository.IsExistAny_Place_ById(request.PlaceId, cancellationToken))
        {
            return new AddNewExcelFileForPropertyInquiryResult()
            {
                PropertyInquiryId = null,
                ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
            };
        }

        #endregion

        //Fill PropertyInquiry Model
        Domain.Entities.PropertyInquiry.PropertyInquiry propertyInquiry = new()
        {
            UserId = request.UserId,
            PlaceId = request.PlaceId,
        };

        //Add File To Server 
        if (request.Model.ExcelFile != null)
        {
            var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.Model.ExcelFile.FileName);
            //await request.Model.ExcelFile.AddFilesToServer(imageName, FilePaths.PropertyInquiryExcelFile);
            propertyInquiry.ExcelFile = imageName;
        }

        try
        {
            #region Find From internet

            IFormFile file = request.Model.ExcelFile;
            string folderName = "UploadExcel";
            string newPath = FilePaths.PropertyInquiryExcelFile;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                //Add Iqnuiry 
                await _propertyInquiryCommandRepository.AddAsync(propertyInquiry, cancellationToken);
                await _unitOfWork.SaveChangesAsync();

                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats   
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format   
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook    
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row 
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    }

                    List<PropertyInquiryDetail> detail = new();

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File 
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        if (!string.IsNullOrEmpty(row.Cells[2].ToString()))
                        {
                            var product = await _productQueryRepository.GetProduct_Product_ByRfId(row.Cells[2].ToString(), cancellationToken);
                            if (product is not null)
                            {
                                detail.Add(new PropertyInquiryDetail()
                                {
                                    RF_Id = row.Cells[2].ToString(),
                                    PropertyInquiryId = propertyInquiry.Id
                                });

                                //Add Product Log
                                await _productLogCommandRepository.AddProductLog(new Domain.DTO.SiteSide.ProductLog.CreateProductLogDto
                                    (
                                        UserId : request.UserId , 
                                        ProductId : product.Id , 
                                        PlaceId : request.PlaceId , 
                                        Description : "یافت شده در استعلام گردش اموال"
                                    ),cancellationToken);
                            }
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
            }
            else
            {
                return new AddNewExcelFileForPropertyInquiryResult()
                {
                    PropertyInquiryId = null,
                    ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
                };
            }

            #endregion
        }
        catch (Exception)
        {
            return new AddNewExcelFileForPropertyInquiryResult()
            {
                PropertyInquiryId = null,
                ResState = AddNewExcelFileForPropertyInquiryResultState.Faild
            };
        }
    }
}
