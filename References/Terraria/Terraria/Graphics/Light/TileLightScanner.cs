using System;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.GameContent;
using Terraria.GameContent.Liquid;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000117 RID: 279
	public class TileLightScanner
	{
		// Token: 0x060016E7 RID: 5863 RVA: 0x004CA2E8 File Offset: 0x004C84E8
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
							Vector3 value;
							this.GetTileLight(i, j, out value);
							outputMap[i - area.X, j - area.Y] = value;
						}
					}
				}
			}, null);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x004CA344 File Offset: 0x004C8544
		private bool IsTileNullOrTouchingNull(int x, int y)
		{
			return !WorldGen.InWorld(x, y, 1) || Main.tile[x, y] == null || Main.tile[x + 1, y] == null || Main.tile[x - 1, y] == null || Main.tile[x, y - 1] == null || Main.tile[x, y + 1] == null;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x004CA3AE File Offset: 0x004C85AE
		public void Update()
		{
			this._random.NextSeed();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x004CA3BB File Offset: 0x004C85BB
		public LightMaskMode GetMaskMode(int x, int y)
		{
			return this.GetTileMask(Main.tile[x, y]);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x004CA3D0 File Offset: 0x004C85D0
		private LightMaskMode GetTileMask(Tile tile)
		{
			if (this.LightIsBlocked(tile) && tile.type != 131 && !tile.inActive() && tile.slope() == 0)
			{
				return LightMaskMode.Solid;
			}
			if (tile.lava() || tile.liquid <= 128)
			{
				return LightMaskMode.None;
			}
			if (!tile.honey())
			{
				return LightMaskMode.Water;
			}
			return LightMaskMode.Honey;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x004CA430 File Offset: 0x004C8630
		public void GetTileLight(int x, int y, out Vector3 outputColor)
		{
			outputColor = Vector3.Zero;
			Tile tile = Main.tile[x, y];
			FastRandom fastRandom = this._random.WithModifier(x, y);
			if (y <= (int)Main.worldSurface)
			{
				this.ApplySurfaceLight(tile, x, y, ref outputColor);
			}
			else if (y > Main.UnderworldLayer)
			{
				this.ApplyHellLight(tile, x, y, ref outputColor);
			}
			this.ApplyWallLight(tile, x, y, ref fastRandom, ref outputColor);
			if (tile.active())
			{
				this.ApplyTileLight(tile, x, y, ref fastRandom, ref outputColor);
			}
			this.ApplyLiquidLight(tile, ref outputColor);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x004CA4B4 File Offset: 0x004C86B4
		private void ApplyLiquidLight(Tile tile, ref Vector3 lightColor)
		{
			if (tile.liquid <= 0)
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

		// Token: 0x060016EE RID: 5870 RVA: 0x004CA5AE File Offset: 0x004C87AE
		private bool LightIsBlocked(Tile tile)
		{
			return tile.active() && Main.tileBlockLight[(int)tile.type] && (!tile.invisibleBlock() || this._drawInvisibleWalls);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x004CA5D8 File Offset: 0x004C87D8
		private void ApplyWallLight(Tile tile, int x, int y, ref FastRandom localRandom, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			ushort wall = tile.wall;
			if (wall <= 156)
			{
				if (wall <= 44)
				{
					if (wall != 33)
					{
						if (wall == 44)
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
				else if (wall != 137)
				{
					switch (wall)
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
			else if (wall <= 176)
			{
				switch (wall)
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
					switch (wall)
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
			else if (wall != 182)
			{
				switch (wall)
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

		// Token: 0x060016F0 RID: 5872 RVA: 0x004CA98C File Offset: 0x004C8B8C
		private void ApplyTileLight(Tile tile, int x, int y, ref FastRandom localRandom, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			if (Main.tileLighted[(int)tile.type])
			{
				ushort type = tile.type;
				if (type <= 302)
				{
					if (type <= 129)
					{
						if (type <= 49)
						{
							if (type <= 20)
							{
								if (type != 4)
								{
									if (type == 17)
									{
										goto IL_280B;
									}
									if (type != 20)
									{
										goto IL_3473;
									}
									int num4 = (int)(tile.frameX / 18);
									if (num4 >= 30 && num4 <= 32)
									{
										num = 0.325f;
										num2 = 0.15f;
										num3 = 0.05f;
										goto IL_3473;
									}
									goto IL_3473;
								}
								else
								{
									if (tile.frameX < 66)
									{
										TorchID.TorchColor((int)(tile.frameY / 22), out num, out num2, out num3);
										goto IL_3473;
									}
									goto IL_3473;
								}
							}
							else if (type <= 37)
							{
								if (type == 22)
								{
									goto IL_2850;
								}
								switch (type)
								{
								case 26:
								case 31:
								{
									if ((tile.type == 31 && tile.frameX >= 36) || (tile.type == 26 && tile.frameX >= 54))
									{
										float num5 = (float)localRandom.Next(-5, 6) * 0.0025f;
										num = 0.5f + num5 * 2f;
										num2 = 0.2f + num5;
										num3 = 0.1f;
										goto IL_3473;
									}
									float num6 = (float)localRandom.Next(-5, 6) * 0.0025f;
									num = 0.31f + num6;
									num2 = 0.1f;
									num3 = 0.44f + num6 * 2f;
									goto IL_3473;
								}
								case 27:
									if (tile.frameY < 36)
									{
										num = 0.3f;
										num2 = 0.27f;
										goto IL_3473;
									}
									goto IL_3473;
								case 28:
								case 29:
								case 30:
								case 32:
								case 36:
									goto IL_3473;
								case 33:
									if (tile.frameX == 0)
									{
										switch (tile.frameY / 22)
										{
										case 0:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 1:
											num = 0.55f;
											num2 = 0.85f;
											num3 = 0.35f;
											goto IL_3473;
										case 2:
											num = 0.65f;
											num2 = 0.95f;
											num3 = 0.5f;
											goto IL_3473;
										case 3:
											num = 0.2f;
											num2 = 0.75f;
											num3 = 1f;
											goto IL_3473;
										case 5:
											num = 0.85f;
											num2 = 0.6f;
											num3 = 1f;
											goto IL_3473;
										case 7:
										case 8:
											num = 0.75f;
											num2 = 0.85f;
											num3 = 1f;
											goto IL_3473;
										case 9:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 10:
											num = 1f;
											num2 = 0.97f;
											num3 = 0.85f;
											goto IL_3473;
										case 14:
											num = 1f;
											num2 = 1f;
											num3 = 0.6f;
											goto IL_3473;
										case 15:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 18:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 19:
											num = 0.37f;
											num2 = 0.8f;
											num3 = 1f;
											goto IL_3473;
										case 20:
											num = 0f;
											num2 = 0.9f;
											num3 = 1f;
											goto IL_3473;
										case 21:
											num = 0.25f;
											num2 = 0.7f;
											num3 = 1f;
											goto IL_3473;
										case 23:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 24:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 25:
											num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
											num2 = 0.3f;
											num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
											goto IL_3473;
										case 28:
											num = 0.9f;
											num2 = 0.75f;
											num3 = 1f;
											goto IL_3473;
										case 29:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 30:
										{
											Vector3 vector = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
											num = vector.X;
											num2 = vector.Y;
											num3 = vector.Z;
											goto IL_3473;
										}
										case 31:
											num = 1f;
											num2 = 0.97f;
											num3 = 0.85f;
											goto IL_3473;
										case 32:
											num = 0.55f;
											num2 = 0.45f;
											num3 = 0.95f;
											goto IL_3473;
										case 33:
											num = 1f;
											num2 = 0.6f;
											num3 = 0.1f;
											goto IL_3473;
										case 34:
											num = 0.3f;
											num2 = 0.75f;
											num3 = 0.55f;
											goto IL_3473;
										case 35:
											num = 0.9f;
											num2 = 0.55f;
											num3 = 0.7f;
											goto IL_3473;
										case 36:
											num = 0.55f;
											num2 = 0.85f;
											num3 = 1f;
											goto IL_3473;
										case 37:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 38:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 39:
											num = 0.4f;
											num2 = 0.8f;
											num3 = 0.9f;
											goto IL_3473;
										case 40:
											num = 1f;
											num2 = 1f;
											num3 = 1f;
											goto IL_3473;
										case 41:
											num = 0.95f;
											num2 = 0.5f;
											num3 = 0.4f;
											goto IL_3473;
										}
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									}
									goto IL_3473;
								case 34:
									if (tile.frameX % 108 < 54)
									{
										int num7 = (int)(tile.frameY / 54);
										switch (num7 + (int)(37 * (tile.frameX / 108)))
										{
										case 7:
											num = 0.95f;
											num2 = 0.95f;
											num3 = 0.5f;
											goto IL_3473;
										case 8:
											num = 0.85f;
											num2 = 0.6f;
											num3 = 1f;
											goto IL_3473;
										case 9:
											num = 1f;
											num2 = 0.6f;
											num3 = 0.6f;
											goto IL_3473;
										case 11:
										case 17:
											num = 0.75f;
											num2 = 0.85f;
											num3 = 1f;
											goto IL_3473;
										case 12:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 13:
											num = 1f;
											num2 = 0.97f;
											num3 = 0.85f;
											goto IL_3473;
										case 15:
											num = 1f;
											num2 = 1f;
											num3 = 0.7f;
											goto IL_3473;
										case 16:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 18:
											num = 1f;
											num2 = 1f;
											num3 = 0.6f;
											goto IL_3473;
										case 19:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 23:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 24:
											num = 0.37f;
											num2 = 0.8f;
											num3 = 1f;
											goto IL_3473;
										case 25:
											num = 0f;
											num2 = 0.9f;
											num3 = 1f;
											goto IL_3473;
										case 26:
											num = 0.25f;
											num2 = 0.7f;
											num3 = 1f;
											goto IL_3473;
										case 27:
											num = 0.55f;
											num2 = 0.85f;
											num3 = 0.35f;
											goto IL_3473;
										case 28:
											num = 0.65f;
											num2 = 0.95f;
											num3 = 0.5f;
											goto IL_3473;
										case 29:
											num = 0.2f;
											num2 = 0.75f;
											num3 = 1f;
											goto IL_3473;
										case 30:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 32:
											num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
											num2 = 0.3f;
											num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
											goto IL_3473;
										case 35:
											num = 0.9f;
											num2 = 0.75f;
											num3 = 1f;
											goto IL_3473;
										case 36:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 37:
										{
											Vector3 vector2 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
											num = vector2.X;
											num2 = vector2.Y;
											num3 = vector2.Z;
											goto IL_3473;
										}
										case 38:
											num = 1f;
											num2 = 0.97f;
											num3 = 0.85f;
											goto IL_3473;
										case 39:
											num = 0.55f;
											num2 = 0.45f;
											num3 = 0.95f;
											goto IL_3473;
										case 40:
											num = 1f;
											num2 = 0.6f;
											num3 = 0.1f;
											goto IL_3473;
										case 41:
											num = 0.3f;
											num2 = 0.75f;
											num3 = 0.55f;
											goto IL_3473;
										case 42:
											num = 0.9f;
											num2 = 0.55f;
											num3 = 0.7f;
											goto IL_3473;
										case 43:
											num = 0.55f;
											num2 = 0.85f;
											num3 = 1f;
											goto IL_3473;
										case 44:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 45:
											num = 1f;
											num2 = 0.95f;
											num3 = 0.65f;
											goto IL_3473;
										case 46:
											num = 0.4f;
											num2 = 0.8f;
											num3 = 0.9f;
											goto IL_3473;
										case 47:
											num = 1f;
											num2 = 1f;
											num3 = 1f;
											goto IL_3473;
										case 48:
											num = 0.95f;
											num2 = 0.5f;
											num3 = 0.4f;
											goto IL_3473;
										}
										num = 1f;
										num2 = 0.95f;
										num3 = 0.8f;
										goto IL_3473;
									}
									goto IL_3473;
								case 35:
									if (tile.frameX < 36)
									{
										num = 0.75f;
										num2 = 0.6f;
										num3 = 0.3f;
										goto IL_3473;
									}
									goto IL_3473;
								case 37:
									num = 0.56f;
									num2 = 0.43f;
									num3 = 0.15f;
									goto IL_3473;
								default:
									goto IL_3473;
								}
							}
							else if (type != 42)
							{
								if (type != 49)
								{
									goto IL_3473;
								}
								if (tile.frameX == 0)
								{
									num = 0f;
									num2 = 0.35f;
									num3 = 0.8f;
									goto IL_3473;
								}
								goto IL_3473;
							}
							else
							{
								if (tile.frameX == 0)
								{
									switch (tile.frameY / 36)
									{
									case 0:
										num = 0.7f;
										num2 = 0.65f;
										num3 = 0.55f;
										goto IL_3473;
									case 1:
										num = 0.9f;
										num2 = 0.75f;
										num3 = 0.6f;
										goto IL_3473;
									case 2:
										num = 0.8f;
										num2 = 0.6f;
										num3 = 0.6f;
										goto IL_3473;
									case 3:
										num = 0.65f;
										num2 = 0.5f;
										num3 = 0.2f;
										goto IL_3473;
									case 4:
										num = 0.5f;
										num2 = 0.7f;
										num3 = 0.4f;
										goto IL_3473;
									case 5:
										num = 0.9f;
										num2 = 0.4f;
										num3 = 0.2f;
										goto IL_3473;
									case 6:
										num = 0.7f;
										num2 = 0.75f;
										num3 = 0.3f;
										goto IL_3473;
									case 7:
									{
										float num8 = Main.demonTorch * 0.2f;
										num = 0.9f - num8;
										num2 = 0.9f - num8;
										num3 = 0.7f + num8;
										goto IL_3473;
									}
									case 8:
										num = 0.75f;
										num2 = 0.6f;
										num3 = 0.3f;
										goto IL_3473;
									case 9:
										num = 1f;
										num2 = 0.3f;
										num3 = 0.5f;
										num3 += Main.demonTorch * 0.2f;
										num -= Main.demonTorch * 0.1f;
										num2 -= Main.demonTorch * 0.2f;
										goto IL_3473;
									case 11:
										num = 0.85f;
										num2 = 0.6f;
										num3 = 1f;
										goto IL_3473;
									case 14:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 15:
									case 16:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 17:
										num = 1f;
										num2 = 0.97f;
										num3 = 0.85f;
										goto IL_3473;
									case 18:
										num = 0.75f;
										num2 = 0.85f;
										num3 = 1f;
										goto IL_3473;
									case 21:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 22:
										num = 1f;
										num2 = 1f;
										num3 = 0.6f;
										goto IL_3473;
									case 23:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 27:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 28:
										num = 0.37f;
										num2 = 0.8f;
										num3 = 1f;
										goto IL_3473;
									case 29:
										num = 0f;
										num2 = 0.9f;
										num3 = 1f;
										goto IL_3473;
									case 30:
										num = 0.25f;
										num2 = 0.7f;
										num3 = 1f;
										goto IL_3473;
									case 32:
										num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
										num2 = 0.3f;
										num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
										goto IL_3473;
									case 35:
										num = 0.7f;
										num2 = 0.6f;
										num3 = 0.9f;
										goto IL_3473;
									case 36:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 37:
									{
										Vector3 vector3 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
										num = vector3.X;
										num2 = vector3.Y;
										num3 = vector3.Z;
										goto IL_3473;
									}
									case 38:
										num = 1f;
										num2 = 0.97f;
										num3 = 0.85f;
										goto IL_3473;
									case 39:
										num = 0.55f;
										num2 = 0.45f;
										num3 = 0.95f;
										goto IL_3473;
									case 40:
										num = 1f;
										num2 = 0.6f;
										num3 = 0.1f;
										goto IL_3473;
									case 41:
										num = 0.3f;
										num2 = 0.75f;
										num3 = 0.55f;
										goto IL_3473;
									case 42:
										num = 0.9f;
										num2 = 0.55f;
										num3 = 0.7f;
										goto IL_3473;
									case 43:
										num = 0.55f;
										num2 = 0.85f;
										num3 = 1f;
										goto IL_3473;
									case 44:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 45:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 46:
										num = 0.4f;
										num2 = 0.8f;
										num3 = 0.9f;
										goto IL_3473;
									case 47:
										num = 1f;
										num2 = 1f;
										num3 = 1f;
										goto IL_3473;
									case 48:
										num = 0.95f;
										num2 = 0.5f;
										num3 = 0.4f;
										goto IL_3473;
									}
									num = 1f;
									num2 = 1f;
									num3 = 1f;
									goto IL_3473;
								}
								goto IL_3473;
							}
						}
						else if (type <= 77)
						{
							if (type != 61)
							{
								if (type - 70 <= 2)
								{
									goto IL_2F65;
								}
								if (type != 77)
								{
									goto IL_3473;
								}
								num = 0.75f;
								num2 = 0.45f;
								num3 = 0.25f;
								goto IL_3473;
							}
							else
							{
								if (tile.frameX == 144)
								{
									float num9 = 1f + (float)(270 - (int)Main.mouseTextColor) / 400f;
									float num10 = 0.8f - (float)(270 - (int)Main.mouseTextColor) / 400f;
									num = 0.42f * num10;
									num2 = 0.81f * num9;
									num3 = 0.52f * num10;
									goto IL_3473;
								}
								goto IL_3473;
							}
						}
						else if (type <= 84)
						{
							if (type != 83)
							{
								if (type != 84)
								{
									goto IL_3473;
								}
								int num11 = (int)(tile.frameX / 18);
								if (num11 == 2)
								{
									float num12 = (float)(270 - (int)Main.mouseTextColor) / 800f;
									if (num12 > 1f)
									{
										num12 = 1f;
									}
									else if (num12 < 0f)
									{
										num12 = 0f;
									}
									num = num12 * 0.7f;
									num2 = num12;
									num3 = num12 * 0.1f;
									goto IL_3473;
								}
								if (num11 == 5)
								{
									float num12 = 0.9f;
									num = num12;
									num2 = num12 * 0.8f;
									num3 = num12 * 0.2f;
									goto IL_3473;
								}
								if (num11 == 6)
								{
									float num12 = 0.08f;
									num2 = num12 * 0.8f;
									num3 = num12;
									goto IL_3473;
								}
								goto IL_3473;
							}
							else
							{
								if (tile.frameX == 18 && !Main.dayTime)
								{
									num = 0.1f;
									num2 = 0.4f;
									num3 = 0.6f;
								}
								if (tile.frameX == 90 && !Main.raining && Main.time > 40500.0)
								{
									num = 0.9f;
									num2 = 0.72f;
									num3 = 0.18f;
									goto IL_3473;
								}
								goto IL_3473;
							}
						}
						else
						{
							switch (type)
							{
							case 92:
								if (tile.frameY <= 18 && tile.frameX == 0)
								{
									num = 1f;
									num2 = 1f;
									num3 = 1f;
									goto IL_3473;
								}
								goto IL_3473;
							case 93:
								if (tile.frameX == 0)
								{
									switch (tile.frameY / 54)
									{
									case 1:
										num = 0.95f;
										num2 = 0.95f;
										num3 = 0.5f;
										goto IL_3473;
									case 2:
										num = 0.85f;
										num2 = 0.6f;
										num3 = 1f;
										goto IL_3473;
									case 3:
										num = 0.75f;
										num2 = 1f;
										num3 = 0.6f;
										goto IL_3473;
									case 4:
									case 5:
										num = 0.75f;
										num2 = 0.85f;
										num3 = 1f;
										goto IL_3473;
									case 6:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 7:
										num = 1f;
										num2 = 0.97f;
										num3 = 0.85f;
										goto IL_3473;
									case 9:
										num = 1f;
										num2 = 1f;
										num3 = 0.7f;
										goto IL_3473;
									case 10:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 12:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 13:
										num = 1f;
										num2 = 1f;
										num3 = 0.6f;
										goto IL_3473;
									case 14:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 18:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 19:
										num = 0.37f;
										num2 = 0.8f;
										num3 = 1f;
										goto IL_3473;
									case 20:
										num = 0f;
										num2 = 0.9f;
										num3 = 1f;
										goto IL_3473;
									case 21:
										num = 0.25f;
										num2 = 0.7f;
										num3 = 1f;
										goto IL_3473;
									case 23:
										num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
										num2 = 0.3f;
										num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
										goto IL_3473;
									case 24:
										num = 0.35f;
										num2 = 0.5f;
										num3 = 0.3f;
										goto IL_3473;
									case 25:
										num = 0.34f;
										num2 = 0.4f;
										num3 = 0.31f;
										goto IL_3473;
									case 26:
										num = 0.25f;
										num2 = 0.32f;
										num3 = 0.5f;
										goto IL_3473;
									case 29:
										num = 0.9f;
										num2 = 0.75f;
										num3 = 1f;
										goto IL_3473;
									case 30:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 31:
									{
										Vector3 vector4 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
										num = vector4.X;
										num2 = vector4.Y;
										num3 = vector4.Z;
										goto IL_3473;
									}
									case 32:
										num = 1f;
										num2 = 0.97f;
										num3 = 0.85f;
										goto IL_3473;
									case 33:
										num = 0.55f;
										num2 = 0.45f;
										num3 = 0.95f;
										goto IL_3473;
									case 34:
										num = 1f;
										num2 = 0.6f;
										num3 = 0.1f;
										goto IL_3473;
									case 35:
										num = 0.3f;
										num2 = 0.75f;
										num3 = 0.55f;
										goto IL_3473;
									case 36:
										num = 0.9f;
										num2 = 0.55f;
										num3 = 0.7f;
										goto IL_3473;
									case 37:
										num = 0.55f;
										num2 = 0.85f;
										num3 = 1f;
										goto IL_3473;
									case 38:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 39:
										num = 1f;
										num2 = 0.95f;
										num3 = 0.65f;
										goto IL_3473;
									case 40:
										num = 0.4f;
										num2 = 0.8f;
										num3 = 0.9f;
										goto IL_3473;
									case 41:
										num = 1f;
										num2 = 1f;
										num3 = 1f;
										goto IL_3473;
									case 42:
										num = 0.95f;
										num2 = 0.5f;
										num3 = 0.4f;
										goto IL_3473;
									}
									num = 1f;
									num2 = 0.97f;
									num3 = 0.85f;
									goto IL_3473;
								}
								goto IL_3473;
							case 94:
							case 97:
							case 99:
								goto IL_3473;
							case 95:
								if (tile.frameX < 36)
								{
									num = 1f;
									num2 = 0.95f;
									num3 = 0.8f;
									goto IL_3473;
								}
								goto IL_3473;
							case 96:
								if (tile.frameX >= 36)
								{
									num = 0.5f;
									num2 = 0.35f;
									num3 = 0.1f;
									goto IL_3473;
								}
								goto IL_3473;
							case 98:
								if (tile.frameY == 0)
								{
									num = 1f;
									num2 = 0.97f;
									num3 = 0.85f;
									goto IL_3473;
								}
								goto IL_3473;
							case 100:
								break;
							default:
								switch (type)
								{
								case 125:
								{
									float num13 = (float)localRandom.Next(28, 42) * 0.01f;
									num13 += (float)(270 - (int)Main.mouseTextColor) / 800f;
									num2 = (lightColor.Y = 0.3f * num13);
									num3 = (lightColor.Z = 0.6f * num13);
									goto IL_3473;
								}
								case 126:
									if (tile.frameX < 36)
									{
										num = (float)Main.DiscoR / 255f;
										num2 = (float)Main.DiscoG / 255f;
										num3 = (float)Main.DiscoB / 255f;
										goto IL_3473;
									}
									goto IL_3473;
								case 127:
								case 128:
									goto IL_3473;
								case 129:
									switch (tile.frameX / 18 % 3)
									{
									case 0:
										num = 0f;
										num2 = 0.05f;
										num3 = 0.25f;
										goto IL_3473;
									case 1:
										num = 0.2f;
										num2 = 0f;
										num3 = 0.15f;
										goto IL_3473;
									case 2:
										num = 0.1f;
										num2 = 0f;
										num3 = 0.2f;
										goto IL_3473;
									default:
										goto IL_3473;
									}
									break;
								default:
									goto IL_3473;
								}
								break;
							}
						}
					}
					else if (type <= 190)
					{
						if (type <= 149)
						{
							if (type == 133)
							{
								goto IL_280B;
							}
							if (type == 140)
							{
								goto IL_2850;
							}
							if (type != 149)
							{
								goto IL_3473;
							}
							if (tile.frameX <= 36)
							{
								switch (tile.frameX / 18)
								{
								case 0:
									num = 0.1f;
									num2 = 0.2f;
									num3 = 0.5f;
									break;
								case 1:
									num = 0.5f;
									num2 = 0.1f;
									num3 = 0.1f;
									break;
								case 2:
									num = 0.2f;
									num2 = 0.5f;
									num3 = 0.1f;
									break;
								}
								num *= (float)localRandom.Next(970, 1031) * 0.001f;
								num2 *= (float)localRandom.Next(970, 1031) * 0.001f;
								num3 *= (float)localRandom.Next(970, 1031) * 0.001f;
								goto IL_3473;
							}
							goto IL_3473;
						}
						else if (type <= 174)
						{
							if (type == 160)
							{
								num = (float)Main.DiscoR / 255f * 0.25f;
								num2 = (float)Main.DiscoG / 255f * 0.25f;
								num3 = (float)Main.DiscoB / 255f * 0.25f;
								goto IL_3473;
							}
							switch (type)
							{
							case 171:
								if (tile.frameX < 10)
								{
									x -= (int)tile.frameX;
									y -= (int)tile.frameY;
								}
								switch ((Main.tile[x, y].frameY & 15360) >> 10)
								{
								case 1:
									num = 0.1f;
									num2 = 0.1f;
									num3 = 0.1f;
									break;
								case 2:
									num = 0.2f;
									break;
								case 3:
									num2 = 0.2f;
									break;
								case 4:
									num3 = 0.2f;
									break;
								case 5:
									num = 0.125f;
									num2 = 0.125f;
									break;
								case 6:
									num = 0.2f;
									num2 = 0.1f;
									break;
								case 7:
									num = 0.125f;
									num2 = 0.125f;
									break;
								case 8:
									num = 0.08f;
									num2 = 0.175f;
									break;
								case 9:
									num2 = 0.125f;
									num3 = 0.125f;
									break;
								case 10:
									num = 0.125f;
									num3 = 0.125f;
									break;
								case 11:
									num = 0.1f;
									num2 = 0.1f;
									num3 = 0.2f;
									break;
								default:
									num2 = (num = (num3 = 0f));
									break;
								}
								num *= 0.5f;
								num2 *= 0.5f;
								num3 *= 0.5f;
								goto IL_3473;
							case 172:
								goto IL_3473;
							case 173:
								break;
							case 174:
								if (tile.frameX == 0)
								{
									num = 1f;
									num2 = 0.95f;
									num3 = 0.65f;
									goto IL_3473;
								}
								goto IL_3473;
							default:
								goto IL_3473;
							}
						}
						else if (type != 184)
						{
							if (type != 190)
							{
								goto IL_3473;
							}
							goto IL_2F65;
						}
						else
						{
							if (tile.frameX == 110)
							{
								num = 0.25f;
								num2 = 0.1f;
								num3 = 0f;
							}
							if (tile.frameX == 132)
							{
								num = 0f;
								num2 = 0.25f;
								num3 = 0f;
							}
							if (tile.frameX == 154)
							{
								num = 0f;
								num2 = 0.16f;
								num3 = 0.34f;
							}
							if (tile.frameX == 176)
							{
								num = 0.3f;
								num2 = 0f;
								num3 = 0.17f;
							}
							if (tile.frameX == 198)
							{
								num = 0.3f;
								num2 = 0f;
								num3 = 0.35f;
							}
							if (tile.frameX == 220)
							{
								num = (float)Main.DiscoR / 255f * 0.25f;
								num2 = (float)Main.DiscoG / 255f * 0.25f;
								num3 = (float)Main.DiscoB / 255f * 0.25f;
								goto IL_3473;
							}
							goto IL_3473;
						}
					}
					else if (type <= 215)
					{
						if (type == 204)
						{
							goto IL_299C;
						}
						if (type != 209)
						{
							if (type != 215)
							{
								goto IL_3473;
							}
							if (tile.frameY < 36)
							{
								float num14 = (float)localRandom.Next(28, 42) * 0.005f;
								num14 += (float)(270 - (int)Main.mouseTextColor) / 700f;
								switch (tile.frameX / 54)
								{
								case 1:
									num = 0.7f;
									num2 = 1f;
									num3 = 0.5f;
									break;
								case 2:
									num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
									num2 = 0.3f;
									num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
									break;
								case 3:
									num = 0.45f;
									num2 = 0.75f;
									num3 = 1f;
									break;
								case 4:
									num = 1.15f;
									num2 = 1.15f;
									num3 = 0.5f;
									break;
								case 5:
									num = (float)Main.DiscoR / 255f;
									num2 = (float)Main.DiscoG / 255f;
									num3 = (float)Main.DiscoB / 255f;
									break;
								case 6:
									num = 0.75f;
									num2 = 1.2824999f;
									num3 = 1.2f;
									break;
								case 7:
									num = 0.95f;
									num2 = 0.65f;
									num3 = 1.3f;
									break;
								case 8:
									num = 1.4f;
									num2 = 0.85f;
									num3 = 0.55f;
									break;
								case 9:
									num = 0.25f;
									num2 = 1.3f;
									num3 = 0.8f;
									break;
								case 10:
									num = 0.95f;
									num2 = 0.4f;
									num3 = 1.4f;
									break;
								case 11:
									num = 1.4f;
									num2 = 0.7f;
									num3 = 0.5f;
									break;
								case 12:
									num = 1.25f;
									num2 = 0.6f;
									num3 = 1.2f;
									break;
								case 13:
									num = 0.75f;
									num2 = 1.45f;
									num3 = 0.9f;
									break;
								case 14:
									num = 0.25f;
									num2 = 0.65f;
									num3 = 1f;
									break;
								case 15:
									TorchID.TorchColor(23, out num, out num2, out num3);
									break;
								default:
									num = 0.9f;
									num2 = 0.3f;
									num3 = 0.1f;
									break;
								}
								num += num14;
								num2 += num14;
								num3 += num14;
								goto IL_3473;
							}
							goto IL_3473;
						}
						else
						{
							if (tile.frameX == 234 || tile.frameX == 252)
							{
								Vector3 vector5 = PortalHelper.GetPortalColor(Main.myPlayer, 0).ToVector3() * 0.65f;
								num = vector5.X;
								num2 = vector5.Y;
								num3 = vector5.Z;
								goto IL_3473;
							}
							if (tile.frameX == 306 || tile.frameX == 324)
							{
								Vector3 vector6 = PortalHelper.GetPortalColor(Main.myPlayer, 1).ToVector3() * 0.65f;
								num = vector6.X;
								num2 = vector6.Y;
								num3 = vector6.Z;
								goto IL_3473;
							}
							goto IL_3473;
						}
					}
					else if (type <= 271)
					{
						switch (type)
						{
						case 235:
							if ((double)lightColor.X < 0.6)
							{
								lightColor.X = 0.6f;
							}
							if ((double)lightColor.Y < 0.6)
							{
								lightColor.Y = 0.6f;
								goto IL_3473;
							}
							goto IL_3473;
						case 236:
							goto IL_3473;
						case 237:
							num = 0.1f;
							num2 = 0.1f;
							goto IL_3473;
						case 238:
							if ((double)lightColor.X < 0.5)
							{
								lightColor.X = 0.5f;
							}
							if ((double)lightColor.Z < 0.5)
							{
								lightColor.Z = 0.5f;
								goto IL_3473;
							}
							goto IL_3473;
						default:
							switch (type)
							{
							case 262:
								num = 0.75f;
								num3 = 0.75f;
								goto IL_3473;
							case 263:
								num = 0.75f;
								num2 = 0.75f;
								goto IL_3473;
							case 264:
								num3 = 0.75f;
								goto IL_3473;
							case 265:
								num2 = 0.75f;
								goto IL_3473;
							case 266:
								num = 0.75f;
								goto IL_3473;
							case 267:
								num = 0.75f;
								num2 = 0.75f;
								num3 = 0.75f;
								goto IL_3473;
							case 268:
								num = 0.75f;
								num2 = 0.375f;
								goto IL_3473;
							case 269:
								goto IL_3473;
							case 270:
								num = 0.73f;
								num2 = 1f;
								num3 = 0.41f;
								goto IL_3473;
							case 271:
								num = 0.45f;
								num2 = 0.95f;
								num3 = 1f;
								goto IL_3473;
							default:
								goto IL_3473;
							}
							break;
						}
					}
					else
					{
						if (type == 286)
						{
							goto IL_EB0;
						}
						if (type != 302)
						{
							goto IL_3473;
						}
						goto IL_280B;
					}
					if (tile.frameX < 36)
					{
						switch (tile.frameY / 36)
						{
						case 1:
							num = 0.95f;
							num2 = 0.95f;
							num3 = 0.5f;
							goto IL_3473;
						case 2:
							num = 0.85f;
							num2 = 0.6f;
							num3 = 1f;
							goto IL_3473;
						case 3:
							num = 1f;
							num2 = 0.6f;
							num3 = 0.6f;
							goto IL_3473;
						case 5:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 6:
						case 7:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 8:
							num = 1f;
							num2 = 0.97f;
							num3 = 0.85f;
							goto IL_3473;
						case 9:
							num = 0.75f;
							num2 = 0.85f;
							num3 = 1f;
							goto IL_3473;
						case 11:
							num = 1f;
							num2 = 1f;
							num3 = 0.7f;
							goto IL_3473;
						case 12:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 13:
							num = 1f;
							num2 = 1f;
							num3 = 0.6f;
							goto IL_3473;
						case 14:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 18:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 19:
							num = 0.37f;
							num2 = 0.8f;
							num3 = 1f;
							goto IL_3473;
						case 20:
							num = 0f;
							num2 = 0.9f;
							num3 = 1f;
							goto IL_3473;
						case 21:
							num = 0.25f;
							num2 = 0.7f;
							num3 = 1f;
							goto IL_3473;
						case 22:
							num = 0.35f;
							num2 = 0.5f;
							num3 = 0.3f;
							goto IL_3473;
						case 23:
							num = 0.34f;
							num2 = 0.4f;
							num3 = 0.31f;
							goto IL_3473;
						case 24:
							num = 0.25f;
							num2 = 0.32f;
							num3 = 0.5f;
							goto IL_3473;
						case 25:
							num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
							num2 = 0.3f;
							num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
							goto IL_3473;
						case 29:
							num = 0.9f;
							num2 = 0.75f;
							num3 = 1f;
							goto IL_3473;
						case 30:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 31:
						{
							Vector3 vector7 = Main.hslToRgb(Main.demonTorch * 0.12f + 0.69f, 1f, 0.75f, byte.MaxValue).ToVector3() * 1.2f;
							num = vector7.X;
							num2 = vector7.Y;
							num3 = vector7.Z;
							goto IL_3473;
						}
						case 32:
							num = 1f;
							num2 = 0.97f;
							num3 = 0.85f;
							goto IL_3473;
						case 33:
							num = 0.55f;
							num2 = 0.45f;
							num3 = 0.95f;
							goto IL_3473;
						case 34:
							num = 1f;
							num2 = 0.6f;
							num3 = 0.1f;
							goto IL_3473;
						case 35:
							num = 0.3f;
							num2 = 0.75f;
							num3 = 0.55f;
							goto IL_3473;
						case 36:
							num = 0.9f;
							num2 = 0.55f;
							num3 = 0.7f;
							goto IL_3473;
						case 37:
							num = 0.55f;
							num2 = 0.85f;
							num3 = 1f;
							goto IL_3473;
						case 38:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 39:
							num = 1f;
							num2 = 0.95f;
							num3 = 0.65f;
							goto IL_3473;
						case 40:
							num = 0.4f;
							num2 = 0.8f;
							num3 = 0.9f;
							goto IL_3473;
						case 41:
							num = 1f;
							num2 = 1f;
							num3 = 1f;
							goto IL_3473;
						case 42:
							num = 0.95f;
							num2 = 0.5f;
							num3 = 0.4f;
							goto IL_3473;
						}
						num = 1f;
						num2 = 0.95f;
						num3 = 0.65f;
						goto IL_3473;
					}
					goto IL_3473;
					IL_280B:
					num = 0.83f;
					num2 = 0.6f;
					num3 = 0.5f;
					goto IL_3473;
					IL_2850:
					num = 0.12f;
					num2 = 0.07f;
					num3 = 0.32f;
					goto IL_3473;
				}
				if (type > 503)
				{
					if (type > 598)
					{
						if (type > 638)
						{
							if (type <= 663)
							{
								if (type != 646)
								{
									switch (type)
									{
									case 656:
										num = 0.2f;
										num2 = 0.55f;
										num3 = 0.5f;
										goto IL_3473;
									case 657:
									case 661:
									case 662:
										goto IL_3473;
									case 658:
										if (tile.invisibleBlock())
										{
											goto IL_3473;
										}
										TorchID.TorchColor(23, out num, out num2, out num3);
										switch (tile.frameY / 54)
										{
										default:
											num *= 0.2f;
											num2 *= 0.2f;
											num3 *= 0.2f;
											goto IL_3473;
										case 1:
											num *= 0.3f;
											num2 *= 0.3f;
											num3 *= 0.3f;
											goto IL_3473;
										case 2:
											num *= 0.1f;
											num2 *= 0.1f;
											num3 *= 0.1f;
											goto IL_3473;
										}
										break;
									case 659:
										break;
									case 660:
										TorchID.TorchColor(23, out num, out num2, out num3);
										goto IL_3473;
									case 663:
										if (Main.moondialCooldown == 0)
										{
											num = 0f;
											num2 = 0.25f;
											num3 = 0.45f;
											goto IL_3473;
										}
										goto IL_3473;
									default:
										goto IL_3473;
									}
								}
								else
								{
									if (tile.frameX == 0)
									{
										num = 0.2f;
										num2 = 0.3f;
										num3 = 0.32f;
										goto IL_3473;
									}
									goto IL_3473;
								}
							}
							else if (type != 667)
							{
								switch (type)
								{
								case 687:
									goto IL_A4B;
								case 688:
									goto IL_A90;
								case 689:
									goto IL_A62;
								case 690:
									goto IL_A79;
								case 691:
									goto IL_AA7;
								case 692:
									goto IL_ABE;
								default:
									goto IL_3473;
								}
							}
							Vector4 shimmerBaseColor = LiquidRenderer.GetShimmerBaseColor((float)x, (float)y);
							num = shimmerBaseColor.X;
							num2 = shimmerBaseColor.Y;
							num3 = shimmerBaseColor.Z;
							goto IL_3473;
						}
						if (type - 613 <= 1)
						{
							num = 0.7f;
							num2 = 0.3f;
							num3 = 0.2f;
							goto IL_3473;
						}
						switch (type)
						{
						case 619:
							goto IL_EB0;
						case 620:
						{
							Color value = new Color(230, 230, 230, 0).MultiplyRGBA(Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.5f % 1f, 1f, 0.5f, byte.MaxValue));
							value *= 0.4f;
							num = (float)value.R / 255f;
							num2 = (float)value.G / 255f;
							num3 = (float)value.B / 255f;
							goto IL_3473;
						}
						case 621:
						case 622:
						case 623:
						case 624:
							goto IL_3473;
						case 625:
						case 626:
							break;
						case 627:
						case 628:
							goto IL_ABE;
						default:
							switch (type)
							{
							case 633:
							case 637:
							case 638:
								num = 0.325f;
								num2 = 0.15f;
								num3 = 0.05f;
								goto IL_3473;
							case 634:
								num = 0.65f;
								num2 = 0.3f;
								num3 = 0.1f;
								goto IL_3473;
							case 635:
							case 636:
								goto IL_3473;
							default:
								goto IL_3473;
							}
							break;
						}
						IL_AA7:
						num = 0.3f;
						num2 = 0f;
						num3 = 0.35f;
						goto IL_3473;
						IL_ABE:
						num = (float)Main.DiscoR / 255f * 0.25f;
						num2 = (float)Main.DiscoG / 255f * 0.25f;
						num3 = (float)Main.DiscoB / 255f * 0.25f;
						goto IL_3473;
					}
					if (type <= 540)
					{
						if (type == 517)
						{
							goto IL_A4B;
						}
						if (type != 519)
						{
							switch (type)
							{
							case 528:
								goto IL_2F65;
							case 529:
							case 530:
							case 531:
							case 532:
							case 533:
							case 538:
								goto IL_3473;
							case 534:
							case 535:
								break;
							case 536:
							case 537:
								goto IL_A79;
							case 539:
							case 540:
								goto IL_A90;
							default:
								goto IL_3473;
							}
						}
						else
						{
							if (tile.frameY != 90)
							{
								goto IL_3473;
							}
							if (tile.color() == 0)
							{
								float num15 = (float)localRandom.Next(28, 42) * 0.005f;
								num15 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
								num = 0.1f;
								num2 = 0.2f + num15 / 2f;
								num3 = 0.7f + num15;
								goto IL_3473;
							}
							Color color = WorldGen.paintColor((int)tile.color());
							num = (float)color.R / 255f;
							num2 = (float)color.G / 255f;
							num3 = (float)color.B / 255f;
							goto IL_3473;
						}
					}
					else
					{
						if (type > 572)
						{
							switch (type)
							{
							case 578:
								goto IL_2F65;
							case 579:
								goto IL_3473;
							case 580:
								num = 0.7f;
								num2 = 0.3f;
								num3 = 0.2f;
								goto IL_3473;
							case 581:
								num = 1f;
								num2 = 0.75f;
								num3 = 0.5f;
								goto IL_3473;
							case 582:
								break;
							default:
								switch (type)
								{
								case 592:
									if (tile.frameY > 0)
									{
										float num16 = (float)localRandom.Next(28, 42) * 0.005f;
										num16 += (float)(270 - (int)Main.mouseTextColor) / 700f;
										num = 1.35f;
										num2 = 0.45f;
										num3 = 0.15f;
										num += num16;
										num2 += num16;
										num3 += num16;
										goto IL_3473;
									}
									goto IL_3473;
								case 593:
									if (tile.frameX < 18)
									{
										num = 0.8f;
										num2 = 0.3f;
										num3 = 0.1f;
										goto IL_3473;
									}
									goto IL_3473;
								case 594:
									if (tile.frameX < 36)
									{
										num = 0.8f;
										num2 = 0.3f;
										num3 = 0.1f;
										goto IL_3473;
									}
									goto IL_3473;
								case 595:
								case 596:
									goto IL_3473;
								case 597:
									switch (tile.frameX / 54)
									{
									case 0:
										num = 0.05f;
										num2 = 0.8f;
										num3 = 0.3f;
										break;
									case 1:
										num = 0.7f;
										num2 = 0.8f;
										num3 = 0.05f;
										break;
									case 2:
										num = 0.7f;
										num2 = 0.5f;
										num3 = 0.9f;
										break;
									case 3:
										num = 0.6f;
										num2 = 0.6f;
										num3 = 0.8f;
										break;
									case 4:
										num = 0.4f;
										num2 = 0.4f;
										num3 = 1.15f;
										break;
									case 5:
										num = 0.85f;
										num2 = 0.45f;
										num3 = 0.1f;
										break;
									case 6:
										num = 0.8f;
										num2 = 0.8f;
										num3 = 1f;
										break;
									case 7:
										num = 0.5f;
										num2 = 0.8f;
										num3 = 1.2f;
										break;
									}
									num *= 0.75f;
									num2 *= 0.75f;
									num3 *= 0.75f;
									goto IL_3473;
								case 598:
									break;
								default:
									goto IL_3473;
								}
								break;
							}
							num = 0.7f;
							num2 = 0.2f;
							num3 = 0.1f;
							goto IL_3473;
						}
						if (type != 548)
						{
							switch (type)
							{
							case 564:
								if (tile.frameX < 36)
								{
									num = 0.05f;
									num2 = 0.3f;
									num3 = 0.55f;
									goto IL_3473;
								}
								goto IL_3473;
							case 565:
							case 566:
							case 567:
							case 571:
								goto IL_3473;
							case 568:
								num = 1f;
								num2 = 0.61f;
								num3 = 0.65f;
								goto IL_3473;
							case 569:
								num = 0.12f;
								num2 = 1f;
								num3 = 0.66f;
								goto IL_3473;
							case 570:
								num = 0.57f;
								num2 = 0.57f;
								num3 = 1f;
								goto IL_3473;
							case 572:
								switch (tile.frameY / 36)
								{
								case 0:
									num = 0.9f;
									num2 = 0.5f;
									num3 = 0.7f;
									goto IL_3473;
								case 1:
									num = 0.7f;
									num2 = 0.55f;
									num3 = 0.96f;
									goto IL_3473;
								case 2:
									num = 0.45f;
									num2 = 0.96f;
									num3 = 0.95f;
									goto IL_3473;
								case 3:
									num = 0.5f;
									num2 = 0.96f;
									num3 = 0.62f;
									goto IL_3473;
								case 4:
									num = 0.47f;
									num2 = 0.69f;
									num3 = 0.95f;
									goto IL_3473;
								case 5:
									num = 0.92f;
									num2 = 0.57f;
									num3 = 0.51f;
									goto IL_3473;
								default:
									goto IL_3473;
								}
								break;
							default:
								goto IL_3473;
							}
						}
						else
						{
							if (tile.frameX / 54 >= 7)
							{
								num = 0.7f;
								num2 = 0.3f;
								num3 = 0.2f;
								goto IL_3473;
							}
							goto IL_3473;
						}
					}
					IL_A62:
					num = 0f;
					num2 = 0.25f;
					num3 = 0f;
					goto IL_3473;
					IL_A79:
					num = 0f;
					num2 = 0.16f;
					num3 = 0.34f;
					goto IL_3473;
					IL_A90:
					num = 0.3f;
					num2 = 0f;
					num3 = 0.17f;
					goto IL_3473;
				}
				if (type <= 390)
				{
					if (type <= 356)
					{
						if (type - 316 > 2)
						{
							if (type == 327)
							{
								float num17 = 0.5f;
								num17 += (float)(270 - (int)Main.mouseTextColor) / 1500f;
								num17 += (float)localRandom.Next(0, 50) * 0.0005f;
								num = 1f * num17;
								num2 = 0.5f * num17;
								num3 = 0.1f * num17;
								goto IL_3473;
							}
							switch (type)
							{
							case 336:
								num = 0.85f;
								num2 = 0.5f;
								num3 = 0.3f;
								goto IL_3473;
							case 337:
							case 338:
							case 339:
							case 345:
							case 346:
							case 351:
							case 352:
							case 353:
							case 355:
								goto IL_3473;
							case 340:
								num = 0.45f;
								num2 = 1f;
								num3 = 0.45f;
								goto IL_3473;
							case 341:
								num = 0.4f * Main.demonTorch + 0.6f * (1f - Main.demonTorch);
								num2 = 0.35f;
								num3 = 1f * Main.demonTorch + 0.6f * (1f - Main.demonTorch);
								goto IL_3473;
							case 342:
								num = 0.5f;
								num2 = 0.5f;
								num3 = 1.1f;
								goto IL_3473;
							case 343:
								num = 0.85f;
								num2 = 0.85f;
								num3 = 0.3f;
								goto IL_3473;
							case 344:
								num = 0.6f;
								num2 = 1.026f;
								num3 = 0.96000004f;
								goto IL_3473;
							case 347:
								goto IL_299C;
							case 348:
							case 349:
								goto IL_2F65;
							case 350:
							{
								double num18 = Main.timeForVisualEffects * 0.08;
								num2 = (num3 = (num = (float)(-(float)Math.Cos(((int)(num18 / 6.283) % 3 == 1) ? num18 : 0.0) * 0.1 + 0.1)));
								goto IL_3473;
							}
							case 354:
								num = 0.65f;
								num2 = 0.35f;
								num3 = 0.15f;
								goto IL_3473;
							case 356:
								if (Main.sundialCooldown == 0)
								{
									num = 0.45f;
									num2 = 0.25f;
									num3 = 0f;
									goto IL_3473;
								}
								goto IL_3473;
							default:
								goto IL_3473;
							}
						}
						else
						{
							int num19 = x - (int)(tile.frameX / 18);
							int num20 = y - (int)(tile.frameY / 18);
							int num21 = num19 / 2 * (num20 / 3);
							num21 %= Main.cageFrames;
							bool flag = Main.jellyfishCageMode[(int)(tile.type - 316), num21] == 2;
							if (tile.type == 316)
							{
								if (flag)
								{
									num = 0.2f;
									num2 = 0.3f;
									num3 = 0.8f;
								}
								else
								{
									num = 0.1f;
									num2 = 0.2f;
									num3 = 0.5f;
								}
							}
							if (tile.type == 317)
							{
								if (flag)
								{
									num = 0.2f;
									num2 = 0.7f;
									num3 = 0.3f;
								}
								else
								{
									num = 0.05f;
									num2 = 0.45f;
									num3 = 0.1f;
								}
							}
							if (tile.type != 318)
							{
								goto IL_3473;
							}
							if (flag)
							{
								num = 0.7f;
								num2 = 0.2f;
								num3 = 0.5f;
								goto IL_3473;
							}
							num = 0.4f;
							num2 = 0.1f;
							num3 = 0.25f;
							goto IL_3473;
						}
					}
					else if (type <= 372)
					{
						if (type == 370)
						{
							num = 0.32f;
							num2 = 0.16f;
							num3 = 0.12f;
							goto IL_3473;
						}
						if (type != 372)
						{
							goto IL_3473;
						}
						if (tile.frameX == 0)
						{
							num = 0.9f;
							num2 = 0.1f;
							num3 = 0.75f;
							goto IL_3473;
						}
						goto IL_3473;
					}
					else if (type != 381)
					{
						if (type != 390)
						{
							goto IL_3473;
						}
						num = 0.4f;
						num2 = 0.2f;
						num3 = 0.1f;
						goto IL_3473;
					}
				}
				else if (type <= 418)
				{
					if (type == 391)
					{
						num = 0.3f;
						num2 = 0.1f;
						num3 = 0.25f;
						goto IL_3473;
					}
					if (type != 405)
					{
						switch (type)
						{
						case 415:
							num = 0.7f;
							num2 = 0.5f;
							num3 = 0.1f;
							goto IL_3473;
						case 416:
							num = 0f;
							num2 = 0.6f;
							num3 = 0.7f;
							goto IL_3473;
						case 417:
							num = 0.6f;
							num2 = 0.2f;
							num3 = 0.6f;
							goto IL_3473;
						case 418:
							num = 0.6f;
							num2 = 0.6f;
							num3 = 0.9f;
							goto IL_3473;
						default:
							goto IL_3473;
						}
					}
					else
					{
						if (tile.frameX < 54)
						{
							float num22 = (float)localRandom.Next(28, 42) * 0.005f;
							num22 += (float)(270 - (int)Main.mouseTextColor) / 700f;
							switch (tile.frameX / 54)
							{
							case 1:
								num = 0.7f;
								num2 = 1f;
								num3 = 0.5f;
								break;
							case 2:
								num = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
								num2 = 0.3f;
								num3 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
								break;
							case 3:
								num = 0.45f;
								num2 = 0.75f;
								num3 = 1f;
								break;
							case 4:
								num = 1.15f;
								num2 = 1.15f;
								num3 = 0.5f;
								break;
							case 5:
								num = (float)Main.DiscoR / 255f;
								num2 = (float)Main.DiscoG / 255f;
								num3 = (float)Main.DiscoB / 255f;
								break;
							default:
								num = 0.9f;
								num2 = 0.3f;
								num3 = 0.1f;
								break;
							}
							num += num22;
							num2 += num22;
							num3 += num22;
							goto IL_3473;
						}
						goto IL_3473;
					}
				}
				else if (type <= 463)
				{
					if (type != 429)
					{
						if (type != 463)
						{
							goto IL_3473;
						}
						num = 0.2f;
						num2 = 0.4f;
						num3 = 0.8f;
						goto IL_3473;
					}
					else
					{
						short num23 = tile.frameX / 18;
						bool flag2 = num23 % 2 >= 1;
						bool flag3 = num23 % 4 >= 2;
						bool flag4 = num23 % 8 >= 4;
						bool flag5 = num23 % 16 >= 8;
						if (flag2)
						{
							num += 0.5f;
						}
						if (flag3)
						{
							num2 += 0.5f;
						}
						if (flag4)
						{
							num3 += 0.5f;
						}
						if (flag5)
						{
							num += 0.2f;
							num2 += 0.2f;
							goto IL_3473;
						}
						goto IL_3473;
					}
				}
				else
				{
					if (type == 491)
					{
						num = 0.5f;
						num2 = 0.4f;
						num3 = 0.7f;
						goto IL_3473;
					}
					switch (type)
					{
					case 500:
						num = 0.525f;
						num2 = 0.375f;
						num3 = 0.075f;
						goto IL_3473;
					case 501:
						num = 0f;
						num2 = 0.45f;
						num3 = 0.525f;
						goto IL_3473;
					case 502:
						num = 0.45f;
						num2 = 0.15f;
						num3 = 0.45f;
						goto IL_3473;
					case 503:
						num = 0.45f;
						num2 = 0.45f;
						num3 = 0.675f;
						goto IL_3473;
					default:
						goto IL_3473;
					}
				}
				IL_A4B:
				num = 0.25f;
				num2 = 0.1f;
				num3 = 0f;
				goto IL_3473;
				IL_EB0:
				num = 0.1f;
				num2 = 0.2f;
				num3 = 0.7f;
				goto IL_3473;
				IL_299C:
				num = 0.35f;
				goto IL_3473;
				IL_2F65:
				if (tile.type != 349 || tile.frameX >= 36)
				{
					float num24 = (float)localRandom.Next(28, 42) * 0.005f;
					num24 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
					if (tile.color() == 0)
					{
						num = 0f;
						num2 = 0.2f + num24 / 2f;
						num3 = 1f;
					}
					else
					{
						Color color2 = WorldGen.paintColor((int)tile.color());
						num = (float)color2.R / 255f;
						num2 = (float)color2.G / 255f;
						num3 = (float)color2.B / 255f;
					}
				}
			}
			IL_3473:
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

		// Token: 0x060016F1 RID: 5873 RVA: 0x004CDE44 File Offset: 0x004CC044
		private void ApplySurfaceLight(Tile tile, int x, int y, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = (float)Main.tileColor.R / 255f;
			float num5 = (float)Main.tileColor.G / 255f;
			float num6 = (float)Main.tileColor.B / 255f;
			float num7 = (num4 + num5 + num6) / 3f;
			if (tile.active() && TileID.Sets.AllowLightInWater[(int)tile.type])
			{
				if (lightColor.X < num7 && (Main.wallLight[(int)tile.wall] || tile.wall == 73 || tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)))
				{
					num = num4;
					num2 = num5;
					num3 = num6;
				}
			}
			else if ((!tile.active() || !Main.tileNoSunLight[(int)tile.type] || ((tile.slope() != 0 || tile.halfBrick() || (tile.invisibleBlock() && !this._drawInvisibleWalls)) && Main.tile[x, y - 1].liquid == 0 && Main.tile[x, y + 1].liquid == 0 && Main.tile[x - 1, y].liquid == 0 && Main.tile[x + 1, y].liquid == 0)) && lightColor.X < num7 && (Main.wallLight[(int)tile.wall] || tile.wall == 73 || tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)))
			{
				if (tile.liquid < 200)
				{
					if (!tile.halfBrick() || Main.tile[x, y - 1].liquid < 200)
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
			if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int)tile.type]) && ((tile.wall >= 88 && tile.wall <= 93) || tile.wall == 241) && tile.liquid < 255)
			{
				num = num4;
				num2 = num5;
				num3 = num6;
				int num8 = (int)(tile.wall - 88);
				if (tile.wall == 241)
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

		// Token: 0x060016F2 RID: 5874 RVA: 0x004CE270 File Offset: 0x004CC470
		private void ApplyHellLight(Tile tile, int x, int y, ref Vector3 lightColor)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0.55f + (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 2f)) * 0.08f;
			if ((!tile.active() || !Main.tileNoSunLight[(int)tile.type] || ((tile.slope() != 0 || tile.halfBrick()) && Main.tile[x, y - 1].liquid == 0 && Main.tile[x, y + 1].liquid == 0 && Main.tile[x - 1, y].liquid == 0 && Main.tile[x + 1, y].liquid == 0)) && lightColor.X < num4 && (Main.wallLight[(int)tile.wall] || tile.wall == 73 || tile.wall == 227 || (tile.invisibleWall() && !this._drawInvisibleWalls)) && tile.liquid < 200 && (!tile.halfBrick() || Main.tile[x, y - 1].liquid < 200))
			{
				num = num4;
				num2 = num4 * 0.6f;
				num3 = num4 * 0.2f;
			}
			if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int)tile.type]) && tile.wall >= 88 && tile.wall <= 93 && tile.liquid < 255)
			{
				num = num4;
				num2 = num4 * 0.6f;
				num3 = num4 * 0.2f;
				switch (tile.wall)
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

		// Token: 0x040013AB RID: 5035
		private FastRandom _random = FastRandom.CreateWithRandomSeed();

		// Token: 0x040013AC RID: 5036
		private bool _drawInvisibleWalls;
	}
}
