
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public record DeleteUserCommandHanlder : IRequestHandler<DeleteUserCommand, bool>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHanlder(IUserQueryRepository userQueryRepository,
                                    IUserCommandRepository userCommandRepository,
                                    IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //Get User By Id 
        var userOldInfos = await _userQueryRepository.GetByIdAsync(cancellationToken, request.userId);
        if (userOldInfos == null) return false;

        userOldInfos.IsDelete = true;

        _userCommandRepository.Update(userOldInfos);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
