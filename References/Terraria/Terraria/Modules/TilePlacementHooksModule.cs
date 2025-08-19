using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x0200005C RID: 92
	public class TilePlacementHooksModule
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x0048C530 File Offset: 0x0048A730
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

		// Token: 0x04000EEF RID: 3823
		public PlacementHook check;

		// Token: 0x04000EF0 RID: 3824
		public PlacementHook postPlaceEveryone;

		// Token: 0x04000EF1 RID: 3825
		public PlacementHook postPlaceMyPlayer;

		// Token: 0x04000EF2 RID: 3826
		public PlacementHook placeOverride;
	}
}
