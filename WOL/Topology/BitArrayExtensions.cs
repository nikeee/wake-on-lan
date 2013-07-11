using System.Collections;
using System.Text;

namespace System.Net.Topology
{
    internal static class BitArrayExtensions
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

        internal static string ToBinaryString(this byte[] bits, char separator, int separationDistance)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            throw new NotImplementedException();

            /*var sb = new StringBuilder();
            for (int i = bits.Length - 1; i >= 0; --i)
            {
                if (i != 0 && i % separationDistance == 0)
                    sb.Append(separator);
                sb.Append(bits[i] ? '1' : '0');
            }
            return sb.ToString().Reverse();*/
        }
        internal static string ToBinaryString(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            throw new NotImplementedException();

            /*var sb = new StringBuilder();
            for (int i = bits.Length - 1; i >= 0; --i)
                sb.Append(bits[i] ? '1' : '0');
            return sb.ToString().Reverse();*/
        }

        internal static bool RepresentsValidNetMask(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            throw new NotImplementedException();
            /*
            bool shouldBeZerosNow = false;
            for(int i = 0; i < bits.Length; ++i)
            {
                if (!bits[i] && !shouldBeZerosNow)
                    shouldBeZerosNow = true;
                if (bits[i] && shouldBeZerosNow)
                    return false;
            }
            return true;
             * */
        }

        internal static byte[] And(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            throw new NotImplementedException();
        }
        internal static byte[] Or(this byte[] b1, byte[] b2)
        {
            if (b1 == null)
                throw new ArgumentNullException("b1");
            if (b2 == null)
                throw new ArgumentNullException("b2");

            throw new NotImplementedException();
        }
        internal static byte[] Not(this byte[] bits)
        {
            if (bits == null)
                throw new ArgumentNullException("b1");

            throw new NotImplementedException();
        }
    }
}
