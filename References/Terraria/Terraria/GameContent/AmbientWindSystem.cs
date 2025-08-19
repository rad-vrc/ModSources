using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x020001F8 RID: 504
	public class AmbientWindSystem
	{
		// Token: 0x06001D05 RID: 7429 RVA: 0x004FF7DC File Offset: 0x004FD9DC
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

		// Token: 0x06001D06 RID: 7430 RVA: 0x004FF868 File Offset: 0x004FDA68
		private void SpawnAirborneWind()
		{
			foreach (Point point in this._spotsForAirboneWind)
			{
				this.SpawnAirborneCloud(point.X, point.Y);
			}
			this._spotsForAirboneWind.Clear();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x004FF8D4 File Offset: 0x004FDAD4
		private Rectangle GetTileWorkSpace()
		{
			Point point = Main.LocalPlayer.Center.ToTileCoordinates();
			int num = 120;
			int num2 = 30;
			return new Rectangle(point.X - num / 2, point.Y - num2 / 2, num, num2);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x004FF914 File Offset: 0x004FDB14
		private void TrySpawningWind(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 10))
			{
				return;
			}
			if (Main.tile[x, y] == null)
			{
				return;
			}
			this.TestAirCloud(x, y);
			Tile tile = Main.tile[x, y];
			if (!tile.active() || tile.slope() > 0 || tile.halfBrick() || !Main.tileSolid[(int)tile.type])
			{
				return;
			}
			tile = Main.tile[x, y - 1];
			if (WorldGen.SolidTile(tile))
			{
				return;
			}
			if (this._random.Next(120) != 0)
			{
				return;
			}
			this.SpawnFloorCloud(x, y);
			if (this._random.Next(3) == 0)
			{
				this.SpawnFloorCloud(x, y - 1);
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x004FF9C4 File Offset: 0x004FDBC4
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
			Vector2 value = new Vector2(-10f, 0f);
			Vector2 value2 = new Point(x, y).ToWorldCoordinates(8f, 8f);
			num4 -= num5 * (float)num * 0.5f;
			float num6 = num4;
			for (int i = 0; i < num; i++)
			{
				if (Main.rand.Next(10) == 0)
				{
					num5 *= this._random.NextFloatDirection();
				}
				Vector2 value3 = this._random.NextVector2Circular(4f, 4f);
				int type = 1091 + this._random.Next(2) * 2;
				float scaleFactor = 1.4f;
				float num7 = num2 + this._random.NextFloat() * num3;
				float num8 = num6 + num5;
				Vector2 value4 = Vector2.UnitX.RotatedBy((double)num8, default(Vector2)) * scaleFactor;
				Gore.NewGorePerfect(value2 + value3 - value, value4 * Main.WindForVisuals, type, num7);
				value2 += value4 * 6.5f * num7;
				num6 = num8;
			}
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x004FFBB0 File Offset: 0x004FDDB0
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
			float scaleFactor = 0.4f;
			float scale = 0.8f + this._random.NextFloat() * 0.2f;
			Gore.NewGorePerfect(position, Vector2.UnitX * scaleFactor * Main.WindForVisuals, type, scale);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x004FFC58 File Offset: 0x004FDE58
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

		// Token: 0x06001D0C RID: 7436 RVA: 0x004FFCCC File Offset: 0x004FDECC
		private bool DoesTileAllowWind(Tile t)
		{
			return !t.active() || !Main.tileSolid[(int)t.type];
		}

		// Token: 0x040043F1 RID: 17393
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040043F2 RID: 17394
		private List<Point> _spotsForAirboneWind = new List<Point>();

		// Token: 0x040043F3 RID: 17395
		private int _updatesCounter;
	}
}
