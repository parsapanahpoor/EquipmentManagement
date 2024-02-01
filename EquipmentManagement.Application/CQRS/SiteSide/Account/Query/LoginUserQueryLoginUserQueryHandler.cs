using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.Account;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Query;

public record LoginUserQueryLoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserResult>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public LoginUserQueryLoginUserQueryHandler(IUserQueryRepository userQueryRepository)
    {
            _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        if (!await _userQueryRepository.IsMobileExist(request.Mobile.Trim().ToLower() , cancellationToken))
        {
            return LoginUserResult.MobileExist;
        }

        if (!await _userQueryRepository.IsUserActive(request.Mobile.Trim().ToLower(), cancellationToken))
        {
            return LoginUserResult.UserNotActive;
        }

        if (!await _userQueryRepository.IsPasswordValid(request.Mobile.Trim().ToLower(),
                  PasswordHelper.EncodePasswordMd5(request.Password) , cancellationToken))
        {
            return LoginUserResult.WrongPassword;
        }

        return LoginUserResult.Success;
    }
}
