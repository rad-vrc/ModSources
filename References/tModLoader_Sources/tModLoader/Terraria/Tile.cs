using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria
{
	/// <summary>
	/// A data structure used for accessing information about tiles, walls, wires, and liquids at a single position in the world.<para />
	/// Vanilla tile code and a mods tile code will be quite different, since tModLoader reworked how tiles function to improve performance. This means that copying vanilla code will leave you with many errors. Running the code through tModPorter will fix most of the issues, however.<para />
	/// For your sanity, all of the changes are well documented to make it easier to port vanilla code.
	/// </summary>
	// Token: 0x02000058 RID: 88
	public readonly struct Tile
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x003FC411 File Offset: 0x003FA611
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>
		/// Resets all of the data at this position.<br />
		/// To only remove the tile data, use <see cref="M:Terraria.Tile.ClearTile" />.
		/// </summary>
		// Token: 0x06000F19 RID: 3865 RVA: 0x003FC423 File Offset: 0x003FA623
		public void ClearEverything()
		{
			TileData.ClearSingle(this.TileId);
		}

		/// <summary>
		/// Resets the tile data at this position.<br />
		/// Sets <see cref="P:Terraria.Tile.HasTile" /> and <see cref="P:Terraria.Tile.IsActuated" /> to <see langword="false" /> and sets the <see cref="P:Terraria.Tile.BlockType" /> to <see cref="F:Terraria.ID.BlockType.Solid" />.
		/// </summary>
		/// <remarks>
		/// Does not reset data related to walls, wires, or anything else. For that, use <see cref="M:Terraria.Tile.ClearEverything" />.
		/// </remarks>
		// Token: 0x06000F1A RID: 3866 RVA: 0x003FC430 File Offset: 0x003FA630
		public void ClearTile()
		{
			this.slope(0);
			this.halfBrick(false);
			this.active(false);
			this.inActive(false);
		}

		/// <summary>
		/// Copies all data from the given position to this position.
		/// </summary>
		/// <param name="from">The position to copy the data from.</param>
		// Token: 0x06000F1B RID: 3867 RVA: 0x003FC44E File Offset: 0x003FA64E
		public void CopyFrom(Tile from)
		{
			TileData.CopySingle(from.TileId, this.TileId);
		}

		/// <summary>
		/// Legacy code, consider the data you want to compare directly.
		/// </summary>
		/// <param name="compTile"></param>
		/// <returns></returns>
		// Token: 0x06000F1C RID: 3868 RVA: 0x003FC464 File Offset: 0x003FA664
		internal unsafe bool isTheSameAs(Tile compTile)
		{
			if (this.Get<TileWallWireStateData>().NonFrameBits != compTile.Get<TileWallWireStateData>().NonFrameBits)
			{
				return false;
			}
			if (*this.wall != *compTile.wall || *this.liquid != *compTile.liquid)
			{
				return false;
			}
			if (*this.liquid > 0 && this.LiquidType != compTile.LiquidType)
			{
				return false;
			}
			if (this.Get<TileWallBrightnessInvisibilityData>().Data != compTile.Get<TileWallBrightnessInvisibilityData>().Data)
			{
				return false;
			}
			if (this.HasTile)
			{
				if (*this.type != *compTile.type)
				{
					return false;
				}
				if (Main.tileFrameImportant[(int)(*this.type)] && (*this.frameX != *compTile.frameX || *this.frameY != *compTile.frameY))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.BlockType" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F1D RID: 3869 RVA: 0x003FC538 File Offset: 0x003FA738
		internal int blockType()
		{
			if (this.halfBrick())
			{
				return 1;
			}
			int num = (int)this.slope();
			if (num > 0)
			{
				num++;
			}
			return num;
		}

		/// <summary>
		/// Resets all of the data at this position except for the <see cref="P:Terraria.Tile.WallType" />, and sets <see cref="P:Terraria.Tile.TileType" /> to <paramref name="type" />. 
		/// </summary>
		/// <param name="type">The <see cref="T:Terraria.ID.TileID" /> to set this tile to.</param>
		// Token: 0x06000F1E RID: 3870 RVA: 0x003FC55F File Offset: 0x003FA75F
		public unsafe void ResetToType(ushort type)
		{
			this.ClearMetadata();
			this.HasTile = true;
			*this.TileType = type;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x003FC576 File Offset: 0x003FA776
		internal unsafe void ClearMetadata()
		{
			*this.Get<LiquidData>() = default(LiquidData);
			*this.Get<TileWallWireStateData>() = default(TileWallWireStateData);
			*this.Get<TileWallBrightnessInvisibilityData>() = default(TileWallBrightnessInvisibilityData);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x003FC59C File Offset: 0x003FA79C
		internal Color actColor(Color oldColor)
		{
			if (!this.inActive())
			{
				return oldColor;
			}
			double num = 0.4;
			return new Color((int)((byte)(num * (double)oldColor.R)), (int)((byte)(num * (double)oldColor.G)), (int)((byte)(num * (double)oldColor.B)), (int)oldColor.A);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x003FC5EA File Offset: 0x003FA7EA
		internal void actColor(ref Vector3 oldColor)
		{
			if (this.inActive())
			{
				oldColor *= 0.4f;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TopSlope" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F22 RID: 3874 RVA: 0x003FC60C File Offset: 0x003FA80C
		internal bool topSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 2;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.BottomSlope" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F23 RID: 3875 RVA: 0x003FC62C File Offset: 0x003FA82C
		internal bool bottomSlope()
		{
			byte b = this.slope();
			return b == 3 || b == 4;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.LeftSlope" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F24 RID: 3876 RVA: 0x003FC64C File Offset: 0x003FA84C
		internal bool leftSlope()
		{
			byte b = this.slope();
			return b == 2 || b == 4;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.RightSlope" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F25 RID: 3877 RVA: 0x003FC66C File Offset: 0x003FA86C
		internal bool rightSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 3;
		}

		/// <summary>
		/// Clears the specified data at this position based on the given <see cref="T:Terraria.DataStructures.TileDataType" />.
		/// </summary>
		/// <param name="types">The <see cref="T:Terraria.DataStructures.TileDataType" /> to clear.</param>
		// Token: 0x06000F26 RID: 3878 RVA: 0x003FC68C File Offset: 0x003FA88C
		public unsafe void Clear(TileDataType types)
		{
			if ((types & TileDataType.Tile) != (TileDataType)0)
			{
				*this.type = 0;
				this.active(false);
				*this.frameX = 0;
				*this.frameY = 0;
			}
			if ((types & TileDataType.Wall) != (TileDataType)0)
			{
				*this.wall = 0;
				this.wallFrameX(0);
				this.wallFrameY(0);
			}
			if ((types & TileDataType.TilePaint) != (TileDataType)0)
			{
				this.ClearBlockPaintAndCoating();
			}
			if ((types & TileDataType.WallPaint) != (TileDataType)0)
			{
				this.ClearWallPaintAndCoating();
			}
			if ((types & TileDataType.Liquid) != (TileDataType)0)
			{
				*this.liquid = 0;
				this.liquidType(0);
				this.checkingLiquid(false);
			}
			if ((types & TileDataType.Slope) != (TileDataType)0)
			{
				this.slope(0);
				this.halfBrick(false);
			}
			if ((types & TileDataType.Wiring) != (TileDataType)0)
			{
				this.wire(false);
				this.wire2(false);
				this.wire3(false);
				this.wire4(false);
			}
			if ((types & TileDataType.Actuator) != (TileDataType)0)
			{
				this.actuator(false);
				this.inActive(false);
			}
		}

		/// <summary>
		/// Slopes a tile based on the tiles adjacent to it.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <param name="applyToNeighbors">Whether the adjacent tiles should be automatically smoothed.</param>
		/// <param name="sync">Whether the changes should automatically be synced to multiplayer.</param>
		// Token: 0x06000F27 RID: 3879 RVA: 0x003FC758 File Offset: 0x003FA958
		public static void SmoothSlope(int x, int y, bool applyToNeighbors = true, bool sync = false)
		{
			if (applyToNeighbors)
			{
				Tile.SmoothSlope(x + 1, y, false, sync);
				Tile.SmoothSlope(x - 1, y, false, sync);
				Tile.SmoothSlope(x, y + 1, false, sync);
				Tile.SmoothSlope(x, y - 1, false, sync);
			}
			Tile tile = Main.tile[x, y];
			if (!WorldGen.CanPoundTile(x, y) || !WorldGen.SolidOrSlopedTile(x, y))
			{
				return;
			}
			bool flag = !WorldGen.TileEmpty(x, y - 1);
			bool flag2 = !WorldGen.SolidOrSlopedTile(x, y - 1) && flag;
			bool flag3 = WorldGen.SolidOrSlopedTile(x, y + 1);
			bool flag4 = WorldGen.SolidOrSlopedTile(x - 1, y);
			bool flag5 = WorldGen.SolidOrSlopedTile(x + 1, y);
			int num = (((flag > false) ? 1 : 0) << 3 | ((flag3 > false) ? 1 : 0) << 2 | ((flag4 > false) ? 1 : 0) << 1 | flag5 > false) ? 1 : 0;
			bool flag6 = tile.halfBrick();
			int num2 = (int)tile.slope();
			switch (num)
			{
			case 4:
				tile.slope(0);
				tile.halfBrick(true);
				goto IL_151;
			case 5:
				tile.halfBrick(false);
				tile.slope(2);
				goto IL_151;
			case 6:
				tile.halfBrick(false);
				tile.slope(1);
				goto IL_151;
			case 9:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(4);
					goto IL_151;
				}
				goto IL_151;
			case 10:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(3);
					goto IL_151;
				}
				goto IL_151;
			}
			tile.halfBrick(false);
			tile.slope(0);
			IL_151:
			if (sync)
			{
				int num3 = (int)tile.slope();
				bool flag7 = flag6 != tile.halfBrick();
				bool flag8 = num2 != num3;
				if (flag7 && flag8)
				{
					NetMessage.SendData(17, -1, -1, null, 23, (float)x, (float)y, (float)num3, 0, 0, 0);
					return;
				}
				if (flag7)
				{
					NetMessage.SendData(17, -1, -1, null, 7, (float)x, (float)y, 1f, 0, 0, 0);
					return;
				}
				if (flag8)
				{
					NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)y, (float)num3, 0, 0, 0);
				}
			}
		}

		/// <summary>
		/// Copies the paint and coating data from the specified tile to this tile.<br />
		/// Does not copy wall paint and coating data.
		/// </summary>
		/// <param name="other">The <see cref="T:Terraria.Tile" /> to copy the data from.</param>
		// Token: 0x06000F28 RID: 3880 RVA: 0x003FC931 File Offset: 0x003FAB31
		public void CopyPaintAndCoating(Tile other)
		{
			this.color(other.color());
			this.invisibleBlock(other.invisibleBlock());
			this.fullbrightBlock(other.fullbrightBlock());
		}

		/// <summary>
		/// Gets the paint and coating information from the tile at this position as a <see cref="T:Terraria.TileColorCache" />.
		/// </summary>
		/// <returns>A <see cref="T:Terraria.TileColorCache" /> representing the paint and coatings on the tile at this position.</returns>
		// Token: 0x06000F29 RID: 3881 RVA: 0x003FC95C File Offset: 0x003FAB5C
		public TileColorCache BlockColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.color(),
				FullBright = this.fullbrightBlock(),
				Invisible = this.invisibleBlock()
			};
		}

		/// <summary>
		/// Gets the paint and coating information from the wall at this position as a <see cref="T:Terraria.TileColorCache" />.
		/// </summary>
		/// <returns>A <see cref="T:Terraria.TileColorCache" /> representing the paint and coatings on the wall at this position.</returns>
		// Token: 0x06000F2A RID: 3882 RVA: 0x003FC99C File Offset: 0x003FAB9C
		public TileColorCache WallColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.wallColor(),
				FullBright = this.fullbrightWall(),
				Invisible = this.invisibleWall()
			};
		}

		/// <summary>
		/// Sets the paint and coating of the tile at this position based on the given <see cref="T:Terraria.TileColorCache" />.
		/// </summary>
		/// <param name="cache">The <see cref="T:Terraria.TileColorCache" /> to apply.</param>
		// Token: 0x06000F2B RID: 3883 RVA: 0x003FC9D9 File Offset: 0x003FABD9
		public void UseBlockColors(TileColorCache cache)
		{
			cache.ApplyToBlock(this);
		}

		/// <summary>
		/// Sets the paint and coating of the wall at this position based on the given <see cref="T:Terraria.TileColorCache" />.
		/// </summary>
		/// <param name="cache">The <see cref="T:Terraria.TileColorCache" /> to apply.</param>
		// Token: 0x06000F2C RID: 3884 RVA: 0x003FC9E8 File Offset: 0x003FABE8
		public void UseWallColors(TileColorCache cache)
		{
			cache.ApplyToWall(this);
		}

		/// <summary>
		/// Clears any paint or coating on the tile at this position.
		/// </summary>
		// Token: 0x06000F2D RID: 3885 RVA: 0x003FC9F7 File Offset: 0x003FABF7
		public void ClearBlockPaintAndCoating()
		{
			this.color(0);
			this.fullbrightBlock(false);
			this.invisibleBlock(false);
		}

		/// <summary>
		/// Clears any paint or coating on the wall at this position.
		/// </summary>
		// Token: 0x06000F2E RID: 3886 RVA: 0x003FCA0E File Offset: 0x003FAC0E
		public void ClearWallPaintAndCoating()
		{
			this.wallColor(0);
			this.fullbrightWall(false);
			this.invisibleWall(false);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x003FCA28 File Offset: 0x003FAC28
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Tile Type:",
				this.type.ToString(),
				" Active:",
				this.active().ToString(),
				" Wall:",
				this.wall.ToString(),
				" Slope:",
				this.slope().ToString(),
				" fX:",
				this.frameX.ToString(),
				" fY:",
				this.frameY.ToString()
			});
		}

		/// <summary>
		/// The <see cref="T:Terraria.ID.TileID" /> of the tile at this position.<br />
		/// This value is only valid if <see cref="P:Terraria.Tile.HasTile" /> is true.<br />
		/// Legacy/vanilla equivalent is <see cref="P:Terraria.Tile.type" />.
		/// </summary>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x003FCACE File Offset: 0x003FACCE
		public ref ushort TileType
		{
			get
			{
				return ref this.Get<TileTypeData>().Type;
			}
		}

		/// <summary>
		/// The <see cref="T:Terraria.ID.WallID" /> of the wall at this position.<br />
		/// A value of 0 indicates no wall.<br />
		/// Legacy/vanilla equivalent is <see cref="P:Terraria.Tile.wall" />.
		/// </summary>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x003FCADB File Offset: 0x003FACDB
		public ref ushort WallType
		{
			get
			{
				return ref this.Get<WallTypeData>().Type;
			}
		}

		/// <summary>
		/// Whether there is a tile at this position. Check this whenever you are accessing data from a tile to avoid getting data from an empty tile.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.active" /> or <see cref="M:Terraria.Tile.active(System.Boolean)" />.
		/// </summary>
		/// <remarks>
		/// Actuated tiles are not solid, so use <see cref="P:Terraria.Tile.HasUnactuatedTile" /> instead of <see cref="P:Terraria.Tile.HasTile" /> for collision checks.<br />
		/// This only corresponds to whether a tile exists, however, a wall can exist without a tile. To check if a wall exists, use <c>tile.WallType != WallID.None</c>.
		/// </remarks>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x003FCAE8 File Offset: 0x003FACE8
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x003FCAF5 File Offset: 0x003FACF5
		public bool HasTile
		{
			get
			{
				return this.Get<TileWallWireStateData>().HasTile;
			}
			set
			{
				this.Get<TileWallWireStateData>().HasTile = value;
			}
		}

		/// <summary>
		/// Whether the tile at this position is actuated by an actuator.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.inActive" /> or <see cref="M:Terraria.Tile.inActive(System.Boolean)" />.
		/// </summary>
		/// <remarks>
		/// Actuated tiles are <strong>not</strong> solid.
		/// </remarks>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x003FCB03 File Offset: 0x003FAD03
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x003FCB10 File Offset: 0x003FAD10
		public bool IsActuated
		{
			get
			{
				return this.Get<TileWallWireStateData>().IsActuated;
			}
			set
			{
				this.Get<TileWallWireStateData>().IsActuated = value;
			}
		}

		/// <summary>
		/// Whether there is an actuator at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.actuator" /> or <see cref="M:Terraria.Tile.actuator(System.Boolean)" />.
		/// </summary>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x003FCB1E File Offset: 0x003FAD1E
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x003FCB2B File Offset: 0x003FAD2B
		public bool HasActuator
		{
			get
			{
				return this.Get<TileWallWireStateData>().HasActuator;
			}
			set
			{
				this.Get<TileWallWireStateData>().HasActuator = value;
			}
		}

		/// <summary>
		/// Whether there is a tile at this position that isn't actuated.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.nactive" />.
		/// </summary>
		/// <remarks>
		/// Actuated tiles are not solid, so use <see cref="P:Terraria.Tile.HasUnactuatedTile" /> instead of <see cref="P:Terraria.Tile.HasTile" /> for collision checks.<br />
		/// When checking if a tile exists, use <see cref="P:Terraria.Tile.HasTile" /> instead of <see cref="P:Terraria.Tile.HasUnactuatedTile" />.
		/// </remarks>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x003FCB39 File Offset: 0x003FAD39
		public bool HasUnactuatedTile
		{
			get
			{
				return this.HasTile && !this.IsActuated;
			}
		}

		/// <summary>
		/// The slope shape of the tile, which can be changed by hammering.<br />
		/// Used by <see cref="M:Terraria.WorldGen.SlopeTile(System.Int32,System.Int32,System.Int32,System.Boolean)" /> and <see cref="P:Terraria.Tile.BlockType" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.slope" /> or <see cref="M:Terraria.Tile.slope(System.Byte)" />.
		/// </summary>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x003FCB4E File Offset: 0x003FAD4E
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x003FCB5B File Offset: 0x003FAD5B
		public SlopeType Slope
		{
			get
			{
				return this.Get<TileWallWireStateData>().Slope;
			}
			set
			{
				this.Get<TileWallWireStateData>().Slope = value;
			}
		}

		/// <summary>
		/// The <see cref="P:Terraria.Tile.Slope" /> and <see cref="P:Terraria.Tile.IsHalfBlock" /> of this tile combined, which can be changed by hammering.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.blockType" />.
		/// </summary>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x003FCB69 File Offset: 0x003FAD69
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x003FCB76 File Offset: 0x003FAD76
		public BlockType BlockType
		{
			get
			{
				return this.Get<TileWallWireStateData>().BlockType;
			}
			set
			{
				this.Get<TileWallWireStateData>().BlockType = value;
			}
		}

		/// <summary>
		/// Whether a tile is a half block shape, which can be changed by hammering.<br />
		/// Used by <see cref="M:Terraria.WorldGen.PoundTile(System.Int32,System.Int32)" /> and <see cref="P:Terraria.Tile.BlockType" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.halfBrick" /> or <see cref="M:Terraria.Tile.halfBrick(System.Boolean)" />.
		/// </summary>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x003FCB84 File Offset: 0x003FAD84
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x003FCB91 File Offset: 0x003FAD91
		public bool IsHalfBlock
		{
			get
			{
				return this.Get<TileWallWireStateData>().IsHalfBlock;
			}
			set
			{
				this.Get<TileWallWireStateData>().IsHalfBlock = value;
			}
		}

		/// <summary>
		/// If the top side of a tile is sloped (<see cref="P:Terraria.Tile.Slope" />), meaning the bottom side is solid. (<see cref="F:Terraria.ID.SlopeType.SlopeDownLeft" /> or <see cref="F:Terraria.ID.SlopeType.SlopeDownRight" />).<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.topSlope" />.
		/// </summary>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x003FCB9F File Offset: 0x003FAD9F
		public bool TopSlope
		{
			get
			{
				return this.Slope == SlopeType.SlopeDownLeft || this.Slope == SlopeType.SlopeDownRight;
			}
		}

		/// <summary>
		/// If the bottom side of a tile is sloped (<see cref="P:Terraria.Tile.Slope" />), meaning the top side is solid. (<see cref="F:Terraria.ID.SlopeType.SlopeUpLeft" /> or <see cref="F:Terraria.ID.SlopeType.SlopeUpRight" />).<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.bottomSlope" />.
		/// </summary>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x003FCBB5 File Offset: 0x003FADB5
		public bool BottomSlope
		{
			get
			{
				return this.Slope == SlopeType.SlopeUpLeft || this.Slope == SlopeType.SlopeUpRight;
			}
		}

		/// <summary>
		/// If the left side of a tile is sloped (<see cref="P:Terraria.Tile.Slope" />), meaning the right side is solid. (<see cref="F:Terraria.ID.SlopeType.SlopeDownRight" /> or <see cref="F:Terraria.ID.SlopeType.SlopeUpRight" />).<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.leftSlope" />.
		/// </summary>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x003FCBCB File Offset: 0x003FADCB
		public bool LeftSlope
		{
			get
			{
				return this.Slope == SlopeType.SlopeDownRight || this.Slope == SlopeType.SlopeUpRight;
			}
		}

		/// <summary>
		/// If the right side of a tile is sloped (<see cref="P:Terraria.Tile.Slope" />), meaning the left side is solid. (<see cref="F:Terraria.ID.SlopeType.SlopeDownLeft" /> or <see cref="F:Terraria.ID.SlopeType.SlopeUpLeft" />).<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.rightSlope" />.
		/// </summary>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x003FCBE1 File Offset: 0x003FADE1
		public bool RightSlope
		{
			get
			{
				return this.Slope == SlopeType.SlopeDownLeft || this.Slope == SlopeType.SlopeUpLeft;
			}
		}

		/// <summary>
		/// The X coordinate of the top left corner of the area in the spritesheet for the <see cref="P:Terraria.Tile.TileType" /> to be used to draw the tile at this position.<para />
		/// For a Framed tile, this value is set automatically according to the framing logic as the world loads or other tiles are placed or mined nearby. See <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#framed-vs-frameimportant-tiles">Framed vs FrameImportant</see> for more info. For <see cref="F:Terraria.Main.tileFrameImportant" /> tiles, this value will not change due to tile framing and will be saved and synced in Multiplayer. In either case, <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> correspond to the coordinates of the top left corner of the area in the spritesheet corresponding to the <see cref="P:Terraria.Tile.TileType" /> that should be drawn at this position. Custom drawing logic can adjust these values.<para />
		/// Some tiles such as Christmas Tree and Weapon Rack use the higher bits of these fields to do tile-specific behaviors. Modders should not attempt to do similar approaches, but should use <see cref="T:Terraria.ModLoader.ModTileEntity" />s.<para />
		/// Legacy/vanilla equivalent is <see cref="P:Terraria.Tile.frameX" />.
		/// </summary>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x003FCBF7 File Offset: 0x003FADF7
		public ref short TileFrameX
		{
			get
			{
				return ref this.Get<TileWallWireStateData>().TileFrameX;
			}
		}

		/// <summary>
		/// The Y coordinate of the top left corner of the area in the spritesheet for the <see cref="P:Terraria.Tile.TileType" /> to be used to draw the tile at this position.<para />
		/// For a Framed tile, this value is set automatically according to the framing logic as the world loads or other tiles are placed or mined nearby. See <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#framed-vs-frameimportant-tiles">Framed vs FrameImportant</see> for more info. For <see cref="F:Terraria.Main.tileFrameImportant" /> tiles, this value will not change due to tile framing and will be saved and synced in Multiplayer. In either case, <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> correspond to the coordinates of the top left corner of the area in the spritesheet corresponding to the <see cref="P:Terraria.Tile.TileType" /> that should be drawn at this position. Custom drawing logic can adjust these values.<para />
		/// Some tiles such as Christmas Tree and Weapon Rack use the higher bits of these fields to do tile-specific behaviors. Modders should not attempt to do similar approaches, but should use <see cref="T:Terraria.ModLoader.ModTileEntity" />s.<para />
		/// Legacy/vanilla equivalent is <see cref="P:Terraria.Tile.frameY" />.
		/// </summary>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x003FCC04 File Offset: 0x003FAE04
		public ref short TileFrameY
		{
			get
			{
				return ref this.Get<TileWallWireStateData>().TileFrameY;
			}
		}

		/// <summary>
		/// The X coordinate of the top left corner of the area in the spritesheet for the <see cref="P:Terraria.Tile.WallType" /> to be used to draw the wall at this position.<para />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wallFrameX" /> or <see cref="M:Terraria.Tile.wallFrameX(System.Int32)" />.
		/// </summary>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x003FCC11 File Offset: 0x003FAE11
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x003FCC1E File Offset: 0x003FAE1E
		public int WallFrameX
		{
			get
			{
				return this.Get<TileWallWireStateData>().WallFrameX;
			}
			set
			{
				this.Get<TileWallWireStateData>().WallFrameX = value;
			}
		}

		/// <summary>
		/// The Y coordinate of the top left corner of the area in the spritesheet for the <see cref="P:Terraria.Tile.WallType" /> to be used to draw the wall at this position.<para />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wallFrameY" /> or <see cref="M:Terraria.Tile.wallFrameY(System.Int32)" />.
		/// </summary>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x003FCC2C File Offset: 0x003FAE2C
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x003FCC39 File Offset: 0x003FAE39
		public int WallFrameY
		{
			get
			{
				return this.Get<TileWallWireStateData>().WallFrameY;
			}
			set
			{
				this.Get<TileWallWireStateData>().WallFrameY = value;
			}
		}

		/// <summary>
		/// The random style number the tile at this position has, which is random number between 0 and 2 (inclusive).<br />
		/// This is used in non-<see cref="F:Terraria.Main.tileFrameImportant" /> tiles (aka "Terrain" tiles) to provide visual variation and is not synced in multiplayer nor will it be preserved when saving and loading the world.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.frameNumber" /> or <see cref="M:Terraria.Tile.frameNumber(System.Byte)" />.
		/// </summary>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x003FCC47 File Offset: 0x003FAE47
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x003FCC54 File Offset: 0x003FAE54
		public int TileFrameNumber
		{
			get
			{
				return this.Get<TileWallWireStateData>().TileFrameNumber;
			}
			set
			{
				this.Get<TileWallWireStateData>().TileFrameNumber = value;
			}
		}

		/// <summary>
		/// The random style number the wall at this position has, which is a random number between 0 and 2 (inclusive).<br />
		/// This is used to provide visual variation and is not synced in multiplayer nor will it be preserved when saving and loading the world.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wallFrameNumber" /> or <see cref="M:Terraria.Tile.wallFrameNumber(System.Byte)" />.
		/// </summary>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x003FCC62 File Offset: 0x003FAE62
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x003FCC6F File Offset: 0x003FAE6F
		public int WallFrameNumber
		{
			get
			{
				return this.Get<TileWallWireStateData>().WallFrameNumber;
			}
			set
			{
				this.Get<TileWallWireStateData>().WallFrameNumber = value;
			}
		}

		/// <summary>
		/// The <see cref="T:Terraria.ID.PaintID" /> the tile at this position is painted with. Is <see cref="F:Terraria.ID.PaintID.None" /> if not painted.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.color" /> or <see cref="M:Terraria.Tile.color(System.Byte)" />.
		/// </summary>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x003FCC7D File Offset: 0x003FAE7D
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x003FCC8A File Offset: 0x003FAE8A
		public byte TileColor
		{
			get
			{
				return this.Get<TileWallWireStateData>().TileColor;
			}
			set
			{
				this.Get<TileWallWireStateData>().TileColor = value;
			}
		}

		/// <summary>
		/// The <see cref="T:Terraria.ID.PaintID" /> the wall at this position is painted with. Is <see cref="F:Terraria.ID.PaintID.None" /> if not painted.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wallColor" /> or <see cref="M:Terraria.Tile.wallColor(System.Byte)" />.
		/// </summary>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x003FCC98 File Offset: 0x003FAE98
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x003FCCA5 File Offset: 0x003FAEA5
		public byte WallColor
		{
			get
			{
				return this.Get<TileWallWireStateData>().WallColor;
			}
			set
			{
				this.Get<TileWallWireStateData>().WallColor = value;
			}
		}

		/// <summary>
		/// The amount of liquid at this position.<br />
		/// Ranges from 0, no liquid, to 255, filled with liquid.<br />
		/// Legacy/vanilla equivalent is <see cref="P:Terraria.Tile.liquid" />.
		/// </summary>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x003FCCB3 File Offset: 0x003FAEB3
		public ref byte LiquidAmount
		{
			get
			{
				return ref this.Get<LiquidData>().Amount;
			}
		}

		/// <summary>
		/// The <see cref="T:Terraria.ID.LiquidID" /> of the liquid at this position.<br />
		/// Make sure to check that <see cref="P:Terraria.Tile.LiquidAmount" /> is greater than 0.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.liquidType" /> or <see cref="M:Terraria.Tile.liquidType(System.Int32)" />.
		/// </summary>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x003FCCC0 File Offset: 0x003FAEC0
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x003FCCCD File Offset: 0x003FAECD
		public int LiquidType
		{
			get
			{
				return this.Get<LiquidData>().LiquidType;
			}
			set
			{
				this.Get<LiquidData>().LiquidType = value;
			}
		}

		/// <summary>
		/// Whether the liquid at this position should skip updating for 1 tick.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.skipLiquid" /> or <see cref="M:Terraria.Tile.skipLiquid(System.Boolean)" />.
		/// </summary>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x003FCCDB File Offset: 0x003FAEDB
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x003FCCE8 File Offset: 0x003FAEE8
		public bool SkipLiquid
		{
			get
			{
				return this.Get<LiquidData>().SkipLiquid;
			}
			set
			{
				this.Get<LiquidData>().SkipLiquid = value;
			}
		}

		/// <summary>
		/// Whether there is liquid at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.checkingLiquid" /> or <see cref="M:Terraria.Tile.checkingLiquid(System.Boolean)" />.
		/// </summary>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x003FCCF6 File Offset: 0x003FAEF6
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x003FCD03 File Offset: 0x003FAF03
		public bool CheckingLiquid
		{
			get
			{
				return this.Get<LiquidData>().CheckingLiquid;
			}
			set
			{
				this.Get<LiquidData>().CheckingLiquid = value;
			}
		}

		/// <summary>
		/// Whether there is red wire at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wire" /> or <see cref="M:Terraria.Tile.wire(System.Boolean)" />.
		/// </summary>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x003FCD11 File Offset: 0x003FAF11
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x003FCD1E File Offset: 0x003FAF1E
		public bool RedWire
		{
			get
			{
				return this.Get<TileWallWireStateData>().RedWire;
			}
			set
			{
				this.Get<TileWallWireStateData>().RedWire = value;
			}
		}

		/// <summary>
		/// Whether there is green wire at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wire3" /> or <see cref="M:Terraria.Tile.wire3(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x003FCD2C File Offset: 0x003FAF2C
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x003FCD39 File Offset: 0x003FAF39
		public bool GreenWire
		{
			get
			{
				return this.Get<TileWallWireStateData>().GreenWire;
			}
			set
			{
				this.Get<TileWallWireStateData>().GreenWire = value;
			}
		}

		/// <summary>
		/// Whether there is blue wire at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wire2" /> or <see cref="M:Terraria.Tile.wire2(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x003FCD47 File Offset: 0x003FAF47
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x003FCD54 File Offset: 0x003FAF54
		public bool BlueWire
		{
			get
			{
				return this.Get<TileWallWireStateData>().BlueWire;
			}
			set
			{
				this.Get<TileWallWireStateData>().BlueWire = value;
			}
		}

		/// <summary>
		/// Whether there is yellow wire at this position.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.wire4" /> or <see cref="M:Terraria.Tile.wire4(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x003FCD62 File Offset: 0x003FAF62
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x003FCD6F File Offset: 0x003FAF6F
		public bool YellowWire
		{
			get
			{
				return this.Get<TileWallWireStateData>().YellowWire;
			}
			set
			{
				this.Get<TileWallWireStateData>().YellowWire = value;
			}
		}

		/// <summary>
		/// Whether the tile at this position is invisible. Used by <see cref="F:Terraria.ID.ItemID.EchoCoating" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.invisibleBlock" /> or <see cref="M:Terraria.Tile.invisibleBlock(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x003FCD7D File Offset: 0x003FAF7D
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x003FCD8A File Offset: 0x003FAF8A
		public bool IsTileInvisible
		{
			get
			{
				return this.Get<TileWallBrightnessInvisibilityData>().IsTileInvisible;
			}
			set
			{
				this.Get<TileWallBrightnessInvisibilityData>().IsTileInvisible = value;
			}
		}

		/// <summary>
		/// Whether the wall at this position is invisible. Used by <see cref="F:Terraria.ID.ItemID.EchoCoating" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.invisibleWall" /> or <see cref="M:Terraria.Tile.invisibleWall(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x003FCD98 File Offset: 0x003FAF98
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x003FCDA5 File Offset: 0x003FAFA5
		public bool IsWallInvisible
		{
			get
			{
				return this.Get<TileWallBrightnessInvisibilityData>().IsWallInvisible;
			}
			set
			{
				this.Get<TileWallBrightnessInvisibilityData>().IsWallInvisible = value;
			}
		}

		/// <summary>
		/// Whether the tile at this position is fully illuminated. Used by <see cref="F:Terraria.ID.ItemID.GlowPaint" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.fullbrightBlock" /> or <see cref="M:Terraria.Tile.fullbrightBlock(System.Boolean)" />.
		/// </summary>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x003FCDB3 File Offset: 0x003FAFB3
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x003FCDC0 File Offset: 0x003FAFC0
		public bool IsTileFullbright
		{
			get
			{
				return this.Get<TileWallBrightnessInvisibilityData>().IsTileFullbright;
			}
			set
			{
				this.Get<TileWallBrightnessInvisibilityData>().IsTileFullbright = value;
			}
		}

		/// <summary>
		/// Whether the wall at this position is fully illuminated. Used by <see cref="F:Terraria.ID.ItemID.GlowPaint" />.<br />
		/// Legacy/vanilla equivalent is <see cref="M:Terraria.Tile.fullbrightWall" /> or <see cref="M:Terraria.Tile.fullbrightWall(System.Boolean)" />.
		/// </summary>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x003FCDCE File Offset: 0x003FAFCE
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x003FCDDB File Offset: 0x003FAFDB
		public bool IsWallFullbright
		{
			get
			{
				return this.Get<TileWallBrightnessInvisibilityData>().IsWallFullbright;
			}
			set
			{
				this.Get<TileWallBrightnessInvisibilityData>().IsWallFullbright = value;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x003FCDE9 File Offset: 0x003FAFE9
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		internal Tile(uint tileId)
		{
			this.TileId = tileId;
		}

		/// <summary>
		/// Used to get a reference to the <see cref="T:Terraria.ITileData" /> at this position.
		/// </summary>
		/// <typeparam name="T">The <see cref="T:Terraria.ITileData" /> to get.</typeparam>
		/// <returns>The <see cref="T:Terraria.ITileData" /> of type <typeparamref name="T" /> at this position.</returns>
		// Token: 0x06000F69 RID: 3945 RVA: 0x003FCDF2 File Offset: 0x003FAFF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T Get<[IsUnmanaged] T>() where T : struct, ValueType, ITileData
		{
			return ref TileData<T>.ptr[(ulong)this.TileId * (ulong)((long)sizeof(T)) / (ulong)sizeof(T)];
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x003FCE0A File Offset: 0x003FB00A
		public override int GetHashCode()
		{
			return (int)this.TileId;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x003FCE12 File Offset: 0x003FB012
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Tile tile, Tile tile2)
		{
			return tile.TileId == tile2.TileId;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x003FCE22 File Offset: 0x003FB022
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Tile tile, Tile tile2)
		{
			return tile.TileId != tile2.TileId;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x003FCE35 File Offset: 0x003FB035
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Tile tile, ArgumentException justSoYouCanCompareWithNull)
		{
			return false;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x003FCE38 File Offset: 0x003FB038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Tile tile, ArgumentException justSoYouCanCompareWithNull)
		{
			return true;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TileType" /> instead.
		/// </summary>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x003FCE3B File Offset: 0x003FB03B
		internal ref ushort type
		{
			get
			{
				return this.TileType;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.WallType" /> instead.
		/// </summary>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x003FCE43 File Offset: 0x003FB043
		internal ref ushort wall
		{
			get
			{
				return this.WallType;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.HasTile" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F71 RID: 3953 RVA: 0x003FCE4B File Offset: 0x003FB04B
		internal bool active()
		{
			return this.HasTile;
		}

		/// <inheritdoc cref="M:Terraria.Tile.active" />
		// Token: 0x06000F72 RID: 3954 RVA: 0x003FCE53 File Offset: 0x003FB053
		internal void active(bool active)
		{
			this.HasTile = active;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsActuated" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F73 RID: 3955 RVA: 0x003FCE5C File Offset: 0x003FB05C
		internal bool inActive()
		{
			return this.IsActuated;
		}

		/// <inheritdoc cref="M:Terraria.Tile.inActive" />
		// Token: 0x06000F74 RID: 3956 RVA: 0x003FCE64 File Offset: 0x003FB064
		internal void inActive(bool inActive)
		{
			this.IsActuated = inActive;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.HasActuator" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F75 RID: 3957 RVA: 0x003FCE6D File Offset: 0x003FB06D
		internal bool actuator()
		{
			return this.HasActuator;
		}

		/// <inheritdoc cref="M:Terraria.Tile.actuator" />
		// Token: 0x06000F76 RID: 3958 RVA: 0x003FCE75 File Offset: 0x003FB075
		internal void actuator(bool actuator)
		{
			this.HasActuator = actuator;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.HasUnactuatedTile" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F77 RID: 3959 RVA: 0x003FCE7E File Offset: 0x003FB07E
		internal bool nactive()
		{
			return this.HasUnactuatedTile;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.Slope" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F78 RID: 3960 RVA: 0x003FCE86 File Offset: 0x003FB086
		internal byte slope()
		{
			return (byte)this.Slope;
		}

		/// <inheritdoc cref="M:Terraria.Tile.slope" />
		// Token: 0x06000F79 RID: 3961 RVA: 0x003FCE8F File Offset: 0x003FB08F
		internal void slope(byte slope)
		{
			this.Slope = (SlopeType)slope;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsHalfBlock" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F7A RID: 3962 RVA: 0x003FCE98 File Offset: 0x003FB098
		internal bool halfBrick()
		{
			return this.IsHalfBlock;
		}

		/// <inheritdoc cref="M:Terraria.Tile.halfBrick" />
		// Token: 0x06000F7B RID: 3963 RVA: 0x003FCEA0 File Offset: 0x003FB0A0
		internal void halfBrick(bool halfBrick)
		{
			this.IsHalfBlock = halfBrick;
		}

		/// <summary>
		/// Legacy code, use <c>tile1.Slope == tile2.Slope</c> instead.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		// Token: 0x06000F7C RID: 3964 RVA: 0x003FCEA9 File Offset: 0x003FB0A9
		internal bool HasSameSlope(Tile tile)
		{
			return this.Slope == tile.Slope;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TileFrameX" /> instead.
		/// </summary>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x003FCEBA File Offset: 0x003FB0BA
		internal ref short frameX
		{
			get
			{
				return this.TileFrameX;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TileFrameY" /> instead.
		/// </summary>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x003FCEC2 File Offset: 0x003FB0C2
		internal ref short frameY
		{
			get
			{
				return this.TileFrameY;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.WallFrameX" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F7F RID: 3967 RVA: 0x003FCECA File Offset: 0x003FB0CA
		internal int wallFrameX()
		{
			return this.WallFrameX;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wallFrameX" />
		// Token: 0x06000F80 RID: 3968 RVA: 0x003FCED2 File Offset: 0x003FB0D2
		internal void wallFrameX(int wallFrameX)
		{
			this.WallFrameX = wallFrameX;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.WallFrameY" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F81 RID: 3969 RVA: 0x003FCEDB File Offset: 0x003FB0DB
		internal int wallFrameY()
		{
			return this.WallFrameY;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wallFrameY" />
		// Token: 0x06000F82 RID: 3970 RVA: 0x003FCEE3 File Offset: 0x003FB0E3
		internal void wallFrameY(int wallFrameY)
		{
			this.WallFrameY = wallFrameY;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TileFrameNumber" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F83 RID: 3971 RVA: 0x003FCEEC File Offset: 0x003FB0EC
		internal byte frameNumber()
		{
			return (byte)this.TileFrameNumber;
		}

		/// <inheritdoc cref="M:Terraria.Tile.frameNumber" />
		// Token: 0x06000F84 RID: 3972 RVA: 0x003FCEF5 File Offset: 0x003FB0F5
		internal void frameNumber(byte frameNumber)
		{
			this.TileFrameNumber = (int)frameNumber;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.WallFrameNumber" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F85 RID: 3973 RVA: 0x003FCEFE File Offset: 0x003FB0FE
		internal byte wallFrameNumber()
		{
			return (byte)this.WallFrameNumber;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wallFrameNumber" />
		// Token: 0x06000F86 RID: 3974 RVA: 0x003FCF07 File Offset: 0x003FB107
		internal void wallFrameNumber(byte wallFrameNumber)
		{
			this.WallFrameNumber = (int)wallFrameNumber;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.TileColor" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F87 RID: 3975 RVA: 0x003FCF10 File Offset: 0x003FB110
		internal byte color()
		{
			return this.TileColor;
		}

		/// <inheritdoc cref="M:Terraria.Tile.color" />
		// Token: 0x06000F88 RID: 3976 RVA: 0x003FCF18 File Offset: 0x003FB118
		internal void color(byte color)
		{
			this.TileColor = color;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.WallColor" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F89 RID: 3977 RVA: 0x003FCF21 File Offset: 0x003FB121
		internal byte wallColor()
		{
			return this.WallColor;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wallColor" />
		// Token: 0x06000F8A RID: 3978 RVA: 0x003FCF29 File Offset: 0x003FB129
		internal void wallColor(byte wallColor)
		{
			this.WallColor = wallColor;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.LiquidAmount" /> instead.
		/// </summary>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x003FCF32 File Offset: 0x003FB132
		internal ref byte liquid
		{
			get
			{
				return this.LiquidAmount;
			}
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.LiquidType" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F8C RID: 3980 RVA: 0x003FCF3A File Offset: 0x003FB13A
		internal byte liquidType()
		{
			return (byte)this.LiquidType;
		}

		/// <inheritdoc cref="M:Terraria.Tile.liquidType" />
		// Token: 0x06000F8D RID: 3981 RVA: 0x003FCF43 File Offset: 0x003FB143
		internal void liquidType(int liquidType)
		{
			this.LiquidType = liquidType;
		}

		/// <summary>
		/// Legacy code, use <c>tile.LiquidType == LiquidID.Lava</c> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F8E RID: 3982 RVA: 0x003FCF4C File Offset: 0x003FB14C
		internal bool lava()
		{
			return this.LiquidType == 1;
		}

		/// <inheritdoc cref="M:Terraria.Tile.lava" />
		// Token: 0x06000F8F RID: 3983 RVA: 0x003FCF57 File Offset: 0x003FB157
		internal void lava(bool lava)
		{
			this.SetIsLiquidType(1, lava);
		}

		/// <summary>
		/// Legacy code, use <c>tile.LiquidType == LiquidID.Honey</c> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F90 RID: 3984 RVA: 0x003FCF61 File Offset: 0x003FB161
		internal bool honey()
		{
			return this.LiquidType == 2;
		}

		/// <inheritdoc cref="M:Terraria.Tile.honey" />
		// Token: 0x06000F91 RID: 3985 RVA: 0x003FCF6C File Offset: 0x003FB16C
		internal void honey(bool honey)
		{
			this.SetIsLiquidType(2, honey);
		}

		/// <summary>
		/// Legacy code, use <c>tile.LiquidType == LiquidID.Shimmer</c> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F92 RID: 3986 RVA: 0x003FCF76 File Offset: 0x003FB176
		internal bool shimmer()
		{
			return this.LiquidType == 3;
		}

		/// <inheritdoc cref="M:Terraria.Tile.shimmer" />
		// Token: 0x06000F93 RID: 3987 RVA: 0x003FCF81 File Offset: 0x003FB181
		internal void shimmer(bool shimmer)
		{
			this.SetIsLiquidType(3, shimmer);
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.SkipLiquid" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F94 RID: 3988 RVA: 0x003FCF8B File Offset: 0x003FB18B
		internal bool skipLiquid()
		{
			return this.SkipLiquid;
		}

		/// <inheritdoc cref="M:Terraria.Tile.skipLiquid" />
		// Token: 0x06000F95 RID: 3989 RVA: 0x003FCF93 File Offset: 0x003FB193
		internal void skipLiquid(bool skipLiquid)
		{
			this.SkipLiquid = skipLiquid;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.CheckingLiquid" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F96 RID: 3990 RVA: 0x003FCF9C File Offset: 0x003FB19C
		internal bool checkingLiquid()
		{
			return this.CheckingLiquid;
		}

		/// <inheritdoc cref="M:Terraria.Tile.checkingLiquid" />
		// Token: 0x06000F97 RID: 3991 RVA: 0x003FCFA4 File Offset: 0x003FB1A4
		internal void checkingLiquid(bool checkingLiquid)
		{
			this.CheckingLiquid = checkingLiquid;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.RedWire" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F98 RID: 3992 RVA: 0x003FCFAD File Offset: 0x003FB1AD
		internal bool wire()
		{
			return this.RedWire;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wire" />
		// Token: 0x06000F99 RID: 3993 RVA: 0x003FCFB5 File Offset: 0x003FB1B5
		internal void wire(bool wire)
		{
			this.RedWire = wire;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.BlueWire" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F9A RID: 3994 RVA: 0x003FCFBE File Offset: 0x003FB1BE
		internal bool wire2()
		{
			return this.BlueWire;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wire2" />
		// Token: 0x06000F9B RID: 3995 RVA: 0x003FCFC6 File Offset: 0x003FB1C6
		internal void wire2(bool wire2)
		{
			this.BlueWire = wire2;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.GreenWire" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F9C RID: 3996 RVA: 0x003FCFCF File Offset: 0x003FB1CF
		internal bool wire3()
		{
			return this.GreenWire;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wire3" />
		// Token: 0x06000F9D RID: 3997 RVA: 0x003FCFD7 File Offset: 0x003FB1D7
		internal void wire3(bool wire3)
		{
			this.GreenWire = wire3;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.YellowWire" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000F9E RID: 3998 RVA: 0x003FCFE0 File Offset: 0x003FB1E0
		internal bool wire4()
		{
			return this.YellowWire;
		}

		/// <inheritdoc cref="M:Terraria.Tile.wire4" />
		// Token: 0x06000F9F RID: 3999 RVA: 0x003FCFE8 File Offset: 0x003FB1E8
		internal void wire4(bool wire4)
		{
			this.YellowWire = wire4;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsTileInvisible" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000FA0 RID: 4000 RVA: 0x003FCFF1 File Offset: 0x003FB1F1
		internal bool invisibleBlock()
		{
			return this.IsTileInvisible;
		}

		/// <inheritdoc cref="M:Terraria.Tile.invisibleBlock" />
		// Token: 0x06000FA1 RID: 4001 RVA: 0x003FCFF9 File Offset: 0x003FB1F9
		internal void invisibleBlock(bool invisibleBlock)
		{
			this.IsTileInvisible = invisibleBlock;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsWallInvisible" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000FA2 RID: 4002 RVA: 0x003FD002 File Offset: 0x003FB202
		internal bool invisibleWall()
		{
			return this.IsWallInvisible;
		}

		/// <inheritdoc cref="M:Terraria.Tile.invisibleWall" />
		// Token: 0x06000FA3 RID: 4003 RVA: 0x003FD00A File Offset: 0x003FB20A
		internal void invisibleWall(bool invisibleWall)
		{
			this.IsWallInvisible = invisibleWall;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsTileFullbright" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000FA4 RID: 4004 RVA: 0x003FD013 File Offset: 0x003FB213
		internal bool fullbrightBlock()
		{
			return this.IsTileFullbright;
		}

		/// <inheritdoc cref="M:Terraria.Tile.fullbrightBlock" />
		// Token: 0x06000FA5 RID: 4005 RVA: 0x003FD01B File Offset: 0x003FB21B
		internal void fullbrightBlock(bool fullbrightBlock)
		{
			this.IsTileFullbright = fullbrightBlock;
		}

		/// <summary>
		/// Legacy code, use <see cref="P:Terraria.Tile.IsWallFullbright" /> instead.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x003FD024 File Offset: 0x003FB224
		internal bool fullbrightWall()
		{
			return this.IsWallFullbright;
		}

		/// <inheritdoc cref="M:Terraria.Tile.fullbrightWall" />
		// Token: 0x06000FA7 RID: 4007 RVA: 0x003FD02C File Offset: 0x003FB22C
		internal void fullbrightWall(bool fullbrightWall)
		{
			this.IsWallFullbright = fullbrightWall;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x003FD035 File Offset: 0x003FB235
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetIsLiquidType(int liquidId, bool value)
		{
			if (value)
			{
				this.LiquidType = liquidId;
				return;
			}
			if (this.LiquidType == liquidId)
			{
				this.LiquidType = 0;
			}
		}

		// Token: 0x04000EC9 RID: 3785
		internal readonly uint TileId;
	}
}
