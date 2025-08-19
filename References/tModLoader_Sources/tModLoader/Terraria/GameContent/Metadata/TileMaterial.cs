using System;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x020005DE RID: 1502
	public class TileMaterial
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x005FC6C8 File Offset: 0x005FA8C8
		// (set) Token: 0x06004327 RID: 17191 RVA: 0x005FC6D0 File Offset: 0x005FA8D0
		[JsonProperty]
		public TileGolfPhysics GolfPhysics { get; private set; }
	}
}
