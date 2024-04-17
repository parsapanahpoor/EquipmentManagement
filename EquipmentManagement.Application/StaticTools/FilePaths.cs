namespace EquipmentManagement.Application.StaticTools;

public static class FilePaths
{
    #region Site

    public static string SiteFarsiName = "سامانه ی هوشمند مدیریت ";
    public static string SiteAddress = "https://shafaprop.ir";
    public static string merchant = "123456789";

    #endregion

    #region UserAvatar

    public static readonly string UserAvatarPath = "/content/images/user/main/";
    public static readonly string UserAvatarPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

    public static readonly string UserAvatarPathThumb = "/content/images/user/thumb/";
    public static readonly string UserAvatarPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

    public static readonly string DefaultUserAvatar = "/content/images/user/DefaultAvatar.png";

    #endregion

    #region Property Inquiry

    public static readonly string PropertyInquiryExcelFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/PropertyInquiryExcelFile/");

    #endregion
}
