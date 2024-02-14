using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public record EdirUserCommandHandler : IRequestHandler<EdirUserCommand, EditUserResult>
{
    #region Ctor

    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EdirUserCommandHandler(IUserCommandRepository userCommandRepository ,
                                  IUserQueryRepository userQueryRepository ,
                                  IRoleCommandRepository roleCommandRepository ,
                                  IRoleQueryRepository roleQueryRepository ,
                                  IUnitOfWork unitOfWork) 
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<EditUserResult> Handle(EdirUserCommand request, CancellationToken cancellationToken)
    {
        //Get User By Id 
        var userOldInfos = await _userQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (userOldInfos == null) return EditUserResult.Error;

        //Checkind incomin mobile 
        if (await _userQueryRepository.IsMobileExist(request.Mobile, cancellationToken) && request.Mobile != userOldInfos.Mobile)
        {
            return EditUserResult.DuplicateMobileNumber;
        }

        if (userOldInfos != null)
        {
            userOldInfos.Username = request.Username;
            userOldInfos.Mobile = request.Mobile.SanitizeText();
            userOldInfos.IsActive = request.IsActive;

            if (request.Password != null)
            {
                userOldInfos.Password = request.Password.SanitizeText();
            }

            #region User Avatar

            if (request.avatar != null && request.avatar.IsImage())
            {
                if (!string.IsNullOrEmpty(userOldInfos.Avatar))
                {
                    userOldInfos.Avatar.DeleteImage(FilePaths.UserAvatarPathServer, FilePaths.UserAvatarPathThumbServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.avatar.FileName);
                request.avatar.AddImageToServer(imageName, FilePaths.UserAvatarPathServer, 270, 270, FilePaths.UserAvatarPathThumbServer);
                userOldInfos.Avatar = imageName;
            }

            #endregion

            _userCommandRepository.Update(userOldInfos);

            #region Delete User Roles

            await _roleCommandRepository.RemoveUserRolesByUserId(request.Id, cancellationToken);

            #endregion

            #region Add User Roles

            if (request.UserRoles != null && request.UserRoles.Any())
            {
                foreach (var roleId in request.UserRoles)
                {
                    var userRole = new UserRole()
                    {
                        RoleId = roleId,
                        UserId = request.Id
                    };

                    await _roleCommandRepository.AddUserSelectedRole(userRole, cancellationToken);
                }
            }

            #endregion

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return EditUserResult.Success;
        }

        return EditUserResult.Error;
    }
}
