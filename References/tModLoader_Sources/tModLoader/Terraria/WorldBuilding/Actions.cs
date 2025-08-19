using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006E RID: 110
	public static class Actions
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0049F544 File Offset: 0x0049D744
		public static GenAction Chain(params GenAction[] actions)
		{
			for (int i = 0; i < actions.Length - 1; i++)
			{
				actions[i].NextAction = actions[i + 1];
			}
			return actions[0];
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0049F571 File Offset: 0x0049D771
		public static GenAction Continue(GenAction action)
		{
			return new Actions.ContinueWrapper(action);
		}

		// Token: 0x0200080D RID: 2061
		public class ContinueWrapper : GenAction
		{
			// Token: 0x06005070 RID: 20592 RVA: 0x00693E46 File Offset: 0x00692046
			public ContinueWrapper(GenAction action)
			{
				this._action = action;
			}

			// Token: 0x06005071 RID: 20593 RVA: 0x00693E55 File Offset: 0x00692055
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._action.Apply(origin, x, y, args);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400686F RID: 26735
			private GenAction _action;
		}

		// Token: 0x0200080E RID: 2062
		public class Count : GenAction
		{
			// Token: 0x06005072 RID: 20594 RVA: 0x00693E73 File Offset: 0x00692073
			public Count(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x06005073 RID: 20595 RVA: 0x00693E82 File Offset: 0x00692082
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006870 RID: 26736
			private Ref<int> _count;
		}

		// Token: 0x0200080F RID: 2063
		public class Scanner : GenAction
		{
			// Token: 0x06005074 RID: 20596 RVA: 0x00693EA2 File Offset: 0x006920A2
			public Scanner(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x06005075 RID: 20597 RVA: 0x00693EB1 File Offset: 0x006920B1
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006871 RID: 26737
			private Ref<int> _count;
		}

		// Token: 0x02000810 RID: 2064
		public class TileScanner : GenAction
		{
			// Token: 0x06005076 RID: 20598 RVA: 0x00693ED4 File Offset: 0x006920D4
			public TileScanner(params ushort[] tiles)
			{
				this._tileIds = tiles;
				this._tileCounts = new Dictionary<ushort, int>();
				for (int i = 0; i < tiles.Length; i++)
				{
					this._tileCounts[this._tileIds[i]] = 0;
				}
			}

			// Token: 0x06005077 RID: 20599 RVA: 0x00693F1C File Offset: 0x0069211C
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (tile.active() && this._tileCounts.ContainsKey(*tile.type))
				{
					Dictionary<ushort, int> tileCounts = this._tileCounts;
					ushort key = *tile.type;
					int num = tileCounts[key];
					tileCounts[key] = num + 1;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06005078 RID: 20600 RVA: 0x00693F80 File Offset: 0x00692180
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

			// Token: 0x06005079 RID: 20601 RVA: 0x00693FD1 File Offset: 0x006921D1
			public Dictionary<ushort, int> GetResults()
			{
				return this._tileCounts;
			}

			// Token: 0x0600507A RID: 20602 RVA: 0x00693FD9 File Offset: 0x006921D9
			public int GetCount(ushort tileId)
			{
				if (!this._tileCounts.ContainsKey(tileId))
				{
					return -1;
				}
				return this._tileCounts[tileId];
			}

			// Token: 0x04006872 RID: 26738
			private ushort[] _tileIds;

			// Token: 0x04006873 RID: 26739
			private Dictionary<ushort, int> _tileCounts;
		}

		// Token: 0x02000811 RID: 2065
		public class Blank : GenAction
		{
			// Token: 0x0600507B RID: 20603 RVA: 0x00693FF7 File Offset: 0x006921F7
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x02000812 RID: 2066
		public class Custom : GenAction
		{
			// Token: 0x0600507D RID: 20605 RVA: 0x0069400C File Offset: 0x0069220C
			public Custom(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x0600507E RID: 20606 RVA: 0x0069401B File Offset: 0x0069221B
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return this._perUnit(x, y, args) | base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006874 RID: 26740
			private GenBase.CustomPerUnitAction _perUnit;
		}

		// Token: 0x02000813 RID: 2067
		public class ClearMetadata : GenAction
		{
			// Token: 0x0600507F RID: 20607 RVA: 0x00694038 File Offset: 0x00692238
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearMetadata();
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x02000814 RID: 2068
		public class Clear : GenAction
		{
			// Token: 0x06005081 RID: 20609 RVA: 0x0069406C File Offset: 0x0069226C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearEverything();
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x02000815 RID: 2069
		public class ClearTile : GenAction
		{
			// Token: 0x06005083 RID: 20611 RVA: 0x006940A0 File Offset: 0x006922A0
			public ClearTile(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06005084 RID: 20612 RVA: 0x006940AF File Offset: 0x006922AF
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearTile(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006875 RID: 26741
			private bool _frameNeighbors;
		}

		// Token: 0x02000816 RID: 2070
		public class ClearWall : GenAction
		{
			// Token: 0x06005085 RID: 20613 RVA: 0x006940C9 File Offset: 0x006922C9
			public ClearWall(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06005086 RID: 20614 RVA: 0x006940D8 File Offset: 0x006922D8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearWall(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006876 RID: 26742
			private bool _frameNeighbors;
		}

		// Token: 0x02000817 RID: 2071
		public class HalfBlock : GenAction
		{
			// Token: 0x06005087 RID: 20615 RVA: 0x006940F2 File Offset: 0x006922F2
			public HalfBlock(bool value = true)
			{
				this._value = value;
			}

			// Token: 0x06005088 RID: 20616 RVA: 0x00694104 File Offset: 0x00692304
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._value);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006877 RID: 26743
			private bool _value;
		}

		// Token: 0x02000818 RID: 2072
		public class SetTile : GenAction
		{
			// Token: 0x06005089 RID: 20617 RVA: 0x00694136 File Offset: 0x00692336
			public SetTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
			}

			// Token: 0x0600508A RID: 20618 RVA: 0x00694154 File Offset: 0x00692354
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				*GenBase._tiles[x, y].type = this._type;
				GenBase._tiles[x, y].active(true);
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006878 RID: 26744
			private ushort _type;

			// Token: 0x04006879 RID: 26745
			private bool _doFraming;

			// Token: 0x0400687A RID: 26746
			private bool _doNeighborFraming;
		}

		// Token: 0x02000819 RID: 2073
		public class SetTileKeepWall : GenAction
		{
			// Token: 0x0600508B RID: 20619 RVA: 0x006941C7 File Offset: 0x006923C7
			public SetTileKeepWall(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
			}

			// Token: 0x0600508C RID: 20620 RVA: 0x006941E4 File Offset: 0x006923E4
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				ushort wall = *GenBase._tiles[x, y].wall;
				int wallFrameX = GenBase._tiles[x, y].wallFrameX();
				int wallFrameY = GenBase._tiles[x, y].wallFrameY();
				GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				*GenBase._tiles[x, y].type = this._type;
				GenBase._tiles[x, y].active(true);
				if (wall > 0)
				{
					*GenBase._tiles[x, y].wall = wall;
					GenBase._tiles[x, y].wallFrameX(wallFrameX);
					GenBase._tiles[x, y].wallFrameY(wallFrameY);
				}
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400687B RID: 26747
			private ushort _type;

			// Token: 0x0400687C RID: 26748
			private bool _doFraming;

			// Token: 0x0400687D RID: 26749
			private bool _doNeighborFraming;
		}

		// Token: 0x0200081A RID: 2074
		public class DebugDraw : GenAction
		{
			// Token: 0x0600508D RID: 20621 RVA: 0x006942DB File Offset: 0x006924DB
			public DebugDraw(SpriteBatch spriteBatch, Color color = default(Color))
			{
				this._spriteBatch = spriteBatch;
				this._color = color;
			}

			// Token: 0x0600508E RID: 20622 RVA: 0x006942F4 File Offset: 0x006924F4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((x << 4) - (int)Main.screenPosition.X, (y << 4) - (int)Main.screenPosition.Y, 16, 16), this._color);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400687E RID: 26750
			private Color _color;

			// Token: 0x0400687F RID: 26751
			private SpriteBatch _spriteBatch;
		}

		// Token: 0x0200081B RID: 2075
		public class SetSlope : GenAction
		{
			// Token: 0x0600508F RID: 20623 RVA: 0x0069434E File Offset: 0x0069254E
			public SetSlope(int slope)
			{
				this._slope = slope;
			}

			// Token: 0x06005090 RID: 20624 RVA: 0x0069435D File Offset: 0x0069255D
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.SlopeTile(x, y, this._slope, false);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006880 RID: 26752
			private int _slope;
		}

		// Token: 0x0200081C RID: 2076
		public class SetHalfTile : GenAction
		{
			// Token: 0x06005091 RID: 20625 RVA: 0x00694379 File Offset: 0x00692579
			public SetHalfTile(bool halfTile)
			{
				this._halfTile = halfTile;
			}

			// Token: 0x06005092 RID: 20626 RVA: 0x00694388 File Offset: 0x00692588
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._halfTile);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006881 RID: 26753
			private bool _halfTile;
		}

		// Token: 0x0200081D RID: 2077
		public class SetTileAndWallRainbowPaint : GenAction
		{
			// Token: 0x06005093 RID: 20627 RVA: 0x006943BC File Offset: 0x006925BC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				byte paintIDForPosition = this.GetPaintIDForPosition(x, y);
				GenBase._tiles[x, y].color(paintIDForPosition);
				GenBase._tiles[x, y].wallColor(paintIDForPosition);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06005094 RID: 20628 RVA: 0x00694408 File Offset: 0x00692608
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

		// Token: 0x0200081E RID: 2078
		public class PlaceTile : GenAction
		{
			// Token: 0x06005096 RID: 20630 RVA: 0x0069444D File Offset: 0x0069264D
			public PlaceTile(ushort type, int style = 0)
			{
				this._type = type;
				this._style = style;
			}

			// Token: 0x06005097 RID: 20631 RVA: 0x00694463 File Offset: 0x00692663
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.PlaceTile(x, y, (int)this._type, true, false, -1, this._style);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006882 RID: 26754
			private ushort _type;

			// Token: 0x04006883 RID: 26755
			private int _style;
		}

		// Token: 0x0200081F RID: 2079
		public class RemoveWall : GenAction
		{
			// Token: 0x06005098 RID: 20632 RVA: 0x00694488 File Offset: 0x00692688
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				*GenBase._tiles[x, y].wall = 0;
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x02000820 RID: 2080
		public class PlaceWall : GenAction
		{
			// Token: 0x0600509A RID: 20634 RVA: 0x006944BE File Offset: 0x006926BE
			public PlaceWall(ushort type, bool neighbors = true)
			{
				this._type = type;
				this._neighbors = neighbors;
			}

			// Token: 0x0600509B RID: 20635 RVA: 0x006944D4 File Offset: 0x006926D4
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				*GenBase._tiles[x, y].wall = this._type;
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

			// Token: 0x04006884 RID: 26756
			private ushort _type;

			// Token: 0x04006885 RID: 26757
			private bool _neighbors;
		}

		// Token: 0x02000821 RID: 2081
		public class SetLiquid : GenAction
		{
			// Token: 0x0600509C RID: 20636 RVA: 0x0069453F File Offset: 0x0069273F
			public SetLiquid(int type = 0, byte value = 255)
			{
				this._value = value;
				this._type = type;
			}

			// Token: 0x0600509D RID: 20637 RVA: 0x00694558 File Offset: 0x00692758
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].liquidType(this._type);
				*GenBase._tiles[x, y].liquid = this._value;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006886 RID: 26758
			private int _type;

			// Token: 0x04006887 RID: 26759
			private byte _value;
		}

		// Token: 0x02000822 RID: 2082
		public class SwapSolidTile : GenAction
		{
			// Token: 0x0600509E RID: 20638 RVA: 0x006945A5 File Offset: 0x006927A5
			public SwapSolidTile(ushort type)
			{
				this._type = type;
			}

			// Token: 0x0600509F RID: 20639 RVA: 0x006945B4 File Offset: 0x006927B4
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

			// Token: 0x04006888 RID: 26760
			private ushort _type;
		}

		// Token: 0x02000823 RID: 2083
		public class SetFrames : GenAction
		{
			// Token: 0x060050A0 RID: 20640 RVA: 0x006945F5 File Offset: 0x006927F5
			public SetFrames(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x060050A1 RID: 20641 RVA: 0x00694604 File Offset: 0x00692804
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.TileFrame(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006889 RID: 26761
			private bool _frameNeighbors;
		}

		// Token: 0x02000824 RID: 2084
		public class Smooth : GenAction
		{
			// Token: 0x060050A2 RID: 20642 RVA: 0x0069461E File Offset: 0x0069281E
			public Smooth(bool applyToNeighbors = false)
			{
				this._applyToNeighbors = applyToNeighbors;
			}

			// Token: 0x060050A3 RID: 20643 RVA: 0x0069462D File Offset: 0x0069282D
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile.SmoothSlope(x, y, this._applyToNeighbors, false);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400688A RID: 26762
			private bool _applyToNeighbors;
		}
	}
}
