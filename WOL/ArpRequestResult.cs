using System.Net.NetworkInformation;
using System.Text;

namespace System.Net
{
    /// <summary>
    /// Enthält die Rückgabewerte der ArpRequest.Send-Funktion.
    /// </summary>
    public class ArpRequestResult
    {
        /// <summary>Falls Fehler bei der Protokollanfrage auftreten, werden diese in dieser Eigenschaft abgelegt. Andernfalls null.</summary>
        public Exception Exception { get; private set; }

        /// <summary>Die aufgelöste physikalische Adresse.</summary>
        public PhysicalAddress Address { get; private set; }

        /// <summary>Erstellt eine neue ArpRequestResult-Instanz</summary>
        /// <param name="address">Die physikalische Adresse</param>
        public ArpRequestResult(PhysicalAddress address)
        {
            Exception = null;
            Address = address;
        }

        /// <summary>Erstellt eine neue ArpRequestResult-Instanz</summary>
        /// <param name="exception">Der aufgetretene Fehler</param>
        public ArpRequestResult(Exception exception)
        {
            Exception = exception;
            Address = null;
        }

        /// <summary>Konvertiert ARP-Rückgabewerte in eine Zeichenfolge.</summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Address == null)
                sb.Append("no address");
            else
            {
                sb.Append("address: ");
                sb.Append(Address);
            }
            sb.Append(", ");
            if (Exception == null)
                sb.Append("no exception");
            else
            {
                sb.Append("exception: ");
                sb.Append(Exception.Message);
            }
            return sb.ToString();
        }
    }
}