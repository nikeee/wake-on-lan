#if NET35

#if NET45
using System.Threading.Tasks;
#endif

using System.Net.NetworkInformation;

namespace System.Net
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für das Senden von Wake-On-LAN-Signalen bereit.
    /// </summary>
    public static class PhysicalAddressExtensions
    {
        #region Wol

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der PhysicalAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        public static void SendWol(this PhysicalAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            IPAddress.Broadcast.SendWol(address.GetAddressBytes());
        }
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der PhysicalAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this PhysicalAddress address, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            IPAddress.Broadcast.SendWol(address.GetAddressBytes(), password);
        }
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        public static void SendWol(this PhysicalAddress address, IPAddress target)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            target.SendWol(address.GetAddressBytes());
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this PhysicalAddress address, IPAddress target, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            target.SendWol(address.GetAddressBytes(), password);
        }


        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        public static void SendWol(this PhysicalAddress address, IPEndPoint target)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            target.SendWol(address.GetAddressBytes());
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz mit der MAC-Adresse der Instanz.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        public static void SendWol(this PhysicalAddress address, IPEndPoint target, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            target.SendWol(address.GetAddressBytes(), password);
        }

        #endregion
        #region TAP

#if NET45

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der PhysicalAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return IPAddress.Broadcast.SendWolAsync(address.GetAddressBytes());
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die Broadcast-IP-Adresse, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der PhysicalAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return IPAddress.Broadcast.SendWolAsync(address.GetAddressBytes(), password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address, IPAddress target)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return target.SendWolAsync(address.GetAddressBytes());
        }


        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an die IP-Adresse der target-Instanz, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address, IPAddress target, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return target.SendWolAsync(address.GetAddressBytes(), password);
        }


        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address, IPEndPoint target)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return target.SendWolAsync(address.GetAddressBytes());
        }


        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an den IP-Endpunkt der target-Instanz, was als asynchroner Vorgang mithilfe eines Taskobjekts angegeben wird.
        /// </summary>
        /// <param name="address">Die Instanz der MacAddress, die im Wake-On-LAN-Signal angesprochen werden soll.</param>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendWolAsync(this PhysicalAddress address, IPEndPoint target, SecureOnPassword password)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            return target.SendWolAsync(address.GetAddressBytes(), password);
        }
#endif

        #endregion
        #region common

        /// <summary>
        /// Ruft den Typ einer physikalischen Adresse ab.
        /// </summary>
        /// <param name="address">Die Adresse</param>
        /// <returns>Der <see cref="T:System.Net.PhysicalAddressType">Typ</see> der physikalischen Adresse.</returns>
        public static PhysicalAddressType GetAddressType(this PhysicalAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            var bytes = address.GetAddressBytes();
            if (bytes == null || bytes.Length < 1)
                throw new ArgumentException("Invalid PhysicalAddress.");
            return (bytes[0] & 0x1) == 0 ? PhysicalAddressType.Unicast : PhysicalAddressType.Multicast;
        }

        /// <summary>
        /// Ruft den administratortypen einer physikalischen Adresse ab.
        /// </summary>
        /// <param name="address">Die Adresse</param>
        /// <returns>Der <see cref="T:System.Net.PhysicalAddressAdministrator">administratortyp</see> der physikalischen Adresse.</returns>
        public static PhysicalAddressAdministrator GetAddressAdministrator(this PhysicalAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            var bytes = address.GetAddressBytes();
            if (bytes == null || bytes.Length < 1)
                throw new ArgumentException("Invalid PhysicalAddress.");
            return (bytes[0] & 0x2) == 0 ? PhysicalAddressAdministrator.Global : PhysicalAddressAdministrator.Local;
        }

        #endregion
    }
}

#endif