using System.Runtime.InteropServices;

namespace System.Net
{
    internal static class NativeMethods
    {
    	private const string IphlpApi = "iphlpapi.dll";
    	
        [DllImport(IphlpApi, ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIp, byte[] macAddr, ref uint physicalAddrLen);
    }
}
