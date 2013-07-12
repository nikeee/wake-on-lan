using System;
using System.Net;
using System.Linq;
using System.Net.Topology;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WakeOnLan.Testing
{
    [TestClass]
    public class IPAddressExtensionsTests : TestHelper
    {
        [TestMethod]
        public void GetNetworkPrefix()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress expectedPrefix = new IPAddress(Ba(10, 20, 30, 0));
            IPAddress prefix = ip.GetNetworkPrefix(m);

            Assert.AreEqual(expectedPrefix, prefix);

            // TODO: Add more tests!
        }
    }
}