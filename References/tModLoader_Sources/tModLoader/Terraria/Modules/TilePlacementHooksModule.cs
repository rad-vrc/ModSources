using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x02000137 RID: 311
	public class TilePlacementHooksModule
	{
		// Token: 0x06001A83 RID: 6787 RVA: 0x004CBEC4 File Offset: 0x004CA0C4
		public TilePlacementHooksModule(TilePlacementHooksModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.check = default(PlacementHook);
				this.postPlaceEveryone = default(PlacementHook);
				this.postPlaceMyPlayer = default(PlacementHook);
				this.placeOverride = default(PlacementHook);
				return;
			}
			this.check = copyFrom.check;
			this.postPlaceEveryone = copyFrom.postPlaceEveryone;
			this.postPlaceMyPlayer = copyFrom.postPlaceMyPlayer;
			this.placeOverride = copyFrom.placeOverride;
		}

		// Token: 0x0400145D RID: 5213
		public PlacementHook check;

		// Token: 0x0400145E RID: 5214
		public PlacementHook postPlaceEveryone;

		// Token: 0x0400145F RID: 5215
		public PlacementHook postPlaceMyPlayer;

		// Token: 0x04001460 RID: 5216
		public PlacementHook placeOverride;
	}
}
