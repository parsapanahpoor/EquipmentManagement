using EquipmentManagement.Domain.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FilterProductDTO : BasePaging<Entities.Product.Product>
{
    public string? ProductTitle { get; set; }

    public string? Brand { get; set; }

    public string? PlaceTitle { get; set; }

    public string? CategoryTitle { get; set; }

    public string? RepostiroyCode { get; set; }

    public ulong?  PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string? BarCode { get; set; }

    public PlaceSorting? PlaceSorting { get; set; }
    public ProductTitleSorting? ProductTitleSorting { get; set; }
    public BrandSorting? BrandSorting { get; set; }
    public CategorySorting? CategorySorting { get; set; }
    public RepostiroyCodeSorting? RepostiroyCodeSorting { get; set; }
    public BarCodeSorting? BarCodeSorting { get; set; }
    public ProductIdSorting? ProductIdSorting { get; set; }
    public CreateDateSorting? CreateDateSorting { get; set; }
}

public record FilterProductForExcelFilesDTO
{
    public ulong Id { get; set; }

    public string? ProductTitle { get; init; }

    public string? Brand { get; set; }


    public string? PlaceTitle { get; init; }

    public string? CategoryTitle { get; init; }

    public string? RepostiroyCode { get; set; }

    public string? Description { get; set; }

    public ulong? PlaceId { get; init; }

    public ulong? CategoryId { get; init; }

    public string? BarCode { get; init; }

    public DateTime CreateDate { get; set; }
}

public enum PlaceSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum ProductTitleSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum BrandSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum CategorySorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum RepostiroyCodeSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum BarCodeSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum ProductIdSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}

public enum CreateDateSorting
{
    [Display(Name = "همه")] All,
    [Display(Name = "صعودی")] Ascending,
    [Display(Name = "نزولی")] Descending,
}