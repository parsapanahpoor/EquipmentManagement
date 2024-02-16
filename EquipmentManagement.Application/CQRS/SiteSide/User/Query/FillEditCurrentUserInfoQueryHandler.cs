using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Domain.Entities.Users;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public record FillEditCurrentUserInfoQueryHandler : IRequestHandler<FillEditCurrentUserInfoQuery, EditUserInfoDTO>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public FillEditCurrentUserInfoQueryHandler(IUserQueryRepository userQueryRepository)
    {
            _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<EditUserInfoDTO?> Handle(FillEditCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        #region Get User By Id

        var user = await _userQueryRepository.GetByIdAsync(cancellationToken , request.userId);
        if (user == null) return null;

        #endregion

        #region Fill View Model

        EditUserInfoDTO model = new EditUserInfoDTO()
        {
            Mobile = user.Mobile,
            UserId = user.Id,
            AvatarName = user.Avatar,
            username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        if (!string.IsNullOrEmpty(user.NationalId))
        {
            model.NationalId = user.NationalId;
        }

        #endregion

        return model;
    }
}
