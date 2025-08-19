using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace UwUPnP
{
	/// <summary>
	/// A simple UPnP library
	/// See: https://github.com/Rartrin/UwUPnP
	/// </summary>
	// Token: 0x02000017 RID: 23
	public static class UPnP
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000056E1 File Offset: 0x000038E1
		private static Gateway Gateway
		{
			get
			{
				if (UPnP.gatewayNotYetRequested)
				{
					UPnP.gatewayNotYetRequested = false;
					UPnP.FindGateway();
				}
				while (UPnP.searching)
				{
					Thread.Sleep(1);
				}
				return UPnP.defaultGateway;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005709 File Offset: 0x00003909
		public static bool IsAvailable
		{
			get
			{
				return UPnP.Gateway != null;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005716 File Offset: 0x00003916
		public static IPAddress ExternalIP
		{
			get
			{
				Gateway gateway = UPnP.Gateway;
				if (gateway == null)
				{
					return null;
				}
				return gateway.ExternalIPAddress;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005728 File Offset: 0x00003928
		public static IPAddress LocalIP
		{
			get
			{
				Gateway gateway = UPnP.Gateway;
				if (gateway == null)
				{
					return null;
				}
				return gateway.InternalClient;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000573A File Offset: 0x0000393A
		public static void Open(Protocol protocol, ushort externalPort, ushort? internalPort = null, string description = null)
		{
			Gateway gateway = UPnP.Gateway;
			if (gateway == null)
			{
				return;
			}
			gateway.AddPortMapping(externalPort, protocol, internalPort, description);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000574F File Offset: 0x0000394F
		public static void Close(Protocol protocol, ushort externalPort)
		{
			Gateway gateway = UPnP.Gateway;
			if (gateway == null)
			{
				return;
			}
			gateway.DeletePortMapping(externalPort, protocol);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005762 File Offset: 0x00003962
		public static bool IsOpen(Protocol protocol, ushort externalPort)
		{
			Gateway gateway = UPnP.Gateway;
			return gateway != null && gateway.SpecificPortMappingExists(externalPort, protocol);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005776 File Offset: 0x00003976
		public static Dictionary<string, string> GetGenericPortMappingEntry(int portMappingIndex)
		{
			Gateway gateway = UPnP.Gateway;
			if (gateway == null)
			{
				return null;
			}
			return gateway.GetGenericPortMappingEntry(portMappingIndex);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000578C File Offset: 0x0000398C
		private static void FindGateway()
		{
			UPnP.searching = true;
			List<Task> listeners = new List<Task>();
			using (IEnumerator<IPAddress> enumerator = UPnP.GetLocalIPs().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPAddress ip = enumerator.Current;
					listeners.Add(Task.Run(delegate()
					{
						UPnP.StartListener(ip);
					}));
				}
			}
			Task.WhenAll(listeners).ContinueWith<bool>((Task t) => UPnP.searching = false);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000582C File Offset: 0x00003A2C
		private static void StartListener(IPAddress ip)
		{
			Gateway gateway;
			if (Gateway.TryNew(ip, out gateway))
			{
				Interlocked.CompareExchange<Gateway>(ref UPnP.defaultGateway, gateway, null);
				UPnP.searching = false;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005858 File Offset: 0x00003A58
		private static IEnumerable<IPAddress> GetLocalIPs()
		{
			IEnumerable<NetworkInterface> allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			Func<NetworkInterface, bool> predicate;
			if ((predicate = UPnP.<>O.<0>__IsValidInterface) == null)
			{
				predicate = (UPnP.<>O.<0>__IsValidInterface = new Func<NetworkInterface, bool>(UPnP.IsValidInterface));
			}
			IEnumerable<NetworkInterface> source = allNetworkInterfaces.Where(predicate);
			Func<NetworkInterface, IEnumerable<IPAddress>> selector;
			if ((selector = UPnP.<>O.<1>__GetValidNetworkIPs) == null)
			{
				selector = (UPnP.<>O.<1>__GetValidNetworkIPs = new Func<NetworkInterface, IEnumerable<IPAddress>>(UPnP.GetValidNetworkIPs));
			}
			return source.SelectMany(selector);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000058AA File Offset: 0x00003AAA
		private static bool IsValidInterface(NetworkInterface network)
		{
			return network.OperationalStatus == OperationalStatus.Up && network.NetworkInterfaceType != NetworkInterfaceType.Loopback && network.NetworkInterfaceType != NetworkInterfaceType.Ppp;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000058D0 File Offset: 0x00003AD0
		private static IEnumerable<IPAddress> GetValidNetworkIPs(NetworkInterface network)
		{
			return from a in network.GetIPProperties().UnicastAddresses
			select a.Address into a
			where a.AddressFamily == AddressFamily.InterNetwork || a.AddressFamily == AddressFamily.InterNetworkV6
			select a;
		}

		// Token: 0x04000064 RID: 100
		private static bool gatewayNotYetRequested = true;

		// Token: 0x04000065 RID: 101
		private static bool searching = false;

		// Token: 0x04000066 RID: 102
		private static Gateway defaultGateway = null;

		// Token: 0x02000781 RID: 1921
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400658B RID: 25995
			public static Func<NetworkInterface, bool> <0>__IsValidInterface;

			// Token: 0x0400658C RID: 25996
			[Nullable(new byte[]
			{
				0,
				0,
				1,
				0
			})]
			public static Func<NetworkInterface, IEnumerable<IPAddress>> <1>__GetValidNetworkIPs;
		}
	}
}
