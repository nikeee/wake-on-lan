using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
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
            var a = new List<KeyValuePair<byte[], string>>
            {
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x00, 0x00, 0x00, 0x01 },
                	"00000000000000000000000000000001"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x00, 0x00, 0x00, 0x80 },
                	"00000000000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x80, 0x00, 0x00, 0x80 },
                	"10000000000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x00, 0x00, 0x80 },
                	"11111111000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x00, 0x01, 0x80 },
                	"11111111000000000000000110000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x02, 0x01, 0x80 },
                	"11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.Key;
                var str = item.Value;
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
            var a = new List<KeyValuePair<byte[], string>>
            {
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x00, 0x00, 0x00, 0x01 },
                	"00000000000000000000000000000001"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x00, 0x00, 0x00, 0x80 },
                	"00000000000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0x80, 0x00, 0x00, 0x80 },
                	"10000000000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x00, 0x00, 0x80 },
                	"11111111000000000000000010000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x00, 0x01, 0x80 },
                	"11111111000000000000000110000000"),
                new KeyValuePair<byte[], string>(new byte[] 
                	{ 0xFF, 0x02, 0x01, 0x80 },
                	"11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.Key;
                var str = item.Value.Reverse();
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
    }
}