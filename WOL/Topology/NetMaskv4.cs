using System.Text;
using System.Collections;

namespace System.Net.Topology
{
    /// <summary>Represents an IPv4 net mask.</summary>
    public sealed class NetMaskv4 : INetMask, IEquatable<NetMaskv4>
    {
        private BitArray _bits;

        /// <summary>Represents an empty IPv4 NetMask (all bits set to 0).</summary>
        public static readonly NetMaskv4 Empty = new NetMaskv4();

        /// <summary>Gets the length of the net mask in bits.</summary>
        public int AddressLength { get { return 32; } }

        /// <summary>Gets the amount of set bits from the left side (used in CIDR-Notation of net masks).</summary>
        public int Cidr
        {
            get
            {
                System.Diagnostics.Debug.Assert(_bits.Count == 32);
                return _bits.CountFromLeft(true);
            }
        }

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

        public NetMaskv4(BitArray mask)
        {
            if (mask == null)
                throw new ArgumentNullException("mask");
            if (mask.Count != 32)
                throw new ArgumentException("Invalid mask length.");

            _bits = new BitArray(mask);
        }

        #endregion

        /// <summary>Gets the bits of the net mask instance as an BitArray object instance.</summary>
        /// <returns>The bits of the net mask instance as an BitArray object instance</returns>
        public BitArray GetBits()
        {
            return _bits;
        }

        #region Operators

        #region Equality

        public static bool operator ==(NetMaskv4 a, NetMaskv4 b)
        {
            if (Object.ReferenceEquals(a, b))
                return true;
            if (((object)a == null) || ((object)b == null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(NetMaskv4 a, NetMaskv4 b)
        {
            return !(a == b); // Problem solved
        }

        #endregion
        #region And

        public static IPAddress operator &(IPAddress address, NetMaskv4 mask)
        {
            return mask & address;
        }

        public static IPAddress operator &(NetMaskv4 mask, IPAddress address)
        {
            throw new NotImplementedException();
        }

        public static NetMaskv4 operator &(NetMaskv4 a, NetMaskv4 b)
        {
            if (a == null || b == null)
                return NetMaskv4.Empty;
            return new NetMaskv4(a._bits.And(b._bits));
        }

        #endregion
        #region Or

        public static NetMaskv4 operator |(NetMaskv4 a, NetMaskv4 b)
        {
            if (a == null || b == null)
                return NetMaskv4.Empty;
            return new NetMaskv4(a._bits.Or(b._bits));
        }

        #endregion

        #endregion
        #region Common overrides

        public override string ToString()
        {
            var sb = new StringBuilder(4 * 3 + 3 * 3 + 32 + 3 + 3); // 255.255.255.255 (11111111111111111111111111111111)

            var arr = new byte[4];
            _bits.CopyTo(arr, 0);
            var asString = _bits.ToBinaryString('.', 4);

            sb.Append(arr[0]).Append('.').Append(arr[1]).Append('.').Append(arr[2]).Append('.').Append(arr[3]).Append("( ");
            sb.Append(asString).Append(')');

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var mask = obj as NetMaskv4;
            if ((object)mask == null)
                return false;

            return Equals(mask);
        }

        public bool Equals(NetMaskv4 other)
        {
            if (other == null)
                return false;

            if (other._bits.Count != 32)
                return false;
            if (other._bits.Count != _bits.Count)
                return false;

            for (int i = 0; i < _bits.Count; ++i)
                if (_bits[i] != other._bits[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return _bits.GetHashCode();
        }

        #endregion
    }
}
