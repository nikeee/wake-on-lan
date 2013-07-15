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
            
            TestSiblings(a, ip, mask);

            a = new List<IPAddress> {
                new IPAddress(Ba(10, 20, 3, 192)),
             // new IPAddress(Ba(10, 20, 3, 193)),
                new IPAddress(Ba(10, 20, 3, 194)),
                new IPAddress(Ba(10, 20, 3, 195)),
                new IPAddress(Ba(10, 20, 3, 196)),
                new IPAddress(Ba(10, 20, 3, 197)),
                new IPAddress(Ba(10, 20, 3, 198)),
                new IPAddress(Ba(10, 20, 3, 199)),
                new IPAddress(Ba(10, 20, 3, 200)),
                new IPAddress(Ba(10, 20, 3, 201)),
                new IPAddress(Ba(10, 20, 3, 202)),
                new IPAddress(Ba(10, 20, 3, 203)),
                new IPAddress(Ba(10, 20, 3, 204)),
                new IPAddress(Ba(10, 20, 3, 205)),
                new IPAddress(Ba(10, 20, 3, 206)),
                new IPAddress(Ba(10, 20, 3, 207))
            };
            ip = new IPAddress(Ba(10, 20, 3, 193));
            mask = new NetMask(255, 255, 255, 240);

            TestSiblings(a, ip, mask, SiblingOptions.IncludeNetworkIdentifier | SiblingOptions.IncludeBroadcast);

            a.RemoveAt(a.Count - 1);
            TestSiblings(a, ip, mask, SiblingOptions.IncludeNetworkIdentifier);

            a.RemoveAt(0);
            TestSiblings(a, ip, mask, SiblingOptions.ExcludeAll);
        }

        private void TestSiblings(List<IPAddress> expectedAddresses, IPAddress currentIp, NetMask mask, SiblingOptions options = SiblingOptions.IncludeAll)
        {
            var enumerable = currentIp.GetSiblings(mask, options);

            var siblingCount = currentIp.GetSiblingCount(mask, options);
            Assert.AreEqual(expectedAddresses.Count, siblingCount);
            
            int i = 0;
            foreach (var t in enumerable)
            {
                var expected = expectedAddresses[i++];
                Assert.AreEqual(expected, t);
            }
            Assert.AreEqual(expectedAddresses.Count, i);
        }
    }
}