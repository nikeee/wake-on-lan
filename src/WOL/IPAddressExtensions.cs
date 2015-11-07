#if NET35

#if NET45
using System.Threading.Tasks;
#endif

using System.Net.NetworkInformation;

namespace System.Net
{
    /// <summary>Stellt Erweiterungsmethoden für das Senden von Wake-On-LAN-Signalen bereit.</summary>
    public static class IPAddressExtensions
    {
        private const int DefaultWolPort = 7;
        #region Wol

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5, int port)
        {
            Net.SendWol.Send(new IPEndPoint(target, port), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        public static void SendWol(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, byte[] macAddress, int port)
        {
            Net.SendWol.Send(new IPEndPoint(target, port), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, byte[] macAddress, int port, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, port), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, byte[] macAddress)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, byte[] macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, PhysicalAddress macAddress)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Fehler beim Zugriff auf den Socket. Weitere Informationen finden Sie im Abschnitt "Hinweise".</exception>
        /// <returns />
        public static void SendWol(this IPAddress target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            Net.SendWol.Send(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        #endregion
        #region ARP

        /// <summary>
        /// Sendet eine Anfrage über das ARP-Protokoll, um eine IP-Adresse in die Physikalische Adresse aufzulösen. Falls sich die physikalische Adresse bereits im Cache des Hosts befindet, wird diese zurückgegeben.
        /// </summary>
        /// <param name="destination">Die Ziel-IPAdress</param>
        /// <returns>Eine <see cref="T:System.Net.ArpRequestResult">ArpRequestResult</see>-Instanz, welche die Ergebnisse der Anfrage enthält.</returns>
        public static ArpRequestResult SendArpRequest(this IPAddress destination) => ArpRequest.Send(destination);

        #endregion
        #region TAP

#if NET45
        #region Wol

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5, int port)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, port), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">die Ziel-IPAddress</param>
        /// <param name="mac0">First MAC Address byte.</param>
        /// <param name="mac1">Second MAC Address byte.</param>
        /// <param name="mac2">Third MAC Address byte.</param>
        /// <param name="mac3">Fourth MAC Address byte.</param>
        /// <param name="mac4">Fifth MAC Address byte.</param>
        /// <param name="mac5">Sixth MAC Address byte.</param>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte mac0, byte mac1, byte mac2, byte mac3, byte mac4, byte mac5)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), mac0, mac1, mac2, mac3, mac4, mac5);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, int port)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, port), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="port">der Port, an den das Signal gesendet wird</param>
        /// <param name="password">The SecureOn password of the client.</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, int port, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, port), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, byte[] macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        ///<exception cref="System.ArgumentException">Die Länge der System.Byte-Array macAddress ist nicht 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, PhysicalAddress macAddress)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress);
        }

        /// <summary>Sends a Wake On LAN signal (magic packet) to a client.</summary>
        /// <param name="target">der Hostname des Zielclients</param>
        /// <param name="macAddress">Die MAC-Adresse des Clients.</param>
        /// <param name="password">The SecureOn password of the client.</param>
        /// <exception cref="ArgumentException">The length of the <see cref="T:System.Byte" /> array macAddress is not 6.</exception>
        /// <exception cref="ArgumentNullException">macAddress is null.</exception>
        /// <exception cref="ArgumentNullException">password is null.</exception>
        /// <returns>An asynchronous <see cref="Task"/> which sends a Wake On LAN signal (magic packet) to a client.</returns>
        public static Task SendWolAsync(this IPAddress target, PhysicalAddress macAddress, SecureOnPassword password)
        {
            return Net.SendWol.SendAsync(new IPEndPoint(target, DefaultWolPort), macAddress, password);
        }

        #endregion

        #region ARP

        /// <summary>
        /// Sendet eine Anfrage über das ARP-Protokoll, um eine IP-Adresse in die Physikalische Adresse aufzulösen. Falls sich die physikalische Adresse bereits im Cache des Hosts befindet, wird diese zurückgegeben.
        /// </summary>
        /// <param name="destination">Die Ziel-IPAdress</param>
        /// <returns>Ein asynchroner Task, welcher einen ARP-Request sendet.</returns>
        public static Task<ArpRequestResult> SendArpRequestAsync(this IPAddress destination) => ArpRequest.SendAsync(destination);

        #endregion

#endif
        #endregion
    }
}
#endif
