using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Topology
{
    public interface INetMask
    {
        int AddressLength { get; }
        int Cidr { get; }

        BitArray GetBits();
    }
}
