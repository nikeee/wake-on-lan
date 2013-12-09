#if NET35 && INCLUDEOBSOLETE

using System.Linq;
using System.Net.NetworkInformation;

namespace System.Net
{
    /// <summary>
    /// Stellt eine Media-Access-Control-Adresse (MAC-Adresse) bereit.
    /// </summary>
    [Serializable]
    [Obsolete(Localization.ObsoleteMacAddress)]
    public class MacAddress 
    {

#region properties

        /// <summary>
        /// Die Adressdaten der MAC-Adresse
        /// </summary>
        public byte[] Address { get; private set; }

        #endregion

#region statics

        /// <summary>
        /// Stellt die Broadcast MAC-Adresse (FF-FF-FF-FF-FF-FF) bereit.
        /// </summary>
        public static MacAddress Broadcast
        {
            get
            {
                return _broadcast ?? (_broadcast = new MacAddress(0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF));
            }
        }
        private static MacAddress _broadcast;

        #endregion

#region ctors

        /// <summary>
        /// Initialisiert eine neue Instanz der System.Net.MacAddress-Klasse mit der Adresse, die über die einzelnen System.Byte-Bestandteile angegeben ist.
        /// </summary>
        /// <param name="mac0">Erstes MAC-Adress-Byte.</param>
        /// <param name="mac1">Zweites MAC-Adress-Byte.</param>
        /// <param name="mac2">Drittes MAC-Adress-Byte.</param>
        /// <param name="mac3">Viertes MAC-Adress-Byte.</param>
        /// <param name="mac4">Fünftes MAC-Adress-Byte.</param>
        /// <param name="mac5">Sechstes MAC-Adress-Byte.</param>
        public MacAddress(byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Address = new byte[6];
            Address[0] = mac0;
            Address[1] = mac1;
            Address[2] = mac2;
            Address[3] = mac3;
            Address[4] = mac4;
            Address[5] = mac5;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der System.Net.MacAddress-Klasse mit der Adresse, die als System.Byte-Array angegeben ist.
        /// </summary>
        /// <param name="address">Der Bytearraywert der MAC-Adresse.</param>
        /// <exception cref="System.ArgumentNullException">address ist null.</exception>
        /// <exception cref="System.ArgumentException">address hat nicht die Länge 6.</exception>
        public MacAddress(byte[] address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (address.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidMacAddressLength);
            Address = address;
        }

        #endregion
        
#region parsing

        /// <summary>
        /// Bestimmt, ob eine Zeichenfolge eine gültige MAC-Adresse ist.
        /// </summary>
        /// <param name="macString">Die zu validierende Zeichenfolge.</param>
        /// <param name="delimieter">Das Delimiterzeichen der einzelnen Adress-Bytes.</param>
        /// <param name="macAddress">Die System.Net.MacAddress-Version der Zeichenfolge.</param>
        /// <returns>true, wenn macString eine gültige MAC-Adresse ist, andernfalls false.</returns>
        public static bool TryParse(string macString, char delimieter, out MacAddress macAddress)
        {
            macAddress = null;

            if (string.IsNullOrEmpty(macString) || macString.Trim() == string.Empty)
                return false;

            macString = macString.Trim();

            //00-00-00-00-00-00
            //6*2+5 =>17
            if (macString.Length != 17)
                return false;

            string[] split;
            if (!macString.Contains(delimieter))
                return false;
            split = macString.Split(delimieter);
            if (split.Length != 6)
                return false;

            byte[] mac = new byte[6];
            for (int i = 0; i < split.Length; i++)
                if (!byte.TryParse(split[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out mac[i]))
                    return false;
            macAddress = new MacAddress(mac);
            return true;
        }

        /// <summary>
        /// Bestimmt, ob eine Zeichenfolge eine gültige MAC-Adresse ist.
        /// </summary>
        /// <param name="macString">Die zu validierende Zeichenfolge.</param>
        /// <param name="numberStyle">Der Nummernstil der zu validierenden Zeichenfolge.</param>
        /// <param name="delimieter">Das Delimiterzeichen der einzelnen Adress-Bytes.</param>
        /// <param name="macAddress">Die System.Net.MacAddress-Version der Zeichenfolge.</param>
        /// <returns>true, wenn macString eine gültige MAC-Adresse ist, andernfalls false.</returns>
        public static bool TryParse(string macString, char delimieter, System.Globalization.NumberStyles numberStyle, out MacAddress macAddress)
        {
            macAddress = null;

            if (string.IsNullOrEmpty(macString) || macString.Trim() == string.Empty)
                return false;

            macString = macString.Trim();

            //00-00-00-00-00-00
            //6*2+5 =>17
            if (macString.Length != 17)
                return false;

            string[] split;
            if (!macString.Contains(delimieter))
                return false;

            split = macString.Split(delimieter);

            if (split.Length != 6)
                return false;

            byte[] mac = new byte[6];
            for (int i = 0; i < split.Length; i++)
                if (!byte.TryParse(split[i], numberStyle, System.Globalization.CultureInfo.InvariantCulture, out mac[i]))
                    return false;
            macAddress = new MacAddress(mac);
            return true;
        }

        /// <summary>
        /// Konvertiert eine MAC-Adresszeichenfolge in eine System.Net.MacAddress-Instanz.
        /// </summary>
        /// <param name="macString">Eine Zeichenfolge, die eine MAC-Adresse darstellt. Das Delimiterzeichen darf nur ein '-', ':' oder ' ' sein.</param>
        /// <returns>Eine System.Net.MacAddress-Instanz.</returns>
        /// <exception cref="System.FormatException">macString ist keine gültige MAC-Adresse.</exception>
        /// <exception cref="System.ArgumentNullException">macString ist null.</exception>
        public static MacAddress Parse(string macString)
        {
            if (string.IsNullOrEmpty(macString) || macString.Trim() == string.Empty)
                throw new ArgumentNullException("macString");

            //00-00-00-00-00-00
            //6*2+5 =>17
            if (macString.Length != 17)
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            string[] split;
            if (macString.Contains(':'))
                split = macString.Split(':');
            else if (macString.Contains('-'))
                split = macString.Split('-');
            else if (macString.Contains(' '))
                split = macString.Split(' ');
            else
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            if (split.Length != 6)
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            byte[] mac = new byte[6];
            for (int i = 0; i < split.Length; i++)
                if (!byte.TryParse(split[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out mac[i]))
                    throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);
            return new MacAddress(mac);
        }

        /// <summary>
        /// Konvertiert eine MAC-Adresszeichenfolge in eine System.Net.MacAddress-Instanz.
        /// </summary>
        /// <param name="macString">Eine Zeichenfolge, die eine MAC-Adresse darstellt.</param>
        /// <param name="delimiter">Das Delimiterzeichen, welches die Adressbytes trennt.</param>
        /// <returns>Eine System.Net.MacAddress-Instanz.</returns>
        /// <exception cref="System.FormatException">macString ist keine gültige MAC-Adresse.</exception>
        /// <exception cref="System.ArgumentNullException">macString ist null.</exception>
        public static MacAddress Parse(string macString, char delimiter)
        {
            if (string.IsNullOrEmpty(macString) || macString.Trim() == string.Empty)
                throw new ArgumentNullException("macString");

            //00-00-00-00-00-00
            //6*2+5 =>17
            if (macString.Length != 17)
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            string[] split;
            if (!macString.Contains(delimiter))
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            split = macString.Split(delimiter);

            if (split.Length != 6)
                throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            byte[] mac = new byte[6];
            for (int i = 0; i < split.Length; i++)
                if (!byte.TryParse(split[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out mac[i]))
                    throw new FormatException("macString" + Localization.FormatExceptionIsNotAValidMadAddress);

            return new MacAddress(mac);
        }

        #endregion

#region tostring

        /// <summary>
        /// Konvertiert MAC-Adressen in die Standardnotation.
        /// </summary>
        /// <returns>Eine Zeichenfolge mit einer MAC-Adresse im Strichformat.</returns>
        public override string ToString()
        {
            return ToString('-');
        }

        /// <summary>
        /// Konvertiert MAC-Adressen in die Standardnotation.
        /// </summary>
        /// <param name="delimiter">Das Delimiterzeichen, welches die Adressbytes trennt.</param>
        /// <returns>Eine Zeichenfolge mit einer MAC-Adresse im jeweiligen Format.</returns>
        public string ToString(char delimiter)
        {
            string[] f = new string[6];
            for (int i = 0; i < f.Length; i++)
                f[i] = Address[i].ToString("X2");
            return string.Join(delimiter.ToString(), f);
        }

        #endregion

        //#region operators

        //public static bool operator ==(MacAddress m1, MacAddress m2)
        //{
        //    for (int i = 0; i < 6; i++)
        //        if (m1.Address[i] != m2.Address[i])
        //            return false;
        //    return true;
        //}
        //public static bool operator !=(MacAddress m1, MacAddress m2)
        //{
        //    return !(m1 == m2);
        //}

        //#endregion

#region common overrides

        /*
        
        /// <summary>
        /// Bestimmt, ob das angegebene Object und die aktuelle MAC-Adresse gleich sind.
        /// </summary>
        /// <param name="obj">Das Objekt, das mit der aktuellen MAC-Adresse verglichen werden soll.</param>
        /// <returns>Bestimmt, ob das angegebene Object und die aktuelle MAC-Adresse gleich sind.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj as MacAddress == null)
                return false;
            return (obj as MacAddress) == this;
        }

        /// <summary>
        /// Fungiert als Hashfunktion für die aktuelle MAC-Adresse.
        /// </summary>
        /// <returns>Ein Hashcode für die aktuelle MAC-Adresse.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        */

        #endregion

        /// <summary>
        /// Stellt ein Oktett der MAC-Adresse eines bestimmten Indizes bereit.
        /// </summary>
        /// <param name="index">Der Index.</param>
        /// <returns>Das Byte der MAC-Adresse mit dem angegebenen Index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Der Index liegt nicht zwischen 0 und 5.</exception>
        public byte this[int index]
        {
            get
            {
                return Address[index];
            }
        }

        /// <summary>
        /// Gibt den entsprechenden PhysicalAddress-Typen zur aktuellen MacAddress zurück.
        /// </summary>
        /// <returns>Den entsprechenden PhysicalAddress-Typen zur aktuellen MacAddress-Instanz.</returns>
        public PhysicalAddress ToPhysicalAddress()
        {
            return new PhysicalAddress(Address);
        }
    }
}

#endif
