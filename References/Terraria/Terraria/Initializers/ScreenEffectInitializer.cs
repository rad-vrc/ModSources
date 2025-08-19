﻿using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace Terraria.Initializers
{
	// Token: 0x020000E8 RID: 232
	public static class ScreenEffectInitializer
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x004BCD18 File Offset: 0x004BAF18
		public static void Load()
		{
			Filters.Scene["Nebula"] = new Filter(new ScreenShaderData("FilterTower").UseColor(1f, 0f, 0.9f).UseOpacity(0.35f), EffectPriority.High);
			Filters.Scene["Solar"] = new Filter(new ScreenShaderData("FilterTower").UseColor(1f, 0.7f, 0f).UseOpacity(0.3f), EffectPriority.High);
			Filters.Scene["Stardust"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.High);
			Filters.Scene["Vortex"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.7f, 0.7f).UseOpacity(0.5f), EffectPriority.High);
			Filters.Scene["MonolithNebula"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0f, 0.9f).UseOpacity(0.35f), EffectPriority.Medium);
			Filters.Scene["MonolithSolar"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.7f, 0f).UseOpacity(0.3f), EffectPriority.Medium);
			Filters.Scene["MonolithStardust"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.Medium);
			Filters.Scene["MonolithVortex"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.7f, 0.7f).UseOpacity(0.5f), EffectPriority.Medium);
			Filters.Scene["MoonLord"] = new Filter(new MoonLordScreenShaderData("FilterMoonLord", false), EffectPriority.VeryHigh);
			Filters.Scene["MoonLordShake"] = new Filter(new MoonLordScreenShaderData("FilterMoonLordShake", false), EffectPriority.VeryHigh);
			Filters.Scene["MonolithMoonLord"] = new Filter(new MoonLordScreenShaderData("FilterMoonLord", true), EffectPriority.Medium);
			Filters.Scene["Graveyard"] = new Filter(new ScreenShaderData("FilterGraveyard"), EffectPriority.Medium);
			Filters.Scene["testInvert"] = new Filter(new ScreenShaderData("FilterInvert"), EffectPriority.VeryHigh);
			Filters.Scene["BloodMoon"] = new Filter(new BloodMoonScreenShaderData("FilterBloodMoon").UseColor(2f, -0.8f, -0.6f), EffectPriority.Medium);
			Filters.Scene["Sepia"] = new Filter(new SepiaScreenShaderData("FilterSepia").UseImage("Images/DSTNoise", 0, null).UseIntensity(1f), EffectPriority.Medium);
			Filters.Scene["Sandstorm"] = new Filter(new SandstormShaderData("FilterSandstormForeground").UseColor(1.1f, 1f, 0.5f).UseSecondaryColor(0.7f, 0.5f, 0.3f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f), EffectPriority.High);
			Overlays.Scene["Sandstorm"] = new SimpleOverlay("Images/Misc/noise", new SandstormShaderData("FilterSandstormBackground").UseColor(1.1f, 1f, 0.5f).UseSecondaryColor(0.7f, 0.5f, 0.3f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f), EffectPriority.High, RenderLayers.Landscape);
			Filters.Scene["Blizzard"] = new Filter(new BlizzardShaderData("FilterBlizzardForeground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
			Overlays.Scene["Blizzard"] = new SimpleOverlay("Images/Misc/noise", new BlizzardShaderData("FilterBlizzardBackground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High, RenderLayers.Landscape);
			Filters.Scene["HeatDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);
			Filters.Scene["WaterDistortion"] = new Filter(new WaterShaderData("FilterWaterDistortion").UseIntensity(1f).UseImage("Images/Misc/noise", 0, null), EffectPriority.VeryHigh);
			Filters.Scene["CrystalDestructionColor"] = new Filter(new ScreenShaderData("FilterCrystalDestructionColor").UseColor(1f, 0f, 0.75f).UseIntensity(1f).UseOpacity(0.8f), EffectPriority.VeryHigh);
			Filters.Scene["CrystalDestructionVortex"] = new Filter(new ScreenShaderData("FilterCrystalDestructionVortex").UseImage("Images/Misc/noise", 0, null), EffectPriority.VeryHigh);
			Filters.Scene["CrystalWin"] = new Filter(new ScreenShaderData("FilterCrystalWin"), EffectPriority.VeryHigh);
			Filters.Scene["Test"] = new Filter(new ScreenShaderData("FilterTest"), EffectPriority.VeryHigh);
			Filters.Scene["Test2"] = new Filter(new ScreenShaderData("FilterTest2"), EffectPriority.VeryHigh);
			Filters.Scene["CRT"] = new Filter(new ScreenShaderData("FilterCRT"), EffectPriority.VeryHigh);
			Filters.Scene["Test3"] = new Filter(new ScreenShaderData("FilterTest3").UseImage("Images/Extra_" + 156, 0, null), EffectPriority.VeryHigh);
			Overlays.Scene.Load();
			Filters.Scene.Load();
			ScreenEffectInitializer.LoadSkies();
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x004BD368 File Offset: 0x004BB568
		private static void LoadSkies()
		{
			SkyManager.Instance["Party"] = new PartySky();
			SkyManager.Instance["Martian"] = new MartianSky();
			SkyManager.Instance["Nebula"] = new NebulaSky();
			SkyManager.Instance["Stardust"] = new StardustSky();
			SkyManager.Instance["Vortex"] = new VortexSky();
			SkyManager.Instance["Solar"] = new SolarSky();
			SkyManager.Instance["Slime"] = new SlimeSky();
			SkyManager.Instance["MoonLord"] = new MoonLordSky(false);
			SkyManager.Instance["CreditsRoll"] = new CreditsRollSky();
			SkyManager.Instance["MonolithNebula"] = new NebulaSky();
			SkyManager.Instance["MonolithStardust"] = new StardustSky();
			SkyManager.Instance["MonolithVortex"] = new VortexSky();
			SkyManager.Instance["MonolithSolar"] = new SolarSky();
			SkyManager.Instance["MonolithMoonLord"] = new MoonLordSky(true);
			SkyManager.Instance["Sandstorm"] = new SandstormSky();
			SkyManager.Instance["Blizzard"] = new BlizzardSky();
			SkyManager.Instance["Ambience"] = new AmbientSky();
			SkyManager.Instance["Lantern"] = new LanternSky();
			SkyManager.Instance.Load();
		}
	}
}
