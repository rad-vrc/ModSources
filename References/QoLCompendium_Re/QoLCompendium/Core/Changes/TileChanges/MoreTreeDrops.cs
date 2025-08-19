using System;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021D RID: 541
	public class MoreTreeDrops : ModSystem
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x00066D66 File Offset: 0x00064F66
		public override void Load()
		{
			On_WorldGen.SetGemTreeDrops += new On_WorldGen.hook_SetGemTreeDrops(this.GemAlways);
			MoreTreeDrops.ShakeTreeTweak.Load();
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00066D7E File Offset: 0x00064F7E
		public override void Unload()
		{
			On_WorldGen.SetGemTreeDrops -= new On_WorldGen.hook_SetGemTreeDrops(this.GemAlways);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00066D91 File Offset: 0x00064F91
		private void GemAlways(On_WorldGen.orig_SetGemTreeDrops orig, int gemType, int seedType, Tile tileCache, ref int dropItem, ref int secondaryItem)
		{
			if (QoLCompendium.mainConfig.TreesDropMoreWhenShook)
			{
				dropItem = gemType;
				secondaryItem = seedType;
				return;
			}
			orig.Invoke(gemType, seedType, tileCache, ref dropItem, ref secondaryItem);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00066DB8 File Offset: 0x00064FB8
		public static int GetShakeTreeFruit(TreeTypes treeType)
		{
			switch (treeType)
			{
			case TreeTypes.Forest:
			{
				WeightedRandom<short> weightedRandom = new WeightedRandom<short>();
				weightedRandom.Add(4009, 1.0);
				weightedRandom.Add(4282, 1.0);
				weightedRandom.Add(4293, 1.0);
				weightedRandom.Add(4290, 1.0);
				weightedRandom.Add(4291, 1.0);
				return (int)weightedRandom.Get();
			}
			case TreeTypes.Corrupt:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4284;
				}
				return 4289;
			case TreeTypes.Crimson:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4296;
				}
				return 4285;
			case TreeTypes.Jungle:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4294;
				}
				return 4292;
			case TreeTypes.Snow:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4286;
				}
				return 4295;
			case TreeTypes.Hallowed:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4297;
				}
				return 4288;
			case TreeTypes.Palm:
			case TreeTypes.PalmCrimson:
			case TreeTypes.PalmCorrupt:
			case TreeTypes.PalmHallowed:
				if (WorldGen.genRand.NextBool(2))
				{
					return 4283;
				}
				return 4287;
			case TreeTypes.Ash:
				if (WorldGen.genRand.NextBool(2))
				{
					return 5278;
				}
				return 5277;
			}
			return -1;
		}

		// Token: 0x02000544 RID: 1348
		private class ShakeTreeTweak
		{
			// Token: 0x06001C95 RID: 7317 RVA: 0x000925B4 File Offset: 0x000907B4
			public unsafe static void Load()
			{
				FieldInfo numShakes = typeof(WorldGen).GetField("numTreeShakes", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo maxShakes = typeof(WorldGen).GetField("maxTreeShakes", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo shakeX = typeof(WorldGen).GetField("treeShakeX", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo shakeY = typeof(WorldGen).GetField("treeShakeY", BindingFlags.Static | BindingFlags.NonPublic);
				On_WorldGen.ShakeTree += delegate(On_WorldGen.orig_ShakeTree orig, int i, int j)
				{
					if (!QoLCompendium.mainConfig.TreesDropMoreWhenShook)
					{
						orig.Invoke(i, j);
						return;
					}
					MoreTreeDrops.ShakeTreeTweak._isShakingTree = true;
					MoreTreeDrops.ShakeTreeTweak._hasItemDropped = false;
					bool treeShaken = false;
					int x;
					int y;
					WorldGen.GetTreeBottom(i, j, out x, out y);
					for (int k = 0; k < (int)numShakes.GetValue(null); k++)
					{
						if (shakeX.GetValue(null).Equals(k) && shakeY.GetValue(null).Equals(k))
						{
							treeShaken = true;
							break;
						}
					}
					orig.Invoke(i, j);
					MoreTreeDrops.ShakeTreeTweak._isShakingTree = false;
					if ((int)numShakes.GetValue(null) == (int)maxShakes.GetValue(null) || MoreTreeDrops.ShakeTreeTweak._hasItemDropped || treeShaken)
					{
						return;
					}
					TreeTypes treeType = WorldGen.GetTreeType((int)(*Main.tile[x, y].TileType));
					if (treeType == TreeTypes.None)
					{
						return;
					}
					y--;
					while (y > 10 && Main.tile[x, y].HasTile && TileID.Sets.IsShakeable[(int)(*Main.tile[x, y].TileType)])
					{
						y--;
					}
					y++;
					if (!WorldGen.IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
					{
						return;
					}
					int fruit = MoreTreeDrops.GetShakeTreeFruit(treeType);
					if (fruit > -1)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, fruit, 1, false, 0, false, false);
					}
				};
			}

			// Token: 0x04000EE2 RID: 3810
			private static bool _isShakingTree;

			// Token: 0x04000EE3 RID: 3811
			private static bool _hasItemDropped;

			// Token: 0x0200057E RID: 1406
			private class ShakeTreeItem : GlobalItem
			{
				// Token: 0x06001D03 RID: 7427 RVA: 0x00092E2E File Offset: 0x0009102E
				public override void OnSpawn(Item item, IEntitySource source)
				{
					if (MoreTreeDrops.ShakeTreeTweak._isShakingTree && source is EntitySource_ShakeTree)
					{
						MoreTreeDrops.ShakeTreeTweak._hasItemDropped = true;
					}
				}
			}
		}
	}
}
