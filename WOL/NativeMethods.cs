using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
    internal static class NativeMethods
    {
    	private const string IphlpApi = "iphlpapi.dll";
    	
        [DllImport(IphlpApi, ExactSpelling = true)]
        [SecurityCritical]
        public static extern int SendARP(int destIp, int srcIp, byte[] macAddr, ref uint physicalAddrLen);
    }
}
