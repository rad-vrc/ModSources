using System;
using System.Collections.Generic;
using CrystalDragons.Content;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x0200024C RID: 588
	[ExtendsFromMod(new string[]
	{
		"CrystalDragons"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"CrystalDragons"
	})]
	public class CrystalDragonsRaceRemovePrefixes : GlobalItem
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0006F33C File Offset: 0x0006D53C
		public override bool AllowPrefix(Item item, int pre)
		{
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazHardened"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazWarding"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBulwark"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazGenin"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazShinobi"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazIai"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazDiverse"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBalanced"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazEqualized"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazMartial"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazStriking"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazEviscerating"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazViolent"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazDestructive"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazAnnihilating"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazFit"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazStrong"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazMighty"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBrisk"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazFleet"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazQuantum"));
			hashSet.Add(Common.GetModPrefix(ModConditions.crystalDragonsMod, "L A K E"));
			HashSet<int> prefixes = hashSet;
			if (!Main.gameMenu && Main.LocalPlayer.active && ModConditions.mrPlagueRacesLoaded && !Main.LocalPlayer.GetModPlayer<CrystalDragonPlayer>().topaz && prefixes.Contains(pre) && ++CrystalDragonsRaceRemovePrefixes.infiniteLoopHackFix < 30)
			{
				return false;
			}
			CrystalDragonsRaceRemovePrefixes.infiniteLoopHackFix = 0;
			return base.AllowPrefix(item, pre);
		}

		// Token: 0x040005B1 RID: 1457
		private static int infiniteLoopHackFix;
	}
}
