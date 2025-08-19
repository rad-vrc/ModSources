using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000223 RID: 547
	public class VanillaFountainBiomes : ModPlayer
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x0006754C File Offset: 0x0006574C
		public override void PostUpdateMiscEffects()
		{
			if (QoLCompendium.mainConfig.FountainsCauseBiomes)
			{
				if (Main.SceneMetrics.ActiveFountainColor == 0)
				{
					base.Player.ZoneBeach = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 2)
				{
					base.Player.ZoneCorrupt = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 3)
				{
					base.Player.ZoneJungle = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 4 && Main.hardMode)
				{
					base.Player.ZoneHallow = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 5)
				{
					base.Player.ZoneSnow = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 6 || Main.SceneMetrics.ActiveFountainColor == 12)
				{
					base.Player.ZoneDesert = true;
				}
				if ((Main.SceneMetrics.ActiveFountainColor == 6 || Main.SceneMetrics.ActiveFountainColor == 12) && base.Player.Center.Y > 3200f)
				{
					base.Player.ZoneUndergroundDesert = true;
				}
				if (Main.SceneMetrics.ActiveFountainColor == 10)
				{
					base.Player.ZoneCrimson = true;
				}
			}
			if (QoLCompendium.mainConfig.FountainsWorkFromInventories)
			{
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[0]))
				{
					base.Player.ZoneBeach = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[1]))
				{
					base.Player.ZoneCorrupt = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[2]))
				{
					base.Player.ZoneJungle = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[3]) && Main.hardMode)
				{
					base.Player.ZoneHallow = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[4]))
				{
					base.Player.ZoneSnow = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[5]) || base.Player.HasItemInAnyInventory(Common.VanillaFountains[6]))
				{
					base.Player.ZoneDesert = true;
				}
				if ((base.Player.HasItemInAnyInventory(Common.VanillaFountains[5]) || base.Player.HasItemInAnyInventory(Common.VanillaFountains[6])) && base.Player.Center.Y > 3200f)
				{
					base.Player.ZoneDesert = true;
				}
				if (base.Player.HasItemInAnyInventory(Common.VanillaFountains[7]))
				{
					base.Player.ZoneCrimson = true;
				}
			}
		}
	}
}
