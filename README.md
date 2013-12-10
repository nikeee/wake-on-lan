# Wake-On-LAN

A simple library for sending magic packets and performing IP address operations.

### Why?
I wanted to create a library that matches all Microsoft Code Analysis rules and design guidelines.
However, it does not fit at least one naming guideline. The default namespace is `System.Net`. I decided to do this because I want the library to be used like a normal part of the .NET Framework.

## Samples

### Sending a Magic Packet
This sample uses `00:11:22:33:44:55` as MAC address.

```C#
using System.Net;
// ...

// Using the IPAddess extension
IPAddress.Broadcast.SendWol(0x00, 0x11, 0x22, 0x33, 0x44, 0x55);

// via core SendWol class
var endPoint = new IPEndPoint(IPAddress.Broadcast, 7); // You don't have to use Broadcast.
                                                       // Every IP/port-combination is possible.
SendWol.Send(endPoint, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55)

// via IPEndPoint extension
endPoint.SendWol(0x00, 0x11, 0x22, 0x33, 0x44, 0x55)
```


### Getting Subnet Information
You can also retrieve information about a subnet.
```C#
using System.Net;
using System.Net.Topology;
// ...

var someIp = new IPAddress(new byte[] { 192, 168, 1, 23 }); // Some IP address in the subnet
var mask = new NetMask(255, 255, 255, 0); // the network mask of the subnet

// CIDR-notation number of the network mask
int cidr = mask.Cidr;

var networkPrefix = someIp & mask; // bitwise operation to get the network address (192.168.1.0)
networkPrefix = someIp.GetNetworkPrefix(mask); // using the extension method for IPAddress

// retrieve broadcast address of the subnet (192.168.1.255)
var broadcastAddress = someIp.GetBroadcastAddress(mask);

IEnumerable<IPAddress> siblings = someIp.GetSiblings(mask, SiblingOptions.ExcludeUnusable);
// Enumerate through all IP addresses in the subnet, except network prefix and broadcast (RFC 950, 2^n-2)
foreach (IPAddress someIpInNetwork in siblings)
{
    Console.WriteLine(someIpInNetwork.ToString());
}

// Get number of possible siblings without someIp, broadcast and network prefix
int siblingCount = mask.GetSiblingCount(SiblingOptions.ExcludeAll);
```

### ARP Requests
To retrieve the MAC address of a host, there is a functionality for ARP-request built-in. It uses the Windows API method [SendArp].
```C#
ArpRequestResult res = ArpRequest.Send(someIp);
if(res.Exception != null)
{
    Console.WriteLine("ARP error occurred: " + res.Exception.Message);
}
else
{
    Console.WriteLine("Host MAC address: " + res.Address.ToString());
}
```
Note that there isn't always an MAC address available although there is a host. The reason for this could be the host is offline and/or the physical address is not cached somewhere.
Also, this function uses a p/invoke. This might cause problems when used on platforms other than Windows.

### Async/Await
This library also supports the Task-based Asynchronous Pattern (TAP). Every method that can send a magic packet synchronously is available as a TAP method returning a `Task`.
```C#
await IPAddress.Broadcast.SendWolAsync(0x00, 0x11, 0x22, 0x33, 0x44, 0x55);
```

### Further Samples
The [System.Net.NetworkInformation.PhysicalAddress][5] class is also supported as it represents a MAC address.
```C#
var mac = new PhysicalAddress(new byte[] {0x00, 0x11, 0x22, 0x33, 0x44, 0x55});
mac.SendWol(); // via extension method
```

### Documentation
There is an online documentation available [here][0]. It was built using [Sandcastle] and the [Sandcastle Help File Builder].
You can download the `.chm` file [here][3].

### Compability
There is a compiled version for several versions of the .NET Framework. Currently these frameworks are supported:
- .NET 2.0 (does not include extension methods and async features)
- .NET 3.5 Client Profile (does not include async features)
- .NET 4.0 Client Profile (does not include async features)
- .NET 4.5
- .NET 4.5.1

The latest compiled assemblies are in the [build/lib directory][6] of this repository. Alternatively, you can install the needed version using NuGet (see below).

The following support is planned but not available yet:
- Silverlight
    - Windows Phone 7
- Windows Phone 8(.1)
- Windows Store

### NuGet
Install the [NuGet package][4] of this library using
```
Install-Package WakeOnLan
```

[0]: http://holz.nu/doc/wol
[Sandcastle]: https://sandcastle.codeplex.com
[Sandcastle Help File Builder]: https://shfb.codeplex.com
[SendArp]: http://msdn.microsoft.com/en-us/library/windows/desktop/aa366358(v=vs.85).aspx
[3]: https://github.com/nikeee/wake-on-lan/raw/master/src/Documentation/WOL45/Documentation.chm
[4]: https://nuget.org/packages/WakeOnLan
[5]: http://msdn.microsoft.com/en-us/library/system.net.networkinformation.physicaladdress(v=vs.110).aspx
[6]: https://github.com/nikeee/wake-on-lan/tree/master/build/lib
