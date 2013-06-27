using System.Collections;
using System.Text;

namespace System.Net.Topology
{
    internal static class BitArrayExtensions
    {
        internal static int CountFromLeft(this BitArray bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            int counter = 0;
            for (int i = 0; i < bits.Length; ++i)
            {
                if (bits[i] == value)
                    ++counter;
                else
                    break;
            }

            return counter;
        }

        internal static int CountFromRight(this BitArray bits, bool value)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            int counter = 0;
            for (int i = bits.Length; i >= 0; --i)
            {
                if (bits[i] == value)
                    ++counter;
                else
                    break;
            }
            return counter;
        }

        internal static string ToBinaryString(this BitArray bits, char separator, int separationDistance)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            var sb = new StringBuilder();
            for (int i = 0; i < bits.Length; ++i)
            {
                if (i != 0 && i % separationDistance == 0)
                    sb.Append(separator);
                sb.Append(bits[i] ? '1' : '0');
            }
            return sb.ToString();
        }
        internal static string ToBinaryString(this BitArray bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            var sb = new StringBuilder();
            for (int i = 0; i < bits.Length; ++i)
                sb.Append(bits[i] ? '1' : '0');
            return sb.ToString();
        }

        internal static bool RepresentsValidNetMask(this BitArray bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            bool shouldBeZerosNow = false;
            for(int i = 0; i < bits.Length; ++i)
            {
                if (!bits[i] && !shouldBeZerosNow)
                    shouldBeZerosNow = true;
                if (bits[i] && shouldBeZerosNow)
                    return false;
            }
            return true;
        }
    }
}
