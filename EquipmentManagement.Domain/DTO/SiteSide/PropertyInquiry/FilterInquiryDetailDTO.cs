using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.PropertyInquiry;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

public class FilterInquiryDetailDTO
{
    #region properties

    public ulong PropertyInquiryId { get; set; }

    public ulong PlaceId { get; set; }

    public string? PlaceName{ get; set; }

    public string? CategoryName { get; set; }

    public IsExistInInquiey_Inquiry IsExistInInquiey_Inquiry { get; set; } 

    #endregion
}

public enum IsExistInInquiey_Inquiry
{
    [Display(Name = "همه")]All,
    [Display(Name = "محصولات این مکان که در استعلام یافت شده است.")]FoundInInquiry,
    [Display(Name = "محصولات این مکان که در استعلام یافت نشده است.")]NotFoundInInquiry,
    [Display(Name = "محصولات یافت شده اما نامتعلق به این مکان.")]NewProductsFromAnotherPlaces
}

public enum IsExistInInquiey
{
    FoundInInquiry,
    NotFoundInInquiry,
    NewProductsFromAnotherPlaces
}

public class FilterInquiryDetail
{
    #region properties

    public ulong InquiryDetailId { get; set; }

    public ulong PropertyId { get; set; }

    public string PropertyTitle { get; set; }

    public string RfId { get; set; }

    public string CategoryTitle { get; set; }

    public string PlaceTitle { get; set; }

    public IsExistInInquiey IsExistInInquiey { get; set; }

    #endregion
}

public record FilterPropertiesInquiry_BadgesCountDTO
{
    #region properties

    public int FoundInInquiry { get; set; }

    public int NotFoundInInquiry { get; set; }

    public int NewProductsFromAnotherPlaces { get; set; }

    #endregion
}