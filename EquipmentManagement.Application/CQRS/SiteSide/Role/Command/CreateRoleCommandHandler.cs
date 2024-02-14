
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.Role;
using EquipmentManagement.Domain.IRepositories.Role;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Command;

public record CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
{
    #region Ctor 

    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IRoleCommandRepository roleCommandRepository,
                                    IRoleQueryRepository roleQueryRepository , 
                                    IUnitOfWork unitOfWork)
    {
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        #region Check For Title Comming Being Unique

        if (await _roleQueryRepository.IsExistAnyRoleByRoleUniqueTitle(request.RoleUniqueName , cancellationToken))
        {
            return false;
        }

        #endregion

        #region Add Role To The Data Base

        Domain.Entities.Account.Role role = new Domain.Entities.Account.Role()
        {
            RoleUniqueName = request.RoleUniqueName,
            Title = request.Title,
        };

        await _roleCommandRepository.AddAsync(role , cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        // Add permissions
        if (request.Permissions != null && request.Permissions.Any())
        {
            foreach (var permissionId in request.Permissions)
            {
                var rolePermission = new RolePermission
                {
                    PermissionId = permissionId,
                    RoleId = role.Id
                };

                await _roleCommandRepository.AddPermissionToRole(rolePermission);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        #endregion

        return true;
    }
}
