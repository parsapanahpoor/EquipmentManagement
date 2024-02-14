using EquipmentManagement.Domain.DTO.SiteSide.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public class EdirUserCommand : IRequest<EditUserResult>
{
    #region properties

    public ulong Id { get; set; }

    [DisplayName("Username")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [MaxLength(200)]
    public string Username { get; set; }

    [MaxLength(200)]
    [DisplayName("Password")]
    public string? Password { get; set; }

    [MaxLength(200)]
    [DisplayName("Mobile")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string Mobile { get; set; }

    [DisplayName("Avatar")]
    public string? Avatar { get; set; }

    [Display(Name = "انتخاب نقش های کاربر")]
    public List<ulong>? UserRoles { get; set; }

    public bool IsActive { get; set; }

    public IFormFile avatar{ get; set; }

    #endregion
}
