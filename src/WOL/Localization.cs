#define GERMAN

namespace System.Net
{
    internal static class Localization
    {
#if GERMAN
        public const string ObsoleteMacAddress = "Die MacAddress-Klasse ist veraltet. Verwenden Sie die Klasse System.Net.NetworkInformation.PhysicalAddress.";

        public const string ArgumentExceptionInvalidMacAddressLength = "Die MAC-Adresse hat eine ungültige Länge.";
        public const string ArgumentExceptionInvalidPasswordLength = "Das Passwort hat eine ungültige Länge.";

        public const string FormatExceptionIsNotAValidMadAddress = " ist keine gültige MAC-Adresse.";
#else
        public const string ObsoleteMacAddress = "The MacAddress class is obsolete. Consider using the System.Net.NetworkInformation.PhysicalAddress class.";

        public const string ArgumentExceptionInvalidMacAddressLength = "Invalid MAC address length.";
        public const string ArgumentExceptionInvalidPasswordLength = "Invalid password length.";

        public const string ArgumentExceptionIsNotAValidMadAddress = " is not a valid MAC address.";
#endif
    }
}
