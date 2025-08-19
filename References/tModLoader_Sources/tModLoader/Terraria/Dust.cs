using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria
{
	/// <summary>
	/// Dust are particles effects used to add visual elements to weapons and other effects. Dust are completely visual and should never be used as a gameplay element. Dust count limits are imposed by the game to keep performance consistent, so Dust are not guaranteed to spawn when code attempts to spawn them.<para />
	/// Vanilla Dust are enumerated in the <see cref="T:Terraria.ID.DustID" /> class.<para />
	/// New Dust can be implemented using the <see cref="T:Terraria.ModLoader.ModDust" /> class.<para />
	/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Dust">Basic Dust Guide</see> teaches the basics on using Dust. In addition, the guide has resources teaching how to discover and use vanilla Dust.
	/// </summary>
	// Token: 0x0200002A RID: 42
	public class Dust
	{
		/// <summary>
		/// Attempts to spawn a single Dust into the game world. <paramref name="Position" /> indicates the spawn position and <paramref name="Velocity" /> indicates the initial velocity. Unlike <see cref="M:Terraria.Dust.NewDust(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Int32,Microsoft.Xna.Framework.Color,System.Single)" />, position and velocity will not be slightly randomized, making this method suitable for visual effects that need exact positioning.
		/// </summary>
		/// <param name="Position"></param>
		/// <param name="Type"></param>
		/// <param name="Velocity"></param>
		/// <param name="Alpha"></param>
		/// <param name="newColor"></param>
		/// <param name="Scale"></param>
		/// <returns>The <see cref="T:Terraria.Dust" /> instance spawned</returns>
		// Token: 0x060001B9 RID: 441 RVA: 0x00019E5C File Offset: 0x0001805C
		public static Dust NewDustPerfect(Vector2 Position, int Type, Vector2? Velocity = null, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			Dust dust = Main.dust[Dust.NewDust(Position, 0, 0, Type, 0f, 0f, Alpha, newColor, Scale)];
			dust.position = Position;
			if (Velocity != null)
			{
				dust.velocity = Velocity.Value;
			}
			return dust;
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.Dust.NewDust(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Int32,Microsoft.Xna.Framework.Color,System.Single)" />
		/// <para /> Unlike <see cref="M:Terraria.Dust.NewDust(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Int32,Microsoft.Xna.Framework.Color,System.Single)" />, this method returns a <see cref="T:Terraria.Dust" /> instance directly.
		/// </summary>
		/// <param name="Position"></param>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <param name="Type"></param>
		/// <param name="SpeedX"></param>
		/// <param name="SpeedY"></param>
		/// <param name="Alpha"></param>
		/// <param name="newColor"></param>
		/// <param name="Scale"></param>
		/// <returns>The <see cref="T:Terraria.Dust" /> instance spawned</returns>
		// Token: 0x060001BA RID: 442 RVA: 0x00019EA8 File Offset: 0x000180A8
		public static Dust NewDustDirect(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0f, float SpeedY = 0f, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			Dust dust = Main.dust[Dust.NewDust(Position, Width, Height, Type, SpeedX, SpeedY, Alpha, newColor, Scale)];
			if (dust.velocity.HasNaNs())
			{
				dust.velocity = Vector2.Zero;
			}
			return dust;
		}

		/// <summary>
		/// Attempts to spawn a single Dust into the game world. The Position, Width, and Height parameters dictate a rectangle, the dust will be spawned randomly within that rectangle. SpeedX and SpeedY dictate the initial velocity, they will be slightly randomized as well.
		/// </summary>
		/// <param name="Position"></param>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		/// <param name="Type">Either an <see cref="T:Terraria.ID.DustID" /> entry or <see cref="M:Terraria.ModLoader.ModContent.DustType``1" /></param>
		/// <param name="SpeedX"></param>
		/// <param name="SpeedY"></param>
		/// <param name="Alpha"></param>
		/// <param name="newColor"></param>
		/// <param name="Scale"></param>
		/// <returns>The index of the dust within <see cref="F:Terraria.Main.dust" /></returns>
		// Token: 0x060001BB RID: 443 RVA: 0x00019EE8 File Offset: 0x000180E8
		public static int NewDust(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0f, float SpeedY = 0f, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			if (Main.gameMenu)
			{
				return 6000;
			}
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
			}
			if (Main.gamePaused)
			{
				return 6000;
			}
			if (WorldGen.gen)
			{
				return 6000;
			}
			if (Main.netMode == 2)
			{
				return 6000;
			}
			int num = (int)(400f * (1f - Dust.dCount));
			Rectangle rectangle;
			rectangle..ctor((int)(Main.screenPosition.X - (float)num), (int)(Main.screenPosition.Y - (float)num), Main.screenWidth + num * 2, Main.screenHeight + num * 2);
			Rectangle value;
			value..ctor((int)Position.X, (int)Position.Y, 10, 10);
			if (!rectangle.Intersects(value))
			{
				return 6000;
			}
			int result = 6000;
			int i = 0;
			while (i < 6000)
			{
				Dust dust = Main.dust[i];
				if (!dust.active)
				{
					if ((double)i > (double)Main.maxDustToDraw * 0.9)
					{
						if (Main.rand.Next(4) != 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.8)
					{
						if (Main.rand.Next(3) != 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.7)
					{
						if (Main.rand.Next(2) == 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.6)
					{
						if (Main.rand.Next(4) == 0)
						{
							return 6000;
						}
					}
					else if ((double)i > (double)Main.maxDustToDraw * 0.5)
					{
						if (Main.rand.Next(5) == 0)
						{
							return 6000;
						}
					}
					else
					{
						Dust.dCount = 0f;
					}
					int num2 = Width;
					int num3 = Height;
					if (num2 < 5)
					{
						num2 = 5;
					}
					if (num3 < 5)
					{
						num3 = 5;
					}
					result = i;
					dust.fadeIn = 0f;
					dust.active = true;
					dust.type = Type;
					dust.noGravity = false;
					dust.color = newColor;
					dust.alpha = Alpha;
					dust.position.X = Position.X + (float)Main.rand.Next(num2 - 4) + 4f;
					dust.position.Y = Position.Y + (float)Main.rand.Next(num3 - 4) + 4f;
					dust.velocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedX;
					dust.velocity.Y = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedY;
					dust.frame.X = 10 * Type;
					dust.frame.Y = 10 * Main.rand.Next(3);
					dust.shader = null;
					dust.customData = null;
					dust.noLightEmittence = false;
					int num4 = Type;
					while (num4 >= 100)
					{
						num4 -= 100;
						Dust dust2 = dust;
						dust2.frame.X = dust2.frame.X - 1000;
						Dust dust3 = dust;
						dust3.frame.Y = dust3.frame.Y + 30;
					}
					dust.frame.Width = 8;
					dust.frame.Height = 8;
					dust.rotation = 0f;
					dust.scale = 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
					dust.scale *= Scale;
					dust.noLight = false;
					dust.firstFrame = true;
					if (dust.type == 228 || dust.type == 279 || dust.type == 269 || dust.type == 135 || dust.type == 6 || dust.type == 242 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158 || dust.type == 293 || dust.type == 294 || dust.type == 295 || dust.type == 296 || dust.type == 297 || dust.type == 298 || dust.type == 302 || dust.type == 307 || dust.type == 310)
					{
						dust.velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
						Dust dust4 = dust;
						dust4.velocity.X = dust4.velocity.X * 0.3f;
						dust.scale *= 0.7f;
					}
					if (dust.type == 127 || dust.type == 187)
					{
						dust.velocity *= 0.3f;
						dust.scale *= 0.7f;
					}
					if (dust.type == 308)
					{
						dust.velocity *= 0.5f;
						Dust dust5 = dust;
						dust5.velocity.Y = dust5.velocity.Y + 1f;
					}
					if (dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105)
					{
						dust.alpha = 170;
						dust.velocity *= 0.5f;
						Dust dust6 = dust;
						dust6.velocity.Y = dust6.velocity.Y + 1f;
					}
					if (dust.type == 41)
					{
						dust.velocity *= 0f;
					}
					if (dust.type == 80)
					{
						dust.alpha = 50;
					}
					DustLoader.SetupDust(dust);
					if (dust.type != 34 && dust.type != 35 && dust.type != 152)
					{
						break;
					}
					dust.velocity *= 0.1f;
					dust.velocity.Y = -0.5f;
					if (dust.type == 34 && !Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
					{
						dust.active = false;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001A60D File Offset: 0x0001880D
		public static Dust CloneDust(int dustIndex)
		{
			return Dust.CloneDust(Main.dust[dustIndex]);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001A61C File Offset: 0x0001881C
		public static Dust CloneDust(Dust rf)
		{
			if (rf.dustIndex == Main.maxDustToDraw)
			{
				return rf;
			}
			int num = Dust.NewDust(rf.position, 0, 0, rf.type, 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[num];
			dust.position = rf.position;
			dust.velocity = rf.velocity;
			dust.fadeIn = rf.fadeIn;
			dust.noGravity = rf.noGravity;
			dust.scale = rf.scale;
			dust.rotation = rf.rotation;
			dust.noLight = rf.noLight;
			dust.active = rf.active;
			dust.type = rf.type;
			dust.color = rf.color;
			dust.alpha = rf.alpha;
			dust.frame = rf.frame;
			dust.shader = rf.shader;
			dust.customData = rf.customData;
			return dust;
		}

		/// <inheritdoc cref="M:Terraria.Dust.QuickDust(Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)" />
		// Token: 0x060001BE RID: 446 RVA: 0x0001A714 File Offset: 0x00018914
		public static Dust QuickDust(int x, int y, Color color)
		{
			return Dust.QuickDust(new Point(x, y), color);
		}

		/// <inheritdoc cref="M:Terraria.Dust.QuickDust(Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)" />
		// Token: 0x060001BF RID: 447 RVA: 0x0001A723 File Offset: 0x00018923
		public static Dust QuickDust(Point tileCoords, Color color)
		{
			return Dust.QuickDust(tileCoords.ToWorldCoordinates(8f, 8f), color);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001A73C File Offset: 0x0001893C
		public static void QuickBox(Vector2 topLeft, Vector2 bottomRight, int divisions, Color color, Action<Dust> manipulator)
		{
			float num = (float)(divisions + 2);
			for (float num2 = 0f; num2 <= (float)(divisions + 2); num2 += 1f)
			{
				Dust obj = Dust.QuickDust(new Vector2(MathHelper.Lerp(topLeft.X, bottomRight.X, num2 / num), topLeft.Y), color);
				if (manipulator != null)
				{
					manipulator(obj);
				}
				obj = Dust.QuickDust(new Vector2(MathHelper.Lerp(topLeft.X, bottomRight.X, num2 / num), bottomRight.Y), color);
				if (manipulator != null)
				{
					manipulator(obj);
				}
				obj = Dust.QuickDust(new Vector2(topLeft.X, MathHelper.Lerp(topLeft.Y, bottomRight.Y, num2 / num)), color);
				if (manipulator != null)
				{
					manipulator(obj);
				}
				obj = Dust.QuickDust(new Vector2(bottomRight.X, MathHelper.Lerp(topLeft.Y, bottomRight.Y, num2 / num)), color);
				if (manipulator != null)
				{
					manipulator(obj);
				}
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001A834 File Offset: 0x00018A34
		public static void DrawDebugBox(Rectangle itemRectangle)
		{
			Vector2 vector = itemRectangle.TopLeft();
			itemRectangle.BottomRight();
			for (int i = 0; i <= itemRectangle.Width; i++)
			{
				for (int j = 0; j <= itemRectangle.Height; j++)
				{
					if (i == 0 || j == 0 || i == itemRectangle.Width - 1 || j == itemRectangle.Height - 1)
					{
						Dust.QuickDust(vector + new Vector2((float)i, (float)j), Color.White).scale = 1f;
					}
				}
			}
		}

		/// <summary>
		/// Spawns dust 267 tinted to the provided color at the specified position with no velocity or gravity. Used solely for debugging purposes.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		// Token: 0x060001C2 RID: 450 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public static Dust QuickDust(Vector2 pos, Color color)
		{
			Dust dust = Main.dust[Dust.NewDust(pos, 0, 0, 267, 0f, 0f, 0, default(Color), 1f)];
			dust.position = pos;
			dust.velocity = Vector2.Zero;
			dust.fadeIn = 1f;
			dust.noLight = true;
			dust.noGravity = true;
			dust.color = color;
			return dust;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001A91C File Offset: 0x00018B1C
		public static Dust QuickDustSmall(Vector2 pos, Color color, bool floorPositionValues = false)
		{
			Dust dust = Dust.QuickDust(pos, color);
			dust.fadeIn = 0f;
			dust.scale = 0.35f;
			if (floorPositionValues)
			{
				dust.position = dust.position.Floor();
			}
			return dust;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001A95C File Offset: 0x00018B5C
		public static void QuickDustLine(Vector2 start, Vector2 end, float splits, Color color)
		{
			Dust.QuickDust(start, color).scale = 0.3f;
			Dust.QuickDust(end, color).scale = 0.3f;
			float num = 1f / splits;
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				Dust.QuickDust(Vector2.Lerp(start, end, num2), color).scale = 0.3f;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		public static int dustWater()
		{
			if (Main.waterStyle >= 15)
			{
				return LoaderManager.Get<WaterStylesLoader>().Get(Main.waterStyle).GetSplashDust();
			}
			switch (Main.waterStyle)
			{
			case 2:
				return 98;
			case 3:
				return 99;
			case 4:
				return 100;
			case 5:
				return 101;
			case 6:
				return 102;
			case 7:
				return 103;
			case 8:
				return 104;
			case 9:
				return 105;
			case 10:
				return 123;
			case 12:
				return 288;
			}
			return 33;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public static void UpdateDust()
		{
			int num = 0;
			Dust.lavaBubbles = 0;
			Main.snowDust = 0;
			Dust.SandStormCount = 0;
			bool flag = Sandstorm.ShouldSandstormDustPersist();
			for (int i = 0; i < 6000; i++)
			{
				Dust dust = Main.dust[i];
				if (i < Main.maxDustToDraw)
				{
					if (dust.active)
					{
						Dust.dCount += 1f;
						DustLoader.SetupUpdateType(dust);
						ModDust modDust = DustLoader.GetDust(dust.type);
						if (modDust != null && !modDust.Update(dust))
						{
							DustLoader.TakeDownUpdateType(dust);
						}
						else
						{
							if (dust.scale > 10f)
							{
								dust.active = false;
							}
							if (dust.firstFrame && !ChildSafety.Disabled && ChildSafety.DangerousDust(dust.type))
							{
								if (Main.rand.Next(2) == 0)
								{
									dust.firstFrame = false;
									dust.type = 16;
									dust.scale = Main.rand.NextFloat() * 1.6f + 0.3f;
									dust.color = Color.Transparent;
									dust.frame.X = 10 * dust.type;
									dust.frame.Y = 10 * Main.rand.Next(3);
									dust.shader = null;
									dust.customData = null;
									int num2 = dust.type / 100;
									Dust dust2 = dust;
									dust2.frame.X = dust2.frame.X - 1000 * num2;
									Dust dust3 = dust;
									dust3.frame.Y = dust3.frame.Y + 30 * num2;
									dust.noGravity = true;
								}
								else
								{
									dust.active = false;
								}
							}
							int num3 = dust.type;
							if (num3 - 299 <= 2 || num3 == 305)
							{
								dust.scale *= 0.96f;
								Dust dust4 = dust;
								dust4.velocity.Y = dust4.velocity.Y - 0.01f;
							}
							if (dust.type == 35)
							{
								Dust.lavaBubbles++;
							}
							dust.position += dust.velocity;
							if (dust.type == 258)
							{
								dust.noGravity = true;
								dust.scale += 0.015f;
							}
							if (dust.type == 309)
							{
								float r = (float)dust.color.R / 255f * dust.scale;
								float g = (float)dust.color.G / 255f * dust.scale;
								float b = (float)dust.color.B / 255f * dust.scale;
								Lighting.AddLight(dust.position, r, g, b);
								dust.scale *= 0.97f;
							}
							if (((dust.type >= 86 && dust.type <= 92) || dust.type == 286) && !dust.noLight && !dust.noLightEmittence)
							{
								float num4 = dust.scale * 0.6f;
								if (num4 > 1f)
								{
									num4 = 1f;
								}
								int num5 = dust.type - 85;
								float num6 = num4;
								float num7 = num4;
								float num8 = num4;
								switch (num5)
								{
								case 1:
									num6 *= 0.9f;
									num7 *= 0f;
									num8 *= 0.9f;
									break;
								case 2:
									num6 *= 0.9f;
									num7 *= 0.9f;
									num8 *= 0f;
									break;
								case 3:
									num6 *= 0f;
									num7 *= 0.1f;
									num8 *= 1.3f;
									break;
								case 4:
									num6 *= 0f;
									num7 *= 1f;
									num8 *= 0.1f;
									break;
								case 5:
									num6 *= 1f;
									num7 *= 0.1f;
									num8 *= 0.1f;
									break;
								case 6:
									num6 *= 1.3f;
									num7 *= 1.3f;
									num8 *= 1.3f;
									break;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num4 * num6, num4 * num7, num4 * num8);
							}
							if ((dust.type >= 86 && dust.type <= 92) || dust.type == 286)
							{
								if (dust.customData != null && dust.customData is Player)
								{
									Player player = (Player)dust.customData;
									dust.position += player.position - player.oldPosition;
								}
								else if (dust.customData != null && dust.customData is Projectile)
								{
									Projectile projectile = (Projectile)dust.customData;
									if (projectile.active)
									{
										dust.position += projectile.position - projectile.oldPosition;
									}
								}
							}
							if (dust.type == 262 && !dust.noLight)
							{
								Vector3 rgb = new Vector3(0.9f, 0.6f, 0f) * dust.scale * 0.6f;
								Lighting.AddLight(dust.position, rgb);
							}
							if (dust.type == 240 && dust.customData != null && dust.customData is Projectile)
							{
								Projectile projectile2 = (Projectile)dust.customData;
								if (projectile2.active)
								{
									dust.position += projectile2.position - projectile2.oldPosition;
								}
							}
							if ((dust.type == 259 || dust.type == 6 || dust.type == 158 || dust.type == 135) && dust.customData != null && dust.customData is int)
							{
								if ((int)dust.customData == 0)
								{
									if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
									{
										dust.scale *= 0.9f;
										dust.velocity *= 0.25f;
									}
								}
								else if ((int)dust.customData == 1)
								{
									dust.scale *= 0.98f;
									Dust dust5 = dust;
									dust5.velocity.Y = dust5.velocity.Y * 0.98f;
									if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
									{
										dust.scale *= 0.9f;
										dust.velocity *= 0.25f;
									}
								}
							}
							if (dust.type == 263 || dust.type == 264)
							{
								if (!dust.noLight)
								{
									Vector3 rgb2 = dust.color.ToVector3() * dust.scale * 0.4f;
									Lighting.AddLight(dust.position, rgb2);
								}
								if (dust.customData != null && dust.customData is Player)
								{
									Player player2 = (Player)dust.customData;
									dust.position += player2.position - player2.oldPosition;
									dust.customData = null;
								}
								else if (dust.customData != null && dust.customData is Projectile)
								{
									Projectile projectile3 = (Projectile)dust.customData;
									dust.position += projectile3.position - projectile3.oldPosition;
								}
							}
							if (dust.type == 230)
							{
								float num9 = dust.scale * 0.6f;
								float num10 = num9;
								float num11 = num9;
								float num12 = num9;
								num10 *= 0.5f;
								num11 *= 0.9f;
								num12 *= 1f;
								dust.scale += 0.02f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num9 * num10, num9 * num11, num9 * num12);
								if (dust.customData != null && dust.customData is Player)
								{
									Vector2 center = ((Player)dust.customData).Center;
									Vector2 vector = dust.position - center;
									float num13 = vector.Length();
									vector /= num13;
									dust.scale = Math.Min(dust.scale, num13 / 24f - 1f);
									dust.velocity -= vector * (100f / Math.Max(50f, num13));
								}
							}
							if (dust.type == 154 || dust.type == 218)
							{
								dust.rotation += dust.velocity.X * 0.3f;
								dust.scale -= 0.03f;
							}
							if (dust.type == 172)
							{
								float num14 = dust.scale * 0.5f;
								if (num14 > 1f)
								{
									num14 = 1f;
								}
								float num15 = num14;
								float num16 = num14;
								float num17 = num14;
								num15 *= 0f;
								num16 *= 0.25f;
								num17 *= 1f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num14 * num15, num14 * num16, num14 * num17);
							}
							if (dust.type == 182)
							{
								dust.rotation += 1f;
								if (!dust.noLight)
								{
									float num18 = dust.scale * 0.25f;
									if (num18 > 1f)
									{
										num18 = 1f;
									}
									float num19 = num18;
									float num20 = num18;
									float num21 = num18;
									num19 *= 1f;
									num20 *= 0.2f;
									num21 *= 0.1f;
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num18 * num19, num18 * num20, num18 * num21);
								}
								if (dust.customData != null && dust.customData is Player)
								{
									Player player3 = (Player)dust.customData;
									dust.position += player3.position - player3.oldPosition;
									dust.customData = null;
								}
							}
							if (dust.type == 261)
							{
								if (!dust.noLight && !dust.noLightEmittence)
								{
									float num22 = dust.scale * 0.3f;
									if (num22 > 1f)
									{
										num22 = 1f;
									}
									Lighting.AddLight(dust.position, new Vector3(0.4f, 0.6f, 0.7f) * num22);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								dust.velocity *= new Vector2(0.97f, 0.99f);
								dust.scale -= 0.0025f;
								if (dust.customData != null && dust.customData is Player)
								{
									Player player4 = (Player)dust.customData;
									dust.position += player4.position - player4.oldPosition;
								}
							}
							if (dust.type == 254)
							{
								float num23 = dust.scale * 0.35f;
								if (num23 > 1f)
								{
									num23 = 1f;
								}
								float num24 = num23;
								float num25 = num23;
								float num26 = num23;
								num24 *= 0.9f;
								num25 *= 0.1f;
								num26 *= 0.75f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num23 * num24, num23 * num25, num23 * num26);
							}
							if (dust.type == 255)
							{
								float num27 = dust.scale * 0.25f;
								if (num27 > 1f)
								{
									num27 = 1f;
								}
								float num28 = num27;
								float num29 = num27;
								float num30 = num27;
								num28 *= 0.9f;
								num29 *= 0.1f;
								num30 *= 0.75f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num27 * num28, num27 * num29, num27 * num30);
							}
							if (dust.type == 211 && dust.noLight && Collision.SolidCollision(dust.position, 4, 4))
							{
								dust.active = false;
							}
							if (dust.type == 284 && Collision.SolidCollision(dust.position - Vector2.One * 4f, 8, 8) && dust.fadeIn == 0f)
							{
								dust.velocity *= 0.25f;
							}
							if (dust.type == 213 || dust.type == 260)
							{
								dust.rotation = 0f;
								float num31 = dust.scale / 2.5f * 0.2f;
								Vector3 vector2 = Vector3.Zero;
								int num113 = dust.type;
								if (num113 != 213)
								{
									if (num113 == 260)
									{
										vector2..ctor(255f, 48f, 48f);
									}
								}
								else
								{
									vector2..ctor(255f, 217f, 48f);
								}
								vector2 /= 255f;
								if (num31 > 1f)
								{
									num31 = 1f;
								}
								vector2 *= num31;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), vector2.X, vector2.Y, vector2.Z);
							}
							if (dust.type == 157)
							{
								float num32 = dust.scale * 0.2f;
								float num33 = num32;
								float num34 = num32;
								float num35 = num32;
								num33 *= 0.25f;
								num34 *= 1f;
								num35 *= 0.5f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num32 * num33, num32 * num34, num32 * num35);
							}
							if (dust.type == 206)
							{
								dust.scale -= 0.1f;
								float num36 = dust.scale * 0.4f;
								float num37 = num36;
								float num38 = num36;
								float num39 = num36;
								num37 *= 0.1f;
								num38 *= 0.6f;
								num39 *= 1f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num36 * num37, num36 * num38, num36 * num39);
							}
							if (dust.type == 163)
							{
								float num40 = dust.scale * 0.25f;
								float num41 = num40;
								float num42 = num40;
								float num43 = num40;
								num41 *= 0.25f;
								num42 *= 1f;
								num43 *= 0.05f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num40 * num41, num40 * num42, num40 * num43);
							}
							if (dust.type == 205)
							{
								float num44 = dust.scale * 0.25f;
								float num45 = num44;
								float num46 = num44;
								float num47 = num44;
								num45 *= 1f;
								num46 *= 0.05f;
								num47 *= 1f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num44 * num45, num44 * num46, num44 * num47);
							}
							if (dust.type == 170)
							{
								float num48 = dust.scale * 0.5f;
								float num49 = num48;
								float num50 = num48;
								float num51 = num48;
								num49 *= 1f;
								num50 *= 1f;
								num51 *= 0.05f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num48 * num49, num48 * num50, num48 * num51);
							}
							if (dust.type == 156)
							{
								float num52 = dust.scale * 0.6f;
								int num114 = dust.type;
								float num53 = num52;
								num53 *= 1f;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 12, num52);
							}
							if (dust.type == 234)
							{
								float lightAmount = dust.scale * 0.6f;
								int num115 = dust.type;
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 13, lightAmount);
							}
							if (dust.type == 175)
							{
								dust.scale -= 0.05f;
							}
							if (dust.type == 174)
							{
								dust.scale -= 0.01f;
								float num54 = dust.scale * 1f;
								if (num54 > 0.6f)
								{
									num54 = 0.6f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num54, num54 * 0.4f, 0f);
							}
							if (dust.type == 235)
							{
								Vector2 vector3;
								vector3..ctor((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
								vector3.Normalize();
								vector3 *= 15f;
								dust.scale -= 0.01f;
							}
							else if (dust.type == 228 || dust.type == 279 || dust.type == 229 || dust.type == 6 || dust.type == 242 || dust.type == 135 || dust.type == 127 || dust.type == 187 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158 || dust.type == 293 || dust.type == 294 || dust.type == 295 || dust.type == 296 || dust.type == 297 || dust.type == 298 || dust.type == 302 || dust.type == 307 || dust.type == 310)
							{
								if (!dust.noGravity)
								{
									Dust dust6 = dust;
									dust6.velocity.Y = dust6.velocity.Y + 0.05f;
								}
								if (dust.type == 229 || dust.type == 228 || dust.type == 279)
								{
									if (dust.customData != null && dust.customData is NPC)
									{
										NPC nPC = (NPC)dust.customData;
										dust.position += nPC.position - nPC.oldPos[1];
									}
									else if (dust.customData != null && dust.customData is Player)
									{
										Player player5 = (Player)dust.customData;
										dust.position += player5.position - player5.oldPosition;
									}
									else if (dust.customData != null && dust.customData is Vector2)
									{
										Vector2 vector4 = (Vector2)dust.customData - dust.position;
										if (vector4 != Vector2.Zero)
										{
											vector4.Normalize();
										}
										dust.velocity = (dust.velocity * 4f + vector4 * dust.velocity.Length()) / 5f;
									}
								}
								if (!dust.noLight && !dust.noLightEmittence)
								{
									float num55 = dust.scale * 1.4f;
									if (dust.type == 29)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55 * 0.1f, num55 * 0.4f, num55);
									}
									else if (dust.type == 75)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										if (dust.customData is float)
										{
											Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 8, num55 * (float)dust.customData);
										}
										else
										{
											Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 8, num55);
										}
									}
									else if (dust.type == 169)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 11, num55);
									}
									else if (dust.type == 135)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 9, num55);
									}
									else if (dust.type == 158)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 10, num55);
									}
									else if (dust.type == 228)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55 * 0.7f, num55 * 0.65f, num55 * 0.3f);
									}
									else if (dust.type == 229)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55 * 0.3f, num55 * 0.65f, num55 * 0.7f);
									}
									else if (dust.type == 242)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 15, num55);
									}
									else if (dust.type == 293)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										num55 *= 0.95f;
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 16, num55);
									}
									else if (dust.type == 294)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 17, num55);
									}
									else if (dust.type >= 59 && dust.type <= 65)
									{
										if (num55 > 0.8f)
										{
											num55 = 0.8f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1 + dust.type - 59, num55);
									}
									else if (dust.type == 127)
									{
										num55 *= 1.3f;
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55, num55 * 0.45f, num55 * 0.2f);
									}
									else if (dust.type == 187)
									{
										num55 *= 1.3f;
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55 * 0.2f, num55 * 0.45f, num55);
									}
									else if (dust.type == 295)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 18, num55);
									}
									else if (dust.type == 296)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 19, num55);
									}
									else if (dust.type == 297)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 20, num55);
									}
									else if (dust.type == 298)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 21, num55);
									}
									else if (dust.type == 307)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 22, num55);
									}
									else if (dust.type == 310)
									{
										if (num55 > 1f)
										{
											num55 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 23, num55);
									}
									else
									{
										if (num55 > 0.6f)
										{
											num55 = 0.6f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num55, num55 * 0.65f, num55 * 0.4f);
									}
								}
							}
							else if (dust.type == 306)
							{
								if (!dust.noGravity)
								{
									Dust dust7 = dust;
									dust7.velocity.Y = dust7.velocity.Y + 0.05f;
								}
								dust.scale -= 0.04f;
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
							}
							else if (dust.type == 269)
							{
								if (!dust.noLight)
								{
									float num56 = dust.scale * 1.4f;
									if (num56 > 1f)
									{
										num56 = 1f;
									}
									Vector3 rgb3 = new Vector3(0.7f, 0.65f, 0.3f) * num56;
									Lighting.AddLight(dust.position, rgb3);
								}
								if (dust.customData != null && dust.customData is Vector2)
								{
									Vector2 vector5 = (Vector2)dust.customData - dust.position;
									Dust dust8 = dust;
									dust8.velocity.X = dust8.velocity.X + 1f * (float)Math.Sign(vector5.X) * dust.scale;
								}
							}
							else if (dust.type == 159)
							{
								float num57 = dust.scale * 1.3f;
								if (num57 > 1f)
								{
									num57 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num57, num57, num57 * 0.1f);
								if (dust.noGravity)
								{
									if (dust.scale < 0.7f)
									{
										dust.velocity *= 1.075f;
									}
									else if (Main.rand.Next(2) == 0)
									{
										dust.velocity *= -0.95f;
									}
									else
									{
										dust.velocity *= 1.05f;
									}
									dust.scale -= 0.03f;
								}
								else
								{
									dust.scale += 0.005f;
									dust.velocity *= 0.9f;
									Dust dust9 = dust;
									dust9.velocity.X = dust9.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
									Dust dust10 = dust;
									dust10.velocity.Y = dust10.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
									if (Main.rand.Next(5) == 0)
									{
										int num58 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
										Main.dust[num58].noGravity = true;
										Main.dust[num58].scale = dust.scale * 2.5f;
									}
								}
							}
							else if (dust.type == 164)
							{
								float num59 = dust.scale;
								if (num59 > 1f)
								{
									num59 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num59, num59 * 0.1f, num59 * 0.8f);
								if (dust.noGravity)
								{
									if (dust.scale < 0.7f)
									{
										dust.velocity *= 1.075f;
									}
									else if (Main.rand.Next(2) == 0)
									{
										dust.velocity *= -0.95f;
									}
									else
									{
										dust.velocity *= 1.05f;
									}
									dust.scale -= 0.03f;
								}
								else
								{
									dust.scale -= 0.005f;
									dust.velocity *= 0.9f;
									Dust dust11 = dust;
									dust11.velocity.X = dust11.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
									Dust dust12 = dust;
									dust12.velocity.Y = dust12.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
									if (Main.rand.Next(5) == 0)
									{
										int num60 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
										Main.dust[num60].noGravity = true;
										Main.dust[num60].scale = dust.scale * 2.5f;
									}
								}
							}
							else if (dust.type == 173)
							{
								float num61 = dust.scale;
								if (num61 > 1f)
								{
									num61 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.4f, num61 * 0.1f, num61);
								if (dust.noGravity)
								{
									dust.velocity *= 0.8f;
									Dust dust13 = dust;
									dust13.velocity.X = dust13.velocity.X + (float)Main.rand.Next(-20, 21) * 0.01f;
									Dust dust14 = dust;
									dust14.velocity.Y = dust14.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.01f;
									dust.scale -= 0.01f;
								}
								else
								{
									dust.scale -= 0.015f;
									dust.velocity *= 0.8f;
									Dust dust15 = dust;
									dust15.velocity.X = dust15.velocity.X + (float)Main.rand.Next(-10, 11) * 0.005f;
									Dust dust16 = dust;
									dust16.velocity.Y = dust16.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.005f;
									if (Main.rand.Next(10) == 10)
									{
										int num62 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
										Main.dust[num62].noGravity = true;
										Main.dust[num62].scale = dust.scale;
									}
								}
							}
							else if (dust.type == 304)
							{
								dust.velocity.Y = (float)Math.Sin((double)dust.rotation) / 5f;
								dust.rotation += 0.015f;
								if (dust.scale < 1.15f)
								{
									dust.alpha = Math.Max(0, dust.alpha - 20);
									dust.scale += 0.0015f;
								}
								else
								{
									dust.alpha += 6;
									if (dust.alpha >= 255)
									{
										dust.active = false;
									}
								}
								if (dust.customData != null && dust.customData is Player)
								{
									Player player6 = (Player)dust.customData;
									float num63 = Utils.Remap(dust.scale, 1f, 1.05f, 1f, 0f, true);
									if (num63 > 0f)
									{
										dust.position += player6.velocity * num63;
									}
									float num64 = player6.Center.X - dust.position.X;
									if (Math.Abs(num64) > 20f)
									{
										float value = num64 * 0.01f;
										dust.velocity.X = MathHelper.Lerp(dust.velocity.X, value, num63 * 0.2f);
									}
								}
							}
							else if (dust.type == 184)
							{
								if (!dust.noGravity)
								{
									dust.velocity *= 0f;
									dust.scale -= 0.01f;
								}
							}
							else if (dust.type == 160 || dust.type == 162)
							{
								float num65 = dust.scale * 1.3f;
								if (num65 > 1f)
								{
									num65 = 1f;
								}
								if (dust.type == 162)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num65, num65 * 0.7f, num65 * 0.1f);
								}
								else
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num65 * 0.1f, num65, num65);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.8f;
									Dust dust17 = dust;
									dust17.velocity.X = dust17.velocity.X + (float)Main.rand.Next(-20, 21) * 0.04f;
									Dust dust18 = dust;
									dust18.velocity.Y = dust18.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.04f;
									dust.scale -= 0.1f;
								}
								else
								{
									dust.scale -= 0.1f;
									Dust dust19 = dust;
									dust19.velocity.X = dust19.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
									Dust dust20 = dust;
									dust20.velocity.Y = dust20.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
									if ((double)dust.scale > 0.3 && Main.rand.Next(50) == 0)
									{
										int num66 = Dust.NewDust(new Vector2(dust.position.X - 4f, dust.position.Y - 4f), 1, 1, dust.type, 0f, 0f, 0, default(Color), 1f);
										Main.dust[num66].noGravity = true;
										Main.dust[num66].scale = dust.scale * 1.5f;
									}
								}
							}
							else if (dust.type == 168)
							{
								float num67 = dust.scale * 0.8f;
								if ((double)num67 > 0.55)
								{
									num67 = 0.55f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num67, 0f, num67 * 0.8f);
								dust.scale += 0.03f;
								Dust dust21 = dust;
								dust21.velocity.X = dust21.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
								Dust dust22 = dust;
								dust22.velocity.Y = dust22.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
								dust.velocity *= 0.99f;
							}
							else if (dust.type >= 139 && dust.type < 143)
							{
								Dust dust23 = dust;
								dust23.velocity.X = dust23.velocity.X * 0.98f;
								Dust dust24 = dust;
								dust24.velocity.Y = dust24.velocity.Y * 0.98f;
								if (dust.velocity.Y < 1f)
								{
									Dust dust25 = dust;
									dust25.velocity.Y = dust25.velocity.Y + 0.05f;
								}
								dust.scale += 0.009f;
								dust.rotation -= dust.velocity.X * 0.4f;
								if (dust.velocity.X > 0f)
								{
									dust.rotation += 0.005f;
								}
								else
								{
									dust.rotation -= 0.005f;
								}
							}
							else if (dust.type == 14 || dust.type == 16 || dust.type == 31 || dust.type == 46 || dust.type == 124 || dust.type == 186 || dust.type == 188 || dust.type == 303)
							{
								Dust dust26 = dust;
								dust26.velocity.Y = dust26.velocity.Y * 0.98f;
								Dust dust27 = dust;
								dust27.velocity.X = dust27.velocity.X * 0.98f;
								if (dust.type == 31)
								{
									if (dust.customData != null && dust.customData is float)
									{
										float num68 = (float)dust.customData;
										Dust dust28 = dust;
										dust28.velocity.Y = dust28.velocity.Y + num68;
									}
									if (dust.customData != null && dust.customData is NPC)
									{
										NPC nPC2 = (NPC)dust.customData;
										dust.position += nPC2.position - nPC2.oldPosition;
										if (dust.noGravity)
										{
											dust.velocity *= 1.02f;
										}
										dust.alpha -= 70;
										if (dust.alpha < 0)
										{
											dust.alpha = 0;
										}
										dust.scale *= 0.97f;
										if (dust.scale <= 0.01f)
										{
											dust.scale = 0.0001f;
											dust.alpha = 255;
										}
									}
									else if (dust.noGravity)
									{
										dust.velocity *= 1.02f;
										dust.scale += 0.02f;
										dust.alpha += 4;
										if (dust.alpha > 255)
										{
											dust.scale = 0.0001f;
											dust.alpha = 255;
										}
									}
								}
								if (dust.type == 303 && dust.noGravity)
								{
									dust.velocity *= 1.02f;
									dust.scale += 0.03f;
									if (dust.alpha < 90)
									{
										dust.alpha = 90;
									}
									dust.alpha += 4;
									if (dust.alpha > 255)
									{
										dust.scale = 0.0001f;
										dust.alpha = 255;
									}
								}
							}
							else if (dust.type == 32)
							{
								dust.scale -= 0.01f;
								Dust dust29 = dust;
								dust29.velocity.X = dust29.velocity.X * 0.96f;
								if (!dust.noGravity)
								{
									Dust dust30 = dust;
									dust30.velocity.Y = dust30.velocity.Y + 0.1f;
								}
							}
							else if (dust.type >= 244 && dust.type <= 247)
							{
								dust.rotation += 0.1f * dust.scale;
								Color color = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
								byte b2 = (color.R + color.G + color.B) / 3;
								float num69 = ((float)b2 / 270f + 1f) / 2f;
								float num70 = ((float)b2 / 270f + 1f) / 2f;
								float num71 = ((float)b2 / 270f + 1f) / 2f;
								num69 *= dust.scale * 0.9f;
								num70 *= dust.scale * 0.9f;
								num71 *= dust.scale * 0.9f;
								if (dust.alpha < 255)
								{
									dust.scale += 0.09f;
									if (dust.scale >= 1f)
									{
										dust.scale = 1f;
										dust.alpha = 255;
									}
								}
								else
								{
									if ((double)dust.scale < 0.8)
									{
										dust.scale -= 0.01f;
									}
									if ((double)dust.scale < 0.5)
									{
										dust.scale -= 0.01f;
									}
								}
								float num72 = 1f;
								if (dust.type == 244)
								{
									num69 *= 0.8862745f;
									num70 *= 0.4627451f;
									num71 *= 0.29803923f;
									num72 = 0.9f;
								}
								else if (dust.type == 245)
								{
									num69 *= 0.5137255f;
									num70 *= 0.6745098f;
									num71 *= 0.6784314f;
									num72 = 1f;
								}
								else if (dust.type == 246)
								{
									num69 *= 0.8f;
									num70 *= 0.70980394f;
									num71 *= 0.28235295f;
									num72 = 1.1f;
								}
								else if (dust.type == 247)
								{
									num69 *= 0.6f;
									num70 *= 0.6745098f;
									num71 *= 0.7254902f;
									num72 = 1.2f;
								}
								num69 *= num72;
								num70 *= num72;
								num71 *= num72;
								if (!dust.noLightEmittence)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num69, num70, num71);
								}
							}
							else if (dust.type == 43)
							{
								dust.rotation += 0.1f * dust.scale;
								Color color2 = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
								float num73 = (float)color2.R / 270f;
								float num74 = (float)color2.G / 270f;
								float num75 = (float)color2.B / 270f;
								float num76 = (float)dust.color.R / 255f;
								float num77 = (float)dust.color.G / 255f;
								float num78 = (float)dust.color.B / 255f;
								num73 *= dust.scale * 1.07f * num76;
								num74 *= dust.scale * 1.07f * num77;
								num75 *= dust.scale * 1.07f * num78;
								if (dust.alpha < 255)
								{
									dust.scale += 0.09f;
									if (dust.scale >= 1f)
									{
										dust.scale = 1f;
										dust.alpha = 255;
									}
								}
								else
								{
									if ((double)dust.scale < 0.8)
									{
										dust.scale -= 0.01f;
									}
									if ((double)dust.scale < 0.5)
									{
										dust.scale -= 0.01f;
									}
								}
								if ((double)num73 < 0.05 && (double)num74 < 0.05 && (double)num75 < 0.05)
								{
									dust.active = false;
								}
								else if (!dust.noLightEmittence)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num73, num74, num75);
								}
								if (dust.customData != null && dust.customData is Player)
								{
									Player player7 = (Player)dust.customData;
									dust.position += player7.position - player7.oldPosition;
								}
							}
							else if (dust.type == 15 || dust.type == 57 || dust.type == 58 || dust.type == 274 || dust.type == 292)
							{
								Dust dust31 = dust;
								dust31.velocity.Y = dust31.velocity.Y * 0.98f;
								Dust dust32 = dust;
								dust32.velocity.X = dust32.velocity.X * 0.98f;
								if (!dust.noLightEmittence)
								{
									float num79 = dust.scale;
									if (dust.type != 15)
									{
										num79 = dust.scale * 0.8f;
									}
									if (dust.noLight)
									{
										dust.velocity *= 0.95f;
									}
									if (num79 > 1f)
									{
										num79 = 1f;
									}
									if (dust.type == 15)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num79 * 0.45f, num79 * 0.55f, num79);
									}
									else if (dust.type == 57)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num79 * 0.95f, num79 * 0.95f, num79 * 0.45f);
									}
									else if (dust.type == 58)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num79, num79 * 0.55f, num79 * 0.75f);
									}
								}
							}
							else if (dust.type == 204)
							{
								if (dust.fadeIn > dust.scale)
								{
									dust.scale += 0.02f;
								}
								else
								{
									dust.scale -= 0.02f;
								}
								dust.velocity *= 0.95f;
							}
							else if (dust.type == 110)
							{
								float num80 = dust.scale * 0.1f;
								if (num80 > 1f)
								{
									num80 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num80 * 0.2f, num80, num80 * 0.5f);
							}
							else if (dust.type == 111)
							{
								float num81 = dust.scale * 0.125f;
								if (num81 > 1f)
								{
									num81 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num81 * 0.2f, num81 * 0.7f, num81);
							}
							else if (dust.type == 112)
							{
								float num82 = dust.scale * 0.1f;
								if (num82 > 1f)
								{
									num82 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num82 * 0.8f, num82 * 0.2f, num82 * 0.8f);
							}
							else if (dust.type == 113)
							{
								float num83 = dust.scale * 0.1f;
								if (num83 > 1f)
								{
									num83 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num83 * 0.2f, num83 * 0.3f, num83 * 1.3f);
							}
							else if (dust.type == 114)
							{
								float num84 = dust.scale * 0.1f;
								if (num84 > 1f)
								{
									num84 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num84 * 1.2f, num84 * 0.5f, num84 * 0.4f);
							}
							else if (dust.type == 311)
							{
								float num85 = dust.scale * 0.1f;
								if (num85 > 1f)
								{
									num85 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 16, num85);
							}
							else if (dust.type == 312)
							{
								float num86 = dust.scale * 0.1f;
								if (num86 > 1f)
								{
									num86 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 9, num86);
							}
							else if (dust.type == 313)
							{
								float num87 = dust.scale * 0.25f;
								if (num87 > 1f)
								{
									num87 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num87 * 1f, num87 * 0.8f, num87 * 0.6f);
							}
							else if (dust.type == 66)
							{
								if (dust.velocity.X < 0f)
								{
									dust.rotation -= 1f;
								}
								else
								{
									dust.rotation += 1f;
								}
								Dust dust33 = dust;
								dust33.velocity.Y = dust33.velocity.Y * 0.98f;
								Dust dust34 = dust;
								dust34.velocity.X = dust34.velocity.X * 0.98f;
								dust.scale += 0.02f;
								float num88 = dust.scale;
								if (dust.type != 15)
								{
									num88 = dust.scale * 0.8f;
								}
								if (num88 > 1f)
								{
									num88 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num88 * ((float)dust.color.R / 255f), num88 * ((float)dust.color.G / 255f), num88 * ((float)dust.color.B / 255f));
							}
							else if (dust.type == 267)
							{
								if (dust.velocity.X < 0f)
								{
									dust.rotation -= 1f;
								}
								else
								{
									dust.rotation += 1f;
								}
								Dust dust35 = dust;
								dust35.velocity.Y = dust35.velocity.Y * 0.98f;
								Dust dust36 = dust;
								dust36.velocity.X = dust36.velocity.X * 0.98f;
								dust.scale += 0.02f;
								float num89 = dust.scale * 0.8f;
								if (num89 > 1f)
								{
									num89 = 1f;
								}
								if (dust.noLight)
								{
									dust.noLight = false;
								}
								if (!dust.noLight && !dust.noLightEmittence)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num89 * ((float)dust.color.R / 255f), num89 * ((float)dust.color.G / 255f), num89 * ((float)dust.color.B / 255f));
								}
							}
							else if (dust.type == 20 || dust.type == 21 || dust.type == 231)
							{
								dust.scale += 0.005f;
								Dust dust37 = dust;
								dust37.velocity.Y = dust37.velocity.Y * 0.94f;
								Dust dust38 = dust;
								dust38.velocity.X = dust38.velocity.X * 0.94f;
								float num90 = dust.scale * 0.8f;
								if (num90 > 1f)
								{
									num90 = 1f;
								}
								if (dust.type == 21 && !dust.noLightEmittence)
								{
									num90 = dust.scale * 0.4f;
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num90 * 0.8f, num90 * 0.3f, num90);
								}
								else if (dust.type == 231)
								{
									num90 = dust.scale * 0.4f;
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num90, num90 * 0.5f, num90 * 0.3f);
								}
								else
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num90 * 0.3f, num90 * 0.6f, num90);
								}
							}
							else if (dust.type == 27 || dust.type == 45)
							{
								if (dust.type == 27 && dust.fadeIn >= 100f)
								{
									if ((double)dust.scale >= 1.5)
									{
										dust.scale -= 0.01f;
									}
									else
									{
										dust.scale -= 0.05f;
									}
									if ((double)dust.scale <= 0.5)
									{
										dust.scale -= 0.05f;
									}
									if ((double)dust.scale <= 0.25)
									{
										dust.scale -= 0.05f;
									}
								}
								dust.velocity *= 0.94f;
								dust.scale += 0.002f;
								float num91 = dust.scale;
								if (dust.noLight)
								{
									num91 *= 0.1f;
									dust.scale -= 0.06f;
									if (dust.scale < 1f)
									{
										dust.scale -= 0.06f;
									}
									if (Main.player[Main.myPlayer].wet)
									{
										dust.position += Main.player[Main.myPlayer].velocity * 0.5f;
									}
									else
									{
										dust.position += Main.player[Main.myPlayer].velocity;
									}
								}
								if (num91 > 1f)
								{
									num91 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num91 * 0.6f, num91 * 0.2f, num91);
							}
							else if (dust.type == 55 || dust.type == 56 || dust.type == 73 || dust.type == 74)
							{
								dust.velocity *= 0.98f;
								if (!dust.noLightEmittence)
								{
									float num92 = dust.scale * 0.8f;
									if (dust.type == 55)
									{
										if (num92 > 1f)
										{
											num92 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92, num92, num92 * 0.6f);
									}
									else if (dust.type == 73)
									{
										if (num92 > 1f)
										{
											num92 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92, num92 * 0.35f, num92 * 0.5f);
									}
									else if (dust.type == 74)
									{
										if (num92 > 1f)
										{
											num92 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92 * 0.35f, num92, num92 * 0.5f);
									}
									else
									{
										num92 = dust.scale * 1.2f;
										if (num92 > 1f)
										{
											num92 = 1f;
										}
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92 * 0.35f, num92 * 0.5f, num92);
									}
								}
							}
							else if (dust.type == 71 || dust.type == 72)
							{
								dust.velocity *= 0.98f;
								float num93 = dust.scale;
								if (num93 > 1f)
								{
									num93 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num93 * 0.2f, 0f, num93 * 0.1f);
							}
							else if (dust.type == 76)
							{
								Main.snowDust++;
								dust.scale += 0.009f;
								float y = Main.player[Main.myPlayer].velocity.Y;
								if (y > 0f && dust.fadeIn == 0f && dust.velocity.Y < y)
								{
									dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y, 0.04f);
								}
								if (!dust.noLight && y > 0f)
								{
									Dust dust39 = dust;
									dust39.position.Y = dust39.position.Y + Main.player[Main.myPlayer].velocity.Y * 0.2f;
								}
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
							}
							else if (dust.type == 270)
							{
								dust.velocity *= 1.0050251f;
								dust.scale += 0.01f;
								dust.rotation = 0f;
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.95f;
									dust.velocity *= 0.25f;
								}
								else
								{
									dust.velocity.Y = (float)Math.Sin((double)(dust.position.X * 0.0043982295f)) * 2f;
									Dust dust40 = dust;
									dust40.velocity.Y = dust40.velocity.Y - 3f;
									Dust dust41 = dust;
									dust41.velocity.Y = dust41.velocity.Y / 20f;
								}
							}
							else if (dust.type == 271)
							{
								dust.velocity *= 1.0050251f;
								dust.scale += 0.003f;
								dust.rotation = 0f;
								Dust dust42 = dust;
								dust42.velocity.Y = dust42.velocity.Y - 4f;
								Dust dust43 = dust;
								dust43.velocity.Y = dust43.velocity.Y / 6f;
							}
							else if (dust.type == 268)
							{
								Dust.SandStormCount++;
								dust.velocity *= 1.0050251f;
								dust.scale += 0.01f;
								if (!flag)
								{
									dust.scale -= 0.05f;
								}
								dust.rotation = 0f;
								float y2 = Main.player[Main.myPlayer].velocity.Y;
								if (y2 > 0f && dust.fadeIn == 0f && dust.velocity.Y < y2)
								{
									dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y2, 0.04f);
								}
								if (!dust.noLight && y2 > 0f)
								{
									Dust dust44 = dust;
									dust44.position.Y = dust44.position.Y + y2 * 0.2f;
								}
								if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
								else
								{
									dust.velocity.Y = (float)Math.Sin((double)(dust.position.X * 0.0043982295f)) * 2f;
									Dust dust45 = dust;
									dust45.velocity.Y = dust45.velocity.Y + 3f;
								}
							}
							else if (!dust.noGravity && dust.type != 41 && dust.type != 44 && dust.type != 309)
							{
								if (dust.type == 107)
								{
									dust.velocity *= 0.9f;
								}
								else
								{
									Dust dust46 = dust;
									dust46.velocity.Y = dust46.velocity.Y + 0.1f;
								}
							}
							if (dust.type == 5 || (dust.type == 273 && dust.noGravity))
							{
								dust.scale -= 0.04f;
							}
							if (dust.type == 308 || dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105 || dust.type == 123 || dust.type == 288)
							{
								if (dust.velocity.X == 0f)
								{
									if (Collision.SolidCollision(dust.position, 2, 2))
									{
										dust.scale = 0f;
									}
									dust.rotation += 0.5f;
									dust.scale -= 0.01f;
								}
								if (Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y), 4, 4))
								{
									dust.alpha += 20;
									dust.scale -= 0.1f;
								}
								dust.alpha += 2;
								dust.scale -= 0.005f;
								if (dust.alpha > 255)
								{
									dust.scale = 0f;
								}
								if (dust.velocity.Y > 4f)
								{
									dust.velocity.Y = 4f;
								}
								if (dust.noGravity)
								{
									if (dust.velocity.X < 0f)
									{
										dust.rotation -= 0.2f;
									}
									else
									{
										dust.rotation += 0.2f;
									}
									dust.scale += 0.03f;
									Dust dust47 = dust;
									dust47.velocity.X = dust47.velocity.X * 1.05f;
									Dust dust48 = dust;
									dust48.velocity.Y = dust48.velocity.Y + 0.15f;
								}
							}
							if (dust.type == 35 && dust.noGravity)
							{
								dust.scale += 0.03f;
								if (dust.scale < 1f)
								{
									Dust dust49 = dust;
									dust49.velocity.Y = dust49.velocity.Y + 0.075f;
								}
								Dust dust50 = dust;
								dust50.velocity.X = dust50.velocity.X * 1.08f;
								if (dust.velocity.X > 0f)
								{
									dust.rotation += 0.01f;
								}
								else
								{
									dust.rotation -= 0.01f;
								}
								float num94 = dust.scale * 0.6f;
								if (num94 > 1f)
								{
									num94 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f + 1f), num94, num94 * 0.3f, num94 * 0.1f);
							}
							else if (dust.type == 152 && dust.noGravity)
							{
								dust.scale += 0.03f;
								if (dust.scale < 1f)
								{
									Dust dust51 = dust;
									dust51.velocity.Y = dust51.velocity.Y + 0.075f;
								}
								Dust dust52 = dust;
								dust52.velocity.X = dust52.velocity.X * 1.08f;
								if (dust.velocity.X > 0f)
								{
									dust.rotation += 0.01f;
								}
								else
								{
									dust.rotation -= 0.01f;
								}
							}
							else if (dust.type == 67 || dust.type == 92)
							{
								float num95 = dust.scale;
								if (num95 > 1f)
								{
									num95 = 1f;
								}
								if (dust.noLight)
								{
									num95 *= 0.1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, num95 * 0.8f, num95);
							}
							else if (dust.type == 185)
							{
								float num96 = dust.scale;
								if (num96 > 1f)
								{
									num96 = 1f;
								}
								if (dust.noLight)
								{
									num96 *= 0.1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96 * 0.1f, num96 * 0.7f, num96);
							}
							else if (dust.type == 107)
							{
								float num97 = dust.scale * 0.5f;
								if (num97 > 1f)
								{
									num97 = 1f;
								}
								if (!dust.noLightEmittence)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num97 * 0.1f, num97, num97 * 0.4f);
								}
							}
							else if (dust.type == 34 || dust.type == 35 || dust.type == 152)
							{
								if (!Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
								{
									dust.scale = 0f;
								}
								else
								{
									dust.alpha += Main.rand.Next(2);
									if (dust.alpha > 255)
									{
										dust.scale = 0f;
									}
									dust.velocity.Y = -0.5f;
									if (dust.type == 34)
									{
										dust.scale += 0.005f;
									}
									else
									{
										dust.alpha++;
										dust.scale -= 0.01f;
										dust.velocity.Y = -0.2f;
									}
									Dust dust53 = dust;
									dust53.velocity.X = dust53.velocity.X + (float)Main.rand.Next(-10, 10) * 0.002f;
									if ((double)dust.velocity.X < -0.25)
									{
										dust.velocity.X = -0.25f;
									}
									if ((double)dust.velocity.X > 0.25)
									{
										dust.velocity.X = 0.25f;
									}
								}
								if (dust.type == 35)
								{
									float num98 = dust.scale * 0.3f + 0.4f;
									if (num98 > 1f)
									{
										num98 = 1f;
									}
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num98, num98 * 0.5f, num98 * 0.3f);
								}
							}
							if (dust.type == 68)
							{
								float num99 = dust.scale * 0.3f;
								if (num99 > 1f)
								{
									num99 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num99 * 0.1f, num99 * 0.2f, num99);
							}
							if (dust.type == 70)
							{
								float num100 = dust.scale * 0.3f;
								if (num100 > 1f)
								{
									num100 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num100 * 0.5f, 0f, num100);
							}
							if (dust.type == 41)
							{
								Dust dust54 = dust;
								dust54.velocity.X = dust54.velocity.X + (float)Main.rand.Next(-10, 11) * 0.01f;
								Dust dust55 = dust;
								dust55.velocity.Y = dust55.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.01f;
								if ((double)dust.velocity.X > 0.75)
								{
									dust.velocity.X = 0.75f;
								}
								if ((double)dust.velocity.X < -0.75)
								{
									dust.velocity.X = -0.75f;
								}
								if ((double)dust.velocity.Y > 0.75)
								{
									dust.velocity.Y = 0.75f;
								}
								if ((double)dust.velocity.Y < -0.75)
								{
									dust.velocity.Y = -0.75f;
								}
								dust.scale += 0.007f;
								float num101 = dust.scale * 0.7f;
								if (num101 > 1f)
								{
									num101 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num101 * 0.4f, num101 * 0.9f, num101);
							}
							else if (dust.type == 44)
							{
								Dust dust56 = dust;
								dust56.velocity.X = dust56.velocity.X + (float)Main.rand.Next(-10, 11) * 0.003f;
								Dust dust57 = dust;
								dust57.velocity.Y = dust57.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.003f;
								if ((double)dust.velocity.X > 0.35)
								{
									dust.velocity.X = 0.35f;
								}
								if ((double)dust.velocity.X < -0.35)
								{
									dust.velocity.X = -0.35f;
								}
								if ((double)dust.velocity.Y > 0.35)
								{
									dust.velocity.Y = 0.35f;
								}
								if ((double)dust.velocity.Y < -0.35)
								{
									dust.velocity.Y = -0.35f;
								}
								dust.scale += 0.0085f;
								float num102 = dust.scale * 0.7f;
								if (num102 > 1f)
								{
									num102 = 1f;
								}
								Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num102 * 0.7f, num102, num102 * 0.8f);
							}
							else if (dust.type != 304 && (modDust == null || !modDust.MidUpdate(dust)))
							{
								Dust dust58 = dust;
								dust58.velocity.X = dust58.velocity.X * 0.99f;
							}
							if (dust.type == 322 && !dust.noGravity)
							{
								dust.scale *= 0.98f;
							}
							if (dust.type != 79 && dust.type != 268 && dust.type != 304)
							{
								dust.rotation += dust.velocity.X * 0.5f;
							}
							if (dust.fadeIn > 0f && dust.fadeIn < 100f)
							{
								if (dust.type == 235)
								{
									dust.scale += 0.007f;
									int num103 = (int)dust.fadeIn - 1;
									if (num103 >= 0 && num103 <= 255)
									{
										Vector2 vector6 = dust.position - Main.player[num103].Center;
										float num104 = vector6.Length();
										num104 = 100f - num104;
										if (num104 > 0f)
										{
											dust.scale -= num104 * 0.0015f;
										}
										vector6.Normalize();
										float num105 = (1f - dust.scale) * 20f;
										vector6 *= 0f - num105;
										dust.velocity = (dust.velocity * 4f + vector6) / 5f;
									}
								}
								else if (dust.type == 46)
								{
									dust.scale += 0.1f;
								}
								else if (dust.type == 213 || dust.type == 260)
								{
									dust.scale += 0.1f;
								}
								else
								{
									dust.scale += 0.03f;
								}
								if (dust.scale > dust.fadeIn)
								{
									dust.fadeIn = 0f;
								}
							}
							else if (dust.type != 304)
							{
								if (dust.type == 213 || dust.type == 260)
								{
									dust.scale -= 0.2f;
								}
								else
								{
									dust.scale -= 0.01f;
								}
							}
							if (dust.type >= 130 && dust.type <= 134)
							{
								float num106 = dust.scale;
								if (num106 > 1f)
								{
									num106 = 1f;
								}
								if (dust.type == 130)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num106 * 1f, num106 * 0.5f, num106 * 0.4f);
								}
								if (dust.type == 131)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num106 * 0.4f, num106 * 1f, num106 * 0.6f);
								}
								if (dust.type == 132)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num106 * 0.3f, num106 * 0.5f, num106 * 1f);
								}
								if (dust.type == 133)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num106 * 0.9f, num106 * 0.9f, num106 * 0.3f);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								else if (dust.type == 131)
								{
									dust.velocity *= 0.98f;
									Dust dust59 = dust;
									dust59.velocity.Y = dust59.velocity.Y - 0.1f;
									dust.scale += 0.0025f;
								}
								else
								{
									dust.velocity *= 0.95f;
									dust.scale -= 0.0025f;
								}
							}
							else if (dust.type == 278)
							{
								float num107 = dust.scale;
								if (num107 > 1f)
								{
									num107 = 1f;
								}
								if (!dust.noLight && !dust.noLightEmittence)
								{
									Lighting.AddLight(dust.position, dust.color.ToVector3() * num107);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								else
								{
									dust.velocity *= 0.95f;
									dust.scale -= 0.0025f;
								}
								if (WorldGen.SolidTile(Framing.GetTileSafely(dust.position)) && dust.fadeIn == 0f && !dust.noGravity)
								{
									dust.scale *= 0.9f;
									dust.velocity *= 0.25f;
								}
							}
							else if (dust.type >= 219 && dust.type <= 223)
							{
								float num108 = dust.scale;
								if (num108 > 1f)
								{
									num108 = 1f;
								}
								if (!dust.noLight)
								{
									if (dust.type == 219)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num108 * 1f, num108 * 0.5f, num108 * 0.4f);
									}
									if (dust.type == 220)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num108 * 0.4f, num108 * 1f, num108 * 0.6f);
									}
									if (dust.type == 221)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num108 * 0.3f, num108 * 0.5f, num108 * 1f);
									}
									if (dust.type == 222)
									{
										Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num108 * 0.9f, num108 * 0.9f, num108 * 0.3f);
									}
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								dust.velocity *= new Vector2(0.97f, 0.99f);
								dust.scale -= 0.0025f;
								if (dust.customData != null && dust.customData is Player)
								{
									Player player8 = (Player)dust.customData;
									dust.position += player8.position - player8.oldPosition;
								}
							}
							else if (dust.type == 226)
							{
								float num109 = dust.scale;
								if (num109 > 1f)
								{
									num109 = 1f;
								}
								if (!dust.noLight)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num109 * 0.2f, num109 * 0.7f, num109 * 1f);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								dust.velocity *= new Vector2(0.97f, 0.99f);
								if (dust.customData != null && dust.customData is Player)
								{
									Player player9 = (Player)dust.customData;
									dust.position += player9.position - player9.oldPosition;
								}
								dust.scale -= 0.01f;
							}
							else if (dust.type == 272)
							{
								float num110 = dust.scale;
								if (num110 > 1f)
								{
									num110 = 1f;
								}
								if (!dust.noLight)
								{
									Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110 * 0.5f, num110 * 0.2f, num110 * 0.8f);
								}
								if (dust.noGravity)
								{
									dust.velocity *= 0.93f;
									if (dust.fadeIn == 0f)
									{
										dust.scale += 0.0025f;
									}
								}
								dust.velocity *= new Vector2(0.97f, 0.99f);
								if (dust.customData != null && dust.customData is Player)
								{
									Player player10 = (Player)dust.customData;
									dust.position += player10.position - player10.oldPosition;
								}
								if (dust.customData != null && dust.customData is NPC)
								{
									NPC nPC3 = (NPC)dust.customData;
									dust.position += nPC3.position - nPC3.oldPosition;
								}
								dust.scale -= 0.01f;
							}
							else if (dust.type != 304 && dust.noGravity)
							{
								dust.velocity *= 0.92f;
								if (dust.fadeIn == 0f)
								{
									dust.scale -= 0.04f;
								}
							}
							if (dust.position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
							{
								dust.active = false;
							}
							float num111 = 0.1f;
							if ((double)Dust.dCount == 0.5)
							{
								dust.scale -= 0.001f;
							}
							if ((double)Dust.dCount == 0.6)
							{
								dust.scale -= 0.0025f;
							}
							if ((double)Dust.dCount == 0.7)
							{
								dust.scale -= 0.005f;
							}
							if ((double)Dust.dCount == 0.8)
							{
								dust.scale -= 0.01f;
							}
							if ((double)Dust.dCount == 0.9)
							{
								dust.scale -= 0.02f;
							}
							if ((double)Dust.dCount == 0.5)
							{
								num111 = 0.11f;
							}
							if ((double)Dust.dCount == 0.6)
							{
								num111 = 0.13f;
							}
							if ((double)Dust.dCount == 0.7)
							{
								num111 = 0.16f;
							}
							if ((double)Dust.dCount == 0.8)
							{
								num111 = 0.22f;
							}
							if ((double)Dust.dCount == 0.9)
							{
								num111 = 0.25f;
							}
							if (dust.scale < num111)
							{
								dust.active = false;
							}
							DustLoader.TakeDownUpdateType(dust);
						}
					}
				}
				else
				{
					dust.active = false;
				}
			}
			int num112 = num;
			if ((double)num112 > (double)Main.maxDustToDraw * 0.9)
			{
				Dust.dCount = 0.9f;
				return;
			}
			if ((double)num112 > (double)Main.maxDustToDraw * 0.8)
			{
				Dust.dCount = 0.8f;
				return;
			}
			if ((double)num112 > (double)Main.maxDustToDraw * 0.7)
			{
				Dust.dCount = 0.7f;
				return;
			}
			if ((double)num112 > (double)Main.maxDustToDraw * 0.6)
			{
				Dust.dCount = 0.6f;
				return;
			}
			if ((double)num112 > (double)Main.maxDustToDraw * 0.5)
			{
				Dust.dCount = 0.5f;
				return;
			}
			Dust.dCount = 0f;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000207BC File Offset: 0x0001E9BC
		public Color GetAlpha(Color newColor)
		{
			ModDust modDust = DustLoader.GetDust(this.type);
			if (modDust != null)
			{
				Color? modColor = modDust.GetAlpha(this, newColor);
				if (modColor != null)
				{
					return modColor.Value;
				}
			}
			float num = (float)(255 - this.alpha) / 255f;
			int num7 = this.type;
			switch (num7)
			{
			case 299:
			case 300:
			case 301:
			case 305:
			{
				Color color = default(Color);
				switch (this.type)
				{
				case 299:
					color..ctor(50, 255, 50, 200);
					break;
				case 300:
					color..ctor(50, 200, 255, 255);
					break;
				case 301:
					color..ctor(255, 50, 125, 200);
					break;
				default:
					color..ctor(255, 150, 150, 200);
					break;
				case 305:
					color..ctor(200, 50, 200, 200);
					break;
				}
				return color;
			}
			case 302:
			case 303:
			case 304:
			case 306:
			case 307:
				break;
			case 308:
			case 309:
				return new Color(225, 200, 250, 190);
			default:
				if (num7 == 323)
				{
					return Color.White;
				}
				break;
			}
			if (this.type == 304)
			{
				return Color.White * num;
			}
			if (this.type == 306)
			{
				return this.color * num;
			}
			if (this.type == 292)
			{
				return Color.White;
			}
			if (this.type == 259)
			{
				return new Color(230, 230, 230, 230);
			}
			if (this.type == 261)
			{
				return new Color(230, 230, 230, 115);
			}
			if (this.type == 254 || this.type == 255)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 258)
			{
				return new Color(150, 50, 50, 0);
			}
			if (this.type == 263 || this.type == 264)
			{
				return new Color((int)(this.color.R / 2 + 127), (int)(this.color.G + 127), (int)(this.color.B + 127), (int)(this.color.A / 8)) * 0.5f;
			}
			if (this.type == 235)
			{
				return new Color(255, 255, 255, 0);
			}
			if (((this.type >= 86 && this.type <= 91) || this.type == 262 || this.type == 286) && !this.noLight)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 213 || this.type == 260)
			{
				int num8 = (int)(this.scale / 2.5f * 255f);
				return new Color(num8, num8, num8, num8);
			}
			if (this.type == 64 && this.alpha == 255 && this.noLight)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 197)
			{
				return new Color(250, 250, 250, 150);
			}
			if ((this.type >= 110 && this.type <= 114) || this.type == 311 || this.type == 312 || this.type == 313)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 204)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 181)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 182 || this.type == 206)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 159)
			{
				return new Color(250, 250, 250, 50);
			}
			if (this.type == 163 || this.type == 205)
			{
				return new Color(250, 250, 250, 0);
			}
			if (this.type == 170)
			{
				return new Color(200, 200, 200, 100);
			}
			if (this.type == 180)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 175)
			{
				return new Color(200, 200, 200, 0);
			}
			if (this.type == 183)
			{
				return new Color(50, 0, 0, 0);
			}
			if (this.type == 172)
			{
				return new Color(250, 250, 250, 150);
			}
			if (this.type == 160 || this.type == 162 || this.type == 164 || this.type == 173)
			{
				int num9 = (int)(250f * this.scale);
				return new Color(num9, num9, num9, 0);
			}
			if (this.type == 92 || this.type == 106 || this.type == 107)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 185)
			{
				return new Color(200, 200, 255, 125);
			}
			if (this.type == 127 || this.type == 187)
			{
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 156 || this.type == 230 || this.type == 234)
			{
				return new Color(255, 255, 255, 0);
			}
			if (this.type == 270)
			{
				return new Color((int)(newColor.R / 2 + 127), (int)(newColor.G / 2 + 127), (int)(newColor.B / 2 + 127), 25);
			}
			if (this.type == 271)
			{
				return new Color((int)(newColor.R / 2 + 127), (int)(newColor.G / 2 + 127), (int)(newColor.B / 2 + 127), 127);
			}
			if (this.type == 6 || this.type == 242 || this.type == 174 || this.type == 135 || this.type == 75 || this.type == 20 || this.type == 21 || this.type == 231 || this.type == 169 || (this.type >= 130 && this.type <= 134) || this.type == 158 || this.type == 293 || this.type == 294 || this.type == 295 || this.type == 296 || this.type == 297 || this.type == 298 || this.type == 307 || this.type == 310)
			{
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 278)
			{
				Color result;
				result..ctor(newColor.ToVector3() * this.color.ToVector3());
				result.A = 25;
				return result;
			}
			if (this.type >= 219 && this.type <= 223)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.5f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 226 || this.type == 272)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 228)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if (this.type == 279)
			{
				int a = (int)newColor.A;
				newColor = Color.Lerp(newColor, Color.White, 0.8f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, a) * MathHelper.Min(this.scale, 1f);
			}
			if (this.type == 229 || this.type == 269)
			{
				newColor = Color.Lerp(newColor, Color.White, 0.6f);
				return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 25);
			}
			if ((this.type == 68 || this.type == 70) && this.noGravity)
			{
				return new Color(255, 255, 255, 0);
			}
			int num4;
			int num3;
			int num2;
			if (this.type == 157)
			{
				num2 = (num3 = (num4 = 255));
				float num5 = (float)Main.mouseTextColor / 100f - 1.6f;
				num3 = (int)((float)num3 * num5);
				num2 = (int)((float)num2 * num5);
				num4 = (int)((float)num4 * num5);
				int a2 = (int)(100f * num5);
				num3 += 50;
				if (num3 > 255)
				{
					num3 = 255;
				}
				num2 += 50;
				if (num2 > 255)
				{
					num2 = 255;
				}
				num4 += 50;
				if (num4 > 255)
				{
					num4 = 255;
				}
				return new Color(num3, num2, num4, a2);
			}
			if (this.type == 284)
			{
				Color result2;
				result2..ctor(newColor.ToVector4() * this.color.ToVector4());
				result2.A = this.color.A;
				return result2;
			}
			if (this.type == 15 || this.type == 274 || this.type == 20 || this.type == 21 || this.type == 29 || this.type == 35 || this.type == 41 || this.type == 44 || this.type == 27 || this.type == 45 || this.type == 55 || this.type == 56 || this.type == 57 || this.type == 58 || this.type == 73 || this.type == 74)
			{
				num = (num + 3f) / 4f;
			}
			else if (this.type == 43)
			{
				num = (num + 9f) / 10f;
			}
			else
			{
				if (this.type >= 244 && this.type <= 247)
				{
					return new Color(255, 255, 255, 0);
				}
				if (this.type == 66)
				{
					return new Color((int)newColor.R, (int)newColor.G, (int)newColor.B, 0);
				}
				if (this.type == 267)
				{
					return new Color((int)this.color.R, (int)this.color.G, (int)this.color.B, 0);
				}
				if (this.type == 71)
				{
					return new Color(200, 200, 200, 0);
				}
				if (this.type == 72)
				{
					return new Color(200, 200, 200, 200);
				}
			}
			num3 = (int)((float)newColor.R * num);
			num2 = (int)((float)newColor.G * num);
			num4 = (int)((float)newColor.B * num);
			int num6 = (int)newColor.A - this.alpha;
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num6 > 255)
			{
				num6 = 255;
			}
			return new Color(num3, num2, num4, num6);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000214B0 File Offset: 0x0001F6B0
		public Color GetColor(Color newColor)
		{
			if (this.type == 284)
			{
				return Color.Transparent;
			}
			int num2 = (int)(this.color.R - (byte.MaxValue - newColor.R));
			int num3 = (int)(this.color.G - (byte.MaxValue - newColor.G));
			int num4 = (int)(this.color.B - (byte.MaxValue - newColor.B));
			int num5 = (int)(this.color.A - (byte.MaxValue - newColor.A));
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			return new Color(num2, num3, num4, num5);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00021591 File Offset: 0x0001F791
		public float GetVisualRotation()
		{
			if (this.type == 304)
			{
				return 0f;
			}
			return this.rotation;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000215AC File Offset: 0x0001F7AC
		public float GetVisualScale()
		{
			if (this.type == 304)
			{
				return 1f;
			}
			return this.scale;
		}

		// Token: 0x040001A4 RID: 420
		public static float dCount;

		// Token: 0x040001A5 RID: 421
		public static int lavaBubbles;

		// Token: 0x040001A6 RID: 422
		public static int SandStormCount;

		// Token: 0x040001A7 RID: 423
		public int dustIndex;

		/// <summary>
		/// Current position of this dust.
		/// </summary>
		// Token: 0x040001A8 RID: 424
		public Vector2 position;

		/// <summary>
		/// Current velocity of this dust.
		/// </summary>
		// Token: 0x040001A9 RID: 425
		public Vector2 velocity;

		/// <summary>
		/// Used by some dust AI to control logic pertaining to the dust fading in. The specific behavior depends on the dust type. Defaults to 0f.
		/// </summary>
		// Token: 0x040001AA RID: 426
		public float fadeIn;

		/// <summary>
		/// Indicates if a dust should be affected by gravity or not. Not all vanilla dust have logic checking this value and modded dust with custom <see cref="M:Terraria.ModLoader.ModDust.Update(Terraria.Dust)" /> code would need to implement this into the logic for it to have effect. Defaults to <see langword="false" />.
		/// </summary>
		// Token: 0x040001AB RID: 427
		public bool noGravity;

		/// <summary>
		/// The draw scale of the dust. Many dust rely on scale to determine when it should despawn. The Scale parameter of <see cref="M:Terraria.Dust.NewDust(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Int32,Microsoft.Xna.Framework.Color,System.Single)" /> will be slightly randomized (multiplied by a number between 80% to 120%) and then assigned to <see cref="F:Terraria.Dust.scale" />.
		/// </summary>
		// Token: 0x040001AC RID: 428
		public float scale;

		/// <summary>
		/// Current rotation of this dust.
		/// </summary>
		// Token: 0x040001AD RID: 429
		public float rotation;

		/// <summary>
		/// If true, the dust will not emit light. The specific behavior depends on the dust type, as many dust might not emit light or honor <see cref="F:Terraria.Dust.noLight" />. Defaults to <see langword="false" />.
		/// </summary>
		// Token: 0x040001AE RID: 430
		public bool noLight;

		// Token: 0x040001AF RID: 431
		public bool noLightEmittence;

		// Token: 0x040001B0 RID: 432
		public bool active;

		/// <summary>
		/// The Dust ID of this dust. The Dust ID will be equal to either a <see cref="T:Terraria.ID.DustID" /> entry or <see cref="M:Terraria.ModLoader.ModContent.DustType``1" />.
		/// </summary>
		// Token: 0x040001B1 RID: 433
		public int type;

		/// <summary>
		/// A tinting to the color this dust is drawn. Works best with sprites with low saturation, such as greyscale sprites. Not as effective with colored sprites.
		/// </summary>
		// Token: 0x040001B2 RID: 434
		public Color color;

		/// <summary>
		/// How transparent to draw this dust. 0 to 255. 255 is completely transparent. Used to fade dust out and sometimes to determine if a dust should despawn.
		/// </summary>
		// Token: 0x040001B3 RID: 435
		public int alpha;

		/// <summary>
		/// The portion of the sprite sheet that this dust will be drawn using. Typical vanilla dust spawn with a randomly chosen 8x8 frame from a set of 3 options, but modded dust can use whatever dimensions are desired. 
		/// </summary>
		// Token: 0x040001B4 RID: 436
		public Rectangle frame;

		/// <summary>
		/// The shader that will be applied to this dust when drawing. Typically assigned based on the dye attached to the accessory or equipment spawning the dust. This is done using code similar to <c>dust.shader = GameShaders.Armor.GetSecondaryShader(player.cPet, player);</c>
		/// </summary>
		// Token: 0x040001B5 RID: 437
		public ArmorShaderData shader;

		/// <summary>
		/// Can be used to store any custom data. Typically used to store a Player, NPC, or Projectile instance that the dust will attempt to follow.
		/// <para /> <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Dusts/ExampleAdvancedDust.cs">ExampleAdvancedDust.cs</see> showcases how customData can be used for advanced movement behavior.
		/// </summary>
		// Token: 0x040001B6 RID: 438
		public object customData;

		// Token: 0x040001B7 RID: 439
		public bool firstFrame;

		/// <summary>
		/// Used to temporarily store <see cref="F:Terraria.Dust.type" /> when using <see cref="P:Terraria.ModLoader.ModDust.UpdateType" /> to copy the behavior of an existing dust type.
		/// </summary>
		// Token: 0x040001B8 RID: 440
		internal int realType = -1;
	}
}
