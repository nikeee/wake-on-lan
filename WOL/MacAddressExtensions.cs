#if NET35 && INCLUDEOBSOLETE

#if NET45
using System.Threading.Tasks;
#endif

namespace System.Net
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für das Senden von Wake-On-LAN-Signalen bereit.
    /// </summary>
    [Obsolete(Localization.ObsoleteMacAddress)]
    public static class MacAddressExtensions
    {
#region Wol

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        public static void SendWol(this MacAddress address)
        {
            IPAddress.Broadcast.SendWol(address.Address);
        }
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this MacAddress address, SecureOnPassword password)
        {
            IPAddress.Broadcast.SendWol(address.Address, password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        public static void SendWol(this MacAddress address, IPAddress target)
        {
            target.SendWol(address.Address);
        }


        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this MacAddress address, IPAddress target, SecureOnPassword password)
        {
            target.SendWol(address.Address, password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        public static void SendWol(this MacAddress address, IPEndPoint target)
        {
            target.SendWol(address.Address);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this MacAddress address, IPEndPoint target, SecureOnPassword password)
        {
            target.SendWol(address.Address, password);
        }

        #endregion
#region TAP
#if NET45
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address)
        {
            return IPAddress.Broadcast.SendWolAsync(address.Address);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address, SecureOnPassword password)
        {
            return IPAddress.Broadcast.SendWolAsync(address.Address, password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address, IPAddress target)
        {
            return target.SendWolAsync(address.Address);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address, IPAddress target, SecureOnPassword password)
        {
            return target.SendWolAsync(address.Address, password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address, IPEndPoint target)
        {
           return target.SendWolAsync(address.Address);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz in einem separaten Thread.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this MacAddress address, IPEndPoint target, SecureOnPassword password)
        {
           return target.SendWolAsync(address.Address,password);
        }
#endif
#endregion
    }
}

#endif