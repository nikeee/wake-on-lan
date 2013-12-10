Wake-On-LAN
===========

A simple library for sending magic packets and performing IP address operations

# Why?
I wanted to make a library that fits all Microsoft Code Analysis rules and Design guidelines.
However, it does not fit at least one naming guideline. The default namespace is System.Net. I decided to do this because I want the library to be used like a normal part of the .NET Framework.

# Samples

## Sending a magic packet
This sample uses <code>00:11:22:33:44:55</code> as MAC address.

```C#
using System.Net;
// ...

// Using the IPAddess extension
IPAddress.Broadcast.SendWol(0x00, 0x11, 0x22, 0x33, 0x44, 0x55);

// via core SendWol class
var endPoint = new IPEndPoint(IPAddress.Broadcast, 7);
SendWol.Send(endPoint, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55)

// via IPEndPoint extension
endPoint.SendWol(0x00, 0x11, 0x22, 0x33, 0x44, 0x55)

```
## Getting Subnet Information
// ADDME

## Async/Await
This library also supports the Task-based Asynchronous Pattern (TAP).
```C#
await IPAddress.Broadcast.SendWolAsync(0x00, 0x11, 0x22, 0x33, 0x44, 0x55);)
```

# Documentation
There is an online documentation available [here][0]. It has been built using [Sandcastle][1] and the [Sandcastle Help File Builder][2].
You can download the <code>.chm</code> file [here][3].

[0]: http://holz.nu/doc/wol
[1]: https://sandcastle.codeplex.com
[2]: https://shfb.codeplex.com
[3]: https://github.com/nikeee/wake-on-lan/raw/master/src/Documentation/WOL45/Documentation.chm
