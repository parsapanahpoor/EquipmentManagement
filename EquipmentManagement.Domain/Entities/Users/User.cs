using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.Entities.Users;

public class User : BaseEntities<ulong>
{
    #region properties

    [MaxLength(50)]
    public string? Username { get; set; }

    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(30)]
    public string? NationalId { get; set; }

    [MaxLength(30)]
    public string? Mobile { get; set; }

    [MaxLength(50)]
    public string Password { get; set; }

    public string? Avatar { get; set; }

    [MaxLength(100)]
    public string MobileActivationCode { get; set; }

    public bool IsAdmin { get; set; } = false;

    public bool IsActive { get; set; } = false;

    public DateTime? ExpireMobileSMSDateTime { get; set; }

    #endregion

    public ICollection<UserSelectedOrganizationChartEntity>? UserSelectedOrganizationChartEntities { get; set; }
    public ICollection<RepairRequest>? RepairRequests { get; set; }
}
