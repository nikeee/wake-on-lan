using System;
using System.Net;
using System.Linq;
using System.Net.Topology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetNetworkPrefixEx()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress prefix = ip.GetNetworkPrefix(m);
        }

        [TestMethod]
        public void GetHostIdentifier()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress expectedId = new IPAddress(Ba(0, 0, 0, 40));
            IPAddress id = ip.GetHostIdentifier(m);

            Assert.AreEqual(expectedId, id);

            // TODO: Add more tests!
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetHostIdentifierEx()
        {
            var ip = new IPAddress(Ba(10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40, 10, 20, 30, 40));
            var m = new NetMask(Ba(255, 255, 255, 0));

            IPAddress id = ip.GetHostIdentifier(m);
        }

        [TestMethod]
        public void GetSiblings()
        {
            var a = new List<IPAddress> {
                new IPAddress(Ba(192, 168, 178, 0)),
                new IPAddress(Ba(192, 168, 178, 1)),
                new IPAddress(Ba(192, 168, 178, 2)),
                new IPAddress(Ba(192, 168, 178, 3)),
                new IPAddress(Ba(192, 168, 178, 4)),
                new IPAddress(Ba(192, 168, 178, 5)),
                new IPAddress(Ba(192, 168, 178, 6)),
                new IPAddress(Ba(192, 168, 178, 7))
            };
            var ip = new IPAddress(Ba(192, 168, 178, 5));
            var mask = new NetMask(255, 255, 255, 248);

            var enumerable = ip.GetSiblings(mask, SiblingOptions.IncludeAll);
            int i = 0;
            
            foreach (var t in enumerable)
            {
                var expected = a[i];
                Assert.AreEqual(expected, t);
                ++i;
            }
            Assert.AreEqual(a.Count, i);
        }
    }
}