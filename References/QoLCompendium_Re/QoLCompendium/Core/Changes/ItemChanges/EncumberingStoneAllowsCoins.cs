using System;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025D RID: 605
	public class EncumberingStoneAllowsCoins : ModPlayer
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00070EF0 File Offset: 0x0006F0F0
		public override void PostUpdateMiscEffects()
		{
			for (int i = 0; i < Common.CoinIDs.Count; i++)
			{
				ItemID.Sets.IgnoresEncumberingStone[Common.CoinIDs.ElementAt(i)] = QoLCompendium.mainConfig.EncumberingStoneAllowsCoins;
			}
		}
	}
}
