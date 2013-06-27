#if NET35

using System.Collections.Generic;
using System.Collections;

namespace System.Net.Topology
{
    public static class IPAddressExtensions
    {
        private const string OnlyIPv4Supported = "Only IPv4 is currently supported";
        public static int GetSiblingCount(this IPAddress address, NetMaskv4 mask)
        {
            return GetSiblingCount(address, mask, SiblingOptions.ExcludeAll);
        }

        public static int GetSiblingCount(this IPAddress address, NetMaskv4 mask, SiblingOptions options)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (mask == null)
                throw new ArgumentNullException("mask");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new NotSupportedException(OnlyIPv4Supported);

            bool includeSelf = BitHelper.IsOptionSet(options, SiblingOptions.IncludeSelf);

            throw new NotImplementedException();
        }

        public static IEnumerable<IPAddress> GetSiblings(this IPAddress address, NetMaskv4 mask)
        {
            return GetSiblings(address, mask, SiblingOptions.ExcludeAll);
        }

        public static IEnumerable<IPAddress> GetSiblings(this IPAddress address, NetMaskv4 mask, SiblingOptions options)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (mask == null)
                throw new ArgumentNullException("mask");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new NotSupportedException(OnlyIPv4Supported);

            bool includeSelf = BitHelper.IsOptionSet(options, SiblingOptions.IncludeSelf);
            bool includeBroadcast = BitHelper.IsOptionSet(options,SiblingOptions.IncludeBroadcast);
            bool includeNetworkIdentifier = BitHelper.IsOptionSet(options, SiblingOptions.IncludeNetworkIdentifier);

            throw new NotImplementedException();
        }

        public static IPAddress GetNetworkPrefix(this IPAddress address, NetMaskv4 mask)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (mask == null)
                throw new ArgumentNullException("mask");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new NotSupportedException(OnlyIPv4Supported);

            return mask & address;
        }

        public static IPAddress GetHostIdentifier(this IPAddress address, NetMaskv4 mask)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (mask == null)
                throw new ArgumentNullException("mask");
            if (address.AddressFamily != Sockets.AddressFamily.InterNetwork)
                throw new NotSupportedException(OnlyIPv4Supported);

            var maskBits = mask.GetBits();
            var ipBits = new BitArray(address.GetAddressBytes());

            // TODO: Testing!

            // !Mask & IP
            var retVal = maskBits.Not().And(ipBits); // why does BitArray has no operators?
            var bytes = new byte[4];
            retVal.CopyTo(bytes, 0);

            return new IPAddress(bytes);
        }
    }
}
#endif