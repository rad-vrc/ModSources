using System;
using QoLCompendium.Content.NPCs;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000242 RID: 578
	public class CensusSupport : ModSystem
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0006D82C File Offset: 0x0006BA2C
		public override void PostSetupContent()
		{
			Mod Census;
			if (ModLoader.TryGetMod("Census", out Census))
			{
				if (QoLCompendium.mainConfig.BlackMarketDealerCanSpawn)
				{
					Census.Call(new object[]
					{
						"TownNPCCondition",
						ModContent.NPCType<BMDealerNPC>(),
						ModContent.GetInstance<BMDealerNPC>().GetLocalization("Census.SpawnCondition", null)
					});
				}
				if (QoLCompendium.mainConfig.EtherealCollectorCanSpawn)
				{
					Census.Call(new object[]
					{
						"TownNPCCondition",
						ModContent.NPCType<EtherealCollectorNPC>(),
						ModContent.GetInstance<EtherealCollectorNPC>().GetLocalization("Census.SpawnCondition", null)
					});
				}
			}
		}
	}
}
