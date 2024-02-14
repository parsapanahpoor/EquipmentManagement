using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.Domain.DTO.SiteSide.Role;

public record EditRoleDTO 
{
    #region properties

    public string Title { get; set; }

    public string RoleUniqueName { get; set; }

    public List<ulong>? Permissions { get; set; }

    public ulong Id { get; set; }

    #endregion
}

public enum EditRoleResult
{
    Success,
    RoleNotFound,
    UniqueNameExists
}