using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.User;

public record EditUserInfoDTO
{
    #region properties

    public ulong UserId { get; set; }

    [Display(Name = "User Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [MaxLength(150, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    public string username { get; set; }

    public string? AvatarName { get; set; }

    [Display(Name = "Mobile")]
    [MaxLength(20, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "The information entered is not valid.")]
    public string? Mobile { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [MaxLength(150, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    public string FirstName { get; set; }

    [Display(Name = "LastName")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [MaxLength(150, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    public string LastName { get; set; }

    [Display(Name = "NationalId")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "The information entered is not valid.")]
    public string NationalId { get; set; }

    #endregion
}

public enum UserPanelEditUserInfoResult
{
    NotValidImage,
    UserNotFound,
    Success,
    NationalId,
    NotValidNationalId
}