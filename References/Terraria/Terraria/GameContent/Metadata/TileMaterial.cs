using System;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x02000210 RID: 528
	public class TileMaterial
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0050747D File Offset: 0x0050567D
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x00507485 File Offset: 0x00505685
		[JsonProperty]
		public TileGolfPhysics GolfPhysics { get; private set; }
	}
}
