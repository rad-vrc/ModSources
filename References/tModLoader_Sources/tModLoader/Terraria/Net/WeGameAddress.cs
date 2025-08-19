using System;
using rail;

namespace Terraria.Net
{
	// Token: 0x02000126 RID: 294
	public class WeGameAddress : RemoteAddress
	{
		// Token: 0x06001A41 RID: 6721 RVA: 0x004CB203 File Offset: 0x004C9403
		public WeGameAddress(RailID id, string name)
		{
			this.Type = AddressType.WeGame;
			this.rail_id = id;
			this.nickname = name;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x004CB220 File Offset: 0x004C9420
		public override string ToString()
		{
			return "WEGAME_0:" + this.rail_id.id_.ToString();
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x004CB23C File Offset: 0x004C943C
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x004CB244 File Offset: 0x004C9444
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localwegameid") && Program.LaunchParameters["-localwegameid"].Equals(this.rail_id.id_.ToString());
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x004CB27D File Offset: 0x004C947D
		public override string GetFriendlyName()
		{
			return this.nickname;
		}

		// Token: 0x04001428 RID: 5160
		public readonly RailID rail_id;

		// Token: 0x04001429 RID: 5161
		private string nickname;
	}
}
