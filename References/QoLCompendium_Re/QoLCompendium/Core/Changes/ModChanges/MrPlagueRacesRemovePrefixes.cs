using System;
using System.Collections.Generic;
using MrPlagueRaces;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000254 RID: 596
	[ExtendsFromMod(new string[]
	{
		"MrPlagueRaces"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"MrPlagueRaces"
	})]
	public class MrPlagueRacesRemovePrefixes : GlobalItem
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0007008C File Offset: 0x0006E28C
		public override bool AllowPrefix(Item item, int pre)
		{
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Accelerative"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Bewitched"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Bombarding"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Combustible"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Constructive"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Explosive"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Flawless"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Fortunate"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Hexed"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Immolating"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Impactful"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Luminescent"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Recreational"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Regenerative"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Reinforced"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Resilient"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Revitalizing"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Streamlined"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Trailblazing"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Tranquilizing"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Undying"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Volatile"));
			hashSet.Add(Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Warping"));
			HashSet<int> prefixes = hashSet;
			if (!Main.gameMenu && Main.LocalPlayer.active && ModConditions.mrPlagueRacesLoaded && !MrPlagueRacesConfig.Instance.raceStats && prefixes.Contains(pre) && ++MrPlagueRacesRemovePrefixes.infiniteLoopHackFix < 30)
			{
				return false;
			}
			MrPlagueRacesRemovePrefixes.infiniteLoopHackFix = 0;
			return base.AllowPrefix(item, pre);
		}

		// Token: 0x040005B3 RID: 1459
		private static int infiniteLoopHackFix;
	}
}
