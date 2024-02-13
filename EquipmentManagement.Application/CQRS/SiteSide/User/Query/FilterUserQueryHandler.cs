using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public record FilterUserQueryHandler : IRequestHandler<FilterUserQuery, FilterUsersDTO>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public FilterUserQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<FilterUsersDTO> Handle(FilterUserQuery request, CancellationToken cancellationToken)
    {
        FilterUsersDTO model = new()
        {
            Username = request.Username,
            Mobile = request.Mobile,
        };

        return await _userQueryRepository.FilterUsers(model , cancellationToken);
    }
}
