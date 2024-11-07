using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Command;

public class AddNewExcelFileForPropertyInquiryCommand : 
    IRequest<AddNewExcelFileForPropertyInquiryResult>
{
    public ulong UserId { get; set; }

    public ulong PlaceId { get; set; }

    public AddNewExcelFileDTO Model { get; set; }
}

public class AddNewExcelFileForPropertyInquiryResult
{
    public AddNewExcelFileForPropertyInquiryResultState ResState { get; set; }

    public ulong? PropertyInquiryId { get; set; }
}

public enum AddNewExcelFileForPropertyInquiryResultState
{
    Success,
    Faild
}

