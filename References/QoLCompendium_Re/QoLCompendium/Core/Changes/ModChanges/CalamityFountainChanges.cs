using System;
using System.Reflection;
using CalamityMod;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Furniture.Fountains;
using MonoMod.RuntimeDetour;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000249 RID: 585
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	public class CalamityFountainChanges : ModSystem
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0006EDDC File Offset: 0x0006CFDC
		public override void Load()
		{
			Hook astralInfection = new Hook(CalamityFountainChanges.AstralInfectionActiveMethod, new Func<CalamityFountainChanges.Orig_AstralInfectionActive, AstralInfectionBiome, Player, bool>(CalamityFountainChanges.AstralInfectionActive_Detour));
			astralInfection.Apply();
			Common.detours.Add(astralInfection);
			Hook brimstoneCrags = new Hook(CalamityFountainChanges.BrimstoneCragsActiveMethod, new Func<CalamityFountainChanges.Orig_BrimstoneCragsActive, BrimstoneCragsBiome, Player, bool>(CalamityFountainChanges.BrimstoneCragsActive_Detour));
			brimstoneCrags.Apply();
			Common.detours.Add(brimstoneCrags);
			Hook sulphurousSea = new Hook(CalamityFountainChanges.SulphurousSeaActiveMethod, new Func<CalamityFountainChanges.Orig_SulphurousSeaActive, SulphurousSeaBiome, Player, bool>(CalamityFountainChanges.SulphurousSeaActive_Detour));
			sulphurousSea.Apply();
			Common.detours.Add(sulphurousSea);
			Hook sunkenSea = new Hook(CalamityFountainChanges.SunkenSeaActiveMethod, new Func<CalamityFountainChanges.Orig_SunkenSeaActive, SunkenSeaBiome, Player, bool>(CalamityFountainChanges.SunkenSeaActive_Detour));
			sunkenSea.Apply();
			Common.detours.Add(sunkenSea);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0006EE8C File Offset: 0x0006D08C
		internal static bool AstralInfectionActive_Detour(CalamityFountainChanges.Orig_AstralInfectionActive orig, AstralInfectionBiome self, Player player)
		{
			bool result = orig(self, player);
			if (QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>("CalamityMod/AstralWater").Slot)
			{
				return true;
			}
			if (QoLCompendium.mainConfig.FountainsWorkFromInventories && player.HasItemInAnyInventory(ModContent.ItemType<AstralFountainItem>()))
			{
				Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>("CalamityMod/AstralWater").Slot;
				return true;
			}
			return result;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0006EF00 File Offset: 0x0006D100
		internal static bool BrimstoneCragsActive_Detour(CalamityFountainChanges.Orig_BrimstoneCragsActive orig, BrimstoneCragsBiome self, Player player)
		{
			bool result = orig(self, player);
			if (QoLCompendium.mainConfig.FountainsCauseBiomes && CalamityUtils.Calamity(player).BrimstoneLavaFountainCounter > 0)
			{
				return true;
			}
			if (QoLCompendium.mainConfig.FountainsWorkFromInventories && player.HasItemInAnyInventory(ModContent.ItemType<BrimstoneLavaFountainItem>()))
			{
				CalamityUtils.Calamity(player).BrimstoneLavaFountainCounter = 1;
				return true;
			}
			return result;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0006EF5C File Offset: 0x0006D15C
		internal static bool SulphurousSeaActive_Detour(CalamityFountainChanges.Orig_SulphurousSeaActive orig, SulphurousSeaBiome self, Player player)
		{
			bool result = orig(self, player);
			string text = Main.zenithWorld ? "CalamityMod/PissWater" : "CalamityMod/SulphuricWater";
			if (QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>(text).Slot)
			{
				return true;
			}
			if (QoLCompendium.mainConfig.FountainsWorkFromInventories && player.HasItemInAnyInventory(ModContent.ItemType<SulphurousFountainItem>()))
			{
				Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>(text).Slot;
				return true;
			}
			return result;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0006EFDC File Offset: 0x0006D1DC
		internal static bool SunkenSeaActive_Detour(CalamityFountainChanges.Orig_SunkenSeaActive orig, SunkenSeaBiome self, Player player)
		{
			bool result = orig(self, player);
			if (QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>("CalamityMod/SunkenSeaWater").Slot)
			{
				return true;
			}
			if (QoLCompendium.mainConfig.FountainsWorkFromInventories && player.HasItemInAnyInventory(ModContent.ItemType<SunkenSeaFountain>()))
			{
				Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>("CalamityMod/SunkenSeaWater").Slot;
				return true;
			}
			return result;
		}

		// Token: 0x040005AD RID: 1453
		private static readonly MethodInfo AstralInfectionActiveMethod = typeof(AstralInfectionBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);

		// Token: 0x040005AE RID: 1454
		private static readonly MethodInfo BrimstoneCragsActiveMethod = typeof(BrimstoneCragsBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);

		// Token: 0x040005AF RID: 1455
		private static readonly MethodInfo SulphurousSeaActiveMethod = typeof(SulphurousSeaBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);

		// Token: 0x040005B0 RID: 1456
		private static readonly MethodInfo SunkenSeaActiveMethod = typeof(SunkenSeaBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);

		// Token: 0x02000554 RID: 1364
		// (Invoke) Token: 0x06001CC9 RID: 7369
		public delegate bool Orig_AstralInfectionActive(AstralInfectionBiome self, Player player);

		// Token: 0x02000555 RID: 1365
		// (Invoke) Token: 0x06001CCD RID: 7373
		public delegate bool Orig_BrimstoneCragsActive(BrimstoneCragsBiome self, Player player);

		// Token: 0x02000556 RID: 1366
		// (Invoke) Token: 0x06001CD1 RID: 7377
		public delegate bool Orig_SulphurousSeaActive(SulphurousSeaBiome self, Player player);

		// Token: 0x02000557 RID: 1367
		// (Invoke) Token: 0x06001CD5 RID: 7381
		public delegate bool Orig_SunkenSeaActive(SunkenSeaBiome self, Player player);
	}
}
