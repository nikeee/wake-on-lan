using NUnit.Framework;
using System;
using System.Net;
using System.Net.Topology;

namespace WakeOnLan.Testing
{
    [TestFixture]
    public class IPAddressExtensionsTests : TestHelper
    {
        [Test]
        public void GetNetworkPrefix()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress expectedPrefix = new IPAddress(Ba(10, 20, 30, 0));
            IPAddress prefix = ip.GetNetworkPrefix(m);

            Assert.AreEqual(expectedPrefix, prefix);

            // TODO: Add more tests!
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetNetworkPrefixEx()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress prefix = ip.GetNetworkPrefix(m);
        }

        [Test]
        public void GetHostIdentifier()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress expectedId = new IPAddress(Ba(0, 0, 0, 40));
            IPAddress id = ip.GetHostIdentifier(m);

            Assert.AreEqual(expectedId, id);

            // TODO: Add more tests!
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetHostIdentifierEx()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress id = ip.GetHostIdentifier(m);
        }

        [Test]
        public void GetBroadcastAddress()
        {
            // TODO: Implement
        }
    }
}
