#if NET35

#if NET45
using System.Threading.Tasks;
#endif

using System.Net.NetworkInformation;

namespace System.Net
{
    /// <summary>Stellt Erweiterungsmethoden für das Senden von Wake-On-LAN-Signalen bereit.</summary>
    public static class IPEndPointExtensions
    {
        #region Wol
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="mac0">Erstes MAC-Adress-Byte.</param>
        /// <param name="mac1">Zweites MAC-Adress-Byte.</param>
        /// <param name="mac2">Drittes MAC-Adress-Byte.</param>
        /// <param name="mac3">Viertes MAC-Adress-Byte.</param>
        /// <param name="mac4">Fünftes MAC-Adress-Byte.</param>
        /// <param name="mac5">Sechstes MAC-Adress-Byte.</param>
        public static void SendWol(this IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Net.SendWol.Send(target, mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        public static void SendWol(this IPEndPoint target, byte[] macAddress)
        {
            Net.SendWol.Send(target, macAddress);
        }

#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static void SendWol(this IPEndPoint target, MacAddress macAddress)
        {
            Net.SendWol.Send(target, macAddress);
        }
#endif

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        public static void SendWol(this IPEndPoint target, PhysicalAddress macAddress)
        {
            Net.SendWol.Send(target, macAddress);
        }
        
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(target, macAddress, password);
        }

#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static void SendWol(this IPEndPoint target, MacAddress macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(target, macAddress, password);
        }
#endif

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(target, macAddress, password);
        }

        #endregion
        #region TAP

#if NET45
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="mac0">Erstes MAC-Adress-Byte.</param>
        /// <param name="mac1">Zweites MAC-Adress-Byte.</param>
        /// <param name="mac2">Drittes MAC-Adress-Byte.</param>
        /// <param name="mac3">Viertes MAC-Adress-Byte.</param>
        /// <param name="mac4">Fünftes MAC-Adress-Byte.</param>
        /// <param name="mac5">Sechstes MAC-Adress-Byte.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            return Net.SendWol.SendAsync(target, mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this IPEndPoint target, byte[] macAddress)
        {
            return Net.SendWol.SendAsync(target, macAddress);
        }

#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static Task SendWolAsync(this IPEndPoint target, MacAddress macAddress)
        {
            return Net.SendWol.SendAsync(target, macAddress);
        }
#endif

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this IPEndPoint target, PhysicalAddress macAddress)
        {
            return Net.SendWol.SendAsync(target, macAddress);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(target, macAddress, password);
        }

#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static Task SendWolAsync(this IPEndPoint target, MacAddress macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(target, macAddress, password);
        }
#endif

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(target, macAddress, password);
        }
#endif
        #endregion
    }
}

#endif