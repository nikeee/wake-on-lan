using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Topology
{
    public static class IPAddressExtensions
    {
        private const string NotAnIPv4Address = "Not an IPv4 address";
        public static int GetIPv4SiblingCount(this IPAddress address, NetMaskv4 mask)
        {
            return GetIPv4SiblingCount(address, mask, SiblingOptions.IncludeNothing);
        }

        public static int GetIPv4SiblingCount(this IPAddress address, NetMaskv4 mask, SiblingOptions options)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new ArgumentNullException();

            bool includeSelf = (options & SiblingOptions.IncludeSelf) == SiblingOptions.IncludeSelf;

            throw new NotImplementedException(NotAnIPv4Address);
        }

        public static IEnumerable<IPAddress> GetIPv4Siblings(this IPAddress address, NetMaskv4 mask)
        {
            return GetIPv4Siblings(address, mask, SiblingOptions.IncludeNothing);
        }

        public static IEnumerable<IPAddress> GetIPv4Siblings(this IPAddress address, NetMaskv4 mask, SiblingOptions options)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new ArgumentNullException(NotAnIPv4Address);

            bool includeSelf = (options & SiblingOptions.IncludeSelf) == SiblingOptions.IncludeSelf;

            throw new NotImplementedException();
        }

        [Flags]
        public enum SiblingOptions
        {
            IncludeNothing = 0,
            IncludeSelf = 1,
            IncludeBroadcast = 2,
            IncludeNet = 4, 
            IncludeAll = IncludeSelf | IncludeBroadcast | IncludeNet
        }
    }
}
