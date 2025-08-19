using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terraria
{
	// Token: 0x02000065 RID: 101
	public struct TileObject
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x003FD7F8 File Offset: 0x003FB9F8
		public unsafe static bool Place(TileObject toBePlaced)
		{
			TileObjectData tileData = TileObjectData.GetTileData(toBePlaced.type, toBePlaced.style, toBePlaced.alternate);
			if (tileData == null)
			{
				return false;
			}
			if (tileData.HookPlaceOverride.hook != null)
			{
				int arg;
				int arg2;
				if (tileData.HookPlaceOverride.processedCoordinates)
				{
					arg = toBePlaced.xCoord;
					arg2 = toBePlaced.yCoord;
				}
				else
				{
					arg = toBePlaced.xCoord + (int)tileData.Origin.X;
					arg2 = toBePlaced.yCoord + (int)tileData.Origin.Y;
				}
				if (tileData.HookPlaceOverride.hook(arg, arg2, toBePlaced.type, toBePlaced.style, 1, toBePlaced.alternate) == tileData.HookPlaceOverride.badReturn)
				{
					return false;
				}
			}
			else
			{
				ushort num = (ushort)toBePlaced.type;
				int num2 = tileData.CalculatePlacementStyle(toBePlaced.style, toBePlaced.alternate, toBePlaced.random);
				int num3 = 0;
				if (tileData.StyleWrapLimit > 0)
				{
					num3 = num2 / tileData.StyleWrapLimit * tileData.StyleLineSkip;
					num2 %= tileData.StyleWrapLimit;
				}
				int num4;
				int num5;
				if (tileData.StyleHorizontal)
				{
					num4 = tileData.CoordinateFullWidth * num2;
					num5 = tileData.CoordinateFullHeight * num3;
				}
				else
				{
					num4 = tileData.CoordinateFullWidth * num3;
					num5 = tileData.CoordinateFullHeight * num2;
				}
				int num6 = toBePlaced.xCoord;
				int num7 = toBePlaced.yCoord;
				for (int i = 0; i < tileData.Width; i++)
				{
					for (int j = 0; j < tileData.Height; j++)
					{
						Tile tileSafely = Framing.GetTileSafely(num6 + i, num7 + j);
						if (tileSafely.active() && *tileSafely.type != 484 && (Main.tileCut[(int)(*tileSafely.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tileSafely.type)]))
						{
							WorldGen.KillTile(num6 + i, num7 + j, false, false, false);
							if (!Main.tile[num6 + i, num7 + j].active() && Main.netMode != 0)
							{
								NetMessage.SendData(17, -1, -1, null, 0, (float)(num6 + i), (float)(num7 + j), 0f, 0, 0, 0);
							}
						}
					}
				}
				for (int k = 0; k < tileData.Width; k++)
				{
					int num8 = num4 + k * (tileData.CoordinateWidth + tileData.CoordinatePadding);
					int num9 = num5;
					for (int l = 0; l < tileData.Height; l++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(num6 + k, num7 + l);
						if (!tileSafely2.active())
						{
							tileSafely2.active(true);
							*tileSafely2.frameX = (short)num8;
							*tileSafely2.frameY = (short)num9;
							*tileSafely2.type = num;
						}
						num9 += tileData.CoordinateHeights[l] + tileData.CoordinatePadding;
					}
				}
			}
			if (tileData.FlattenAnchors)
			{
				AnchorData anchorBottom = tileData.AnchorBottom;
				if (anchorBottom.tileCount != 0 && (anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile)
				{
					int num10 = toBePlaced.xCoord + anchorBottom.checkStart;
					int j2 = toBePlaced.yCoord + tileData.Height;
					for (int m = 0; m < anchorBottom.tileCount; m++)
					{
						Tile tileSafely3 = Framing.GetTileSafely(num10 + m, j2);
						if (Main.tileSolid[(int)(*tileSafely3.type)] && !Main.tileSolidTop[(int)(*tileSafely3.type)] && tileSafely3.blockType() != 0)
						{
							WorldGen.SlopeTile(num10 + m, j2, 0, false);
						}
					}
				}
				anchorBottom = tileData.AnchorTop;
				if (anchorBottom.tileCount != 0 && (anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile)
				{
					int num11 = toBePlaced.xCoord + anchorBottom.checkStart;
					int j3 = toBePlaced.yCoord - 1;
					for (int n = 0; n < anchorBottom.tileCount; n++)
					{
						Tile tileSafely4 = Framing.GetTileSafely(num11 + n, j3);
						if (Main.tileSolid[(int)(*tileSafely4.type)] && !Main.tileSolidTop[(int)(*tileSafely4.type)] && tileSafely4.blockType() != 0)
						{
							WorldGen.SlopeTile(num11 + n, j3, 0, false);
						}
					}
				}
				anchorBottom = tileData.AnchorRight;
				if (anchorBottom.tileCount != 0 && (anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile)
				{
					int i2 = toBePlaced.xCoord + tileData.Width;
					int num12 = toBePlaced.yCoord + anchorBottom.checkStart;
					for (int num13 = 0; num13 < anchorBottom.tileCount; num13++)
					{
						Tile tileSafely5 = Framing.GetTileSafely(i2, num12 + num13);
						if (Main.tileSolid[(int)(*tileSafely5.type)] && !Main.tileSolidTop[(int)(*tileSafely5.type)] && tileSafely5.blockType() != 0)
						{
							WorldGen.SlopeTile(i2, num12 + num13, 0, false);
						}
					}
				}
				anchorBottom = tileData.AnchorLeft;
				if (anchorBottom.tileCount != 0 && (anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile)
				{
					int i3 = toBePlaced.xCoord - 1;
					int num14 = toBePlaced.yCoord + anchorBottom.checkStart;
					for (int num15 = 0; num15 < anchorBottom.tileCount; num15++)
					{
						Tile tileSafely6 = Framing.GetTileSafely(i3, num14 + num15);
						if (Main.tileSolid[(int)(*tileSafely6.type)] && !Main.tileSolidTop[(int)(*tileSafely6.type)] && tileSafely6.blockType() != 0)
						{
							WorldGen.SlopeTile(i3, num14 + num15, 0, false);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x003FDD28 File Offset: 0x003FBF28
		public unsafe static bool CanPlace(int x, int y, int type, int style, int dir, out TileObject objectData, bool onlyCheck = false, int? forcedRandom = null, bool checkStay = false)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			objectData = TileObject.Empty;
			if (tileData == null)
			{
				return false;
			}
			int num = x - (int)tileData.Origin.X;
			int num2 = y - (int)tileData.Origin.Y;
			if (num < 0 || num + tileData.Width >= Main.maxTilesX || num2 < 0 || num2 + tileData.Height >= Main.maxTilesY)
			{
				return false;
			}
			bool flag = tileData.RandomStyleRange > 0;
			if (TileObjectPreviewData.placementCache == null)
			{
				TileObjectPreviewData.placementCache = new TileObjectPreviewData();
			}
			TileObjectPreviewData.placementCache.Reset();
			int num3 = 0;
			if (tileData.AlternatesCount != 0)
			{
				num3 = tileData.AlternatesCount;
			}
			float num4 = -1f;
			float num5 = -1f;
			int num6 = 0;
			TileObjectData tileObjectData = null;
			int num7 = -1;
			while (num7 < num3)
			{
				num7++;
				TileObjectData tileData2 = TileObjectData.GetTileData(type, style, num7);
				if (tileData2.Direction == TileObjectDirection.None || ((tileData2.Direction != TileObjectDirection.PlaceLeft || dir != 1) && (tileData2.Direction != TileObjectDirection.PlaceRight || dir != -1)))
				{
					int num8 = x - (int)tileData2.Origin.X;
					int num9 = y - (int)tileData2.Origin.Y;
					if (num8 < 5 || num8 + tileData2.Width > Main.maxTilesX - 5 || num9 < 5 || num9 + tileData2.Height > Main.maxTilesY - 5)
					{
						return false;
					}
					Rectangle rectangle;
					rectangle..ctor(0, 0, tileData2.Width, tileData2.Height);
					int num10 = 0;
					int num11 = 0;
					if (tileData2.AnchorTop.tileCount != 0)
					{
						if (rectangle.Y == 0)
						{
							rectangle.Y = -1;
							rectangle.Height++;
							num11++;
						}
						int checkStart = tileData2.AnchorTop.checkStart;
						if (checkStart < rectangle.X)
						{
							rectangle.Width += rectangle.X - checkStart;
							num10 += rectangle.X - checkStart;
							rectangle.X = checkStart;
						}
						int num12 = checkStart + tileData2.AnchorTop.tileCount - 1;
						int num13 = rectangle.X + rectangle.Width - 1;
						if (num12 > num13)
						{
							rectangle.Width += num12 - num13;
						}
					}
					if (tileData2.AnchorBottom.tileCount != 0)
					{
						if (rectangle.Y + rectangle.Height == tileData2.Height)
						{
							rectangle.Height++;
						}
						int checkStart2 = tileData2.AnchorBottom.checkStart;
						if (checkStart2 < rectangle.X)
						{
							rectangle.Width += rectangle.X - checkStart2;
							num10 += rectangle.X - checkStart2;
							rectangle.X = checkStart2;
						}
						int num14 = checkStart2 + tileData2.AnchorBottom.tileCount - 1;
						int num15 = rectangle.X + rectangle.Width - 1;
						if (num14 > num15)
						{
							rectangle.Width += num14 - num15;
						}
					}
					if (tileData2.AnchorLeft.tileCount != 0)
					{
						if (rectangle.X == 0)
						{
							rectangle.X = -1;
							rectangle.Width++;
							num10++;
						}
						int num16 = tileData2.AnchorLeft.checkStart;
						if ((tileData2.AnchorLeft.type & AnchorType.Tree) == AnchorType.Tree)
						{
							num16--;
						}
						if (num16 < rectangle.Y)
						{
							rectangle.Width += rectangle.Y - num16;
							num11 += rectangle.Y - num16;
							rectangle.Y = num16;
						}
						int num17 = num16 + tileData2.AnchorLeft.tileCount - 1;
						if ((tileData2.AnchorLeft.type & AnchorType.Tree) == AnchorType.Tree)
						{
							num17 += 2;
						}
						int num18 = rectangle.Y + rectangle.Height - 1;
						if (num17 > num18)
						{
							rectangle.Height += num17 - num18;
						}
					}
					if (tileData2.AnchorRight.tileCount != 0)
					{
						if (rectangle.X + rectangle.Width == tileData2.Width)
						{
							rectangle.Width++;
						}
						int num19 = tileData2.AnchorLeft.checkStart;
						if ((tileData2.AnchorRight.type & AnchorType.Tree) == AnchorType.Tree)
						{
							num19--;
						}
						if (num19 < rectangle.Y)
						{
							rectangle.Width += rectangle.Y - num19;
							num11 += rectangle.Y - num19;
							rectangle.Y = num19;
						}
						int num20 = num19 + tileData2.AnchorRight.tileCount - 1;
						if ((tileData2.AnchorRight.type & AnchorType.Tree) == AnchorType.Tree)
						{
							num20 += 2;
						}
						int num21 = rectangle.Y + rectangle.Height - 1;
						if (num20 > num21)
						{
							rectangle.Height += num20 - num21;
						}
					}
					if (onlyCheck)
					{
						TileObject.objectPreview.Reset();
						TileObject.objectPreview.Active = true;
						TileObject.objectPreview.Type = (ushort)type;
						TileObject.objectPreview.Style = (short)style;
						TileObject.objectPreview.Alternate = num7;
						TileObject.objectPreview.Size = new Point16(rectangle.Width, rectangle.Height);
						TileObject.objectPreview.ObjectStart = new Point16(num10, num11);
						TileObject.objectPreview.Coordinates = new Point16(num8 - num10, num9 - num11);
					}
					float num22 = 0f;
					float num23 = (float)(tileData2.Width * tileData2.Height);
					float num24 = 0f;
					float num25 = 0f;
					for (int i = 0; i < tileData2.Width; i++)
					{
						for (int j = 0; j < tileData2.Height; j++)
						{
							Tile tileSafely = Framing.GetTileSafely(num8 + i, num9 + j);
							bool flag2 = !tileData2.LiquidPlace(tileSafely, checkStay);
							bool flag3 = false;
							if (tileData2.AnchorWall)
							{
								num25 += 1f;
								if (!tileData2.isValidWallAnchor((int)(*tileSafely.wall)))
								{
									flag3 = true;
								}
								else
								{
									num24 += 1f;
								}
							}
							bool flag4 = false;
							if (tileSafely.active() && (!Main.tileCut[(int)(*tileSafely.type)] || *tileSafely.type == 484 || *tileSafely.type == 654) && !TileID.Sets.BreakableWhenPlacing[(int)(*tileSafely.type)] && !checkStay)
							{
								flag4 = true;
							}
							if (flag4 || flag2 || flag3)
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[i + num10, j + num11] = 2;
								}
							}
							else
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[i + num10, j + num11] = 1;
								}
								num22 += 1f;
							}
						}
					}
					AnchorData anchorBottom = tileData2.AnchorBottom;
					if (anchorBottom.tileCount != 0)
					{
						num25 += (float)anchorBottom.tileCount;
						int height = tileData2.Height;
						for (int k = 0; k < anchorBottom.tileCount; k++)
						{
							int num26 = anchorBottom.checkStart + k;
							Tile tileSafely2 = Framing.GetTileSafely(num8 + num26, num9 + height);
							bool flag5 = false;
							if (tileSafely2.nactive())
							{
								if ((anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile && Main.tileSolid[(int)(*tileSafely2.type)] && !Main.tileSolidTop[(int)(*tileSafely2.type)] && !Main.tileNoAttach[(int)(*tileSafely2.type)] && (tileData2.FlattenAnchors || tileSafely2.blockType() == 0))
								{
									flag5 = tileData2.isValidTileAnchor((int)(*tileSafely2.type));
								}
								if (!flag5 && ((anchorBottom.type & AnchorType.SolidWithTop) == AnchorType.SolidWithTop || (anchorBottom.type & AnchorType.Table) == AnchorType.Table))
								{
									if (TileID.Sets.Platforms[(int)(*tileSafely2.type)])
									{
										int num48 = (int)(*tileSafely2.frameX) / TileObjectData.PlatformFrameWidth();
										if (!tileSafely2.halfBrick() && WorldGen.PlatformProperTopFrame(*tileSafely2.frameX))
										{
											flag5 = true;
										}
									}
									else if (Main.tileSolid[(int)(*tileSafely2.type)] && Main.tileSolidTop[(int)(*tileSafely2.type)])
									{
										flag5 = true;
									}
								}
								if (!flag5 && (anchorBottom.type & AnchorType.Table) == AnchorType.Table && !TileID.Sets.Platforms[(int)(*tileSafely2.type)] && Main.tileTable[(int)(*tileSafely2.type)] && tileSafely2.blockType() == 0)
								{
									flag5 = true;
								}
								if (!flag5 && (anchorBottom.type & AnchorType.SolidSide) == AnchorType.SolidSide && Main.tileSolid[(int)(*tileSafely2.type)] && !Main.tileSolidTop[(int)(*tileSafely2.type)] && tileSafely2.blockType() - 4 <= 1)
								{
									flag5 = tileData2.isValidTileAnchor((int)(*tileSafely2.type));
								}
								if (!flag5 && (anchorBottom.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int)(*tileSafely2.type)))
								{
									flag5 = true;
								}
							}
							else if (!flag5 && (anchorBottom.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
							{
								flag5 = true;
							}
							if (!flag5)
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num26 + num10, height + num11] = 2;
								}
							}
							else
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num26 + num10, height + num11] = 1;
								}
								num24 += 1f;
							}
						}
					}
					anchorBottom = tileData2.AnchorTop;
					if (anchorBottom.tileCount != 0)
					{
						num25 += (float)anchorBottom.tileCount;
						int num27 = -1;
						for (int l = 0; l < anchorBottom.tileCount; l++)
						{
							int num28 = anchorBottom.checkStart + l;
							Tile tileSafely3 = Framing.GetTileSafely(num8 + num28, num9 + num27);
							bool flag6 = false;
							if (tileSafely3.nactive())
							{
								if ((anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile && Main.tileSolid[(int)(*tileSafely3.type)] && !Main.tileSolidTop[(int)(*tileSafely3.type)] && !Main.tileNoAttach[(int)(*tileSafely3.type)] && (tileData2.FlattenAnchors || tileSafely3.blockType() == 0))
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.SolidBottom) == AnchorType.SolidBottom && ((Main.tileSolid[(int)(*tileSafely3.type)] && (!Main.tileSolidTop[(int)(*tileSafely3.type)] || (TileID.Sets.Platforms[(int)(*tileSafely3.type)] && (tileSafely3.halfBrick() || tileSafely3.topSlope())))) || tileSafely3.halfBrick() || tileSafely3.topSlope()) && !TileID.Sets.NotReallySolid[(int)(*tileSafely3.type)] && !tileSafely3.bottomSlope())
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.Platform) == AnchorType.Platform && TileID.Sets.Platforms[(int)(*tileSafely3.type)])
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.PlatformNonHammered) == AnchorType.PlatformNonHammered && TileID.Sets.Platforms[(int)(*tileSafely3.type)] && tileSafely3.slope() == 0 && !tileSafely3.halfBrick())
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.PlanterBox) == AnchorType.PlanterBox && *tileSafely3.type == 380)
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.SolidSide) == AnchorType.SolidSide && Main.tileSolid[(int)(*tileSafely3.type)] && !Main.tileSolidTop[(int)(*tileSafely3.type)] && tileSafely3.blockType() - 2 <= 1)
								{
									flag6 = tileData2.isValidTileAnchor((int)(*tileSafely3.type));
								}
								if (!flag6 && (anchorBottom.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int)(*tileSafely3.type)))
								{
									flag6 = true;
								}
							}
							else if (!flag6 && (anchorBottom.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
							{
								flag6 = true;
							}
							if (!flag6)
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num28 + num10, num27 + num11] = 2;
								}
							}
							else
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num28 + num10, num27 + num11] = 1;
								}
								num24 += 1f;
							}
						}
					}
					anchorBottom = tileData2.AnchorRight;
					if (anchorBottom.tileCount != 0)
					{
						num25 += (float)anchorBottom.tileCount;
						int width = tileData2.Width;
						for (int m = 0; m < anchorBottom.tileCount; m++)
						{
							int num29 = anchorBottom.checkStart + m;
							Tile tileSafely4 = Framing.GetTileSafely(num8 + width, num9 + num29);
							bool flag7 = false;
							if (tileSafely4.nactive())
							{
								if ((anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile && Main.tileSolid[(int)(*tileSafely4.type)] && !Main.tileSolidTop[(int)(*tileSafely4.type)] && !Main.tileNoAttach[(int)(*tileSafely4.type)] && (tileData2.FlattenAnchors || tileSafely4.blockType() == 0))
								{
									flag7 = tileData2.isValidTileAnchor((int)(*tileSafely4.type));
								}
								if (!flag7 && (anchorBottom.type & AnchorType.SolidSide) == AnchorType.SolidSide && Main.tileSolid[(int)(*tileSafely4.type)] && !Main.tileSolidTop[(int)(*tileSafely4.type)])
								{
									int num30 = tileSafely4.blockType();
									if (num30 == 2 || num30 == 4)
									{
										flag7 = tileData2.isValidTileAnchor((int)(*tileSafely4.type));
									}
								}
								if (!flag7 && (anchorBottom.type & AnchorType.Tree) == AnchorType.Tree && TileID.Sets.IsATreeTrunk[(int)(*tileSafely4.type)])
								{
									flag7 = true;
									if (m == 0)
									{
										num25 += 1f;
										Tile tileSafely5 = Framing.GetTileSafely(num8 + width, num9 + num29 - 1);
										if (tileSafely5.nactive() && TileID.Sets.IsATreeTrunk[(int)(*tileSafely5.type)])
										{
											num24 += 1f;
											if (onlyCheck)
											{
												TileObject.objectPreview[width + num10, num29 + num11 - 1] = 1;
											}
										}
										else if (onlyCheck)
										{
											TileObject.objectPreview[width + num10, num29 + num11 - 1] = 2;
										}
									}
									if (m == anchorBottom.tileCount - 1)
									{
										num25 += 1f;
										Tile tileSafely6 = Framing.GetTileSafely(num8 + width, num9 + num29 + 1);
										if (tileSafely6.nactive() && TileID.Sets.IsATreeTrunk[(int)(*tileSafely6.type)])
										{
											num24 += 1f;
											if (onlyCheck)
											{
												TileObject.objectPreview[width + num10, num29 + num11 + 1] = 1;
											}
										}
										else if (onlyCheck)
										{
											TileObject.objectPreview[width + num10, num29 + num11 + 1] = 2;
										}
									}
								}
								if (!flag7 && (anchorBottom.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int)(*tileSafely4.type)))
								{
									flag7 = true;
								}
							}
							else if (!flag7 && (anchorBottom.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
							{
								flag7 = true;
							}
							if (!flag7)
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[width + num10, num29 + num11] = 2;
								}
							}
							else
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[width + num10, num29 + num11] = 1;
								}
								num24 += 1f;
							}
						}
					}
					anchorBottom = tileData2.AnchorLeft;
					if (anchorBottom.tileCount != 0)
					{
						num25 += (float)anchorBottom.tileCount;
						int num31 = -1;
						for (int n = 0; n < anchorBottom.tileCount; n++)
						{
							int num32 = anchorBottom.checkStart + n;
							Tile tileSafely7 = Framing.GetTileSafely(num8 + num31, num9 + num32);
							bool flag8 = false;
							if (tileSafely7.nactive())
							{
								if ((anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile && Main.tileSolid[(int)(*tileSafely7.type)] && !Main.tileSolidTop[(int)(*tileSafely7.type)] && !Main.tileNoAttach[(int)(*tileSafely7.type)] && (tileData2.FlattenAnchors || tileSafely7.blockType() == 0))
								{
									flag8 = tileData2.isValidTileAnchor((int)(*tileSafely7.type));
								}
								if (!flag8 && (anchorBottom.type & AnchorType.SolidSide) == AnchorType.SolidSide && Main.tileSolid[(int)(*tileSafely7.type)] && !Main.tileSolidTop[(int)(*tileSafely7.type)])
								{
									int num33 = tileSafely7.blockType();
									if (num33 == 3 || num33 == 5)
									{
										flag8 = tileData2.isValidTileAnchor((int)(*tileSafely7.type));
									}
								}
								if (!flag8 && (anchorBottom.type & AnchorType.Tree) == AnchorType.Tree && TileID.Sets.IsATreeTrunk[(int)(*tileSafely7.type)])
								{
									flag8 = true;
									if (n == 0)
									{
										num25 += 1f;
										Tile tileSafely8 = Framing.GetTileSafely(num8 + num31, num9 + num32 - 1);
										if (tileSafely8.nactive() && TileID.Sets.IsATreeTrunk[(int)(*tileSafely8.type)])
										{
											num24 += 1f;
											if (onlyCheck)
											{
												TileObject.objectPreview[num31 + num10, num32 + num11 - 1] = 1;
											}
										}
										else if (onlyCheck)
										{
											TileObject.objectPreview[num31 + num10, num32 + num11 - 1] = 2;
										}
									}
									if (n == anchorBottom.tileCount - 1)
									{
										num25 += 1f;
										Tile tileSafely9 = Framing.GetTileSafely(num8 + num31, num9 + num32 + 1);
										if (tileSafely9.nactive() && TileID.Sets.IsATreeTrunk[(int)(*tileSafely9.type)])
										{
											num24 += 1f;
											if (onlyCheck)
											{
												TileObject.objectPreview[num31 + num10, num32 + num11 + 1] = 1;
											}
										}
										else if (onlyCheck)
										{
											TileObject.objectPreview[num31 + num10, num32 + num11 + 1] = 2;
										}
									}
								}
								if (!flag8 && (anchorBottom.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int)(*tileSafely7.type)))
								{
									flag8 = true;
								}
							}
							else if (!flag8 && (anchorBottom.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
							{
								flag8 = true;
							}
							if (!flag8)
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num31 + num10, num32 + num11] = 2;
								}
							}
							else
							{
								if (onlyCheck)
								{
									TileObject.objectPreview[num31 + num10, num32 + num11] = 1;
								}
								num24 += 1f;
							}
						}
					}
					if (tileData2.HookCheckIfCanPlace.hook != null)
					{
						if (tileData2.HookCheckIfCanPlace.processedCoordinates)
						{
							Point16 origin = tileData2.Origin;
							Point16 origin2 = tileData2.Origin;
						}
						if (tileData2.HookCheckIfCanPlace.hook(x, y, type, style, dir, num7) == tileData2.HookCheckIfCanPlace.badReturn && tileData2.HookCheckIfCanPlace.badResponse == 0)
						{
							num24 = 0f;
							num22 = 0f;
							TileObject.objectPreview.AllInvalid();
						}
					}
					float num34 = num24 / num25;
					if (num25 == 0f)
					{
						num34 = 1f;
					}
					float num35 = num22 / num23;
					if (num35 == 1f && num25 == 0f)
					{
						num34 = 1f;
						num35 = 1f;
					}
					if (num34 == 1f && num35 == 1f)
					{
						num4 = 1f;
						num5 = 1f;
						num6 = num7;
						tileObjectData = tileData2;
						break;
					}
					if (num34 > num4 || (num34 == num4 && num35 > num5))
					{
						TileObjectPreviewData.placementCache.CopyFrom(TileObject.objectPreview);
						num4 = num34;
						num5 = num35;
						tileObjectData = tileData2;
						num6 = num7;
					}
				}
			}
			int num36 = -1;
			if (flag)
			{
				if (TileObjectPreviewData.randomCache == null)
				{
					TileObjectPreviewData.randomCache = new TileObjectPreviewData();
				}
				bool flag9 = false;
				if ((int)TileObjectPreviewData.randomCache.Type == type)
				{
					Point16 coordinates = TileObjectPreviewData.randomCache.Coordinates;
					Point16 objectStart = TileObjectPreviewData.randomCache.ObjectStart;
					int num37 = (int)(coordinates.X + objectStart.X);
					int num38 = (int)(coordinates.Y + objectStart.Y);
					int num39 = x - (int)tileObjectData.Origin.X;
					int num40 = y - (int)tileObjectData.Origin.Y;
					if (num37 != num39 || num38 != num40)
					{
						flag9 = true;
					}
				}
				else
				{
					flag9 = true;
				}
				int randomStyleRange = tileData.RandomStyleRange;
				int num41 = Main.rand.Next(tileData.RandomStyleRange);
				if (forcedRandom != null)
				{
					num41 = (forcedRandom.Value % randomStyleRange + randomStyleRange) % randomStyleRange;
				}
				num36 = ((!flag9 && forcedRandom == null) ? TileObjectPreviewData.randomCache.Random : num41);
			}
			if (tileData.SpecificRandomStyles != null)
			{
				if (TileObjectPreviewData.randomCache == null)
				{
					TileObjectPreviewData.randomCache = new TileObjectPreviewData();
				}
				bool flag10 = false;
				if ((int)TileObjectPreviewData.randomCache.Type == type)
				{
					Point16 coordinates2 = TileObjectPreviewData.randomCache.Coordinates;
					Point16 objectStart2 = TileObjectPreviewData.randomCache.ObjectStart;
					int num42 = (int)(coordinates2.X + objectStart2.X);
					int num43 = (int)(coordinates2.Y + objectStart2.Y);
					int num44 = x - (int)tileData.Origin.X;
					int num45 = y - (int)tileData.Origin.Y;
					if (num42 != num44 || num43 != num45)
					{
						flag10 = true;
					}
				}
				else
				{
					flag10 = true;
				}
				int num46 = tileData.SpecificRandomStyles.Length;
				int num47 = Main.rand.Next(num46);
				if (forcedRandom != null)
				{
					num47 = (forcedRandom.Value % num46 + num46) % num46;
				}
				num36 = ((!flag10 && forcedRandom == null) ? TileObjectPreviewData.randomCache.Random : (tileData.SpecificRandomStyles[num47] - style));
			}
			if (onlyCheck)
			{
				if (num4 != 1f || num5 != 1f)
				{
					TileObject.objectPreview.CopyFrom(TileObjectPreviewData.placementCache);
					num7 = num6;
				}
				TileObject.objectPreview.Random = num36;
				if (tileData.RandomStyleRange > 0 || tileData.SpecificRandomStyles != null)
				{
					TileObjectPreviewData.randomCache.CopyFrom(TileObject.objectPreview);
				}
			}
			if (!onlyCheck)
			{
				objectData.xCoord = x - (int)tileObjectData.Origin.X;
				objectData.yCoord = y - (int)tileObjectData.Origin.Y;
				objectData.type = type;
				objectData.style = style;
				objectData.alternate = num7;
				objectData.random = num36;
			}
			return num4 == 1f && num5 == 1f;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x003FF244 File Offset: 0x003FD444
		public static void DrawPreview(SpriteBatch sb, TileObjectPreviewData op, Vector2 position)
		{
			Point16 coordinates = op.Coordinates;
			Texture2D value = TextureAssets.Tile[(int)op.Type].Value;
			TileObjectData tileData = TileObjectData.GetTileData((int)op.Type, (int)op.Style, op.Alternate);
			int num3 = tileData.CalculatePlacementStyle((int)op.Style, op.Alternate, op.Random);
			int num4 = 0;
			int num5 = tileData.DrawYOffset;
			int drawXOffset = tileData.DrawXOffset;
			num3 += tileData.DrawStyleOffset;
			int num6 = tileData.StyleWrapLimit;
			int num7 = tileData.StyleLineSkip;
			if (tileData.StyleWrapLimitVisualOverride != null)
			{
				num6 = tileData.StyleWrapLimitVisualOverride.Value;
			}
			if (tileData.styleLineSkipVisualOverride != null)
			{
				num7 = tileData.styleLineSkipVisualOverride.Value;
			}
			if (num6 > 0)
			{
				num4 = num3 / num6 * num7;
				num3 %= num6;
			}
			int num8;
			int num9;
			if (tileData.StyleHorizontal)
			{
				num8 = tileData.CoordinateFullWidth * num3;
				num9 = tileData.CoordinateFullHeight * num4;
			}
			else
			{
				num8 = tileData.CoordinateFullWidth * num4;
				num9 = tileData.CoordinateFullHeight * num3;
			}
			for (int i = 0; i < (int)op.Size.X; i++)
			{
				int x = num8 + (i - (int)op.ObjectStart.X) * (tileData.CoordinateWidth + tileData.CoordinatePadding);
				int num10 = num9;
				int j = 0;
				while (j < (int)op.Size.Y)
				{
					int num11 = (int)coordinates.X + i;
					int num12 = (int)coordinates.Y + j;
					if (j == 0 && tileData.DrawStepDown != 0 && WorldGen.SolidTile(Framing.GetTileSafely(num11, num12 - 1)))
					{
						num5 += tileData.DrawStepDown;
					}
					if (op.Type == 567)
					{
						num5 = ((j != 0) ? tileData.DrawYOffset : (tileData.DrawYOffset - 2));
					}
					int num13 = op[i, j];
					Color color;
					if (num13 == 1)
					{
						color = Color.White;
						goto IL_1D5;
					}
					if (num13 == 2)
					{
						color = Color.Red * 0.7f;
						goto IL_1D5;
					}
					IL_354:
					j++;
					continue;
					IL_1D5:
					color *= 0.5f;
					if (i >= (int)op.ObjectStart.X && i < (int)op.ObjectStart.X + tileData.Width && j >= (int)op.ObjectStart.Y && j < (int)op.ObjectStart.Y + tileData.Height)
					{
						SpriteEffects spriteEffects = 0;
						if (tileData.DrawFlipHorizontal && num11 % 2 == 0)
						{
							spriteEffects |= 1;
						}
						if (tileData.DrawFlipVertical && num12 % 2 == 0)
						{
							spriteEffects |= 2;
						}
						int coordinateWidth = tileData.CoordinateWidth;
						int num14 = tileData.CoordinateHeights[j - (int)op.ObjectStart.Y];
						if (op.Type == 114 && j == 1)
						{
							num14 += 2;
						}
						Rectangle frame;
						frame..ctor(x, num10, coordinateWidth, num14);
						Vector2 drawPosition;
						drawPosition..ctor((float)(num11 * 16 - (int)(position.X + (float)(coordinateWidth - 16) / 2f) + drawXOffset), (float)(num12 * 16 - (int)position.Y + num5));
						bool validPlacement = num13 == 1;
						if (TileLoader.PreDrawPlacementPreview(num11, num12, (int)op.Type, sb, ref frame, ref drawPosition, ref color, validPlacement, ref spriteEffects))
						{
							Rectangle? rectangle = new Rectangle?(frame);
							sb.Draw(value, drawPosition, rectangle, color, 0f, Vector2.Zero, 1f, spriteEffects, 0f);
						}
						TileLoader.PostDrawPlacementPreview(num11, num12, (int)op.Type, sb, frame, drawPosition, color, validPlacement, spriteEffects);
						num10 += num14 + tileData.CoordinatePadding;
						goto IL_354;
					}
					goto IL_354;
				}
			}
		}

		// Token: 0x04000EDF RID: 3807
		public int xCoord;

		// Token: 0x04000EE0 RID: 3808
		public int yCoord;

		// Token: 0x04000EE1 RID: 3809
		public int type;

		// Token: 0x04000EE2 RID: 3810
		public int style;

		/// <summary> Note: The index of the alternate within the <see cref="P:Terraria.ObjectData.TileObjectData.Alternates" />, not the alternate placement style offset (<see cref="P:Terraria.ObjectData.TileObjectData.Style" />). This counts from 1, a value of 0 means it is not an alternate. </summary>
		// Token: 0x04000EE3 RID: 3811
		public int alternate;

		// Token: 0x04000EE4 RID: 3812
		public int random;

		// Token: 0x04000EE5 RID: 3813
		public static TileObject Empty = default(TileObject);

		// Token: 0x04000EE6 RID: 3814
		public static TileObjectPreviewData objectPreview = new TileObjectPreviewData();
	}
}
