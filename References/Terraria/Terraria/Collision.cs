using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x0200003B RID: 59
	public class Collision
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0025A790 File Offset: 0x00258990
		public static Vector2[] CheckLinevLine(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
		{
			if (a1.Equals(a2) && b1.Equals(b2))
			{
				if (a1.Equals(b1))
				{
					return new Vector2[]
					{
						a1
					};
				}
				return new Vector2[0];
			}
			else if (b1.Equals(b2))
			{
				if (Collision.PointOnLine(b1, a1, a2))
				{
					return new Vector2[]
					{
						b1
					};
				}
				return new Vector2[0];
			}
			else if (a1.Equals(a2))
			{
				if (Collision.PointOnLine(a1, b1, b2))
				{
					return new Vector2[]
					{
						a1
					};
				}
				return new Vector2[0];
			}
			else
			{
				float num = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
				float num2 = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
				float num3 = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
				if (-Collision.Epsilon >= num3 || num3 >= Collision.Epsilon)
				{
					float num4 = num / num3;
					float num5 = num2 / num3;
					if (0f <= num4 && num4 <= 1f && 0f <= num5 && num5 <= 1f)
					{
						return new Vector2[]
						{
							new Vector2(a1.X + num4 * (a2.X - a1.X), a1.Y + num4 * (a2.Y - a1.Y))
						};
					}
					return new Vector2[0];
				}
				else
				{
					if ((-Collision.Epsilon >= num || num >= Collision.Epsilon) && (-Collision.Epsilon >= num2 || num2 >= Collision.Epsilon))
					{
						return new Vector2[0];
					}
					if (a1.Equals(a2))
					{
						return Collision.OneDimensionalIntersection(b1, b2, a1, a2);
					}
					return Collision.OneDimensionalIntersection(a1, a2, b1, b2);
				}
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0025A998 File Offset: 0x00258B98
		private static double DistFromSeg(Vector2 p, Vector2 q0, Vector2 q1, double radius, ref float u)
		{
			double num = (double)(q1.X - q0.X);
			double num2 = (double)(q1.Y - q0.Y);
			double num3 = (double)(q0.X - p.X);
			double num4 = (double)(q0.Y - p.Y);
			double num5 = Math.Sqrt(num * num + num2 * num2);
			if (num5 < (double)Collision.Epsilon)
			{
				throw new Exception("Expected line segment, not point.");
			}
			return Math.Abs(num * num4 - num3 * num2) / num5;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0025AA10 File Offset: 0x00258C10
		private static bool PointOnLine(Vector2 p, Vector2 a1, Vector2 a2)
		{
			float num = 0f;
			return Collision.DistFromSeg(p, a1, a2, (double)Collision.Epsilon, ref num) < (double)Collision.Epsilon;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0025AA3C File Offset: 0x00258C3C
		private static Vector2[] OneDimensionalIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
		{
			float num = a2.X - a1.X;
			float num2 = a2.Y - a1.Y;
			float relativePoint;
			float relativePoint2;
			if (Math.Abs(num) > Math.Abs(num2))
			{
				relativePoint = (b1.X - a1.X) / num;
				relativePoint2 = (b2.X - a1.X) / num;
			}
			else
			{
				relativePoint = (b1.Y - a1.Y) / num2;
				relativePoint2 = (b2.Y - a1.Y) / num2;
			}
			List<Vector2> list = new List<Vector2>();
			foreach (float num3 in Collision.FindOverlapPoints(relativePoint, relativePoint2))
			{
				float x = a2.X * num3 + a1.X * (1f - num3);
				float y = a2.Y * num3 + a1.Y * (1f - num3);
				list.Add(new Vector2(x, y));
			}
			return list.ToArray();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0025AB2C File Offset: 0x00258D2C
		private static float[] FindOverlapPoints(float relativePoint1, float relativePoint2)
		{
			float val = Math.Min(relativePoint1, relativePoint2);
			float val2 = Math.Max(relativePoint1, relativePoint2);
			float num = Math.Max(0f, val);
			float num2 = Math.Min(1f, val2);
			if (num > num2)
			{
				return new float[0];
			}
			if (num == num2)
			{
				return new float[]
				{
					num
				};
			}
			return new float[]
			{
				num,
				num2
			};
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0025AB8C File Offset: 0x00258D8C
		public static bool CheckAABBvAABBCollision(Vector2 position1, Vector2 dimensions1, Vector2 position2, Vector2 dimensions2)
		{
			return position1.X < position2.X + dimensions2.X && position1.Y < position2.Y + dimensions2.Y && position1.X + dimensions1.X > position2.X && position1.Y + dimensions1.Y > position2.Y;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0025ABF0 File Offset: 0x00258DF0
		private static int collisionOutcode(Vector2 aabbPosition, Vector2 aabbDimensions, Vector2 point)
		{
			float num = aabbPosition.X + aabbDimensions.X;
			float num2 = aabbPosition.Y + aabbDimensions.Y;
			int num3 = 0;
			if (aabbDimensions.X <= 0f)
			{
				num3 |= 5;
			}
			else if (point.X < aabbPosition.X)
			{
				num3 |= 1;
			}
			else if (point.X - num > 0f)
			{
				num3 |= 4;
			}
			if (aabbDimensions.Y <= 0f)
			{
				num3 |= 10;
			}
			else if (point.Y < aabbPosition.Y)
			{
				num3 |= 2;
			}
			else if (point.Y - num2 > 0f)
			{
				num3 |= 8;
			}
			return num3;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0025AC94 File Offset: 0x00258E94
		public static bool CheckAABBvLineCollision(Vector2 aabbPosition, Vector2 aabbDimensions, Vector2 lineStart, Vector2 lineEnd)
		{
			int num;
			if ((num = Collision.collisionOutcode(aabbPosition, aabbDimensions, lineEnd)) == 0)
			{
				return true;
			}
			int num2;
			while ((num2 = Collision.collisionOutcode(aabbPosition, aabbDimensions, lineStart)) != 0)
			{
				if ((num2 & num) != 0)
				{
					return false;
				}
				if ((num2 & 5) != 0)
				{
					float num3 = aabbPosition.X;
					if ((num2 & 4) != 0)
					{
						num3 += aabbDimensions.X;
					}
					lineStart.Y += (num3 - lineStart.X) * (lineEnd.Y - lineStart.Y) / (lineEnd.X - lineStart.X);
					lineStart.X = num3;
				}
				else
				{
					float num4 = aabbPosition.Y;
					if ((num2 & 8) != 0)
					{
						num4 += aabbDimensions.Y;
					}
					lineStart.X += (num4 - lineStart.Y) * (lineEnd.X - lineStart.X) / (lineEnd.Y - lineStart.Y);
					lineStart.Y = num4;
				}
			}
			return true;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0025AD68 File Offset: 0x00258F68
		public static bool CheckAABBvLineCollision2(Vector2 aabbPosition, Vector2 aabbDimensions, Vector2 lineStart, Vector2 lineEnd)
		{
			float num = 0f;
			return Utils.RectangleLineCollision(aabbPosition, aabbPosition + aabbDimensions, lineStart, lineEnd) || Collision.CheckAABBvLineCollision(aabbPosition, aabbDimensions, lineStart, lineEnd, 0.0001f, ref num);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0025ADA0 File Offset: 0x00258FA0
		public static bool CheckAABBvLineCollision(Vector2 objectPosition, Vector2 objectDimensions, Vector2 lineStart, Vector2 lineEnd, float lineWidth, ref float collisionPoint)
		{
			float num = lineWidth * 0.5f;
			Vector2 position = lineStart;
			Vector2 vector = lineEnd - lineStart;
			if (vector.X > 0f)
			{
				vector.X += lineWidth;
				position.X -= num;
			}
			else
			{
				position.X += vector.X - num;
				vector.X = -vector.X + lineWidth;
			}
			if (vector.Y > 0f)
			{
				vector.Y += lineWidth;
				position.Y -= num;
			}
			else
			{
				position.Y += vector.Y - num;
				vector.Y = -vector.Y + lineWidth;
			}
			if (!Collision.CheckAABBvAABBCollision(objectPosition, objectDimensions, position, vector))
			{
				return false;
			}
			Vector2 vector2 = objectPosition - lineStart;
			Vector2 vector3 = vector2 + objectDimensions;
			Vector2 spinningpoint = new Vector2(vector2.X, vector3.Y);
			Vector2 spinningpoint2 = new Vector2(vector3.X, vector2.Y);
			Vector2 vector4 = lineEnd - lineStart;
			float num2 = vector4.Length();
			float num3 = (float)Math.Atan2((double)vector4.Y, (double)vector4.X);
			Vector2[] array = new Vector2[]
			{
				vector2.RotatedBy((double)(-(double)num3), default(Vector2)),
				spinningpoint2.RotatedBy((double)(-(double)num3), default(Vector2)),
				vector3.RotatedBy((double)(-(double)num3), default(Vector2)),
				spinningpoint.RotatedBy((double)(-(double)num3), default(Vector2))
			};
			collisionPoint = num2;
			bool result = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (Math.Abs(array[i].Y) < num && array[i].X < collisionPoint && array[i].X >= 0f)
				{
					collisionPoint = array[i].X;
					result = true;
				}
			}
			Vector2 vector5 = new Vector2(0f, num);
			Vector2 value = new Vector2(num2, num);
			Vector2 vector6 = new Vector2(0f, -num);
			Vector2 value2 = new Vector2(num2, -num);
			for (int j = 0; j < array.Length; j++)
			{
				int num4 = (j + 1) % array.Length;
				Vector2 vector7 = value - vector5;
				Vector2 vector8 = array[num4] - array[j];
				float num5 = vector7.X * vector8.Y - vector7.Y * vector8.X;
				if (num5 != 0f)
				{
					Vector2 vector9 = array[j] - vector5;
					float num6 = (vector9.X * vector8.Y - vector9.Y * vector8.X) / num5;
					if (num6 >= 0f && num6 <= 1f)
					{
						float num7 = (vector9.X * vector7.Y - vector9.Y * vector7.X) / num5;
						if (num7 >= 0f && num7 <= 1f)
						{
							result = true;
							collisionPoint = Math.Min(collisionPoint, vector5.X + num6 * vector7.X);
						}
					}
				}
				vector7 = value2 - vector6;
				num5 = vector7.X * vector8.Y - vector7.Y * vector8.X;
				if (num5 != 0f)
				{
					Vector2 vector10 = array[j] - vector6;
					float num8 = (vector10.X * vector8.Y - vector10.Y * vector8.X) / num5;
					if (num8 >= 0f && num8 <= 1f)
					{
						float num9 = (vector10.X * vector7.Y - vector10.Y * vector7.X) / num5;
						if (num9 >= 0f && num9 <= 1f)
						{
							result = true;
							collisionPoint = Math.Min(collisionPoint, vector6.X + num8 * vector7.X);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0025B1CB File Offset: 0x002593CB
		public static bool CanHit(Entity source, Entity target)
		{
			return Collision.CanHit(source.position, source.width, source.height, target.position, target.width, target.height);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0025B1F6 File Offset: 0x002593F6
		public static bool CanHit(Entity source, NPCAimedTarget target)
		{
			return Collision.CanHit(source.position, source.width, source.height, target.Position, target.Width, target.Height);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0025B221 File Offset: 0x00259421
		public static bool CanHit(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
		{
			return Collision.CanHit(Position1.ToPoint(), Width1, Height1, Position2.ToPoint(), Width2, Height2);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0025B23C File Offset: 0x0025943C
		public static bool CanHit(Point Position1, int Width1, int Height1, Point Position2, int Width2, int Height2)
		{
			int num = (Position1.X + Width1 / 2) / 16;
			int num2 = (Position1.Y + Height1 / 2) / 16;
			int num3 = (Position2.X + Width2 / 2) / 16;
			int num4 = (Position2.Y + Height2 / 2) / 16;
			if (num <= 1)
			{
				num = 1;
			}
			if (num >= Main.maxTilesX)
			{
				num = Main.maxTilesX - 1;
			}
			if (num3 <= 1)
			{
				num3 = 1;
			}
			if (num3 >= Main.maxTilesX)
			{
				num3 = Main.maxTilesX - 1;
			}
			if (num2 <= 1)
			{
				num2 = 1;
			}
			if (num2 >= Main.maxTilesY)
			{
				num2 = Main.maxTilesY - 1;
			}
			if (num4 <= 1)
			{
				num4 = 1;
			}
			if (num4 >= Main.maxTilesY)
			{
				num4 = Main.maxTilesY - 1;
			}
			bool result;
			try
			{
				for (;;)
				{
					int num5 = Math.Abs(num - num3);
					int num6 = Math.Abs(num2 - num4);
					if (num == num3 && num2 == num4)
					{
						break;
					}
					if (num5 > num6)
					{
						if (num < num3)
						{
							num++;
						}
						else
						{
							num--;
						}
						if (Main.tile[num, num2 - 1] == null)
						{
							goto Block_14;
						}
						if (Main.tile[num, num2 + 1] == null)
						{
							goto Block_15;
						}
						if (!Main.tile[num, num2 - 1].inActive() && Main.tile[num, num2 - 1].active() && Main.tileSolid[(int)Main.tile[num, num2 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 - 1].type] && Main.tile[num, num2 - 1].slope() == 0 && !Main.tile[num, num2 - 1].halfBrick() && !Main.tile[num, num2 + 1].inActive() && Main.tile[num, num2 + 1].active() && Main.tileSolid[(int)Main.tile[num, num2 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 + 1].type] && Main.tile[num, num2 + 1].slope() == 0 && !Main.tile[num, num2 + 1].halfBrick())
						{
							goto Block_27;
						}
					}
					else
					{
						if (num2 < num4)
						{
							num2++;
						}
						else
						{
							num2--;
						}
						if (Main.tile[num - 1, num2] == null)
						{
							goto Block_29;
						}
						if (Main.tile[num + 1, num2] == null)
						{
							goto Block_30;
						}
						if (!Main.tile[num - 1, num2].inActive() && Main.tile[num - 1, num2].active() && Main.tileSolid[(int)Main.tile[num - 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num - 1, num2].type] && Main.tile[num - 1, num2].slope() == 0 && !Main.tile[num - 1, num2].halfBrick() && !Main.tile[num + 1, num2].inActive() && Main.tile[num + 1, num2].active() && Main.tileSolid[(int)Main.tile[num + 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num + 1, num2].type] && Main.tile[num + 1, num2].slope() == 0 && !Main.tile[num + 1, num2].halfBrick())
						{
							goto Block_42;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_43;
					}
					if (!Main.tile[num, num2].inActive() && Main.tile[num, num2].active() && Main.tileSolid[(int)Main.tile[num, num2].type] && !Main.tileSolidTop[(int)Main.tile[num, num2].type])
					{
						goto Block_47;
					}
				}
				return true;
				Block_14:
				return false;
				Block_15:
				return false;
				Block_27:
				return false;
				Block_29:
				return false;
				Block_30:
				return false;
				Block_42:
				return false;
				Block_43:
				return false;
				Block_47:
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0025B694 File Offset: 0x00259894
		public static bool CanHitWithCheck(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2, Utils.TileActionAttempt check)
		{
			int num = (int)((Position1.X + (float)(Width1 / 2)) / 16f);
			int num2 = (int)((Position1.Y + (float)(Height1 / 2)) / 16f);
			int num3 = (int)((Position2.X + (float)(Width2 / 2)) / 16f);
			int num4 = (int)((Position2.Y + (float)(Height2 / 2)) / 16f);
			if (num <= 1)
			{
				num = 1;
			}
			if (num >= Main.maxTilesX)
			{
				num = Main.maxTilesX - 1;
			}
			if (num3 <= 1)
			{
				num3 = 1;
			}
			if (num3 >= Main.maxTilesX)
			{
				num3 = Main.maxTilesX - 1;
			}
			if (num2 <= 1)
			{
				num2 = 1;
			}
			if (num2 >= Main.maxTilesY)
			{
				num2 = Main.maxTilesY - 1;
			}
			if (num4 <= 1)
			{
				num4 = 1;
			}
			if (num4 >= Main.maxTilesY)
			{
				num4 = Main.maxTilesY - 1;
			}
			bool result;
			try
			{
				for (;;)
				{
					int num5 = Math.Abs(num - num3);
					int num6 = Math.Abs(num2 - num4);
					if (num == num3 && num2 == num4)
					{
						break;
					}
					if (num5 > num6)
					{
						if (num < num3)
						{
							num++;
						}
						else
						{
							num--;
						}
						if (Main.tile[num, num2 - 1] == null)
						{
							goto Block_14;
						}
						if (Main.tile[num, num2 + 1] == null)
						{
							goto Block_15;
						}
						if (!Main.tile[num, num2 - 1].inActive() && Main.tile[num, num2 - 1].active() && Main.tileSolid[(int)Main.tile[num, num2 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 - 1].type] && Main.tile[num, num2 - 1].slope() == 0 && !Main.tile[num, num2 - 1].halfBrick() && !Main.tile[num, num2 + 1].inActive() && Main.tile[num, num2 + 1].active() && Main.tileSolid[(int)Main.tile[num, num2 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 + 1].type] && Main.tile[num, num2 + 1].slope() == 0 && !Main.tile[num, num2 + 1].halfBrick())
						{
							goto Block_27;
						}
					}
					else
					{
						if (num2 < num4)
						{
							num2++;
						}
						else
						{
							num2--;
						}
						if (Main.tile[num - 1, num2] == null)
						{
							goto Block_29;
						}
						if (Main.tile[num + 1, num2] == null)
						{
							goto Block_30;
						}
						if (!Main.tile[num - 1, num2].inActive() && Main.tile[num - 1, num2].active() && Main.tileSolid[(int)Main.tile[num - 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num - 1, num2].type] && Main.tile[num - 1, num2].slope() == 0 && !Main.tile[num - 1, num2].halfBrick() && !Main.tile[num + 1, num2].inActive() && Main.tile[num + 1, num2].active() && Main.tileSolid[(int)Main.tile[num + 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num + 1, num2].type] && Main.tile[num + 1, num2].slope() == 0 && !Main.tile[num + 1, num2].halfBrick())
						{
							goto Block_42;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_43;
					}
					if (!Main.tile[num, num2].inActive() && Main.tile[num, num2].active() && Main.tileSolid[(int)Main.tile[num, num2].type] && !Main.tileSolidTop[(int)Main.tile[num, num2].type])
					{
						goto Block_47;
					}
					if (!check(num, num2))
					{
						goto Block_48;
					}
				}
				return true;
				Block_14:
				return false;
				Block_15:
				return false;
				Block_27:
				return false;
				Block_29:
				return false;
				Block_30:
				return false;
				Block_42:
				return false;
				Block_43:
				return false;
				Block_47:
				return false;
				Block_48:
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0025BB08 File Offset: 0x00259D08
		public static bool CanHitLine(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
		{
			int num = (int)((Position1.X + (float)(Width1 / 2)) / 16f);
			int num2 = (int)((Position1.Y + (float)(Height1 / 2)) / 16f);
			int num3 = (int)((Position2.X + (float)(Width2 / 2)) / 16f);
			int num4 = (int)((Position2.Y + (float)(Height2 / 2)) / 16f);
			if (num <= 1)
			{
				num = 1;
			}
			if (num >= Main.maxTilesX)
			{
				num = Main.maxTilesX - 1;
			}
			if (num3 <= 1)
			{
				num3 = 1;
			}
			if (num3 >= Main.maxTilesX)
			{
				num3 = Main.maxTilesX - 1;
			}
			if (num2 <= 1)
			{
				num2 = 1;
			}
			if (num2 >= Main.maxTilesY)
			{
				num2 = Main.maxTilesY - 1;
			}
			if (num4 <= 1)
			{
				num4 = 1;
			}
			if (num4 >= Main.maxTilesY)
			{
				num4 = Main.maxTilesY - 1;
			}
			float num5 = (float)Math.Abs(num - num3);
			float num6 = (float)Math.Abs(num2 - num4);
			if (num5 == 0f && num6 == 0f)
			{
				return true;
			}
			float num7 = 1f;
			float num8 = 1f;
			if (num5 == 0f || num6 == 0f)
			{
				if (num5 == 0f)
				{
					num7 = 0f;
				}
				if (num6 == 0f)
				{
					num8 = 0f;
				}
			}
			else if (num5 > num6)
			{
				num7 = num5 / num6;
			}
			else
			{
				num8 = num6 / num5;
			}
			float num9 = 0f;
			float num10 = 0f;
			int num11 = 1;
			if (num2 < num4)
			{
				num11 = 2;
			}
			int num12 = (int)num5;
			int num13 = (int)num6;
			int num14 = Math.Sign(num3 - num);
			int num15 = Math.Sign(num4 - num2);
			bool flag = false;
			bool flag2 = false;
			bool result;
			try
			{
				for (;;)
				{
					if (num11 == 2)
					{
						num9 += num7;
						int num16 = (int)num9;
						num9 %= 1f;
						for (int i = 0; i < num16; i++)
						{
							if (Main.tile[num, num2 - 1] == null)
							{
								goto Block_18;
							}
							if (Main.tile[num, num2] == null)
							{
								goto Block_19;
							}
							if (Main.tile[num, num2 + 1] == null)
							{
								goto Block_20;
							}
							Tile tile = Main.tile[num, num2 - 1];
							Tile tile2 = Main.tile[num, num2 + 1];
							Tile tile3 = Main.tile[num, num2];
							if ((!tile.inActive() && tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type]) || (!tile2.inActive() && tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type]) || (!tile3.inActive() && tile3.active() && Main.tileSolid[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type]))
							{
								goto IL_28E;
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num += num14;
							num12--;
							if (num12 == 0 && num13 == 0 && num16 == 1)
							{
								flag2 = true;
							}
						}
						if (num13 != 0)
						{
							num11 = 1;
						}
					}
					else if (num11 == 1)
					{
						num10 += num8;
						int num17 = (int)num10;
						num10 %= 1f;
						for (int j = 0; j < num17; j++)
						{
							if (Main.tile[num - 1, num2] == null)
							{
								goto Block_37;
							}
							if (Main.tile[num, num2] == null)
							{
								goto Block_38;
							}
							if (Main.tile[num + 1, num2] == null)
							{
								goto Block_39;
							}
							Tile tile4 = Main.tile[num - 1, num2];
							Tile tile5 = Main.tile[num + 1, num2];
							Tile tile6 = Main.tile[num, num2];
							if ((!tile4.inActive() && tile4.active() && Main.tileSolid[(int)tile4.type] && !Main.tileSolidTop[(int)tile4.type]) || (!tile5.inActive() && tile5.active() && Main.tileSolid[(int)tile5.type] && !Main.tileSolidTop[(int)tile5.type]) || (!tile6.inActive() && tile6.active() && Main.tileSolid[(int)tile6.type] && !Main.tileSolidTop[(int)tile6.type]))
							{
								goto IL_406;
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num2 += num15;
							num13--;
							if (num12 == 0 && num13 == 0 && num17 == 1)
							{
								flag2 = true;
							}
						}
						if (num12 != 0)
						{
							num11 = 2;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_55;
					}
					Tile tile7 = Main.tile[num, num2];
					if (!tile7.inActive() && tile7.active() && Main.tileSolid[(int)tile7.type] && !Main.tileSolidTop[(int)tile7.type])
					{
						goto Block_59;
					}
					if (flag || flag2)
					{
						goto Block_60;
					}
				}
				Block_18:
				return false;
				Block_19:
				return false;
				Block_20:
				return false;
				IL_28E:
				return false;
				Block_37:
				return false;
				Block_38:
				return false;
				Block_39:
				return false;
				IL_406:
				return false;
				Block_55:
				return false;
				Block_59:
				return false;
				Block_60:
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0025BFEC File Offset: 0x0025A1EC
		public static bool TupleHitLine(int x1, int y1, int x2, int y2, int ignoreX, int ignoreY, List<Tuple<int, int>> ignoreTargets, out Tuple<int, int> col)
		{
			int num = Utils.Clamp<int>(x1, 1, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(x2, 1, Main.maxTilesX - 1);
			int num3 = Utils.Clamp<int>(y1, 1, Main.maxTilesY - 1);
			int num4 = Utils.Clamp<int>(y2, 1, Main.maxTilesY - 1);
			float num5 = (float)Math.Abs(num - num2);
			float num6 = (float)Math.Abs(num3 - num4);
			if (num5 == 0f && num6 == 0f)
			{
				col = new Tuple<int, int>(num, num3);
				return true;
			}
			float num7 = 1f;
			float num8 = 1f;
			if (num5 == 0f || num6 == 0f)
			{
				if (num5 == 0f)
				{
					num7 = 0f;
				}
				if (num6 == 0f)
				{
					num8 = 0f;
				}
			}
			else if (num5 > num6)
			{
				num7 = num5 / num6;
			}
			else
			{
				num8 = num6 / num5;
			}
			float num9 = 0f;
			float num10 = 0f;
			int num11 = 1;
			if (num3 < num4)
			{
				num11 = 2;
			}
			int num12 = (int)num5;
			int num13 = (int)num6;
			int num14 = Math.Sign(num2 - num);
			int num15 = Math.Sign(num4 - num3);
			bool flag = false;
			bool flag2 = false;
			bool result;
			try
			{
				for (;;)
				{
					if (num11 == 2)
					{
						num9 += num7;
						int num16 = (int)num9;
						num9 %= 1f;
						for (int i = 0; i < num16; i++)
						{
							if (Main.tile[num, num3 - 1] == null)
							{
								goto Block_10;
							}
							if (Main.tile[num, num3 + 1] == null)
							{
								goto Block_11;
							}
							Tile tile = Main.tile[num, num3 - 1];
							Tile tile2 = Main.tile[num, num3 + 1];
							Tile tile3 = Main.tile[num, num3];
							if (!ignoreTargets.Contains(new Tuple<int, int>(num, num3)) && !ignoreTargets.Contains(new Tuple<int, int>(num, num3 - 1)) && !ignoreTargets.Contains(new Tuple<int, int>(num, num3 + 1)))
							{
								if (ignoreY != -1 && num15 < 0 && !tile.inActive() && tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
								{
									goto Block_20;
								}
								if (ignoreY != 1 && num15 > 0 && !tile2.inActive() && tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
								{
									goto Block_26;
								}
								if (!tile3.inActive() && tile3.active() && Main.tileSolid[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type])
								{
									goto Block_30;
								}
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num += num14;
							num12--;
							if (num12 == 0 && num13 == 0 && num16 == 1)
							{
								flag2 = true;
							}
						}
						if (num13 != 0)
						{
							num11 = 1;
						}
					}
					else if (num11 == 1)
					{
						num10 += num8;
						int num17 = (int)num10;
						num10 %= 1f;
						for (int j = 0; j < num17; j++)
						{
							if (Main.tile[num - 1, num3] == null)
							{
								goto Block_38;
							}
							if (Main.tile[num + 1, num3] == null)
							{
								goto Block_39;
							}
							Tile tile4 = Main.tile[num - 1, num3];
							Tile tile5 = Main.tile[num + 1, num3];
							Tile tile6 = Main.tile[num, num3];
							if (!ignoreTargets.Contains(new Tuple<int, int>(num, num3)) && !ignoreTargets.Contains(new Tuple<int, int>(num - 1, num3)) && !ignoreTargets.Contains(new Tuple<int, int>(num + 1, num3)))
							{
								if (ignoreX != -1 && num14 < 0 && !tile4.inActive() && tile4.active() && Main.tileSolid[(int)tile4.type] && !Main.tileSolidTop[(int)tile4.type])
								{
									goto Block_48;
								}
								if (ignoreX != 1 && num14 > 0 && !tile5.inActive() && tile5.active() && Main.tileSolid[(int)tile5.type] && !Main.tileSolidTop[(int)tile5.type])
								{
									goto Block_54;
								}
								if (!tile6.inActive() && tile6.active() && Main.tileSolid[(int)tile6.type] && !Main.tileSolidTop[(int)tile6.type])
								{
									goto Block_58;
								}
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num3 += num15;
							num13--;
							if (num12 == 0 && num13 == 0 && num17 == 1)
							{
								flag2 = true;
							}
						}
						if (num12 != 0)
						{
							num11 = 2;
						}
					}
					if (Main.tile[num, num3] == null)
					{
						goto Block_65;
					}
					Tile tile7 = Main.tile[num, num3];
					if (!ignoreTargets.Contains(new Tuple<int, int>(num, num3)) && !tile7.inActive() && tile7.active() && Main.tileSolid[(int)tile7.type] && !Main.tileSolidTop[(int)tile7.type])
					{
						goto Block_70;
					}
					if (flag || flag2)
					{
						goto Block_71;
					}
				}
				Block_10:
				col = new Tuple<int, int>(num, num3 - 1);
				return false;
				Block_11:
				col = new Tuple<int, int>(num, num3 + 1);
				return false;
				Block_20:
				col = new Tuple<int, int>(num, num3 - 1);
				return true;
				Block_26:
				col = new Tuple<int, int>(num, num3 + 1);
				return true;
				Block_30:
				col = new Tuple<int, int>(num, num3);
				return true;
				Block_38:
				col = new Tuple<int, int>(num - 1, num3);
				return false;
				Block_39:
				col = new Tuple<int, int>(num + 1, num3);
				return false;
				Block_48:
				col = new Tuple<int, int>(num - 1, num3);
				return true;
				Block_54:
				col = new Tuple<int, int>(num + 1, num3);
				return true;
				Block_58:
				col = new Tuple<int, int>(num, num3);
				return true;
				Block_65:
				col = new Tuple<int, int>(num, num3);
				return false;
				Block_70:
				col = new Tuple<int, int>(num, num3);
				return true;
				Block_71:
				col = new Tuple<int, int>(num, num3);
				result = true;
			}
			catch
			{
				col = new Tuple<int, int>(x1, y1);
				result = false;
			}
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0025C5BC File Offset: 0x0025A7BC
		public static Tuple<int, int> TupleHitLineWall(int x1, int y1, int x2, int y2)
		{
			int num = x1;
			int num2 = y1;
			int num3 = x2;
			int num4 = y2;
			if (num <= 1)
			{
				num = 1;
			}
			if (num >= Main.maxTilesX)
			{
				num = Main.maxTilesX - 1;
			}
			if (num3 <= 1)
			{
				num3 = 1;
			}
			if (num3 >= Main.maxTilesX)
			{
				num3 = Main.maxTilesX - 1;
			}
			if (num2 <= 1)
			{
				num2 = 1;
			}
			if (num2 >= Main.maxTilesY)
			{
				num2 = Main.maxTilesY - 1;
			}
			if (num4 <= 1)
			{
				num4 = 1;
			}
			if (num4 >= Main.maxTilesY)
			{
				num4 = Main.maxTilesY - 1;
			}
			float num5 = (float)Math.Abs(num - num3);
			float num6 = (float)Math.Abs(num2 - num4);
			if (num5 == 0f && num6 == 0f)
			{
				return new Tuple<int, int>(num, num2);
			}
			float num7 = 1f;
			float num8 = 1f;
			if (num5 == 0f || num6 == 0f)
			{
				if (num5 == 0f)
				{
					num7 = 0f;
				}
				if (num6 == 0f)
				{
					num8 = 0f;
				}
			}
			else if (num5 > num6)
			{
				num7 = num5 / num6;
			}
			else
			{
				num8 = num6 / num5;
			}
			float num9 = 0f;
			float num10 = 0f;
			int num11 = 1;
			if (num2 < num4)
			{
				num11 = 2;
			}
			int num12 = (int)num5;
			int num13 = (int)num6;
			int num14 = Math.Sign(num3 - num);
			int num15 = Math.Sign(num4 - num2);
			bool flag = false;
			bool flag2 = false;
			Tuple<int, int> result;
			try
			{
				for (;;)
				{
					if (num11 == 2)
					{
						num9 += num7;
						int num16 = (int)num9;
						num9 %= 1f;
						for (int i = 0; i < num16; i++)
						{
							Main.tile[num, num2];
							if (Collision.HitWallSubstep(num, num2))
							{
								goto Block_18;
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num += num14;
							num12--;
							if (num12 == 0 && num13 == 0 && num16 == 1)
							{
								flag2 = true;
							}
						}
						if (num13 != 0)
						{
							num11 = 1;
						}
					}
					else if (num11 == 1)
					{
						num10 += num8;
						int num17 = (int)num10;
						num10 %= 1f;
						for (int j = 0; j < num17; j++)
						{
							Main.tile[num, num2];
							if (Collision.HitWallSubstep(num, num2))
							{
								goto Block_26;
							}
							if (num12 == 0 && num13 == 0)
							{
								flag = true;
								break;
							}
							num2 += num15;
							num13--;
							if (num12 == 0 && num13 == 0 && num17 == 1)
							{
								flag2 = true;
							}
						}
						if (num12 != 0)
						{
							num11 = 2;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_33;
					}
					Main.tile[num, num2];
					if (Collision.HitWallSubstep(num, num2))
					{
						goto Block_34;
					}
					if (flag || flag2)
					{
						goto Block_35;
					}
				}
				Block_18:
				return new Tuple<int, int>(num, num2);
				Block_26:
				return new Tuple<int, int>(num, num2);
				Block_33:
				return new Tuple<int, int>(-1, -1);
				Block_34:
				return new Tuple<int, int>(num, num2);
				Block_35:
				result = new Tuple<int, int>(num, num2);
			}
			catch
			{
				result = new Tuple<int, int>(-1, -1);
			}
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0025C86C File Offset: 0x0025AA6C
		public static bool HitWallSubstep(int x, int y)
		{
			if (Main.tile[x, y].wall == 0)
			{
				return false;
			}
			bool flag = false;
			if (Main.wallHouse[(int)Main.tile[x, y].wall])
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						if ((i != 0 || j != 0) && Main.tile[x + i, y + j].wall == 0)
						{
							flag = true;
						}
					}
				}
			}
			if (Main.tile[x, y].active() && flag)
			{
				bool flag2 = true;
				for (int k = -1; k < 2; k++)
				{
					for (int l = -1; l < 2; l++)
					{
						if (k != 0 || l != 0)
						{
							Tile tile = Main.tile[x + k, y + l];
							if (!tile.active() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type])
							{
								flag2 = false;
							}
						}
					}
				}
				if (flag2)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0025C968 File Offset: 0x0025AB68
		public static bool EmptyTile(int i, int j, bool ignoreTiles = false)
		{
			Rectangle rectangle = new Rectangle(i * 16, j * 16, 16, 16);
			if (Main.tile[i, j].active() && !ignoreTiles)
			{
				return false;
			}
			for (int k = 0; k < 255; k++)
			{
				if (Main.player[k].active && !Main.player[k].dead && !Main.player[k].ghost && rectangle.Intersects(new Rectangle((int)Main.player[k].position.X, (int)Main.player[k].position.Y, Main.player[k].width, Main.player[k].height)))
				{
					return false;
				}
			}
			for (int l = 0; l < 200; l++)
			{
				if (Main.npc[l].active && rectangle.Intersects(new Rectangle((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0025CA94 File Offset: 0x0025AC94
		public static bool DrownCollision(Vector2 Position, int Width, int Height, float gravDir = -1f, bool includeSlopes = false)
		{
			Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
			int num = 10;
			int num2 = 12;
			if (num > Width)
			{
				num = Width;
			}
			if (num2 > Height)
			{
				num2 = Height;
			}
			vector = new Vector2(vector.X - (float)(num / 2), Position.Y + -2f);
			if (gravDir == -1f)
			{
				vector.Y += (float)(Height / 2 - 6);
			}
			int value = (int)(Position.X / 16f) - 1;
			int num3 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num4 = (int)(Position.Y / 16f) - 1;
			int num5 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num6 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesX - 1);
			num4 = Utils.Clamp<int>(num4, 0, Main.maxTilesY - 1);
			num5 = Utils.Clamp<int>(num5, 0, Main.maxTilesY - 1);
			int num7 = (gravDir == 1f) ? num4 : (num5 - 1);
			for (int i = num6; i < num3; i++)
			{
				for (int j = num4; j < num5; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.liquid > 0 && !tile.lava() && !tile.shimmer() && (j != num7 || !tile.active() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type] || (includeSlopes && tile.blockType() != 0)))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num8 = 16;
						float num9 = (float)(256 - (int)Main.tile[i, j].liquid);
						num9 /= 32f;
						vector2.Y += num9 * 2f;
						num8 -= (int)(num9 * 2f);
						if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num8)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0025CCFC File Offset: 0x0025AEFC
		public static bool IsWorldPointSolid(Vector2 pos, bool treatPlatformsAsNonSolid = false)
		{
			Point point = pos.ToTileCoordinates();
			if (!WorldGen.InWorld(point.X, point.Y, 1))
			{
				return false;
			}
			Tile tile = Main.tile[point.X, point.Y];
			if (tile == null || !tile.active() || tile.inActive() || !Main.tileSolid[(int)tile.type])
			{
				return false;
			}
			if (treatPlatformsAsNonSolid && tile.type > 0 && tile.type <= TileID.Count && (TileID.Sets.Platforms[(int)tile.type] || tile.type == 380))
			{
				return false;
			}
			int num = tile.blockType();
			switch (num)
			{
			case 0:
				return pos.X >= (float)(point.X * 16) && pos.X <= (float)(point.X * 16 + 16) && pos.Y >= (float)(point.Y * 16) && pos.Y <= (float)(point.Y * 16 + 16);
			case 1:
				return pos.X >= (float)(point.X * 16) && pos.X <= (float)(point.X * 16 + 16) && pos.Y >= (float)(point.Y * 16 + 8) && pos.Y <= (float)(point.Y * 16 + 16);
			case 2:
			case 3:
			case 4:
			case 5:
			{
				if (pos.X < (float)(point.X * 16) && pos.X > (float)(point.X * 16 + 16) && pos.Y < (float)(point.Y * 16) && pos.Y > (float)(point.Y * 16 + 16))
				{
					return false;
				}
				float num2 = pos.X % 16f;
				float num3 = pos.Y % 16f;
				switch (num)
				{
				case 2:
					return num3 >= num2;
				case 3:
					return num2 + num3 >= 16f;
				case 4:
					return num2 + num3 <= 16f;
				case 5:
					return num3 <= num2;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0025CF20 File Offset: 0x0025B120
		public static bool GetWaterLine(Point pt, out float waterLineHeight)
		{
			return Collision.GetWaterLine(pt.X, pt.Y, out waterLineHeight);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0025CF34 File Offset: 0x0025B134
		public static bool GetWaterLine(int X, int Y, out float waterLineHeight)
		{
			waterLineHeight = 0f;
			if (Main.tile[X, Y - 2] == null)
			{
				Main.tile[X, Y - 2] = new Tile();
			}
			if (Main.tile[X, Y - 1] == null)
			{
				Main.tile[X, Y - 1] = new Tile();
			}
			if (Main.tile[X, Y] == null)
			{
				Main.tile[X, Y] = new Tile();
			}
			if (Main.tile[X, Y + 1] == null)
			{
				Main.tile[X, Y + 1] = new Tile();
			}
			if (Main.tile[X, Y - 2].liquid > 0)
			{
				return false;
			}
			if (Main.tile[X, Y - 1].liquid > 0)
			{
				waterLineHeight = (float)(Y * 16);
				waterLineHeight -= (float)(Main.tile[X, Y - 1].liquid / 16);
				return true;
			}
			if (Main.tile[X, Y].liquid > 0)
			{
				waterLineHeight = (float)((Y + 1) * 16);
				waterLineHeight -= (float)(Main.tile[X, Y].liquid / 16);
				return true;
			}
			if (Main.tile[X, Y + 1].liquid > 0)
			{
				waterLineHeight = (float)((Y + 2) * 16);
				waterLineHeight -= (float)(Main.tile[X, Y + 1].liquid / 16);
				return true;
			}
			return false;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0025D09A File Offset: 0x0025B29A
		public static bool GetWaterLineIterate(Point pt, out float waterLineHeight)
		{
			return Collision.GetWaterLineIterate(pt.X, pt.Y, out waterLineHeight);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0025D0B0 File Offset: 0x0025B2B0
		public static bool GetWaterLineIterate(int X, int Y, out float waterLineHeight)
		{
			waterLineHeight = 0f;
			while (Y > 0 && Framing.GetTileSafely(X, Y).liquid > 0)
			{
				Y--;
			}
			Y++;
			if (Main.tile[X, Y] == null)
			{
				Main.tile[X, Y] = new Tile();
			}
			if (Main.tile[X, Y].liquid > 0)
			{
				waterLineHeight = (float)(Y * 16);
				waterLineHeight -= (float)(Main.tile[X, Y - 1].liquid / 16);
				return true;
			}
			return false;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0025D13C File Offset: 0x0025B33C
		public static bool WetCollision(Vector2 Position, int Width, int Height)
		{
			Collision.honey = false;
			Collision.shimmer = false;
			Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
			int num = 10;
			int num2 = Height / 2;
			if (num > Width)
			{
				num = Width;
			}
			if (num2 > Height)
			{
				num2 = Height;
			}
			vector = new Vector2(vector.X - (float)(num / 2), vector.Y - (float)(num2 / 2));
			int value = (int)(Position.X / 16f) - 1;
			int num3 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num4 = (int)(Position.Y / 16f) - 1;
			int num5 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num6 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesX - 1);
			num4 = Utils.Clamp<int>(num4, 0, Main.maxTilesY - 1);
			num5 = Utils.Clamp<int>(num5, 0, Main.maxTilesY - 1);
			for (int i = num6; i < num3; i++)
			{
				for (int j = num4; j < num5; j++)
				{
					if (Main.tile[i, j] != null)
					{
						if (Main.tile[i, j].liquid > 0)
						{
							Vector2 vector2;
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							int num7 = 16;
							float num8 = (float)(256 - (int)Main.tile[i, j].liquid);
							num8 /= 32f;
							vector2.Y += num8 * 2f;
							num7 -= (int)(num8 * 2f);
							if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num7)
							{
								if (Main.tile[i, j].honey())
								{
									Collision.honey = true;
								}
								if (Main.tile[i, j].shimmer())
								{
									Collision.shimmer = true;
								}
								return true;
							}
						}
						else if (Main.tile[i, j].active() && Main.tile[i, j].slope() != 0 && j > 0 && Main.tile[i, j - 1] != null && Main.tile[i, j - 1].liquid > 0)
						{
							Vector2 vector2;
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							int num9 = 16;
							if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num9)
							{
								if (Main.tile[i, j - 1].honey())
								{
									Collision.honey = true;
								}
								else if (Main.tile[i, j - 1].shimmer())
								{
									Collision.shimmer = true;
								}
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0025D484 File Offset: 0x0025B684
		public static bool LavaCollision(Vector2 Position, int Width, int Height)
		{
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			for (int i = num4; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].liquid > 0 && Main.tile[i, j].lava())
					{
						Vector2 vector;
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num5 = 16;
						float num6 = (float)(256 - (int)Main.tile[i, j].liquid);
						num6 /= 32f;
						vector.Y += num6 * 2f;
						num5 -= (int)(num6 * 2f);
						if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num5)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0025D638 File Offset: 0x0025B838
		public static Vector4 WalkDownSlope(Vector2 Position, Vector2 Velocity, int Width, int Height, float gravity = 0f)
		{
			if (Velocity.Y != gravity)
			{
				return new Vector4(Position, Velocity.X, Velocity.Y);
			}
			int num = (int)(Position.X / 16f);
			int num2 = (int)((Position.X + (float)Width) / 16f);
			int num3 = (int)((Position.Y + (float)Height + 4f) / 16f);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesX - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 3);
			float num4 = (float)((num3 + 3) * 16);
			int num5 = -1;
			int num6 = -1;
			int num7 = 1;
			if (Velocity.X < 0f)
			{
				num7 = 2;
			}
			for (int i = num; i <= num2; i++)
			{
				for (int j = num3; j <= num3 + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type]))
					{
						int num8 = j * 16;
						if (Main.tile[i, j].halfBrick())
						{
							num8 += 8;
						}
						Rectangle rectangle = new Rectangle(i * 16, j * 16 - 17, 16, 16);
						if (rectangle.Intersects(new Rectangle((int)Position.X, (int)Position.Y, Width, Height)) && (float)num8 <= num4)
						{
							if (num4 == (float)num8)
							{
								if (Main.tile[i, j].slope() != 0)
								{
									if (num5 != -1 && num6 != -1 && Main.tile[num5, num6] != null && Main.tile[num5, num6].slope() != 0)
									{
										if ((int)Main.tile[i, j].slope() == num7)
										{
											num4 = (float)num8;
											num5 = i;
											num6 = j;
										}
									}
									else
									{
										num4 = (float)num8;
										num5 = i;
										num6 = j;
									}
								}
							}
							else
							{
								num4 = (float)num8;
								num5 = i;
								num6 = j;
							}
						}
					}
				}
			}
			int num9 = num5;
			int num10 = num6;
			if (num5 != -1 && num6 != -1 && Main.tile[num9, num10] != null && Main.tile[num9, num10].slope() > 0)
			{
				int num11 = (int)Main.tile[num9, num10].slope();
				Vector2 vector;
				vector.X = (float)(num9 * 16);
				vector.Y = (float)(num10 * 16);
				if (num11 == 2)
				{
					float num12 = vector.X + 16f - (Position.X + (float)Width);
					if (Position.Y + (float)Height >= vector.Y + num12 && Velocity.X < 0f)
					{
						Velocity.Y += Math.Abs(Velocity.X);
					}
				}
				else if (num11 == 1)
				{
					float num12 = Position.X - vector.X;
					if (Position.Y + (float)Height >= vector.Y + num12 && Velocity.X > 0f)
					{
						Velocity.Y += Math.Abs(Velocity.X);
					}
				}
			}
			return new Vector4(Position, Velocity.X, Velocity.Y);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0025D9A0 File Offset: 0x0025BBA0
		public static Vector4 SlopeCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, float gravity = 0f, bool fall = false)
		{
			Collision.stair = false;
			Collision.stairFall = false;
			bool[] array = new bool[5];
			float y = Position.Y;
			float y2 = Position.Y;
			Collision.sloping = false;
			Vector2 vector = Position;
			Vector2 vector2 = Velocity;
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			for (int i = num4; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].active() && !Main.tile[i, j].inActive() && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
					{
						Vector2 vector3;
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num5 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector3.Y += 8f;
							num5 -= 8;
						}
						if (Position.X + (float)Width > vector3.X && Position.X < vector3.X + 16f && Position.Y + (float)Height > vector3.Y && Position.Y < vector3.Y + (float)num5)
						{
							bool flag = true;
							if (TileID.Sets.Platforms[(int)Main.tile[i, j].type])
							{
								if (Velocity.Y < 0f)
								{
									flag = false;
								}
								if (Position.Y + (float)Height < (float)(j * 16) || Position.Y + (float)Height - (1f + Math.Abs(Velocity.X)) > (float)(j * 16 + 16))
								{
									flag = false;
								}
								if (((Main.tile[i, j].slope() == 1 && Velocity.X >= 0f) || (Main.tile[i, j].slope() == 2 && Velocity.X <= 0f)) && (Position.Y + (float)Height) / 16f - 1f == (float)j)
								{
									flag = false;
								}
							}
							if (flag)
							{
								bool flag2 = false;
								if (fall && TileID.Sets.Platforms[(int)Main.tile[i, j].type])
								{
									flag2 = true;
								}
								int num6 = (int)Main.tile[i, j].slope();
								vector3.X = (float)(i * 16);
								vector3.Y = (float)(j * 16);
								if (Position.X + (float)Width > vector3.X && Position.X < vector3.X + 16f && Position.Y + (float)Height > vector3.Y && Position.Y < vector3.Y + 16f)
								{
									float num7 = 0f;
									if (num6 == 3 || num6 == 4)
									{
										if (num6 == 3)
										{
											num7 = Position.X - vector3.X;
										}
										if (num6 == 4)
										{
											num7 = vector3.X + 16f - (Position.X + (float)Width);
										}
										if (num7 >= 0f)
										{
											if (Position.Y <= vector3.Y + 16f - num7)
											{
												float num8 = vector3.Y + 16f - Position.Y - num7;
												if (Position.Y + num8 > y2)
												{
													vector.Y = Position.Y + num8;
													y2 = vector.Y;
													if (vector2.Y < 0.0101f)
													{
														vector2.Y = 0.0101f;
													}
													array[num6] = true;
												}
											}
										}
										else if (Position.Y > vector3.Y)
										{
											float num9 = vector3.Y + 16f;
											if (vector.Y < num9)
											{
												vector.Y = num9;
												if (vector2.Y < 0.0101f)
												{
													vector2.Y = 0.0101f;
												}
											}
										}
									}
									if (num6 == 1 || num6 == 2)
									{
										if (num6 == 1)
										{
											num7 = Position.X - vector3.X;
										}
										if (num6 == 2)
										{
											num7 = vector3.X + 16f - (Position.X + (float)Width);
										}
										if (num7 >= 0f)
										{
											if (Position.Y + (float)Height >= vector3.Y + num7)
											{
												float num10 = vector3.Y - (Position.Y + (float)Height) + num7;
												if (Position.Y + num10 < y)
												{
													if (flag2)
													{
														Collision.stairFall = true;
													}
													else
													{
														if (TileID.Sets.Platforms[(int)Main.tile[i, j].type])
														{
															Collision.stair = true;
														}
														else
														{
															Collision.stair = false;
														}
														vector.Y = Position.Y + num10;
														y = vector.Y;
														if (vector2.Y > 0f)
														{
															vector2.Y = 0f;
														}
														array[num6] = true;
													}
												}
											}
										}
										else if (TileID.Sets.Platforms[(int)Main.tile[i, j].type] && Position.Y + (float)Height - 4f - Math.Abs(Velocity.X) > vector3.Y)
										{
											if (flag2)
											{
												Collision.stairFall = true;
											}
										}
										else
										{
											float num11 = vector3.Y - (float)Height;
											if (vector.Y > num11)
											{
												if (flag2)
												{
													Collision.stairFall = true;
												}
												else
												{
													if (TileID.Sets.Platforms[(int)Main.tile[i, j].type])
													{
														Collision.stair = true;
													}
													else
													{
														Collision.stair = false;
													}
													vector.Y = num11;
													if (vector2.Y > 0f)
													{
														vector2.Y = 0f;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			Vector2 vector4 = vector - Position;
			Vector2 vector5 = Collision.TileCollision(Position, vector4, Width, Height, false, false, 1);
			if (vector5.Y > vector4.Y)
			{
				float num12 = vector4.Y - vector5.Y;
				vector.Y = Position.Y + vector5.Y;
				if (array[1])
				{
					vector.X = Position.X - num12;
				}
				if (array[2])
				{
					vector.X = Position.X + num12;
				}
				vector2.X = 0f;
				vector2.Y = 0f;
				Collision.up = false;
			}
			else if (vector5.Y < vector4.Y)
			{
				float num13 = vector5.Y - vector4.Y;
				vector.Y = Position.Y + vector5.Y;
				if (array[3])
				{
					vector.X = Position.X - num13;
				}
				if (array[4])
				{
					vector.X = Position.X + num13;
				}
				vector2.X = 0f;
				vector2.Y = 0f;
			}
			return new Vector4(vector, vector2.X, vector2.Y);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0025E140 File Offset: 0x0025C340
		public static Vector2 noSlopeCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			float num9 = (float)((num3 + 3) * 16);
			for (int i = num8; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].active() && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num10 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector2.Y += 8f;
							num10 -= 8;
						}
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num10)
						{
							if (Position.Y + (float)Height <= vector2.Y)
							{
								Collision.down = true;
								if ((!Main.tileSolidTop[(int)Main.tile[i, j].type] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num9 > vector2.Y)
								{
									num6 = i;
									num7 = j;
									if (num10 < 16)
									{
										num7++;
									}
									if (num6 != num4)
									{
										result.Y = vector2.Y - (Position.Y + (float)Height);
										num9 = vector2.Y;
									}
								}
							}
							else if (Position.X + (float)Width <= vector2.X && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								num4 = i;
								num5 = j;
								if (num5 != num7)
								{
									result.X = vector2.X - (Position.X + (float)Width);
								}
								if (num6 == num4)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector2.X + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								num4 = i;
								num5 = j;
								if (num5 != num7)
								{
									result.X = vector2.X + 16f - Position.X;
								}
								if (num6 == num4)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector2.Y + (float)num10 && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								Collision.up = true;
								num6 = i;
								num7 = j;
								result.Y = vector2.Y + (float)num10 - Position.Y + 0.01f;
								if (num7 == num5)
								{
									result.X = Velocity.X;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0025E538 File Offset: 0x0025C738
		public static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			float num9 = (float)((num3 + 3) * 16);
			for (int i = num8; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].active() && !Main.tile[i, j].inActive() && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num10 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector2.Y += 8f;
							num10 -= 8;
						}
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num10)
						{
							bool flag = false;
							bool flag2 = false;
							if (Main.tile[i, j].slope() > 2)
							{
								if (Main.tile[i, j].slope() == 3 && Position.Y + Math.Abs(Velocity.X) >= vector2.Y && Position.X >= vector2.X)
								{
									flag2 = true;
								}
								if (Main.tile[i, j].slope() == 4 && Position.Y + Math.Abs(Velocity.X) >= vector2.Y && Position.X + (float)Width <= vector2.X + 16f)
								{
									flag2 = true;
								}
							}
							else if (Main.tile[i, j].slope() > 0)
							{
								flag = true;
								if (Main.tile[i, j].slope() == 1 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector2.Y + (float)num10 && Position.X >= vector2.X)
								{
									flag2 = true;
								}
								if (Main.tile[i, j].slope() == 2 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector2.Y + (float)num10 && Position.X + (float)Width <= vector2.X + 16f)
								{
									flag2 = true;
								}
							}
							if (!flag2)
							{
								if (Position.Y + (float)Height <= vector2.Y)
								{
									Collision.down = true;
									if ((!Main.tileSolidTop[(int)Main.tile[i, j].type] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num9 > vector2.Y)
									{
										num6 = i;
										num7 = j;
										if (num10 < 16)
										{
											num7++;
										}
										if (num6 != num4 && !flag)
										{
											result.Y = vector2.Y - (Position.Y + (float)Height) + ((gravDir == -1) ? -0.01f : 0f);
											num9 = vector2.Y;
										}
									}
								}
								else if (Position.X + (float)Width <= vector2.X && !Main.tileSolidTop[(int)Main.tile[i, j].type])
								{
									if (i >= 1 && Main.tile[i - 1, j] == null)
									{
										Main.tile[i - 1, j] = new Tile();
									}
									if (i < 1 || (Main.tile[i - 1, j].slope() != 2 && Main.tile[i - 1, j].slope() != 4))
									{
										num4 = i;
										num5 = j;
										if (num5 != num7)
										{
											result.X = vector2.X - (Position.X + (float)Width);
										}
										if (num6 == num4)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.X >= vector2.X + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
								{
									if (Main.tile[i + 1, j] == null)
									{
										Main.tile[i + 1, j] = new Tile();
									}
									if (Main.tile[i + 1, j].slope() != 1 && Main.tile[i + 1, j].slope() != 3)
									{
										num4 = i;
										num5 = j;
										if (num5 != num7)
										{
											result.X = vector2.X + 16f - Position.X;
										}
										if (num6 == num4)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.Y >= vector2.Y + (float)num10 && !Main.tileSolidTop[(int)Main.tile[i, j].type])
								{
									Collision.up = true;
									num6 = i;
									num7 = j;
									result.Y = vector2.Y + (float)num10 - Position.Y + ((gravDir == 1) ? 0.01f : 0f);
									if (num7 == num5)
									{
										result.X = Velocity.X;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0025EBBC File Offset: 0x0025CDBC
		public static bool IsClearSpotTest(Vector2 position, float testMagnitude, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1, bool checkCardinals = true, bool checkSlopes = false)
		{
			if (checkCardinals)
			{
				Vector2 vector = Vector2.UnitX * testMagnitude;
				if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
				{
					return false;
				}
				vector = -Vector2.UnitX * testMagnitude;
				if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
				{
					return false;
				}
				vector = Vector2.UnitY * testMagnitude;
				if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
				{
					return false;
				}
				vector = -Vector2.UnitY * testMagnitude;
				if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
				{
					return false;
				}
			}
			if (checkSlopes)
			{
				Vector2 vector = Vector2.UnitX * testMagnitude;
				Vector4 value = new Vector4(position, testMagnitude, 0f);
				if (Collision.SlopeCollision(position, vector, Width, Height, (float)gravDir, fallThrough) != value)
				{
					return false;
				}
				vector = -Vector2.UnitX * testMagnitude;
				value = new Vector4(position, -testMagnitude, 0f);
				if (Collision.SlopeCollision(position, vector, Width, Height, (float)gravDir, fallThrough) != value)
				{
					return false;
				}
				vector = Vector2.UnitY * testMagnitude;
				value = new Vector4(position, 0f, testMagnitude);
				if (Collision.SlopeCollision(position, vector, Width, Height, (float)gravDir, fallThrough) != value)
				{
					return false;
				}
				vector = -Vector2.UnitY * testMagnitude;
				value = new Vector4(position, 0f, -testMagnitude);
				if (Collision.SlopeCollision(position, vector, Width, Height, (float)gravDir, fallThrough) != value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0025ED64 File Offset: 0x0025CF64
		public static List<Point> FindCollisionTile(int Direction, Vector2 position, float testMagnitude, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1, bool checkCardinals = true, bool checkSlopes = false)
		{
			List<Point> list = new List<Point>();
			if (Direction > 1)
			{
				if (Direction - 2 <= 1)
				{
					Vector2 vector = (Direction == 2) ? (Vector2.UnitY * testMagnitude) : (-Vector2.UnitY * testMagnitude);
					Vector4 vec = new Vector4(position, vector.X, vector.Y);
					int num = (int)(position.Y + (float)((Direction == 2) ? Height : 0)) / 16;
					float num2 = Math.Min(16f - position.X % 16f, (float)Width);
					float num3 = num2;
					if (checkCardinals && Collision.TileCollision(position - vector, vector, (int)num2, Height, fallThrough, fall2, gravDir) != vector)
					{
						list.Add(new Point((int)position.X / 16, num));
					}
					else if (checkSlopes && Collision.SlopeCollision(position, vector, (int)num2, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
					{
						list.Add(new Point((int)position.X / 16, num));
					}
					while (num3 + 16f <= (float)(Width - 16))
					{
						if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitX * num3, vector, 16, Height, fallThrough, fall2, gravDir) != vector)
						{
							list.Add(new Point((int)(position.X + num3) / 16, num));
						}
						else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitX * num3, vector, 16, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
						{
							list.Add(new Point((int)(position.X + num3) / 16, num));
						}
						num3 += 16f;
					}
					int width = Width - (int)num3;
					if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitX * num3, vector, width, Height, fallThrough, fall2, gravDir) != vector)
					{
						list.Add(new Point((int)(position.X + num3) / 16, num));
					}
					else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitX * num3, vector, width, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
					{
						list.Add(new Point((int)(position.X + num3) / 16, num));
					}
				}
			}
			else
			{
				Vector2 vector = (Direction == 0) ? (Vector2.UnitX * testMagnitude) : (-Vector2.UnitX * testMagnitude);
				Vector4 vec = new Vector4(position, vector.X, vector.Y);
				int num = (int)(position.X + (float)((Direction == 0) ? Width : 0)) / 16;
				float num4 = Math.Min(16f - position.Y % 16f, (float)Height);
				float num5 = num4;
				if (checkCardinals && Collision.TileCollision(position - vector, vector, Width, (int)num4, fallThrough, fall2, gravDir) != vector)
				{
					list.Add(new Point(num, (int)position.Y / 16));
				}
				else if (checkSlopes && Collision.SlopeCollision(position, vector, Width, (int)num4, (float)gravDir, fallThrough).XZW() != vec.XZW())
				{
					list.Add(new Point(num, (int)position.Y / 16));
				}
				while (num5 + 16f <= (float)(Height - 16))
				{
					if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitY * num5, vector, Width, 16, fallThrough, fall2, gravDir) != vector)
					{
						list.Add(new Point(num, (int)(position.Y + num5) / 16));
					}
					else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitY * num5, vector, Width, 16, (float)gravDir, fallThrough).XZW() != vec.XZW())
					{
						list.Add(new Point(num, (int)(position.Y + num5) / 16));
					}
					num5 += 16f;
				}
				int height = Height - (int)num5;
				if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitY * num5, vector, Width, height, fallThrough, fall2, gravDir) != vector)
				{
					list.Add(new Point(num, (int)(position.Y + num5) / 16));
				}
				else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitY * num5, vector, Width, height, (float)gravDir, fallThrough).XZW() != vec.XZW())
				{
					list.Add(new Point(num, (int)(position.Y + num5) / 16));
				}
			}
			return list;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0025F238 File Offset: 0x0025D438
		public static bool FindCollisionDirection(out int Direction, Vector2 position, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Vector2 vector = Vector2.UnitX * 16f;
			if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
			{
				Direction = 0;
				return true;
			}
			vector = -Vector2.UnitX * 16f;
			if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
			{
				Direction = 1;
				return true;
			}
			vector = Vector2.UnitY * 16f;
			if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
			{
				Direction = 2;
				return true;
			}
			vector = -Vector2.UnitY * 16f;
			if (Collision.TileCollision(position - vector, vector, Width, Height, fallThrough, fall2, gravDir) != vector)
			{
				Direction = 3;
				return true;
			}
			Direction = -1;
			return false;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0025F31C File Offset: 0x0025D51C
		public static bool SolidCollision(Vector2 Position, int Width, int Height)
		{
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			for (int i = num4; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && !Main.tile[i, j].inActive() && Main.tile[i, j].active() && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
					{
						Vector2 vector;
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num5 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector.Y += 8f;
							num5 -= 8;
						}
						if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num5)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0025F4E8 File Offset: 0x0025D6E8
		public static bool SolidCollision(Vector2 Position, int Width, int Height, bool acceptTopSurfaces)
		{
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			for (int i = num4; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && !tile.inActive())
					{
						bool flag = Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type];
						if (acceptTopSurfaces)
						{
							flag |= (Main.tileSolidTop[(int)tile.type] && tile.frameY == 0);
						}
						if (flag)
						{
							Vector2 vector;
							vector.X = (float)(i * 16);
							vector.Y = (float)(j * 16);
							int num5 = 16;
							if (tile.halfBrick())
							{
								vector.Y += 8f;
								num5 -= 8;
							}
							if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num5)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0025F6A8 File Offset: 0x0025D8A8
		public static Vector2 WaterCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, bool lavaWalk = true)
		{
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			for (int i = num4; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].liquid > 0 && Main.tile[i, j - 1].liquid == 0 && (!Main.tile[i, j].lava() || lavaWalk))
					{
						int num5 = (int)(Main.tile[i, j].liquid / 32 * 2 + 2);
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16 + 16 - num5);
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num5 && Position.Y + (float)Height <= vector2.Y && !fallThrough)
						{
							result.Y = vector2.Y - (Position.Y + (float)Height);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0025F88C File Offset: 0x0025DA8C
		public static Vector2 AnyCollisionWithSpecificTiles(Vector2 Position, Vector2 Velocity, int Width, int Height, bool[] tilesWeCanCollideWithByType, bool evenActuated = false)
		{
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && (evenActuated || !tile.inActive()) && tilesWeCanCollideWithByType[(int)tile.type])
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num9 = 16;
						if (tile.halfBrick())
						{
							vector2.Y += 8f;
							num9 -= 8;
						}
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num9)
						{
							if (Position.Y + (float)Height <= vector2.Y)
							{
								num7 = i;
								num8 = j;
								if (num7 != num5)
								{
									result.Y = vector2.Y - (Position.Y + (float)Height);
								}
							}
							else if (Position.X + (float)Width <= vector2.X && !Main.tileSolidTop[(int)tile.type])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector2.X - (Position.X + (float)Width);
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector2.X + 16f && !Main.tileSolidTop[(int)tile.type])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector2.X + 16f - Position.X;
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector2.Y + (float)num9 && !Main.tileSolidTop[(int)tile.type])
							{
								num7 = i;
								num8 = j;
								result.Y = vector2.Y + (float)num9 - Position.Y + 0.01f;
								if (num8 == num6)
								{
									result.X = Velocity.X + 0.01f;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0025FB98 File Offset: 0x0025DD98
		public static Vector2 AnyCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool evenActuated = false)
		{
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].active() && (evenActuated || !Main.tile[i, j].inActive()))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num9 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector2.Y += 8f;
							num9 -= 8;
						}
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num9)
						{
							if (Position.Y + (float)Height <= vector2.Y)
							{
								num7 = i;
								num8 = j;
								if (num7 != num5)
								{
									result.Y = vector2.Y - (Position.Y + (float)Height);
								}
							}
							else if (Position.X + (float)Width <= vector2.X && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector2.X - (Position.X + (float)Width);
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector2.X + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector2.X + 16f - Position.X;
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector2.Y + (float)num9 && !Main.tileSolidTop[(int)Main.tile[i, j].type])
							{
								num7 = i;
								num8 = j;
								result.Y = vector2.Y + (float)num9 - Position.Y + 0.01f;
								if (num8 == num6)
								{
									result.X = Velocity.X + 0.01f;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0025FEDC File Offset: 0x0025E0DC
		public static void HitTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
		{
			Vector2 vector = Position + Velocity;
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && !Main.tile[i, j].inActive() && Main.tile[i, j].active() && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num5 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector2.Y += 8f;
							num5 -= 8;
						}
						if (vector.X + (float)Width >= vector2.X && vector.X <= vector2.X + 16f && vector.Y + (float)Height >= vector2.Y && vector.Y <= vector2.Y + (float)num5)
						{
							WorldGen.KillTile(i, j, true, true, false);
						}
					}
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x002600C4 File Offset: 0x0025E2C4
		public static bool AnyHurtingTiles(Vector2 Position, int Width, int Height)
		{
			return Collision.HurtTiles(Position, Width, Height, null).type >= 0;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x002600DC File Offset: 0x0025E2DC
		public static Collision.HurtTile HurtTiles(Vector2 Position, int Width, int Height, Player player)
		{
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && !tile.inActive() && tile.active())
					{
						Vector2 vector;
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num5 = 16;
						if (tile.halfBrick())
						{
							vector.Y += 8f;
							num5 -= 8;
						}
						int num6 = 0;
						if (TileID.Sets.Suffocate[(int)tile.type])
						{
							num6 = 2;
						}
						if (Position.X + (float)Width - (float)num6 >= vector.X && Position.X + (float)num6 <= vector.X + 16f && Position.Y + (float)Height - (float)num6 >= vector.Y - 0.5f && Position.Y + (float)num6 <= vector.Y + (float)num5 + 0.5f && Collision.CanTileHurt(tile.type, i, j, player))
						{
							if (tile.slope() > 0)
							{
								if (num6 > 0)
								{
									goto IL_259;
								}
								int num7 = 0;
								if (tile.rightSlope() && Position.X > vector.X)
								{
									num7++;
								}
								if (tile.leftSlope() && Position.X + (float)Width < vector.X + 16f)
								{
									num7++;
								}
								if (tile.bottomSlope() && Position.Y > vector.Y)
								{
									num7++;
								}
								if (tile.topSlope() && Position.Y + (float)Height < vector.Y + (float)num5)
								{
									num7++;
								}
								if (num7 == 2)
								{
									goto IL_259;
								}
							}
							return new Collision.HurtTile
							{
								type = (int)tile.type,
								x = i,
								y = j
							};
						}
					}
					IL_259:;
				}
			}
			return new Collision.HurtTile
			{
				type = -1
			};
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00260370 File Offset: 0x0025E570
		public static bool CanTileHurt(ushort type, int i, int j, Player player)
		{
			return (type != 230 || Main.getGoodWorld) && (type != 80 || Main.dontStarveWorld) && (TileID.Sets.TouchDamageBleeding[(int)type] || TileID.Sets.Suffocate[(int)type] || TileID.Sets.TouchDamageImmediate[(int)type] > 0 || (TileID.Sets.TouchDamageHot[(int)type] && (player == null || !player.fireWalk)));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x002603D4 File Offset: 0x0025E5D4
		public static bool SwitchTiles(Vector2 Position, int Width, int Height, Vector2 oldPosition, int objType)
		{
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null)
					{
						int type = (int)Main.tile[i, j].type;
						if (Main.tile[i, j].active() && (type == 135 || type == 210 || type == 443 || type == 442))
						{
							Vector2 vector;
							vector.X = (float)(i * 16);
							vector.Y = (float)(j * 16 + 12);
							bool flag = false;
							if (type == 442)
							{
								if (objType == 4)
								{
									float r1StartX = 0f;
									float r1StartY = 0f;
									float r1Width = 0f;
									float r1Height = 0f;
									switch (Main.tile[i, j].frameX / 22)
									{
									case 0:
										r1StartX = (float)(i * 16);
										r1StartY = (float)(j * 16 + 16 - 10);
										r1Width = 16f;
										r1Height = 10f;
										break;
									case 1:
										r1StartX = (float)(i * 16);
										r1StartY = (float)(j * 16);
										r1Width = 16f;
										r1Height = 10f;
										break;
									case 2:
										r1StartX = (float)(i * 16);
										r1StartY = (float)(j * 16);
										r1Width = 10f;
										r1Height = 16f;
										break;
									case 3:
										r1StartX = (float)(i * 16 + 16 - 10);
										r1StartY = (float)(j * 16);
										r1Width = 10f;
										r1Height = 16f;
										break;
									}
									if (Utils.FloatIntersect(r1StartX, r1StartY, r1Width, r1Height, Position.X, Position.Y, (float)Width, (float)Height) && !Utils.FloatIntersect(r1StartX, r1StartY, r1Width, r1Height, oldPosition.X, oldPosition.Y, (float)Width, (float)Height))
									{
										Wiring.HitSwitch(i, j);
										NetMessage.SendData(59, -1, -1, null, i, (float)j, 0f, 0f, 0, 0, 0);
										return true;
									}
								}
								flag = true;
							}
							if (!flag && Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && (double)Position.Y < (double)vector.Y + 4.01)
							{
								if (type == 210)
								{
									WorldGen.ExplodeMine(i, j, false);
								}
								else if (oldPosition.X + (float)Width <= vector.X || oldPosition.X >= vector.X + 16f || oldPosition.Y + (float)Height <= vector.Y || (double)oldPosition.Y >= (double)vector.Y + 16.01)
								{
									if (type == 443)
									{
										if (objType == 1)
										{
											Wiring.HitSwitch(i, j);
											NetMessage.SendData(59, -1, -1, null, i, (float)j, 0f, 0f, 0, 0, 0);
										}
									}
									else
									{
										int num5 = (int)(Main.tile[i, j].frameY / 18);
										bool flag2 = true;
										if ((num5 == 4 || num5 == 2 || num5 == 3 || num5 == 6 || num5 == 7) && objType != 1)
										{
											flag2 = false;
										}
										if (num5 == 5 && (objType == 1 || objType == 4))
										{
											flag2 = false;
										}
										if (flag2)
										{
											Wiring.HitSwitch(i, j);
											NetMessage.SendData(59, -1, -1, null, i, (float)j, 0f, 0f, 0, 0, 0);
											if (num5 == 7)
											{
												WorldGen.KillTile(i, j, false, false, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
												}
											}
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00260814 File Offset: 0x0025EA14
		public bool SwitchTilesNew(Vector2 Position, int Width, int Height, Vector2 oldPosition, int objType)
		{
			Point point = Position.ToTileCoordinates();
			Point point2 = (Position + new Vector2((float)Width, (float)Height)).ToTileCoordinates();
			int num = Utils.Clamp<int>(point.X, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(point.Y, 0, Main.maxTilesY - 1);
			int num3 = Utils.Clamp<int>(point2.X, 0, Main.maxTilesX - 1);
			int num4 = Utils.Clamp<int>(point2.Y, 0, Main.maxTilesY - 1);
			for (int i = num; i <= num3; i++)
			{
				for (int j = num2; j <= num4; j++)
				{
					if (Main.tile[i, j] != null)
					{
						ushort type = Main.tile[i, j].type;
					}
				}
			}
			return false;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x002608D8 File Offset: 0x0025EAD8
		public static Vector2 StickyTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
		{
			int num = (int)(Position.X / 16f) - 1;
			int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int num3 = (int)(Position.Y / 16f) - 1;
			int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].active() && !Main.tile[i, j].inActive())
					{
						if (Main.tile[i, j].type == 51)
						{
							int num5 = 0;
							Vector2 vector;
							vector.X = (float)(i * 16);
							vector.Y = (float)(j * 16);
							if (Position.X + (float)Width > vector.X - (float)num5 && Position.X < vector.X + 16f + (float)num5 && Position.Y + (float)Height > vector.Y && (double)Position.Y < (double)vector.Y + 16.01)
							{
								if (Main.tile[i, j].type == 51 && (double)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7 && Main.rand.Next(30) == 0)
								{
									Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 30, 0f, 0f, 0, default(Color), 1f);
								}
								return new Vector2((float)i, (float)j);
							}
						}
						else if (Main.tile[i, j].type == 229 && Main.tile[i, j].slope() == 0)
						{
							int num6 = 1;
							Vector2 vector;
							vector.X = (float)(i * 16);
							vector.Y = (float)(j * 16);
							float num7 = 16.01f;
							if (Main.tile[i, j].halfBrick())
							{
								vector.Y += 8f;
								num7 -= 8f;
							}
							if (Position.X + (float)Width > vector.X - (float)num6 && Position.X < vector.X + 16f + (float)num6 && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + num7)
							{
								if (Main.tile[i, j].type == 51 && (double)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7 && Main.rand.Next(30) == 0)
								{
									Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 30, 0f, 0f, 0, default(Color), 1f);
								}
								return new Vector2((float)i, (float)j);
							}
						}
					}
				}
			}
			return new Vector2(-1f, -1f);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00260C6F File Offset: 0x0025EE6F
		public static bool SolidTilesVersatile(int startX, int endX, int startY, int endY)
		{
			if (startX > endX)
			{
				Utils.Swap<int>(ref startX, ref endX);
			}
			if (startY > endY)
			{
				Utils.Swap<int>(ref startY, ref endY);
			}
			return Collision.SolidTiles(startX, endX, startY, endY);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00260C94 File Offset: 0x0025EE94
		public static bool SolidTiles(Vector2 position, int width, int height)
		{
			return Collision.SolidTiles((int)(position.X / 16f), (int)((position.X + (float)width) / 16f), (int)(position.Y / 16f), (int)((position.Y + (float)height) / 16f));
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00260CE0 File Offset: 0x0025EEE0
		public static bool SolidTiles(int startX, int endX, int startY, int endY)
		{
			if (startX < 0)
			{
				return true;
			}
			if (endX >= Main.maxTilesX)
			{
				return true;
			}
			if (startY < 0)
			{
				return true;
			}
			if (endY >= Main.maxTilesY)
			{
				return true;
			}
			for (int i = startX; i < endX + 1; i++)
			{
				for (int j = startY; j < endY + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						return false;
					}
					if (Main.tile[i, j].active() && !Main.tile[i, j].inActive() && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00260D98 File Offset: 0x0025EF98
		public static bool SolidTiles(Vector2 position, int width, int height, bool allowTopSurfaces)
		{
			return Collision.SolidTiles((int)(position.X / 16f), (int)((position.X + (float)width) / 16f), (int)(position.Y / 16f), (int)((position.Y + (float)height) / 16f), allowTopSurfaces);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00260DE8 File Offset: 0x0025EFE8
		public static bool SolidTiles(int startX, int endX, int startY, int endY, bool allowTopSurfaces)
		{
			if (startX < 0)
			{
				return true;
			}
			if (endX >= Main.maxTilesX)
			{
				return true;
			}
			if (startY < 0)
			{
				return true;
			}
			if (endY >= Main.maxTilesY)
			{
				return true;
			}
			for (int i = startX; i < endX + 1; i++)
			{
				for (int j = startY; j < endY + 1; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						return false;
					}
					if (tile.active() && !Main.tile[i, j].inActive())
					{
						ushort type = tile.type;
						bool flag = Main.tileSolid[(int)type] && !Main.tileSolidTop[(int)type];
						if (allowTopSurfaces)
						{
							flag |= (Main.tileSolidTop[(int)type] && tile.frameY == 0);
						}
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00260EA8 File Offset: 0x0025F0A8
		public static void StepDown(ref Vector2 position, ref Vector2 velocity, int width, int height, ref float stepSpeed, ref float gfxOffY, int gravDir = 1, bool waterWalk = false)
		{
			Vector2 vector = position;
			vector.X += velocity.X;
			vector.Y = (float)Math.Floor((double)((vector.Y + (float)height) / 16f)) * 16f - (float)height;
			bool flag = false;
			int num = (int)(vector.X / 16f);
			int num2 = (int)((vector.X + (float)width) / 16f);
			int num3 = (int)((vector.Y + (float)height + 4f) / 16f);
			int num4 = height / 16 + ((height % 16 == 0) ? 0 : 1);
			float num5 = (float)((num3 + num4) * 16);
			float num6 = Main.bottomWorld / 16f - 42f;
			for (int i = num; i <= num2; i++)
			{
				for (int j = num3; j <= num3 + 1; j++)
				{
					if (WorldGen.InWorld(i, j, 1))
					{
						if (Main.tile[i, j] == null)
						{
							Main.tile[i, j] = new Tile();
						}
						if (Main.tile[i, j - 1] == null)
						{
							Main.tile[i, j - 1] = new Tile();
						}
						if (waterWalk && Main.tile[i, j].liquid > 0 && Main.tile[i, j - 1].liquid == 0)
						{
							int num7 = (int)(Main.tile[i, j].liquid / 32 * 2 + 2);
							int num8 = j * 16 + 16 - num7;
							Rectangle rectangle = new Rectangle(i * 16, j * 16 - 17, 16, 16);
							if (rectangle.Intersects(new Rectangle((int)position.X, (int)position.Y, width, height)) && (float)num8 < num5)
							{
								num5 = (float)num8;
							}
						}
						if ((float)j >= num6 || (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type])))
						{
							int num9 = j * 16;
							if (Main.tile[i, j].halfBrick())
							{
								num9 += 8;
							}
							if (Utils.FloatIntersect((float)(i * 16), (float)(j * 16 - 17), 16f, 16f, position.X, position.Y, (float)width, (float)height) && (float)num9 < num5)
							{
								num5 = (float)num9;
							}
						}
					}
				}
			}
			float num10 = num5 - (position.Y + (float)height);
			if (num10 > 7f && num10 < 17f && !flag)
			{
				stepSpeed = 1.5f;
				if (num10 > 9f)
				{
					stepSpeed = 2.5f;
				}
				gfxOffY += position.Y + (float)height - num5;
				position.Y = num5 - (float)height;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00261190 File Offset: 0x0025F390
		public static void StepUp(ref Vector2 position, ref Vector2 velocity, int width, int height, ref float stepSpeed, ref float gfxOffY, int gravDir = 1, bool holdsMatching = false, int specialChecksMode = 0)
		{
			int num = 0;
			if (velocity.X < 0f)
			{
				num = -1;
			}
			if (velocity.X > 0f)
			{
				num = 1;
			}
			Vector2 vector = position;
			vector.X += velocity.X;
			int num2 = (int)((vector.X + (float)(width / 2) + (float)((width / 2 + 1) * num)) / 16f);
			int num3 = (int)(((double)vector.Y + 0.1) / 16.0);
			if (gravDir == 1)
			{
				num3 = (int)((vector.Y + (float)height - 1f) / 16f);
			}
			int num4 = height / 16 + ((height % 16 == 0) ? 0 : 1);
			bool flag = true;
			bool flag2 = true;
			if (Main.tile[num2, num3] == null)
			{
				return;
			}
			for (int i = 1; i < num4 + 2; i++)
			{
				if (!WorldGen.InWorld(num2, num3 - i * gravDir, 0) || Main.tile[num2, num3 - i * gravDir] == null)
				{
					return;
				}
			}
			if (!WorldGen.InWorld(num2 - num, num3 - num4 * gravDir, 0) || Main.tile[num2 - num, num3 - num4 * gravDir] == null)
			{
				return;
			}
			Tile tile;
			for (int j = 2; j < num4 + 1; j++)
			{
				if (!WorldGen.InWorld(num2, num3 - j * gravDir, 0))
				{
					return;
				}
				if (Main.tile[num2, num3 - j * gravDir] == null)
				{
					return;
				}
				tile = Main.tile[num2, num3 - j * gravDir];
				flag = (flag && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]));
			}
			tile = Main.tile[num2 - num, num3 - num4 * gravDir];
			flag2 = (flag2 && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]));
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			Tile tile2;
			if (gravDir == 1)
			{
				if (Main.tile[num2, num3 - gravDir] == null)
				{
					return;
				}
				if (Main.tile[num2, num3 - (num4 + 1) * gravDir] == null)
				{
					return;
				}
				tile = Main.tile[num2, num3 - gravDir];
				tile2 = Main.tile[num2, num3 - (num4 + 1) * gravDir];
				flag3 = (flag3 && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type] || (tile.slope() == 1 && position.X + (float)(width / 2) > (float)(num2 * 16)) || (tile.slope() == 2 && position.X + (float)(width / 2) < (float)(num2 * 16 + 16)) || (tile.halfBrick() && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type]))));
				tile = Main.tile[num2, num3];
				tile2 = Main.tile[num2, num3 - 1];
				if (specialChecksMode == 1)
				{
					flag5 = (tile.type != 16 && tile.type != 18 && tile.type != 14 && tile.type != 469 && tile.type != 134);
				}
				flag4 = (flag4 && ((tile.nactive() && (!tile.topSlope() || (tile.slope() == 1 && position.X + (float)(width / 2) < (float)(num2 * 16)) || (tile.slope() == 2 && position.X + (float)(width / 2) > (float)(num2 * 16 + 16))) && (!tile.topSlope() || position.Y + (float)height > (float)(num3 * 16)) && ((Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type]) || (holdsMatching && ((Main.tileSolidTop[(int)tile.type] && tile.frameY == 0) || TileID.Sets.Platforms[(int)tile.type]) && (!Main.tileSolid[(int)tile2.type] || !tile2.nactive()) && flag5))) || (tile2.halfBrick() && tile2.nactive())));
				flag4 &= (!Main.tileSolidTop[(int)tile.type] || !Main.tileSolidTop[(int)tile2.type]);
			}
			else
			{
				tile = Main.tile[num2, num3 - gravDir];
				tile2 = Main.tile[num2, num3 - (num4 + 1) * gravDir];
				flag3 = (flag3 && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type] || tile.slope() != 0 || (tile.halfBrick() && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type]))));
				tile = Main.tile[num2, num3];
				tile2 = Main.tile[num2, num3 + 1];
				flag4 = (flag4 && ((tile.nactive() && ((Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type]) || (holdsMatching && Main.tileSolidTop[(int)tile.type] && tile.frameY == 0 && (!Main.tileSolid[(int)tile2.type] || !tile2.nactive())))) || (tile2.halfBrick() && tile2.nactive())));
			}
			if ((float)(num2 * 16) < vector.X + (float)width && (float)(num2 * 16 + 16) > vector.X)
			{
				if (gravDir == 1)
				{
					if (flag4 && flag3 && flag && flag2)
					{
						float num5 = (float)(num3 * 16);
						if (Main.tile[num2, num3 - 1].halfBrick())
						{
							num5 -= 8f;
						}
						else if (Main.tile[num2, num3].halfBrick())
						{
							num5 += 8f;
						}
						if (num5 < vector.Y + (float)height)
						{
							float num6 = vector.Y + (float)height - num5;
							if ((double)num6 <= 16.1)
							{
								gfxOffY += position.Y + (float)height - num5;
								position.Y = num5 - (float)height;
								if (num6 < 9f)
								{
									stepSpeed = 1f;
									return;
								}
								stepSpeed = 2f;
								return;
							}
						}
					}
				}
				else if (flag4 && flag3 && flag && flag2 && !Main.tile[num2, num3].bottomSlope() && !TileID.Sets.Platforms[(int)tile2.type])
				{
					float num7 = (float)(num3 * 16 + 16);
					if (num7 > vector.Y)
					{
						float num8 = num7 - vector.Y;
						if ((double)num8 <= 16.1)
						{
							gfxOffY -= num7 - position.Y;
							position.Y = num7;
							velocity.Y = 0f;
							if (num8 < 9f)
							{
								stepSpeed = 1f;
								return;
							}
							stepSpeed = 2f;
						}
					}
				}
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x002618CA File Offset: 0x0025FACA
		public static bool InTileBounds(int x, int y, int lx, int ly, int hx, int hy)
		{
			return x >= lx && x <= hx && y >= ly && y <= hy;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x002618E4 File Offset: 0x0025FAE4
		public static float GetTileRotation(Vector2 position)
		{
			float num = position.Y % 16f;
			int num2 = (int)(position.X / 16f);
			int num3 = (int)(position.Y / 16f);
			Tile tile = Main.tile[num2, num3];
			bool flag = false;
			for (int i = 2; i >= 0; i--)
			{
				if (tile.active())
				{
					if (Main.tileSolid[(int)tile.type])
					{
						int num4 = tile.blockType();
						if (tile.type == 19)
						{
							int num5 = (int)(tile.frameX / 18);
							if (((num5 >= 0 && num5 <= 7) || (num5 >= 12 && num5 <= 16)) && (num == 0f || flag))
							{
								return 0f;
							}
							switch (num5)
							{
							case 8:
							case 19:
							case 21:
							case 23:
								return -0.7853982f;
							case 10:
							case 20:
							case 22:
							case 24:
								return 0.7853982f;
							case 25:
							case 26:
								if (flag)
								{
									return 0f;
								}
								if (num4 == 2)
								{
									return 0.7853982f;
								}
								if (num4 == 3)
								{
									return -0.7853982f;
								}
								break;
							}
						}
						else
						{
							if (num4 == 1)
							{
								return 0f;
							}
							if (num4 == 2)
							{
								return 0.7853982f;
							}
							if (num4 == 3)
							{
								return -0.7853982f;
							}
							return 0f;
						}
					}
					else if (Main.tileSolidTop[(int)tile.type] && tile.frameY == 0 && flag)
					{
						return 0f;
					}
				}
				num3++;
				tile = Main.tile[num2, num3];
				flag = true;
			}
			return 0f;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00261A90 File Offset: 0x0025FC90
		public static void GetEntityEdgeTiles(List<Point> p, Entity entity, bool left = true, bool right = true, bool up = true, bool down = true)
		{
			int num = (int)entity.position.X;
			int num2 = (int)entity.position.Y;
			int num3 = num % 16;
			int num4 = num2 % 16;
			int num5 = (int)entity.Right.X;
			int num6 = (int)entity.Bottom.Y;
			if (num % 16 == 0)
			{
				num--;
			}
			if (num2 % 16 == 0)
			{
				num2--;
			}
			if (num5 % 16 == 0)
			{
				num5++;
			}
			if (num6 % 16 == 0)
			{
				num6++;
			}
			int num7 = num5 / 16 - num / 16;
			int num8 = num6 / 16 - num2 / 16;
			num /= 16;
			num2 /= 16;
			for (int i = num; i <= num + num7; i++)
			{
				if (up)
				{
					p.Add(new Point(i, num2));
				}
				if (down)
				{
					p.Add(new Point(i, num2 + num8));
				}
			}
			for (int j = num2; j < num2 + num8; j++)
			{
				if (left)
				{
					p.Add(new Point(num, j));
				}
				if (right)
				{
					p.Add(new Point(num + num7, j));
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00261B98 File Offset: 0x0025FD98
		public static void StepConveyorBelt(Entity entity, float gravDir)
		{
			Player player = null;
			if (entity is Player)
			{
				player = (Player)entity;
				if (Math.Abs(player.gfxOffY) > 2f || player.grapCount > 0 || player.pulley)
				{
					return;
				}
				entity.height -= 5;
				entity.position.Y = entity.position.Y + 5f;
			}
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int num3 = (int)entity.position.Y + entity.height;
			entity.Hitbox.Inflate(2, 2);
			Vector2 topLeft = entity.TopLeft;
			Vector2 topRight = entity.TopRight;
			Vector2 bottomLeft = entity.BottomLeft;
			Vector2 bottomRight = entity.BottomRight;
			List<Point> cacheForConveyorBelts = Collision._cacheForConveyorBelts;
			cacheForConveyorBelts.Clear();
			Collision.GetEntityEdgeTiles(cacheForConveyorBelts, entity, false, false, true, true);
			Vector2 vector = new Vector2(0.0001f);
			for (int i = 0; i < cacheForConveyorBelts.Count; i++)
			{
				Point point = cacheForConveyorBelts[i];
				if (WorldGen.InWorld(point.X, point.Y, 0) && (player == null || !player.onTrack || point.Y >= num3))
				{
					Tile tile = Main.tile[point.X, point.Y];
					if (tile != null && tile.active() && tile.nactive())
					{
						int num4 = TileID.Sets.ConveyorDirection[(int)tile.type];
						if (num4 != 0)
						{
							Vector2 lineStart;
							Vector2 lineStart2;
							lineStart.X = (lineStart2.X = (float)(point.X * 16));
							Vector2 lineEnd;
							Vector2 lineEnd2;
							lineEnd.X = (lineEnd2.X = (float)(point.X * 16 + 16));
							switch (tile.slope())
							{
							case 1:
								lineStart2.Y = (float)(point.Y * 16);
								lineEnd2.Y = (lineEnd.Y = (lineStart.Y = (float)(point.Y * 16 + 16)));
								break;
							case 2:
								lineEnd2.Y = (float)(point.Y * 16);
								lineStart2.Y = (lineEnd.Y = (lineStart.Y = (float)(point.Y * 16 + 16)));
								break;
							case 3:
								lineEnd.Y = (lineStart2.Y = (lineEnd2.Y = (float)(point.Y * 16)));
								lineStart.Y = (float)(point.Y * 16 + 16);
								break;
							case 4:
								lineStart.Y = (lineStart2.Y = (lineEnd2.Y = (float)(point.Y * 16)));
								lineEnd.Y = (float)(point.Y * 16 + 16);
								break;
							default:
								if (tile.halfBrick())
								{
									lineStart2.Y = (lineEnd2.Y = (float)(point.Y * 16 + 8));
								}
								else
								{
									lineStart2.Y = (lineEnd2.Y = (float)(point.Y * 16));
								}
								lineStart.Y = (lineEnd.Y = (float)(point.Y * 16 + 16));
								break;
							}
							int num5 = 0;
							if (!TileID.Sets.Platforms[(int)tile.type] && Collision.CheckAABBvLineCollision2(entity.position - vector, entity.Size + vector * 2f, lineStart, lineEnd))
							{
								num5--;
							}
							if (Collision.CheckAABBvLineCollision2(entity.position - vector, entity.Size + vector * 2f, lineStart2, lineEnd2))
							{
								num5++;
							}
							if (num5 != 0)
							{
								flag = true;
								num += num4 * num5 * (int)gravDir;
								if (tile.leftSlope())
								{
									num2 += (int)gravDir * -num4;
								}
								if (tile.rightSlope())
								{
									num2 -= (int)gravDir * -num4;
								}
							}
						}
					}
				}
			}
			if (entity is Player)
			{
				entity.height += 5;
				entity.position.Y = entity.position.Y - 5f;
			}
			if (!flag)
			{
				return;
			}
			if (num != 0)
			{
				num = Math.Sign(num);
				num2 = Math.Sign(num2);
				Vector2 velocity = Vector2.Normalize(new Vector2((float)num * gravDir, (float)num2)) * 2.5f;
				Vector2 value = Collision.TileCollision(entity.position, velocity, entity.width, entity.height, false, false, (int)gravDir);
				entity.position += value;
				velocity = new Vector2(0f, 2.5f * gravDir);
				value = Collision.TileCollision(entity.position, velocity, entity.width, entity.height, false, false, (int)gravDir);
				entity.position += value;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00262078 File Offset: 0x00260278
		public static List<Point> GetTilesIn(Vector2 TopLeft, Vector2 BottomRight)
		{
			List<Point> list = new List<Point>();
			Point point = TopLeft.ToTileCoordinates();
			Point point2 = BottomRight.ToTileCoordinates();
			int num = Utils.Clamp<int>(point.X, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(point.Y, 0, Main.maxTilesY - 1);
			int num3 = Utils.Clamp<int>(point2.X, 0, Main.maxTilesX - 1);
			int num4 = Utils.Clamp<int>(point2.Y, 0, Main.maxTilesY - 1);
			for (int i = num; i <= num3; i++)
			{
				for (int j = num2; j <= num4; j++)
				{
					if (Main.tile[i, j] != null)
					{
						list.Add(new Point(i, j));
					}
				}
			}
			return list;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0026212C File Offset: 0x0026032C
		public static void ExpandVertically(int startX, int startY, out int topY, out int bottomY, int maxExpandUp = 100, int maxExpandDown = 100)
		{
			topY = startY;
			bottomY = startY;
			if (!WorldGen.InWorld(startX, startY, 10))
			{
				return;
			}
			int num = 0;
			while (num < maxExpandUp && topY > 0 && topY >= 10 && Main.tile[startX, topY] != null && !WorldGen.SolidTile3(startX, topY))
			{
				topY--;
				num++;
			}
			int num2 = 0;
			while (num2 < maxExpandDown && bottomY < Main.maxTilesY - 10 && bottomY <= Main.maxTilesY - 10 && Main.tile[startX, bottomY] != null && !WorldGen.SolidTile3(startX, bottomY))
			{
				bottomY++;
				num2++;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x002621C8 File Offset: 0x002603C8
		public static Vector2 AdvancedTileCollision(bool[] forcedIgnoredTiles, Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int value = (int)(Position.X / 16f) - 1;
			int num = (int)((Position.X + (float)Width) / 16f) + 2;
			int num2 = (int)(Position.Y / 16f) - 1;
			int num3 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			num = Utils.Clamp<int>(num, 0, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(num2, 0, Main.maxTilesY - 1);
			num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesY - 1);
			float num9 = (float)((num3 + 3) * 16);
			for (int i = num8; i < num; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && !tile.inActive() && !forcedIgnoredTiles[(int)tile.type] && (Main.tileSolid[(int)tile.type] || (Main.tileSolidTop[(int)tile.type] && tile.frameY == 0)))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num10 = 16;
						if (tile.halfBrick())
						{
							vector2.Y += 8f;
							num10 -= 8;
						}
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num10)
						{
							bool flag = false;
							bool flag2 = false;
							if (tile.slope() > 2)
							{
								if (tile.slope() == 3 && Position.Y + Math.Abs(Velocity.X) >= vector2.Y && Position.X >= vector2.X)
								{
									flag2 = true;
								}
								if (tile.slope() == 4 && Position.Y + Math.Abs(Velocity.X) >= vector2.Y && Position.X + (float)Width <= vector2.X + 16f)
								{
									flag2 = true;
								}
							}
							else if (tile.slope() > 0)
							{
								flag = true;
								if (tile.slope() == 1 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector2.Y + (float)num10 && Position.X >= vector2.X)
								{
									flag2 = true;
								}
								if (tile.slope() == 2 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector2.Y + (float)num10 && Position.X + (float)Width <= vector2.X + 16f)
								{
									flag2 = true;
								}
							}
							if (!flag2)
							{
								if (Position.Y + (float)Height <= vector2.Y)
								{
									Collision.down = true;
									if ((!Main.tileSolidTop[(int)tile.type] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num9 > vector2.Y)
									{
										num6 = i;
										num7 = j;
										if (num10 < 16)
										{
											num7++;
										}
										if (num6 != num4 && !flag)
										{
											result.Y = vector2.Y - (Position.Y + (float)Height) + ((gravDir == -1) ? -0.01f : 0f);
											num9 = vector2.Y;
										}
									}
								}
								else if (Position.X + (float)Width <= vector2.X && !Main.tileSolidTop[(int)tile.type])
								{
									if (Main.tile[i - 1, j] == null)
									{
										Main.tile[i - 1, j] = new Tile();
									}
									if (Main.tile[i - 1, j].slope() != 2 && Main.tile[i - 1, j].slope() != 4)
									{
										num4 = i;
										num5 = j;
										if (num5 != num7)
										{
											result.X = vector2.X - (Position.X + (float)Width);
										}
										if (num6 == num4)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.X >= vector2.X + 16f && !Main.tileSolidTop[(int)tile.type])
								{
									if (Main.tile[i + 1, j] == null)
									{
										Main.tile[i + 1, j] = new Tile();
									}
									if (Main.tile[i + 1, j].slope() != 1 && Main.tile[i + 1, j].slope() != 3)
									{
										num4 = i;
										num5 = j;
										if (num5 != num7)
										{
											result.X = vector2.X + 16f - Position.X;
										}
										if (num6 == num4)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.Y >= vector2.Y + (float)num10 && !Main.tileSolidTop[(int)tile.type])
								{
									Collision.up = true;
									num6 = i;
									num7 = j;
									result.Y = vector2.Y + (float)num10 - Position.Y + ((gravDir == 1) ? 0.01f : 0f);
									if (num7 == num5)
									{
										result.X = Velocity.X;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00262798 File Offset: 0x00260998
		public static void LaserScan(Vector2 samplingPoint, Vector2 directionUnit, float samplingWidth, float maxDistance, float[] samples)
		{
			for (int i = 0; i < samples.Length; i++)
			{
				float num = (float)i / (float)(samples.Length - 1);
				Vector2 vector = samplingPoint + directionUnit.RotatedBy(1.5707963705062866, default(Vector2)) * (num - 0.5f) * samplingWidth;
				int num2 = (int)vector.X / 16;
				int num3 = (int)vector.Y / 16;
				Vector2 vector2 = vector + directionUnit * maxDistance;
				int num4 = (int)vector2.X / 16;
				int num5 = (int)vector2.Y / 16;
				Tuple<int, int> tuple;
				float num6;
				if (!Collision.TupleHitLine(num2, num3, num4, num5, 0, 0, new List<Tuple<int, int>>(), out tuple))
				{
					num6 = new Vector2((float)Math.Abs(num2 - tuple.Item1), (float)Math.Abs(num3 - tuple.Item2)).Length() * 16f;
				}
				else if (tuple.Item1 == num4 && tuple.Item2 == num5)
				{
					num6 = maxDistance;
				}
				else
				{
					num6 = new Vector2((float)Math.Abs(num2 - tuple.Item1), (float)Math.Abs(num3 - tuple.Item2)).Length() * 16f;
				}
				samples[i] = num6;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x002628D7 File Offset: 0x00260AD7
		public static void AimingLaserScan(Vector2 startPoint, Vector2 endPoint, float samplingWidth, int samplesToTake, out Vector2 vectorTowardsTarget, out float[] samples)
		{
			samples = new float[samplesToTake];
			vectorTowardsTarget = endPoint - startPoint;
			Collision.LaserScan(startPoint, vectorTowardsTarget.SafeNormalize(Vector2.Zero), samplingWidth, vectorTowardsTarget.Length(), samples);
		}

		// Token: 0x040003E9 RID: 1001
		public static bool stair;

		// Token: 0x040003EA RID: 1002
		public static bool stairFall;

		// Token: 0x040003EB RID: 1003
		public static bool honey;

		// Token: 0x040003EC RID: 1004
		public static bool shimmer;

		// Token: 0x040003ED RID: 1005
		public static bool sloping;

		// Token: 0x040003EE RID: 1006
		public static bool landMine = false;

		// Token: 0x040003EF RID: 1007
		public static bool up;

		// Token: 0x040003F0 RID: 1008
		public static bool down;

		// Token: 0x040003F1 RID: 1009
		public static float Epsilon = 2.7182817f;

		// Token: 0x040003F2 RID: 1010
		private static List<Point> _cacheForConveyorBelts = new List<Point>();

		// Token: 0x020004B8 RID: 1208
		public struct HurtTile
		{
			// Token: 0x04005667 RID: 22119
			public int type;

			// Token: 0x04005668 RID: 22120
			public int x;

			// Token: 0x04005669 RID: 22121
			public int y;
		}
	}
}
