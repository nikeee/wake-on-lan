using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Topology
{
    public static class IPAddressExtensions
    {
        public static int GetIPv4SiblingCount(this IPAddress address, NetMaskv4 mask)
        {
            return GetIPv4SiblingCount(address, mask, true);
        }

        public static int GetIPv4SiblingCount(this IPAddress address, NetMaskv4 mask, bool includeSelf)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            throw new NotImplementedException();
        }

        public static IEnumerable<IPAddress> GetIPv4Siblings(this IPAddress address, NetMaskv4 mask)
        {
            return GetIPv4Siblings(address, mask, true);
        }

        public static IEnumerable<IPAddress> GetIPv4Siblings(this IPAddress address, NetMaskv4 mask, bool includeSelf)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            throw new NotImplementedException();
        }
    }
}
