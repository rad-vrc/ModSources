using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000653 RID: 1619
	public class DeadMansChestBiome : MicroBiome
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x006330C8 File Offset: 0x006312C8
		public override bool Place(Point origin, StructureMap structures)
		{
			if (!DeadMansChestBiome.IsAGoodSpot(origin))
			{
				return false;
			}
			this.ClearCaches();
			Point position;
			position..ctor(origin.X, origin.Y + 1);
			this.FindBoulderTrapSpots(position);
			this.FindDartTrapSpots(position);
			this.FindExplosiveTrapSpots(position);
			if (!this.AreThereEnoughTraps())
			{
				return false;
			}
			this.TurnGoldChestIntoDeadMansChest(origin);
			foreach (DeadMansChestBiome.DartTrapPlacementAttempt dartTrapPlacementSpot in this._dartTrapPlacementSpots)
			{
				this.ActuallyPlaceDartTrap(dartTrapPlacementSpot.position, dartTrapPlacementSpot.directionX, dartTrapPlacementSpot.x, dartTrapPlacementSpot.y, dartTrapPlacementSpot.xPush, dartTrapPlacementSpot.t);
			}
			foreach (DeadMansChestBiome.WirePlacementAttempt wirePlacementSpot in this._wirePlacementSpots)
			{
				this.PlaceWireLine(wirePlacementSpot.position, wirePlacementSpot.dirX, wirePlacementSpot.dirY, wirePlacementSpot.steps);
			}
			foreach (DeadMansChestBiome.BoulderPlacementAttempt boulderPlacementSpot in this._boulderPlacementSpots)
			{
				this.ActuallyPlaceBoulderTrap(boulderPlacementSpot.position, boulderPlacementSpot.yPush, boulderPlacementSpot.requiredHeight, boulderPlacementSpot.bestType);
			}
			foreach (DeadMansChestBiome.ExplosivePlacementAttempt item in this._explosivePlacementAttempt)
			{
				this.ActuallyPlaceExplosive(item.position);
			}
			this.PlaceWiresForExplosives(origin);
			return true;
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00633298 File Offset: 0x00631498
		private void PlaceWiresForExplosives(Point origin)
		{
			if (this._explosivePlacementAttempt.Count <= 0)
			{
				return;
			}
			this.PlaceWireLine(origin, 0, 1, this._explosivePlacementAttempt[0].position.Y - origin.Y);
			int num = this._explosivePlacementAttempt[0].position.X;
			int num2 = this._explosivePlacementAttempt[0].position.X;
			int y = this._explosivePlacementAttempt[0].position.Y;
			for (int i = 1; i < this._explosivePlacementAttempt.Count; i++)
			{
				int x = this._explosivePlacementAttempt[i].position.X;
				if (num > x)
				{
					num = x;
				}
				if (num2 < x)
				{
					num2 = x;
				}
			}
			this.PlaceWireLine(new Point(num, y), 1, 0, num2 - num);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x0063336F File Offset: 0x0063156F
		private bool AreThereEnoughTraps()
		{
			return (this._boulderPlacementSpots.Count >= 1 || this._explosivePlacementAttempt.Count >= 1) && this._dartTrapPlacementSpots.Count >= 1;
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x006333A0 File Offset: 0x006315A0
		private void ClearCaches()
		{
			this._dartTrapPlacementSpots.Clear();
			this._wirePlacementSpots.Clear();
			this._boulderPlacementSpots.Clear();
			this._explosivePlacementAttempt.Clear();
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x006333D0 File Offset: 0x006315D0
		private void FindBoulderTrapSpots(Point position)
		{
			int x = position.X;
			int num = GenBase._random.Next(this._numberOfBoulderTraps);
			int num2 = GenBase._random.Next(this._numberOfStepsBetweenBoulderTraps);
			x -= num / 2 * num2;
			int num3 = position.Y - 6;
			for (int i = 0; i <= num; i++)
			{
				this.FindBoulderTrapSpot(new Point(x, num3));
				x += num2;
			}
			if (this._boulderPlacementSpots.Count <= 0)
			{
				return;
			}
			int num4 = this._boulderPlacementSpots[0].position.X;
			int num5 = this._boulderPlacementSpots[0].position.X;
			for (int j = 1; j < this._boulderPlacementSpots.Count; j++)
			{
				int x2 = this._boulderPlacementSpots[j].position.X;
				if (num4 > x2)
				{
					num4 = x2;
				}
				if (num5 < x2)
				{
					num5 = x2;
				}
			}
			if (num4 > position.X)
			{
				num4 = position.X;
			}
			if (num5 < position.X)
			{
				num5 = position.X;
			}
			this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(new Point(num4, num3 - 1), 1, 0, num5 - num4));
			this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(position, 0, -1, 7));
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x00633518 File Offset: 0x00631718
		private void FindBoulderTrapSpot(Point position)
		{
			int x = position.X;
			int y = position.Y;
			for (int i = 0; i < 50; i++)
			{
				if (Main.tile[x, y - i].active())
				{
					this.PlaceBoulderTrapSpot(new Point(x, y - i), i);
					return;
				}
			}
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x0063356C File Offset: 0x0063176C
		private unsafe void PlaceBoulderTrapSpot(Point position, int yPush)
		{
			int[] array = new int[TileLoader.TileCount];
			for (int i = position.X; i < position.X + 2; i++)
			{
				for (int j = position.Y - 4; j <= position.Y; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && !Main.tileFrameImportant[(int)(*tile.type)] && Main.tileSolid[(int)(*tile.type)])
					{
						array[(int)(*tile.type)]++;
					}
					if ((tile.active() && !TileID.Sets.CanBeClearedDuringGeneration[(int)(*tile.type)]) || (tile.active() && TileID.Sets.IsAContainer[(int)(*tile.type)]))
					{
						return;
					}
				}
			}
			for (int k = position.X - 1; k < position.X + 2 + 1; k++)
			{
				for (int l = position.Y - 4 - 1; l <= position.Y - 4 + 2; l++)
				{
					Tile tile2 = Main.tile[k, l];
					if (!tile2.active() || TileID.Sets.IsAContainer[(int)(*tile2.type)])
					{
						return;
					}
				}
			}
			int num = 2;
			int num7 = position.X - num;
			int num2 = position.Y - 4 - num;
			int num3 = position.X + num + 1;
			int num4 = position.Y - 4 + num + 1;
			for (int m = num7; m <= num3; m++)
			{
				for (int n = num2; n <= num4; n++)
				{
					Tile tile3 = Main.tile[m, n];
					if (tile3.active() && TileID.Sets.IsAContainer[(int)(*tile3.type)])
					{
						return;
					}
				}
			}
			int num5 = -1;
			for (int num6 = 0; num6 < array.Length; num6++)
			{
				if (num5 == -1 || array[num5] < array[num6])
				{
					num5 = num6;
				}
			}
			this._boulderPlacementSpots.Add(new DeadMansChestBiome.BoulderPlacementAttempt(position, yPush - 1, 4, num5));
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x0063376C File Offset: 0x0063196C
		private void FindDartTrapSpots(Point position)
		{
			int num = GenBase._random.Next(this._numberOfDartTraps);
			int num2 = (GenBase._random.Next(2) != 0) ? 1 : -1;
			int steps = -1;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.FindDartTrapSpotSingle(position, num2);
				num2 *= -1;
				position.Y--;
				if (flag)
				{
					steps = i;
				}
			}
			this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(new Point(position.X, position.Y + num), 0, -1, steps));
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x006337EC File Offset: 0x006319EC
		private unsafe bool FindDartTrapSpotSingle(Point position, int directionX)
		{
			int x = position.X;
			int y = position.Y;
			int i = 0;
			while (i < 20)
			{
				Tile tile = Main.tile[x + i * directionX, y];
				if ((!tile.active() || *tile.type < 0 || !TileID.Sets.IsAContainer[(int)(*tile.type)]) && tile.active() && Main.tileSolid[(int)(*tile.type)])
				{
					if (i >= 5 && !tile.actuator() && !Main.tileFrameImportant[(int)(*tile.type)] && TileID.Sets.CanBeClearedDuringGeneration[(int)(*tile.type)])
					{
						this._dartTrapPlacementSpots.Add(new DeadMansChestBiome.DartTrapPlacementAttempt(position, directionX, x, y, i, tile));
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x006338B0 File Offset: 0x00631AB0
		private void FindExplosiveTrapSpots(Point position)
		{
			int x = position.X;
			int y = position.Y + 3;
			List<int> list = new List<int>();
			if (this.IsGoodSpotsForExplosive(x, y))
			{
				list.Add(x);
			}
			x++;
			if (this.IsGoodSpotsForExplosive(x, y))
			{
				list.Add(x);
			}
			int num = -1;
			if (list.Count > 0)
			{
				num = list[GenBase._random.Next(list.Count)];
			}
			list.Clear();
			x += GenBase._random.Next(2, 6);
			int num2 = 4;
			for (int i = x; i < x + num2; i++)
			{
				if (this.IsGoodSpotsForExplosive(i, y))
				{
					list.Add(i);
				}
			}
			int num3 = -1;
			if (list.Count > 0)
			{
				num3 = list[GenBase._random.Next(list.Count)];
			}
			x = position.X - num2 - GenBase._random.Next(2, 6);
			for (int j = x; j < x + num2; j++)
			{
				if (this.IsGoodSpotsForExplosive(j, y))
				{
					list.Add(j);
				}
			}
			int num4 = -1;
			if (list.Count > 0)
			{
				num4 = list[GenBase._random.Next(list.Count)];
			}
			if (num4 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num4, y)));
			}
			if (num != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num, y)));
			}
			if (num3 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num3, y)));
			}
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x00633A34 File Offset: 0x00631C34
		private unsafe bool IsGoodSpotsForExplosive(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return (!tile.active() || *tile.type < 0 || !TileID.Sets.IsAContainer[(int)(*tile.type)]) && (tile.active() && Main.tileSolid[(int)(*tile.type)] && !Main.tileFrameImportant[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)]);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x00633AB0 File Offset: 0x00631CB0
		public List<int> GetPossibleChestsToTrapify(StructureMap structures)
		{
			List<int> list = new List<int>();
			bool[] array = new bool[TileID.Sets.GeneralPlacementTiles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TileID.Sets.GeneralPlacementTiles[i];
			}
			array[21] = true;
			array[467] = true;
			for (int j = 0; j < 8000; j++)
			{
				Chest chest = Main.chest[j];
				if (chest != null)
				{
					Point position;
					position..ctor(chest.x, chest.y);
					if (DeadMansChestBiome.IsAGoodSpot(position))
					{
						this.ClearCaches();
						Point position2;
						position2..ctor(position.X, position.Y + 1);
						this.FindBoulderTrapSpots(position2);
						this.FindDartTrapSpots(position2);
						if (this.AreThereEnoughTraps() && (structures == null || structures.CanPlace(new Rectangle(position.X, position.Y, 1, 1), array, 10)))
						{
							list.Add(j);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00633B98 File Offset: 0x00631D98
		private unsafe static bool IsAGoodSpot(Point position)
		{
			if (!WorldGen.InWorld(position.X, position.Y, 50))
			{
				return false;
			}
			if (WorldGen.oceanDepths(position.X, position.Y))
			{
				return false;
			}
			Tile tile = Main.tile[position.X, position.Y];
			if (*tile.type != 21)
			{
				return false;
			}
			if (*tile.frameX / 36 != 1)
			{
				return false;
			}
			tile = Main.tile[position.X, position.Y + 2];
			return TileID.Sets.CanBeClearedDuringGeneration[(int)(*tile.type)] && WorldGen.countWires(position.X, position.Y, 20) <= 0 && WorldGen.countTiles(position.X, position.Y, false, true) >= 40;
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00633C64 File Offset: 0x00631E64
		private unsafe void TurnGoldChestIntoDeadMansChest(Point position)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = position.X + i;
					int num2 = position.Y + j;
					Tile tile = Main.tile[num, num2];
					*tile.type = 467;
					*tile.frameX = (short)(144 + i * 18);
					*tile.frameY = (short)(j * 18);
				}
			}
			if (GenBase._random.Next(3) != 0)
			{
				return;
			}
			int num3 = Chest.FindChest(position.X, position.Y);
			if (num3 <= -1)
			{
				return;
			}
			Item[] item = Main.chest[num3].item;
			for (int num4 = item.Length - 2; num4 > 0; num4--)
			{
				Item item2 = item[num4];
				if (item2.stack != 0)
				{
					item[num4 + 1] = item2.DeepClone();
				}
			}
			item[1] = new Item();
			item[1].SetDefaults(5007);
			Main.chest[num3].item = item;
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00633D60 File Offset: 0x00631F60
		private unsafe void ActuallyPlaceDartTrap(Point position, int directionX, int x, int y, int xPush, Tile t)
		{
			*t.type = 137;
			*t.frameY = 0;
			if (directionX == -1)
			{
				*t.frameX = 18;
			}
			else
			{
				*t.frameX = 0;
			}
			t.slope(0);
			t.halfBrick(false);
			WorldGen.TileFrame(x, y, true, false);
			this.PlaceWireLine(position, directionX, 0, xPush);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00633DC4 File Offset: 0x00631FC4
		private void PlaceWireLine(Point start, int offsetX, int offsetY, int steps)
		{
			for (int i = 0; i <= steps; i++)
			{
				Main.tile[start.X + offsetX * i, start.Y + offsetY * i].wire(true);
			}
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00633E08 File Offset: 0x00632008
		private unsafe void ActuallyPlaceBoulderTrap(Point position, int yPush, int requiredHeight, int bestType)
		{
			for (int i = position.X; i < position.X + 2; i++)
			{
				for (int j = position.Y - requiredHeight; j <= position.Y + 2; j++)
				{
					Tile tile = Main.tile[i, j];
					if (j < position.Y - requiredHeight + 2)
					{
						tile.ClearTile();
					}
					else if (j <= position.Y)
					{
						if (!tile.active())
						{
							tile.active(true);
							*tile.type = (ushort)bestType;
						}
						tile.slope(0);
						tile.halfBrick(false);
						tile.actuator(true);
						tile.wire(true);
						WorldGen.TileFrame(i, j, true, false);
					}
					else
					{
						tile.ClearTile();
					}
				}
			}
			int num = position.X + 1;
			int num2 = position.Y - requiredHeight + 1;
			int num3 = 3;
			int num4 = num - num3;
			int num5 = num2 - num3;
			int num6 = num + num3 - 1;
			int num7 = num2 + num3 - 1;
			for (int k = num4; k <= num6; k++)
			{
				for (int l = num5; l <= num7; l++)
				{
					if (*Main.tile[k, l].type != 138)
					{
						*Main.tile[k, l].type = 1;
					}
				}
			}
			WorldGen.PlaceTile(num, num2, 138, false, false, -1, 0);
			this.PlaceWireLine(position, 0, 1, yPush);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00633F80 File Offset: 0x00632180
		private unsafe void ActuallyPlaceExplosive(Point position)
		{
			Tile tile = Main.tile[position.X, position.Y];
			*tile.type = 141;
			*tile.frameX = (*tile.frameY = 0);
			tile.slope(0);
			tile.halfBrick(false);
			WorldGen.TileFrame(position.X, position.Y, true, false);
		}

		// Token: 0x04005B99 RID: 23449
		private List<DeadMansChestBiome.DartTrapPlacementAttempt> _dartTrapPlacementSpots = new List<DeadMansChestBiome.DartTrapPlacementAttempt>();

		// Token: 0x04005B9A RID: 23450
		private List<DeadMansChestBiome.WirePlacementAttempt> _wirePlacementSpots = new List<DeadMansChestBiome.WirePlacementAttempt>();

		// Token: 0x04005B9B RID: 23451
		private List<DeadMansChestBiome.BoulderPlacementAttempt> _boulderPlacementSpots = new List<DeadMansChestBiome.BoulderPlacementAttempt>();

		// Token: 0x04005B9C RID: 23452
		private List<DeadMansChestBiome.ExplosivePlacementAttempt> _explosivePlacementAttempt = new List<DeadMansChestBiome.ExplosivePlacementAttempt>();

		// Token: 0x04005B9D RID: 23453
		[JsonProperty("NumberOfDartTraps")]
		private IntRange _numberOfDartTraps = new IntRange(3, 6);

		// Token: 0x04005B9E RID: 23454
		[JsonProperty("NumberOfBoulderTraps")]
		private IntRange _numberOfBoulderTraps = new IntRange(2, 4);

		// Token: 0x04005B9F RID: 23455
		[JsonProperty("NumberOfStepsBetweenBoulderTraps")]
		private IntRange _numberOfStepsBetweenBoulderTraps = new IntRange(2, 4);

		// Token: 0x02000D0A RID: 3338
		private class DartTrapPlacementAttempt
		{
			// Token: 0x060062E8 RID: 25320 RVA: 0x006D76C8 File Offset: 0x006D58C8
			public DartTrapPlacementAttempt(Point position, int directionX, int x, int y, int xPush, Tile t)
			{
				this.position = position;
				this.directionX = directionX;
				this.x = x;
				this.y = y;
				this.xPush = xPush;
				this.t = t;
			}

			// Token: 0x04007AA1 RID: 31393
			public int directionX;

			// Token: 0x04007AA2 RID: 31394
			public int xPush;

			// Token: 0x04007AA3 RID: 31395
			public int x;

			// Token: 0x04007AA4 RID: 31396
			public int y;

			// Token: 0x04007AA5 RID: 31397
			public Point position;

			// Token: 0x04007AA6 RID: 31398
			public Tile t;
		}

		// Token: 0x02000D0B RID: 3339
		private class BoulderPlacementAttempt
		{
			// Token: 0x060062E9 RID: 25321 RVA: 0x006D76FD File Offset: 0x006D58FD
			public BoulderPlacementAttempt(Point position, int yPush, int requiredHeight, int bestType)
			{
				this.position = position;
				this.yPush = yPush;
				this.requiredHeight = requiredHeight;
				this.bestType = bestType;
			}

			// Token: 0x04007AA7 RID: 31399
			public Point position;

			// Token: 0x04007AA8 RID: 31400
			public int yPush;

			// Token: 0x04007AA9 RID: 31401
			public int requiredHeight;

			// Token: 0x04007AAA RID: 31402
			public int bestType;
		}

		// Token: 0x02000D0C RID: 3340
		private class WirePlacementAttempt
		{
			// Token: 0x060062EA RID: 25322 RVA: 0x006D7722 File Offset: 0x006D5922
			public WirePlacementAttempt(Point position, int dirX, int dirY, int steps)
			{
				this.position = position;
				this.dirX = dirX;
				this.dirY = dirY;
				this.steps = steps;
			}

			// Token: 0x04007AAB RID: 31403
			public Point position;

			// Token: 0x04007AAC RID: 31404
			public int dirX;

			// Token: 0x04007AAD RID: 31405
			public int dirY;

			// Token: 0x04007AAE RID: 31406
			public int steps;
		}

		// Token: 0x02000D0D RID: 3341
		private class ExplosivePlacementAttempt
		{
			// Token: 0x060062EB RID: 25323 RVA: 0x006D7747 File Offset: 0x006D5947
			public ExplosivePlacementAttempt(Point position)
			{
				this.position = position;
			}

			// Token: 0x04007AAF RID: 31407
			public Point position;
		}
	}
}
