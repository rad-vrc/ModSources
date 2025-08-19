using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000060 RID: 96
	public static class Actions
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0048C7AC File Offset: 0x0048A9AC
		public static GenAction Chain(params GenAction[] actions)
		{
			for (int i = 0; i < actions.Length - 1; i++)
			{
				actions[i].NextAction = actions[i + 1];
			}
			return actions[0];
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0048C7D9 File Offset: 0x0048A9D9
		public static GenAction Continue(GenAction action)
		{
			return new Actions.ContinueWrapper(action);
		}

		// Token: 0x020004F2 RID: 1266
		public class ContinueWrapper : GenAction
		{
			// Token: 0x0600300B RID: 12299 RVA: 0x005E1AE3 File Offset: 0x005DFCE3
			public ContinueWrapper(GenAction action)
			{
				this._action = action;
			}

			// Token: 0x0600300C RID: 12300 RVA: 0x005E1AF2 File Offset: 0x005DFCF2
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._action.Apply(origin, x, y, args);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057B7 RID: 22455
			private GenAction _action;
		}

		// Token: 0x020004F3 RID: 1267
		public class Count : GenAction
		{
			// Token: 0x0600300D RID: 12301 RVA: 0x005E1B10 File Offset: 0x005DFD10
			public Count(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x0600300E RID: 12302 RVA: 0x005E1B1F File Offset: 0x005DFD1F
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057B8 RID: 22456
			private Ref<int> _count;
		}

		// Token: 0x020004F4 RID: 1268
		public class Scanner : GenAction
		{
			// Token: 0x0600300F RID: 12303 RVA: 0x005E1B3F File Offset: 0x005DFD3F
			public Scanner(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x06003010 RID: 12304 RVA: 0x005E1B4E File Offset: 0x005DFD4E
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057B9 RID: 22457
			private Ref<int> _count;
		}

		// Token: 0x020004F5 RID: 1269
		public class TileScanner : GenAction
		{
			// Token: 0x06003011 RID: 12305 RVA: 0x005E1B70 File Offset: 0x005DFD70
			public TileScanner(params ushort[] tiles)
			{
				this._tileIds = tiles;
				this._tileCounts = new Dictionary<ushort, int>();
				for (int i = 0; i < tiles.Length; i++)
				{
					this._tileCounts[this._tileIds[i]] = 0;
				}
			}

			// Token: 0x06003012 RID: 12306 RVA: 0x005E1BB8 File Offset: 0x005DFDB8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (tile.active() && this._tileCounts.ContainsKey(tile.type))
				{
					Dictionary<ushort, int> tileCounts = this._tileCounts;
					ushort type = tile.type;
					int num = tileCounts[type];
					tileCounts[type] = num + 1;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003013 RID: 12307 RVA: 0x005E1C18 File Offset: 0x005DFE18
			public Actions.TileScanner Output(Dictionary<ushort, int> resultsOutput)
			{
				this._tileCounts = resultsOutput;
				for (int i = 0; i < this._tileIds.Length; i++)
				{
					if (!this._tileCounts.ContainsKey(this._tileIds[i]))
					{
						this._tileCounts[this._tileIds[i]] = 0;
					}
				}
				return this;
			}

			// Token: 0x06003014 RID: 12308 RVA: 0x005E1C69 File Offset: 0x005DFE69
			public Dictionary<ushort, int> GetResults()
			{
				return this._tileCounts;
			}

			// Token: 0x06003015 RID: 12309 RVA: 0x005E1C71 File Offset: 0x005DFE71
			public int GetCount(ushort tileId)
			{
				if (!this._tileCounts.ContainsKey(tileId))
				{
					return -1;
				}
				return this._tileCounts[tileId];
			}

			// Token: 0x040057BA RID: 22458
			private ushort[] _tileIds;

			// Token: 0x040057BB RID: 22459
			private Dictionary<ushort, int> _tileCounts;
		}

		// Token: 0x020004F6 RID: 1270
		public class Blank : GenAction
		{
			// Token: 0x06003016 RID: 12310 RVA: 0x005E1C8F File Offset: 0x005DFE8F
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020004F7 RID: 1271
		public class Custom : GenAction
		{
			// Token: 0x06003018 RID: 12312 RVA: 0x005E1C9C File Offset: 0x005DFE9C
			public Custom(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x06003019 RID: 12313 RVA: 0x005E1CAB File Offset: 0x005DFEAB
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return this._perUnit(x, y, args) | base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057BC RID: 22460
			private GenBase.CustomPerUnitAction _perUnit;
		}

		// Token: 0x020004F8 RID: 1272
		public class ClearMetadata : GenAction
		{
			// Token: 0x0600301A RID: 12314 RVA: 0x005E1CC8 File Offset: 0x005DFEC8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearMetadata();
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020004F9 RID: 1273
		public class Clear : GenAction
		{
			// Token: 0x0600301C RID: 12316 RVA: 0x005E1CE6 File Offset: 0x005DFEE6
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearEverything();
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020004FA RID: 1274
		public class ClearTile : GenAction
		{
			// Token: 0x0600301E RID: 12318 RVA: 0x005E1D04 File Offset: 0x005DFF04
			public ClearTile(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x0600301F RID: 12319 RVA: 0x005E1D13 File Offset: 0x005DFF13
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearTile(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057BD RID: 22461
			private bool _frameNeighbors;
		}

		// Token: 0x020004FB RID: 1275
		public class ClearWall : GenAction
		{
			// Token: 0x06003020 RID: 12320 RVA: 0x005E1D2D File Offset: 0x005DFF2D
			public ClearWall(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06003021 RID: 12321 RVA: 0x005E1D3C File Offset: 0x005DFF3C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearWall(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057BE RID: 22462
			private bool _frameNeighbors;
		}

		// Token: 0x020004FC RID: 1276
		public class HalfBlock : GenAction
		{
			// Token: 0x06003022 RID: 12322 RVA: 0x005E1D56 File Offset: 0x005DFF56
			public HalfBlock(bool value = true)
			{
				this._value = value;
			}

			// Token: 0x06003023 RID: 12323 RVA: 0x005E1D65 File Offset: 0x005DFF65
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._value);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057BF RID: 22463
			private bool _value;
		}

		// Token: 0x020004FD RID: 1277
		public class SetTile : GenAction
		{
			// Token: 0x06003024 RID: 12324 RVA: 0x005E1D89 File Offset: 0x005DFF89
			public SetTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
			}

			// Token: 0x06003025 RID: 12325 RVA: 0x005E1DA8 File Offset: 0x005DFFA8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				GenBase._tiles[x, y].type = this._type;
				GenBase._tiles[x, y].active(true);
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057C0 RID: 22464
			private ushort _type;

			// Token: 0x040057C1 RID: 22465
			private bool _doFraming;

			// Token: 0x040057C2 RID: 22466
			private bool _doNeighborFraming;
		}

		// Token: 0x020004FE RID: 1278
		public class SetTileKeepWall : GenAction
		{
			// Token: 0x06003026 RID: 12326 RVA: 0x005E1E11 File Offset: 0x005E0011
			public SetTileKeepWall(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
			}

			// Token: 0x06003027 RID: 12327 RVA: 0x005E1E30 File Offset: 0x005E0030
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				ushort wall = GenBase._tiles[x, y].wall;
				int wallFrameX = GenBase._tiles[x, y].wallFrameX();
				int wallFrameY = GenBase._tiles[x, y].wallFrameY();
				GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				GenBase._tiles[x, y].type = this._type;
				GenBase._tiles[x, y].active(true);
				if (wall > 0)
				{
					GenBase._tiles[x, y].wall = wall;
					GenBase._tiles[x, y].wallFrameX(wallFrameX);
					GenBase._tiles[x, y].wallFrameY(wallFrameY);
				}
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057C3 RID: 22467
			private ushort _type;

			// Token: 0x040057C4 RID: 22468
			private bool _doFraming;

			// Token: 0x040057C5 RID: 22469
			private bool _doNeighborFraming;
		}

		// Token: 0x020004FF RID: 1279
		public class DebugDraw : GenAction
		{
			// Token: 0x06003028 RID: 12328 RVA: 0x005E1F09 File Offset: 0x005E0109
			public DebugDraw(SpriteBatch spriteBatch, Color color = default(Color))
			{
				this._spriteBatch = spriteBatch;
				this._color = color;
			}

			// Token: 0x06003029 RID: 12329 RVA: 0x005E1F20 File Offset: 0x005E0120
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((x << 4) - (int)Main.screenPosition.X, (y << 4) - (int)Main.screenPosition.Y, 16, 16), this._color);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057C6 RID: 22470
			private Color _color;

			// Token: 0x040057C7 RID: 22471
			private SpriteBatch _spriteBatch;
		}

		// Token: 0x02000500 RID: 1280
		public class SetSlope : GenAction
		{
			// Token: 0x0600302A RID: 12330 RVA: 0x005E1F7A File Offset: 0x005E017A
			public SetSlope(int slope)
			{
				this._slope = slope;
			}

			// Token: 0x0600302B RID: 12331 RVA: 0x005E1F89 File Offset: 0x005E0189
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.SlopeTile(x, y, this._slope, false);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057C8 RID: 22472
			private int _slope;
		}

		// Token: 0x02000501 RID: 1281
		public class SetHalfTile : GenAction
		{
			// Token: 0x0600302C RID: 12332 RVA: 0x005E1FA5 File Offset: 0x005E01A5
			public SetHalfTile(bool halfTile)
			{
				this._halfTile = halfTile;
			}

			// Token: 0x0600302D RID: 12333 RVA: 0x005E1FB4 File Offset: 0x005E01B4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._halfTile);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057C9 RID: 22473
			private bool _halfTile;
		}

		// Token: 0x02000502 RID: 1282
		public class SetTileAndWallRainbowPaint : GenAction
		{
			// Token: 0x0600302F RID: 12335 RVA: 0x005E1FD8 File Offset: 0x005E01D8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				byte paintIDForPosition = this.GetPaintIDForPosition(x, y);
				GenBase._tiles[x, y].color(paintIDForPosition);
				GenBase._tiles[x, y].wallColor(paintIDForPosition);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003030 RID: 12336 RVA: 0x005E2020 File Offset: 0x005E0220
			private byte GetPaintIDForPosition(int x, int y)
			{
				int num = x % 52 + y % 52;
				num %= 26;
				if (num > 12)
				{
					num = 12 - (num - 12);
				}
				num = Math.Min(12, Math.Max(1, num));
				return (byte)(12 + num);
			}
		}

		// Token: 0x02000503 RID: 1283
		public class PlaceTile : GenAction
		{
			// Token: 0x06003031 RID: 12337 RVA: 0x005E205D File Offset: 0x005E025D
			public PlaceTile(ushort type, int style = 0)
			{
				this._type = type;
				this._style = style;
			}

			// Token: 0x06003032 RID: 12338 RVA: 0x005E2073 File Offset: 0x005E0273
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.PlaceTile(x, y, (int)this._type, true, false, -1, this._style);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057CA RID: 22474
			private ushort _type;

			// Token: 0x040057CB RID: 22475
			private int _style;
		}

		// Token: 0x02000504 RID: 1284
		public class RemoveWall : GenAction
		{
			// Token: 0x06003033 RID: 12339 RVA: 0x005E2097 File Offset: 0x005E0297
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].wall = 0;
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x02000505 RID: 1285
		public class PlaceWall : GenAction
		{
			// Token: 0x06003035 RID: 12341 RVA: 0x005E20B6 File Offset: 0x005E02B6
			public PlaceWall(ushort type, bool neighbors = true)
			{
				this._type = type;
				this._neighbors = neighbors;
			}

			// Token: 0x06003036 RID: 12342 RVA: 0x005E20CC File Offset: 0x005E02CC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].wall = this._type;
				WorldGen.SquareWallFrame(x, y, true);
				if (this._neighbors)
				{
					WorldGen.SquareWallFrame(x + 1, y, true);
					WorldGen.SquareWallFrame(x - 1, y, true);
					WorldGen.SquareWallFrame(x, y - 1, true);
					WorldGen.SquareWallFrame(x, y + 1, true);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057CC RID: 22476
			private ushort _type;

			// Token: 0x040057CD RID: 22477
			private bool _neighbors;
		}

		// Token: 0x02000506 RID: 1286
		public class SetLiquid : GenAction
		{
			// Token: 0x06003037 RID: 12343 RVA: 0x005E2133 File Offset: 0x005E0333
			public SetLiquid(int type = 0, byte value = 255)
			{
				this._value = value;
				this._type = type;
			}

			// Token: 0x06003038 RID: 12344 RVA: 0x005E2149 File Offset: 0x005E0349
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].liquidType(this._type);
				GenBase._tiles[x, y].liquid = this._value;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057CE RID: 22478
			private int _type;

			// Token: 0x040057CF RID: 22479
			private byte _value;
		}

		// Token: 0x02000507 RID: 1287
		public class SwapSolidTile : GenAction
		{
			// Token: 0x06003039 RID: 12345 RVA: 0x005E2184 File Offset: 0x005E0384
			public SwapSolidTile(ushort type)
			{
				this._type = type;
			}

			// Token: 0x0600303A RID: 12346 RVA: 0x005E2194 File Offset: 0x005E0394
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (WorldGen.SolidTile(tile))
				{
					tile.ResetToType(this._type);
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057D0 RID: 22480
			private ushort _type;
		}

		// Token: 0x02000508 RID: 1288
		public class SetFrames : GenAction
		{
			// Token: 0x0600303B RID: 12347 RVA: 0x005E21D4 File Offset: 0x005E03D4
			public SetFrames(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x0600303C RID: 12348 RVA: 0x005E21E3 File Offset: 0x005E03E3
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.TileFrame(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057D1 RID: 22481
			private bool _frameNeighbors;
		}

		// Token: 0x02000509 RID: 1289
		public class Smooth : GenAction
		{
			// Token: 0x0600303D RID: 12349 RVA: 0x005E21FD File Offset: 0x005E03FD
			public Smooth(bool applyToNeighbors = false)
			{
				this._applyToNeighbors = applyToNeighbors;
			}

			// Token: 0x0600303E RID: 12350 RVA: 0x005E220C File Offset: 0x005E040C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile.SmoothSlope(x, y, this._applyToNeighbors, false);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057D2 RID: 22482
			private bool _applyToNeighbors;
		}
	}
}
