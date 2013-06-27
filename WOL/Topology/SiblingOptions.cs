using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Topology
{
    /// <summary>Provides options for doing network sibling calculations using a net mask.</summary>
    [Flags]
    public enum SiblingOptions
    {
        /// <summary>Do not include the broadcast or net address neither the addess passed to the method.</summary>
        ExcludeAll = 0,
        /// <summary>Include the addess passed to the method.</summary>
        IncludeSelf = 1,
        /// <summary>Include the broadcast address.</summary>
        IncludeBroadcast = 2,
        /// <summary>Include the net address.</summary>
        IncludeNet = 4,
        /// <summary>Include all addresses possible.</summary>
        IncludeAll = IncludeSelf | IncludeBroadcast | IncludeNet
    }
}
