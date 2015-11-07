using System.ComponentModel;
using System.Net.NetworkInformation;

#if NET45
using System.Threading.Tasks;
#endif

namespace System.Net
{
    /// <summary>Stellt Methoden für das Senden von Anfragen über das ARP-Protokoll bereit.</summary>
    public static class ArpRequest
    {
        /// <summary>
        /// Sendet eine Anfrage über das ARP-Protokoll, um eine IP-Adresse in die Physikalische Adresse aufzulösen. Falls sich die physikalische Adresse bereits im Cache des Hosts befindet, wird diese zurückgegeben.
        /// </summary>
        /// <param name="destination">Die Ziel-IPAdress</param>
        /// <returns>Eine <see cref="T:System.Net.ArpRequestResult">ArpRequestResult</see>-Instanz, welche die Ergebnisse der Anfrage enthält.</returns>
        public static ArpRequestResult Send(IPAddress destination)
        {
            if (destination == null)
                throw new ArgumentNullException("destination");

            int destIp = BitConverter.ToInt32(destination.GetAddressBytes(), 0);

            var addr = new byte[6];
            var len = addr.Length;

            var res = NativeMethods.SendARP(destIp, 0, addr, ref len);

            if (res == 0)
                return new ArpRequestResult(new PhysicalAddress(addr));
            return new ArpRequestResult(new Win32Exception(res));
        }

#if NET45
        /// <summary>
        /// Sendet eine Anfrage über das ARP-Protokoll, um eine IP-Adresse in die Physikalische Adresse aufzulösen. Falls sich die physikalische Adresse bereits im Cache des Hosts befindet, wird diese zurückgegeben.
        /// </summary>
        /// <param name="destination">Die Ziel-IPAdress</param>
        /// <returns>Ein asynchroner Task, welcher einen ARP-Request sendet.</returns>
        public static Task<ArpRequestResult> SendAsync(IPAddress destination) => Task.Run(() => Send(destination));
#endif
    }
}
