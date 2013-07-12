using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Linq;
using System.Net.Topology;
using System.Diagnostics;
using System.Collections.Generic;

namespace WakeOnLan.Testing
{
    [TestClass]
    public class ByteArrayExtensionsTests
    {
        [TestMethod]
        public void CountFromLeft()
        {
            byte[] mask = new byte[] { 0x00, 0x00, 0x00, 0x01 };
            int bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(31, bitsFalseFromLeft);

            int bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(0, bitsTrueFromLeft);

            mask = new byte[] { 0x00, 0x01, 0x00, 0x01 };
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(15, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(0, bitsTrueFromLeft);

            mask = new byte[] { 0xF0, 0x01, 0x00, 0x01 };
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(4, bitsTrueFromLeft);

            mask = new byte[] { 0xFF, 0x80, 0x00, 0x01 };
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(9, bitsTrueFromLeft);

            mask = new byte[] { 0xFF, 0x80, 0x00, 0x01 };
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(9, bitsTrueFromLeft);
        }

        [TestMethod]
        public void CountFromRight()
        {
            byte[] mask = new byte[] { 0x00, 0x00, 0x00, 0x01 };
            int bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            int bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(1, bitsTrueFromRight);

            mask = new byte[] { 0x80, 0x01, 0x00, 0x00 };
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(16, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(0, bitsTrueFromRight);

            mask = new byte[] { 0xF0, 0x01, 0x00, 0x0F };
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(4, bitsTrueFromRight);

            mask = new byte[] { 0xFF, 0x80, 0x00, 0x00 };
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(23, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(0, bitsTrueFromRight);

            mask = new byte[] { 0x00, 0x01, 0x01, 0xFF };
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(9, bitsTrueFromRight);
        }

        [TestMethod]
        public void BitStreamFromLeft()
        {
            var a = new TestingCollection<byte[], string>
            {
                new BaSTestItem(new byte[] { 0x00, 0x00, 0x00, 0x01 }, "00000000000000000000000000000001"),
                new BaSTestItem(new byte[] { 0x00, 0x00, 0x00, 0x80 }, "00000000000000000000000010000000"),
                new BaSTestItem(new byte[] { 0x80, 0x00, 0x00, 0x80 }, "10000000000000000000000010000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x00, 0x00, 0x80 }, "11111111000000000000000010000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x00, 0x01, 0x80 }, "11111111000000000000000110000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x02, 0x01, 0x80 }, "11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.ToTest;
                var str = item.Expected;
                int i = 0;
                var bits = mask.ToBitStream(true);
                foreach (var b in bits)
                {
                    Debug.Write(b ? '1' : '0');
                    Assert.AreEqual(str[i], b ? '1' : '0');
                    ++i;
                }
                Debug.WriteLine(".");
            }
        }

        [TestMethod]
        public void BitStreamFromRight()
        {
            var a = new TestingCollection<byte[], string> { 
                new BaSTestItem(new byte[] { 0x00, 0x00, 0x00, 0x01 }, "00000000000000000000000000000001"),
                new BaSTestItem(new byte[] { 0x00, 0x00, 0x00, 0x80 }, "00000000000000000000000010000000"),
                new BaSTestItem(new byte[] { 0x80, 0x00, 0x00, 0x80 }, "10000000000000000000000010000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x00, 0x00, 0x80 }, "11111111000000000000000010000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x00, 0x01, 0x80 }, "11111111000000000000000110000000"),
                new BaSTestItem(new byte[] { 0xFF, 0x02, 0x01, 0x80 }, "11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.ToTest;
                var str = item.Expected.Reverse();
                int i = 0;
                var bits = mask.ToBitStream(false);
                foreach (var b in bits)
                {
                    Debug.Write(b ? '1' : '0');
                    Assert.AreEqual(str[i], b ? '1' : '0');
                    ++i;
                }
                Debug.WriteLine(".");
            }
        }

        [TestMethod]
        public void RepresentsValidNetMask()
        {
            var a = new TestingCollection<byte[], bool> {
                new BaBTestItem(new byte[] {255,255,255,0}, true),
                new BaBTestItem(new byte[] {255,255,255,1}, false),
                new BaBTestItem(new byte[] {255,255,255,248}, true),
                new BaBTestItem(new byte[] {255,255,255,249}, false),
                new BaBTestItem(new byte[] {255,255,255,240}, true),
                new BaBTestItem(new byte[] {255,255,255,242}, false),
                new BaBTestItem(new byte[] {255,255,0,0}, true),
                new BaBTestItem(new byte[] {255,255,1,0}, false),
                new BaBTestItem(new byte[] {255,0,255,242}, false),
                new BaBTestItem(new byte[] {0,255,255,255}, false),
                new BaBTestItem(new byte[] {0,0,255,242}, false)
            };
            int index = 0;
            foreach (var i in a)
            {
                bool isValid = i.ToTest.RepresentsValidNetMask();
                Debug.WriteLine("Testing " + BitConverter.ToString(i.ToTest));
                Assert.AreEqual(i.Expected, isValid);
                ++index;
            }
        }

        [TestMethod]
        public void Not()
        {
            var a = new TestingCollection<byte[], byte[]> {
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0x00}, new byte[]{0x00, 0x00, 0x00, 0xFF}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0x01}, new byte[]{0x00, 0x00, 0x00, 0xFE}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0xF0}, new byte[]{0x00, 0x00, 0x00, 0x0F}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0xF9}, new byte[]{0x00, 0x00, 0x00, 0x06}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0xF0}, new byte[]{0x00, 0x00, 0x00, 0x0F}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0xFF, 0xF2}, new byte[]{0x00, 0x00, 0x00, 0x0D}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0x00, 0x00}, new byte[]{0x00, 0x00, 0xFF, 0xFF}),
                new BaBaTestItem(new byte[] {0xFF, 0xFF, 0x01, 0x00}, new byte[]{0x00, 0x00, 0xFE, 0xFF}),
                new BaBaTestItem(new byte[] {0xFF, 0x00, 0xFF, 0xF2}, new byte[]{0x00, 0xFF, 0x00, 0x0D}),
                new BaBaTestItem(new byte[] {0x00, 0xFF, 0xFF, 0xFF}, new byte[]{0xFF, 0x00, 0x00, 0x00}),
                new BaBaTestItem(new byte[] {0x00, 0x00, 0xFF, 0xF2}, new byte[]{0xFF, 0xFF, 0x00, 0x0D}),
                new BaBaTestItem(new byte[] {0xAA, 0xAA, 0xAA, 0xAA}, new byte[]{0x55, 0x55, 0x55, 0x55})
            };
            foreach (var i in a)
            {
                byte[] inversed = i.ToTest.Not();
                Debug.WriteLine("Testing: " + BitConverter.ToString(i.Expected) + "\r\n     --> " + BitConverter.ToString(inversed));
                Assert.IsTrue(i.Expected.SequenceEqual(inversed));
            }
        }
    }
}