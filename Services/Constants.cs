namespace CCC_Rugby_Web.Services
{
    public class Constants
    {
        public const string TokenCookieName = "authToken";

        public enum ArchivoType
        {
            Other, // 0
            Image, // 1
            Video, // 2
            Audio, // 3
            Document // 4
        }

        public const string AdminRoleCode = "admin";
    }
}
