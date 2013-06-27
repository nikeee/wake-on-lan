using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var sb = new StringBuilder();
            for (int i = 0; i < bits.Length; ++i)
                sb.Append(bits[i] ? '1' : '0');
            return sb.ToString();
        }
    }
}
