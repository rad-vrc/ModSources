using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000658 RID: 1624
	public class HiveBiome : MicroBiome
	{
		// Token: 0x060046E9 RID: 18153 RVA: 0x006357F4 File Offset: 0x006339F4
		public unsafe override bool Place(Point origin, StructureMap structures)
		{
			if (!structures.CanPlace(new Rectangle(origin.X - 50, origin.Y - 50, 100, 100), 0))
			{
				return false;
			}
			if (HiveBiome.TooCloseToImportantLocations(origin))
			{
				return false;
			}
			Ref<int> @ref = new Ref<int>(0);
			Ref<int> ref2 = new Ref<int>(0);
			Ref<int> ref3 = new Ref<int>(0);
			WorldUtils.Gen(origin, new Shapes.Circle(15), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsSolid(),
				new Actions.Scanner(@ref),
				new Modifiers.OnlyTiles(new ushort[]
				{
					60,
					59
				}),
				new Actions.Scanner(ref2),
				new Modifiers.OnlyTiles(new ushort[]
				{
					60
				}),
				new Actions.Scanner(ref3)
			}));
			if ((double)ref2.Value / (double)@ref.Value < 0.75 || ref3.Value < 2)
			{
				return false;
			}
			int num = 0;
			int[] array = new int[1000];
			int[] array2 = new int[1000];
			Vector2D vector2D = origin.ToVector2D();
			int num2 = WorldGen.genRand.Next(2, 5);
			if (WorldGen.drunkWorldGen)
			{
				num2 += WorldGen.genRand.Next(7, 10);
			}
			else if (WorldGen.remixWorldGen)
			{
				num2 += WorldGen.genRand.Next(2, 5);
			}
			for (int i = 0; i < num2; i++)
			{
				Vector2D vector2D2 = vector2D;
				int num3 = WorldGen.genRand.Next(2, 5);
				for (int j = 0; j < num3; j++)
				{
					vector2D2 = HiveBiome.CreateHiveTunnel((int)vector2D.X, (int)vector2D.Y, WorldGen.genRand);
				}
				vector2D = vector2D2;
				array[num] = (int)vector2D.X;
				array2[num] = (int)vector2D.Y;
				num++;
			}
			HiveBiome.FrameOutAllHiveContents(origin, 50);
			for (int k = 0; k < num; k++)
			{
				int num4 = array[k];
				int y = array2[k];
				int num5 = 1;
				if (WorldGen.genRand.Next(2) == 0)
				{
					num5 = -1;
				}
				bool flag = false;
				while (WorldGen.InWorld(num4, y, 10) && HiveBiome.BadSpotForHoneyFall(num4, y))
				{
					num4 += num5;
					if (Math.Abs(num4 - array[k]) > 50)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num4 += num5;
					if (!HiveBiome.SpotActuallyNotInHive(num4, y))
					{
						HiveBiome.CreateBlockedHoneyCube(num4, y);
						HiveBiome.CreateDentForHoneyFall(num4, y, num5);
					}
				}
			}
			HiveBiome.CreateStandForLarva(vector2D);
			if (WorldGen.drunkWorldGen)
			{
				for (int l = 0; l < 1000; l++)
				{
					Vector2D vector2D3 = vector2D;
					vector2D3.X += (double)WorldGen.genRand.Next(-50, 51);
					vector2D3.Y += (double)WorldGen.genRand.Next(-50, 51);
					if (WorldGen.InWorld((int)vector2D3.X, (int)vector2D3.Y, 0) && Vector2D.Distance(vector2D, vector2D3) > 10.0 && !Main.tile[(int)vector2D3.X, (int)vector2D3.Y].active() && *Main.tile[(int)vector2D3.X, (int)vector2D3.Y].wall == 86)
					{
						HiveBiome.CreateStandForLarva(vector2D3);
						break;
					}
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X - 50, origin.Y - 50, 100, 100), 5);
			return true;
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00635B50 File Offset: 0x00633D50
		private unsafe static void FrameOutAllHiveContents(Point origin, int squareHalfWidth)
		{
			int num5 = Math.Max(10, origin.X - squareHalfWidth);
			int num2 = Math.Min(Main.maxTilesX - 10, origin.X + squareHalfWidth);
			int num3 = Math.Max(10, origin.Y - squareHalfWidth);
			int num4 = Math.Min(Main.maxTilesY - 10, origin.Y + squareHalfWidth);
			for (int i = num5; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && *tile.type == 225)
					{
						WorldGen.SquareTileFrame(i, j, true);
					}
					if (*tile.wall == 86)
					{
						WorldGen.SquareWallFrame(i, j, true);
					}
				}
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x00635C08 File Offset: 0x00633E08
		private unsafe static Vector2D CreateHiveTunnel(int i, int j, UnifiedRandom random)
		{
			double num = (double)random.Next(12, 21);
			double num2 = (double)random.Next(10, 21);
			if (WorldGen.drunkWorldGen)
			{
				num = (double)random.Next(8, 26);
				num2 = (double)random.Next(10, 41);
				double num3 = (double)Main.maxTilesX / 4200.0;
				num3 = (num3 + 1.0) / 2.0;
				num *= num3;
				num2 *= num3;
			}
			else if (WorldGen.remixWorldGen)
			{
				num += (double)random.Next(3);
			}
			double num4 = num;
			Vector2D result = default(Vector2D);
			result.X = (double)i;
			result.Y = (double)j;
			Vector2D vector2D = default(Vector2D);
			vector2D.X = (double)random.Next(-10, 11) * 0.2;
			vector2D.Y = (double)random.Next(-10, 11) * 0.2;
			while (num > 0.0 && num2 > 0.0)
			{
				if (result.Y > (double)(Main.maxTilesY - 250))
				{
					num2 = 0.0;
				}
				num = num4 * (1.0 + (double)random.Next(-20, 20) * 0.01);
				num2 -= 1.0;
				int num5 = (int)(result.X - num);
				int num6 = (int)(result.X + num);
				int num7 = (int)(result.Y - num);
				int num8 = (int)(result.Y + num);
				if (num5 < 1)
				{
					num5 = 1;
				}
				if (num6 > Main.maxTilesX - 1)
				{
					num6 = Main.maxTilesX - 1;
				}
				if (num7 < 1)
				{
					num7 = 1;
				}
				if (num8 > Main.maxTilesY - 1)
				{
					num8 = Main.maxTilesY - 1;
				}
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if (!WorldGen.InWorld(k, l, 50))
						{
							num2 = 0.0;
						}
						else
						{
							if (*Main.tile[k - 10, l].wall == 87)
							{
								num2 = 0.0;
							}
							if (*Main.tile[k + 10, l].wall == 87)
							{
								num2 = 0.0;
							}
							if (*Main.tile[k, l - 10].wall == 87)
							{
								num2 = 0.0;
							}
							if (*Main.tile[k, l + 10].wall == 87)
							{
								num2 = 0.0;
							}
						}
						if ((double)l < Main.worldSurface && *Main.tile[k, l - 5].wall == 0)
						{
							num2 = 0.0;
						}
						double num11 = Math.Abs((double)k - result.X);
						double num9 = Math.Abs((double)l - result.Y);
						double num10 = Math.Sqrt(num11 * num11 + num9 * num9);
						if (num10 < num4 * 0.4 * (1.0 + (double)random.Next(-10, 11) * 0.005))
						{
							if (random.Next(3) == 0)
							{
								*Main.tile[k, l].liquid = byte.MaxValue;
							}
							if (WorldGen.drunkWorldGen)
							{
								*Main.tile[k, l].liquid = byte.MaxValue;
							}
							Main.tile[k, l].honey(true);
							*Main.tile[k, l].wall = 86;
							Main.tile[k, l].active(false);
							Main.tile[k, l].halfBrick(false);
							Main.tile[k, l].slope(0);
						}
						else if (num10 < num4 * 0.75 * (1.0 + (double)random.Next(-10, 11) * 0.005))
						{
							*Main.tile[k, l].liquid = 0;
							if (*Main.tile[k, l].wall != 86)
							{
								Main.tile[k, l].active(true);
								Main.tile[k, l].halfBrick(false);
								Main.tile[k, l].slope(0);
								*Main.tile[k, l].type = 225;
							}
						}
						if (num10 < num4 * 0.6 * (1.0 + (double)random.Next(-10, 11) * 0.005))
						{
							*Main.tile[k, l].wall = 86;
							if (WorldGen.drunkWorldGen && random.Next(2) == 0)
							{
								*Main.tile[k, l].liquid = byte.MaxValue;
								Main.tile[k, l].honey(true);
							}
						}
					}
				}
				result += vector2D;
				num2 -= 1.0;
				vector2D.Y += (double)random.Next(-10, 11) * 0.05;
				vector2D.X += (double)random.Next(-10, 11) * 0.05;
			}
			return result;
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x006361BC File Offset: 0x006343BC
		private unsafe static bool TooCloseToImportantLocations(Point origin)
		{
			int x = origin.X;
			int y = origin.Y;
			int num = 150;
			for (int i = x - num; i < x + num; i += 10)
			{
				if (i > 0 && i <= Main.maxTilesX - 1)
				{
					for (int j = y - num; j < y + num; j += 10)
					{
						if (j > 0 && j <= Main.maxTilesY - 1)
						{
							if (Main.tile[i, j].active() && *Main.tile[i, j].type == 226)
							{
								return true;
							}
							if (*Main.tile[i, j].wall == 83 || *Main.tile[i, j].wall == 3 || *Main.tile[i, j].wall == 87)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x006362C4 File Offset: 0x006344C4
		private unsafe static void CreateDentForHoneyFall(int x, int y, int dir)
		{
			dir *= -1;
			y++;
			int num = 0;
			while ((num < 4 || WorldGen.SolidTile(x, y, false)) && x > 10 && x < Main.maxTilesX - 10)
			{
				num++;
				x += dir;
				if (WorldGen.SolidTile(x, y, false))
				{
					WorldGen.PoundTile(x, y);
					if (!Main.tile[x, y + 1].active())
					{
						Main.tile[x, y + 1].active(true);
						*Main.tile[x, y + 1].type = 225;
					}
				}
			}
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00636368 File Offset: 0x00634568
		private unsafe static void CreateBlockedHoneyCube(int x, int y)
		{
			for (int i = x - 1; i <= x + 2; i++)
			{
				for (int j = y - 1; j <= y + 2; j++)
				{
					if (i >= x && i <= x + 1 && j >= y && j <= y + 1)
					{
						Main.tile[i, j].active(false);
						*Main.tile[i, j].liquid = byte.MaxValue;
						Main.tile[i, j].honey(true);
					}
					else
					{
						Main.tile[i, j].active(true);
						*Main.tile[i, j].type = 225;
					}
				}
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0063642C File Offset: 0x0063462C
		private unsafe static bool SpotActuallyNotInHive(int x, int y)
		{
			for (int i = x - 1; i <= x + 2; i++)
			{
				for (int j = y - 1; j <= y + 2; j++)
				{
					if (i < 10 || i > Main.maxTilesX - 10)
					{
						return true;
					}
					if (Main.tile[i, j].active() && *Main.tile[i, j].type != 225)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x006364A0 File Offset: 0x006346A0
		private static bool BadSpotForHoneyFall(int x, int y)
		{
			return !Main.tile[x, y].active() || !Main.tile[x, y + 1].active() || !Main.tile[x + 1, y].active() || !Main.tile[x + 1, y + 1].active();
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00636510 File Offset: 0x00634710
		public unsafe static void CreateStandForLarva(Vector2D position)
		{
			GenVars.larvaX[GenVars.numLarva] = Utils.Clamp<int>((int)position.X, 5, Main.maxTilesX - 5);
			GenVars.larvaY[GenVars.numLarva] = Utils.Clamp<int>((int)position.Y, 5, Main.maxTilesY - 5);
			GenVars.numLarva++;
			if (GenVars.numLarva >= GenVars.larvaX.Length)
			{
				GenVars.numLarva = GenVars.larvaX.Length - 1;
			}
			int num = (int)position.X;
			int num2 = (int)position.Y;
			int i = num - 1;
			while (i <= num + 1 && i > 0 && i < Main.maxTilesX)
			{
				int j = num2 - 2;
				while (j <= num2 + 1 && j > 0 && j < Main.maxTilesY)
				{
					if (j != num2 + 1)
					{
						Main.tile[i, j].active(false);
					}
					else
					{
						Main.tile[i, j].active(true);
						*Main.tile[i, j].type = 225;
						Main.tile[i, j].slope(0);
						Main.tile[i, j].halfBrick(false);
					}
					j++;
				}
				i++;
			}
		}
	}
}
