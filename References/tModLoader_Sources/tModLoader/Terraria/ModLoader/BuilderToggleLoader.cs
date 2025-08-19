using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.ModLoader
{
	// Token: 0x0200014C RID: 332
	public static class BuilderToggleLoader
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x004CF515 File Offset: 0x004CD715
		public static int BuilderToggleCount
		{
			get
			{
				return BuilderToggleLoader.BuilderToggles.Count;
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x004CF524 File Offset: 0x004CD724
		static BuilderToggleLoader()
		{
			BuilderToggleLoader.RegisterDefaultToggles();
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x004CF5D9 File Offset: 0x004CD7D9
		internal static int Add(BuilderToggle builderToggle)
		{
			BuilderToggleLoader.BuilderToggles.Add(builderToggle);
			return BuilderToggleLoader.BuilderToggles.Count - 1;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x004CF5F2 File Offset: 0x004CD7F2
		internal static void Unload()
		{
			BuilderToggleLoader.BuilderToggles.RemoveRange(BuilderToggleLoader.DefaultDisplayCount, BuilderToggleLoader.BuilderToggles.Count - BuilderToggleLoader.DefaultDisplayCount);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x004CF614 File Offset: 0x004CD814
		internal static void ResizeArrays()
		{
			IEnumerable<BuilderToggle> enumerable = BuilderToggleLoader.BuilderToggles.TakeLast(BuilderToggleLoader.BuilderToggles.Count - BuilderToggleLoader.DefaultDisplayCount);
			List<BuilderToggle> sortedToggles = BuilderToggleLoader.BuilderToggles.Take(BuilderToggleLoader.DefaultDisplayCount).ToList<BuilderToggle>();
			foreach (BuilderToggle toggle in enumerable)
			{
				BuilderToggle.Position orderPosition = toggle.OrderPosition;
				BuilderToggle.Before before = orderPosition as BuilderToggle.Before;
				if (before == null)
				{
					BuilderToggle.After after = orderPosition as BuilderToggle.After;
					if (after == null)
					{
						sortedToggles.Add(toggle);
					}
					else
					{
						int index = sortedToggles.IndexOf(after.Toggle);
						if (index != -1)
						{
							sortedToggles.Insert(index + 1, toggle);
						}
						else
						{
							sortedToggles.Add(toggle);
						}
					}
				}
				else
				{
					int index2 = sortedToggles.IndexOf(before.Toggle);
					if (index2 != -1)
					{
						sortedToggles.Insert(index2, toggle);
					}
					else
					{
						sortedToggles.Add(toggle);
					}
				}
			}
			BuilderToggleLoader._drawOrder = sortedToggles;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x004CF70C File Offset: 0x004CD90C
		internal static void RegisterDefaultToggles()
		{
			int[] defaultTogglesShowOrder = new int[]
			{
				10,
				11,
				8,
				9,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			};
			int i = 0;
			foreach (BuilderToggle builderToggle in BuilderToggleLoader.BuilderToggles)
			{
				builderToggle.Type = defaultTogglesShowOrder[i++];
				ContentInstance.Register(builderToggle);
				ModTypeLookup<BuilderToggle>.Register(builderToggle);
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x004CF784 File Offset: 0x004CD984
		internal static List<BuilderToggle> ActiveBuilderTogglesList()
		{
			List<BuilderToggle> activeToggles = new List<BuilderToggle>(BuilderToggleLoader._drawOrder.Count);
			for (int i = 0; i < BuilderToggleLoader._drawOrder.Count; i++)
			{
				if (BuilderToggleLoader._drawOrder[i].Active())
				{
					activeToggles.Add(BuilderToggleLoader._drawOrder[i]);
				}
			}
			return activeToggles;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x004CF7DA File Offset: 0x004CD9DA
		public static int ActiveBuilderToggles()
		{
			return BuilderToggleLoader.ActiveBuilderTogglesList().Count;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x004CF7E6 File Offset: 0x004CD9E6
		public static bool Active(BuilderToggle builderToggle)
		{
			return builderToggle.Active();
		}

		// Token: 0x040014AD RID: 5293
		internal static readonly List<BuilderToggle> BuilderToggles = new List<BuilderToggle>
		{
			BuilderToggle.BlockSwap,
			BuilderToggle.TorchBiome,
			BuilderToggle.HideAllWires,
			BuilderToggle.ActuatorsVisibility,
			BuilderToggle.RulerLine,
			BuilderToggle.RulerGrid,
			BuilderToggle.AutoActuate,
			BuilderToggle.AutoPaint,
			BuilderToggle.RedWireVisibility,
			BuilderToggle.BlueWireVisibility,
			BuilderToggle.GreenWireVisibility,
			BuilderToggle.YellowWireVisibility
		};

		// Token: 0x040014AE RID: 5294
		private static List<BuilderToggle> _drawOrder;

		// Token: 0x040014AF RID: 5295
		internal static readonly int DefaultDisplayCount = BuilderToggleLoader.BuilderToggles.Count;

		// Token: 0x040014B0 RID: 5296
		public static int BuilderTogglePage = 0;
	}
}
