using System;
using rail;

namespace Terraria.Net
{
	// Token: 0x020000BD RID: 189
	public class WeGameAddress : RemoteAddress
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x004A2158 File Offset: 0x004A0358
		public WeGameAddress(RailID id, string name)
		{
			this.Type = AddressType.WeGame;
			this.rail_id = id;
			this.nickname = name;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x004A2175 File Offset: 0x004A0375
		public override string ToString()
		{
			return "WEGAME_0:" + this.rail_id.id_.ToString();
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x004A2069 File Offset: 0x004A0269
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x004A2191 File Offset: 0x004A0391
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localwegameid") && Program.LaunchParameters["-localwegameid"].Equals(this.rail_id.id_.ToString());
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x004A21CA File Offset: 0x004A03CA
		public override string GetFriendlyName()
		{
			return this.nickname;
		}

		// Token: 0x040011E7 RID: 4583
		public readonly RailID rail_id;

		// Token: 0x040011E8 RID: 4584
		private string nickname;
	}
}
