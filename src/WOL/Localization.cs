#define GERMAN

namespace System.Net
{
    internal static class Localization
    {
#if GERMAN
        public const string ArgumentExceptionInvalidMacAddressLength = "Die MAC-Adresse hat eine ungültige Länge.";
        public const string ArgumentExceptionInvalidPasswordLength = "Das Passwort hat eine ungültige Länge.";

        public const string FormatExceptionIsNotAValidMadAddress = " ist keine gültige MAC-Adresse.";
#else
        public const string ArgumentExceptionInvalidMacAddressLength = "Invalid MAC address length.";
        public const string ArgumentExceptionInvalidPasswordLength = "Invalid password length.";

        public const string ArgumentExceptionIsNotAValidMadAddress = " is not a valid MAC address.";
#endif
    }
}
