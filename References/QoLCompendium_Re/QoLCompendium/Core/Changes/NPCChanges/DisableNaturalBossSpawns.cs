using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000239 RID: 569
	public class DisableNaturalBossSpawns : ModSystem
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0006CFC8 File Offset: 0x0006B1C8
		public override void PreUpdateTime()
		{
			if (QoLCompendium.mainConfig.NoNaturalBossSpawns)
			{
				WorldGen.spawnEye = false;
				WorldGen.spawnHardBoss = 0;
			}
		}
	}
}
