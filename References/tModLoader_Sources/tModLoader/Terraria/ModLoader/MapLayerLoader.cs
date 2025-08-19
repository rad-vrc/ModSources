using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria.Map;

namespace Terraria.ModLoader
{
	// Token: 0x02000194 RID: 404
	public static class MapLayerLoader
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x004E097A File Offset: 0x004DEB7A
		public static int MapLayerCount
		{
			get
			{
				return MapLayerLoader.MapLayers.Count;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x004E0986 File Offset: 0x004DEB86
		internal static IMapLayer FirstVanillaLayer
		{
			get
			{
				return MapLayerLoader.MapLayers[0];
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x004E0993 File Offset: 0x004DEB93
		internal static IMapLayer LastVanillaLayer
		{
			get
			{
				return MapLayerLoader.MapLayers[MapLayerLoader.DefaultLayerCount - 1];
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x004E09A6 File Offset: 0x004DEBA6
		private static IEnumerable<ModMapLayer> ModdedLayers
		{
			get
			{
				return MapLayerLoader.MapLayers.Skip(MapLayerLoader.DefaultLayerCount).Cast<ModMapLayer>();
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x004E09BC File Offset: 0x004DEBBC
		internal static void Add(IMapLayer layer)
		{
			MapLayerLoader.MapLayers.Add(layer);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x004E09C9 File Offset: 0x004DEBC9
		internal static void Unload()
		{
			MapLayerLoader.MapLayers.RemoveRange(MapLayerLoader.DefaultLayerCount, MapLayerLoader.MapLayerCount - MapLayerLoader.DefaultLayerCount);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x004E09E8 File Offset: 0x004DEBE8
		internal static void ResizeArrays()
		{
			List<ModMapLayer>[] sortingSlots = new List<ModMapLayer>[MapLayerLoader.DefaultLayerCount + 1];
			for (int i = 0; i < sortingSlots.Length; i++)
			{
				sortingSlots[i] = new List<ModMapLayer>();
			}
			foreach (ModMapLayer layer2 in MapLayerLoader.ModdedLayers)
			{
				MapLayerLoader.<>c__DisplayClass12_0 CS$<>8__locals1;
				CS$<>8__locals1.layer = layer2;
				ModMapLayer.Position position = CS$<>8__locals1.layer.GetDefaultPosition();
				ModMapLayer.After after = position as ModMapLayer.After;
				if (after == null)
				{
					ModMapLayer.Before before = position as ModMapLayer.Before;
					if (before == null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
						defaultInterpolatedStringHandler.AppendLiteral("ModMapLayer ");
						defaultInterpolatedStringHandler.AppendFormatted<ModMapLayer>(CS$<>8__locals1.layer);
						defaultInterpolatedStringHandler.AppendLiteral(" has unknown Position ");
						defaultInterpolatedStringHandler.AppendFormatted<ModMapLayer.Position>(position);
						throw MapLayerLoader.<ResizeArrays>g__BlameMapLayerException|12_0(new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear()), ref CS$<>8__locals1);
					}
					int beforeIndex = MapLayerLoader.MapLayers.IndexOf(before.Layer);
					if (beforeIndex >= MapLayerLoader.DefaultLayerCount || beforeIndex == -1)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 1);
						defaultInterpolatedStringHandler.AppendLiteral("ModMapLayer ");
						defaultInterpolatedStringHandler.AppendFormatted<ModMapLayer>(CS$<>8__locals1.layer);
						defaultInterpolatedStringHandler.AppendLiteral(" did not refer to a vanilla map layer in GetDefaultPosition()");
						throw MapLayerLoader.<ResizeArrays>g__BlameMapLayerException|12_0(new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear()), ref CS$<>8__locals1);
					}
					sortingSlots[beforeIndex].Add(CS$<>8__locals1.layer);
				}
				else
				{
					int afterIndex = MapLayerLoader.MapLayers.IndexOf(after.Layer);
					if (afterIndex >= MapLayerLoader.DefaultLayerCount || afterIndex == -1)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 1);
						defaultInterpolatedStringHandler.AppendLiteral("ModMapLayer ");
						defaultInterpolatedStringHandler.AppendFormatted<ModMapLayer>(CS$<>8__locals1.layer);
						defaultInterpolatedStringHandler.AppendLiteral(" did not refer to a vanilla map layer in GetDefaultPosition()");
						throw MapLayerLoader.<ResizeArrays>g__BlameMapLayerException|12_0(new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear()), ref CS$<>8__locals1);
					}
					sortingSlots[afterIndex + 1].Add(CS$<>8__locals1.layer);
				}
			}
			List<IMapLayer> sortedLayers = new List<IMapLayer>();
			for (int j = 0; j < MapLayerLoader.DefaultLayerCount + 1; j++)
			{
				List<ModMapLayer> elements = sortingSlots[j];
				foreach (IMapLayer layer in new TopoSort<IMapLayer>(elements, delegate(IMapLayer l)
				{
					ModMapLayer modMapLayer = l as ModMapLayer;
					IEnumerable<IMapLayer> enumerable;
					if (modMapLayer == null)
					{
						enumerable = null;
					}
					else
					{
						IEnumerable<ModMapLayer.Position> moddedConstraints = modMapLayer.GetModdedConstraints();
						if (moddedConstraints == null)
						{
							enumerable = null;
						}
						else
						{
							enumerable = (from a in moddedConstraints.OfType<ModMapLayer.After>()
							select a.Layer).Where(new Func<IMapLayer, bool>(elements.Contains<IMapLayer>));
						}
					}
					return enumerable ?? Array.Empty<IMapLayer>();
				}, delegate(IMapLayer l)
				{
					ModMapLayer modMapLayer = l as ModMapLayer;
					IEnumerable<IMapLayer> enumerable;
					if (modMapLayer == null)
					{
						enumerable = null;
					}
					else
					{
						IEnumerable<ModMapLayer.Position> moddedConstraints = modMapLayer.GetModdedConstraints();
						if (moddedConstraints == null)
						{
							enumerable = null;
						}
						else
						{
							enumerable = (from b in moddedConstraints.OfType<ModMapLayer.Before>()
							select b.Layer).Where(new Func<IMapLayer, bool>(elements.Contains<IMapLayer>));
						}
					}
					return enumerable ?? Array.Empty<IMapLayer>();
				}).Sort())
				{
					sortedLayers.Add(layer);
				}
				if (j < MapLayerLoader.DefaultLayerCount)
				{
					sortedLayers.Add(MapLayerLoader.MapLayers[j]);
				}
			}
			Main.MapIcons = MapLayerLoader.CreateOverlayWithLayers(sortedLayers);
			Main.Pings = (PingMapLayer)IMapLayer.Pings;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x004E0CB8 File Offset: 0x004DEEB8
		private static MapIconOverlay CreateOverlayWithLayers(IEnumerable<IMapLayer> layers)
		{
			MapIconOverlay overlay = new MapIconOverlay();
			foreach (IMapLayer layer in layers)
			{
				overlay.AddLayer(layer);
			}
			return overlay;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x004E0D08 File Offset: 0x004DEF08
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static MapLayerLoader()
		{
			List<IMapLayer> list = new List<IMapLayer>();
			CollectionsMarshal.SetCount<IMapLayer>(list, 3);
			Span<IMapLayer> span = CollectionsMarshal.AsSpan<IMapLayer>(list);
			int num = 0;
			*span[num] = IMapLayer.Spawn;
			num++;
			*span[num] = IMapLayer.Pylons;
			num++;
			*span[num] = IMapLayer.Pings;
			num++;
			MapLayerLoader.MapLayers = list;
			MapLayerLoader.DefaultLayerCount = MapLayerLoader.MapLayers.Count;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x004E0D74 File Offset: 0x004DEF74
		[CompilerGenerated]
		internal static Exception <ResizeArrays>g__BlameMapLayerException|12_0(Exception ex, ref MapLayerLoader.<>c__DisplayClass12_0 A_1)
		{
			if (A_1.layer != null)
			{
				ModMapLayer moddedLayer = A_1.layer;
				ex.Data["mod"] = moddedLayer.Mod.Name;
			}
			return ex;
		}

		// Token: 0x04001672 RID: 5746
		internal static readonly List<IMapLayer> MapLayers;

		// Token: 0x04001673 RID: 5747
		internal static readonly int DefaultLayerCount;
	}
}
