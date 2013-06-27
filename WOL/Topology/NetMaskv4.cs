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

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/> with all bits set to 0.</summary>
        public NetMaskv4()
        {
            _bits = new BitArray(32);
        }

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/> from an array of <see cref="System.Byte"/>.</summary>
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

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/> from a given <see cref="T:System.Net.IPAddress"/>.</summary>
        /// <param name="ipAddress">The IPv4 address.</param>
        public NetMaskv4(IPAddress ipAddress)
            : this(ipAddress == null ? null : ipAddress.GetAddressBytes())
        { }

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/>.</summary>
        /// <param name="a">The first byte.</param>
        /// <param name="b">The second byte.</param>
        /// <param name="c">The third byte.</param>
        /// <param name="d">The fourth byte.</param>
        public NetMaskv4(byte a, byte b, byte c, byte d)
            : this(new byte[4] { a, b, c, d })
        { }

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/>.</summary>
        /// <param name="mask">The mask represented by a 32-Bit integer.</param>
        public NetMaskv4(int mask) // uint is not CLS-compliant, so int will do the job.
        {
            _bits = new BitArray(mask);
        }

        /// <summary>Creates a new instance of <see cref="T:System.Net.Topology.NetMaskv4"/> using a <see cref="T:System.Collections.BitArray"/>.</summary>
        /// <param name="mask">The mask represented by a <see cref="T:System.Collections.BitArray"/>.</param>
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
        /// <returns>The bits of the net mask instance as an BitArray object instance.</returns>
        public BitArray GetBits()
        {
            return _bits;
        }

        #region Operators

        #region Equality

        /// <summary>Returns a value indicating whether two instances of <see cref="T:System.Net.Topology.NetMaskv4" /> are equal.</summary>
        /// <param name="n1">The first value to compare.</param>
        /// <param name="n2">The second value to compare.</param>
        /// <returns>true if <paramref name="n1" /> and <paramref name="n2" /> are equal; otherwise, false.</returns>
        public static bool operator ==(NetMaskv4 n1, NetMaskv4 n2)
        {
            if (Object.ReferenceEquals(n1, n2))
                return true;
            if (((object)n1 == null) || ((object)n2 == null))
                return false;
            return n1.Equals(n2);
        }

        /// <summary>Returns a value indicating whether two instances of <see cref="T:System.Net.Topology.NetMaskv4" /> are not equal.</summary>
        /// <param name="n1">The first value to compare. </param>
        /// <param name="n2">The second value to compare. </param>
        /// <returns>true if <paramref name="n1" /> and <paramref name="n2" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(NetMaskv4 n1, NetMaskv4 n2)
        {
            return !(n1 == n2); // Problem solved
        }

        #endregion
        #region And

        /// <summary>Bitwise combines a <see cref="T:System.Net.Topology.NetMaskv4" /> instance and an <see cref="T:System.Net.IPAddress"/> the AND operation.</summary>
        /// <param name="mask">The net mask.</param>
        /// <param name="address">The IPAddress.</param>
        /// <returns>The bitwised combination using the AND operation.</returns>
        public static IPAddress operator &(IPAddress address, NetMaskv4 mask)
        {
            return mask & address;
        }

        /// <summary>Bitwise combines a <see cref="T:System.Net.Topology.NetMaskv4" /> instance and an <see cref="T:System.Net.IPAddress"/> the AND operation.</summary>
        /// <param name="mask">The net mask.</param>
        /// <param name="address">The IPAddress.</param>
        /// <returns>The bitwised combination using the AND operation.</returns>
        public static IPAddress operator &(NetMaskv4 mask, IPAddress address)
        {
            throw new NotImplementedException();
        }

        /// <summary>Bitwise combines the two instances of <see cref="T:System.Net.Topology.NetMaskv4" /> using the AND operation.</summary>
        /// <param name="n1">The first value.</param>
        /// <param name="n2">The second value.</param>
        /// <returns>The bitwised combination using the AND operation.</returns>
        public static NetMaskv4 operator &(NetMaskv4 n1, NetMaskv4 n2)
        {
            if (n1 == null || n2 == null)
                return NetMaskv4.Empty;
            return new NetMaskv4(n1._bits.And(n2._bits));
        }

        #endregion
        #region Or

        /// <summary>Bitwise combines the two instances of <see cref="T:System.Net.Topology.NetMaskv4" /> using the OR operation.</summary>
        /// <param name="n1">The first value.</param>
        /// <param name="n2">The second value.</param>
        /// <returns>The bitwised combination using the OR operation.</returns>
        public static NetMaskv4 operator |(NetMaskv4 n1, NetMaskv4 n2)
        {
            if (n1 == null || n2 == null)
                return NetMaskv4.Empty;
            return new NetMaskv4(n1._bits.Or(n2._bits));
        }

        #endregion

        #endregion
        #region Common overrides

        /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
        /// <returns>A string that represents the value of this instance.</returns>
        /// <filterpriority>1</filterpriority>
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

        /// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.</summary>
        /// <returns>true if <paramref name="value" /> is a <see cref="T:System.Net.Topology.NetMaskv4" /> and equal to this instance; otherwise, false.</returns>
        /// <param name="value">The object to compare with this instance. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            var mask = value as NetMaskv4;
            if ((object)mask == null)
                return false;

            return Equals(mask);
        }

        /// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Net.Topology.NetMaskv4" /> object represent the same value.</summary>
        /// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
        /// <param name="value">An object to compare to this instance.</param>
        /// <filterpriority>2</filterpriority>
        public bool Equals(NetMaskv4 value)
        {
            if (value == null)
                return false;

            if (value._bits.Count != 32)
                return false;
            if (value._bits.Count != _bits.Count)
                return false;

            for (int i = 0; i < _bits.Count; ++i)
                if (_bits[i] != value._bits[i])
                    return false;

            return true;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return _bits.GetHashCode();
        }

        #endregion
    }
}
