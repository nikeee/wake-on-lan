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
    public class ByteArrayExtensionsTests : TestHelper
    {
        [TestMethod]
        public void CountFromLeft()
        {
            byte[] mask = Ba(0x00, 0x00, 0x00, 0x01);
            int bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(31, bitsFalseFromLeft);

            int bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(0, bitsTrueFromLeft);

            mask = Ba(0x00, 0x01, 0x00, 0x01);
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(15, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(0, bitsTrueFromLeft);

            mask = Ba(0xF0, 0x01, 0x00, 0x01);
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(4, bitsTrueFromLeft);

            mask = Ba(0xFF, 0x80, 0x00, 0x01);
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(9, bitsTrueFromLeft);

            mask = Ba(0xFF, 0x80, 0x00, 0x01);
            bitsFalseFromLeft = mask.CountFromLeft(false);
            Assert.AreEqual(0, bitsFalseFromLeft);

            bitsTrueFromLeft = mask.CountFromLeft(true);
            Assert.AreEqual(9, bitsTrueFromLeft);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void CounFromLeftEx()
        {
            ByteArrayExtensions.CountFromLeft(null, true);
        }

        [TestMethod]
        public void CountFromRight()
        {
            byte[] mask = Ba(0x00, 0x00, 0x00, 0x01);
            int bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            int bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(1, bitsTrueFromRight);

            mask = Ba(0x80, 0x01, 0x00, 0x00);
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(16, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(0, bitsTrueFromRight);

            mask = Ba(0xF0, 0x01, 0x00, 0x0F);
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(4, bitsTrueFromRight);

            mask = Ba(0xFF, 0x80, 0x00, 0x00);
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(23, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(0, bitsTrueFromRight);

            mask = Ba(0x00, 0x01, 0x01, 0xFF);
            bitsFalseFromRight = mask.CountFromRight(false);
            Assert.AreEqual(0, bitsFalseFromRight);

            bitsTrueFromRight = mask.CountFromRight(true);
            Assert.AreEqual(9, bitsTrueFromRight);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void CountFromRightEx()
        {
            ByteArrayExtensions.CountFromRight(null, true);
        }

        [TestMethod]
        public void BitStreamFromLeft()
        {
            var a = new TestingCollection<byte[], string>
            {
                new BaSTestItem(Ba( 0x00, 0x00, 0x00, 0x01 ), "00000000000000000000000000000001"),
                new BaSTestItem(Ba( 0x00, 0x00, 0x00, 0x80 ), "00000000000000000000000010000000"),
                new BaSTestItem(Ba( 0x80, 0x00, 0x00, 0x80 ), "10000000000000000000000010000000"),
                new BaSTestItem(Ba( 0xFF, 0x00, 0x00, 0x80 ), "11111111000000000000000010000000"),
                new BaSTestItem(Ba( 0xFF, 0x00, 0x01, 0x80 ), "11111111000000000000000110000000"),
                new BaSTestItem(Ba( 0xFF, 0x02, 0x01, 0x80 ), "11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.ToTest1;
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
                new BaSTestItem(Ba (0x00, 0x00, 0x00, 0x01 ), "00000000000000000000000000000001"),
                new BaSTestItem(Ba (0x00, 0x00, 0x00, 0x80 ), "00000000000000000000000010000000"),
                new BaSTestItem(Ba (0x80, 0x00, 0x00, 0x80 ), "10000000000000000000000010000000"),
                new BaSTestItem(Ba (0xFF, 0x00, 0x00, 0x80 ), "11111111000000000000000010000000"),
                new BaSTestItem(Ba (0xFF, 0x00, 0x01, 0x80 ), "11111111000000000000000110000000"),
                new BaSTestItem(Ba (0xFF, 0x02, 0x01, 0x80 ), "11111111000000100000000110000000")
            };

            foreach (var item in a)
            {
                byte[] mask = item.ToTest1;
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
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void BitStreamFromRightEx()
        {
            var res = ByteArrayExtensions.ToBitStream(null, false);
            foreach (var item in res)
            {
                // lolexception
            }
        }

        [TestMethod]
        public void RepresentsValidNetMask()
        {
            var a = new TestingCollection<byte[], bool> {
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0x00), true),
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0x01), false),
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF8), true),
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF9), false),
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF0), true),
                new BaBTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF2), false),
                new BaBTestItem(Ba(0xFF, 0xFF, 0x00, 0x00), true),
                new BaBTestItem(Ba(0xFF, 0xFF, 0x01, 0x00), false),
                new BaBTestItem(Ba(0xFF, 0x00, 0xFF, 0xF2), false),
                new BaBTestItem(Ba(0x00, 0xFF, 0xFF, 0xFF), false),
                new BaBTestItem(Ba(0x00, 0x00, 0xFF, 0xF2), false)
            };
            int index = 0;
            foreach (var i in a)
            {
                bool isValid = i.ToTest1.RepresentsValidNetMask();
                Debug.WriteLine("Testing " + BitConverter.ToString(i.ToTest1));
                Assert.AreEqual(i.Expected, isValid);
                ++index;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void RepresentsValidNetMaskEx()
        {
            ByteArrayExtensions.RepresentsValidNetMask(null);
        }

        [TestMethod]
        public void ToBinaryString()
        {
            byte[] mask = Ba(0x00, 0x00, 0x00, 0x01);
            string expected = "00000000000000000000000000000001";
            var str = mask.ToBinaryString();
            Assert.AreEqual(expected, str);

            expected = "00000000.00000000.00000000.00000001";
            str = mask.ToBinaryString('.');
            Assert.AreEqual(expected, str);

            mask = Ba(0x00, 0xF0, 0x04, 0xF1);
            expected = "00000000111100000000010011110001";
            str = mask.ToBinaryString();
            Assert.AreEqual(expected, str);

            expected = "00000000.11110000.00000100.11110001";
            str = mask.ToBinaryString('.');
            Assert.AreEqual(expected, str);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void ToBinaryStringEx()
        {
            ByteArrayExtensions.ToBinaryString(null, '.');
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void ToBinaryStringEx1()
        {
            ByteArrayExtensions.ToBinaryString(null);
        }

        [TestMethod]
        public void Not()
        {

            var a = new TestingCollection<byte[], byte[]> {
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0x00), Ba(0x00, 0x00, 0x00, 0xFF)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0x01), Ba(0x00, 0x00, 0x00, 0xFE)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF0), Ba(0x00, 0x00, 0x00, 0x0F)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF9), Ba(0x00, 0x00, 0x00, 0x06)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF0), Ba(0x00, 0x00, 0x00, 0x0F)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0xFF, 0xF2), Ba(0x00, 0x00, 0x00, 0x0D)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0x00, 0x00), Ba(0x00, 0x00, 0xFF, 0xFF)),
                new BaBaTestItem(Ba(0xFF, 0xFF, 0x01, 0x00), Ba(0x00, 0x00, 0xFE, 0xFF)),
                new BaBaTestItem(Ba(0xFF, 0x00, 0xFF, 0xF2), Ba(0x00, 0xFF, 0x00, 0x0D)),
                new BaBaTestItem(Ba(0x00, 0xFF, 0xFF, 0xFF), Ba(0xFF, 0x00, 0x00, 0x00)),
                new BaBaTestItem(Ba(0x00, 0x00, 0xFF, 0xF2), Ba(0xFF, 0xFF, 0x00, 0x0D)),
                new BaBaTestItem(Ba(0xAA, 0xAA, 0xAA, 0xAA), Ba(0x55, 0x55, 0x55, 0x55)),
                new BaBaTestItem(Ba(0x0F, 0x00, 0x10), Ba(0xF0, 0xFF, 0xEF)),
                new BaBaTestItem(Ba(0x0F, 0xF0), Ba(0xF0, 0x0F)),
                new BaBaTestItem(Ba(0x1F, 0x13), Ba(0xE0, 0xEC)),
                new BaBaTestItem(Ba(0x00), Ba(0xFF)),
                new BaBaTestItem(Ba(0x0F), Ba(0xF0))
            };
            foreach (var i in a)
            {
                byte[] inversed = i.ToTest1.Not();
                Debug.WriteLine("Testing: " + BitConverter.ToString(i.Expected) + "\r\n     --> " + BitConverter.ToString(inversed));
                Assert.AreEqual(i.Expected.Length, inversed.Length);
                Assert.IsTrue(i.Expected.SequenceEqual(inversed), BitConverter.ToString(inversed) + " != " + BitConverter.ToString(i.Expected));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void NotEx()
        {
            var re = ByteArrayExtensions.Not(null);
        }

        [TestMethod]
        public void Or()
        {
            var a = new TestingCollection<byte[], byte[], byte[]>
            {
                new BaBaBaTestItem(Ba(0x01), Ba(0x00), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x01), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x00), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x01), Ba(0x01), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x00), Ba(0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x00, 0x00), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x00, 0x00), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x01, 0x01), Ba(0x01, 0x00), Ba(0x01, 0x01)),
                new BaBaBaTestItem(Ba(0xF0, 0x00), Ba(0x0F, 0x01), Ba(0xFF, 0x01)),
                new BaBaBaTestItem(Ba(0x80, 0xF1), Ba(0x00, 0x0E), Ba(0x80, 0xFF)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00), Ba(0x00, 0x00, 0x00), Ba(0x00, 0xF1, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x01), Ba(0x00, 0x00, 0x00, 0x00), Ba(0x00, 0xF1, 0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0x0E, 0x00, 0x00), Ba(0x00, 0xFF, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x01, 0xF0), Ba(0x00, 0x00, 0x0E, 0x00), Ba(0x00, 0xF1, 0x0F, 0xF0)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x0F), Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0xF1, 0x00, 0x0F))
            };

            foreach (var i in a)
            {
                byte[] actual = i.ToTest1.Or(i.ToTest2);
                Debug.WriteLine("Testing: " + BitConverter.ToString(i.ToTest1));
                Debug.WriteLine("       | " + BitConverter.ToString(i.ToTest2));
                Debug.WriteLine("     --> " + BitConverter.ToString(actual));
                Assert.AreEqual(i.Expected.Length, actual.Length);
                Assert.IsTrue(i.Expected.SequenceEqual(actual));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void OrEx()
        {
            byte[] b1 = null;
            byte[] b2 = new byte[0];
            byte[] re = ByteArrayExtensions.Or(b1, b2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void OrEx1()
        {
            byte[] b1 = new byte[0];
            byte[] b2 = null;
            byte[] re = ByteArrayExtensions.Or(b1, b2);
        }

        [TestMethod]
        public void And()
        {
            var a = new TestingCollection<byte[], byte[], byte[]>
            {
                new BaBaBaTestItem(Ba(0x01), Ba(0x00), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x01), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x00), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x01), Ba(0x01), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x00), Ba(0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x01), Ba(0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x01, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x00, 0x00), Ba(0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x41, 0x00), Ba(0x4F, 0x01), Ba(0x41, 0x00)),
                new BaBaBaTestItem(Ba(0x01, 0x01), Ba(0x01, 0x00), Ba(0x01, 0x00)),
                new BaBaBaTestItem(Ba(0xF0, 0x0F), Ba(0x0F, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x80, 0xF1), Ba(0x00, 0x0F), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00), Ba(0x00, 0x80, 0x00), Ba(0x00, 0x80, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x01), Ba(0x00, 0x00, 0x00, 0x00), Ba(0x00, 0x00, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0x0E, 0x00, 0x00), Ba(0x00, 0x00, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x01, 0xF0), Ba(0x00, 0xF0, 0x0E, 0x00), Ba(0x00, 0xF0, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0xF0), Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0xF1, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0xF0), Ba(0x00, 0xF1, 0x00), Ba(0x00, 0xF1, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0xF0), Ba(0x00, 0xF1), Ba(0x00, 0xF1, 0x00, 0x00))
            };

            foreach (var i in a)
            {
                byte[] actual = i.ToTest1.And(i.ToTest2);
                Debug.WriteLine("Testing: " + BitConverter.ToString(i.ToTest1));
                Debug.WriteLine("       & " + BitConverter.ToString(i.ToTest2));
                Debug.WriteLine("     --> " + BitConverter.ToString(actual));
                Assert.AreEqual(i.Expected.Length, actual.Length);
                Assert.IsTrue(i.Expected.SequenceEqual(actual));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void AndEx()
        {
            byte[] b1 = null;
            byte[] b2 = new byte[0];
            byte[] re = ByteArrayExtensions.And(b1, b2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void AndEx1()
        {
            byte[] b1 = new byte[0];
            byte[] b2 = null;
            byte[] re = ByteArrayExtensions.And(b1, b2);
        }

        [TestMethod]
        public void Xor()
        {
            var a = new TestingCollection<byte[], byte[], byte[]>
            {
                new BaBaBaTestItem(Ba(0x01), Ba(0x00), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x01), Ba(0x01)),
                new BaBaBaTestItem(Ba(0x00), Ba(0x00), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x01), Ba(0x01), Ba(0x00)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x00), Ba(0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x00, 0x00), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x01), Ba(0x00, 0x00), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0x00), Ba(0x00, 0x01), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x01, 0x01), Ba(0x01, 0x00), Ba(0x00, 0x01)),
                new BaBaBaTestItem(Ba(0xF0, 0x00), Ba(0x0F, 0x01), Ba(0xFF, 0x01)),
                new BaBaBaTestItem(Ba(0x80, 0xF1), Ba(0x00, 0x0E), Ba(0x80, 0xFF)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00), Ba(0x00, 0x00, 0x00), Ba(0x00, 0xF1, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x01), Ba(0x00, 0x00, 0x00, 0x00), Ba(0x00, 0xF1, 0x00, 0x01)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0x0E, 0x00, 0x00), Ba(0x00, 0xFF, 0x00, 0x00)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x01, 0xF0), Ba(0x00, 0x00, 0x0E, 0x00), Ba(0x00, 0xF1, 0x0F, 0xF0)),
                new BaBaBaTestItem(Ba(0x00, 0xF1, 0x00, 0x0F), Ba(0x00, 0xF1, 0x00, 0x00), Ba(0x00, 0x00, 0x00, 0x0F))
            };

            foreach (var i in a)
            {
                byte[] actual = i.ToTest1.Xor(i.ToTest2);
                Debug.WriteLine("Testing: " + BitConverter.ToString(i.ToTest1));
                Debug.WriteLine("       ^ " + BitConverter.ToString(i.ToTest2));
                Debug.WriteLine("     --> " + BitConverter.ToString(actual));
                Assert.AreEqual(i.Expected.Length, actual.Length);
                Assert.IsTrue(i.Expected.SequenceEqual(actual));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void XorEx()
        {
            byte[] b1 = null;
            byte[] b2 = new byte[0];
            byte[] re = ByteArrayExtensions.Xor(b1, b2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("ArgumentNullException Tests")]
        public void XorEx1()
        {
            byte[] b1 = new byte[0];
            byte[] b2 = null;
            byte[] re = ByteArrayExtensions.Xor(b1, b2);
        }
    }
}