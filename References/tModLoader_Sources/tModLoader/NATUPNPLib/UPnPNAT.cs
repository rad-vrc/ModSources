using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UwUPnP;

namespace NATUPNPLib
{
	// Token: 0x0200001B RID: 27
	public sealed class UPnPNAT : IUPnPNAT
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005944 File Offset: 0x00003B44
		public IStaticPortMappingCollection StaticPortMappingCollection { get; } = new UPnPNAT.MappingCollection();

		// Token: 0x02000784 RID: 1924
		private class StaticPortMapping : IStaticPortMapping, IEquatable<UPnPNAT.StaticPortMapping>
		{
			// Token: 0x06004D84 RID: 19844 RVA: 0x006731FD File Offset: 0x006713FD
			public StaticPortMapping(int InternalPort, string Protocol, string InternalClient)
			{
				this.InternalPort = InternalPort;
				this.Protocol = Protocol;
				this.InternalClient = InternalClient;
				base..ctor();
			}

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x06004D85 RID: 19845 RVA: 0x0067321A File Offset: 0x0067141A
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(UPnPNAT.StaticPortMapping);
				}
			}

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x06004D86 RID: 19846 RVA: 0x00673226 File Offset: 0x00671426
			// (set) Token: 0x06004D87 RID: 19847 RVA: 0x0067322E File Offset: 0x0067142E
			public int InternalPort { get; set; }

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x06004D88 RID: 19848 RVA: 0x00673237 File Offset: 0x00671437
			// (set) Token: 0x06004D89 RID: 19849 RVA: 0x0067323F File Offset: 0x0067143F
			public string Protocol { get; set; }

			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x06004D8A RID: 19850 RVA: 0x00673248 File Offset: 0x00671448
			// (set) Token: 0x06004D8B RID: 19851 RVA: 0x00673250 File Offset: 0x00671450
			public string InternalClient { get; set; }

			// Token: 0x06004D8C RID: 19852 RVA: 0x0067325C File Offset: 0x0067145C
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("StaticPortMapping");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06004D8D RID: 19853 RVA: 0x006732A8 File Offset: 0x006714A8
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("InternalPort = ");
				builder.Append(this.InternalPort.ToString());
				builder.Append(", Protocol = ");
				builder.Append(this.Protocol);
				builder.Append(", InternalClient = ");
				builder.Append(this.InternalClient);
				return true;
			}

			// Token: 0x06004D8E RID: 19854 RVA: 0x00673314 File Offset: 0x00671514
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(UPnPNAT.StaticPortMapping left, UPnPNAT.StaticPortMapping right)
			{
				return !(left == right);
			}

			// Token: 0x06004D8F RID: 19855 RVA: 0x00673320 File Offset: 0x00671520
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(UPnPNAT.StaticPortMapping left, UPnPNAT.StaticPortMapping right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06004D90 RID: 19856 RVA: 0x00673334 File Offset: 0x00671534
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<InternalPort>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Protocol>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InternalClient>k__BackingField);
			}

			// Token: 0x06004D91 RID: 19857 RVA: 0x00673396 File Offset: 0x00671596
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as UPnPNAT.StaticPortMapping);
			}

			// Token: 0x06004D92 RID: 19858 RVA: 0x006733A4 File Offset: 0x006715A4
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(UPnPNAT.StaticPortMapping other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<InternalPort>k__BackingField, other.<InternalPort>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Protocol>k__BackingField, other.<Protocol>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<InternalClient>k__BackingField, other.<InternalClient>k__BackingField));
			}

			// Token: 0x06004D94 RID: 19860 RVA: 0x0067341D File Offset: 0x0067161D
			[CompilerGenerated]
			protected StaticPortMapping([Nullable(1)] UPnPNAT.StaticPortMapping original)
			{
				this.InternalPort = original.<InternalPort>k__BackingField;
				this.Protocol = original.<Protocol>k__BackingField;
				this.InternalClient = original.<InternalClient>k__BackingField;
			}

			// Token: 0x06004D95 RID: 19861 RVA: 0x00673449 File Offset: 0x00671649
			[CompilerGenerated]
			public void Deconstruct(out int InternalPort, out string Protocol, out string InternalClient)
			{
				InternalPort = this.InternalPort;
				Protocol = this.Protocol;
				InternalClient = this.InternalClient;
			}
		}

		// Token: 0x02000785 RID: 1925
		private class MappingCollection : IStaticPortMappingCollection, IEnumerable
		{
			// Token: 0x06004D96 RID: 19862 RVA: 0x00673463 File Offset: 0x00671663
			public IStaticPortMapping Add(int lExternalPort, string bstrProtocol, int lInternalPort, string bstrInternalClient, bool bEnabled, string bstrDescription)
			{
				UPnP.Open(Enum.Parse<Protocol>(bstrProtocol), (ushort)lExternalPort, new ushort?((ushort)lInternalPort), bstrDescription);
				return new UPnPNAT.StaticPortMapping(lInternalPort, bstrProtocol, bstrInternalClient);
			}

			// Token: 0x06004D97 RID: 19863 RVA: 0x00673484 File Offset: 0x00671684
			public void Remove(int lExternalPort, string bstrProtocol)
			{
				UPnP.Close(Enum.Parse<Protocol>(bstrProtocol), (ushort)lExternalPort);
			}

			// Token: 0x06004D98 RID: 19864 RVA: 0x00673493 File Offset: 0x00671693
			public IEnumerator GetEnumerator()
			{
				int i = 0;
				for (;;)
				{
					Dictionary<string, string> args;
					try
					{
						args = UPnP.GetGenericPortMappingEntry(i);
					}
					catch
					{
						yield break;
					}
					string s_port;
					int port;
					string protocol;
					string client;
					if (args == null || !args.TryGetValue("NewInternalPort", out s_port) || !int.TryParse(s_port, out port) || !args.TryGetValue("NewProtocol", out protocol) || !args.TryGetValue("NewInternalClient", out client))
					{
						break;
					}
					yield return new UPnPNAT.StaticPortMapping(port, protocol, client);
					int num = i;
					i = num + 1;
				}
				yield break;
				yield break;
			}
		}
	}
}
