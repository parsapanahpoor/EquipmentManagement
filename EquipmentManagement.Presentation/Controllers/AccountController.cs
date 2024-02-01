#region Usings

using EquipmentManagement.Application.CQRS.SiteSide.Account.Command;
using EquipmentManagement.Domain.DTO.SiteSide.Account;
using EquipmentManagement.Web.HttpManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EquipmentManagement.Application.CQRS.SiteSide.Account.Query;

namespace EquipmentManagement.Presentation.Controllers;

#endregion

public class AccountController : SiteBaseController
{
    #region Ctor



    #endregion

    #region Register

    [HttpGet("register"), RedirectIfLoggedInActionFilter]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register"), ValidateAntiForgeryToken, RedirectIfLoggedInActionFilter]
    public async Task<IActionResult> Register(RegisterUserDTO model, CancellationToken cancellation)
    {
        if (ModelState.IsValid)
        {
            #region Mapping Fields

            RegisterUserCommand command = new RegisterUserCommand()
            {
                Mobile = model.Mobile,
                Password = model.Password,
            };

            #endregion

            var res = await Mediator.Send(command, cancellation);
            switch (res)
            {
                case RegisterUserResult.EmailExist:
                    TempData[ErrorMessage] = "ایمیل وارد شده در سامانه موجود می باشد.";
                    break;

                case RegisterUserResult.MobileExist:
                    TempData[ErrorMessage] = "کاربری با موبایل وارد شده در سامانه موجود است.";
                    break;

                case RegisterUserResult.Success:
                    TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                    return RedirectToAction(nameof(Login));

                default:
                    TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                    break;
            }

            return View(model);
        }

        return View(model);
    }

    #endregion

    #region Login 

    [HttpGet("login"), RedirectIfLoggedInActionFilter]
    public IActionResult Login(string? returnUrl = "/")
    {
        return View();
    }

    [HttpPost("login"), ValidateAntiForgeryToken, RedirectIfLoggedInActionFilter]
    public async Task<IActionResult> Login(LoginUserDTO model,
                                           CancellationToken cancellation,
                                           string? returnUrl = "/")
    {
        if (ModelState.IsValid)
        {
            #region Check User 

            LoginUserQuery query = new LoginUserQuery()
            {
                Mobile = model.Mobile,
                Password = model.Password,
            };

            var res = await Mediator.Send(query, cancellation);
            if (res != LoginUserResult.Success)
            {
                switch (res)
                {
                    case LoginUserResult.Success:
                        break;

                    case LoginUserResult.UserNotActive:
                        TempData[ErrorMessage] = "شما دسترسی برای ورود به سایت ندارید.";
                        break;

                    case LoginUserResult.WrongPassword:
                        TempData[ErrorMessage] = "کلمه ی عبور وارد شده صحیح نمی باشد.";
                        break;

                    case LoginUserResult.MobileExist:
                        TempData[ErrorMessage] = "موبایل وارد شده در سایت موجود نمی باشد.";
                        break;

                    default:
                        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                        break;
                }

                return View(model);
            }

            #endregion

            #region Login User Into Identity

            var user = await Mediator.Send(new GetUserByMobileQuery { Mobile = model.Mobile }, cancellation);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.MobilePhone, user.Mobile),
                new (ClaimTypes.Name, user.Username),
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimIdentity);

            var authProps = new AuthenticationProperties();
            authProps.IsPersistent = true;

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);

            return RedirectToAction("Index", "Home");

            #endregion
        }

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(model);
    }

    #endregion

    #region Logout

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }

    #endregion
}
