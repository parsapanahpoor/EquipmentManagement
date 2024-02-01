using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EquipmentManagement.Domain.DTO.SiteSide.Account;

public record RegisterUserDTO
{
    #region Properties

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("کلمه ی عبور")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("تکرار کلمه ی عبور")]
    [Compare("Password", ErrorMessage = "Password And Re Password Does Not Match")]
    [DataType(DataType.Password)]
    public string RePassword { get; set; }

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("موبایل")]
    public string Mobile { get; set; }

    #endregion
}

public enum RegisterUserResult
{
    EmailExist, MobileExist, Success
}