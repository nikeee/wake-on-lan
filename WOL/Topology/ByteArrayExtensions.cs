using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Net.Topology
{
    internal static class ByteArrayExtensions
    {
        internal static IEnumerable<bool> ToBitStream(this byte[] bytes, bool fromLeft)
        {
            if (bytes == null)
                throw new ArgumentNullException();

            if (fromLeft)
            {
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte tmp = bytes[i].ReverseBits();
                    for (int j = 0; j < 8; ++j)
                    {
                        yield return (tmp & 1) == 1;
                        tmp >>= 1;
                    }
                }
            }
            else
            {
                for (int i = bytes.Length - 1; i >= 0; --i)
                {
                    byte tmp = bytes[i];
                    for (int j = 0; j < 8; ++j)
                    {
                        yield return (tmp & 1) == 1;
                        tmp >>= 1;
                    }
                }
            }
        }

        private static int CountFromSide(byte[] bits, bool value, bool fromleft)
        {
            int counter = 0;
            var str = bits.ToBitStream(fromleft);
            foreach (var bit in str)
            {
                if (bit == value)
                    ++counter;
                else return counter;
            }
            return counter;
        }

        internal static int CountFromLeft(this byte[] bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");
            return CountFromSide(bits, value, true);
        }

        internal static int CountFromRight(this byte[] bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");
            return CountFromSide(bits, value, false);
        }

        internal static string ToBinaryString(this byte[] bits, char separator)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            const int radix = 2;
            const int padding = 8;
            const char paddingChar = '0';

            var sb = new StringBuilder();
            sb.Append(Convert.ToString(bits[0], radix).PadLeft(padding, paddingChar)).Append(separator);
            sb.Append(Convert.ToString(bits[1], radix).PadLeft(padding, paddingChar)).Append(separator);
            sb.Append(Convert.ToString(bits[2], radix).PadLeft(padding, paddingChar)).Append(separator);
            sb.Append(Convert.ToString(bits[3], radix).PadLeft(padding, paddingChar));
            return sb.ToString();
        }
        
        internal static string ToBinaryString(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            const int radix = 2;
            const int padding = 8;
            const char paddingChar = '0';

            var sb = new StringBuilder();
            sb.Append(Convert.ToString(bits[0], radix).PadLeft(padding, paddingChar));
            sb.Append(Convert.ToString(bits[1], radix).PadLeft(padding, paddingChar));
            sb.Append(Convert.ToString(bits[2], radix).PadLeft(padding, paddingChar));
            sb.Append(Convert.ToString(bits[3], radix).PadLeft(padding, paddingChar));
            return sb.ToString();
        }

        internal static bool RepresentsValidNetMask(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            int fromLeft = bits.CountFromLeft(true);
            int fromRight = bits.CountFromRight(false);

            // Sum of all counted indexes schloud be equal the whole length
            return (fromLeft + fromRight) == (8 * NetMask.MaskLength);
        }

        internal static byte[] And(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            if (b1.Length == 1 && b2.Length == 1)
            {
                int ib1 = (int)b1[0];
                int ib2 = (int)b2[0];
                return new byte[] { (byte)(ib1 & ib2) };
            }
            if (b1.Length == 2 && b2.Length == 2)
            {
                var sb1 = BitConverter.ToInt16(b1, 0);
                var sb2 = BitConverter.ToInt16(b2, 0);
                return BitConverter.GetBytes((short)(sb1 & sb2));
            }
            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 & ib2);
            }
            if (b1.Length != b2.Length)
            {
                // Or maybe throw exception?

                int maxIndex = Math.Max(b1.Length, b2.Length);
                byte[] biggerArray = b1.Length > b2.Length ? b1 : b2;
                byte[] smallerArray = b1.Length <= b2.Length ? b1 : b2;

                byte[] paddedArray = new byte[maxIndex];

                Buffer.BlockCopy(smallerArray, 0, paddedArray, 0, smallerArray.Length);

                Debug.Assert(biggerArray.Length == paddedArray.Length);

                return And(biggerArray, paddedArray);
            }

            var targetIndex = b1.Length;
            var andedArray = new byte[targetIndex];
            Buffer.BlockCopy(b1, 0, andedArray, 0, andedArray.Length);

            for (int i = 0; i < targetIndex; ++i)
                andedArray[i] &= b2[i];

            return andedArray;
        }

        internal static byte[] Or(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");
            
            if (b1.Length == 1 && b2.Length == 1)
            {
                int ib1 = (int)b1[0];
                int ib2 = (int)b2[0];
                return new byte[] { (byte)(ib1 | ib2) };
            }
            if (b1.Length == 2 && b2.Length == 2)
            {
                var sb1 = BitConverter.ToInt16(b1, 0);
                var sb2 = BitConverter.ToInt16(b2, 0);
                return BitConverter.GetBytes((short)(sb1 | sb2));
            }
            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 | ib2);
            }
            if(b1.Length != b2.Length)
            {
                // Or maybe throw exception?

                int maxIndex = Math.Max(b1.Length, b2.Length);
                byte[] biggerArray = b1.Length > b2.Length ? b1 : b2;
                byte[] smallerArray = b1.Length <= b2.Length ? b1 : b2;
                
                byte[] paddedArray = new byte[maxIndex];

                Buffer.BlockCopy(smallerArray, 0, paddedArray, 0, smallerArray.Length);

                Debug.Assert(biggerArray.Length == paddedArray.Length);

                return Or(biggerArray, paddedArray);
            }

            var targetIndex = b1.Length;
            var oredArray = new byte[targetIndex];
            Buffer.BlockCopy(b1, 0, oredArray, 0, oredArray.Length);

            for (int i = 0; i < targetIndex; ++i)
                oredArray[i] |= b2[i];

            return oredArray;
        }

        internal static byte[] Xor(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            // TODO: Testing

            if (b1.Length == 1 && b2.Length == 1)
            {
                int ib1 = (int)b1[0];
                int ib2 = (int)b2[0];
                return new byte[] { (byte)(ib1 ^ ib2) };
            }
            if (b1.Length == 2 && b2.Length == 2)
            {
                var sb1 = BitConverter.ToInt16(b1, 0);
                var sb2 = BitConverter.ToInt16(b2, 0);
                return BitConverter.GetBytes((short)(sb1 ^ sb2));
            }
            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 ^ ib2);
            }
            if (b1.Length != b2.Length)
            {
                // Or maybe throw exception?

                int maxIndex = Math.Max(b1.Length, b2.Length);
                byte[] biggerArray = b1.Length > b2.Length ? b1 : b2;
                byte[] smallerArray = b1.Length <= b2.Length ? b1 : b2;
                
                byte[] paddedArray = new byte[maxIndex];

                Buffer.BlockCopy(smallerArray, 0, paddedArray, 0, smallerArray.Length);

                Debug.Assert(biggerArray.Length == paddedArray.Length);

                return Xor(biggerArray, paddedArray);
            }

            var targetIndex = b1.Length;
            var xoredArray = new byte[targetIndex];
            Buffer.BlockCopy(b1, 0, xoredArray, 0, xoredArray.Length);

            for (int i = 0; i < targetIndex; ++i)
                xoredArray[i] ^= b2[i];

            return xoredArray;
        }

        internal static byte[] Not(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("b1");

            if (bits.Length == 4)
            {
                var i = BitConverter.ToInt32(bits, 0);
                return BitConverter.GetBytes(~i);
            }
            else
            {
            Debugger.Break();
                byte[] newBytes = new byte[bits.Length];

                for (int i = 0; i < newBytes.Length; ++i)
                    newBytes[i] = (byte)(~(int)bits[i]);

                return newBytes;
            }
        }
    }

    internal static class ByteExtensions
    {
        public static byte[] BitReverseTable = {
            0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0,
            0x10, 0x90, 0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0,
            0x08, 0x88, 0x48, 0xc8, 0x28, 0xa8, 0x68, 0xe8,
            0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 0x78, 0xf8,
            0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
            0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4,
            0x0c, 0x8c, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec,
            0x1c, 0x9c, 0x5c, 0xdc, 0x3c, 0xbc, 0x7c, 0xfc,
            0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2,
            0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
            0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea,
            0x1a, 0x9a, 0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa,
            0x06, 0x86, 0x46, 0xc6, 0x26, 0xa6, 0x66, 0xe6,
            0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
            0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
            0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe,
            0x01, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1,
            0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
            0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9,
            0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
            0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5,
            0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
            0x0d, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed,
            0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
            0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
            0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
            0x0b, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb,
            0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
            0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7,
            0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
            0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef,
            0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
        };

        public static byte ReverseBits(this byte toReverse)
        {
            return BitReverseTable[toReverse];
        }
    }
}