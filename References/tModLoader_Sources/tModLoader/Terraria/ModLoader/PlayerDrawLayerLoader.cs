using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	// Token: 0x020001E7 RID: 487
	public static class PlayerDrawLayerLoader
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x004F6AE3 File Offset: 0x004F4CE3
		public static IReadOnlyList<PlayerDrawLayer> Layers
		{
			get
			{
				return PlayerDrawLayerLoader._layers;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x004F6AEA File Offset: 0x004F4CEA
		public static IReadOnlyList<PlayerDrawLayer> DrawOrder
		{
			get
			{
				return PlayerDrawLayerLoader._drawOrder;
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x004F6AF1 File Offset: 0x004F4CF1
		internal static void Add(PlayerDrawLayer layer)
		{
			PlayerDrawLayerLoader._layers.Add(layer);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x004F6B00 File Offset: 0x004F4D00
		internal static void Unload()
		{
			PlayerDrawLayerLoader._layers = new List<PlayerDrawLayer>(PlayerDrawLayers.VanillaLayers);
			foreach (PlayerDrawLayer playerDrawLayer in PlayerDrawLayerLoader._layers)
			{
				playerDrawLayer.ClearChildren();
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x004F6B60 File Offset: 0x004F4D60
		internal static void ResizeArrays()
		{
			Dictionary<PlayerDrawLayer, PlayerDrawLayer.Position> dictionary = PlayerDrawLayerLoader.Layers.ToDictionary((PlayerDrawLayer l) => l, (PlayerDrawLayer l) => l.GetDefaultPosition());
			PlayerLoader.ModifyDrawLayerOrdering(dictionary);
			Dictionary<PlayerDrawLayer, PlayerDrawLayer.Between> betweens = new Dictionary<PlayerDrawLayer, PlayerDrawLayer.Between>();
			foreach (KeyValuePair<PlayerDrawLayer, PlayerDrawLayer.Position> keyValuePair in dictionary.ToArray<KeyValuePair<PlayerDrawLayer, PlayerDrawLayer.Position>>())
			{
				PlayerDrawLayer playerDrawLayer;
				PlayerDrawLayer.Position position;
				keyValuePair.Deconstruct(out playerDrawLayer, out position);
				PlayerDrawLayer layer = playerDrawLayer;
				PlayerDrawLayer.Position pos = position;
				PlayerDrawLayer.Between b = pos as PlayerDrawLayer.Between;
				if (b == null)
				{
					PlayerDrawLayer.BeforeParent b2 = pos as PlayerDrawLayer.BeforeParent;
					if (b2 == null)
					{
						PlayerDrawLayer.AfterParent a = pos as PlayerDrawLayer.AfterParent;
						if (a == null)
						{
							PlayerDrawLayer.Multiple i = pos as PlayerDrawLayer.Multiple;
							if (i != null)
							{
								int slot = 0;
								using (IEnumerator<ValueTuple<PlayerDrawLayer.Between, PlayerDrawLayer.Multiple.Condition>> enumerator = i.Positions.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										ValueTuple<PlayerDrawLayer.Between, PlayerDrawLayer.Multiple.Condition> valueTuple = enumerator.Current;
										PlayerDrawLayer.Between b3 = valueTuple.Item1;
										PlayerDrawLayer.Multiple.Condition cond = valueTuple.Item2;
										betweens.Add(new PlayerDrawLayerSlot(layer, cond, slot++), b3);
									}
									goto IL_193;
								}
							}
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
							defaultInterpolatedStringHandler.AppendLiteral("PlayerDrawLayer ");
							defaultInterpolatedStringHandler.AppendFormatted<PlayerDrawLayer>(layer);
							defaultInterpolatedStringHandler.AppendLiteral(" has unknown Position type ");
							defaultInterpolatedStringHandler.AppendFormatted<PlayerDrawLayer.Position>(pos);
							throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						a.Parent.AddChildAfter(layer);
					}
					else
					{
						b2.Parent.AddChildBefore(layer);
					}
				}
				else
				{
					betweens.Add(layer, b);
				}
				IL_193:;
			}
			foreach (KeyValuePair<PlayerDrawLayer, PlayerDrawLayer.Between> keyValuePair2 in betweens)
			{
				PlayerDrawLayer playerDrawLayer;
				PlayerDrawLayer.Between between;
				keyValuePair2.Deconstruct(out playerDrawLayer, out between);
				PlayerDrawLayer layer2 = playerDrawLayer;
				PlayerDrawLayer.Between between2 = between;
				PlayerDrawLayer after = between2.Layer1;
				if (after != null && !betweens.ContainsKey(after))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(140, 3);
					defaultInterpolatedStringHandler.AppendFormatted(layer2.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" cannot be positioned after ");
					defaultInterpolatedStringHandler.AppendFormatted(after.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" because ");
					defaultInterpolatedStringHandler.AppendFormatted(after.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" does not have a fixed position. Consider using AfterParent or referring to a different layer (or null)");
					ArgumentException ex = new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					object key = "mod";
					ex.Data[key] = layer2.Mod.Name;
					throw ex;
				}
				PlayerDrawLayer before = between2.Layer2;
				if (before != null && !betweens.ContainsKey(before))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(142, 3);
					defaultInterpolatedStringHandler.AppendFormatted(layer2.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" cannot be positioned before ");
					defaultInterpolatedStringHandler.AppendFormatted(before.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" because ");
					defaultInterpolatedStringHandler.AppendFormatted(before.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" does not have a fixed position. Consider using BeforeParent or referring to a different layer (or null)");
					ArgumentException ex2 = new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					object key = "mod";
					ex2.Data[key] = layer2.Mod.Name;
					throw ex2;
				}
			}
			PlayerDrawLayerLoader._drawOrder = new TopoSort<PlayerDrawLayer>(betweens.Keys, delegate(PlayerDrawLayer l)
			{
				PlayerDrawLayer v = betweens[l].Layer1;
				if (v == null)
				{
					return Array.Empty<PlayerDrawLayer>();
				}
				return new <>z__ReadOnlyArray<PlayerDrawLayer>(new PlayerDrawLayer[]
				{
					v
				});
			}, delegate(PlayerDrawLayer l)
			{
				PlayerDrawLayer v = betweens[l].Layer2;
				if (v == null)
				{
					return Array.Empty<PlayerDrawLayer>();
				}
				return new <>z__ReadOnlyArray<PlayerDrawLayer>(new PlayerDrawLayer[]
				{
					v
				});
			}).Sort().ToArray();
		}

		/// <summary>
		/// Note, not threadsafe
		/// </summary>
		// Token: 0x060025F1 RID: 9713 RVA: 0x004F6F14 File Offset: 0x004F5114
		public static PlayerDrawLayer[] GetDrawLayers(PlayerDrawSet drawInfo)
		{
			PlayerDrawLayer[] drawOrder = PlayerDrawLayerLoader._drawOrder;
			for (int i = 0; i < drawOrder.Length; i++)
			{
				drawOrder[i].ResetVisibility(drawInfo);
			}
			PlayerLoader.HideDrawLayers(drawInfo);
			return PlayerDrawLayerLoader._drawOrder;
		}

		// Token: 0x040017DD RID: 6109
		private static List<PlayerDrawLayer> _layers = new List<PlayerDrawLayer>(PlayerDrawLayers.VanillaLayers);

		// Token: 0x040017DE RID: 6110
		private static PlayerDrawLayer[] _drawOrder;
	}
}
