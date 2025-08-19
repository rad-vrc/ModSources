using System;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.GameContent;
using Terraria.GameContent.Liquid;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000465 RID: 1125
	public class TileLightScanner
	{
		// Token: 0x06003703 RID: 14083 RVA: 0x0058234C File Offset: 0x0058054C
		public void ExportTo(Rectangle area, LightMap outputMap, TileLightScannerOptions options)
		{
			this._drawInvisibleWalls = options.DrawInvisibleWalls;
			FastParallel.For(area.Left, area.Right, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					for (int j = area.Top; j <= area.Bottom; j++)
					{
						if (this.IsTileNullOrTouchingNull(i, j))
						{
							outputMap.SetMaskAt(i - area.X, j - area.Y, LightMaskMode.None);
							outputMap[i - area.X, j - area.Y] = Vector3.Zero;
						}
						else
						{
							LightMaskMode tileMask = this.GetTileMask(Main.tile[i, j]);
							outputMap.SetMaskAt(i - area.X, j - area.Y, tileMask);
							Vector3 outputColor;
							this.GetTileLight(i, j, out outputColor);
							outputMap[i - area.X, j - area.Y] = outputColor;
						}
					}
				}
			}, null);
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x005823A8 File Offset: 0x005805A8
		private bool IsTileNullOrTouchingNull(int x, int y)
		{
			return !WorldGen.InWorld(x, y, 1) || !(Main.tile[x, y] != null) || !(Main.tile[x + 1, y] != null) || !(Main.tile[x - 1, y] != null) || !(Main.tile[x, y - 1] != null) || Main.tile[x, y + 1] == null;
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x0058242D File Offset: 0x0058062D
		public void Update()
		{
			this._random.NextSeed();
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0058243A File Offset: 0x0058063A
		public LightMaskMode GetMaskMode(int x, int y)
		{
			return this.GetTileMask(Main.tile[x, y]);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x00582450 File Offset: 0x00580650
		private unsafe LightMaskMode GetTileMask(Tile tile)
		{
			if (this.LightIsBlocked(tile) && *tile.type != 131 && !tile.inActive() && tile.slope() == 0)
			{
				return LightMaskMode.Solid;
			}
			if (tile.lava() || *tile.liquid <= 128)
			{
				return LightMaskMode.None;
			}
			if (!tile.honey())
			{
				return LightMaskMode.Water;
			}
			return LightMaskMode.Honey;
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x005824B0 File Offset: 0x005806B0
		public void GetTileLight(int x, int y, out Vector3 outputColor)
		{
			outputColor = Vector3.Zero;
			Tile tile = Main.tile[x, y];
			FastRandom localRandom = this._random.WithModifier(x, y);
			if (y <= (int)Main.worldSurface)
			{
				this.ApplySurfaceLight(tile, x, y, ref outputColor);
			}
			else if (y > Main.UnderworldLayer)
			{
				this.ApplyHellLight(tile, x, y, ref outputColor);
			}
			this.ApplyWallLight(tile, x, y, ref localRandom, ref outputColor);
			if (tile.active())
			{
				this.ApplyTileLight(tile, x, y, ref localRandom, ref outputColor);
			}
			this.ApplyLiquidLight(tile, ref outputColor);
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x00582534 File Offset: 0x00580734
		private unsafe void ApplyLiquidLight(Tile tile, ref Vector3 lightColor)
		{
			if (*tile.liquid <= 0)
			{
				return;
			}
			if (tile.lava())
			{
				float num = 0.55f;
				num += (float)(270 - (int)Main.mouseTextColor) / 900f;
				if (lightColor.X < num)
				{
					lightColor.X = num;
				}
				if (lightColor.Y < num)
				{
					lightColor.Y = num * 0.6f;
				}
				if (lightColor.Z < num)
				{
					lightColor.Z = num * 0.2f;
					return;
				}
			}
			else if (tile.shimmer())
			{
				float num2 = 0.7f;
				float num3 = 0.7f;
				num2 += (float)(270 - (int)Main.mouseTextColor) / 900f;
				num3 += (float)(270 - (int)Main.mouseTextColor) / 125f;
				if (lightColor.X < num2)
				{
					lightColor.X = num2 * 0.6f;
				}
				if (lightColor.Y < num3)
				{
					lightColor.Y = num3 * 0.25f;
				}
				if (lightColor.Z < num2)
				{
					lightColor.Z = num2 * 0.9f;
				}
			}
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x00582632 File Offset: 0x00580832
		private unsafe bool LightIsBlocked(Tile tile)
		{
			return tile.active() && Main.tileBlockLight[(int)(*tile.type)] && (!tile.invisibleBlock() || this._drawInvisibleWalls);
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x00582660 File Offset: 0x00580860
		private unsafe void ApplyWallLight(Tile tile, int x, int y, ref FastRandom localRandom, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			ushort num5 = *tile.wall;
			if (num5 <= 156)
			{
				if (num5 <= 44)
				{
					if (num5 != 33)
					{
						if (num5 == 44)
						{
							if (!this.LightIsBlocked(tile))
							{
								num = (float)Main.DiscoR / 255f * 0.15f;
								num2 = (float)Main.DiscoG / 255f * 0.15f;
								num3 = (float)Main.DiscoB / 255f * 0.15f;
							}
						}
					}
					else if (!this.LightIsBlocked(tile))
					{
						num = 0.089999996f;
						num2 = 0.052500002f;
						num3 = 0.24f;
					}
				}
				else if (num5 != 137)
				{
					switch (num5)
					{
					case 153:
						num = 0.6f;
						num2 = 0.3f;
						break;
					case 154:
						num = 0.6f;
						num3 = 0.6f;
						break;
					case 155:
						num = 0.6f;
						num2 = 0.6f;
						num3 = 0.6f;
						break;
					case 156:
						num2 = 0.6f;
						break;
					}
				}
				else if (!this.LightIsBlocked(tile))
				{
					float num4 = 0.4f;
					num4 += (float)(270 - (int)Main.mouseTextColor) / 1500f;
					num4 += (float)localRandom.Next(0, 50) * 0.0005f;
					num = 1f * num4;
					num2 = 0.5f * num4;
					num3 = 0.1f * num4;
				}
			}
			else if (num5 <= 176)
			{
				switch (num5)
				{
				case 164:
					num = 0.6f;
					break;
				case 165:
					num3 = 0.6f;
					break;
				case 166:
					num = 0.6f;
					num2 = 0.6f;
					break;
				default:
					switch (num5)
					{
					case 174:
						if (!this.LightIsBlocked(tile))
						{
							num = 0.2975f;
						}
						break;
					case 175:
						if (!this.LightIsBlocked(tile))
						{
							num = 0.075f;
							num2 = 0.15f;
							num3 = 0.4f;
						}
						break;
					case 176:
						if (!this.LightIsBlocked(tile))
						{
							num = 0.1f;
							num2 = 0.1f;
							num3 = 0.1f;
						}
						break;
					}
					break;
				}
			}
			else if (num5 != 182)
			{
				switch (num5)
				{
				case 341:
					if (!this.LightIsBlocked(tile))
					{
						num = 0.25f;
						num2 = 0.1f;
						num3 = 0f;
					}
					break;
				case 342:
					if (!this.LightIsBlocked(tile))
					{
						num = 0.3f;
						num2 = 0f;
						num3 = 0.17f;
					}
					break;
				case 343:
					if (!this.LightIsBlocked(tile))
					{
						num = 0f;
						num2 = 0.25f;
						num3 = 0f;
					}
					break;
				case 344:
					if (!this.LightIsBlocked(tile))
					{
						num = 0f;
						num2 = 0.16f;
						num3 = 0.34f;
					}
					break;
				case 345:
					if (!this.LightIsBlocked(tile))
					{
						num = 0.3f;
						num2 = 0f;
						num3 = 0.35f;
					}
					break;
				case 346:
					if (!this.LightIsBlocked(tile))
					{
						num = (float)Main.DiscoR / 255f * 0.25f;
						num2 = (float)Main.DiscoG / 255f * 0.25f;
						num3 = (float)Main.DiscoB / 255f * 0.25f;
					}
					break;
				}
			}
			else if (!this.LightIsBlocked(tile))
			{
				num = 0.24f;
				num2 = 0.12f;
				num3 = 0.089999996f;
			}
			WallLoader.ModifyLight(x, y, (int)(*tile.wall), ref num, ref num2, ref num3);
			if (lightColor.X < num)
			{
				lightColor.X = num;
			}
			if (lightColor.Y < num2)
			{
				lightColor.Y = num2;
			}
			if (lightColor.Z < num3)
			{
				lightColor.Z = num3;
			}
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x00582A2C File Offset: 0x00580C2C
		private unsafe void ApplyTileLight(Tile tile, int x, int y, ref FastRandom localRandom, ref Vector3 lightColor)
		{
			float R = 0f;
			float G = 0f;
			float B = 0f;
			if (Main.tileLighted[(int)(*tile.type)])
			{
				ushort num39 = *tile.type;
				if (num39 <= 302)
				{
					if (num39 <= 129)
					{
						if (num39 <= 49)
						{
							if (num39 <= 20)
							{
								if (num39 != 4)
								{
									if (num39 == 17)
									{
										goto IL_2869;
									}
									if (num39 != 20)
									{
										goto IL_351D;
									}
									int num22 = (int)(*tile.frameX / 18);
									if (num22 >= 30 && num22 <= 32)
									{
										R = 0.325f;
										G = 0.15f;
										B = 0.05f;
										goto IL_351D;
									}
									goto IL_351D;
								}
								else
								{
									if (*tile.frameX < 66)
									{
										TorchID.TorchColor((int)(*tile.frameY / 22), out R, out G, out B);
										goto IL_351D;
									}
									goto IL_351D;
								}
							}
							else if (num39 <= 37)
							{
								if (num39 == 22)
								{
									goto IL_28AE;
								}
								switch (num39)
								{
								case 26:
								case 31:
								{
									if ((*tile.type == 31 && *tile.frameX >= 36) || (*tile.type == 26 && *tile.frameX >= 54))
									{
										float num23 = (float)localRandom.Next(-5, 6) * 0.0025f;
										R = 0.5f + num23 * 2f;
										G = 0.2f + num23;
										B = 0.1f;
										goto IL_351D;
									}
									float num24 = (float)localRandom.Next(-5, 6) * 0.0025f;
									R = 0.31f + num24;
									G = 0.1f;
									B = 0.44f + num24 * 2f;
									goto IL_351D;
								}
								case 27:
									if (*tile.frameY < 36)
									{
										R = 0.3f;
										G = 0.27f;
										goto IL_351D;
									}
									goto IL_351D;
								case 28:
								case 29:
								case 30:
								case 32:
								case 36:
									goto IL_351D;
								case 33:
									if (*tile.frameX == 0)
									{
										switch (*tile.frameY / 22)
										{
										case 0:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 1:
											R = 0.55f;
											G = 0.85f;
											B = 0.35f;
											goto IL_351D;
										case 2:
											R = 0.65f;
											G = 0.95f;
											B = 0.5f;
											goto IL_351D;
										case 3:
											R = 0.2f;
											G = 0.75f;
											B = 1f;
											goto IL_351D;
										case 5:
											R = 0.85f;
											G = 0.6f;
											B = 1f;
											goto IL_351D;
										case 7:
										case 8:
											R = 0.75f;
											G = 0.85f;
											B = 1f;
											goto IL_351D;
										case 9:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 10:
											R = 1f;
											G = 0.97f;
											B = 0.85f;
											goto IL_351D;
										case 14:
											R = 1f;
											G = 1f;
											B = 0.6f;
											goto IL_351D;
										case 15:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 18:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 19:
											R = 0.37f;
											G = 0.8f;
											B = 1f;
											goto IL_351D;
										case 20:
											R = 0f;
											G = 0.9f;
											B = 1f;
											goto IL_351D;
										case 21:
											R = 0.25f;
											G = 0.7f;
											B = 1f;
											goto IL_351D;
										case 23:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 24:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 25:
											R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
											G = 0.3f;
											B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
											goto IL_351D;
										case 28:
											R = 0.9f;
											G = 0.75f;
											B = 1f;
											goto IL_351D;
										case 29:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 30:
										{
											Vector3 vector = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
											R = vector.X;
											G = vector.Y;
											B = vector.Z;
											goto IL_351D;
										}
										case 31:
											R = 1f;
											G = 0.97f;
											B = 0.85f;
											goto IL_351D;
										case 32:
											R = 0.55f;
											G = 0.45f;
											B = 0.95f;
											goto IL_351D;
										case 33:
											R = 1f;
											G = 0.6f;
											B = 0.1f;
											goto IL_351D;
										case 34:
											R = 0.3f;
											G = 0.75f;
											B = 0.55f;
											goto IL_351D;
										case 35:
											R = 0.9f;
											G = 0.55f;
											B = 0.7f;
											goto IL_351D;
										case 36:
											R = 0.55f;
											G = 0.85f;
											B = 1f;
											goto IL_351D;
										case 37:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 38:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 39:
											R = 0.4f;
											G = 0.8f;
											B = 0.9f;
											goto IL_351D;
										case 40:
											R = 1f;
											G = 1f;
											B = 1f;
											goto IL_351D;
										case 41:
											R = 0.95f;
											G = 0.5f;
											B = 0.4f;
											goto IL_351D;
										}
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									}
									goto IL_351D;
								case 34:
									if (*tile.frameX % 108 < 54)
									{
										switch (*tile.frameY / 54 + 37 * (*tile.frameX / 108))
										{
										case 7:
											R = 0.95f;
											G = 0.95f;
											B = 0.5f;
											goto IL_351D;
										case 8:
											R = 0.85f;
											G = 0.6f;
											B = 1f;
											goto IL_351D;
										case 9:
											R = 1f;
											G = 0.6f;
											B = 0.6f;
											goto IL_351D;
										case 11:
										case 17:
											R = 0.75f;
											G = 0.85f;
											B = 1f;
											goto IL_351D;
										case 12:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 13:
											R = 1f;
											G = 0.97f;
											B = 0.85f;
											goto IL_351D;
										case 15:
											R = 1f;
											G = 1f;
											B = 0.7f;
											goto IL_351D;
										case 16:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 18:
											R = 1f;
											G = 1f;
											B = 0.6f;
											goto IL_351D;
										case 19:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 23:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 24:
											R = 0.37f;
											G = 0.8f;
											B = 1f;
											goto IL_351D;
										case 25:
											R = 0f;
											G = 0.9f;
											B = 1f;
											goto IL_351D;
										case 26:
											R = 0.25f;
											G = 0.7f;
											B = 1f;
											goto IL_351D;
										case 27:
											R = 0.55f;
											G = 0.85f;
											B = 0.35f;
											goto IL_351D;
										case 28:
											R = 0.65f;
											G = 0.95f;
											B = 0.5f;
											goto IL_351D;
										case 29:
											R = 0.2f;
											G = 0.75f;
											B = 1f;
											goto IL_351D;
										case 30:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 32:
											R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
											G = 0.3f;
											B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
											goto IL_351D;
										case 35:
											R = 0.9f;
											G = 0.75f;
											B = 1f;
											goto IL_351D;
										case 36:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 37:
										{
											Vector3 vector2 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
											R = vector2.X;
											G = vector2.Y;
											B = vector2.Z;
											goto IL_351D;
										}
										case 38:
											R = 1f;
											G = 0.97f;
											B = 0.85f;
											goto IL_351D;
										case 39:
											R = 0.55f;
											G = 0.45f;
											B = 0.95f;
											goto IL_351D;
										case 40:
											R = 1f;
											G = 0.6f;
											B = 0.1f;
											goto IL_351D;
										case 41:
											R = 0.3f;
											G = 0.75f;
											B = 0.55f;
											goto IL_351D;
										case 42:
											R = 0.9f;
											G = 0.55f;
											B = 0.7f;
											goto IL_351D;
										case 43:
											R = 0.55f;
											G = 0.85f;
											B = 1f;
											goto IL_351D;
										case 44:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 45:
											R = 1f;
											G = 0.95f;
											B = 0.65f;
											goto IL_351D;
										case 46:
											R = 0.4f;
											G = 0.8f;
											B = 0.9f;
											goto IL_351D;
										case 47:
											R = 1f;
											G = 1f;
											B = 1f;
											goto IL_351D;
										case 48:
											R = 0.95f;
											G = 0.5f;
											B = 0.4f;
											goto IL_351D;
										}
										R = 1f;
										G = 0.95f;
										B = 0.8f;
										goto IL_351D;
									}
									goto IL_351D;
								case 35:
									if (*tile.frameX < 36)
									{
										R = 0.75f;
										G = 0.6f;
										B = 0.3f;
										goto IL_351D;
									}
									goto IL_351D;
								case 37:
									R = 0.56f;
									G = 0.43f;
									B = 0.15f;
									goto IL_351D;
								default:
									goto IL_351D;
								}
							}
							else if (num39 != 42)
							{
								if (num39 != 49)
								{
									goto IL_351D;
								}
								if (*tile.frameX == 0)
								{
									R = 0f;
									G = 0.35f;
									B = 0.8f;
									goto IL_351D;
								}
								goto IL_351D;
							}
							else
							{
								if (*tile.frameX == 0)
								{
									switch (*tile.frameY / 36)
									{
									case 0:
										R = 0.7f;
										G = 0.65f;
										B = 0.55f;
										goto IL_351D;
									case 1:
										R = 0.9f;
										G = 0.75f;
										B = 0.6f;
										goto IL_351D;
									case 2:
										R = 0.8f;
										G = 0.6f;
										B = 0.6f;
										goto IL_351D;
									case 3:
										R = 0.65f;
										G = 0.5f;
										B = 0.2f;
										goto IL_351D;
									case 4:
										R = 0.5f;
										G = 0.7f;
										B = 0.4f;
										goto IL_351D;
									case 5:
										R = 0.9f;
										G = 0.4f;
										B = 0.2f;
										goto IL_351D;
									case 6:
										R = 0.7f;
										G = 0.75f;
										B = 0.3f;
										goto IL_351D;
									case 7:
									{
										float num25 = Main.demonTorch * 0.2f;
										R = 0.9f - num25;
										G = 0.9f - num25;
										B = 0.7f + num25;
										goto IL_351D;
									}
									case 8:
										R = 0.75f;
										G = 0.6f;
										B = 0.3f;
										goto IL_351D;
									case 9:
										R = 1f;
										G = 0.3f;
										B = 0.5f;
										B += Main.demonTorch * 0.2f;
										R -= Main.demonTorch * 0.1f;
										G -= Main.demonTorch * 0.2f;
										goto IL_351D;
									case 11:
										R = 0.85f;
										G = 0.6f;
										B = 1f;
										goto IL_351D;
									case 14:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 15:
									case 16:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 17:
										R = 1f;
										G = 0.97f;
										B = 0.85f;
										goto IL_351D;
									case 18:
										R = 0.75f;
										G = 0.85f;
										B = 1f;
										goto IL_351D;
									case 21:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 22:
										R = 1f;
										G = 1f;
										B = 0.6f;
										goto IL_351D;
									case 23:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 27:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 28:
										R = 0.37f;
										G = 0.8f;
										B = 1f;
										goto IL_351D;
									case 29:
										R = 0f;
										G = 0.9f;
										B = 1f;
										goto IL_351D;
									case 30:
										R = 0.25f;
										G = 0.7f;
										B = 1f;
										goto IL_351D;
									case 32:
										R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
										G = 0.3f;
										B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
										goto IL_351D;
									case 35:
										R = 0.7f;
										G = 0.6f;
										B = 0.9f;
										goto IL_351D;
									case 36:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 37:
									{
										Vector3 vector3 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
										R = vector3.X;
										G = vector3.Y;
										B = vector3.Z;
										goto IL_351D;
									}
									case 38:
										R = 1f;
										G = 0.97f;
										B = 0.85f;
										goto IL_351D;
									case 39:
										R = 0.55f;
										G = 0.45f;
										B = 0.95f;
										goto IL_351D;
									case 40:
										R = 1f;
										G = 0.6f;
										B = 0.1f;
										goto IL_351D;
									case 41:
										R = 0.3f;
										G = 0.75f;
										B = 0.55f;
										goto IL_351D;
									case 42:
										R = 0.9f;
										G = 0.55f;
										B = 0.7f;
										goto IL_351D;
									case 43:
										R = 0.55f;
										G = 0.85f;
										B = 1f;
										goto IL_351D;
									case 44:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 45:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 46:
										R = 0.4f;
										G = 0.8f;
										B = 0.9f;
										goto IL_351D;
									case 47:
										R = 1f;
										G = 1f;
										B = 1f;
										goto IL_351D;
									case 48:
										R = 0.95f;
										G = 0.5f;
										B = 0.4f;
										goto IL_351D;
									}
									R = 1f;
									G = 1f;
									B = 1f;
									goto IL_351D;
								}
								goto IL_351D;
							}
						}
						else if (num39 <= 77)
						{
							if (num39 != 61)
							{
								if (num39 - 70 <= 2)
								{
									goto IL_2FD8;
								}
								if (num39 != 77)
								{
									goto IL_351D;
								}
								R = 0.75f;
								G = 0.45f;
								B = 0.25f;
								goto IL_351D;
							}
							else
							{
								if (*tile.frameX == 144)
								{
									float num26 = 1f + (float)(270 - (int)Main.mouseTextColor) / 400f;
									float num27 = 0.8f - (float)(270 - (int)Main.mouseTextColor) / 400f;
									R = 0.42f * num27;
									G = 0.81f * num26;
									B = 0.52f * num27;
									goto IL_351D;
								}
								goto IL_351D;
							}
						}
						else if (num39 <= 84)
						{
							if (num39 != 83)
							{
								if (num39 != 84)
								{
									goto IL_351D;
								}
								switch (*tile.frameX / 18)
								{
								case 2:
								{
									float num28 = (float)(270 - (int)Main.mouseTextColor) / 800f;
									if (num28 > 1f)
									{
										num28 = 1f;
									}
									else if (num28 < 0f)
									{
										num28 = 0f;
									}
									R = num28 * 0.7f;
									G = num28;
									B = num28 * 0.1f;
									goto IL_351D;
								}
								case 3:
								case 4:
									goto IL_351D;
								case 5:
								{
									float num28 = 0.9f;
									R = num28;
									G = num28 * 0.8f;
									B = num28 * 0.2f;
									goto IL_351D;
								}
								case 6:
								{
									float num28 = 0.08f;
									G = num28 * 0.8f;
									B = num28;
									goto IL_351D;
								}
								default:
									goto IL_351D;
								}
							}
							else
							{
								if (*tile.frameX == 18 && !Main.dayTime)
								{
									R = 0.1f;
									G = 0.4f;
									B = 0.6f;
								}
								if (*tile.frameX == 90 && !Main.raining && Main.time > 40500.0)
								{
									R = 0.9f;
									G = 0.72f;
									B = 0.18f;
									goto IL_351D;
								}
								goto IL_351D;
							}
						}
						else
						{
							switch (num39)
							{
							case 92:
								if (*tile.frameY <= 18 && *tile.frameX == 0)
								{
									R = 1f;
									G = 1f;
									B = 1f;
									goto IL_351D;
								}
								goto IL_351D;
							case 93:
								if (*tile.frameX == 0)
								{
									switch (*tile.frameY / 54)
									{
									case 1:
										R = 0.95f;
										G = 0.95f;
										B = 0.5f;
										goto IL_351D;
									case 2:
										R = 0.85f;
										G = 0.6f;
										B = 1f;
										goto IL_351D;
									case 3:
										R = 0.75f;
										G = 1f;
										B = 0.6f;
										goto IL_351D;
									case 4:
									case 5:
										R = 0.75f;
										G = 0.85f;
										B = 1f;
										goto IL_351D;
									case 6:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 7:
										R = 1f;
										G = 0.97f;
										B = 0.85f;
										goto IL_351D;
									case 9:
										R = 1f;
										G = 1f;
										B = 0.7f;
										goto IL_351D;
									case 10:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 12:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 13:
										R = 1f;
										G = 1f;
										B = 0.6f;
										goto IL_351D;
									case 14:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 18:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 19:
										R = 0.37f;
										G = 0.8f;
										B = 1f;
										goto IL_351D;
									case 20:
										R = 0f;
										G = 0.9f;
										B = 1f;
										goto IL_351D;
									case 21:
										R = 0.25f;
										G = 0.7f;
										B = 1f;
										goto IL_351D;
									case 23:
										R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
										G = 0.3f;
										B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
										goto IL_351D;
									case 24:
										R = 0.35f;
										G = 0.5f;
										B = 0.3f;
										goto IL_351D;
									case 25:
										R = 0.34f;
										G = 0.4f;
										B = 0.31f;
										goto IL_351D;
									case 26:
										R = 0.25f;
										G = 0.32f;
										B = 0.5f;
										goto IL_351D;
									case 29:
										R = 0.9f;
										G = 0.75f;
										B = 1f;
										goto IL_351D;
									case 30:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 31:
									{
										Vector3 vector4 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
										R = vector4.X;
										G = vector4.Y;
										B = vector4.Z;
										goto IL_351D;
									}
									case 32:
										R = 1f;
										G = 0.97f;
										B = 0.85f;
										goto IL_351D;
									case 33:
										R = 0.55f;
										G = 0.45f;
										B = 0.95f;
										goto IL_351D;
									case 34:
										R = 1f;
										G = 0.6f;
										B = 0.1f;
										goto IL_351D;
									case 35:
										R = 0.3f;
										G = 0.75f;
										B = 0.55f;
										goto IL_351D;
									case 36:
										R = 0.9f;
										G = 0.55f;
										B = 0.7f;
										goto IL_351D;
									case 37:
										R = 0.55f;
										G = 0.85f;
										B = 1f;
										goto IL_351D;
									case 38:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 39:
										R = 1f;
										G = 0.95f;
										B = 0.65f;
										goto IL_351D;
									case 40:
										R = 0.4f;
										G = 0.8f;
										B = 0.9f;
										goto IL_351D;
									case 41:
										R = 1f;
										G = 1f;
										B = 1f;
										goto IL_351D;
									case 42:
										R = 0.95f;
										G = 0.5f;
										B = 0.4f;
										goto IL_351D;
									}
									R = 1f;
									G = 0.97f;
									B = 0.85f;
									goto IL_351D;
								}
								goto IL_351D;
							case 94:
							case 97:
							case 99:
								goto IL_351D;
							case 95:
								if (*tile.frameX < 36)
								{
									R = 1f;
									G = 0.95f;
									B = 0.8f;
									goto IL_351D;
								}
								goto IL_351D;
							case 96:
								if (*tile.frameX >= 36)
								{
									R = 0.5f;
									G = 0.35f;
									B = 0.1f;
									goto IL_351D;
								}
								goto IL_351D;
							case 98:
								if (*tile.frameY == 0)
								{
									R = 1f;
									G = 0.97f;
									B = 0.85f;
									goto IL_351D;
								}
								goto IL_351D;
							case 100:
								break;
							default:
								switch (num39)
								{
								case 125:
								{
									float num29 = (float)localRandom.Next(28, 42) * 0.01f;
									num29 += (float)(270 - (int)Main.mouseTextColor) / 800f;
									G = (lightColor.Y = 0.3f * num29);
									B = (lightColor.Z = 0.6f * num29);
									goto IL_351D;
								}
								case 126:
									if (*tile.frameX < 36)
									{
										R = (float)Main.DiscoR / 255f;
										G = (float)Main.DiscoG / 255f;
										B = (float)Main.DiscoB / 255f;
										goto IL_351D;
									}
									goto IL_351D;
								case 127:
								case 128:
									goto IL_351D;
								case 129:
									switch (*tile.frameX / 18 % 3)
									{
									case 0:
										R = 0f;
										G = 0.05f;
										B = 0.25f;
										goto IL_351D;
									case 1:
										R = 0.2f;
										G = 0f;
										B = 0.15f;
										goto IL_351D;
									case 2:
										R = 0.1f;
										G = 0f;
										B = 0.2f;
										goto IL_351D;
									default:
										goto IL_351D;
									}
									break;
								default:
									goto IL_351D;
								}
								break;
							}
						}
					}
					else if (num39 <= 190)
					{
						if (num39 <= 149)
						{
							if (num39 == 133)
							{
								goto IL_2869;
							}
							if (num39 == 140)
							{
								goto IL_28AE;
							}
							if (num39 != 149)
							{
								goto IL_351D;
							}
							if (*tile.frameX <= 36)
							{
								switch (*tile.frameX / 18)
								{
								case 0:
									R = 0.1f;
									G = 0.2f;
									B = 0.5f;
									break;
								case 1:
									R = 0.5f;
									G = 0.1f;
									B = 0.1f;
									break;
								case 2:
									R = 0.2f;
									G = 0.5f;
									B = 0.1f;
									break;
								}
								R *= (float)localRandom.Next(970, 1031) * 0.001f;
								G *= (float)localRandom.Next(970, 1031) * 0.001f;
								B *= (float)localRandom.Next(970, 1031) * 0.001f;
								goto IL_351D;
							}
							goto IL_351D;
						}
						else if (num39 <= 174)
						{
							if (num39 == 160)
							{
								R = (float)Main.DiscoR / 255f * 0.25f;
								G = (float)Main.DiscoG / 255f * 0.25f;
								B = (float)Main.DiscoB / 255f * 0.25f;
								goto IL_351D;
							}
							switch (num39)
							{
							case 171:
								if (*tile.frameX < 10)
								{
									x -= (int)(*tile.frameX);
									y -= (int)(*tile.frameY);
								}
								switch ((*Main.tile[x, y].frameY & 15360) >> 10)
								{
								case 1:
									R = 0.1f;
									G = 0.1f;
									B = 0.1f;
									break;
								case 2:
									R = 0.2f;
									break;
								case 3:
									G = 0.2f;
									break;
								case 4:
									B = 0.2f;
									break;
								case 5:
									R = 0.125f;
									G = 0.125f;
									break;
								case 6:
									R = 0.2f;
									G = 0.1f;
									break;
								case 7:
									R = 0.125f;
									G = 0.125f;
									break;
								case 8:
									R = 0.08f;
									G = 0.175f;
									break;
								case 9:
									G = 0.125f;
									B = 0.125f;
									break;
								case 10:
									R = 0.125f;
									B = 0.125f;
									break;
								case 11:
									R = 0.1f;
									G = 0.1f;
									B = 0.2f;
									break;
								default:
									G = (R = (B = 0f));
									break;
								}
								R *= 0.5f;
								G *= 0.5f;
								B *= 0.5f;
								goto IL_351D;
							case 172:
								goto IL_351D;
							case 173:
								break;
							case 174:
								if (*tile.frameX == 0)
								{
									R = 1f;
									G = 0.95f;
									B = 0.65f;
									goto IL_351D;
								}
								goto IL_351D;
							default:
								goto IL_351D;
							}
						}
						else if (num39 != 184)
						{
							if (num39 != 190)
							{
								goto IL_351D;
							}
							goto IL_2FD8;
						}
						else
						{
							if (*tile.frameX == 110)
							{
								R = 0.25f;
								G = 0.1f;
								B = 0f;
							}
							if (*tile.frameX == 132)
							{
								R = 0f;
								G = 0.25f;
								B = 0f;
							}
							if (*tile.frameX == 154)
							{
								R = 0f;
								G = 0.16f;
								B = 0.34f;
							}
							if (*tile.frameX == 176)
							{
								R = 0.3f;
								G = 0f;
								B = 0.17f;
							}
							if (*tile.frameX == 198)
							{
								R = 0.3f;
								G = 0f;
								B = 0.35f;
							}
							if (*tile.frameX == 220)
							{
								R = (float)Main.DiscoR / 255f * 0.25f;
								G = (float)Main.DiscoG / 255f * 0.25f;
								B = (float)Main.DiscoB / 255f * 0.25f;
								goto IL_351D;
							}
							goto IL_351D;
						}
					}
					else if (num39 <= 215)
					{
						if (num39 == 204)
						{
							goto IL_2A05;
						}
						if (num39 != 209)
						{
							if (num39 != 215)
							{
								goto IL_351D;
							}
							if (*tile.frameY < 36)
							{
								float num30 = (float)localRandom.Next(28, 42) * 0.005f;
								num30 += (float)(270 - (int)Main.mouseTextColor) / 700f;
								switch (*tile.frameX / 54)
								{
								case 1:
									R = 0.7f;
									G = 1f;
									B = 0.5f;
									break;
								case 2:
									R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
									G = 0.3f;
									B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
									break;
								case 3:
									R = 0.45f;
									G = 0.75f;
									B = 1f;
									break;
								case 4:
									R = 1.15f;
									G = 1.15f;
									B = 0.5f;
									break;
								case 5:
									R = (float)Main.DiscoR / 255f;
									G = (float)Main.DiscoG / 255f;
									B = (float)Main.DiscoB / 255f;
									break;
								case 6:
									R = 0.75f;
									G = 1.2824999f;
									B = 1.2f;
									break;
								case 7:
									R = 0.95f;
									G = 0.65f;
									B = 1.3f;
									break;
								case 8:
									R = 1.4f;
									G = 0.85f;
									B = 0.55f;
									break;
								case 9:
									R = 0.25f;
									G = 1.3f;
									B = 0.8f;
									break;
								case 10:
									R = 0.95f;
									G = 0.4f;
									B = 1.4f;
									break;
								case 11:
									R = 1.4f;
									G = 0.7f;
									B = 0.5f;
									break;
								case 12:
									R = 1.25f;
									G = 0.6f;
									B = 1.2f;
									break;
								case 13:
									R = 0.75f;
									G = 1.45f;
									B = 0.9f;
									break;
								case 14:
									R = 0.25f;
									G = 0.65f;
									B = 1f;
									break;
								case 15:
									TorchID.TorchColor(23, out R, out G, out B);
									break;
								default:
									R = 0.9f;
									G = 0.3f;
									B = 0.1f;
									break;
								}
								R += num30;
								G += num30;
								B += num30;
								goto IL_351D;
							}
							goto IL_351D;
						}
						else
						{
							if (*tile.frameX == 234 || *tile.frameX == 252)
							{
								Vector3 vector5 = PortalHelper.GetPortalColor(Main.myPlayer, 0).ToVector3() * 0.65f;
								R = vector5.X;
								G = vector5.Y;
								B = vector5.Z;
								goto IL_351D;
							}
							if (*tile.frameX == 306 || *tile.frameX == 324)
							{
								Vector3 vector6 = PortalHelper.GetPortalColor(Main.myPlayer, 1).ToVector3() * 0.65f;
								R = vector6.X;
								G = vector6.Y;
								B = vector6.Z;
								goto IL_351D;
							}
							goto IL_351D;
						}
					}
					else if (num39 <= 271)
					{
						switch (num39)
						{
						case 235:
							if ((double)lightColor.X < 0.6)
							{
								lightColor.X = 0.6f;
							}
							if ((double)lightColor.Y < 0.6)
							{
								lightColor.Y = 0.6f;
								goto IL_351D;
							}
							goto IL_351D;
						case 236:
							goto IL_351D;
						case 237:
							R = 0.1f;
							G = 0.1f;
							goto IL_351D;
						case 238:
							if ((double)lightColor.X < 0.5)
							{
								lightColor.X = 0.5f;
							}
							if ((double)lightColor.Z < 0.5)
							{
								lightColor.Z = 0.5f;
								goto IL_351D;
							}
							goto IL_351D;
						default:
							switch (num39)
							{
							case 262:
								R = 0.75f;
								B = 0.75f;
								goto IL_351D;
							case 263:
								R = 0.75f;
								G = 0.75f;
								goto IL_351D;
							case 264:
								B = 0.75f;
								goto IL_351D;
							case 265:
								G = 0.75f;
								goto IL_351D;
							case 266:
								R = 0.75f;
								goto IL_351D;
							case 267:
								R = 0.75f;
								G = 0.75f;
								B = 0.75f;
								goto IL_351D;
							case 268:
								R = 0.75f;
								G = 0.375f;
								goto IL_351D;
							case 269:
								goto IL_351D;
							case 270:
								R = 0.73f;
								G = 1f;
								B = 0.41f;
								goto IL_351D;
							case 271:
								R = 0.45f;
								G = 0.95f;
								B = 1f;
								goto IL_351D;
							default:
								goto IL_351D;
							}
							break;
						}
					}
					else
					{
						if (num39 == 286)
						{
							goto IL_ED8;
						}
						if (num39 != 302)
						{
							goto IL_351D;
						}
						goto IL_2869;
					}
					if (*tile.frameX < 36)
					{
						switch (*tile.frameY / 36)
						{
						case 1:
							R = 0.95f;
							G = 0.95f;
							B = 0.5f;
							goto IL_351D;
						case 2:
							R = 0.85f;
							G = 0.6f;
							B = 1f;
							goto IL_351D;
						case 3:
							R = 1f;
							G = 0.6f;
							B = 0.6f;
							goto IL_351D;
						case 5:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 6:
						case 7:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 8:
							R = 1f;
							G = 0.97f;
							B = 0.85f;
							goto IL_351D;
						case 9:
							R = 0.75f;
							G = 0.85f;
							B = 1f;
							goto IL_351D;
						case 11:
							R = 1f;
							G = 1f;
							B = 0.7f;
							goto IL_351D;
						case 12:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 13:
							R = 1f;
							G = 1f;
							B = 0.6f;
							goto IL_351D;
						case 14:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 18:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 19:
							R = 0.37f;
							G = 0.8f;
							B = 1f;
							goto IL_351D;
						case 20:
							R = 0f;
							G = 0.9f;
							B = 1f;
							goto IL_351D;
						case 21:
							R = 0.25f;
							G = 0.7f;
							B = 1f;
							goto IL_351D;
						case 22:
							R = 0.35f;
							G = 0.5f;
							B = 0.3f;
							goto IL_351D;
						case 23:
							R = 0.34f;
							G = 0.4f;
							B = 0.31f;
							goto IL_351D;
						case 24:
							R = 0.25f;
							G = 0.32f;
							B = 0.5f;
							goto IL_351D;
						case 25:
							R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
							G = 0.3f;
							B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
							goto IL_351D;
						case 29:
							R = 0.9f;
							G = 0.75f;
							B = 1f;
							goto IL_351D;
						case 30:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 31:
						{
							Vector3 vector7 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
							R = vector7.X;
							G = vector7.Y;
							B = vector7.Z;
							goto IL_351D;
						}
						case 32:
							R = 1f;
							G = 0.97f;
							B = 0.85f;
							goto IL_351D;
						case 33:
							R = 0.55f;
							G = 0.45f;
							B = 0.95f;
							goto IL_351D;
						case 34:
							R = 1f;
							G = 0.6f;
							B = 0.1f;
							goto IL_351D;
						case 35:
							R = 0.3f;
							G = 0.75f;
							B = 0.55f;
							goto IL_351D;
						case 36:
							R = 0.9f;
							G = 0.55f;
							B = 0.7f;
							goto IL_351D;
						case 37:
							R = 0.55f;
							G = 0.85f;
							B = 1f;
							goto IL_351D;
						case 38:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 39:
							R = 1f;
							G = 0.95f;
							B = 0.65f;
							goto IL_351D;
						case 40:
							R = 0.4f;
							G = 0.8f;
							B = 0.9f;
							goto IL_351D;
						case 41:
							R = 1f;
							G = 1f;
							B = 1f;
							goto IL_351D;
						case 42:
							R = 0.95f;
							G = 0.5f;
							B = 0.4f;
							goto IL_351D;
						}
						R = 1f;
						G = 0.95f;
						B = 0.65f;
						goto IL_351D;
					}
					goto IL_351D;
					IL_2869:
					R = 0.83f;
					G = 0.6f;
					B = 0.5f;
					goto IL_351D;
					IL_28AE:
					R = 0.12f;
					G = 0.07f;
					B = 0.32f;
					goto IL_351D;
				}
				if (num39 > 503)
				{
					if (num39 > 598)
					{
						if (num39 > 638)
						{
							if (num39 <= 663)
							{
								if (num39 != 646)
								{
									switch (num39)
									{
									case 656:
										R = 0.2f;
										G = 0.55f;
										B = 0.5f;
										goto IL_351D;
									case 657:
									case 661:
									case 662:
										goto IL_351D;
									case 658:
									{
										if (tile.invisibleBlock())
										{
											goto IL_351D;
										}
										TorchID.TorchColor(23, out R, out G, out B);
										int num40 = (int)(*tile.frameY / 54);
										if (num40 == 1)
										{
											R *= 0.3f;
											G *= 0.3f;
											B *= 0.3f;
											goto IL_351D;
										}
										if (num40 != 2)
										{
											R *= 0.2f;
											G *= 0.2f;
											B *= 0.2f;
											goto IL_351D;
										}
										R *= 0.1f;
										G *= 0.1f;
										B *= 0.1f;
										goto IL_351D;
									}
									case 659:
										break;
									case 660:
										TorchID.TorchColor(23, out R, out G, out B);
										goto IL_351D;
									case 663:
										if (Main.moondialCooldown == 0)
										{
											R = 0f;
											G = 0.25f;
											B = 0.45f;
											goto IL_351D;
										}
										goto IL_351D;
									default:
										goto IL_351D;
									}
								}
								else
								{
									if (*tile.frameX == 0)
									{
										R = 0.2f;
										G = 0.3f;
										B = 0.32f;
										goto IL_351D;
									}
									goto IL_351D;
								}
							}
							else if (num39 != 667)
							{
								switch (num39)
								{
								case 687:
									goto IL_A57;
								case 688:
									goto IL_A9C;
								case 689:
									goto IL_A6E;
								case 690:
									goto IL_A85;
								case 691:
									goto IL_AB3;
								case 692:
									goto IL_ACA;
								default:
									goto IL_351D;
								}
							}
							Vector4 shimmerBaseColor = LiquidRenderer.GetShimmerBaseColor((float)x, (float)y);
							R = shimmerBaseColor.X;
							G = shimmerBaseColor.Y;
							B = shimmerBaseColor.Z;
							goto IL_351D;
						}
						if (num39 - 613 <= 1)
						{
							R = 0.7f;
							G = 0.3f;
							B = 0.2f;
							goto IL_351D;
						}
						switch (num39)
						{
						case 619:
							goto IL_ED8;
						case 620:
						{
							Color color = new Color(230, 230, 230, 0).MultiplyRGBA(Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.5f % 1f, 1f, 0.5f, byte.MaxValue));
							color *= 0.4f;
							R = (float)color.R / 255f;
							G = (float)color.G / 255f;
							B = (float)color.B / 255f;
							goto IL_351D;
						}
						case 621:
						case 622:
						case 623:
						case 624:
							goto IL_351D;
						case 625:
						case 626:
							break;
						case 627:
						case 628:
							goto IL_ACA;
						default:
							switch (num39)
							{
							case 633:
							case 637:
							case 638:
								R = 0.325f;
								G = 0.15f;
								B = 0.05f;
								goto IL_351D;
							case 634:
								R = 0.65f;
								G = 0.3f;
								B = 0.1f;
								goto IL_351D;
							case 635:
							case 636:
								goto IL_351D;
							default:
								goto IL_351D;
							}
							break;
						}
						IL_AB3:
						R = 0.3f;
						G = 0f;
						B = 0.35f;
						goto IL_351D;
						IL_ACA:
						R = (float)Main.DiscoR / 255f * 0.25f;
						G = (float)Main.DiscoG / 255f * 0.25f;
						B = (float)Main.DiscoB / 255f * 0.25f;
						goto IL_351D;
					}
					if (num39 <= 540)
					{
						if (num39 == 517)
						{
							goto IL_A57;
						}
						if (num39 != 519)
						{
							switch (num39)
							{
							case 528:
								goto IL_2FD8;
							case 529:
							case 530:
							case 531:
							case 532:
							case 533:
							case 538:
								goto IL_351D;
							case 534:
							case 535:
								break;
							case 536:
							case 537:
								goto IL_A85;
							case 539:
							case 540:
								goto IL_A9C;
							default:
								goto IL_351D;
							}
						}
						else
						{
							if (*tile.frameY != 90)
							{
								goto IL_351D;
							}
							if (tile.color() == 0)
							{
								float num31 = (float)localRandom.Next(28, 42) * 0.005f;
								num31 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
								R = 0.1f;
								G = 0.2f + num31 / 2f;
								B = 0.7f + num31;
								goto IL_351D;
							}
							Color color2 = WorldGen.paintColor((int)tile.color());
							R = (float)color2.R / 255f;
							G = (float)color2.G / 255f;
							B = (float)color2.B / 255f;
							goto IL_351D;
						}
					}
					else
					{
						if (num39 > 572)
						{
							switch (num39)
							{
							case 578:
								goto IL_2FD8;
							case 579:
								goto IL_351D;
							case 580:
								R = 0.7f;
								G = 0.3f;
								B = 0.2f;
								goto IL_351D;
							case 581:
								R = 1f;
								G = 0.75f;
								B = 0.5f;
								goto IL_351D;
							case 582:
								break;
							default:
								switch (num39)
								{
								case 592:
									if (*tile.frameY > 0)
									{
										float num32 = (float)localRandom.Next(28, 42) * 0.005f;
										num32 += (float)(270 - (int)Main.mouseTextColor) / 700f;
										R = 1.35f;
										G = 0.45f;
										B = 0.15f;
										R += num32;
										G += num32;
										B += num32;
										goto IL_351D;
									}
									goto IL_351D;
								case 593:
									if (*tile.frameX < 18)
									{
										R = 0.8f;
										G = 0.3f;
										B = 0.1f;
										goto IL_351D;
									}
									goto IL_351D;
								case 594:
									if (*tile.frameX < 36)
									{
										R = 0.8f;
										G = 0.3f;
										B = 0.1f;
										goto IL_351D;
									}
									goto IL_351D;
								case 595:
								case 596:
									goto IL_351D;
								case 597:
									switch (*tile.frameX / 54)
									{
									case 0:
										R = 0.05f;
										G = 0.8f;
										B = 0.3f;
										break;
									case 1:
										R = 0.7f;
										G = 0.8f;
										B = 0.05f;
										break;
									case 2:
										R = 0.7f;
										G = 0.5f;
										B = 0.9f;
										break;
									case 3:
										R = 0.6f;
										G = 0.6f;
										B = 0.8f;
										break;
									case 4:
										R = 0.4f;
										G = 0.4f;
										B = 1.15f;
										break;
									case 5:
										R = 0.85f;
										G = 0.45f;
										B = 0.1f;
										break;
									case 6:
										R = 0.8f;
										G = 0.8f;
										B = 1f;
										break;
									case 7:
										R = 0.5f;
										G = 0.8f;
										B = 1.2f;
										break;
									}
									R *= 0.75f;
									G *= 0.75f;
									B *= 0.75f;
									goto IL_351D;
								case 598:
									break;
								default:
									goto IL_351D;
								}
								break;
							}
							R = 0.7f;
							G = 0.2f;
							B = 0.1f;
							goto IL_351D;
						}
						if (num39 != 548)
						{
							switch (num39)
							{
							case 564:
								if (*tile.frameX < 36)
								{
									R = 0.05f;
									G = 0.3f;
									B = 0.55f;
									goto IL_351D;
								}
								goto IL_351D;
							case 565:
							case 566:
							case 567:
							case 571:
								goto IL_351D;
							case 568:
								R = 1f;
								G = 0.61f;
								B = 0.65f;
								goto IL_351D;
							case 569:
								R = 0.12f;
								G = 1f;
								B = 0.66f;
								goto IL_351D;
							case 570:
								R = 0.57f;
								G = 0.57f;
								B = 1f;
								goto IL_351D;
							case 572:
								switch (*tile.frameY / 36)
								{
								case 0:
									R = 0.9f;
									G = 0.5f;
									B = 0.7f;
									goto IL_351D;
								case 1:
									R = 0.7f;
									G = 0.55f;
									B = 0.96f;
									goto IL_351D;
								case 2:
									R = 0.45f;
									G = 0.96f;
									B = 0.95f;
									goto IL_351D;
								case 3:
									R = 0.5f;
									G = 0.96f;
									B = 0.62f;
									goto IL_351D;
								case 4:
									R = 0.47f;
									G = 0.69f;
									B = 0.95f;
									goto IL_351D;
								case 5:
									R = 0.92f;
									G = 0.57f;
									B = 0.51f;
									goto IL_351D;
								default:
									goto IL_351D;
								}
								break;
							default:
								goto IL_351D;
							}
						}
						else
						{
							if (*tile.frameX / 54 >= 7)
							{
								R = 0.7f;
								G = 0.3f;
								B = 0.2f;
								goto IL_351D;
							}
							goto IL_351D;
						}
					}
					IL_A6E:
					R = 0f;
					G = 0.25f;
					B = 0f;
					goto IL_351D;
					IL_A85:
					R = 0f;
					G = 0.16f;
					B = 0.34f;
					goto IL_351D;
					IL_A9C:
					R = 0.3f;
					G = 0f;
					B = 0.17f;
					goto IL_351D;
				}
				if (num39 <= 390)
				{
					if (num39 <= 356)
					{
						if (num39 - 316 > 2)
						{
							if (num39 == 327)
							{
								float num33 = 0.5f;
								num33 += (float)(270 - (int)Main.mouseTextColor) / 1500f;
								num33 += (float)localRandom.Next(0, 50) * 0.0005f;
								R = 1f * num33;
								G = 0.5f * num33;
								B = 0.1f * num33;
								goto IL_351D;
							}
							switch (num39)
							{
							case 336:
								R = 0.85f;
								G = 0.5f;
								B = 0.3f;
								goto IL_351D;
							case 337:
							case 338:
							case 339:
							case 345:
							case 346:
							case 351:
							case 352:
							case 353:
							case 355:
								goto IL_351D;
							case 340:
								R = 0.45f;
								G = 1f;
								B = 0.45f;
								goto IL_351D;
							case 341:
								R = 0.4f * Main.demonTorch + 0.6f * (1f - Main.demonTorch);
								G = 0.35f;
								B = 1f * Main.demonTorch + 0.6f * (1f - Main.demonTorch);
								goto IL_351D;
							case 342:
								R = 0.5f;
								G = 0.5f;
								B = 1.1f;
								goto IL_351D;
							case 343:
								R = 0.85f;
								G = 0.85f;
								B = 0.3f;
								goto IL_351D;
							case 344:
								R = 0.6f;
								G = 1.026f;
								B = 0.96000004f;
								goto IL_351D;
							case 347:
								goto IL_2A05;
							case 348:
							case 349:
								goto IL_2FD8;
							case 350:
							{
								double num34 = Main.timeForVisualEffects * 0.08;
								G = (B = (R = (float)((0.0 - Math.Cos(((int)(num34 / 6.283) % 3 == 1) ? num34 : 0.0)) * 0.1 + 0.1)));
								goto IL_351D;
							}
							case 354:
								R = 0.65f;
								G = 0.35f;
								B = 0.15f;
								goto IL_351D;
							case 356:
								if (Main.sundialCooldown == 0)
								{
									R = 0.45f;
									G = 0.25f;
									B = 0f;
									goto IL_351D;
								}
								goto IL_351D;
							default:
								goto IL_351D;
							}
						}
						else
						{
							int num41 = x - (int)(*tile.frameX / 18);
							int num35 = y - (int)(*tile.frameY / 18);
							int num36 = num41 / 2 * (num35 / 3);
							num36 %= Main.cageFrames;
							bool flag4 = Main.jellyfishCageMode[(int)(*tile.type - 316), num36] == 2;
							if (*tile.type == 316)
							{
								if (flag4)
								{
									R = 0.2f;
									G = 0.3f;
									B = 0.8f;
								}
								else
								{
									R = 0.1f;
									G = 0.2f;
									B = 0.5f;
								}
							}
							if (*tile.type == 317)
							{
								if (flag4)
								{
									R = 0.2f;
									G = 0.7f;
									B = 0.3f;
								}
								else
								{
									R = 0.05f;
									G = 0.45f;
									B = 0.1f;
								}
							}
							if (*tile.type != 318)
							{
								goto IL_351D;
							}
							if (flag4)
							{
								R = 0.7f;
								G = 0.2f;
								B = 0.5f;
								goto IL_351D;
							}
							R = 0.4f;
							G = 0.1f;
							B = 0.25f;
							goto IL_351D;
						}
					}
					else if (num39 <= 372)
					{
						if (num39 == 370)
						{
							R = 0.32f;
							G = 0.16f;
							B = 0.12f;
							goto IL_351D;
						}
						if (num39 != 372)
						{
							goto IL_351D;
						}
						if (*tile.frameX == 0)
						{
							R = 0.9f;
							G = 0.1f;
							B = 0.75f;
							goto IL_351D;
						}
						goto IL_351D;
					}
					else if (num39 != 381)
					{
						if (num39 != 390)
						{
							goto IL_351D;
						}
						R = 0.4f;
						G = 0.2f;
						B = 0.1f;
						goto IL_351D;
					}
				}
				else if (num39 <= 418)
				{
					if (num39 == 391)
					{
						R = 0.3f;
						G = 0.1f;
						B = 0.25f;
						goto IL_351D;
					}
					if (num39 != 405)
					{
						switch (num39)
						{
						case 415:
							R = 0.7f;
							G = 0.5f;
							B = 0.1f;
							goto IL_351D;
						case 416:
							R = 0f;
							G = 0.6f;
							B = 0.7f;
							goto IL_351D;
						case 417:
							R = 0.6f;
							G = 0.2f;
							B = 0.6f;
							goto IL_351D;
						case 418:
							R = 0.6f;
							G = 0.6f;
							B = 0.9f;
							goto IL_351D;
						default:
							goto IL_351D;
						}
					}
					else
					{
						if (*tile.frameX < 54)
						{
							float num37 = (float)localRandom.Next(28, 42) * 0.005f;
							num37 += (float)(270 - (int)Main.mouseTextColor) / 700f;
							switch (*tile.frameX / 54)
							{
							case 1:
								R = 0.7f;
								G = 1f;
								B = 0.5f;
								break;
							case 2:
								R = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
								G = 0.3f;
								B = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
								break;
							case 3:
								R = 0.45f;
								G = 0.75f;
								B = 1f;
								break;
							case 4:
								R = 1.15f;
								G = 1.15f;
								B = 0.5f;
								break;
							case 5:
								R = (float)Main.DiscoR / 255f;
								G = (float)Main.DiscoG / 255f;
								B = (float)Main.DiscoB / 255f;
								break;
							default:
								R = 0.9f;
								G = 0.3f;
								B = 0.1f;
								break;
							}
							R += num37;
							G += num37;
							B += num37;
							goto IL_351D;
						}
						goto IL_351D;
					}
				}
				else if (num39 <= 463)
				{
					if (num39 != 429)
					{
						if (num39 != 463)
						{
							goto IL_351D;
						}
						R = 0.2f;
						G = 0.4f;
						B = 0.8f;
						goto IL_351D;
					}
					else
					{
						short num42 = *tile.frameX / 18;
						bool flag5 = num42 % 2 >= 1;
						bool flag6 = num42 % 4 >= 2;
						bool flag7 = num42 % 8 >= 4;
						bool flag8 = num42 % 16 >= 8;
						if (flag5)
						{
							R += 0.5f;
						}
						if (flag6)
						{
							G += 0.5f;
						}
						if (flag7)
						{
							B += 0.5f;
						}
						if (flag8)
						{
							R += 0.2f;
							G += 0.2f;
							goto IL_351D;
						}
						goto IL_351D;
					}
				}
				else
				{
					if (num39 == 491)
					{
						R = 0.5f;
						G = 0.4f;
						B = 0.7f;
						goto IL_351D;
					}
					switch (num39)
					{
					case 500:
						R = 0.525f;
						G = 0.375f;
						B = 0.075f;
						goto IL_351D;
					case 501:
						R = 0f;
						G = 0.45f;
						B = 0.525f;
						goto IL_351D;
					case 502:
						R = 0.45f;
						G = 0.15f;
						B = 0.45f;
						goto IL_351D;
					case 503:
						R = 0.45f;
						G = 0.45f;
						B = 0.675f;
						goto IL_351D;
					default:
						goto IL_351D;
					}
				}
				IL_A57:
				R = 0.25f;
				G = 0.1f;
				B = 0f;
				goto IL_351D;
				IL_ED8:
				R = 0.1f;
				G = 0.2f;
				B = 0.7f;
				goto IL_351D;
				IL_2A05:
				R = 0.35f;
				goto IL_351D;
				IL_2FD8:
				if (*tile.type != 349 || *tile.frameX >= 36)
				{
					float num38 = (float)localRandom.Next(28, 42) * 0.005f;
					num38 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
					if (tile.color() == 0)
					{
						R = 0f;
						G = 0.2f + num38 / 2f;
						B = 1f;
					}
					else
					{
						Color color3 = WorldGen.paintColor((int)tile.color());
						R = (float)color3.R / 255f;
						G = (float)color3.G / 255f;
						B = (float)color3.B / 255f;
					}
				}
				IL_351D:
				TileLoader.ModifyLight(x, y, (int)(*tile.type), ref R, ref G, ref B);
			}
			if (lightColor.X < R)
			{
				lightColor.X = R;
			}
			if (lightColor.Y < G)
			{
				lightColor.Y = G;
			}
			if (lightColor.Z < B)
			{
				lightColor.Z = B;
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x00585FA4 File Offset: 0x005841A4
		private unsafe void ApplySurfaceLight(Tile tile, int x, int y, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = (float)Main.tileColor.R / 255f;
			float num5 = (float)Main.tileColor.G / 255f;
			float num6 = (float)Main.tileColor.B / 255f;
			float num7 = (num4 + num5 + num6) / 3f;
			if (tile.active() && TileID.Sets.AllowLightInWater[(int)(*tile.type)])
			{
				if (lightColor.X < num7 && (Main.wallLight[(int)(*tile.wall)] || *tile.wall == 73 || *tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)))
				{
					num = num4;
					num2 = num5;
					num3 = num6;
				}
			}
			else if ((!tile.active() || !Main.tileNoSunLight[(int)(*tile.type)] || ((tile.slope() != 0 || tile.halfBrick() || (tile.invisibleBlock() && !this._drawInvisibleWalls)) && *Main.tile[x, y - 1].liquid == 0 && *Main.tile[x, y + 1].liquid == 0 && *Main.tile[x - 1, y].liquid == 0 && *Main.tile[x + 1, y].liquid == 0)) && lightColor.X < num7 && (Main.wallLight[(int)(*tile.wall)] || *tile.wall == 73 || *tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)))
			{
				if (*tile.liquid < 200)
				{
					if (!tile.halfBrick() || *Main.tile[x, y - 1].liquid < 200)
					{
						num = num4;
						num2 = num5;
						num3 = num6;
					}
				}
				else if (Main.liquidAlpha[13] > 0f)
				{
					if (Main.rand == null)
					{
						Main.rand = new UnifiedRandom();
					}
					num3 = num6 * 0.175f * (1f + Main.rand.NextFloat() * 0.13f) * Main.liquidAlpha[13];
				}
			}
			if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int)(*tile.type)]) && ((*tile.wall >= 88 && *tile.wall <= 93) || *tile.wall == 241) && *tile.liquid < 255)
			{
				num = num4;
				num2 = num5;
				num3 = num6;
				int num8 = (int)(*tile.wall - 88);
				if (*tile.wall == 241)
				{
					num8 = 6;
				}
				switch (num8)
				{
				case 0:
					num *= 0.9f;
					num2 *= 0.15f;
					num3 *= 0.9f;
					break;
				case 1:
					num *= 0.9f;
					num2 *= 0.9f;
					num3 *= 0.15f;
					break;
				case 2:
					num *= 0.15f;
					num2 *= 0.15f;
					num3 *= 0.9f;
					break;
				case 3:
					num *= 0.15f;
					num2 *= 0.9f;
					num3 *= 0.15f;
					break;
				case 4:
					num *= 0.9f;
					num2 *= 0.15f;
					num3 *= 0.15f;
					break;
				case 5:
				{
					float num9 = 0.2f;
					float num10 = 0.7f - num9;
					num *= num10 + (float)Main.DiscoR / 255f * num9;
					num2 *= num10 + (float)Main.DiscoG / 255f * num9;
					num3 *= num10 + (float)Main.DiscoB / 255f * num9;
					break;
				}
				case 6:
					num *= 0.9f;
					num2 *= 0.5f;
					num3 *= 0f;
					break;
				}
			}
			float num11 = 1f - Main.shimmerDarken;
			num *= num11;
			num2 *= num11;
			num3 *= num11;
			if (lightColor.X < num)
			{
				lightColor.X = num;
			}
			if (lightColor.Y < num2)
			{
				lightColor.Y = num2;
			}
			if (lightColor.Z < num3)
			{
				lightColor.Z = num3;
			}
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x00586410 File Offset: 0x00584610
		private unsafe void ApplyHellLight(Tile tile, int x, int y, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0.55f + (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 2f)) * 0.08f;
			if ((!tile.active() || !Main.tileNoSunLight[(int)(*tile.type)] || ((tile.slope() != 0 || tile.halfBrick()) && *Main.tile[x, y - 1].liquid == 0 && *Main.tile[x, y + 1].liquid == 0 && *Main.tile[x - 1, y].liquid == 0 && *Main.tile[x + 1, y].liquid == 0)) && lightColor.X < num4 && (Main.wallLight[(int)(*tile.wall)] || *tile.wall == 73 || *tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)) && *tile.liquid < 200 && (!tile.halfBrick() || *Main.tile[x, y - 1].liquid < 200))
			{
				num = num4;
				num2 = num4 * 0.6f;
				num3 = num4 * 0.2f;
			}
			if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int)(*tile.type)]) && *tile.wall >= 88 && *tile.wall <= 93 && *tile.liquid < 255)
			{
				num = num4;
				num2 = num4 * 0.6f;
				num3 = num4 * 0.2f;
				switch (*tile.wall)
				{
				case 88:
					num *= 0.9f;
					num2 *= 0.15f;
					num3 *= 0.9f;
					break;
				case 89:
					num *= 0.9f;
					num2 *= 0.9f;
					num3 *= 0.15f;
					break;
				case 90:
					num *= 0.15f;
					num2 *= 0.15f;
					num3 *= 0.9f;
					break;
				case 91:
					num *= 0.15f;
					num2 *= 0.9f;
					num3 *= 0.15f;
					break;
				case 92:
					num *= 0.9f;
					num2 *= 0.15f;
					num3 *= 0.15f;
					break;
				case 93:
				{
					float num5 = 0.2f;
					float num6 = 0.7f - num5;
					num *= num6 + (float)Main.DiscoR / 255f * num5;
					num2 *= num6 + (float)Main.DiscoG / 255f * num5;
					num3 *= num6 + (float)Main.DiscoB / 255f * num5;
					break;
				}
				}
			}
			if (lightColor.X < num)
			{
				lightColor.X = num;
			}
			if (lightColor.Y < num2)
			{
				lightColor.Y = num2;
			}
			if (lightColor.Z < num3)
			{
				lightColor.Z = num3;
			}
		}

		// Token: 0x040050DB RID: 20699
		private FastRandom _random = FastRandom.CreateWithRandomSeed();

		// Token: 0x040050DC RID: 20700
		private bool _drawInvisibleWalls;
	}
}
