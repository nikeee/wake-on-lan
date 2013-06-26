using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace System.Net.Topology
{
    public class NetMaskv4 : NetMask // Well there hopefully won't be a V6 version, but who knows.
    {
        private BitArray _bits;

        #region Ctors

        public NetMaskv4()
        {
            _bits = new BitArray(32);
        }

        public NetMaskv4(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                // maybe throw ArgumentNullException?
                _bits = new BitArray(32);
                return;
            }

            if (bytes.Length != 4)
                throw new ArgumentException("Invalid mask length.");

            _bits = new BitArray(bytes);
        }

        public NetMaskv4(IPAddress ipAddress)
            : this(ipAddress == null ? null : ipAddress.GetAddressBytes())
        { }

        #endregion
    }
}
