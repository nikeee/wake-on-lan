using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Topology;
using System.Collections;
using System.Linq;
using System.Text;

namespace WakeOnLan.Testing
{
    [TestClass]
    public class BitArrayTests
    {
        internal static string ToBinaryString(BitArray bits, char separator, int separationDistance)
        {
            if (bits == null)
                throw new ArgumentNullException("bits");

            var sb = new StringBuilder();
            for (int i = bits.Length-1; i >= 0; --i)
            {
                if (i != 0 && i % separationDistance == 0)
                    sb.Append(separator);
                sb.Append(bits[i] ? '1' : '0');
            }
            return sb.ToString();
        }


    }
}
