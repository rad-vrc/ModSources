using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x0200048A RID: 1162
	public class AmbientWindSystem
	{
		// Token: 0x060038B5 RID: 14517 RVA: 0x00593F48 File Offset: 0x00592148
		public void Update()
		{
			if (!Main.LocalPlayer.ZoneGraveyard)
			{
				return;
			}
			this._updatesCounter++;
			Rectangle tileWorkSpace = this.GetTileWorkSpace();
			int num = tileWorkSpace.X + tileWorkSpace.Width;
			int num2 = tileWorkSpace.Y + tileWorkSpace.Height;
			for (int i = tileWorkSpace.X; i < num; i++)
			{
				for (int j = tileWorkSpace.Y; j < num2; j++)
				{
					this.TrySpawningWind(i, j);
				}
			}
			if (this._updatesCounter % 30 == 0)
			{
				this.SpawnAirborneWind();
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x00593FD4 File Offset: 0x005921D4
		private void SpawnAirborneWind()
		{
			foreach (Point item in this._spotsForAirboneWind)
			{
				this.SpawnAirborneCloud(item.X, item.Y);
			}
			this._spotsForAirboneWind.Clear();
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x00594040 File Offset: 0x00592240
		private Rectangle GetTileWorkSpace()
		{
			Point point = Main.LocalPlayer.Center.ToTileCoordinates();
			int num = 120;
			int num2 = 30;
			return new Rectangle(point.X - num / 2, point.Y - num2 / 2, num, num2);
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00594080 File Offset: 0x00592280
		private unsafe void TrySpawningWind(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 10) || Main.tile[x, y] == null)
			{
				return;
			}
			this.TestAirCloud(x, y);
			Tile tile = Main.tile[x, y];
			if (!tile.active() || tile.slope() > 0 || tile.halfBrick() || !Main.tileSolid[(int)(*tile.type)])
			{
				return;
			}
			tile = Main.tile[x, y - 1];
			if (!WorldGen.SolidTile(tile) && this._random.Next(120) == 0)
			{
				this.SpawnFloorCloud(x, y);
				if (this._random.Next(3) == 0)
				{
					this.SpawnFloorCloud(x, y - 1);
				}
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00594138 File Offset: 0x00592338
		private void SpawnAirborneCloud(int x, int y)
		{
			int num = this._random.Next(2, 6);
			float num2 = 1.1f;
			float num3 = 2.2f;
			float num4 = 0.023561945f * this._random.NextFloatDirection();
			float num5 = 0.023561945f * this._random.NextFloatDirection();
			while (num5 > -0.011780973f && num5 < 0.011780973f)
			{
				num5 = 0.023561945f * this._random.NextFloatDirection();
			}
			if (this._random.Next(4) == 0)
			{
				num = this._random.Next(9, 16);
				num2 = 1.1f;
				num3 = 1.2f;
			}
			else if (this._random.Next(4) == 0)
			{
				num = this._random.Next(9, 16);
				num2 = 1.1f;
				num3 = 0.2f;
			}
			Vector2 vector;
			vector..ctor(-10f, 0f);
			Vector2 vector2 = new Point(x, y).ToWorldCoordinates(8f, 8f);
			num4 -= num5 * (float)num * 0.5f;
			float num6 = num4;
			for (int i = 0; i < num; i++)
			{
				if (Main.rand.Next(10) == 0)
				{
					num5 *= this._random.NextFloatDirection();
				}
				Vector2 vector3 = this._random.NextVector2Circular(4f, 4f);
				int type = 1091 + this._random.Next(2) * 2;
				float num7 = 1.4f;
				float num8 = num2 + this._random.NextFloat() * num3;
				float num9 = num6 + num5;
				Vector2 vector4 = Vector2.UnitX.RotatedBy((double)num9, default(Vector2)) * num7;
				Gore.NewGorePerfect(vector2 + vector3 - vector, vector4 * Main.WindForVisuals, type, num8);
				vector2 += vector4 * 6.5f * num8;
				num6 = num9;
			}
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x00594324 File Offset: 0x00592524
		private void SpawnFloorCloud(int x, int y)
		{
			Vector2 position = new Point(x, y - 1).ToWorldCoordinates(8f, 8f);
			int type = this._random.Next(1087, 1090);
			float num = 16f * this._random.NextFloat();
			position.Y -= num;
			if (num < 4f)
			{
				type = 1090;
			}
			float num2 = 0.4f;
			float scale = 0.8f + this._random.NextFloat() * 0.2f;
			Gore.NewGorePerfect(position, Vector2.UnitX * num2 * Main.WindForVisuals, type, scale);
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x005943CC File Offset: 0x005925CC
		private void TestAirCloud(int x, int y)
		{
			if (this._random.Next(120000) != 0)
			{
				return;
			}
			for (int i = -2; i <= 2; i++)
			{
				if (i != 0)
				{
					Tile t = Main.tile[x + i, y];
					if (!this.DoesTileAllowWind(t))
					{
						return;
					}
					t = Main.tile[x, y + i];
					if (!this.DoesTileAllowWind(t))
					{
						return;
					}
				}
			}
			this._spotsForAirboneWind.Add(new Point(x, y));
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00594440 File Offset: 0x00592640
		private unsafe bool DoesTileAllowWind(Tile t)
		{
			return !t.active() || !Main.tileSolid[(int)(*t.type)];
		}

		// Token: 0x04005201 RID: 20993
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005202 RID: 20994
		private List<Point> _spotsForAirboneWind = new List<Point>();

		// Token: 0x04005203 RID: 20995
		private int _updatesCounter;
	}
}
