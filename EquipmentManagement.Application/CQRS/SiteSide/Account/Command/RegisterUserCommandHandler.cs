using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.DTO.SiteSide.Account;
using EquipmentManagement.Domain.Entities.Users;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Command;

public record RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserQueryRepository userQueryRepository,
                                      IUserCommandRepository userCommandRepository , 
                                      IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        #region Check Is Exist User By Mobile

        var userExist = await _userQueryRepository.IsMobileExist(request.Mobile.Trim().ToLower(), cancellationToken);
        if (userExist) return RegisterUserResult.MobileExist;

        #endregion

        #region Add User 

        User user = new User()
        {
            Username = request.Mobile.Trim().ToLower().SanitizeText(),
            Mobile = request.Mobile.Trim().ToLower().SanitizeText(),
            ExpireMobileSMSDateTime = DateTime.Now,
            IsActive = true,
            Password = PasswordHelper.EncodePasswordMd5(request.Password),
            MobileActivationCode = new Random().Next(10000, 999999).ToString(),
        };

        await _userCommandRepository.AddAsync(user , cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        return RegisterUserResult.Success;
    }
}
