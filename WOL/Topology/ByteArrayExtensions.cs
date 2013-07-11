using System.Collections;
using System.Text;

namespace System.Net.Topology
{
    internal static class ByteArrayExtensions
    {
        internal static int CountFromLeft(this byte[] bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            throw new NotImplementedException();

            /*int counter = 0;
            for (int i = 0; i < bits.Length; ++i)
            {
                if (bits[i] == value)
                    ++counter;
                else
                    break;
            }

            return counter;*/
        }

        internal static int CountFromRight(this byte[] bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            throw new NotImplementedException();

            /*int counter = 0;
            for (int i = bits.Length; i >= 0; --i)
            {
                if (bits[i] == value)
                    ++counter;
                else
                    break;
            }
            return counter;*/
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

            // TODO: Testing?

            int fromLeft = bits.CountFromLeft(true);
            int fromRight = bits.CountFromLeft(false);
            return (fromLeft + fromRight) == (8 * NetMask.MaskLength);
        }

        internal static byte[] And(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            // TODO: Testing

            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 & ib2);
            }
            else
                throw new NotImplementedException();
        }

        internal static byte[] Or(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            // TODO: Testing

            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 | ib2);
            }
            else
                throw new NotImplementedException();
        }

        internal static byte[] Xor(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            // TODO: Testing

            if (b1.Length == 4 && b2.Length == 4)
            {
                var ib1 = BitConverter.ToInt32(b1, 0);
                var ib2 = BitConverter.ToInt32(b2, 0);
                return BitConverter.GetBytes(ib1 ^ ib2);
            }
            else
                throw new NotImplementedException();
        }

        internal static byte[] Not(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("b1");

            // TODO: Testing

            if (bits.Length == 4)
            {
                var i = BitConverter.ToInt32(bits, 0);
                return BitConverter.GetBytes(~i);
            }
            else
            {
                byte[] newBytes = new byte[bits.Length];

                for (int i = 0; i < newBytes.Length; ++i)
                    newBytes[i] = (byte)(~(int)bits[i]);

                return newBytes;
            }
        }
    }
}
