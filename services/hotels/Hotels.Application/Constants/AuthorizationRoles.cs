namespace Hotels.Application.Constants;

public static class AuthorizationRoles
{
    public const string Customer = "Customer";
    public const string HotelAdmin = "HotelAdmin";
    public const string SuperAdmin = "SuperAdmin";
    public const string HotelAdminOrSuperAdmin = HotelAdmin + "," + SuperAdmin;

}