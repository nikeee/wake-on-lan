using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Topology;

namespace WakeOnLan.Testing
{
    [TestFixture]
    public class NetMaskTests : TestHelper
    {
        [Test]
        public void Constructor()
        {
            var ip = IPAddress.Parse("255.255.248.0");
            var expected = new NetMask(255, 255, 248, 0);
            var actual = new NetMask(ip);
            Assert.AreEqual(expected, actual);

            var m1 = new NetMask(255, 255, 248, 0);
            var m2 = new NetMask(21);
            Assert.IsTrue(m1 == m2);

            var m3 = new NetMask(Ba(255, 255, 248, 0));
            Assert.IsTrue(m1 == m3);

            var m4 = new NetMask((byte[])null);
            Assert.IsTrue(m4 == NetMask.Empty);

            var m5 = new NetMask(Ba());
            Assert.IsTrue(m5 == NetMask.Empty);

            var m6 = new NetMask((IPAddress)null);
            Assert.IsTrue(m6 == NetMask.Empty);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("ArgumentException Tests")]
        public void ConstructorEx()
        {
            var m3 = new NetMask(Ba(255, 255, 248, 0, 0));
        }

        [Test]
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

        [Test]
        public void Empty()
        {
            Assert.IsTrue(new NetMask(0, 0, 0, 0) == NetMask.Empty);
            Assert.IsTrue(new NetMask(0) == NetMask.Empty);
        }

        [Test]
        public void Length()
        {
            Assert.AreEqual(32, NetMask.MaskLength * 8);
            Assert.AreEqual(32, NetMask.Empty.AddressLength);
        }

        [Test]
        public void Cidr()
        {
            var a = new TestingCollection<byte[], int> {
                new BaITestItem( Ba(0x00, 0x00, 0x00, 0x00), 0),
                new BaITestItem( Ba(0xFF, 0x00, 0x00, 0x00), 8),
                new BaITestItem( Ba(0xFF, 0xFF, 0x00, 0x00), 16),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFF, 0x00), 24),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFF, 0xFF), 32),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFE, 0x00), 23),
                new BaITestItem( Ba(0xFF, 0x80, 0x00, 0x00), 9),
                new BaITestItem( Ba(0xFF, 0xFF, 0x80, 0x00), 17),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFE, 0x00), 23),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFF, 0xF8), 29),
                new BaITestItem( Ba(0xFF, 0xFF, 0xFF, 0xFC), 30)
            };

            foreach (var i in a)
            {
                var nm = new NetMask(i.ToTest1);
                int cidr = nm.Cidr;
                Assert.AreEqual(i.Expected, cidr);
            }
        }

        [Test]
        public void ToStringTest()
        {
            //var m1 = new NetMask(255, 255, 248, 0);
            var m1 = new NetMask(21);
            var expected = "255.255.248.0 (11111111.11111111.11111000.00000000)";
            var str = m1.ToString();
            Assert.AreEqual(expected, str);
        }

        [Test]
        public void GetMaskBytes()
        {
            var actual = Ba(255, 255, 248, 0);
            var m = new NetMask(actual);
            var expected = m.GetMaskBytes();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void EqualsImplementation()
        {
            int a = 2;
            var ip = new IPAddress(0);
            var m = new NetMask(255, 255, 255, 0);
            var m1 = new NetMask(255, 255, 255, 0);

            Assert.IsFalse(m.Equals(a));
            Assert.IsFalse(m.Equals(ip));
            Assert.IsFalse(m.Equals(null));
            Assert.IsFalse(m.Equals((object)null));

            Assert.IsTrue(m.Equals(m));
            Assert.IsTrue(m.Equals(m1));
            Assert.IsTrue(m1.Equals(m));
        }

        [Test]
        public void Or()
        {
            var m1 = new NetMask(255, 255, 248, 0);
            var m2 = new NetMask(255, 255, 0, 0);

            var mOr = m1 | m2;
            Assert.AreEqual(m1, mOr);

            mOr = (NetMask)null | (NetMask)null; // wat
            Assert.AreEqual(NetMask.Empty, mOr);

            mOr = NetMask.BitwiseOr(m1, m2);
            Assert.AreEqual(m1, mOr);

            mOr = NetMask.BitwiseOr(m2, m1);
            Assert.AreEqual(m1, mOr);

            mOr = NetMask.BitwiseOr(m2, null);
            Assert.AreEqual(m2, mOr);

            mOr = NetMask.BitwiseOr(null, m1);
            Assert.AreEqual(m1, mOr);

            mOr = NetMask.BitwiseOr(null, null);
            Assert.AreEqual(NetMask.Empty, mOr);
        }

        [Test]
        public void AndMask()
        {
            var m1 = new NetMask(255, 255, 255, 0);
            var m2 = new NetMask(255, 255, 0, 0);

            var mAnd = m1 & m2;
            Assert.AreEqual(m2, mAnd);

            mAnd = m2 & m1;
            Assert.AreEqual(m2, mAnd);

            mAnd = NetMask.BitwiseAnd(m1, m2);
            Assert.AreEqual(m2, mAnd);

            mAnd = NetMask.BitwiseAnd(m2, m1);
            Assert.AreEqual(m2, mAnd);

            mAnd = NetMask.BitwiseAnd(m2, m2);
            Assert.AreEqual(m2, mAnd);

            mAnd = NetMask.BitwiseAnd(m1, m1);
            Assert.AreEqual(m1, mAnd);


            mAnd = NetMask.BitwiseAnd((NetMask)null, m1);
            Assert.AreEqual(NetMask.Empty, mAnd);

            mAnd = NetMask.BitwiseAnd(m2, (NetMask)null);
            Assert.AreEqual(NetMask.Empty, mAnd);
        }


        [Test]
        public void AndIp()
        {
            var m1 = new NetMask(255, 255, 255, 0);
            var ip1 = new IPAddress(Ba(255, 255, 0, 0));

            var mAnd = m1 & ip1;
            Assert.AreEqual(ip1, mAnd);

            mAnd = ip1 & m1;
            Assert.AreEqual(ip1, mAnd);

            mAnd = NetMask.BitwiseAnd(ip1, m1);
            Assert.AreEqual(ip1, mAnd);

            mAnd = NetMask.BitwiseAnd(m1, ip1);
            Assert.AreEqual(ip1, mAnd);


            mAnd = NetMask.BitwiseAnd(m1, (IPAddress)null);
            Assert.AreEqual(IPAddress.Any, mAnd);

            mAnd = NetMask.BitwiseAnd((NetMask)null, ip1);
            Assert.AreEqual(IPAddress.Any, mAnd);
        }

        [Test]
        public void MaskValidity()
        {
            var b1 = Ba(255, 255, 255, 255);
            var valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(true, valid);

            b1 = Ba(255, 255, 255, 0);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(true, valid);

            b1 = Ba(255, 255, 0, 0);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(true, valid);

            b1 = Ba(255, 0, 0, 0);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(true, valid);

            b1 = Ba(0, 0, 0, 0);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(true, valid);

            b1 = Ba(0, 255, 255, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);

            b1 = Ba(0, 0, 255, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);

            b1 = Ba(0, 0, 0, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);

            b1 = Ba(255, 0, 255, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);

            b1 = Ba(255, 0, 0, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);

            b1 = Ba(255, 255, 0, 255);
            valid = NetMask.GetIsValidNetMask(b1);
            Assert.AreEqual(false, valid);
        }

        [Test]
        public void Expand()
        {
            var expected = new NetMask(1);
            var actual = NetMask.Extend(NetMask.Empty, 1);
            Assert.AreEqual(expected, actual);

            expected = new NetMask(2);
            actual = NetMask.Extend(NetMask.Empty, 2);
            Assert.AreEqual(expected, actual);

            expected = new NetMask(2);
            actual = NetMask.Extend(new NetMask(1), 1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Abbreviate()
        {
            var expected = new NetMask(0);
            var actual = NetMask.Abbreviate(NetMask.Empty, 1);
            Assert.AreEqual(expected, actual);

            expected = new NetMask(3);
            actual = NetMask.Abbreviate(new NetMask(28), 25);
            Assert.AreEqual(expected, actual);

            expected = new NetMask(8);
            actual = NetMask.Abbreviate(new NetMask(9), 1);
            Assert.AreEqual(expected, actual);
        }
    }
}
