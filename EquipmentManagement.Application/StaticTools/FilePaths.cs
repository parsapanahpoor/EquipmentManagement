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

    #region Organization Request Document File 

    public static readonly string OrganizationRequestDocumentPath = "/content/images/OrganizationRequestDocument/main/";
    public static readonly string OrganizationRequestDocumentPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/OrganizationRequestDocument/main/");

    public static readonly string OrganizationRequestDocumentPathThumb = "/content/images/OrganizationRequestDocument/thumb/";
    public static readonly string OrganizationRequestDocumentPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/OrganizationRequestDocument/thumb/");

    public static readonly string OrganizationRequestDocumentAvatar = "/content/images/NoImage.jpg";

    #endregion

    #region Property Inquiry

    public static readonly string PropertyInquiryExcelFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/PropertyInquiryExcelFile/");

    #endregion

    #region InvoiceImage

    public static readonly string InvoiceImagePath = "/content/images/InvoiceImage/main/";
    public static readonly string InvoiceImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/InvoiceImage/main/");

    public static readonly string InvoiceImagePathThumb = "/content/images/InvoiceImage/thumb/";
    public static readonly string InvoiceImagePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/InvoiceImage/thumb/");

    #endregion
}
