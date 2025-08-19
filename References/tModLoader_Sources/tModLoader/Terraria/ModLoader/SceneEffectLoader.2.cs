using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Graphics.Capture;

namespace Terraria.ModLoader
{
	// Token: 0x020001F7 RID: 503
	public class SceneEffectLoader : Loader<ModSceneEffect>
	{
		// Token: 0x06002715 RID: 10005 RVA: 0x00501EF0 File Offset: 0x005000F0
		public SceneEffectLoader()
		{
			base.Initialize(0);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00501F00 File Offset: 0x00500100
		public void UpdateSceneEffect(Player player)
		{
			SceneEffectLoader.SceneEffectInstance result = new SceneEffectLoader.SceneEffectInstance();
			List<SceneEffectLoader.AtmosWeight> shortList = new List<SceneEffectLoader.AtmosWeight>();
			for (int i = 0; i < this.list.Count; i++)
			{
				ModSceneEffect sceneEffect = this.list[i];
				bool isActive = sceneEffect.IsSceneEffectActive(player);
				sceneEffect.SpecialVisuals(player, isActive);
				if (isActive)
				{
					shortList.Add(new SceneEffectLoader.AtmosWeight(sceneEffect.GetCorrWeight(player), sceneEffect));
				}
			}
			if (shortList.Count == 0)
			{
				player.CurrentSceneEffect = result;
				return;
			}
			result.anyActive = true;
			List<SceneEffectLoader.AtmosWeight> list = shortList;
			Comparison<SceneEffectLoader.AtmosWeight> comparison;
			if ((comparison = SceneEffectLoader.<>O.<0>__InvertedCompare) == null)
			{
				comparison = (SceneEffectLoader.<>O.<0>__InvertedCompare = new Comparison<SceneEffectLoader.AtmosWeight>(SceneEffectLoader.AtmosWeight.InvertedCompare));
			}
			list.Sort(comparison);
			int sceneEffectFields = 0;
			int j = 0;
			while (sceneEffectFields < 8 && j < shortList.Count)
			{
				ModSceneEffect sceneEffect2 = shortList[j].type;
				if (result.waterStyle.priority == SceneEffectPriority.None && sceneEffect2.WaterStyle != null)
				{
					result.waterStyle.value = sceneEffect2.WaterStyle.Slot;
					result.waterStyle.priority = sceneEffect2.Priority;
					sceneEffectFields++;
				}
				if (result.undergroundBackground.priority == SceneEffectPriority.None && sceneEffect2.UndergroundBackgroundStyle != null)
				{
					result.undergroundBackground.value = sceneEffect2.UndergroundBackgroundStyle.Slot;
					result.undergroundBackground.priority = sceneEffect2.Priority;
					sceneEffectFields++;
				}
				if (result.surfaceBackground.priority == SceneEffectPriority.None && sceneEffect2.SurfaceBackgroundStyle != null)
				{
					result.surfaceBackground.value = sceneEffect2.SurfaceBackgroundStyle.Slot;
					result.surfaceBackground.priority = sceneEffect2.Priority;
					sceneEffectFields++;
				}
				if (result.music.priority == SceneEffectPriority.None && sceneEffect2.Music != -1)
				{
					result.music.value = sceneEffect2.Music;
					result.music.priority = sceneEffect2.Priority;
					sceneEffectFields++;
				}
				ModBiome modBiome = sceneEffect2 as ModBiome;
				if (modBiome != null)
				{
					if (result.biomeTorchItemType.priority == SceneEffectPriority.None && modBiome.BiomeTorchItemType != -1)
					{
						result.biomeTorchItemType.value = modBiome.BiomeTorchItemType;
						result.biomeTorchItemType.priority = modBiome.Priority;
						sceneEffectFields++;
					}
					if (result.biomeCampfireItemType.priority == SceneEffectPriority.None && modBiome.BiomeCampfireItemType != -1)
					{
						result.biomeCampfireItemType.value = modBiome.BiomeCampfireItemType;
						result.biomeCampfireItemType.priority = modBiome.Priority;
						sceneEffectFields++;
					}
				}
				if (result.tileColorStyle == CaptureBiome.TileColorStyle.Normal && sceneEffect2.TileColorStyle != CaptureBiome.TileColorStyle.Normal)
				{
					result.tileColorStyle = sceneEffect2.TileColorStyle;
					sceneEffectFields++;
				}
				if (result.mapBackground == null && sceneEffect2.MapBackground != null)
				{
					result.mapBackground = sceneEffect2.MapBackground;
					result.mapBackgroundSceneEffect = sceneEffect2;
					sceneEffectFields++;
				}
				j++;
			}
			player.CurrentSceneEffect = result;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x005021B8 File Offset: 0x005003B8
		public void UpdateMusic(ref int music, ref SceneEffectPriority priority)
		{
			SceneEffectLoader.SceneEffectInstance.PrioritizedPair currentMusic = Main.LocalPlayer.CurrentSceneEffect.music;
			if (currentMusic.value > -1 && currentMusic.priority > priority)
			{
				music = currentMusic.value;
				priority = currentMusic.priority;
			}
		}

		// Token: 0x040018AC RID: 6316
		public const int VanillaSceneEffectCount = 0;

		// Token: 0x020009C2 RID: 2498
		public class SceneEffectInstance
		{
			// Token: 0x06005623 RID: 22051 RVA: 0x0069C4F4 File Offset: 0x0069A6F4
			public SceneEffectInstance()
			{
				this.waterStyle = (this.undergroundBackground = (this.surfaceBackground = (this.music = (this.biomeTorchItemType = (this.biomeCampfireItemType = SceneEffectLoader.SceneEffectInstance.PrioritizedPair.Default)))));
				this.tileColorStyle = CaptureBiome.TileColorStyle.Normal;
				this.mapBackground = null;
			}

			// Token: 0x04006BB2 RID: 27570
			public bool anyActive;

			// Token: 0x04006BB3 RID: 27571
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair waterStyle;

			// Token: 0x04006BB4 RID: 27572
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair undergroundBackground;

			// Token: 0x04006BB5 RID: 27573
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair surfaceBackground;

			// Token: 0x04006BB6 RID: 27574
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair music;

			// Token: 0x04006BB7 RID: 27575
			public string mapBackground;

			// Token: 0x04006BB8 RID: 27576
			public ModSceneEffect mapBackgroundSceneEffect;

			// Token: 0x04006BB9 RID: 27577
			public CaptureBiome.TileColorStyle tileColorStyle;

			// Token: 0x04006BBA RID: 27578
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair biomeTorchItemType;

			// Token: 0x04006BBB RID: 27579
			public SceneEffectLoader.SceneEffectInstance.PrioritizedPair biomeCampfireItemType;

			// Token: 0x02000E22 RID: 3618
			public struct PrioritizedPair
			{
				// Token: 0x04007C44 RID: 31812
				public static readonly SceneEffectLoader.SceneEffectInstance.PrioritizedPair Default = new SceneEffectLoader.SceneEffectInstance.PrioritizedPair
				{
					value = -1
				};

				// Token: 0x04007C45 RID: 31813
				public int value;

				// Token: 0x04007C46 RID: 31814
				public SceneEffectPriority priority;
			}
		}

		// Token: 0x020009C3 RID: 2499
		private struct AtmosWeight
		{
			// Token: 0x06005624 RID: 22052 RVA: 0x0069C54D File Offset: 0x0069A74D
			public AtmosWeight(float weight, ModSceneEffect type)
			{
				this.weight = weight;
				this.type = type;
			}

			// Token: 0x06005625 RID: 22053 RVA: 0x0069C55D File Offset: 0x0069A75D
			public static int InvertedCompare(SceneEffectLoader.AtmosWeight a, SceneEffectLoader.AtmosWeight b)
			{
				return -a.weight.CompareTo(b.weight);
			}

			// Token: 0x04006BBC RID: 27580
			public float weight;

			// Token: 0x04006BBD RID: 27581
			public ModSceneEffect type;
		}

		// Token: 0x020009C4 RID: 2500
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006BBE RID: 27582
			public static Comparison<SceneEffectLoader.AtmosWeight> <0>__InvertedCompare;
		}
	}
}
