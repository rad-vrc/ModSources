using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Terraria.ModLoader;

namespace UwUPnP
{
	// Token: 0x02000015 RID: 21
	internal sealed class Gateway
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004C53 File Offset: 0x00002E53
		public IPAddress InternalClient { get; }

		// Token: 0x060000B5 RID: 181 RVA: 0x00004C5C File Offset: 0x00002E5C
		static Gateway()
		{
			Logging.tML.Debug("SSDP search line separator: " + ((Gateway.ssdpLineSep == "\n") ? "LF" : "CRLF"));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004CE4 File Offset: 0x00002EE4
		private Gateway(IPAddress ip, string data)
		{
			this.InternalClient = ip;
			ValueTuple<string, string> info = Gateway.GetInfo(Gateway.GetLocation(data));
			this.serviceType = info.Item1;
			this.controlURL = info.Item2;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004D24 File Offset: 0x00002F24
		public static bool TryNew(IPAddress ip, out Gateway gateway)
		{
			IPEndPoint endPoint = IPEndPoint.Parse("239.255.255.250:1900");
			bool result;
			using (Socket socket = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp)
			{
				ReceiveTimeout = 3000,
				SendTimeout = 3000
			})
			{
				socket.Bind(new IPEndPoint(ip, 0));
				byte[] buffer = new byte[1536];
				foreach (string type in Gateway.searchMessageTypes)
				{
					string request = string.Join(Gateway.ssdpLineSep, new string[]
					{
						"M-SEARCH * HTTP/1.1",
						"HOST: 239.255.255.250:1900",
						"ST: " + type,
						"MAN: \"ssdp:discover\"",
						"MX: 2",
						"",
						""
					});
					byte[] req = Encoding.ASCII.GetBytes(request);
					try
					{
						socket.SendTo(req, endPoint);
					}
					catch (SocketException)
					{
						gateway = null;
						return false;
					}
					int receivedCount = 0;
					for (int i = 0; i < 20; i++)
					{
						try
						{
							receivedCount = socket.Receive(buffer);
						}
						catch (SocketException)
						{
							break;
						}
						try
						{
							gateway = new Gateway(ip, Encoding.ASCII.GetString(buffer, 0, receivedCount));
							return true;
						}
						catch
						{
							gateway = null;
						}
					}
				}
				gateway = null;
				result = false;
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004EA0 File Offset: 0x000030A0
		private static string GetLocation(string data)
		{
			foreach (string line in from l in data.Split('\n', StringSplitOptions.None)
			select l.Trim() into l
			where l.Length > 0
			select l)
			{
				if (!line.StartsWith("HTTP/1.") && !line.StartsWith("NOTIFY *"))
				{
					int colonIndex = line.IndexOf(':');
					if (colonIndex >= 0)
					{
						string name = line.Substring(0, colonIndex);
						string text;
						if (line.Length < name.Length)
						{
							text = null;
						}
						else
						{
							string text2 = line;
							int num = colonIndex + 1;
							text = text2.Substring(num, text2.Length - num).Trim();
						}
						string val = text;
						if (name.ToLowerInvariant() == "location")
						{
							if (val.IndexOf('/', 7) == -1)
							{
								throw new Exception("Unsupported Gateway");
							}
							return val;
						}
					}
				}
			}
			throw new Exception("Unsupported Gateway");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FDC File Offset: 0x000031DC
		[return: TupleElementNames(new string[]
		{
			"serviceType",
			"controlURL"
		})]
		private static ValueTuple<string, string> GetInfo(string location)
		{
			IEnumerable<XElement> enumerable = from d in XDocument.Load(location).Descendants()
			where d.Name.LocalName == "service"
			select d;
			ValueTuple<string, string> ret = new ValueTuple<string, string>(null, null);
			foreach (XContainer xcontainer in enumerable)
			{
				string serviceType = null;
				string controlURL = null;
				foreach (XNode xnode in xcontainer.Nodes())
				{
					XElement ele = xnode as XElement;
					if (ele != null)
					{
						XText i = ele.FirstNode as XText;
						if (i != null)
						{
							string a = ele.Name.LocalName.Trim().ToLowerInvariant();
							if (!(a == "servicetype"))
							{
								if (a == "controlurl")
								{
									controlURL = i.Value.Trim();
								}
							}
							else
							{
								serviceType = i.Value.Trim();
							}
						}
					}
				}
				if (serviceType != null && controlURL != null && (serviceType.ToLowerInvariant().Contains(":wanipconnection:") || serviceType.ToLowerInvariant().Contains(":wanpppconnection:")))
				{
					ret.Item1 = serviceType;
					ret.Item2 = controlURL;
				}
			}
			if (ret.Item2 == null)
			{
				throw new Exception("Unsupported Gateway");
			}
			if (!ret.Item2.StartsWith('/'))
			{
				ret.Item2 = "/" + ret.Item2;
			}
			int slash = location.IndexOf('/', 7);
			ret.Item2 = location.Substring(0, slash) + ret.Item2;
			return ret;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000051A0 File Offset: 0x000033A0
		private static string BuildArgString([TupleElementNames(new string[]
		{
			"Key",
			"Value"
		})] ValueTuple<string, object> arg)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
			defaultInterpolatedStringHandler.AppendLiteral("<");
			defaultInterpolatedStringHandler.AppendFormatted(arg.Item1);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted<object>(arg.Item2);
			defaultInterpolatedStringHandler.AppendLiteral("</");
			defaultInterpolatedStringHandler.AppendFormatted(arg.Item1);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005214 File Offset: 0x00003414
		private Dictionary<string, string> RunCommand(string action, [TupleElementNames(new string[]
		{
			"Key",
			"Value"
		})] params ValueTuple<string, object>[] args)
		{
			string requestData = this.GetRequestData(action, args);
			return Gateway.GetResponse(this.SendRequest(action, requestData));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005238 File Offset: 0x00003438
		private string GetRequestData(string action, [TupleElementNames(new string[]
		{
			"Key",
			"Value"
		})] ValueTuple<string, object>[] args)
		{
			string[] array = new string[8];
			array[0] = "<?xml version=\"1.0\"?>\n";
			array[1] = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">";
			array[2] = "<SOAP-ENV:Body>";
			int num = 3;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<m:");
			defaultInterpolatedStringHandler.AppendFormatted(action);
			defaultInterpolatedStringHandler.AppendLiteral(" xmlns:m=\"");
			defaultInterpolatedStringHandler.AppendFormatted(this.serviceType);
			defaultInterpolatedStringHandler.AppendLiteral("\">");
			array[num] = defaultInterpolatedStringHandler.ToStringAndClear();
			int num2 = 4;
			Func<ValueTuple<string, object>, string> selector;
			if ((selector = Gateway.<>O.<0>__BuildArgString) == null)
			{
				selector = (Gateway.<>O.<0>__BuildArgString = new Func<ValueTuple<string, object>, string>(Gateway.BuildArgString));
			}
			array[num2] = string.Concat(args.Select(selector));
			array[5] = "</m:" + action + ">";
			array[6] = "</SOAP-ENV:Body>";
			array[7] = "</SOAP-ENV:Envelope>";
			return string.Concat(array);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005304 File Offset: 0x00003504
		private HttpWebRequest SendRequest(string action, string requestData)
		{
			byte[] data = Encoding.ASCII.GetBytes(requestData);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.controlURL);
			request.Method = "POST";
			request.ContentType = "text/xml";
			request.ContentLength = (long)data.Length;
			NameValueCollection headers = request.Headers;
			string name = "SOAPAction";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
			defaultInterpolatedStringHandler.AppendLiteral("\"");
			defaultInterpolatedStringHandler.AppendFormatted(this.serviceType);
			defaultInterpolatedStringHandler.AppendLiteral("#");
			defaultInterpolatedStringHandler.AppendFormatted(action);
			defaultInterpolatedStringHandler.AppendLiteral("\"");
			headers.Add(name, defaultInterpolatedStringHandler.ToStringAndClear());
			HttpWebRequest result;
			using (Stream requestStream = request.GetRequestStream())
			{
				requestStream.Write(data);
				result = request;
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000053DC File Offset: 0x000035DC
		private static Dictionary<string, string> GetResponse(HttpWebRequest request)
		{
			Dictionary<string, string> ret = new Dictionary<string, string>();
			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						return ret;
					}
					foreach (XNode xnode in XDocument.Load(response.GetResponseStream()).DescendantNodes())
					{
						XElement ele = xnode as XElement;
						if (ele != null)
						{
							XText txt = ele.FirstNode as XText;
							if (txt != null)
							{
								ret[ele.Name.LocalName] = txt.Value;
							}
						}
					}
				}
			}
			catch
			{
			}
			string errorCode;
			if (ret.TryGetValue("errorCode", out errorCode))
			{
				throw new Exception(errorCode);
			}
			return ret;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000054D0 File Offset: 0x000036D0
		public IPAddress ExternalIPAddress
		{
			get
			{
				string ret;
				if (!this.RunCommand("GetExternalIPAddress", Array.Empty<ValueTuple<string, object>>()).TryGetValue("NewExternalIPAddress", out ret))
				{
					return null;
				}
				return IPAddress.Parse(ret);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005504 File Offset: 0x00003704
		public bool SpecificPortMappingExists(ushort externalPort, Protocol protocol)
		{
			return this.RunCommand("GetSpecificPortMappingEntry", new ValueTuple<string, object>[]
			{
				new ValueTuple<string, object>("NewRemoteHost", ""),
				new ValueTuple<string, object>("NewExternalPort", externalPort),
				new ValueTuple<string, object>("NewProtocol", protocol)
			}).ContainsKey("NewInternalPort");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005570 File Offset: 0x00003770
		public void AddPortMapping(ushort externalPort, Protocol protocol, ushort? internalPort = null, string description = null)
		{
			this.RunCommand("AddPortMapping", new ValueTuple<string, object>[]
			{
				new ValueTuple<string, object>("NewRemoteHost", ""),
				new ValueTuple<string, object>("NewExternalPort", externalPort),
				new ValueTuple<string, object>("NewProtocol", protocol),
				new ValueTuple<string, object>("NewInternalClient", this.InternalClient),
				new ValueTuple<string, object>("NewInternalPort", internalPort.GetValueOrDefault(externalPort)),
				new ValueTuple<string, object>("NewEnabled", 1),
				new ValueTuple<string, object>("NewPortMappingDescription", description ?? "UwUPnP"),
				new ValueTuple<string, object>("NewLeaseDuration", 0)
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005654 File Offset: 0x00003854
		public void DeletePortMapping(ushort externalPort, Protocol protocol)
		{
			this.RunCommand("DeletePortMapping", new ValueTuple<string, object>[]
			{
				new ValueTuple<string, object>("NewRemoteHost", ""),
				new ValueTuple<string, object>("NewExternalPort", externalPort),
				new ValueTuple<string, object>("NewProtocol", protocol)
			});
		}

		/// <summary>2.4.14.GetGenericPortMappingEntry</summary>
		// Token: 0x060000C3 RID: 195 RVA: 0x000056B7 File Offset: 0x000038B7
		public Dictionary<string, string> GetGenericPortMappingEntry(int portMappingIndex)
		{
			return this.RunCommand("GetGenericPortMappingEntry", new ValueTuple<string, object>[]
			{
				new ValueTuple<string, object>("NewPortMappingIndex", portMappingIndex)
			});
		}

		// Token: 0x0400005C RID: 92
		private readonly string serviceType;

		// Token: 0x0400005D RID: 93
		private readonly string controlURL;

		// Token: 0x0400005E RID: 94
		private static readonly string ssdpLineSep = (Environment.GetEnvironmentVariable("SSDP_HEADER_USE_LF") == "1") ? "\n" : "\r\n";

		// Token: 0x0400005F RID: 95
		private static readonly string[] searchMessageTypes = new string[]
		{
			"urn:schemas-upnp-org:device:InternetGatewayDevice:1",
			"urn:schemas-upnp-org:service:WANIPConnection:1",
			"urn:schemas-upnp-org:service:WANPPPConnection:1"
		};

		// Token: 0x0200077F RID: 1919
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006586 RID: 25990
			[TupleElementNames(new string[]
			{
				"Key",
				"Value"
			})]
			public static Func<ValueTuple<string, object>, string> <0>__BuildArgString;
		}
	}
}
