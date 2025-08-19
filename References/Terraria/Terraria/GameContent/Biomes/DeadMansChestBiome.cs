using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002C6 RID: 710
	public class DeadMansChestBiome : MicroBiome
	{
		// Token: 0x0600227E RID: 8830 RVA: 0x00543114 File Offset: 0x00541314
		public override bool Place(Point origin, StructureMap structures)
		{
			if (!DeadMansChestBiome.IsAGoodSpot(origin))
			{
				return false;
			}
			this.ClearCaches();
			Point position = new Point(origin.X, origin.Y + 1);
			this.FindBoulderTrapSpots(position);
			this.FindDartTrapSpots(position);
			this.FindExplosiveTrapSpots(position);
			if (!this.AreThereEnoughTraps())
			{
				return false;
			}
			this.TurnGoldChestIntoDeadMansChest(origin);
			foreach (DeadMansChestBiome.DartTrapPlacementAttempt dartTrapPlacementAttempt in this._dartTrapPlacementSpots)
			{
				this.ActuallyPlaceDartTrap(dartTrapPlacementAttempt.position, dartTrapPlacementAttempt.directionX, dartTrapPlacementAttempt.x, dartTrapPlacementAttempt.y, dartTrapPlacementAttempt.xPush, dartTrapPlacementAttempt.t);
			}
			foreach (DeadMansChestBiome.WirePlacementAttempt wirePlacementAttempt in this._wirePlacementSpots)
			{
				this.PlaceWireLine(wirePlacementAttempt.position, wirePlacementAttempt.dirX, wirePlacementAttempt.dirY, wirePlacementAttempt.steps);
			}
			foreach (DeadMansChestBiome.BoulderPlacementAttempt boulderPlacementAttempt in this._boulderPlacementSpots)
			{
				this.ActuallyPlaceBoulderTrap(boulderPlacementAttempt.position, boulderPlacementAttempt.yPush, boulderPlacementAttempt.requiredHeight, boulderPlacementAttempt.bestType);
			}
			foreach (DeadMansChestBiome.ExplosivePlacementAttempt explosivePlacementAttempt in this._explosivePlacementAttempt)
			{
				this.ActuallyPlaceExplosive(explosivePlacementAttempt.position);
			}
			this.PlaceWiresForExplosives(origin);
			return true;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x005432E4 File Offset: 0x005414E4
		private void PlaceWiresForExplosives(Point origin)
		{
			if (this._explosivePlacementAttempt.Count > 0)
			{
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
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x005433BD File Offset: 0x005415BD
		private bool AreThereEnoughTraps()
		{
			return (this._boulderPlacementSpots.Count >= 1 || this._explosivePlacementAttempt.Count >= 1) && this._dartTrapPlacementSpots.Count >= 1;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x005433EE File Offset: 0x005415EE
		private void ClearCaches()
		{
			this._dartTrapPlacementSpots.Clear();
			this._wirePlacementSpots.Clear();
			this._boulderPlacementSpots.Clear();
			this._explosivePlacementAttempt.Clear();
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0054341C File Offset: 0x0054161C
		private void FindBoulderTrapSpots(Point position)
		{
			int num = position.X;
			int num2 = GenBase._random.Next(this._numberOfBoulderTraps);
			int num3 = GenBase._random.Next(this._numberOfStepsBetweenBoulderTraps);
			num -= num2 / 2 * num3;
			int num4 = position.Y - 6;
			for (int i = 0; i <= num2; i++)
			{
				this.FindBoulderTrapSpot(new Point(num, num4));
				num += num3;
			}
			if (this._boulderPlacementSpots.Count > 0)
			{
				int num5 = this._boulderPlacementSpots[0].position.X;
				int num6 = this._boulderPlacementSpots[0].position.X;
				for (int j = 1; j < this._boulderPlacementSpots.Count; j++)
				{
					int x = this._boulderPlacementSpots[j].position.X;
					if (num5 > x)
					{
						num5 = x;
					}
					if (num6 < x)
					{
						num6 = x;
					}
				}
				if (num5 > position.X)
				{
					num5 = position.X;
				}
				if (num6 < position.X)
				{
					num6 = position.X;
				}
				this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(new Point(num5, num4 - 1), 1, 0, num6 - num5));
				this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(position, 0, -1, 7));
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00543568 File Offset: 0x00541768
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

		// Token: 0x06002284 RID: 8836 RVA: 0x005435B8 File Offset: 0x005417B8
		private void PlaceBoulderTrapSpot(Point position, int yPush)
		{
			int[] array = new int[(int)TileID.Count];
			for (int i = position.X; i < position.X + 2; i++)
			{
				for (int j = position.Y - 4; j <= position.Y; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && !Main.tileFrameImportant[(int)tile.type] && Main.tileSolid[(int)tile.type])
					{
						array[(int)tile.type]++;
					}
					if (tile.active() && !TileID.Sets.CanBeClearedDuringGeneration[(int)tile.type])
					{
						return;
					}
					if (tile.active() && TileID.Sets.IsAContainer[(int)tile.type])
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
					if (!tile2.active())
					{
						return;
					}
					if (TileID.Sets.IsAContainer[(int)tile2.type])
					{
						return;
					}
				}
			}
			int num = 2;
			int num2 = position.X - num;
			int num3 = position.Y - 4 - num;
			int num4 = position.X + num + 1;
			int num5 = position.Y - 4 + num + 1;
			for (int m = num2; m <= num4; m++)
			{
				for (int n = num3; n <= num5; n++)
				{
					Tile tile3 = Main.tile[m, n];
					if (tile3.active() && TileID.Sets.IsAContainer[(int)tile3.type])
					{
						return;
					}
				}
			}
			int num6 = -1;
			for (int num7 = 0; num7 < array.Length; num7++)
			{
				if (num6 == -1 || array[num6] < array[num7])
				{
					num6 = num7;
				}
			}
			this._boulderPlacementSpots.Add(new DeadMansChestBiome.BoulderPlacementAttempt(position, yPush - 1, 4, num6));
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x005437B4 File Offset: 0x005419B4
		private void FindDartTrapSpots(Point position)
		{
			int num = GenBase._random.Next(this._numberOfDartTraps);
			int num2 = (GenBase._random.Next(2) == 0) ? -1 : 1;
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

		// Token: 0x06002286 RID: 8838 RVA: 0x00543834 File Offset: 0x00541A34
		private bool FindDartTrapSpotSingle(Point position, int directionX)
		{
			int x = position.X;
			int y = position.Y;
			int i = 0;
			while (i < 20)
			{
				Tile tile = Main.tile[x + i * directionX, y];
				if ((!tile.active() || tile.type < 0 || tile.type >= TileID.Count || !TileID.Sets.IsAContainer[(int)tile.type]) && tile.active() && Main.tileSolid[(int)tile.type])
				{
					if (i >= 5 && !tile.actuator() && !Main.tileFrameImportant[(int)tile.type] && TileID.Sets.CanBeClearedDuringGeneration[(int)tile.type])
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

		// Token: 0x06002287 RID: 8839 RVA: 0x005438F8 File Offset: 0x00541AF8
		private void FindExplosiveTrapSpots(Point position)
		{
			int num = position.X;
			int y = position.Y + 3;
			List<int> list = new List<int>();
			if (this.IsGoodSpotsForExplosive(num, y))
			{
				list.Add(num);
			}
			num++;
			if (this.IsGoodSpotsForExplosive(num, y))
			{
				list.Add(num);
			}
			int num2 = -1;
			if (list.Count > 0)
			{
				num2 = list[GenBase._random.Next(list.Count)];
			}
			list.Clear();
			num += GenBase._random.Next(2, 6);
			int num3 = 4;
			for (int i = num; i < num + num3; i++)
			{
				if (this.IsGoodSpotsForExplosive(i, y))
				{
					list.Add(i);
				}
			}
			int num4 = -1;
			if (list.Count > 0)
			{
				num4 = list[GenBase._random.Next(list.Count)];
			}
			num = position.X - num3 - GenBase._random.Next(2, 6);
			for (int j = num; j < num + num3; j++)
			{
				if (this.IsGoodSpotsForExplosive(j, y))
				{
					list.Add(j);
				}
			}
			int num5 = -1;
			if (list.Count > 0)
			{
				num5 = list[GenBase._random.Next(list.Count)];
			}
			if (num5 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num5, y)));
			}
			if (num2 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num2, y)));
			}
			if (num4 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num4, y)));
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00543A7C File Offset: 0x00541C7C
		private bool IsGoodSpotsForExplosive(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return (!tile.active() || tile.type < 0 || tile.type >= TileID.Count || !TileID.Sets.IsAContainer[(int)tile.type]) && (tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileFrameImportant[(int)tile.type] && !Main.tileSolidTop[(int)tile.type]);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00543AFC File Offset: 0x00541CFC
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
					Point point = new Point(chest.x, chest.y);
					if (DeadMansChestBiome.IsAGoodSpot(point))
					{
						this.ClearCaches();
						Point position = new Point(point.X, point.Y + 1);
						this.FindBoulderTrapSpots(position);
						this.FindDartTrapSpots(position);
						if (this.AreThereEnoughTraps() && (structures == null || structures.CanPlace(new Rectangle(point.X, point.Y, 1, 1), array, 10)))
						{
							list.Add(j);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00543BE4 File Offset: 0x00541DE4
		private static bool IsAGoodSpot(Point position)
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
			if (tile.type != 21)
			{
				return false;
			}
			if (tile.frameX / 36 != 1)
			{
				return false;
			}
			tile = Main.tile[position.X, position.Y + 2];
			return TileID.Sets.CanBeClearedDuringGeneration[(int)tile.type] && WorldGen.countWires(position.X, position.Y, 20) <= 0 && WorldGen.countTiles(position.X, position.Y, false, true) >= 40;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00543CAC File Offset: 0x00541EAC
		private void TurnGoldChestIntoDeadMansChest(Point position)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = position.X + i;
					int num2 = position.Y + j;
					Tile tile = Main.tile[num, num2];
					tile.type = 467;
					tile.frameX = (short)(144 + i * 18);
					tile.frameY = (short)(j * 18);
				}
			}
			if (GenBase._random.Next(3) == 0)
			{
				int num3 = Chest.FindChest(position.X, position.Y);
				if (num3 > -1)
				{
					Item[] item = Main.chest[num3].item;
					for (int k = item.Length - 2; k > 0; k--)
					{
						Item item2 = item[k];
						if (item2.stack != 0)
						{
							item[k + 1] = item2.DeepClone();
						}
					}
					item[1] = new Item();
					item[1].SetDefaults(5007);
					Main.chest[num3].item = item;
				}
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00543DA8 File Offset: 0x00541FA8
		private void ActuallyPlaceDartTrap(Point position, int directionX, int x, int y, int xPush, Tile t)
		{
			t.type = 137;
			t.frameY = 0;
			if (directionX == -1)
			{
				t.frameX = 18;
			}
			else
			{
				t.frameX = 0;
			}
			t.slope(0);
			t.halfBrick(false);
			WorldGen.TileFrame(x, y, true, false);
			this.PlaceWireLine(position, directionX, 0, xPush);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00543E08 File Offset: 0x00542008
		private void PlaceWireLine(Point start, int offsetX, int offsetY, int steps)
		{
			for (int i = 0; i <= steps; i++)
			{
				Main.tile[start.X + offsetX * i, start.Y + offsetY * i].wire(true);
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00543E48 File Offset: 0x00542048
		private void ActuallyPlaceBoulderTrap(Point position, int yPush, int requiredHeight, int bestType)
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
							tile.type = (ushort)bestType;
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
					if (Main.tile[k, l].type != 138)
					{
						Main.tile[k, l].type = 1;
					}
				}
			}
			WorldGen.PlaceTile(num, num2, 138, false, false, -1, 0);
			this.PlaceWireLine(position, 0, 1, yPush);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00543FB4 File Offset: 0x005421B4
		private void ActuallyPlaceExplosive(Point position)
		{
			Tile tile = Main.tile[position.X, position.Y];
			tile.type = 141;
			tile.frameX = (tile.frameY = 0);
			tile.slope(0);
			tile.halfBrick(false);
			WorldGen.TileFrame(position.X, position.Y, true, false);
		}

		// Token: 0x040047D7 RID: 18391
		private List<DeadMansChestBiome.DartTrapPlacementAttempt> _dartTrapPlacementSpots = new List<DeadMansChestBiome.DartTrapPlacementAttempt>();

		// Token: 0x040047D8 RID: 18392
		private List<DeadMansChestBiome.WirePlacementAttempt> _wirePlacementSpots = new List<DeadMansChestBiome.WirePlacementAttempt>();

		// Token: 0x040047D9 RID: 18393
		private List<DeadMansChestBiome.BoulderPlacementAttempt> _boulderPlacementSpots = new List<DeadMansChestBiome.BoulderPlacementAttempt>();

		// Token: 0x040047DA RID: 18394
		private List<DeadMansChestBiome.ExplosivePlacementAttempt> _explosivePlacementAttempt = new List<DeadMansChestBiome.ExplosivePlacementAttempt>();

		// Token: 0x040047DB RID: 18395
		[JsonProperty("NumberOfDartTraps")]
		private IntRange _numberOfDartTraps = new IntRange(3, 6);

		// Token: 0x040047DC RID: 18396
		[JsonProperty("NumberOfBoulderTraps")]
		private IntRange _numberOfBoulderTraps = new IntRange(2, 4);

		// Token: 0x040047DD RID: 18397
		[JsonProperty("NumberOfStepsBetweenBoulderTraps")]
		private IntRange _numberOfStepsBetweenBoulderTraps = new IntRange(2, 4);

		// Token: 0x020006C4 RID: 1732
		private class DartTrapPlacementAttempt
		{
			// Token: 0x06003677 RID: 13943 RVA: 0x0060C763 File Offset: 0x0060A963
			public DartTrapPlacementAttempt(Point position, int directionX, int x, int y, int xPush, Tile t)
			{
				this.position = position;
				this.directionX = directionX;
				this.x = x;
				this.y = y;
				this.xPush = xPush;
				this.t = t;
			}

			// Token: 0x0400620E RID: 25102
			public int directionX;

			// Token: 0x0400620F RID: 25103
			public int xPush;

			// Token: 0x04006210 RID: 25104
			public int x;

			// Token: 0x04006211 RID: 25105
			public int y;

			// Token: 0x04006212 RID: 25106
			public Point position;

			// Token: 0x04006213 RID: 25107
			public Tile t;
		}

		// Token: 0x020006C5 RID: 1733
		private class BoulderPlacementAttempt
		{
			// Token: 0x06003678 RID: 13944 RVA: 0x0060C798 File Offset: 0x0060A998
			public BoulderPlacementAttempt(Point position, int yPush, int requiredHeight, int bestType)
			{
				this.position = position;
				this.yPush = yPush;
				this.requiredHeight = requiredHeight;
				this.bestType = bestType;
			}

			// Token: 0x04006214 RID: 25108
			public Point position;

			// Token: 0x04006215 RID: 25109
			public int yPush;

			// Token: 0x04006216 RID: 25110
			public int requiredHeight;

			// Token: 0x04006217 RID: 25111
			public int bestType;
		}

		// Token: 0x020006C6 RID: 1734
		private class WirePlacementAttempt
		{
			// Token: 0x06003679 RID: 13945 RVA: 0x0060C7BD File Offset: 0x0060A9BD
			public WirePlacementAttempt(Point position, int dirX, int dirY, int steps)
			{
				this.position = position;
				this.dirX = dirX;
				this.dirY = dirY;
				this.steps = steps;
			}

			// Token: 0x04006218 RID: 25112
			public Point position;

			// Token: 0x04006219 RID: 25113
			public int dirX;

			// Token: 0x0400621A RID: 25114
			public int dirY;

			// Token: 0x0400621B RID: 25115
			public int steps;
		}

		// Token: 0x020006C7 RID: 1735
		private class ExplosivePlacementAttempt
		{
			// Token: 0x0600367A RID: 13946 RVA: 0x0060C7E2 File Offset: 0x0060A9E2
			public ExplosivePlacementAttempt(Point position)
			{
				this.position = position;
			}

			// Token: 0x0400621C RID: 25116
			public Point position;
		}
	}
}
