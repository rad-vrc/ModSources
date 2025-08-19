using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023D RID: 573
	public class NoLavaFromSlimes : GlobalNPC
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0006D1B0 File Offset: 0x0006B3B0
		public unsafe override void HitEffect(NPC npc, NPC.HitInfo hit)
		{
			if (npc.type != 59 || Main.netMode == 1 || npc.life > 0)
			{
				return;
			}
			try
			{
				if (QoLCompendium.mainConfig.LavaSlimesDontDropLava)
				{
					int num = (int)(npc.Center.X / 16f);
					int num2 = (int)(npc.Center.Y / 16f);
					if (!WorldGen.SolidTile(num, num2, false) && Main.tile[num, num2].CheckingLiquid)
					{
						*Main.tile[num, num2].LiquidAmount = 0;
						WorldGen.SquareTileFrame(num, num2, true);
					}
				}
			}
			catch
			{
			}
		}
	}
}
