using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020004AE RID: 1198
	public class PortalHelper
	{
		// Token: 0x0600399F RID: 14751 RVA: 0x00599934 File Offset: 0x00597B34
		static PortalHelper()
		{
			for (int i = 0; i < PortalHelper.SLOPE_EDGES.Length; i++)
			{
				PortalHelper.SLOPE_EDGES[i].Normalize();
			}
			for (int j = 0; j < PortalHelper.FoundPortals.GetLength(0); j++)
			{
				PortalHelper.FoundPortals[j, 0] = -1;
				PortalHelper.FoundPortals[j, 1] = -1;
			}
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x00599AD0 File Offset: 0x00597CD0
		public static void UpdatePortalPoints()
		{
			PortalHelper.anyPortalAtAll = false;
			for (int i = 0; i < PortalHelper.FoundPortals.GetLength(0); i++)
			{
				PortalHelper.FoundPortals[i, 0] = -1;
				PortalHelper.FoundPortals[i, 1] = -1;
			}
			for (int j = 0; j < PortalHelper.PortalCooldownForPlayers.Length; j++)
			{
				if (PortalHelper.PortalCooldownForPlayers[j] > 0)
				{
					PortalHelper.PortalCooldownForPlayers[j]--;
				}
			}
			for (int k = 0; k < PortalHelper.PortalCooldownForNPCs.Length; k++)
			{
				if (PortalHelper.PortalCooldownForNPCs[k] > 0)
				{
					PortalHelper.PortalCooldownForNPCs[k]--;
				}
			}
			for (int l = 0; l < 1000; l++)
			{
				Projectile projectile = Main.projectile[l];
				if (projectile.active && projectile.type == 602 && projectile.ai[1] >= 0f && projectile.ai[1] <= 1f && projectile.owner >= 0 && projectile.owner <= 255)
				{
					PortalHelper.FoundPortals[projectile.owner, (int)projectile.ai[1]] = l;
					if (PortalHelper.FoundPortals[projectile.owner, 0] != -1 && PortalHelper.FoundPortals[projectile.owner, 1] != -1)
					{
						PortalHelper.anyPortalAtAll = true;
					}
				}
			}
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x00599C2C File Offset: 0x00597E2C
		public static void TryGoingThroughPortals(Entity ent)
		{
			if (!PortalHelper.anyPortalAtAll)
			{
				return;
			}
			float collisionPoint = 0f;
			Vector2 velocity = ent.velocity;
			int width = ent.width;
			int height = ent.height;
			int num = 1;
			if (ent is Player)
			{
				num = (int)((Player)ent).gravDir;
			}
			for (int i = 0; i < PortalHelper.FoundPortals.GetLength(0); i++)
			{
				if (PortalHelper.FoundPortals[i, 0] != -1 && PortalHelper.FoundPortals[i, 1] != -1 && (!(ent is Player) || (i < PortalHelper.PortalCooldownForPlayers.Length && PortalHelper.PortalCooldownForPlayers[i] <= 0)) && (!(ent is NPC) || (i < PortalHelper.PortalCooldownForNPCs.Length && PortalHelper.PortalCooldownForNPCs[i] <= 0)))
				{
					for (int j = 0; j < 2; j++)
					{
						Projectile projectile = Main.projectile[PortalHelper.FoundPortals[i, j]];
						Vector2 start;
						Vector2 end;
						PortalHelper.GetPortalEdges(projectile.Center, projectile.ai[0], out start, out end);
						if (Collision.CheckAABBvLineCollision(ent.position + ent.velocity, ent.Size, start, end, 2f, ref collisionPoint))
						{
							Projectile projectile2 = Main.projectile[PortalHelper.FoundPortals[i, 1 - j]];
							float num2 = ent.Hitbox.Distance(projectile.Center);
							int bonusX;
							int bonusY;
							Vector2 vector = PortalHelper.GetPortalOutingPoint(ent.Size, projectile2.Center, projectile2.ai[0], out bonusX, out bonusY) + Vector2.Normalize(new Vector2((float)bonusX, (float)bonusY)) * num2;
							Vector2 vector2 = Vector2.UnitX * 16f;
							if (!(Collision.TileCollision(vector - vector2, vector2, width, height, true, true, num) != vector2))
							{
								vector2 = -Vector2.UnitX * 16f;
								if (!(Collision.TileCollision(vector - vector2, vector2, width, height, true, true, num) != vector2))
								{
									vector2 = Vector2.UnitY * 16f;
									if (!(Collision.TileCollision(vector - vector2, vector2, width, height, true, true, num) != vector2))
									{
										vector2 = -Vector2.UnitY * 16f;
										if (!(Collision.TileCollision(vector - vector2, vector2, width, height, true, true, num) != vector2))
										{
											float num3 = 0.1f;
											if (bonusY == -num)
											{
												num3 = 0.1f;
											}
											if (ent.velocity == Vector2.Zero)
											{
												ent.velocity = (projectile.ai[0] - 1.5707964f).ToRotationVector2() * num3;
											}
											if (ent.velocity.Length() < num3)
											{
												ent.velocity.Normalize();
												ent.velocity *= num3;
											}
											Vector2 vector3 = Vector2.Normalize(new Vector2((float)bonusX, (float)bonusY));
											if (vector3.HasNaNs() || vector3 == Vector2.Zero)
											{
												vector3 = Vector2.UnitX * (float)ent.direction;
											}
											ent.velocity = vector3 * ent.velocity.Length();
											if ((bonusY == -num && Math.Sign(ent.velocity.Y) != -num) || Math.Abs(ent.velocity.Y) < 0.1f)
											{
												ent.velocity.Y = (float)(-(float)num) * 0.1f;
											}
											int num4 = (int)((float)(projectile2.owner * 2) + projectile2.ai[1]);
											int lastPortalColorIndex = num4 + ((num4 % 2 == 0) ? 1 : -1);
											if (ent is Player)
											{
												Player player = (Player)ent;
												player.lastPortalColorIndex = lastPortalColorIndex;
												player.Teleport(vector, 4, num4);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(96, -1, -1, null, player.whoAmI, vector.X, vector.Y, (float)num4, 0, 0, 0);
													NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
												}
												PortalHelper.PortalCooldownForPlayers[i] = 10;
												return;
											}
											if (ent is NPC)
											{
												NPC nPC = (NPC)ent;
												nPC.lastPortalColorIndex = lastPortalColorIndex;
												nPC.Teleport(vector, 4, num4);
												if (Main.netMode == 2)
												{
													NetMessage.SendData(100, -1, -1, null, nPC.whoAmI, vector.X, vector.Y, (float)num4, 0, 0, 0);
													NetMessage.SendData(23, -1, -1, null, nPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
												}
												PortalHelper.PortalCooldownForPlayers[i] = 10;
												if (bonusY == -1 && ent.velocity.Y > -3f)
												{
													ent.velocity.Y = -3f;
												}
											}
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0059A10C File Offset: 0x0059830C
		public static int TryPlacingPortal(Projectile theBolt, Vector2 velocity, Vector2 theCrashVelocity)
		{
			Vector2 vector = velocity / velocity.Length();
			Point position = PortalHelper.FindCollision(theBolt.position, theBolt.position + velocity + vector * 32f).ToTileCoordinates();
			Tile tile = Main.tile[position.X, position.Y];
			Vector2 vector2;
			vector2..ctor((float)(position.X * 16 + 8), (float)(position.Y * 16 + 8));
			if (!WorldGen.SolidOrSlopedTile(tile))
			{
				return -1;
			}
			int num = (int)tile.slope();
			bool flag = tile.halfBrick();
			for (int i = 0; i < (flag ? 2 : PortalHelper.EDGES.Length); i++)
			{
				Point bestPosition;
				if (Vector2.Dot(PortalHelper.EDGES[i], vector) > 0f && PortalHelper.FindValidLine(position, (int)PortalHelper.EDGES[i].Y, (int)(0f - PortalHelper.EDGES[i].X), out bestPosition))
				{
					vector2..ctor((float)(bestPosition.X * 16 + 8), (float)(bestPosition.Y * 16 + 8));
					return PortalHelper.AddPortal(theBolt, vector2 - PortalHelper.EDGES[i] * (flag ? 0f : 8f), (float)Math.Atan2((double)PortalHelper.EDGES[i].Y, (double)PortalHelper.EDGES[i].X) + 1.5707964f, (int)theBolt.ai[0], theBolt.direction);
				}
			}
			if (num != 0)
			{
				Vector2 value = PortalHelper.SLOPE_EDGES[num - 1];
				Point bestPosition2;
				if (Vector2.Dot(value, -vector) > 0f && PortalHelper.FindValidLine(position, -PortalHelper.SLOPE_OFFSETS[num - 1].Y, PortalHelper.SLOPE_OFFSETS[num - 1].X, out bestPosition2))
				{
					vector2..ctor((float)(bestPosition2.X * 16 + 8), (float)(bestPosition2.Y * 16 + 8));
					return PortalHelper.AddPortal(theBolt, vector2, (float)Math.Atan2((double)value.Y, (double)value.X) - 1.5707964f, (int)theBolt.ai[0], theBolt.direction);
				}
			}
			return -1;
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0059A360 File Offset: 0x00598560
		private static bool FindValidLine(Point position, int xOffset, int yOffset, out Point bestPosition)
		{
			bestPosition = position;
			if (PortalHelper.IsValidLine(position, xOffset, yOffset))
			{
				return true;
			}
			Point point;
			point..ctor(position.X - xOffset, position.Y - yOffset);
			if (PortalHelper.IsValidLine(point, xOffset, yOffset))
			{
				bestPosition = point;
				return true;
			}
			Point point2;
			point2..ctor(position.X + xOffset, position.Y + yOffset);
			if (PortalHelper.IsValidLine(point2, xOffset, yOffset))
			{
				bestPosition = point2;
				return true;
			}
			return false;
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0059A3D8 File Offset: 0x005985D8
		private static bool IsValidLine(Point position, int xOffset, int yOffset)
		{
			Tile tile = Main.tile[position.X, position.Y];
			Tile tile2 = Main.tile[position.X - xOffset, position.Y - yOffset];
			Tile tile3 = Main.tile[position.X + xOffset, position.Y + yOffset];
			return !PortalHelper.BlockPortals(Main.tile[position.X + yOffset, position.Y - xOffset]) && !PortalHelper.BlockPortals(Main.tile[position.X + yOffset - xOffset, position.Y - xOffset - yOffset]) && !PortalHelper.BlockPortals(Main.tile[position.X + yOffset + xOffset, position.Y - xOffset + yOffset]) && (PortalHelper.CanPlacePortalOn(tile) && PortalHelper.CanPlacePortalOn(tile2) && PortalHelper.CanPlacePortalOn(tile3) && tile2.HasSameSlope(tile) && tile3.HasSameSlope(tile));
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x0059A4CE File Offset: 0x005986CE
		private unsafe static bool CanPlacePortalOn(Tile t)
		{
			return PortalHelper.DoesTileTypeSupportPortals(*t.type) && WorldGen.SolidOrSlopedTile(t);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0059A4E7 File Offset: 0x005986E7
		private static bool DoesTileTypeSupportPortals(ushort tileType)
		{
			return tileType != 496;
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x0059A4F4 File Offset: 0x005986F4
		private unsafe static bool BlockPortals(Tile t)
		{
			return t.active() && !Main.tileCut[(int)(*t.type)] && !TileID.Sets.BreakableWhenPlacing[(int)(*t.type)] && Main.tileSolid[(int)(*t.type)];
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x0059A534 File Offset: 0x00598734
		private static Vector2 FindCollision(Vector2 startPosition, Vector2 stopPosition)
		{
			int lastX = 0;
			int lastY = 0;
			Utils.PlotLine(startPosition.ToTileCoordinates(), stopPosition.ToTileCoordinates(), delegate(int x, int y)
			{
				lastX = x;
				lastY = y;
				return !WorldGen.SolidOrSlopedTile(x, y);
			}, false);
			return new Vector2((float)lastX * 16f, (float)lastY * 16f);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0059A594 File Offset: 0x00598794
		private static int AddPortal(Projectile sourceProjectile, Vector2 position, float angle, int form, int direction)
		{
			if (!PortalHelper.SupportedTilesAreFine(position, angle))
			{
				return -1;
			}
			PortalHelper.RemoveMyOldPortal(form);
			PortalHelper.RemoveIntersectingPortals(position, angle);
			int num = Projectile.NewProjectile(Entity.InheritSource(sourceProjectile), position.X, position.Y, 0f, 0f, 602, 0, 0f, Main.myPlayer, angle, (float)form, 0f);
			Main.projectile[num].direction = direction;
			Main.projectile[num].netUpdate = true;
			return num;
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0059A610 File Offset: 0x00598810
		private static void RemoveMyOldPortal(int form)
		{
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.type == 602 && projectile.owner == Main.myPlayer && projectile.ai[1] == (float)form)
				{
					projectile.Kill();
					return;
				}
			}
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0059A66C File Offset: 0x0059886C
		private static void RemoveIntersectingPortals(Vector2 position, float angle)
		{
			Vector2 start;
			Vector2 end;
			PortalHelper.GetPortalEdges(position, angle, out start, out end);
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.type == 602)
				{
					Vector2 start2;
					Vector2 end2;
					PortalHelper.GetPortalEdges(projectile.Center, projectile.ai[0], out start2, out end2);
					if (Collision.CheckLinevLine(start, end, start2, end2).Length != 0)
					{
						if (projectile.owner != Main.myPlayer && Main.netMode != 2)
						{
							NetMessage.SendData(95, -1, -1, null, projectile.owner, (float)((int)projectile.ai[1]), 0f, 0f, 0, 0, 0);
						}
						projectile.Kill();
					}
				}
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x0059A71E File Offset: 0x0059891E
		public static Color GetPortalColor(int colorIndex)
		{
			return PortalHelper.GetPortalColor(colorIndex / 2, colorIndex % 2);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x0059A72C File Offset: 0x0059892C
		public static Color GetPortalColor(int player, int portal)
		{
			Color white = Color.White;
			if (Main.netMode == 0)
			{
				white = ((portal != 0) ? Main.hslToRgb(0.52f, 1f, 0.6f, byte.MaxValue) : Main.hslToRgb(0.12f, 1f, 0.5f, byte.MaxValue));
			}
			else
			{
				float num = 0.08f;
				white = Main.hslToRgb((0.5f + (float)player * (num * 2f) + (float)portal * num) % 1f, 1f, 0.5f, byte.MaxValue);
			}
			white.A = 66;
			return white;
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x0059A7C0 File Offset: 0x005989C0
		private static void GetPortalEdges(Vector2 position, float angle, out Vector2 start, out Vector2 end)
		{
			Vector2 vector = angle.ToRotationVector2();
			start = position + vector * -22f;
			end = position + vector * 22f;
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0059A804 File Offset: 0x00598A04
		private static Vector2 GetPortalOutingPoint(Vector2 objectSize, Vector2 portalPosition, float portalAngle, out int bonusX, out int bonusY)
		{
			int num = (int)Math.Round((double)(MathHelper.WrapAngle(portalAngle) / 0.7853982f));
			switch (num)
			{
			case -3:
			case 3:
				bonusX = ((num == -3) ? 1 : -1);
				bonusY = -1;
				return portalPosition + new Vector2((num == -3) ? 0f : (0f - objectSize.X), 0f - objectSize.Y);
			case -2:
			case 2:
				bonusX = ((num != 2) ? 1 : -1);
				bonusY = 0;
				return portalPosition + new Vector2((num == 2) ? (0f - objectSize.X) : 0f, (0f - objectSize.Y) / 2f);
			case -1:
			case 1:
				bonusX = ((num == -1) ? 1 : -1);
				bonusY = 1;
				return portalPosition + new Vector2((num == -1) ? 0f : (0f - objectSize.X), 0f);
			case 0:
			case 4:
				bonusX = 0;
				bonusY = ((num == 0) ? 1 : -1);
				return portalPosition + new Vector2((0f - objectSize.X) / 2f, (num == 0) ? 0f : (0f - objectSize.Y));
			default:
				bonusX = 0;
				bonusY = 0;
				return portalPosition;
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0059A954 File Offset: 0x00598B54
		public static void SyncPortalsOnPlayerJoin(int plr, int fluff, List<Point> dontInclude, out List<Point> portalSections)
		{
			portalSections = new List<Point>();
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && (projectile.type == 602 || projectile.type == 601))
				{
					Vector2 center = projectile.Center;
					int sectionX = Netplay.GetSectionX((int)(center.X / 16f));
					int sectionY = Netplay.GetSectionY((int)(center.Y / 16f));
					for (int j = sectionX - fluff; j < sectionX + fluff + 1; j++)
					{
						for (int k = sectionY - fluff; k < sectionY + fluff + 1; k++)
						{
							if (j >= 0 && j < Main.maxSectionsX && k >= 0 && k < Main.maxSectionsY && !Netplay.Clients[plr].TileSections[j, k] && !dontInclude.Contains(new Point(j, k)))
							{
								portalSections.Add(new Point(j, k));
							}
						}
					}
				}
			}
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0059AA5C File Offset: 0x00598C5C
		public static void SyncPortalSections(Vector2 portalPosition, int fluff)
		{
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					RemoteClient.CheckSection(i, portalPosition, fluff);
				}
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0059AA90 File Offset: 0x00598C90
		public static bool SupportedTilesAreFine(Vector2 portalCenter, float portalAngle)
		{
			Point point = portalCenter.ToTileCoordinates();
			int num = (int)Math.Round((double)(MathHelper.WrapAngle(portalAngle) / 0.7853982f));
			int num2;
			int num3;
			switch (num)
			{
			case -3:
			case 3:
				num2 = ((num == -3) ? 1 : -1);
				num3 = -1;
				break;
			case -2:
			case 2:
				num2 = ((num != 2) ? 1 : -1);
				num3 = 0;
				break;
			case -1:
			case 1:
				num2 = ((num == -1) ? 1 : -1);
				num3 = 1;
				break;
			case 0:
			case 4:
				num2 = 0;
				num3 = ((num == 0) ? 1 : -1);
				break;
			default:
				Main.NewText(string.Concat(new string[]
				{
					"Broken portal! (over4s = ",
					num.ToString(),
					" , ",
					portalAngle.ToString(),
					")"
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				return false;
			}
			if (num2 != 0 && num3 != 0)
			{
				int num4 = 3;
				if (num2 == -1 && num3 == 1)
				{
					num4 = 5;
				}
				if (num2 == 1 && num3 == -1)
				{
					num4 = 2;
				}
				if (num2 == 1 && num3 == 1)
				{
					num4 = 4;
				}
				num4--;
				return PortalHelper.SupportedSlope(point.X, point.Y, num4) && PortalHelper.SupportedSlope(point.X + num2, point.Y - num3, num4) && PortalHelper.SupportedSlope(point.X - num2, point.Y + num3, num4);
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					point.X--;
				}
				return PortalHelper.SupportedNormal(point.X, point.Y) && PortalHelper.SupportedNormal(point.X, point.Y - 1) && PortalHelper.SupportedNormal(point.X, point.Y + 1);
			}
			if (num3 != 0)
			{
				if (num3 == 1)
				{
					point.Y--;
				}
				return (PortalHelper.SupportedNormal(point.X, point.Y) && PortalHelper.SupportedNormal(point.X + 1, point.Y) && PortalHelper.SupportedNormal(point.X - 1, point.Y)) || (PortalHelper.SupportedHalfbrick(point.X, point.Y) && PortalHelper.SupportedHalfbrick(point.X + 1, point.Y) && PortalHelper.SupportedHalfbrick(point.X - 1, point.Y));
			}
			return true;
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x0059ACC4 File Offset: 0x00598EC4
		private unsafe static bool SupportedSlope(int x, int y, int slope)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)(*tile.type)] && !TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)] && Main.tileSolid[(int)(*tile.type)] && (int)tile.slope() == slope && PortalHelper.DoesTileTypeSupportPortals(*tile.type);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0059AD3C File Offset: 0x00598F3C
		private unsafe static bool SupportedHalfbrick(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)(*tile.type)] && !TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)] && Main.tileSolid[(int)(*tile.type)] && tile.halfBrick() && PortalHelper.DoesTileTypeSupportPortals(*tile.type);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0059ADB0 File Offset: 0x00598FB0
		private unsafe static bool SupportedNormal(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)(*tile.type)] && !TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)] && Main.tileSolid[(int)(*tile.type)] && !TileID.Sets.NotReallySolid[(int)(*tile.type)] && !tile.halfBrick() && tile.slope() == 0 && PortalHelper.DoesTileTypeSupportPortals(*tile.type);
		}

		// Token: 0x04005271 RID: 21105
		public const int PORTALS_PER_PERSON = 2;

		// Token: 0x04005272 RID: 21106
		private static int[,] FoundPortals = new int[256, 2];

		// Token: 0x04005273 RID: 21107
		private static int[] PortalCooldownForPlayers = new int[256];

		// Token: 0x04005274 RID: 21108
		private static int[] PortalCooldownForNPCs = new int[200];

		// Token: 0x04005275 RID: 21109
		private static readonly Vector2[] EDGES = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(0f, -1f),
			new Vector2(1f, 0f),
			new Vector2(-1f, 0f)
		};

		// Token: 0x04005276 RID: 21110
		private static readonly Vector2[] SLOPE_EDGES = new Vector2[]
		{
			new Vector2(1f, -1f),
			new Vector2(-1f, -1f),
			new Vector2(1f, 1f),
			new Vector2(-1f, 1f)
		};

		// Token: 0x04005277 RID: 21111
		private static readonly Point[] SLOPE_OFFSETS = new Point[]
		{
			new Point(1, -1),
			new Point(-1, -1),
			new Point(1, 1),
			new Point(-1, 1)
		};

		// Token: 0x04005278 RID: 21112
		private static bool anyPortalAtAll = false;
	}
}
