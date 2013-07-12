using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Topology;

namespace WakeOnLan.Testing
{
    [TestClass]
    public class NetMaskTests
    {
        [TestMethod]
        public void Constructor()
        {
            var ip = IPAddress.Parse("255.255.248.0");
            var expected = new NetMask(255, 255, 248, 0);
            var actual = new NetMask(ip);
            Assert.AreEqual(expected, actual);

            var m1 = new NetMask(255, 255, 248, 0);
            var m2 = new NetMask(-2048);
            Assert.IsTrue(m1 == m2);

            var m3 = new NetMask(new byte[] { 255, 255, 248, 0 });
            Assert.IsTrue(m1 == m3);
        }

        [TestMethod]
        public void EqualityOperator()
        {
            var m1 = new NetMask(255, 255, 248, 0);
            var m2 = new NetMask(255, 255, 248, 0);
            var m3 = new NetMask(255, 255, 0, 0);

            Assert.AreEqual(m1, m2);
            Assert.IsTrue(m1 == m2);
            Assert.IsFalse(m1 == m3);
            Assert.IsTrue(m1 != m3);
        }

        [TestMethod]
        public void Empty()
        {
            Assert.IsTrue(new NetMask(0, 0, 0, 0) == NetMask.Empty);
            Assert.IsTrue(new NetMask(0) == NetMask.Empty);
        }

        [TestMethod]
        public void ToStringTest()
        {
            //var m1 = new NetMask(255, 255, 248, 0);
            var m1 = new NetMask(-2048);
            var expected = "255.255.248.0 (11111111.11111111.11111000.00000000)";
            var str = m1.ToString();
            Assert.AreEqual(expected, str);
        }
    }
}
