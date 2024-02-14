using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record EditRoleQueryHandler : IRequestHandler<EditRoleQuery, EditRoleDTO>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public EditRoleQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<EditRoleDTO?> Handle(EditRoleQuery request, CancellationToken cancellationToken)
    {
        //get Role By Role 
        var role = await _roleQueryRepository.GetByIdAsync(cancellationToken, request.roleId);
        if (role == null) return null;

        return new EditRoleDTO()
        {
            Id = role.Id,
            RoleUniqueName = role.RoleUniqueName,
            Title = role.Title,
            Permissions = await _roleQueryRepository.GetRolePermissionsIdByRoleId(role.Id , cancellationToken)
        };
    }
}
