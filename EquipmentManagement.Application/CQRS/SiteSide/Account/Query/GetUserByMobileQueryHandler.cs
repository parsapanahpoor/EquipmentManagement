using EquipmentManagement.Domain.Entities.Users;
using EquipmentManagement.Domain.IRepositories.User;
namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Query;

public record GetUserByMobileQueryHandler : IRequestHandler<GetUserByMobileQuery, Domain.Entities.Users.User>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public GetUserByMobileQueryHandler(IUserQueryRepository userQueryRepository)
    {
            _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<User?> Handle(GetUserByMobileQuery request, CancellationToken cancellationToken)
    {
        return await _userQueryRepository.GetUserByMobile(request.Mobile.Trim().ToLower(), cancellationToken);
    }
}
