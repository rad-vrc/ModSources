using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000029 RID: 41
	public static class DelegateMethods
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00018B19 File Offset: 0x00016D19
		public static Color ColorLerp_BlackToWhite(float percent)
		{
			return Color.Lerp(Color.Black, Color.White, percent);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00018B2B File Offset: 0x00016D2B
		public static Color ColorLerp_HSL_H(float percent)
		{
			return Main.hslToRgb(percent, 1f, 0.5f, byte.MaxValue);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00018B42 File Offset: 0x00016D42
		public static Color ColorLerp_HSL_S(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, percent, DelegateMethods.v3_1.Z, byte.MaxValue);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00018B63 File Offset: 0x00016D63
		public static Color ColorLerp_HSL_L(float percent)
		{
			return Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, 0.15f + 0.85f * percent, byte.MaxValue);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00018B90 File Offset: 0x00016D90
		public static Color ColorLerp_HSL_O(float percent)
		{
			return Color.Lerp(Color.White, Main.hslToRgb(DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z, byte.MaxValue), percent);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public unsafe static bool SpreadDirt(int x, int y)
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
				Vector2 position;
				position..ctor((float)(x * 16), (float)(y * 16));
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
				if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[(int)(*Main.tile[x, num2].type)] && (Main.tile[x, num2].topSlope() || Main.tile[x, num2].halfBrick()))
				{
					WorldGen.SlopeTile(x, num2, 0, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)num2, 0f, 0, 0, 0);
					}
				}
				num2 = y - 1;
				if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[(int)(*Main.tile[x, num2].type)] && Main.tile[x, num2].bottomSlope())
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
						if (tile.active() && num != (int)(*tile.type) && (*tile.type == 2 || *tile.type == 23 || *tile.type == 60 || *tile.type == 70 || *tile.type == 109 || *tile.type == 199 || *tile.type == 477 || *tile.type == 492))
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
			return !(tile2 == null) && *tile2.type >= 0 && (!Main.tileSolid[(int)(*tile2.type)] || TileID.Sets.Platforms[(int)(*tile2.type)] || *tile2.type == 380);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00018F7C File Offset: 0x0001717C
		public static bool SpreadWater(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 0, 255))
			{
				Vector2 position;
				position..ctor((float)(x * 16), (float)(y * 16));
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00019080 File Offset: 0x00017280
		public static bool SpreadHoney(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 2, 255))
			{
				Vector2 position;
				position..ctor((float)(x * 16), (float)(y * 16));
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

		// Token: 0x060001A5 RID: 421 RVA: 0x0001917C File Offset: 0x0001737C
		public static bool SpreadLava(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.PlaceLiquid(x, y, 1, 255))
			{
				Vector2 position;
				position..ctor((float)(x * 16), (float)(y * 16));
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

		// Token: 0x060001A6 RID: 422 RVA: 0x00019248 File Offset: 0x00017448
		public static bool SpreadDry(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (WorldGen.EmptyLiquid(x, y))
			{
				Vector2 position;
				position..ctor((float)(x * 16), (float)(y * 16));
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

		// Token: 0x060001A7 RID: 423 RVA: 0x00019314 File Offset: 0x00017514
		public unsafe static bool SpreadTest(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (WorldGen.SolidTile(x, y, false) || *tile.wall != 0)
			{
				tile.active();
				return false;
			}
			return true;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00019350 File Offset: 0x00017550
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

		// Token: 0x060001A9 RID: 425 RVA: 0x000193D8 File Offset: 0x000175D8
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

		// Token: 0x060001AA RID: 426 RVA: 0x0001943C File Offset: 0x0001763C
		public unsafe static bool CastLightOpen(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)(*Main.tile[x, y].type)] || !Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				Lighting.AddLight(x, y, DelegateMethods.v3_1.X, DelegateMethods.v3_1.Y, DelegateMethods.v3_1.Z);
			}
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00019508 File Offset: 0x00017708
		public unsafe static bool CheckStopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (Main.tile[x, y].active() && !Main.tile[x, y].inActive() && !Main.tileSolidTop[(int)(*Main.tile[x, y].type)] && Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				DelegateMethods.CheckResultOut = true;
				return false;
			}
			return true;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000195B4 File Offset: 0x000177B4
		public unsafe static bool CastLightOpen_StopForSolids_ScaleWithDistance(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)(*Main.tile[x, y].type)] || !Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				Vector3 vector = DelegateMethods.v3_1;
				Vector2 vector2;
				vector2..ctor((float)x, (float)y);
				float num = Vector2.Distance(DelegateMethods.v2_1, vector2);
				vector *= MathHelper.Lerp(0.65f, 1f, num / DelegateMethods.f_1);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000196B0 File Offset: 0x000178B0
		public unsafe static bool CastLightOpen_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)(*Main.tile[x, y].type)] || !Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00019780 File Offset: 0x00017980
		public unsafe static bool SpreadLightOpen_StopForSolids(int x, int y)
		{
			if (Vector2.Distance(DelegateMethods.v2_1, new Vector2((float)x, (float)y)) > DelegateMethods.f_1)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)(*Main.tile[x, y].type)] || !Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				Vector3 vector = DelegateMethods.v3_1;
				new Vector2((float)x, (float)y);
				Lighting.AddLight(x, y, vector.X, vector.Y, vector.Z);
				return true;
			}
			return false;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001983C File Offset: 0x00017A3C
		public unsafe static bool EmitGolfCartDust_StopForSolids(int x, int y)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tile[x, y].active() || Main.tile[x, y].inActive() || Main.tileSolidTop[(int)(*Main.tile[x, y].type)] || !Main.tileSolid[(int)(*Main.tile[x, y].type)])
			{
				Dust.NewDustPerfect(new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)), 260, new Vector2?(Vector2.UnitY * -0.2f), 0, default(Color), 1f);
				return true;
			}
			return false;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00019924 File Offset: 0x00017B24
		public unsafe static bool NotDoorStand(int x, int y)
		{
			return !(Main.tile[x, y] != null) || !Main.tile[x, y].active() || *Main.tile[x, y].type != 11 || (*Main.tile[x, y].frameX >= 18 && *Main.tile[x, y].frameX < 54);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000199AC File Offset: 0x00017BAC
		public unsafe static bool CutTiles(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 1))
			{
				return false;
			}
			if (Main.tile[x, y] == null)
			{
				return false;
			}
			if (!Main.tileCut[(int)(*Main.tile[x, y].type)])
			{
				return true;
			}
			if (DelegateMethods.tileCutIgnore[(int)(*Main.tile[x, y].type)])
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

		// Token: 0x060001B2 RID: 434 RVA: 0x00019A50 File Offset: 0x00017C50
		public unsafe static bool SearchAvoidedByNPCs(int x, int y)
		{
			return WorldGen.InWorld(x, y, 1) && !(Main.tile[x, y] == null) && (!Main.tile[x, y].active() || !TileID.Sets.AvoidedByNPCs[(int)(*Main.tile[x, y].type)]);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00019AB8 File Offset: 0x00017CB8
		public static void RainbowLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			switch (stage)
			{
			case 0:
				distCovered = 33f;
				frame = new Rectangle(0, 0, 26, 22);
				origin = frame.Size() / 2f;
				return;
			case 1:
				frame = new Rectangle(0, 25, 26, 28);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			case 2:
				distCovered = 22f;
				frame = new Rectangle(0, 56, 26, 22);
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			default:
				distCovered = 9999f;
				frame = Rectangle.Empty;
				origin = Vector2.Zero;
				color = Color.Transparent;
				return;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00019BC0 File Offset: 0x00017DC0
		public static void TurretLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1;
			switch (stage)
			{
			case 0:
				distCovered = 32f;
				frame = new Rectangle(0, 0, 22, 20);
				origin = frame.Size() / 2f;
				return;
			case 1:
			{
				DelegateMethods.i_1++;
				int num = DelegateMethods.i_1 % 5;
				frame = new Rectangle(0, 22 * (num + 1), 22, 20);
				distCovered = (float)(frame.Height - 1);
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			case 2:
				frame = new Rectangle(0, 154, 22, 30);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			default:
				distCovered = 9999f;
				frame = Rectangle.Empty;
				origin = Vector2.Zero;
				color = Color.Transparent;
				return;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public static void LightningLaserDraw(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = DelegateMethods.c_1 * DelegateMethods.f_1;
			switch (stage)
			{
			case 0:
				distCovered = 0f;
				frame = new Rectangle(0, 0, 21, 8);
				origin = frame.Size() / 2f;
				return;
			case 1:
				frame = new Rectangle(0, 8, 21, 6);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			case 2:
				distCovered = 8f;
				frame = new Rectangle(0, 14, 21, 8);
				origin = new Vector2((float)(frame.Width / 2), 2f);
				return;
			default:
				distCovered = 9999f;
				frame = Rectangle.Empty;
				origin = Vector2.Zero;
				color = Color.Transparent;
				return;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00019DF3 File Offset: 0x00017FF3
		public static int CompareYReverse(Point a, Point b)
		{
			return b.Y.CompareTo(a.Y);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00019E07 File Offset: 0x00018007
		public static int CompareDrawSorterByYScale(DrawData a, DrawData b)
		{
			return a.scale.Y.CompareTo(b.scale.Y);
		}

		// Token: 0x0400019C RID: 412
		public static Vector3 v3_1 = Vector3.Zero;

		// Token: 0x0400019D RID: 413
		public static Vector2 v2_1 = Vector2.Zero;

		// Token: 0x0400019E RID: 414
		public static float f_1 = 0f;

		// Token: 0x0400019F RID: 415
		public static Color c_1 = Color.Transparent;

		// Token: 0x040001A0 RID: 416
		public static int i_1;

		// Token: 0x040001A1 RID: 417
		public static bool CheckResultOut;

		// Token: 0x040001A2 RID: 418
		public static TileCuttingContext tilecut_0 = TileCuttingContext.Unknown;

		// Token: 0x040001A3 RID: 419
		public static bool[] tileCutIgnore = null;

		// Token: 0x0200078F RID: 1935
		public static class CharacterPreview
		{
			// Token: 0x06004E75 RID: 20085 RVA: 0x0067401C File Offset: 0x0067221C
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

			// Token: 0x06004E76 RID: 20086 RVA: 0x00674064 File Offset: 0x00672264
			public static void CompanionCubePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					float percent3 = (float)Main.timeForVisualEffects % 30f / 30f;
					float percent2 = (float)Main.timeForVisualEffects % 120f / 120f;
					float num = Utils.MultiLerp(percent3, new float[]
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

			// Token: 0x06004E77 RID: 20087 RVA: 0x006740F2 File Offset: 0x006722F2
			public static void BerniePet(Projectile proj, bool walking)
			{
				if (walking)
				{
					proj.position.X = proj.position.X + 6f;
				}
			}

			// Token: 0x06004E78 RID: 20088 RVA: 0x0067410C File Offset: 0x0067230C
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

			// Token: 0x06004E79 RID: 20089 RVA: 0x00674158 File Offset: 0x00672358
			public static void WormPet(Projectile proj, bool walking)
			{
				float num = -0.3985988f;
				Vector2 vector = (Vector2.UnitY * 2f).RotatedBy((double)num, default(Vector2));
				Vector2 position = proj.position;
				int num2 = proj.oldPos.Length;
				if (proj.type == 893)
				{
					num2 = proj.oldPos.Length - 30;
				}
				for (int i = 0; i < proj.oldPos.Length; i++)
				{
					position -= vector;
					if (i < num2)
					{
						proj.oldPos[i] = position;
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

			// Token: 0x06004E7A RID: 20090 RVA: 0x0067427A File Offset: 0x0067247A
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

			// Token: 0x06004E7B RID: 20091 RVA: 0x006742B0 File Offset: 0x006724B0
			public static void FloatAndRotateForwardWhenWalking(Projectile proj, bool walking)
			{
				DelegateMethods.CharacterPreview.Float(proj, walking);
				DelegateMethods.CharacterPreview.RotateForwardWhenWalking(proj, walking);
			}

			// Token: 0x06004E7C RID: 20092 RVA: 0x006742C0 File Offset: 0x006724C0
			public static void Float(Projectile proj, bool walking)
			{
				float num = 0.5f;
				float num2 = (float)Main.timeForVisualEffects % 60f / 60f;
				proj.position.Y = proj.position.Y + (0f - num + (float)(Math.Cos((double)(num2 * 6.2831855f * 2f)) * (double)(num * 2f)));
			}

			// Token: 0x06004E7D RID: 20093 RVA: 0x0067431A File Offset: 0x0067251A
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

		// Token: 0x02000790 RID: 1936
		public static class Mount
		{
			// Token: 0x06004E7E RID: 20094 RVA: 0x00674336 File Offset: 0x00672536
			public static bool NoHandPosition(Player player, out Vector2? position)
			{
				position = null;
				return true;
			}

			// Token: 0x06004E7F RID: 20095 RVA: 0x00674340 File Offset: 0x00672540
			public static bool WolfMouthPosition(Player player, out Vector2? position)
			{
				Vector2 spinningpoint;
				spinningpoint..ctor((float)(player.direction * 22), player.gravDir * -6f);
				position = new Vector2?(player.RotatedRelativePoint(player.MountedCenter, false, false) + spinningpoint.RotatedBy((double)player.fullRotation, default(Vector2)));
				return true;
			}
		}

		// Token: 0x02000791 RID: 1937
		public static class Minecart
		{
			// Token: 0x06004E80 RID: 20096 RVA: 0x006743A0 File Offset: 0x006725A0
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

			// Token: 0x06004E81 RID: 20097 RVA: 0x0067453A File Offset: 0x0067273A
			public static void JumpingSound(Player Player, Vector2 Position, int Width, int Height)
			{
			}

			// Token: 0x06004E82 RID: 20098 RVA: 0x0067453C File Offset: 0x0067273C
			public static void LandingSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item53), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
			}

			// Token: 0x06004E83 RID: 20099 RVA: 0x00674564 File Offset: 0x00672764
			public static void BumperSound(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item56), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
			}

			// Token: 0x06004E84 RID: 20100 RVA: 0x0067458C File Offset: 0x0067278C
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
				Vector2 vector4 = Position + new Vector2((float)(Width / 2 - 18), (float)(Height - 16));
				Vector2 vector2 = Player.velocity * 0.1f;
				if (vector2.Length() > 2f)
				{
					vector2 = vector2.SafeNormalize(Vector2.Zero) * 2f;
				}
				int num2 = Gore.NewGore(vector4 + new Vector2(0f, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector2;
				Gore gore = Main.gore[num2];
				gore.velocity.Y = gore.velocity.Y * 0.75f;
				num2 = Gore.NewGore(vector4 + new Vector2(0f - num, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector2;
				Gore gore2 = Main.gore[num2];
				gore2.velocity.Y = gore2.velocity.Y * 0.75f;
				num2 = Gore.NewGore(vector4 + new Vector2(num, y), Vector2.Zero, Main.rand.Next(435, 438), 1f);
				Main.gore[num2].velocity *= 0.2f;
				Main.gore[num2].velocity += vector2;
				Gore gore3 = Main.gore[num2];
				gore3.velocity.Y = gore3.velocity.Y * 0.75f;
				if (Player.mount.Active && Player.mount.Type == 53)
				{
					Vector2 vector3 = Position + new Vector2((float)(Width / 2), (float)(Height + 10));
					float num3 = 30f;
					float num4 = -16f;
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustPerfect(vector3 + new Vector2(0f - num3 + num3 * 2f * Main.rand.NextFloat(), num4 * Main.rand.NextFloat()), 107, new Vector2?(Vector2.Zero), 100, Color.Lerp(new Color(64, 220, 96), Color.White, Main.rand.NextFloat() * 0.3f), 0.6f);
						dust.velocity *= (float)Main.rand.Next(15, 51) * 0.01f;
						dust.velocity.X = dust.velocity.X * ((float)Main.rand.Next(25, 101) * 0.01f);
						dust.velocity.Y = dust.velocity.Y - (float)Main.rand.Next(15, 31) * 0.1f;
						dust.velocity += vector2;
						dust.velocity.Y = dust.velocity.Y * 0.75f;
						dust.fadeIn = 0.2f + Main.rand.NextFloat() * 0.1f;
						dust.noGravity = (Main.rand.Next(3) == 0);
						dust.noLightEmittence = true;
					}
				}
			}

			// Token: 0x06004E85 RID: 20101 RVA: 0x00674929 File Offset: 0x00672B29
			public static void JumpingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item16), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06004E86 RID: 20102 RVA: 0x0067495C File Offset: 0x00672B5C
			public static void LandingSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item16), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item53), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, false);
			}

			// Token: 0x06004E87 RID: 20103 RVA: 0x006749C0 File Offset: 0x00672BC0
			public static void BumperSoundFart(Player Player, Vector2 Position, int Width, int Height)
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item16), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				SoundEngine.PlaySound(new SoundStyle?(SoundID.Item56), (int)Position.X + Width / 2, (int)Position.Y + Height / 2);
				DelegateMethods.Minecart.SpawnFartCloud(Player, Position, Width, Height, true);
			}

			// Token: 0x06004E88 RID: 20104 RVA: 0x00674A24 File Offset: 0x00672C24
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

			// Token: 0x06004E89 RID: 20105 RVA: 0x00674BAC File Offset: 0x00672DAC
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

			// Token: 0x06004E8A RID: 20106 RVA: 0x00674D64 File Offset: 0x00672F64
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

			// Token: 0x06004E8B RID: 20107 RVA: 0x00674F00 File Offset: 0x00673100
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

			// Token: 0x040065A5 RID: 26021
			public static Vector2 rotationOrigin;

			// Token: 0x040065A6 RID: 26022
			public static float rotation;
		}
	}
}
