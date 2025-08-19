using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000033 RID: 51
	public static class DelegateMethods
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0003EEAA File Offset: 0x0003D0AA
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0003EEBC File Offset: 0x0003D0BC
		public static Color ColorLerp_HSL_H(float percent)
		{
			return Main.hslToRgb(percent, 1f, 0.5f, byte.MaxValue);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0003EED3 File Offset: 0x0003D0D3
		public static Color ColorLerp_HSL_S(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, percent, DelegateMethods.v3_1.Z, byte.MaxValue);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
		public static Color ColorLerp_HSL_L(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, 0.15f + 0.85f * percent, byte.MaxValue);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0003EF21 File Offset: 0x0003D121
		public static Color ColorLerp_HSL_O(float percent)
		{
			return Color.Lerp(Color.White, Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z, byte.MaxValue), percent);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0003EF58 File Offset: 0x0003D158
		public static bool SpreadDirt(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			WorldGen.TryKillingReplaceableTile(x, y, 0);
			if (WorldGen.PlaceTile(x, y, 0, false, false, -1, 0))
			{
				if (Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, 0f, 0, 0, 0);
				}
				Vector2 position = new Vector2((float)(x * 16), (float)(y * 16));
				int num = 0;
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(position, 16, 16, num, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.noGravity = true;
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 4f;
					Dust dust2 = Dust.NewDustDirect(position, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 2f;
				}
				int num2 = y + 1;
				if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[(int)Main.tile[x, num2].type] && (Main.tile[x, num2].topSlope() || Main.tile[x, num2].halfBrick()))
				{
					WorldGen.SlopeTile(x, num2, 0, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)num2, 0f, 0, 0, 0);
					}
				}
				num2 = y - 1;
				if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[(int)Main.tile[x, num2].type] && Main.tile[x, num2].bottomSlope())
				{
					WorldGen.SlopeTile(x, num2, 0, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)num2, 0f, 0, 0, 0);
					}
				}
				for (int j = x - 1; j <= x + 1; j++)
				{
					for (int k = y - 1; k <= y + 1; k++)
					{
						Tile tile = Main.tile[j, k];
						if (tile.active() && num != (int)tile.type && (tile.type == 2 || tile.type == 23 || tile.type == 60 || tile.type == 70 || tile.type == 109 || tile.type == 199 || tile.type == 477 || tile.type == 492))
						{
							bool flag = true;
							for (int l = j - 1; l <= j + 1; l++)
							{
								for (int m = k - 1; m <= k + 1; m++)
								{
									if (!WorldGen.SolidTile(l, m, false))
									{
										flag = false;
									}
								}
							}
							if (flag)
							{
								WorldGen.KillTile(j, k, true, false, false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)j, (float)k, 1f, 0, 0, 0);
								}
							}
						}
					}
				}
				return true;
			}
			Tile tile2 = Main.tile[x, y];
			return tile2 != null && tile2.type >= 0 && tile2.type < TileID.Count && (!Main.tileSolid[(int)tile2.type] || TileID.Sets.Platforms[(int)tile2.type] || tile2.type == 380);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0003F2F8 File Offset: 0x0003D4F8
		public static bool SpreadWater(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 0, 255))
			{
				Vector2 position = new Vector2((float)(x * 16), (float)(y * 16));
				int type = Dust.dustWater();
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.noGravity = true;
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 7f;
					Dust dust2 = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0003F3FC File Offset: 0x0003D5FC
		public static bool SpreadHoney(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 2, 255))
			{
				Vector2 position = new Vector2((float)(x * 16), (float)(y * 16));
				int type = 152;
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 2.2f);
					dust.velocity.Y = dust.velocity.Y - 1.2f;
					dust.velocity *= 7f;
					Dust dust2 = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 1.3f);
					dust2.velocity.Y = dust2.velocity.Y - 1.2f;
					dust2.velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public static bool SpreadLava(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 1, 255))
			{
				Vector2 position = new Vector2((float)(x * 16), (float)(y * 16));
				int type = 35;
				for (int i = 0; i < 3; i++)
				{
					Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 1.2f).velocity *= 7f;
					Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 0.8f).velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
		public static bool SpreadDry(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.EmptyLiquid(x, y))
			{
				Vector2 position = new Vector2((float)(x * 16), (float)(y * 16));
				int type = 31;
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 1.2f);
					dust.noGravity = true;
					dust.velocity *= 7f;
					Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 0.8f).velocity *= 4f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0003F690 File Offset: 0x0003D890
		public static bool SpreadTest(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (WorldGen.SolidTile(x, y, false) || tile.wall != 0)
			{
				tile.active();
				return false;
			}
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
		public static bool TestDust(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			int num = Dust.NewDust(new Vector2((float)x, (float)y) * 16f + new Vector2(8f), 0, 0, 6, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num].noGravity = true;
			Main.dust[num].noLight = true;
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0003F750 File Offset: 0x0003D950
		public static bool CastLight(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
			return true;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0003F7B0 File Offset: 0x0003D9B0
		public static bool CastLightOpen(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
			}
			return true;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0003F868 File Offset: 0x0003DA68
		public static bool CheckStopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int)Main.tile[x, y].type] && Main.tileSolid[(int)Main.tile[x, y].type])
			{
				DelegateMethods.CheckResultOut = true;
				return false;
			}
			return true;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0003F900 File Offset: 0x0003DB00
		public static bool CastLightOpen_StopForSolids_ScaleWithDistance(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				Vector2 value = new Vector2((float)x, (float)y);
				float num = Vector2.Distance(DelegateMethods.v2_1, value);
				vector *= MathHelper.Lerp(0.65f, 1f, num / DelegateMethods.f_1);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public static bool CastLightOpen_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
		public static bool SpreadLightOpen_StopForSolids(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0003FB58 File Offset: 0x0003DD58
		public static bool EmitGolfCartDust_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)Main.tile[x, y].type] || !Main.tileSolid[(int)Main.tile[x, y].type])
			{
				Dust.NewDustPerfect(new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)), 260, new Vector2?(Vector2.UnitY * -0.2f), 0, default(Color), 1f);
				return true;
			}
			return false;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0003FC30 File Offset: 0x0003DE30
		public static bool NotDoorStand(int x, int y)
		{
			return Main.tile[x, y] == null || !Main.tile[x, y].active() || Main.tile[x, y].type != 11 || (Main.tile[x, y].frameX >= 18 && Main.tile[x, y].frameX < 54);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0003FCA4 File Offset: 0x0003DEA4
		public static bool CutTiles(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 1))
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tileCut[(int)Main.tile[x, y].type])
			{
				return true;
			}
			if (DelegateMethods.tileCutIgnore[(int)Main.tile[x, y].type])
			{
				return true;
			}
			if (WorldGen.CanCutTile(x, y, DelegateMethods.tilecut_0))
			{
				WorldGen.KillTile(x, y, false, false, false);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
				}
			}
			return true;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0003FD3C File Offset: 0x0003DF3C
		public static bool SearchAvoidedByNPCs(int x, int y)
		{
			return WorldGen.InWorld(x, y, 1) && Main.tile[x, y] != null && (!Main.tile[x, y].active() || !TileID.Sets.AvoidedByNPCs[(int)Main.tile[x, y].type]);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0003FD94 File Offset: 0x0003DF94
		public static void RainbowLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			if (stage == 0)
			{
				distCovered = 33f;
				frame = new Rectangle(0, 0, 26, 22);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 25, 26, 28);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 22f;
				frame = new Rectangle(0, 56, 26, 22);
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0003FE90 File Offset: 0x0003E090
		public static void TurretLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			if (stage == 0)
			{
				distCovered = 32f;
				frame = new Rectangle(0, 0, 22, 20);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				DelegateMethods.i_1++;
				int num = DelegateMethods.i_1 % 5;
				frame = new Rectangle(0, 22 * (num + 1), 22, 20);
				distCovered = (float)(frame.Height - 1);
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				frame = new Rectangle(0, 154, 22, 30);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0003FFAC File Offset: 0x0003E1AC
		public static void LightningLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1 * DelegateMethods.f_1;
			if (stage == 0)
			{
				distCovered = 0f;
				frame = new Rectangle(0, 0, 21, 8);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 8, 21, 6);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 8f;
				frame = new Rectangle(0, 14, 21, 8);
				origin = new Vector2((float)(frame.Width / 2), 2f);
				return;
			}
			distCovered = 9999f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000400AB File Offset: 0x0003E2AB
		public static int CompareYReverse(Point a, Point b)
		{
			return b.Y.CompareTo(a.Y);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000400BF File Offset: 0x0003E2BF
		public static int CompareDrawSorterByYScale(DrawData a, DrawData b)
		{
			return a.scale.Y.CompareTo(b.scale.Y);
		}

		// Token: 0x04000228 RID: 552
		public static Vector3 v3_1 = Vector3.Zero;

		// Token: 0x04000229 RID: 553
		public static Vector2 v2_1 = Vector2.Zero;

		// Token: 0x0400022A RID: 554
		public static float f_1 = 0f;

		// Token: 0x0400022B RID: 555
		public static Color c_1 = Color.Transparent;

		// Token: 0x0400022C RID: 556
		public static int i_1;

		// Token: 0x0400022D RID: 557
		public static bool CheckResultOut;

		// Token: 0x0400022E RID: 558
		public static TileCuttingContext tilecut_0 = TileCuttingContext.Unknown;

		// Token: 0x0400022F RID: 559
		public static bool[] tileCutIgnore = null;

		// Token: 0x020004B1 RID: 1201
		public static class CharacterPreview
		{
			// Token: 0x06002ED1 RID: 11985 RVA: 0x005C50C8 File Offset: 0x005C32C8
			public static void EtsyPet(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				if (walking)
				{
					float num = (float)Main.timeForVisualEffects % 90f / 90f;
					proj.localAI[1] = 6.2831855f * num;
					return;
				}
				proj.localAI[1] = 0f;
			}

			// Token: 0x06002ED2 RID: 11986 RVA: 0x005C5110 File Offset: 0x005C3310
			public static void CompanionCubePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					float percent = (float)Main.timeForVisualEffects % 30f / 30f;
					float percent2 = (float)Main.timeForVisualEffects % 120f / 120f;
					float num = Utils.MultiLerp(percent, new float[]
					{
						0f,
						0f,
						16f,
						20f,
						20f,
						16f,
						0f,
						0f
					});
					float num2 = Utils.MultiLerp(percent2, new float[]
					{
						0f,
						0f,
						0.25f,
						0.25f,
						0.5f,
						0.5f,
						0.75f,
						0.75f,
						1f,
						1f
					});
					proj.position.Y = proj.position.Y - num;
					proj.rotation = 6.2831855f * num2;
					return;
				}
				proj.rotation = 0f;
			}

			// Token: 0x06002ED3 RID: 11987 RVA: 0x005C519E File Offset: 0x005C339E
			public static void BerniePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.position.X = proj.position.X + 6f;
				}
			}

			// Token: 0x06002ED4 RID: 11988 RVA: 0x005C51B8 File Offset: 0x005C33B8
			public static void SlimePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					float percent = (float)Main.timeForVisualEffects % 30f / 30f;
					proj.position.Y = proj.position.Y - Utils.MultiLerp(percent, new float[]
					{
						0f,
						0f,
						16f,
						20f,
						20f,
						16f,
						0f,
						0f
					});
				}
			}

			// Token: 0x06002ED5 RID: 11989 RVA: 0x005C5204 File Offset: 0x005C3404
			public static void WormPet(Projectile proj, bool walking)
			{
				float num = -0.3985988f;
				Vector2 vector = (Vector2.UnitY * 2f).RotatedBy((double)num, default(Vector2));
				Vector2 vector2 = proj.position;
				int num2 = proj.oldPos.Length;
				if (proj.type == 893)
				{
					num2 = proj.oldPos.Length - 30;
				}
				for (int i = 0; i < proj.oldPos.Length; i++)
				{
					vector2 -= vector;
					if (i < num2)
					{
						proj.oldPos[i] = vector2;
					}
					else if (i > 0)
					{
						proj.oldPos[i] = proj.oldPos[i - 1];
					}
					vector = vector.RotatedBy(-0.05235987901687622, default(Vector2));
				}
				proj.rotation = vector.ToRotation() + 0.31415927f + 3.1415927f;
				if (proj.type == 887)
				{
					proj.rotation += 0.3926991f;
				}
				if (proj.type == 893)
				{
					proj.rotation += 1.5707964f;
				}
			}

			// Token: 0x06002ED6 RID: 11990 RVA: 0x005C5326 File Offset: 0x005C3526
			public static void FloatAndSpinWhenWalking(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				if (walking)
				{
					proj.rotation = 6.2831855f * ((float)Main.timeForVisualEffects % 20f / 20f);
					return;
				}
				proj.rotation = 0f;
			}

			// Token: 0x06002ED7 RID: 11991 RVA: 0x005C535C File Offset: 0x005C355C
			public static void FloatAndRotateForwardWhenWalking(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				DelegateMethods.CharacterPreview.RotateForwardWhenWalking(proj, walking);
			}

			// Token: 0x06002ED8 RID: 11992 RVA: 0x005C536C File Offset: 0x005C356C
			public static void Float(Projectile proj, bool walking)
			{
				float num = 0.5f;
				float num2 = (float)Main.timeForVisualEffects % 60f / 60f;
				proj.position.Y = proj.position.Y + (-num + (float)(Math.Cos((double)(num2 * 6.2831855f * 2f)) * (double)(num * 2f)));
			}

			// Token: 0x06002ED9 RID: 11993 RVA: 0x005C53C1 File Offset: 0x005C35C1
			public static void RotateForwardWhenWalking(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.rotation = 0.5235988f;
					return;
				}
				proj.rotation = 0f;
			}
		}

		// Token: 0x020004B2 RID: 1202
		public static class Mount
		{
			// Token: 0x06002EDA RID: 11994 RVA: 0x005C53DD File Offset: 0x005C35DD
			public static bool NoHandPosition(Player player, out Vector2? position)
			{
				position = null;
				return true;
			}

			// Token: 0x06002EDB RID: 11995 RVA: 0x005C53E8 File Offset: 0x005C35E8
			public static bool WolfMouthPosition(Player player, out Vector2? position)
			{
				Vector2 spinningpoint = new Vector2((float)(player.direction * 22), player.gravDir * -6f);
				position = new Vector2?(player.RotatedRelativePoint(player.MountedCenter, false, false) + spinningpoint.RotatedBy((double)player.fullRotation, default(Vector2)));
				return true;
			}
		}

		// Token: 0x020004B3 RID: 1203
		public static class Minecart
		{
			// Token: 0x06002EDC RID: 11996 RVA: 0x005C5448 File Offset: 0x005C3648
			public static void Sparks(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : -13), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 213, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 1f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x06002EDD RID: 11997 RVA: 0x0003C3EC File Offset: 0x0003A5EC
			public static void JumpingSound(Player Player, Vector2 Position, int Width, int Height)
			{
			}

			// Token: 0x06002EDE RID: 11998 RVA: 0x005C55E2 File Offset: 0x005C37E2
			public static void LandingSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item53, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
			}

			// Token: 0x06002EDF RID: 11999 RVA: 0x005C5605 File Offset: 0x005C3805
			public static void BumperSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item56, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
			}

			// Token: 0x06002EE0 RID: 12000 RVA: 0x005C5628 File Offset: 0x005C3828
			public static void SpawnFartCloud(Player Player, Vector2 Position, int Width, int Height, bool useDelay = true)
			{
				if (useDelay)
				{
					if (Player.fartKartCloudDelay > 0)
					{
						return;
					}
					Player.fartKartCloudDelay = 20;
				}
				float num = 10f;
				float y = -4f;
				Vector2 value = Position + new Vector2((float)(Width / 2 - 18), (float)(Height - 16));
				Vector2 vector = Player.velocity * 0.1f;
				if (vector.Length() > 2f)
				{
					vector = vector.SafeNormalize(Vector2.Zero) * 2f;
				}
				int num2 = Gore.NewGore(value + new Vector2(0f, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector;
				Gore gore = Main.gore[num2];
				gore.velocity.Y = gore.velocity.Y * 0.75f;
				num2 = Gore.NewGore(value + new Vector2(-num, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector;
				Gore gore2 = Main.gore[num2];
				gore2.velocity.Y = gore2.velocity.Y * 0.75f;
				num2 = Gore.NewGore(value + new Vector2(num, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector;
				Gore gore3 = Main.gore[num2];
				gore3.velocity.Y = gore3.velocity.Y * 0.75f;
				if (Player.mount.Active && Player.mount.Type == 53)
				{
					Vector2 value2 = Position + new Vector2((float)(Width / 2), (float)(Height + 10));
					float num3 = 30f;
					float num4 = -16f;
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustPerfect(value2 + new Vector2(-num3 + num3 * 2f * Main.rand.NextFloat(), num4 * Main.rand.NextFloat()), 107, new Vector2?(Vector2.Zero), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.6f);
						dust.velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
						dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
						dust.velocity.Y = dust.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
						dust.velocity += vector;
						dust.velocity.Y = dust.velocity.Y * 0.75f;
						dust.fadeIn = 0.2f + Main.rand.NextFloat() * 0.1f;
						dust.noGravity = (Main.rand.Next(3) == 0);
						dust.noLightEmittence = true;
					}
				}
			}

			// Token: 0x06002EE1 RID: 12001 RVA: 0x005C59BB File Offset: 0x005C3BBB
			public static void JumpingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06002EE2 RID: 12002 RVA: 0x005C59E8 File Offset: 0x005C3BE8
			public static void LandingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				SoundEngine.PlaySound(SoundID.Item53, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06002EE3 RID: 12003 RVA: 0x005C5A44 File Offset: 0x005C3C44
			public static void BumperSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(SoundID.Item16, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				SoundEngine.PlaySound(SoundID.Item56, (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, true);
			}

			// Token: 0x06002EE4 RID: 12004 RVA: 0x005C5AA0 File Offset: 0x005C3CA0
			public static void SparksFart(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : -13), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 211, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 50, default(Color), 0.8f);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				Main.dust[num].noLight = true;
				Main.dust[num].noGravity = (Main.rand.Next(3) == 0);
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
			}

			// Token: 0x06002EE5 RID: 12005 RVA: 0x005C5C28 File Offset: 0x005C3E28
			public static void SparksTerraFart(Vector2 dustPosition)
			{
				if (Main.rand.Next(2) == 0)
				{
					DelegateMethods.Minecart.SparksFart(dustPosition);
					return;
				}
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : -13), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 107, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.8f);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].alpha += 25;
				}
				Main.dust[num].noLightEmittence = true;
				Main.dust[num].noGravity = (Main.rand.Next(3) == 0);
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
			}

			// Token: 0x06002EE6 RID: 12006 RVA: 0x005C5DE0 File Offset: 0x005C3FE0
			public static void SparksMech(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : -13), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 260, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 0.5f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x06002EE7 RID: 12007 RVA: 0x005C5F7C File Offset: 0x005C417C
			public static void SparksMeow(Vector2 dustPosition)
			{
				dustPosition += new Vector2((float)((Main.rand.Next(2) == 0) ? 13 : -13), 0f).RotatedBy((double)DelegateMethods.Minecart.rotation, default(Vector2));
				int num = Dust.NewDust(dustPosition, 1, 1, 213, (float)Main.rand.Next(-2, 3), (float)Main.rand.Next(-2, 3), 0, default(Color), 1f);
				Main.dust[num].shader = GameShaders.Armor.GetShaderFromItemId(2870);
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = Main.dust[num].scale + 1f + 0.01f * (float)Main.rand.Next(0, 51);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
				Dust dust = Main.dust[num];
				dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
				Dust dust2 = Main.dust[num];
				dust2.velocity.Y = dust2.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
				Dust dust3 = Main.dust[num];
				dust3.position.Y = dust3.position.Y - 4f;
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num].noGravity = false;
					return;
				}
				Main.dust[num].scale *= 0.6f;
			}

			// Token: 0x04005643 RID: 22083
			public static Vector2 rotationOrigin;

			// Token: 0x04005644 RID: 22084
			public static float rotation;
		}
	}
}
