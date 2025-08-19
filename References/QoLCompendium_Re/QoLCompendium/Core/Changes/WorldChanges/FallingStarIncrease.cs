using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000213 RID: 531
	public class FallingStarIncrease : ModSystem
	{
		// Token: 0x06000D00 RID: 3328 RVA: 0x00064FCE File Offset: 0x000631CE
		public override void PostUpdateWorld()
		{
			Star.starfallBoost = (float)QoLCompendium.mainConfig.MoreFallenStars;
		}
	}
}
