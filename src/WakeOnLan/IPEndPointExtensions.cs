using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace System.Net
{
    /// <summary>Provides extension methods for sending Wake On LAN signals (magic packets) to a specific <see cref="IPEndPoint"/>.</summary>
    public static class IPEndPointExtensions
    {
        #region Wol

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        [Obsolete("Use an other overload of this method.")]
        public static void SendWol(this IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Net.SendWol.Send(target, mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        public static void SendWol(this IPEndPoint target, byte[] macAddress)
        {
            Net.SendWol.Send(target, macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        public static void SendWol(this IPEndPoint target, PhysicalAddress macAddress)
        {
            Net.SendWol.Send(target, macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        public static void SendWol(this IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(target, macAddress, password);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        public static void SendWol(this IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(target, macAddress, password);
        }

        #endregion
        #region TAP

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        [Obsolete("Use an other overload of this method.")]
        public static Task SendWolAsync(this IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            return Net.SendWol.SendAsync(target, mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPEndPoint target, byte[] macAddress)
        {
            return Net.SendWol.SendAsync(target, macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPEndPoint target, PhysicalAddress macAddress)
        {
            return Net.SendWol.SendAsync(target, macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(target, macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(target, macAddress, password);
        }
        #endregion
    }
}
