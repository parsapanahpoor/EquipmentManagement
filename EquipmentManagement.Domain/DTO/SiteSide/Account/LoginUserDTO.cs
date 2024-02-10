using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EquipmentManagement.Domain.DTO.SiteSide.Account;

public record LoginUserDTO
{
    #region Properties

    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("موبایل")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }

    #endregion
}

public enum LoginUserResult
{
    Success, UserNotActive, WrongPassword, EmailNotFound , MobileExist
}
