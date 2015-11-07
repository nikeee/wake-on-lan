#if NET45
using System.Threading.Tasks;
#endif

using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace System.Net
{
    /// <summary>
    /// Stellt Methoden für das Senden von Wake-On-LAN-Signalen bereit.
    /// </summary>
    public static class SendWol
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
        /// <exception cref="SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Send(target, new[] { mac0, mac1, mac2, mac3, mac4, mac5 });
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, byte[] macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            byte[] packet = GetWolPacket(macAddress);
            SendPacket(target, packet);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] passwordBuffer = password.GetPasswordBytes();
            byte[] packet = GetWolPacket(macAddress, passwordBuffer);
            SendPacket(target, packet);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, PhysicalAddress macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            byte[] packet = GetWolPacket(macAddress.GetAddressBytes());
            SendPacket(target, packet);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] passwordBuffer = password.GetPasswordBytes();
            byte[] packet = GetWolPacket(macAddress.GetAddressBytes(), passwordBuffer);
            SendPacket(target, packet);
        }

        private static void SendPacket(IPEndPoint target, byte[] packet)
        {
            using (var cl = new UdpClient())
                cl.Send(packet, packet.Length, target);
        }

        #endregion
        #region TAP
#if NET45

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendAsync(IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            return SendAsync(target, new[] { mac0, mac1, mac2, mac3, mac4, mac5 });
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="Byte" /> array macAddress is not 6.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendAsync(IPEndPoint target, byte[] macAddress)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (macAddress.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidMacAddressLength);

            byte[] packet = GetWolPacket(macAddress);
            return SendPacketAsync(target, packet);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendAsync(IPEndPoint target, byte[] macAddress, SecureOnPassword password)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var passwordBuffer = password.GetPasswordBytes();
            var packet = GetWolPacket(macAddress, passwordBuffer);
            return SendPacketAsync(target, packet);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendAsync(IPEndPoint target, PhysicalAddress macAddress)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            var p = GetWolPacket(macAddress.GetAddressBytes());
            return SendPacketAsync(target, p);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">Destination <see cref="IPEndPoint"/>.</param>
        /// <param name="macAddress">The MAC address of the designated client.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendAsync(IPEndPoint target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var passwordBuffer = password.GetPasswordBytes();
            var p = GetWolPacket(macAddress.GetAddressBytes(), passwordBuffer);
            return SendPacketAsync(target, p);
        }

        private static Task SendPacketAsync(IPEndPoint target, byte[] packet)
        {
            var cl = new UdpClient();
            return cl.SendAsync(packet, packet.Length, target).ContinueWith((Task t) => cl.Close());
        }

#endif
        #endregion
        #region internal

        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        private static byte[] GetWolPacket(byte[] macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (macAddress.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidMacAddressLength);

            var packet = new byte[17 * 6];

            int offset, i;

            for (offset = 0; offset < 6; ++offset)
                packet[offset] = 0xFF;

            for (offset = 6; offset < 17 * 6; offset += 6)
                for (i = 0; i < 6; ++i)
                    packet[i + offset] = macAddress[i];

            return packet;
        }

        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array password is not 6.</exception>
        private static byte[] GetWolPacket(byte[] macAddress, byte[] password)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));
            if (macAddress.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidMacAddressLength);
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (password.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidPasswordLength);

            var packet = new byte[18 * 6];

            int offset, i;
            for (offset = 0; offset < 6; ++offset)
                packet[offset] = 0xFF;

            for (offset = 6; offset < 17 * 6; offset += 6)
                for (i = 0; i < 6; ++i)
                    packet[i + offset] = macAddress[i];

            for (offset = 16 * 6 + 6; offset < 18 * 6; offset += 6)
                for (i = 0; i < 6; ++i)
                    packet[i + offset] = password[i];

            return packet;
        }

        #endregion
    }
}
