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
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Send(target, new[] { mac0, mac1, mac2, mac3, mac4, mac5 });
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge dere <see cref="T:System.Byte" />-Arrays macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, byte[] macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            byte[] packet = GetWolPacket(macAddress);
            SendPacket(target, packet);
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentException">Die Länge des <see cref="T:System.Byte" />-Arrays macAddress ist nicht 6.</exception>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
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

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void Send(IPEndPoint target, PhysicalAddress macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            byte[] packet = GetWolPacket(macAddress.GetAddressBytes());
            SendPacket(target, packet);
        }

#if NET35
#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static void Send(IPEndPoint target, MacAddress macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");

            byte[] packet = GetWolPacket(macAddress.Address);
            SendPacket(target, packet);
        }
#endif
#if INCLUDEOBSOLETE
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static void Send(IPEndPoint target, MacAddress macAddress, SecureOnPassword password)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");

            if (password == null)
                throw new ArgumentNullException("password");

            byte[] packet = GetWolPacket(macAddress.Address, password.Password);
            SendPacket(target, packet);
        }
#endif
#endif

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
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

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="mac0">Erstes MAC-Adress-Byte</param>
        /// <param name="mac1">Zweites MAC-Adress-Byte</param>
        /// <param name="mac2">Drittes MAC-Adress-Byte</param>
        /// <param name="mac3">Viertes MAC-Adress-Byte</param>
        /// <param name="mac4">Fünftes MAC-Adress-Byte</param>
        /// <param name="mac5">Sechstes MAC-Adress-Byte</param>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendAsync(IPEndPoint target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            return SendAsync(target, new[] { mac0, mac1, mac2, mac3, mac4, mac5 });
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge des <see cref="T:System.Byte" />-Arrays macAddress ist nicht 6.</exception>
        ///<exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
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

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
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

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        public static Task SendAsync(IPEndPoint target, PhysicalAddress macAddress)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            var p = GetWolPacket(macAddress.GetAddressBytes());
            return SendPacketAsync(target, p);
            //return new Task(() => Send(target, macAddress));
        }

        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">Das SecureOn-Passwort des Clients.</param>
        /// <exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
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

#if NET35
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
        public static Task SendAsync(IPEndPoint target, MacAddress macAddress, SecureOnPassword password)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");

            if (password == null)
                throw new ArgumentNullException("password");

            byte[] packet = GetWolPacket(macAddress.Address, password.Password);
            return SendPacketAsync(target, packet);
        }
#endif
#if INCLUDEOBSOLETE
        
        /// <summary>
        /// Sendet ein Wake-On-LAN-Signal an einen Client.
        /// </summary>
        /// <param name="target">Der Ziel-IPEndPoint.</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        /// <returns>Ein asynchroner Task, welcher ein Wake-On-LAN-Signal an einen Client sendet.</returns>
        [Obsolete(Localization.ObsoleteMacAddress)]
        public static Task SendAsync(IPEndPoint target, MacAddress macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");
            var p = GetWolPacket(macAddress.Address);
            return SendPacketAsync(target, p);
        }
#endif
#endif

#endif
        #endregion
        #region internal

        ///<exception cref="System.ArgumentException">Die Länge des <see cref="T:System.Byte" />-Arrays ist nicht 6.</exception>
        ///<exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        private static byte[] GetWolPacket(byte[] macAddress)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");
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

        ///<exception cref="System.ArgumentException">Die Länge des <see cref="T:System.Byte" />-Arrays macAddress ist nicht 6.</exception>
        ///<exception cref="System.ArgumentNullException">macAddress ist null.</exception>
        ///<exception cref="System.ArgumentException">Die Länge des <see cref="T:System.Byte" />-Arrays password ist nicht 6.</exception>
        ///<exception cref="System.ArgumentNullException">password ist null.</exception>
        private static byte[] GetWolPacket(byte[] macAddress, byte[] password)
        {
            if (macAddress == null)
                throw new ArgumentNullException("macAddress");
            if (macAddress.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidMacAddressLength);
            if (password == null)
                throw new ArgumentNullException("password");
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
