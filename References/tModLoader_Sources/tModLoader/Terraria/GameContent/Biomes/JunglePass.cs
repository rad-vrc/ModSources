using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200065A RID: 1626
	public class JunglePass : GenPass
	{
		// Token: 0x060046F5 RID: 18165 RVA: 0x0063684E File Offset: 0x00634A4E
		public JunglePass() : base("Jungle", 10154.65234375)
		{
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x00636864 File Offset: 0x00634A64
		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			progress.Message = Lang.gen[11].Value;
			this._worldScale = (double)Main.maxTilesX / 4200.0 * 1.5;
			double worldScale = this._worldScale;
			Point point = this.CreateStartPoint();
			int x = point.X;
			int y = point.Y;
			Point zero = Point.Zero;
			this.ApplyRandomMovement(ref x, ref y, 100, 100);
			zero.X += x;
			zero.Y += y;
			this.PlaceFirstPassMud(x, y, 3);
			this.PlaceGemsAt(x, y, 63, 2);
			progress.Set(0.15);
			this.ApplyRandomMovement(ref x, ref y, 250, 150);
			zero.X += x;
			zero.Y += y;
			this.PlaceFirstPassMud(x, y, 0);
			this.PlaceGemsAt(x, y, 65, 2);
			progress.Set(0.3);
			int oldX = x;
			int oldY = y;
			this.ApplyRandomMovement(ref x, ref y, 400, 150);
			zero.X += x;
			zero.Y += y;
			this.PlaceFirstPassMud(x, y, -3);
			this.PlaceGemsAt(x, y, 67, 2);
			progress.Set(0.45);
			x = zero.X / 3;
			y = zero.Y / 3;
			int num = GenBase._random.Next((int)(400.0 * worldScale), (int)(600.0 * worldScale));
			int num2 = (int)(25.0 * worldScale);
			x = Utils.Clamp<int>(x, GenVars.leftBeachEnd + num / 2 + num2, GenVars.rightBeachStart - num / 2 - num2);
			GenVars.mudWall = true;
			WorldGen.TileRunner(x, y, (double)num, 10000, 59, false, 0.0, -20.0, true, true, -1);
			this.GenerateTunnelToSurface(x, y);
			GenVars.mudWall = false;
			progress.Set(0.6);
			this.GenerateHolesInMudWalls();
			this.GenerateFinishingTouches(progress, oldX, oldY);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00636A74 File Offset: 0x00634C74
		private void PlaceGemsAt(int x, int y, ushort baseGem, int gemVariants)
		{
			int i = 0;
			while ((double)i < 6.0 * this._worldScale)
			{
				WorldGen.TileRunner(x + GenBase._random.Next(-(int)(125.0 * this._worldScale), (int)(125.0 * this._worldScale)), y + GenBase._random.Next(-(int)(125.0 * this._worldScale), (int)(125.0 * this._worldScale)), (double)GenBase._random.Next(3, 7), GenBase._random.Next(3, 8), GenBase._random.Next((int)baseGem, (int)baseGem + gemVariants), false, 0.0, 0.0, false, true, -1);
				i++;
			}
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x00636B44 File Offset: 0x00634D44
		private void PlaceFirstPassMud(int x, int y, int xSpeedScale)
		{
			GenVars.mudWall = true;
			WorldGen.TileRunner(x, y, (double)GenBase._random.Next((int)(250.0 * this._worldScale), (int)(500.0 * this._worldScale)), GenBase._random.Next(50, 150), 59, false, (double)(GenVars.dungeonSide * xSpeedScale), 0.0, false, true, -1);
			GenVars.mudWall = false;
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x00636BB9 File Offset: 0x00634DB9
		private Point CreateStartPoint()
		{
			return new Point(GenVars.jungleOriginX, (int)((double)Main.maxTilesY + Main.rockLayer) / 2);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x00636BD4 File Offset: 0x00634DD4
		private void ApplyRandomMovement(ref int x, ref int y, int xRange, int yRange)
		{
			x += GenBase._random.Next((int)((double)(-(double)xRange) * this._worldScale), 1 + (int)((double)xRange * this._worldScale));
			y += GenBase._random.Next((int)((double)(-(double)yRange) * this._worldScale), 1 + (int)((double)yRange * this._worldScale));
			y = Utils.Clamp<int>(y, (int)Main.rockLayer, Main.maxTilesY);
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x00636C44 File Offset: 0x00634E44
		private unsafe void GenerateTunnelToSurface(int i, int j)
		{
			double num = (double)GenBase._random.Next(5, 11);
			Vector2D vector2D = default(Vector2D);
			vector2D.X = (double)i;
			vector2D.Y = (double)j;
			Vector2D vector2D2 = default(Vector2D);
			vector2D2.X = (double)GenBase._random.Next(-10, 11) * 0.1;
			vector2D2.Y = (double)GenBase._random.Next(10, 20) * 0.1;
			int num2 = 0;
			bool flag = true;
			while (flag)
			{
				if (vector2D.Y < Main.worldSurface)
				{
					if (WorldGen.drunkWorldGen)
					{
						flag = false;
					}
					int value = (int)vector2D.X;
					int value2 = (int)vector2D.Y;
					value = Utils.Clamp<int>(value, 10, Main.maxTilesX - 10);
					value2 = Utils.Clamp<int>(value2, 10, Main.maxTilesY - 10);
					if (value2 < 5)
					{
						value2 = 5;
					}
					if (*Main.tile[value, value2].wall == 0 && !Main.tile[value, value2].active() && *Main.tile[value, value2 - 3].wall == 0 && !Main.tile[value, value2 - 3].active() && *Main.tile[value, value2 - 1].wall == 0 && !Main.tile[value, value2 - 1].active() && *Main.tile[value, value2 - 4].wall == 0 && !Main.tile[value, value2 - 4].active() && *Main.tile[value, value2 - 2].wall == 0 && !Main.tile[value, value2 - 2].active() && *Main.tile[value, value2 - 5].wall == 0 && !Main.tile[value, value2 - 5].active())
					{
						flag = false;
					}
				}
				GenVars.JungleX = (int)vector2D.X;
				num += (double)GenBase._random.Next(-20, 21) * 0.1;
				if (num < 5.0)
				{
					num = 5.0;
				}
				if (num > 10.0)
				{
					num = 10.0;
				}
				int value6 = (int)(vector2D.X - num * 0.5);
				int value3 = (int)(vector2D.X + num * 0.5);
				int value4 = (int)(vector2D.Y - num * 0.5);
				int value5 = (int)(vector2D.Y + num * 0.5);
				int num4 = Utils.Clamp<int>(value6, 10, Main.maxTilesX - 10);
				value3 = Utils.Clamp<int>(value3, 10, Main.maxTilesX - 10);
				value4 = Utils.Clamp<int>(value4, 10, Main.maxTilesY - 10);
				value5 = Utils.Clamp<int>(value5, 10, Main.maxTilesY - 10);
				for (int k = num4; k < value3; k++)
				{
					for (int l = value4; l < value5; l++)
					{
						if (Math.Abs((double)k - vector2D.X) + Math.Abs((double)l - vector2D.Y) < num * 0.5 * (1.0 + (double)GenBase._random.Next(-10, 11) * 0.015))
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				num2++;
				if (num2 > 10 && GenBase._random.Next(50) < num2)
				{
					num2 = 0;
					int num3 = -2;
					if (GenBase._random.Next(2) == 0)
					{
						num3 = 2;
					}
					WorldGen.TileRunner((int)vector2D.X, (int)vector2D.Y, (double)GenBase._random.Next(3, 20), GenBase._random.Next(10, 100), -1, false, (double)num3, 0.0, false, true, -1);
				}
				vector2D += vector2D2;
				vector2D2.Y += (double)GenBase._random.Next(-10, 11) * 0.01;
				if (vector2D2.Y > 0.0)
				{
					vector2D2.Y = 0.0;
				}
				if (vector2D2.Y < -2.0)
				{
					vector2D2.Y = -2.0;
				}
				vector2D2.X += (double)GenBase._random.Next(-10, 11) * 0.1;
				if (vector2D.X < (double)(i - 200))
				{
					vector2D2.X += (double)GenBase._random.Next(5, 21) * 0.1;
				}
				if (vector2D.X > (double)(i + 200))
				{
					vector2D2.X -= (double)GenBase._random.Next(5, 21) * 0.1;
				}
				if (vector2D2.X > 1.5)
				{
					vector2D2.X = 1.5;
				}
				if (vector2D2.X < -1.5)
				{
					vector2D2.X = -1.5;
				}
			}
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x006371A0 File Offset: 0x006353A0
		private unsafe void GenerateHolesInMudWalls()
		{
			for (int i = 0; i < Main.maxTilesX / 4; i++)
			{
				int num = GenBase._random.Next(20, Main.maxTilesX - 20);
				int num2 = GenBase._random.Next((int)GenVars.worldSurface + 10, Main.UnderworldLayer);
				while (*Main.tile[num, num2].wall != 64 && *Main.tile[num, num2].wall != 15)
				{
					num = GenBase._random.Next(20, Main.maxTilesX - 20);
					num2 = GenBase._random.Next((int)GenVars.worldSurface + 10, Main.UnderworldLayer);
				}
				WorldGen.MudWallRunner(num, num2);
			}
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x0063725C File Offset: 0x0063545C
		private unsafe void GenerateFinishingTouches(GenerationProgress progress, int oldX, int oldY)
		{
			int num = oldX;
			int num2 = oldY;
			double worldScale = this._worldScale;
			int i = 0;
			while ((double)i <= 20.0 * worldScale)
			{
				progress.Set((60.0 + (double)i / worldScale) * 0.01);
				num += GenBase._random.Next((int)(-5.0 * worldScale), (int)(6.0 * worldScale));
				num2 += GenBase._random.Next((int)(-5.0 * worldScale), (int)(6.0 * worldScale));
				WorldGen.TileRunner(num, num2, (double)GenBase._random.Next(40, 100), GenBase._random.Next(300, 500), 59, false, 0.0, 0.0, false, true, -1);
				i++;
			}
			int j = 0;
			while ((double)j <= 10.0 * worldScale)
			{
				progress.Set((80.0 + (double)j / worldScale * 2.0) * 0.01);
				num = oldX + GenBase._random.Next((int)(-600.0 * worldScale), (int)(600.0 * worldScale));
				num2 = oldY + GenBase._random.Next((int)(-200.0 * worldScale), (int)(200.0 * worldScale));
				while (num < 1 || num >= Main.maxTilesX - 1 || num2 < 1 || num2 >= Main.maxTilesY - 1 || *Main.tile[num, num2].type != 59)
				{
					num = oldX + GenBase._random.Next((int)(-600.0 * worldScale), (int)(600.0 * worldScale));
					num2 = oldY + GenBase._random.Next((int)(-200.0 * worldScale), (int)(200.0 * worldScale));
				}
				int k = 0;
				while ((double)k < 8.0 * worldScale)
				{
					num += GenBase._random.Next(-30, 31);
					num2 += GenBase._random.Next(-30, 31);
					int type = -1;
					if (GenBase._random.Next(7) == 0)
					{
						type = -2;
					}
					WorldGen.TileRunner(num, num2, (double)GenBase._random.Next(10, 20), GenBase._random.Next(30, 70), type, false, 0.0, 0.0, false, true, -1);
					k++;
				}
				j++;
			}
			int l = 0;
			while ((double)l <= 300.0 * worldScale)
			{
				num = oldX + GenBase._random.Next((int)(-600.0 * worldScale), (int)(600.0 * worldScale));
				num2 = oldY + GenBase._random.Next((int)(-200.0 * worldScale), (int)(200.0 * worldScale));
				while (num < 1 || num >= Main.maxTilesX - 1 || num2 < 1 || num2 >= Main.maxTilesY - 1 || *Main.tile[num, num2].type != 59)
				{
					num = oldX + GenBase._random.Next((int)(-600.0 * worldScale), (int)(600.0 * worldScale));
					num2 = oldY + GenBase._random.Next((int)(-200.0 * worldScale), (int)(200.0 * worldScale));
				}
				WorldGen.TileRunner(num, num2, (double)GenBase._random.Next(4, 10), GenBase._random.Next(5, 30), 1, false, 0.0, 0.0, false, true, -1);
				if (GenBase._random.Next(4) == 0)
				{
					int type2 = GenBase._random.Next(63, 69);
					WorldGen.TileRunner(num + GenBase._random.Next(-1, 2), num2 + GenBase._random.Next(-1, 2), (double)GenBase._random.Next(3, 7), GenBase._random.Next(4, 8), type2, false, 0.0, 0.0, false, true, -1);
				}
				l++;
			}
		}

		// Token: 0x04005BA9 RID: 23465
		private double _worldScale;
	}
}
