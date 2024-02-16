using EquipmentManagement.Domain.DTO.SiteSide.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public class ChangeUserPasswordDTOCommand : IRequest<ChangeUserPasswordResponse>
{
    public ulong UserId { get; set; }

    [DisplayName("Current Password")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [DisplayName("New Password")]
    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [DisplayName("Re New Password")]
    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Compare("NewPassword", ErrorMessage = "Password And Re Password Does Not Match")]
    [DataType(DataType.Password)]
    public string ReNewPassword { get; set; }
}
