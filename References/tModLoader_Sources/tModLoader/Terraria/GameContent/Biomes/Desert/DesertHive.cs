using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Utilities;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x02000663 RID: 1635
	public static class DesertHive
	{
		// Token: 0x06004728 RID: 18216 RVA: 0x0063A0E4 File Offset: 0x006382E4
		public static void Place(DesertDescription description)
		{
			DesertHive.ClusterGroup clusters = DesertHive.ClusterGroup.FromDescription(description);
			DesertHive.PlaceClusters(description, clusters);
			DesertHive.AddTileVariance(description);
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x0063A108 File Offset: 0x00638308
		private static void PlaceClusters(DesertDescription description, DesertHive.ClusterGroup clusters)
		{
			Rectangle hive = description.Hive;
			hive.Inflate(20, 20);
			DesertHive.PostPlacementEffect[,] array = new DesertHive.PostPlacementEffect[hive.Width, hive.Height];
			DesertHive.PlaceClustersArea(description, clusters, hive, array, Point.Zero);
			for (int i = hive.Left; i < hive.Right; i++)
			{
				for (int j = hive.Top; j < hive.Bottom; j++)
				{
					DesertHive.PostPlacementEffect postPlacementEffect = array[i - hive.Left, j - hive.Top];
					if (postPlacementEffect.HasFlag(DesertHive.PostPlacementEffect.Smooth))
					{
						Tile.SmoothSlope(i, j, false, false);
					}
				}
			}
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0063A1B0 File Offset: 0x006383B0
		private unsafe static void PlaceClustersArea(DesertDescription description, DesertHive.ClusterGroup clusters, Rectangle area, DesertHive.PostPlacementEffect[,] postEffectMap, Point postEffectMapOffset)
		{
			FastRandom fastRandom = new FastRandom(Main.ActiveWorldFileData.Seed).WithModifier(57005UL);
			Vector2D vector2D;
			vector2D..ctor((double)description.Hive.Width, (double)description.Hive.Height);
			Vector2D vector2D2;
			vector2D2..ctor((double)clusters.Width, (double)clusters.Height);
			Vector2D vector2D3 = description.BlockScale / 2.0;
			for (int i = area.Left; i < area.Right; i++)
			{
				for (int j = area.Top; j < area.Bottom; j++)
				{
					byte liquid = *Main.tile[i, j].liquid;
					if (WorldGen.InWorld(i, j, 1))
					{
						double num = 0.0;
						int num2 = -1;
						double num3 = 0.0;
						ushort type = 53;
						if (fastRandom.Next(3) == 0)
						{
							type = 397;
						}
						int num4 = i - description.Hive.X;
						int num5 = j - description.Hive.Y;
						Vector2D value = (new Vector2D((double)num4, (double)num5) - vector2D3) / vector2D * vector2D2;
						for (int k = 0; k < clusters.Count; k++)
						{
							DesertHive.Cluster cluster = clusters[k];
							if (Math.Abs(cluster[0].Position.X - value.X) <= 10.0 && Math.Abs(cluster[0].Position.Y - value.Y) <= 10.0)
							{
								double num6 = 0.0;
								foreach (DesertHive.Block item in cluster)
								{
									num6 += 1.0 / Vector2D.DistanceSquared(item.Position, value);
								}
								if (num6 > num)
								{
									if (num > num3)
									{
										num3 = num;
									}
									num = num6;
									num2 = k;
								}
								else if (num6 > num3)
								{
									num3 = num6;
								}
							}
						}
						double num7 = num + num3;
						Tile tile = Main.tile[i, j];
						bool flag = ((new Vector2D((double)num4, (double)num5) - vector2D3) / vector2D * 2.0 - Vector2D.One).Length() >= 0.8;
						DesertHive.PostPlacementEffect postPlacementEffect = DesertHive.PostPlacementEffect.None;
						bool flag2 = true;
						if (num7 > 3.5)
						{
							postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
							tile.ClearEverything();
							if (!WorldGen.remixWorldGen || (double)j <= Main.rockLayer + (double)WorldGen.genRand.Next(-1, 2))
							{
								*tile.wall = 187;
								if (num2 % 15 == 2)
								{
									tile.ResetToType(404);
								}
							}
						}
						else if (num7 > 1.8)
						{
							if (!WorldGen.remixWorldGen || (double)j <= Main.rockLayer + (double)WorldGen.genRand.Next(-1, 2))
							{
								*tile.wall = 187;
							}
							if ((double)j < Main.worldSurface)
							{
								*tile.liquid = 0;
							}
							else if (!WorldGen.remixWorldGen)
							{
								tile.lava(true);
							}
							if (!flag || tile.active())
							{
								tile.ResetToType(396);
								postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
							}
						}
						else if (num7 > 0.7 || !flag)
						{
							if (!WorldGen.remixWorldGen || (double)j <= Main.rockLayer + (double)WorldGen.genRand.Next(-1, 2))
							{
								*tile.wall = 216;
								*tile.liquid = 0;
							}
							if (!flag || tile.active())
							{
								tile.ResetToType(type);
								postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
							}
						}
						else if (num7 > 0.25)
						{
							FastRandom fastRandom2 = fastRandom.WithModifier(num4, num5);
							double num8 = (num7 - 0.25) / 0.45;
							if (fastRandom2.NextDouble() < num8)
							{
								if (!WorldGen.remixWorldGen || (double)j <= Main.rockLayer + (double)WorldGen.genRand.Next(-1, 2))
								{
									*tile.wall = 187;
								}
								if ((double)j < Main.worldSurface)
								{
									*tile.liquid = 0;
								}
								else if (!WorldGen.remixWorldGen)
								{
									tile.lava(true);
								}
								if (tile.active())
								{
									tile.ResetToType(type);
									postPlacementEffect = DesertHive.PostPlacementEffect.Smooth;
								}
							}
						}
						else
						{
							flag2 = false;
						}
						if (flag2)
						{
							WorldGen.UpdateDesertHiveBounds(i, j);
						}
						postEffectMap[i - area.X + postEffectMapOffset.X, j - area.Y + postEffectMapOffset.Y] = postPlacementEffect;
						if (WorldGen.remixWorldGen)
						{
							*Main.tile[i, j].liquid = liquid;
						}
					}
				}
			}
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0063A6B8 File Offset: 0x006388B8
		private unsafe static void AddTileVariance(DesertDescription description)
		{
			for (int i = -20; i < description.Hive.Width + 20; i++)
			{
				for (int j = -20; j < description.Hive.Height + 20; j++)
				{
					int num = i + description.Hive.X;
					int num2 = j + description.Hive.Y;
					if (WorldGen.InWorld(num, num2, 1))
					{
						Tile tile = Main.tile[num, num2];
						Tile testTile = Main.tile[num, num2 + 1];
						Tile testTile2 = Main.tile[num, num2 + 2];
						if (*tile.type == 53 && (!WorldGen.SolidTile(testTile) || !WorldGen.SolidTile(testTile2)))
						{
							*tile.type = 397;
						}
					}
				}
			}
			for (int k = -20; k < description.Hive.Width + 20; k++)
			{
				for (int l = -20; l < description.Hive.Height + 20; l++)
				{
					int num3 = k + description.Hive.X;
					int num4 = l + description.Hive.Y;
					if (WorldGen.InWorld(num3, num4, 1))
					{
						Tile tile2 = Main.tile[num3, num4];
						if (tile2.active() && *tile2.type == 396)
						{
							bool flag = true;
							for (int num5 = -1; num5 >= -3; num5--)
							{
								if (Main.tile[num3, num4 + num5].active())
								{
									flag = false;
									break;
								}
							}
							bool flag2 = true;
							for (int m = 1; m <= 3; m++)
							{
								if (Main.tile[num3, num4 + m].active())
								{
									flag2 = false;
									break;
								}
							}
							if (!WorldGen.remixWorldGen || (double)num4 <= Main.rockLayer + (double)WorldGen.genRand.Next(-1, 2))
							{
								if (flag && WorldGen.genRand.Next(20) == 0)
								{
									WorldGen.PlaceTile(num3, num4 - 1, 485, true, true, -1, WorldGen.genRand.Next(4));
								}
								else if (flag && WorldGen.genRand.Next(5) == 0)
								{
									WorldGen.PlaceTile(num3, num4 - 1, 484, true, true, -1, 0);
								}
								else if ((flag ^ flag2) && WorldGen.genRand.Next(5) == 0)
								{
									WorldGen.PlaceTile(num3, num4 + ((!flag) ? 1 : -1), 165, true, true, -1, 0);
								}
								else if (flag && WorldGen.genRand.Next(5) == 0)
								{
									WorldGen.PlaceTile(num3, num4 - 1, 187, true, true, -1, 29 + WorldGen.genRand.Next(6));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x02000D18 RID: 3352
		private struct Block
		{
			// Token: 0x0600630F RID: 25359 RVA: 0x006D796A File Offset: 0x006D5B6A
			public Block(double x, double y)
			{
				this.Position = new Vector2D(x, y);
			}

			// Token: 0x04007ACC RID: 31436
			public Vector2D Position;
		}

		// Token: 0x02000D19 RID: 3353
		private class Cluster : List<DesertHive.Block>
		{
		}

		// Token: 0x02000D1A RID: 3354
		private class ClusterGroup : List<DesertHive.Cluster>
		{
			// Token: 0x06006311 RID: 25361 RVA: 0x006D7981 File Offset: 0x006D5B81
			private ClusterGroup(int width, int height)
			{
				this.Width = width;
				this.Height = height;
				this.Generate();
			}

			// Token: 0x06006312 RID: 25362 RVA: 0x006D799D File Offset: 0x006D5B9D
			public static DesertHive.ClusterGroup FromDescription(DesertDescription description)
			{
				return new DesertHive.ClusterGroup(description.BlockColumnCount, description.BlockRowCount);
			}

			// Token: 0x06006313 RID: 25363 RVA: 0x006D79B0 File Offset: 0x006D5BB0
			private static void SearchForCluster(bool[,] blockMap, List<Point> pointCluster, int x, int y, int level = 2)
			{
				pointCluster.Add(new Point(x, y));
				blockMap[x, y] = false;
				level--;
				if (level != -1)
				{
					if (x > 0 && blockMap[x - 1, y])
					{
						DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x - 1, y, level);
					}
					if (x < blockMap.GetLength(0) - 1 && blockMap[x + 1, y])
					{
						DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x + 1, y, level);
					}
					if (y > 0 && blockMap[x, y - 1])
					{
						DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x, y - 1, level);
					}
					if (y < blockMap.GetLength(1) - 1 && blockMap[x, y + 1])
					{
						DesertHive.ClusterGroup.SearchForCluster(blockMap, pointCluster, x, y + 1, level);
					}
				}
			}

			// Token: 0x06006314 RID: 25364 RVA: 0x006D7A68 File Offset: 0x006D5C68
			private static void AttemptClaim(int x, int y, int[,] clusterIndexMap, List<List<Point>> pointClusters, int index)
			{
				int num = clusterIndexMap[x, y];
				if (num == -1 || num == index)
				{
					return;
				}
				int num2 = (WorldGen.genRand.Next(2) == 0) ? -1 : index;
				foreach (Point item in pointClusters[num])
				{
					clusterIndexMap[item.X, item.Y] = num2;
				}
			}

			// Token: 0x06006315 RID: 25365 RVA: 0x006D7AF0 File Offset: 0x006D5CF0
			private void Generate()
			{
				base.Clear();
				bool[,] array = new bool[this.Width, this.Height];
				int num = this.Width / 2 - 1;
				int num2 = this.Height / 2 - 1;
				int num3 = (num + 1) * (num + 1);
				Point point;
				point..ctor(num, num2);
				for (int i = point.Y - num2; i <= point.Y + num2; i++)
				{
					double num4 = (double)num / (double)num2 * (double)(i - point.Y);
					int num5 = Math.Min(num, (int)Math.Sqrt((double)num3 - num4 * num4));
					for (int j = point.X - num5; j <= point.X + num5; j++)
					{
						array[j, i] = (WorldGen.genRand.Next(2) == 0);
					}
				}
				List<List<Point>> list = new List<List<Point>>();
				for (int k = 0; k < array.GetLength(0); k++)
				{
					for (int l = 0; l < array.GetLength(1); l++)
					{
						if (array[k, l] && WorldGen.genRand.Next(2) == 0)
						{
							List<Point> list2 = new List<Point>();
							DesertHive.ClusterGroup.SearchForCluster(array, list2, k, l, 2);
							if (list2.Count > 2)
							{
								list.Add(list2);
							}
						}
					}
				}
				int[,] array2 = new int[array.GetLength(0), array.GetLength(1)];
				for (int m = 0; m < array2.GetLength(0); m++)
				{
					for (int n = 0; n < array2.GetLength(1); n++)
					{
						array2[m, n] = -1;
					}
				}
				for (int num6 = 0; num6 < list.Count; num6++)
				{
					foreach (Point item in list[num6])
					{
						array2[item.X, item.Y] = num6;
					}
				}
				for (int num7 = 0; num7 < list.Count; num7++)
				{
					foreach (Point point2 in list[num7])
					{
						int x = point2.X;
						int y = point2.Y;
						if (array2[x, y] == -1)
						{
							break;
						}
						int index = array2[x, y];
						if (x > 0)
						{
							DesertHive.ClusterGroup.AttemptClaim(x - 1, y, array2, list, index);
						}
						if (x < array2.GetLength(0) - 1)
						{
							DesertHive.ClusterGroup.AttemptClaim(x + 1, y, array2, list, index);
						}
						if (y > 0)
						{
							DesertHive.ClusterGroup.AttemptClaim(x, y - 1, array2, list, index);
						}
						if (y < array2.GetLength(1) - 1)
						{
							DesertHive.ClusterGroup.AttemptClaim(x, y + 1, array2, list, index);
						}
					}
				}
				foreach (List<Point> list3 in list)
				{
					list3.Clear();
				}
				for (int num8 = 0; num8 < array2.GetLength(0); num8++)
				{
					for (int num9 = 0; num9 < array2.GetLength(1); num9++)
					{
						if (array2[num8, num9] != -1)
						{
							list[array2[num8, num9]].Add(new Point(num8, num9));
						}
					}
				}
				foreach (List<Point> item2 in list)
				{
					if (item2.Count < 4)
					{
						item2.Clear();
					}
				}
				foreach (List<Point> item3 in list)
				{
					DesertHive.Cluster cluster = new DesertHive.Cluster();
					if (item3.Count > 0)
					{
						foreach (Point item4 in item3)
						{
							cluster.Add(new DesertHive.Block((double)item4.X + (WorldGen.genRand.NextDouble() - 0.5) * 0.5, (double)item4.Y + (WorldGen.genRand.NextDouble() - 0.5) * 0.5));
						}
						base.Add(cluster);
					}
				}
			}

			// Token: 0x04007ACD RID: 31437
			public readonly int Width;

			// Token: 0x04007ACE RID: 31438
			public readonly int Height;
		}

		// Token: 0x02000D1B RID: 3355
		[Flags]
		private enum PostPlacementEffect : byte
		{
			// Token: 0x04007AD0 RID: 31440
			None = 0,
			// Token: 0x04007AD1 RID: 31441
			Smooth = 1
		}
	}
}
