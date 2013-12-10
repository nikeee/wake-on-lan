# Wake-On-LAN

A simple library for sending magic packets and performing IP address operations

## Why?
I wanted to make a library that fits all Microsoft Code Analysis rules and Design guidelines.
However, it does not fit at least one naming guideline. The default namespace is System.Net. I decided to do this because I want the library to be used like a normal part of the .NET Framework.

## Samples

### Sending a magic packet
This sample uses <code>00:11:22:33:44:55</code> as MAC address.

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
// ADDME

### Async/Await
This library also supports the Task-based Asynchronous Pattern (TAP). Every method that can send a magic packet synchronously is available as a TAP method returning a <code>Task</code>.
```C#
await IPAddress.Broadcast.SendWolAsync(0x00, 0x11, 0x22, 0x33, 0x44, 0x55);
```

### Further Samples
The [System.Net.NetworkInformation.PhysicalAddress][5] class is also supportat as it can represent a MAC address.
```C#
var mac = new PhysicalAddress(new byte[] {0x00, 0x11, 0x22, 0x33, 0x44, 0x55});

// via extension method
mac.SendWol();
```

## Documentation
There is an online documentation available [here][0]. It has been built using [Sandcastle][1] and the [Sandcastle Help File Builder][2].
You can download the <code>.chm</code> file [here][3].

### Compability
There is a compiled version for several versions of the .NET Framework. Currently these frameworks are supported:
- .NET 2.0
- .NET 3.5 Client Profile
- .NET 4.0 Client Profile
- .NET 4.5
- .NET 4.5.1

The following support is planned but not available yet:
- Silverlight
    - Windows Phone 7
- Windows Phone 8(.1)
- Windows Store

### NuGet
Install the [NuGet package][4] of this library using <code>Install-Package WakeOnLan</code>.

[0]: http://holz.nu/doc/wol
[1]: https://sandcastle.codeplex.com
[2]: https://shfb.codeplex.com
[3]: https://github.com/nikeee/wake-on-lan/raw/master/src/Documentation/WOL45/Documentation.chm
[4]: nuget.org/packages/WakeOnLan
[5]: http://msdn.microsoft.com/en-us/library/system.net.networkinformation.physicaladdress(v=vs.110).aspx
