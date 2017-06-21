#if NET35

#if NET45
using System.Threading.Tasks;
#endif

using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace System.Net
{
    /// <summary>Provides extension methods for sending Wake On LAN signals (magic packets) to a specific <see cref="IPAddress"/>.</summary>
    public static class IPAddressExtensions
    {
        private const int DefaultWolPort = 7;
        #region Wol

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        [Obsolete("Use an other overload of this method.")]
        public static void SendWol(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5) => target.SendWol(mac0, mac1, mac2, mac3, mac4, mac5, DefaultWolPort);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        [Obsolete("Use an other overload of this method.")]
        public static void SendWol(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5, int port)
        {
            Net.SendWol.Send(new IPEndPoint(target, port), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, byte[] macAddress, int port) => target.SendWol(macAddress, port, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, byte[] macAddress, int port, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, port), macAddress, password);
        }
        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, byte[] macAddress) => target.SendWol(macAddress, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, byte[] macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="PhysicalAddress" /> <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, PhysicalAddress macAddress) => target.SendWol(macAddress, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="PhysicalAddress" /> <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="SocketException">An error occurred when accessing the socket. See Remarks section of <see cref="UdpClient.Send(byte[], int, IPEndPoint)"/> for more information.</exception>
        public static void SendWol(this IPAddress target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        #endregion
        #region ARP

        /// <summary>
        /// Sendet eine Anfrage über das ARP-Protokoll, um eine IP-Adresse in die Physikalische Adresse aufzulösen. Falls sich die physikalische Adresse bereits im Cache des Hosts befindet, wird diese zurückgegeben.
        /// </summary>
        /// <param name="destination">Destination <see cref="IPAddress"/>.</param>
        /// <returns>Eine <see cref="T:System.Net.ArpRequestResult">ArpRequestResult</see>-Instanz, welche die Ergebnisse der Anfrage enthält.</returns>
        public static ArpRequestResult SendArpRequest(this IPAddress destination) => ArpRequest.Send(destination);

        #endregion
        #region TAP

#if NET45
        #region Wol

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        [Obsolete("Use an other overload of this method.")]
        public static Task SendWolAsync(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5) => target.SendWolAsync(mac0, mac1, mac2, mac3, mac4, mac5, DefaultWolPort);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        [Obsolete("Use an other overload of this method.")]
        public static Task SendWolAsync(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5, int port)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, port), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, int port) => target.SendWolAsync(macAddress, port, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="port">The port to send the packet to.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, int port, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, port), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress) => target.SendWolAsync(macAddress, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="password"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="PhysicalAddress" /> <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, PhysicalAddress macAddress) => target.SendWolAsync(macAddress, null);

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPAddress"/>.</param>
        /// <param name="macAddress">The MAC address of the client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="PhysicalAddress" /> <paramref name="macAddress"/> is not 6.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="macAddress"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="password"/> is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        #endregion

        #region ARP

        /// <summary>Sends a request via ARP to resolve an IP address to aphysical address. If the physical address is already cached, it's cached value is returned.</summary>
        /// <param name="destination">Destination <see cref="IPAddress"/>.</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends an ARP request.</returns>
        public static Task<ArpRequestResult> SendArpRequestAsync(this IPAddress destination) => ArpRequest.SendAsync(destination);

        #endregion

#endif
        #endregion
    }
}
#endif
