using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000253 RID: 595
	public class ModFailBreak : GlobalTile
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0006FF54 File Offset: 0x0006E154
		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (QoLCompendium.mainConfig.RegrowthAutoReplant && (Main.LocalPlayer.HeldItem.type == 213 || Main.LocalPlayer.HeldItem.type == 5295 || (Main.mouseItem != null && (Main.mouseItem.type == 213 || Main.mouseItem.type == 5295))))
			{
				HashSet<int> hashSet = new HashSet<int>();
				hashSet.Add(Common.GetModTile(ModConditions.depthsMod, "ShadowShrub"));
				hashSet.Add(Common.GetModTile(ModConditions.redemptionMod, "NightshadeTile"));
				hashSet.Add(Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Welkinbell"));
				hashSet.Add(Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Illumifern"));
				hashSet.Add(Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Enduflora"));
				hashSet.Add(Common.GetModTile(ModConditions.spiritMod, "Cloudstalk"));
				hashSet.Add(Common.GetModTile(ModConditions.spiritMod, "SoulBloomTile"));
				hashSet.Add(Common.GetModTile(ModConditions.thoriumMod, "MarineKelp2"));
				if (hashSet.Contains(type))
				{
					fail = true;
				}
			}
		}
	}
}
