using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000268 RID: 616
	public class SliceOfCakeLastsUntilDeath : GlobalBuff
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x000736DC File Offset: 0x000718DC
		public override void SetStaticDefaults()
		{
			bool infinite = QoLCompendium.mainConfig.InfiniteSliceOfCake;
			BuffID.Sets.TimeLeftDoesNotDecrease[192] = (infinite || this._defaultTimeLeft);
			Main.buffNoTimeDisplay[192] = (infinite || this._defaultTimeDisplay);
			Main.buffNoSave[192] = (infinite || this._defaultNoSave);
		}

		// Token: 0x040005C2 RID: 1474
		private readonly bool _defaultTimeLeft = BuffID.Sets.TimeLeftDoesNotDecrease[192];

		// Token: 0x040005C3 RID: 1475
		private readonly bool _defaultTimeDisplay = Main.buffNoTimeDisplay[192];

		// Token: 0x040005C4 RID: 1476
		private readonly bool _defaultNoSave = Main.buffNoSave[192];
	}
}
