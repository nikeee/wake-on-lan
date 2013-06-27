using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace System.Net.Topology
{
    public sealed class NetMaskv4 : INetMask
    {
        private BitArray _bits;

        int AddressLength { get { return 32; } }
        public int Cidr
        {
            get
            {
                System.Diagnostics.Debug.Assert(_bits.Count == 32);
                return _bits.CountFromLeft(true);
            }
        }
        public BitArray Bits { get { return _bits; } }

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

        public NetMaskv4(byte a, byte b, byte c, byte d)
            : this(new byte[4] { a, b, c, d })
        { }

        public NetMaskv4(int mask) // uint is not CLS-compliant, so int will do the job.
        {
            _bits = new BitArray(mask);
        }

        #endregion

        #region Common overrides

        public override string ToString()
        {
            var sb = new StringBuilder(4 * 3 + 3 * 3 + 32 + 3 + 3); // 255.255.255.255 (11111111111111111111111111111111)

            var arr = new byte[4];
            _bits.CopyTo(arr, 0);

            sb.Append(arr[0]).Append('.').Append(arr[1]).Append('.').Append(arr[2]).Append('.').Append(arr[3]).Append("( ");
            sb.Append(_bits.ToBinaryString('.', 4)).Append(')');

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return _bits.GetHashCode();
        }

        #endregion
    }
}
