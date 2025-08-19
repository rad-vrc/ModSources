using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x02000045 RID: 69
	public class Tile
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0033D308 File Offset: 0x0033B508
		public Tile()
		{
			this.type = 0;
			this.wall = 0;
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0033D35C File Offset: 0x0033B55C
		public Tile(Tile copy)
		{
			if (copy == null)
			{
				this.type = 0;
				this.wall = 0;
				this.liquid = 0;
				this.sTileHeader = 0;
				this.bTileHeader = 0;
				this.bTileHeader2 = 0;
				this.bTileHeader3 = 0;
				this.frameX = 0;
				this.frameY = 0;
				return;
			}
			this.type = copy.type;
			this.wall = copy.wall;
			this.liquid = copy.liquid;
			this.sTileHeader = copy.sTileHeader;
			this.bTileHeader = copy.bTileHeader;
			this.bTileHeader2 = copy.bTileHeader2;
			this.bTileHeader3 = copy.bTileHeader3;
			this.frameX = copy.frameX;
			this.frameY = copy.frameY;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00005D11 File Offset: 0x00003F11
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0033D420 File Offset: 0x0033B620
		public void ClearEverything()
		{
			this.type = 0;
			this.wall = 0;
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0033D46C File Offset: 0x0033B66C
		public void ClearTile()
		{
			this.slope(0);
			this.halfBrick(false);
			this.active(false);
			this.inActive(false);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0033D48C File Offset: 0x0033B68C
		public void CopyFrom(Tile from)
		{
			this.type = from.type;
			this.wall = from.wall;
			this.liquid = from.liquid;
			this.sTileHeader = from.sTileHeader;
			this.bTileHeader = from.bTileHeader;
			this.bTileHeader2 = from.bTileHeader2;
			this.bTileHeader3 = from.bTileHeader3;
			this.frameX = from.frameX;
			this.frameY = from.frameY;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0033D508 File Offset: 0x0033B708
		public int collisionType
		{
			get
			{
				if (!this.active())
				{
					return 0;
				}
				if (this.halfBrick())
				{
					return 2;
				}
				if (this.slope() > 0)
				{
					return (int)(2 + this.slope());
				}
				if (Main.tileSolid[(int)this.type] && !Main.tileSolidTop[(int)this.type])
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0033D55C File Offset: 0x0033B75C
		public bool isTheSameAs(Tile compTile)
		{
			if (compTile == null)
			{
				return false;
			}
			if (this.sTileHeader != compTile.sTileHeader)
			{
				return false;
			}
			if (this.active())
			{
				if (this.type != compTile.type)
				{
					return false;
				}
				if (Main.tileFrameImportant[(int)this.type] && (this.frameX != compTile.frameX || this.frameY != compTile.frameY))
				{
					return false;
				}
			}
			if (this.wall != compTile.wall || this.liquid != compTile.liquid)
			{
				return false;
			}
			if (compTile.liquid == 0)
			{
				if (this.wallColor() != compTile.wallColor())
				{
					return false;
				}
				if (this.wire4() != compTile.wire4())
				{
					return false;
				}
			}
			else if (this.bTileHeader != compTile.bTileHeader)
			{
				return false;
			}
			return this.invisibleBlock() == compTile.invisibleBlock() && this.invisibleWall() == compTile.invisibleWall() && this.fullbrightBlock() == compTile.fullbrightBlock() && this.fullbrightWall() == compTile.fullbrightWall();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0033D654 File Offset: 0x0033B854
		public int blockType()
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

		// Token: 0x060009C0 RID: 2496 RVA: 0x0033D67B File Offset: 0x0033B87B
		public void liquidType(int liquidType)
		{
			if (liquidType == 0)
			{
				this.bTileHeader &= 159;
				return;
			}
			if (liquidType == 1)
			{
				this.lava(true);
				return;
			}
			if (liquidType == 2)
			{
				this.honey(true);
				return;
			}
			if (liquidType == 3)
			{
				this.shimmer(true);
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0033D6B7 File Offset: 0x0033B8B7
		public byte liquidType()
		{
			return (byte)((this.bTileHeader & 96) >> 5);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0033D6C5 File Offset: 0x0033B8C5
		public bool nactive()
		{
			return (this.sTileHeader & 96) == 32;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0033D6D7 File Offset: 0x0033B8D7
		public void ResetToType(ushort type)
		{
			this.liquid = 0;
			this.sTileHeader = 32;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
			this.type = type;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0033D712 File Offset: 0x0033B912
		internal void ClearMetadata()
		{
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0033D748 File Offset: 0x0033B948
		public Color actColor(Color oldColor)
		{
			if (!this.inActive())
			{
				return oldColor;
			}
			double num = 0.4;
			return new Color((int)((byte)(num * (double)oldColor.R)), (int)((byte)(num * (double)oldColor.G)), (int)((byte)(num * (double)oldColor.B)), (int)oldColor.A);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0033D796 File Offset: 0x0033B996
		public void actColor(ref Vector3 oldColor)
		{
			if (!this.inActive())
			{
				return;
			}
			oldColor *= 0.4f;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0033D7B8 File Offset: 0x0033B9B8
		public bool topSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 2;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0033D7D8 File Offset: 0x0033B9D8
		public bool bottomSlope()
		{
			byte b = this.slope();
			return b == 3 || b == 4;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0033D7F8 File Offset: 0x0033B9F8
		public bool leftSlope()
		{
			byte b = this.slope();
			return b == 2 || b == 4;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0033D818 File Offset: 0x0033BA18
		public bool rightSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 3;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0033D836 File Offset: 0x0033BA36
		public bool HasSameSlope(Tile tile)
		{
			return (this.sTileHeader & 29696) == (tile.sTileHeader & 29696);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0033D852 File Offset: 0x0033BA52
		public byte wallColor()
		{
			return this.bTileHeader & 31;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0033D85E File Offset: 0x0033BA5E
		public void wallColor(byte wallColor)
		{
			this.bTileHeader = ((this.bTileHeader & 224) | wallColor);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0033D875 File Offset: 0x0033BA75
		public bool lava()
		{
			return (this.bTileHeader & 96) == 32;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0033D884 File Offset: 0x0033BA84
		public void lava(bool lava)
		{
			if (lava)
			{
				this.bTileHeader = ((this.bTileHeader & 159) | 32);
				return;
			}
			this.bTileHeader &= 223;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0033D8B3 File Offset: 0x0033BAB3
		public bool honey()
		{
			return (this.bTileHeader & 96) == 64;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0033D8C2 File Offset: 0x0033BAC2
		public void honey(bool honey)
		{
			if (honey)
			{
				this.bTileHeader = ((this.bTileHeader & 159) | 64);
				return;
			}
			this.bTileHeader &= 191;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0033D8F1 File Offset: 0x0033BAF1
		public bool shimmer()
		{
			return (this.bTileHeader & 96) == 96;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0033D900 File Offset: 0x0033BB00
		public void shimmer(bool shimmer)
		{
			if (shimmer)
			{
				this.bTileHeader = ((this.bTileHeader & 159) | 96);
				return;
			}
			this.bTileHeader &= 159;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0033D92F File Offset: 0x0033BB2F
		public bool wire4()
		{
			return (this.bTileHeader & 128) == 128;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0033D944 File Offset: 0x0033BB44
		public void wire4(bool wire4)
		{
			if (wire4)
			{
				this.bTileHeader |= 128;
				return;
			}
			this.bTileHeader &= 127;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0033D96D File Offset: 0x0033BB6D
		public int wallFrameX()
		{
			return (int)((this.bTileHeader2 & 15) * 36);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0033D97B File Offset: 0x0033BB7B
		public void wallFrameX(int wallFrameX)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 240) | (wallFrameX / 36 & 15));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0033D998 File Offset: 0x0033BB98
		public byte frameNumber()
		{
			return (byte)((this.bTileHeader2 & 48) >> 4);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0033D9A6 File Offset: 0x0033BBA6
		public void frameNumber(byte frameNumber)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 207) | (int)(frameNumber & 3) << 4);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0033D9C1 File Offset: 0x0033BBC1
		public byte wallFrameNumber()
		{
			return (byte)((this.bTileHeader2 & 192) >> 6);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0033D9D2 File Offset: 0x0033BBD2
		public void wallFrameNumber(byte wallFrameNumber)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 63) | (int)(wallFrameNumber & 3) << 6);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0033D9EA File Offset: 0x0033BBEA
		public int wallFrameY()
		{
			return (int)((this.bTileHeader3 & 7) * 36);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0033D9F7 File Offset: 0x0033BBF7
		public void wallFrameY(int wallFrameY)
		{
			this.bTileHeader3 = (byte)((int)(this.bTileHeader3 & 248) | (wallFrameY / 36 & 7));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0033DA13 File Offset: 0x0033BC13
		public bool checkingLiquid()
		{
			return (this.bTileHeader3 & 8) == 8;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0033DA20 File Offset: 0x0033BC20
		public void checkingLiquid(bool checkingLiquid)
		{
			if (checkingLiquid)
			{
				this.bTileHeader3 |= 8;
				return;
			}
			this.bTileHeader3 &= 247;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0033DA48 File Offset: 0x0033BC48
		public bool skipLiquid()
		{
			return (this.bTileHeader3 & 16) == 16;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0033DA57 File Offset: 0x0033BC57
		public void skipLiquid(bool skipLiquid)
		{
			if (skipLiquid)
			{
				this.bTileHeader3 |= 16;
				return;
			}
			this.bTileHeader3 &= 239;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0033DA80 File Offset: 0x0033BC80
		public bool invisibleBlock()
		{
			return (this.bTileHeader3 & 32) == 32;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0033DA8F File Offset: 0x0033BC8F
		public void invisibleBlock(bool invisibleBlock)
		{
			if (invisibleBlock)
			{
				this.bTileHeader3 |= 32;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -33);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0033DAB5 File Offset: 0x0033BCB5
		public bool invisibleWall()
		{
			return (this.bTileHeader3 & 64) == 64;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0033DAC4 File Offset: 0x0033BCC4
		public void invisibleWall(bool invisibleWall)
		{
			if (invisibleWall)
			{
				this.bTileHeader3 |= 64;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -65);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0033DAEA File Offset: 0x0033BCEA
		public bool fullbrightBlock()
		{
			return (this.bTileHeader3 & 128) == 128;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0033DAFF File Offset: 0x0033BCFF
		public void fullbrightBlock(bool fullbrightBlock)
		{
			if (fullbrightBlock)
			{
				this.bTileHeader3 |= 128;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -129);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0033DB2B File Offset: 0x0033BD2B
		public byte color()
		{
			return (byte)(this.sTileHeader & 31);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0033DB37 File Offset: 0x0033BD37
		public void color(byte color)
		{
			this.sTileHeader = ((this.sTileHeader & 65504) | (ushort)color);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0033DB4E File Offset: 0x0033BD4E
		public bool active()
		{
			return (this.sTileHeader & 32) == 32;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0033DB5D File Offset: 0x0033BD5D
		public void active(bool active)
		{
			if (active)
			{
				this.sTileHeader |= 32;
				return;
			}
			this.sTileHeader &= 65503;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0033DB86 File Offset: 0x0033BD86
		public bool inActive()
		{
			return (this.sTileHeader & 64) == 64;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0033DB95 File Offset: 0x0033BD95
		public void inActive(bool inActive)
		{
			if (inActive)
			{
				this.sTileHeader |= 64;
				return;
			}
			this.sTileHeader &= 65471;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0033DBBE File Offset: 0x0033BDBE
		public bool wire()
		{
			return (this.sTileHeader & 128) == 128;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0033DBD3 File Offset: 0x0033BDD3
		public void wire(bool wire)
		{
			if (wire)
			{
				this.sTileHeader |= 128;
				return;
			}
			this.sTileHeader &= 65407;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0033DBFF File Offset: 0x0033BDFF
		public bool wire2()
		{
			return (this.sTileHeader & 256) == 256;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0033DC14 File Offset: 0x0033BE14
		public void wire2(bool wire2)
		{
			if (wire2)
			{
				this.sTileHeader |= 256;
				return;
			}
			this.sTileHeader &= 65279;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0033DC40 File Offset: 0x0033BE40
		public bool wire3()
		{
			return (this.sTileHeader & 512) == 512;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0033DC55 File Offset: 0x0033BE55
		public void wire3(bool wire3)
		{
			if (wire3)
			{
				this.sTileHeader |= 512;
				return;
			}
			this.sTileHeader &= 65023;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0033DC81 File Offset: 0x0033BE81
		public bool halfBrick()
		{
			return (this.sTileHeader & 1024) == 1024;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0033DC96 File Offset: 0x0033BE96
		public void halfBrick(bool halfBrick)
		{
			if (halfBrick)
			{
				this.sTileHeader |= 1024;
				return;
			}
			this.sTileHeader &= 64511;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0033DCC2 File Offset: 0x0033BEC2
		public bool actuator()
		{
			return (this.sTileHeader & 2048) == 2048;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0033DCD7 File Offset: 0x0033BED7
		public void actuator(bool actuator)
		{
			if (actuator)
			{
				this.sTileHeader |= 2048;
				return;
			}
			this.sTileHeader &= 63487;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0033DD03 File Offset: 0x0033BF03
		public byte slope()
		{
			return (byte)((this.sTileHeader & 28672) >> 12);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0033DD15 File Offset: 0x0033BF15
		public void slope(byte slope)
		{
			this.sTileHeader = (ushort)((int)(this.sTileHeader & 36863) | (int)(slope & 7) << 12);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0033DD31 File Offset: 0x0033BF31
		public bool fullbrightWall()
		{
			return (this.sTileHeader & 32768) == 32768;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0033DD46 File Offset: 0x0033BF46
		public void fullbrightWall(bool fullbrightWall)
		{
			if (fullbrightWall)
			{
				this.sTileHeader |= 32768;
				return;
			}
			this.sTileHeader = (ushort)((int)this.sTileHeader & -32769);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0033DD74 File Offset: 0x0033BF74
		public void Clear(TileDataType types)
		{
			if ((types & TileDataType.Tile) != (TileDataType)0)
			{
				this.type = 0;
				this.active(false);
				this.frameX = 0;
				this.frameY = 0;
			}
			if ((types & TileDataType.Wall) != (TileDataType)0)
			{
				this.wall = 0;
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
				this.liquid = 0;
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

		// Token: 0x060009FD RID: 2557 RVA: 0x0033DE3C File Offset: 0x0033C03C
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
			int num = (flag ? 1 : 0) << 3 | (flag3 ? 1 : 0) << 2 | (flag4 ? 1 : 0) << 1 | (flag5 ? 1 : 0);
			bool flag6 = tile.halfBrick();
			int num2 = (int)tile.slope();
			switch (num)
			{
			case 4:
				tile.slope(0);
				tile.halfBrick(true);
				goto IL_14F;
			case 5:
				tile.halfBrick(false);
				tile.slope(2);
				goto IL_14F;
			case 6:
				tile.halfBrick(false);
				tile.slope(1);
				goto IL_14F;
			case 9:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(4);
					goto IL_14F;
				}
				goto IL_14F;
			case 10:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(3);
					goto IL_14F;
				}
				goto IL_14F;
			}
			tile.halfBrick(false);
			tile.slope(0);
			IL_14F:
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

		// Token: 0x060009FE RID: 2558 RVA: 0x0033E011 File Offset: 0x0033C211
		public void CopyPaintAndCoating(Tile other)
		{
			this.color(other.color());
			this.invisibleBlock(other.invisibleBlock());
			this.fullbrightBlock(other.fullbrightBlock());
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0033E038 File Offset: 0x0033C238
		public TileColorCache BlockColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.color(),
				FullBright = this.fullbrightBlock(),
				Invisible = this.invisibleBlock()
			};
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0033E078 File Offset: 0x0033C278
		public TileColorCache WallColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.wallColor(),
				FullBright = this.fullbrightWall(),
				Invisible = this.invisibleWall()
			};
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0033E0B5 File Offset: 0x0033C2B5
		public void UseBlockColors(TileColorCache cache)
		{
			cache.ApplyToBlock(this);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0033E0BF File Offset: 0x0033C2BF
		public void UseWallColors(TileColorCache cache)
		{
			cache.ApplyToWall(this);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0033E0C9 File Offset: 0x0033C2C9
		public void ClearBlockPaintAndCoating()
		{
			this.color(0);
			this.fullbrightBlock(false);
			this.invisibleBlock(false);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0033E0E0 File Offset: 0x0033C2E0
		public void ClearWallPaintAndCoating()
		{
			this.wallColor(0);
			this.fullbrightWall(false);
			this.invisibleWall(false);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0033E0F8 File Offset: 0x0033C2F8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Tile Type:",
				this.type,
				" Active:",
				this.active().ToString(),
				" Wall:",
				this.wall,
				" Slope:",
				this.slope(),
				" fX:",
				this.frameX,
				" fY:",
				this.frameY
			});
		}

		// Token: 0x04000901 RID: 2305
		public ushort type;

		// Token: 0x04000902 RID: 2306
		public ushort wall;

		// Token: 0x04000903 RID: 2307
		public byte liquid;

		// Token: 0x04000904 RID: 2308
		public ushort sTileHeader;

		// Token: 0x04000905 RID: 2309
		public byte bTileHeader;

		// Token: 0x04000906 RID: 2310
		public byte bTileHeader2;

		// Token: 0x04000907 RID: 2311
		public byte bTileHeader3;

		// Token: 0x04000908 RID: 2312
		public short frameX;

		// Token: 0x04000909 RID: 2313
		public short frameY;

		// Token: 0x0400090A RID: 2314
		private const int Bit0 = 1;

		// Token: 0x0400090B RID: 2315
		private const int Bit1 = 2;

		// Token: 0x0400090C RID: 2316
		private const int Bit2 = 4;

		// Token: 0x0400090D RID: 2317
		private const int Bit3 = 8;

		// Token: 0x0400090E RID: 2318
		private const int Bit4 = 16;

		// Token: 0x0400090F RID: 2319
		private const int Bit5 = 32;

		// Token: 0x04000910 RID: 2320
		private const int Bit6 = 64;

		// Token: 0x04000911 RID: 2321
		private const int Bit7 = 128;

		// Token: 0x04000912 RID: 2322
		private const ushort Bit15 = 32768;

		// Token: 0x04000913 RID: 2323
		public const int Type_Solid = 0;

		// Token: 0x04000914 RID: 2324
		public const int Type_Halfbrick = 1;

		// Token: 0x04000915 RID: 2325
		public const int Type_SlopeDownRight = 2;

		// Token: 0x04000916 RID: 2326
		public const int Type_SlopeDownLeft = 3;

		// Token: 0x04000917 RID: 2327
		public const int Type_SlopeUpRight = 4;

		// Token: 0x04000918 RID: 2328
		public const int Type_SlopeUpLeft = 5;

		// Token: 0x04000919 RID: 2329
		public const int Liquid_Water = 0;

		// Token: 0x0400091A RID: 2330
		public const int Liquid_Lava = 1;

		// Token: 0x0400091B RID: 2331
		public const int Liquid_Honey = 2;

		// Token: 0x0400091C RID: 2332
		public const int Liquid_Shimmer = 3;

		// Token: 0x0400091D RID: 2333
		private const int NeitherLavaOrHoney = 159;

		// Token: 0x0400091E RID: 2334
		private const int EitherLavaOrHoney = 96;
	}
}
