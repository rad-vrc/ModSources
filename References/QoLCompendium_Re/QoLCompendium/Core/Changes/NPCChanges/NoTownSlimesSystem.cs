using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000244 RID: 580
	public class NoTownSlimesSystem : ModSystem
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0006D904 File Offset: 0x0006BB04
		public override void PreUpdateWorld()
		{
			if (QoLCompendium.mainConfig.NoTownSlimes)
			{
				NPC.unlockedSlimeBlueSpawn = false;
				NPC.unlockedSlimeCopperSpawn = false;
				NPC.unlockedSlimeGreenSpawn = false;
				NPC.unlockedSlimeOldSpawn = false;
				NPC.unlockedSlimePurpleSpawn = false;
				NPC.unlockedSlimeRainbowSpawn = false;
				NPC.unlockedSlimeRedSpawn = false;
				NPC.unlockedSlimeYellowSpawn = false;
				Main.townNPCCanSpawn[670] = false;
				Main.townNPCCanSpawn[678] = false;
				Main.townNPCCanSpawn[679] = false;
				Main.townNPCCanSpawn[680] = false;
				Main.townNPCCanSpawn[681] = false;
				Main.townNPCCanSpawn[682] = false;
				Main.townNPCCanSpawn[683] = false;
				Main.townNPCCanSpawn[684] = false;
			}
		}
	}
}
