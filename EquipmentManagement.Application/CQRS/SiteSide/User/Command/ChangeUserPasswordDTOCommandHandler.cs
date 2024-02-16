using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Domain.Entities.Users;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public record ChangeUserPasswordDTOCommandHandler : IRequestHandler<ChangeUserPasswordDTOCommand, ChangeUserPasswordResponse>
{
    #region Ctor

    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserPasswordDTOCommandHandler(IUserCommandRepository userCommandRepository ,
                                               IUserQueryRepository userQueryRepository ,
                                               IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<ChangeUserPasswordResponse> Handle(ChangeUserPasswordDTOCommand request, CancellationToken cancellationToken)
    {
        #region Get User By Id

        var user = await _userQueryRepository.GetByIdAsync(cancellationToken , request.UserId);
        if (user == null) return ChangeUserPasswordResponse.UserNotFound;

        #endregion

        #region Change Password

        if (user.Password != PasswordHelper.EncodePasswordMd5(request.CurrentPassword))
        {
            return ChangeUserPasswordResponse.WrongPassword;
        }

        user.Password = PasswordHelper.EncodePasswordMd5(request.NewPassword);

        _userCommandRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        return ChangeUserPasswordResponse.Success;
    }
}
