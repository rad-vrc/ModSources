using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000026 RID: 38
	public class Collision
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
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
				if (0f - Collision.Epsilon >= num3 || num3 >= Collision.Epsilon)
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
					if ((0f - Collision.Epsilon >= num || num >= Collision.Epsilon) && (0f - Collision.Epsilon >= num2 || num2 >= Collision.Epsilon))
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

		// Token: 0x0600013D RID: 317 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		private static double DistFromSeg(Vector2 p, Vector2 q0, Vector2 q1, double radius, ref float u)
		{
			double num6 = (double)(q1.X - q0.X);
			double num2 = (double)(q1.Y - q0.Y);
			double num3 = (double)(q0.X - p.X);
			double num4 = (double)(q0.Y - p.Y);
			double num5 = Math.Sqrt(num6 * num6 + num2 * num2);
			if (num5 < (double)Collision.Epsilon)
			{
				throw new Exception("Expected line segment, not point.");
			}
			return Math.Abs(num6 * num4 - num3 * num2) / num5;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000E42C File Offset: 0x0000C62C
		private static bool PointOnLine(Vector2 p, Vector2 a1, Vector2 a2)
		{
			float u = 0f;
			return Collision.DistFromSeg(p, a1, a2, (double)Collision.Epsilon, ref u) < (double)Collision.Epsilon;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000E458 File Offset: 0x0000C658
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

		// Token: 0x06000140 RID: 320 RVA: 0x0000E548 File Offset: 0x0000C748
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
			if (num != num2)
			{
				return new float[]
				{
					num,
					num2
				};
			}
			return new float[]
			{
				num
			};
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public static bool CheckAABBvAABBCollision(Vector2 position1, Vector2 dimensions1, Vector2 position2, Vector2 dimensions2)
		{
			return position1.X < position2.X + dimensions2.X && position1.Y < position2.Y + dimensions2.Y && position1.X + dimensions1.X > position2.X && position1.Y + dimensions1.Y > position2.Y;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000E60C File Offset: 0x0000C80C
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

		// Token: 0x06000143 RID: 323 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
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

		// Token: 0x06000144 RID: 324 RVA: 0x0000E784 File Offset: 0x0000C984
		public static bool CheckAABBvLineCollision2(Vector2 aabbPosition, Vector2 aabbDimensions, Vector2 lineStart, Vector2 lineEnd)
		{
			float collisionPoint = 0f;
			return Utils.RectangleLineCollision(aabbPosition, aabbPosition + aabbDimensions, lineStart, lineEnd) || Collision.CheckAABBvLineCollision(aabbPosition, aabbDimensions, lineStart, lineEnd, 0.0001f, ref collisionPoint);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public static bool CheckAABBvLineCollision(Vector2 objectPosition, Vector2 objectDimensions, Vector2 lineStart, Vector2 lineEnd, float lineWidth, ref float collisionPoint)
		{
			float num = lineWidth * 0.5f;
			Vector2 position = lineStart;
			Vector2 dimensions = lineEnd - lineStart;
			if (dimensions.X > 0f)
			{
				dimensions.X += lineWidth;
				position.X -= num;
			}
			else
			{
				position.X += dimensions.X - num;
				dimensions.X = 0f - dimensions.X + lineWidth;
			}
			if (dimensions.Y > 0f)
			{
				dimensions.Y += lineWidth;
				position.Y -= num;
			}
			else
			{
				position.Y += dimensions.Y - num;
				dimensions.Y = 0f - dimensions.Y + lineWidth;
			}
			if (!Collision.CheckAABBvAABBCollision(objectPosition, objectDimensions, position, dimensions))
			{
				return false;
			}
			Vector2 vector = objectPosition - lineStart;
			Vector2 spinningpoint = vector + objectDimensions;
			Vector2 spinningpoint2;
			spinningpoint2..ctor(vector.X, spinningpoint.Y);
			Vector2 spinningpoint3;
			spinningpoint3..ctor(spinningpoint.X, vector.Y);
			Vector2 vector2 = lineEnd - lineStart;
			float num2 = vector2.Length();
			float num3 = (float)Math.Atan2((double)vector2.Y, (double)vector2.X);
			Vector2[] array = new Vector2[]
			{
				vector.RotatedBy((double)(0f - num3), default(Vector2)),
				spinningpoint3.RotatedBy((double)(0f - num3), default(Vector2)),
				spinningpoint.RotatedBy((double)(0f - num3), default(Vector2)),
				spinningpoint2.RotatedBy((double)(0f - num3), default(Vector2))
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
			Vector2 vector3;
			vector3..ctor(0f, num);
			Vector2 vector4;
			vector4..ctor(num2, num);
			Vector2 vector5;
			vector5..ctor(0f, 0f - num);
			Vector2 vector6;
			vector6..ctor(num2, 0f - num);
			for (int j = 0; j < array.Length; j++)
			{
				int num4 = (j + 1) % array.Length;
				Vector2 vector7 = vector4 - vector3;
				Vector2 vector8 = array[num4] - array[j];
				float num5 = vector7.X * vector8.Y - vector7.Y * vector8.X;
				if (num5 != 0f)
				{
					Vector2 vector9 = array[j] - vector3;
					float num6 = (vector9.X * vector8.Y - vector9.Y * vector8.X) / num5;
					if (num6 >= 0f && num6 <= 1f)
					{
						float num7 = (vector9.X * vector7.Y - vector9.Y * vector7.X) / num5;
						if (num7 >= 0f && num7 <= 1f)
						{
							result = true;
							collisionPoint = Math.Min(collisionPoint, vector3.X + num6 * vector7.X);
						}
					}
				}
				vector7 = vector6 - vector5;
				num5 = vector7.X * vector8.Y - vector7.Y * vector8.X;
				if (num5 != 0f)
				{
					Vector2 vector10 = array[j] - vector5;
					float num8 = (vector10.X * vector8.Y - vector10.Y * vector8.X) / num5;
					if (num8 >= 0f && num8 <= 1f)
					{
						float num9 = (vector10.X * vector7.Y - vector10.Y * vector7.X) / num5;
						if (num9 >= 0f && num9 <= 1f)
						{
							result = true;
							collisionPoint = Math.Min(collisionPoint, vector5.X + num8 * vector7.X);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000EC0B File Offset: 0x0000CE0B
		public static bool CanHit(Entity source, Entity target)
		{
			return Collision.CanHit(source.position, source.width, source.height, target.position, target.width, target.height);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000EC36 File Offset: 0x0000CE36
		public static bool CanHit(Entity source, NPCAimedTarget target)
		{
			return Collision.CanHit(source.position, source.width, source.height, target.Position, target.Width, target.Height);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000EC61 File Offset: 0x0000CE61
		public static bool CanHit(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
		{
			return Collision.CanHit(Position1.ToPoint(), Width1, Height1, Position2.ToPoint(), Width2, Height2);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public unsafe static bool CanHit(Point Position1, int Width1, int Height1, Point Position2, int Width2, int Height2)
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
						num = ((num >= num3) ? (num - 1) : (num + 1));
						if (Main.tile[num, num2 - 1] == null)
						{
							goto Block_14;
						}
						if (Main.tile[num, num2 + 1] == null)
						{
							goto Block_15;
						}
						if (!Main.tile[num, num2 - 1].inActive() && Main.tile[num, num2 - 1].active() && Main.tileSolid[(int)(*Main.tile[num, num2 - 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2 - 1].type)] && Main.tile[num, num2 - 1].slope() == 0 && !Main.tile[num, num2 - 1].halfBrick() && !Main.tile[num, num2 + 1].inActive() && Main.tile[num, num2 + 1].active() && Main.tileSolid[(int)(*Main.tile[num, num2 + 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2 + 1].type)] && Main.tile[num, num2 + 1].slope() == 0 && !Main.tile[num, num2 + 1].halfBrick())
						{
							goto Block_27;
						}
					}
					else
					{
						num2 = ((num2 >= num4) ? (num2 - 1) : (num2 + 1));
						if (Main.tile[num - 1, num2] == null)
						{
							goto Block_29;
						}
						if (Main.tile[num + 1, num2] == null)
						{
							goto Block_30;
						}
						if (!Main.tile[num - 1, num2].inActive() && Main.tile[num - 1, num2].active() && Main.tileSolid[(int)(*Main.tile[num - 1, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num - 1, num2].type)] && Main.tile[num - 1, num2].slope() == 0 && !Main.tile[num - 1, num2].halfBrick() && !Main.tile[num + 1, num2].inActive() && Main.tile[num + 1, num2].active() && Main.tileSolid[(int)(*Main.tile[num + 1, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num + 1, num2].type)] && Main.tile[num + 1, num2].slope() == 0 && !Main.tile[num + 1, num2].halfBrick())
						{
							goto Block_42;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_43;
					}
					if (!Main.tile[num, num2].inActive() && Main.tile[num, num2].active() && Main.tileSolid[(int)(*Main.tile[num, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2].type)])
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

		// Token: 0x0600014A RID: 330 RVA: 0x0000F170 File Offset: 0x0000D370
		public unsafe static bool CanHitWithCheck(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2, Utils.TileActionAttempt check)
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
						num = ((num >= num3) ? (num - 1) : (num + 1));
						if (Main.tile[num, num2 - 1] == null)
						{
							goto Block_14;
						}
						if (Main.tile[num, num2 + 1] == null)
						{
							goto Block_15;
						}
						if (!Main.tile[num, num2 - 1].inActive() && Main.tile[num, num2 - 1].active() && Main.tileSolid[(int)(*Main.tile[num, num2 - 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2 - 1].type)] && Main.tile[num, num2 - 1].slope() == 0 && !Main.tile[num, num2 - 1].halfBrick() && !Main.tile[num, num2 + 1].inActive() && Main.tile[num, num2 + 1].active() && Main.tileSolid[(int)(*Main.tile[num, num2 + 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2 + 1].type)] && Main.tile[num, num2 + 1].slope() == 0 && !Main.tile[num, num2 + 1].halfBrick())
						{
							goto Block_27;
						}
					}
					else
					{
						num2 = ((num2 >= num4) ? (num2 - 1) : (num2 + 1));
						if (Main.tile[num - 1, num2] == null)
						{
							goto Block_29;
						}
						if (Main.tile[num + 1, num2] == null)
						{
							goto Block_30;
						}
						if (!Main.tile[num - 1, num2].inActive() && Main.tile[num - 1, num2].active() && Main.tileSolid[(int)(*Main.tile[num - 1, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num - 1, num2].type)] && Main.tile[num - 1, num2].slope() == 0 && !Main.tile[num - 1, num2].halfBrick() && !Main.tile[num + 1, num2].inActive() && Main.tile[num + 1, num2].active() && Main.tileSolid[(int)(*Main.tile[num + 1, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num + 1, num2].type)] && Main.tile[num + 1, num2].slope() == 0 && !Main.tile[num + 1, num2].halfBrick())
						{
							goto Block_42;
						}
					}
					if (Main.tile[num, num2] == null)
					{
						goto Block_43;
					}
					if (!Main.tile[num, num2].inActive() && Main.tile[num, num2].active() && Main.tileSolid[(int)(*Main.tile[num, num2].type)] && !Main.tileSolidTop[(int)(*Main.tile[num, num2].type)])
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

		// Token: 0x0600014B RID: 331 RVA: 0x0000F680 File Offset: 0x0000D880
		public unsafe static bool CanHitLine(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
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
					if (num11 != 1)
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
									goto Block_19;
								}
								if (Main.tile[num, num2] == null)
								{
									goto Block_20;
								}
								if (Main.tile[num, num2 + 1] == null)
								{
									goto Block_21;
								}
								Tile tile4 = Main.tile[num, num2 - 1];
								Tile tile5 = Main.tile[num, num2 + 1];
								Tile tile6 = Main.tile[num, num2];
								if ((!tile4.inActive() && tile4.active() && Main.tileSolid[(int)(*tile4.type)] && !Main.tileSolidTop[(int)(*tile4.type)]) || (!tile5.inActive() && tile5.active() && Main.tileSolid[(int)(*tile5.type)] && !Main.tileSolidTop[(int)(*tile5.type)]) || (!tile6.inActive() && tile6.active() && Main.tileSolid[(int)(*tile6.type)] && !Main.tileSolidTop[(int)(*tile6.type)]))
								{
									goto IL_2AE;
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
					}
					else
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
							Tile tile7 = Main.tile[num - 1, num2];
							Tile tile8 = Main.tile[num + 1, num2];
							Tile tile9 = Main.tile[num, num2];
							if ((!tile7.inActive() && tile7.active() && Main.tileSolid[(int)(*tile7.type)] && !Main.tileSolidTop[(int)(*tile7.type)]) || (!tile8.inActive() && tile8.active() && Main.tileSolid[(int)(*tile8.type)] && !Main.tileSolidTop[(int)(*tile8.type)]) || (!tile9.inActive() && tile9.active() && Main.tileSolid[(int)(*tile9.type)] && !Main.tileSolidTop[(int)(*tile9.type)]))
							{
								goto IL_436;
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
					Tile tile10 = Main.tile[num, num2];
					if (!tile10.inActive() && tile10.active() && Main.tileSolid[(int)(*tile10.type)] && !Main.tileSolidTop[(int)(*tile10.type)])
					{
						goto Block_59;
					}
					if (flag || flag2)
					{
						goto Block_60;
					}
				}
				Block_19:
				return false;
				Block_20:
				return false;
				Block_21:
				return false;
				IL_2AE:
				return false;
				Block_37:
				return false;
				Block_38:
				return false;
				Block_39:
				return false;
				IL_436:
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

		// Token: 0x0600014C RID: 332 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public unsafe static bool TupleHitLine(int x1, int y1, int x2, int y2, int ignoreX, int ignoreY, List<Tuple<int, int>> ignoreTargets, out Tuple<int, int> col)
		{
			int value = Utils.Clamp<int>(x1, 1, Main.maxTilesX - 1);
			int value2 = Utils.Clamp<int>(x2, 1, Main.maxTilesX - 1);
			int value3 = Utils.Clamp<int>(y1, 1, Main.maxTilesY - 1);
			int value4 = Utils.Clamp<int>(y2, 1, Main.maxTilesY - 1);
			float num = (float)Math.Abs(value - value2);
			float num2 = (float)Math.Abs(value3 - value4);
			if (num == 0f && num2 == 0f)
			{
				col = new Tuple<int, int>(value, value3);
				return true;
			}
			float num3 = 1f;
			float num4 = 1f;
			if (num == 0f || num2 == 0f)
			{
				if (num == 0f)
				{
					num3 = 0f;
				}
				if (num2 == 0f)
				{
					num4 = 0f;
				}
			}
			else if (num > num2)
			{
				num3 = num / num2;
			}
			else
			{
				num4 = num2 / num;
			}
			float num5 = 0f;
			float num6 = 0f;
			int num7 = 1;
			if (value3 < value4)
			{
				num7 = 2;
			}
			int num8 = (int)num;
			int num9 = (int)num2;
			int num10 = Math.Sign(value2 - value);
			int num11 = Math.Sign(value4 - value3);
			bool flag = false;
			bool flag2 = false;
			bool result;
			try
			{
				for (;;)
				{
					if (num7 != 1)
					{
						if (num7 == 2)
						{
							num5 += num3;
							int num12 = (int)num5;
							num5 %= 1f;
							for (int i = 0; i < num12; i++)
							{
								if (Main.tile[value, value3 - 1] == null)
								{
									goto Block_11;
								}
								if (Main.tile[value, value3 + 1] == null)
								{
									goto Block_12;
								}
								Tile tile4 = Main.tile[value, value3 - 1];
								Tile tile5 = Main.tile[value, value3 + 1];
								Tile tile6 = Main.tile[value, value3];
								if (!ignoreTargets.Contains(new Tuple<int, int>(value, value3)) && !ignoreTargets.Contains(new Tuple<int, int>(value, value3 - 1)) && !ignoreTargets.Contains(new Tuple<int, int>(value, value3 + 1)))
								{
									if (ignoreY != -1 && num11 < 0 && !tile4.inActive() && tile4.active() && Main.tileSolid[(int)(*tile4.type)] && !Main.tileSolidTop[(int)(*tile4.type)])
									{
										goto Block_21;
									}
									if (ignoreY != 1 && num11 > 0 && !tile5.inActive() && tile5.active() && Main.tileSolid[(int)(*tile5.type)] && !Main.tileSolidTop[(int)(*tile5.type)])
									{
										goto Block_27;
									}
									if (!tile6.inActive() && tile6.active() && Main.tileSolid[(int)(*tile6.type)] && !Main.tileSolidTop[(int)(*tile6.type)])
									{
										goto Block_31;
									}
								}
								if (num8 == 0 && num9 == 0)
								{
									flag = true;
									break;
								}
								value += num10;
								num8--;
								if (num8 == 0 && num9 == 0 && num12 == 1)
								{
									flag2 = true;
								}
							}
							if (num9 != 0)
							{
								num7 = 1;
							}
						}
					}
					else
					{
						num6 += num4;
						int num13 = (int)num6;
						num6 %= 1f;
						for (int j = 0; j < num13; j++)
						{
							if (Main.tile[value - 1, value3] == null)
							{
								goto Block_38;
							}
							if (Main.tile[value + 1, value3] == null)
							{
								goto Block_39;
							}
							Tile tile7 = Main.tile[value - 1, value3];
							Tile tile8 = Main.tile[value + 1, value3];
							Tile tile9 = Main.tile[value, value3];
							if (!ignoreTargets.Contains(new Tuple<int, int>(value, value3)) && !ignoreTargets.Contains(new Tuple<int, int>(value - 1, value3)) && !ignoreTargets.Contains(new Tuple<int, int>(value + 1, value3)))
							{
								if (ignoreX != -1 && num10 < 0 && !tile7.inActive() && tile7.active() && Main.tileSolid[(int)(*tile7.type)] && !Main.tileSolidTop[(int)(*tile7.type)])
								{
									goto Block_48;
								}
								if (ignoreX != 1 && num10 > 0 && !tile8.inActive() && tile8.active() && Main.tileSolid[(int)(*tile8.type)] && !Main.tileSolidTop[(int)(*tile8.type)])
								{
									goto Block_54;
								}
								if (!tile9.inActive() && tile9.active() && Main.tileSolid[(int)(*tile9.type)] && !Main.tileSolidTop[(int)(*tile9.type)])
								{
									goto Block_58;
								}
							}
							if (num8 == 0 && num9 == 0)
							{
								flag = true;
								break;
							}
							value3 += num11;
							num9--;
							if (num8 == 0 && num9 == 0 && num13 == 1)
							{
								flag2 = true;
							}
						}
						if (num8 != 0)
						{
							num7 = 2;
						}
					}
					if (Main.tile[value, value3] == null)
					{
						goto Block_65;
					}
					Tile tile10 = Main.tile[value, value3];
					if (!ignoreTargets.Contains(new Tuple<int, int>(value, value3)) && !tile10.inActive() && tile10.active() && Main.tileSolid[(int)(*tile10.type)] && !Main.tileSolidTop[(int)(*tile10.type)])
					{
						goto Block_70;
					}
					if (flag || flag2)
					{
						goto Block_71;
					}
				}
				Block_11:
				col = new Tuple<int, int>(value, value3 - 1);
				return false;
				Block_12:
				col = new Tuple<int, int>(value, value3 + 1);
				return false;
				Block_21:
				col = new Tuple<int, int>(value, value3 - 1);
				return true;
				Block_27:
				col = new Tuple<int, int>(value, value3 + 1);
				return true;
				Block_31:
				col = new Tuple<int, int>(value, value3);
				return true;
				Block_38:
				col = new Tuple<int, int>(value - 1, value3);
				return false;
				Block_39:
				col = new Tuple<int, int>(value + 1, value3);
				return false;
				Block_48:
				col = new Tuple<int, int>(value - 1, value3);
				return true;
				Block_54:
				col = new Tuple<int, int>(value + 1, value3);
				return true;
				Block_58:
				col = new Tuple<int, int>(value, value3);
				return true;
				Block_65:
				col = new Tuple<int, int>(value, value3);
				return false;
				Block_70:
				col = new Tuple<int, int>(value, value3);
				return true;
				Block_71:
				col = new Tuple<int, int>(value, value3);
				result = true;
			}
			catch
			{
				col = new Tuple<int, int>(x1, y1);
				result = false;
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00010198 File Offset: 0x0000E398
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
					if (num11 != 1)
					{
						if (num11 == 2)
						{
							num9 += num7;
							int num16 = (int)num9;
							num9 %= 1f;
							for (int i = 0; i < num16; i++)
							{
								Tile tile = Main.tile[num, num2];
								if (Collision.HitWallSubstep(num, num2))
								{
									goto Block_19;
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
					}
					else
					{
						num10 += num8;
						int num17 = (int)num10;
						num10 %= 1f;
						for (int j = 0; j < num17; j++)
						{
							Tile tile2 = Main.tile[num, num2];
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
					Tile tile3 = Main.tile[num, num2];
					if (Collision.HitWallSubstep(num, num2))
					{
						goto Block_34;
					}
					if (flag || flag2)
					{
						goto Block_35;
					}
				}
				Block_19:
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

		// Token: 0x0600014E RID: 334 RVA: 0x00010450 File Offset: 0x0000E650
		public unsafe static bool HitWallSubstep(int x, int y)
		{
			if (*Main.tile[x, y].wall == 0)
			{
				return false;
			}
			bool flag = false;
			if (Main.wallHouse[(int)(*Main.tile[x, y].wall)])
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						if ((i != 0 || j != 0) && *Main.tile[x + i, y + j].wall == 0)
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
							if (!tile.active() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)])
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

		// Token: 0x0600014F RID: 335 RVA: 0x00010560 File Offset: 0x0000E760
		public static bool EmptyTile(int i, int j, bool ignoreTiles = false)
		{
			Rectangle rectangle;
			rectangle..ctor(i * 16, j * 16, 16, 16);
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

		// Token: 0x06000150 RID: 336 RVA: 0x00010690 File Offset: 0x0000E890
		public unsafe static bool DrownCollision(Vector2 Position, int Width, int Height, float gravDir = -1f, bool includeSlopes = false)
		{
			Vector2 vector;
			vector..ctor(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
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
			vector..ctor(vector.X - (float)(num / 2), Position.Y + -2f);
			if (gravDir == -1f)
			{
				vector.Y += (float)(Height / 2 - 6);
			}
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num6 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			int num3 = (gravDir == 1f) ? value3 : (value4 - 1);
			Vector2 vector2 = default(Vector2);
			for (int i = num6; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && *tile.liquid > 0 && !tile.lava() && !tile.shimmer() && (j != num3 || !tile.active() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)] || (includeSlopes && tile.blockType() != 0)))
					{
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						int num4 = 16;
						float num5 = (float)(256 - (int)(*Main.tile[i, j].liquid));
						num5 /= 32f;
						vector2.Y += num5 * 2f;
						num4 -= (int)(num5 * 2f);
						if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num4)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00010910 File Offset: 0x0000EB10
		public unsafe static bool IsWorldPointSolid(Vector2 pos, bool treatPlatformsAsNonSolid = false)
		{
			Point point = pos.ToTileCoordinates();
			if (!WorldGen.InWorld(point.X, point.Y, 1))
			{
				return false;
			}
			Tile tile = Main.tile[point.X, point.Y];
			if (tile == null || !tile.active() || tile.inActive() || !Main.tileSolid[(int)(*tile.type)])
			{
				return false;
			}
			if (treatPlatformsAsNonSolid && *tile.type > 0 && (TileID.Sets.Platforms[(int)(*tile.type)] || *tile.type == 380))
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

		// Token: 0x06000152 RID: 338 RVA: 0x00010B38 File Offset: 0x0000ED38
		public static bool GetWaterLine(Point pt, out float waterLineHeight)
		{
			return Collision.GetWaterLine(pt.X, pt.Y, out waterLineHeight);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00010B4C File Offset: 0x0000ED4C
		public unsafe static bool GetWaterLine(int X, int Y, out float waterLineHeight)
		{
			waterLineHeight = 0f;
			if (Main.tile[X, Y - 2] == null)
			{
				Main.tile[X, Y - 2] = default(Tile);
			}
			if (Main.tile[X, Y - 1] == null)
			{
				Main.tile[X, Y - 1] = default(Tile);
			}
			if (Main.tile[X, Y] == null)
			{
				Main.tile[X, Y] = default(Tile);
			}
			if (Main.tile[X, Y + 1] == null)
			{
				Main.tile[X, Y + 1] = default(Tile);
			}
			if (*Main.tile[X, Y - 2].liquid > 0)
			{
				return false;
			}
			if (*Main.tile[X, Y - 1].liquid > 0)
			{
				waterLineHeight = (float)(Y * 16);
				waterLineHeight -= (float)(*Main.tile[X, Y - 1].liquid / 16);
				return true;
			}
			if (*Main.tile[X, Y].liquid > 0)
			{
				waterLineHeight = (float)((Y + 1) * 16);
				waterLineHeight -= (float)(*Main.tile[X, Y].liquid / 16);
				return true;
			}
			if (*Main.tile[X, Y + 1].liquid > 0)
			{
				waterLineHeight = (float)((Y + 2) * 16);
				waterLineHeight -= (float)(*Main.tile[X, Y + 1].liquid / 16);
				return true;
			}
			return false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00010CF6 File Offset: 0x0000EEF6
		public static bool GetWaterLineIterate(Point pt, out float waterLineHeight)
		{
			return Collision.GetWaterLineIterate(pt.X, pt.Y, out waterLineHeight);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00010D0C File Offset: 0x0000EF0C
		public unsafe static bool GetWaterLineIterate(int X, int Y, out float waterLineHeight)
		{
			waterLineHeight = 0f;
			while (Y > 0 && *Framing.GetTileSafely(X, Y).liquid > 0)
			{
				Y--;
			}
			Y++;
			if (Main.tile[X, Y] == null)
			{
				Main.tile[X, Y] = default(Tile);
			}
			if (*Main.tile[X, Y].liquid > 0)
			{
				waterLineHeight = (float)(Y * 16);
				waterLineHeight -= (float)(*Main.tile[X, Y - 1].liquid / 16);
				return true;
			}
			return false;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public unsafe static bool WetCollision(Vector2 Position, int Width, int Height)
		{
			Collision.honey = false;
			Collision.shimmer = false;
			Vector2 vector;
			vector..ctor(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
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
			vector..ctor(vector.X - (float)(num / 2), vector.Y - (float)(num2 / 2));
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num6 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector2 = default(Vector2);
			for (int i = num6; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (!(Main.tile[i, j] == null))
					{
						if (*Main.tile[i, j].liquid > 0)
						{
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							int num3 = 16;
							float num4 = (float)(256 - (int)(*Main.tile[i, j].liquid));
							num4 /= 32f;
							vector2.Y += num4 * 2f;
							num3 -= (int)(num4 * 2f);
							if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num3)
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
						else if (Main.tile[i, j].active() && Main.tile[i, j].slope() != 0 && j > 0 && !(Main.tile[i, j - 1] == null) && *Main.tile[i, j - 1].liquid > 0)
						{
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							int num5 = 16;
							if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num5)
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

		// Token: 0x06000157 RID: 343 RVA: 0x00011138 File Offset: 0x0000F338
		public unsafe static bool LavaCollision(Vector2 Position, int Width, int Height)
		{
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num4 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector = default(Vector2);
			for (int i = num4; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (Main.tile[i, j] != null && *Main.tile[i, j].liquid > 0 && Main.tile[i, j].lava())
					{
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num2 = 16;
						float num3 = (float)(256 - (int)(*Main.tile[i, j].liquid));
						num3 /= 32f;
						vector.Y += num3 * 2f;
						num2 -= (int)(num3 * 2f);
						if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00011300 File Offset: 0x0000F500
		public unsafe static Vector4 WalkDownSlope(Vector2 Position, Vector2 Velocity, int Width, int Height, float gravity = 0f)
		{
			if (Velocity.Y != gravity)
			{
				return new Vector4(Position, Velocity.X, Velocity.Y);
			}
			int value = (int)(Position.X / 16f);
			int value2 = (int)((Position.X + (float)Width) / 16f);
			int value3 = (int)((Position.Y + (float)Height + 4f) / 16f);
			value = Utils.Clamp<int>(value, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 3);
			float num = (float)((value3 + 3) * 16);
			int num2 = -1;
			int num3 = -1;
			int num4 = 1;
			if (Velocity.X < 0f)
			{
				num4 = 2;
			}
			for (int i = value; i <= value2; i++)
			{
				for (int j = value3; j <= value3 + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = default(Tile);
					}
					if (Main.tile[i, j].nactive() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || Main.tileSolidTop[(int)(*Main.tile[i, j].type)]))
					{
						int num5 = j * 16;
						if (Main.tile[i, j].halfBrick())
						{
							num5 += 8;
						}
						if (new Rectangle(i * 16, j * 16 - 17, 16, 16).Intersects(new Rectangle((int)Position.X, (int)Position.Y, Width, Height)) && (float)num5 <= num)
						{
							if (num == (float)num5)
							{
								if (Main.tile[i, j].slope() != 0)
								{
									if (num2 != -1 && num3 != -1 && Main.tile[num2, num3] != null && Main.tile[num2, num3].slope() != 0)
									{
										if ((int)Main.tile[i, j].slope() == num4)
										{
											num = (float)num5;
											num2 = i;
											num3 = j;
										}
									}
									else
									{
										num = (float)num5;
										num2 = i;
										num3 = j;
									}
								}
							}
							else
							{
								num = (float)num5;
								num2 = i;
								num3 = j;
							}
						}
					}
				}
			}
			int num6 = num2;
			int num7 = num3;
			if (num2 != -1 && num3 != -1 && Main.tile[num6, num7] != null && Main.tile[num6, num7].slope() > 0)
			{
				int num8 = (int)Main.tile[num6, num7].slope();
				Vector2 vector2 = default(Vector2);
				vector2.X = (float)(num6 * 16);
				vector2.Y = (float)(num7 * 16);
				if (num8 != 1)
				{
					if (num8 == 2)
					{
						float num9 = vector2.X + 16f - (Position.X + (float)Width);
						if (Position.Y + (float)Height >= vector2.Y + num9 && Velocity.X < 0f)
						{
							Velocity.Y += Math.Abs(Velocity.X);
						}
					}
				}
				else
				{
					float num10 = Position.X - vector2.X;
					if (Position.Y + (float)Height >= vector2.Y + num10 && Velocity.X > 0f)
					{
						Velocity.Y += Math.Abs(Velocity.X);
					}
				}
			}
			return new Vector4(Position, Velocity.X, Velocity.Y);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public unsafe static Vector4 SlopeCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, float gravity = 0f, bool fall = false)
		{
			Collision.stair = false;
			Collision.stairFall = false;
			bool[] array = new bool[5];
			float y = Position.Y;
			float y2 = Position.Y;
			Collision.sloping = false;
			Vector2 vector2 = Position;
			Vector2 vector3 = Velocity;
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num11 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector4 = default(Vector2);
			for (int i = num11; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (!(Main.tile[i, j] == null) && Main.tile[i, j].active() && !Main.tile[i, j].inActive() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || (Main.tileSolidTop[(int)(*Main.tile[i, j].type)] && *Main.tile[i, j].frameY == 0)))
					{
						vector4.X = (float)(i * 16);
						vector4.Y = (float)(j * 16);
						int num2 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector4.Y += 8f;
							num2 -= 8;
						}
						if (Position.X + (float)Width > vector4.X && Position.X < vector4.X + 16f && Position.Y + (float)Height > vector4.Y && Position.Y < vector4.Y + (float)num2)
						{
							bool flag = true;
							if (TileID.Sets.Platforms[(int)(*Main.tile[i, j].type)])
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
								if (fall && TileID.Sets.Platforms[(int)(*Main.tile[i, j].type)])
								{
									flag2 = true;
								}
								int num3 = (int)Main.tile[i, j].slope();
								vector4.X = (float)(i * 16);
								vector4.Y = (float)(j * 16);
								if (Position.X + (float)Width > vector4.X && Position.X < vector4.X + 16f && Position.Y + (float)Height > vector4.Y && Position.Y < vector4.Y + 16f)
								{
									float num4 = 0f;
									if (num3 == 3 || num3 == 4)
									{
										if (num3 == 3)
										{
											num4 = Position.X - vector4.X;
										}
										if (num3 == 4)
										{
											num4 = vector4.X + 16f - (Position.X + (float)Width);
										}
										if (num4 >= 0f)
										{
											if (Position.Y <= vector4.Y + 16f - num4)
											{
												float num5 = vector4.Y + 16f - Position.Y - num4;
												if (Position.Y + num5 > y2)
												{
													vector2.Y = Position.Y + num5;
													y2 = vector2.Y;
													if (vector3.Y < 0.0101f)
													{
														vector3.Y = 0.0101f;
													}
													array[num3] = true;
												}
											}
										}
										else if (Position.Y > vector4.Y)
										{
											float num6 = vector4.Y + 16f;
											if (vector2.Y < num6)
											{
												vector2.Y = num6;
												if (vector3.Y < 0.0101f)
												{
													vector3.Y = 0.0101f;
												}
											}
										}
									}
									if (num3 == 1 || num3 == 2)
									{
										if (num3 == 1)
										{
											num4 = Position.X - vector4.X;
										}
										if (num3 == 2)
										{
											num4 = vector4.X + 16f - (Position.X + (float)Width);
										}
										if (num4 >= 0f)
										{
											if (Position.Y + (float)Height >= vector4.Y + num4)
											{
												float num7 = vector4.Y - (Position.Y + (float)Height) + num4;
												if (Position.Y + num7 < y)
												{
													if (flag2)
													{
														Collision.stairFall = true;
													}
													else
													{
														if (TileID.Sets.Platforms[(int)(*Main.tile[i, j].type)])
														{
															Collision.stair = true;
														}
														else
														{
															Collision.stair = false;
														}
														vector2.Y = Position.Y + num7;
														y = vector2.Y;
														if (vector3.Y > 0f)
														{
															vector3.Y = 0f;
														}
														array[num3] = true;
													}
												}
											}
										}
										else if (TileID.Sets.Platforms[(int)(*Main.tile[i, j].type)] && Position.Y + (float)Height - 4f - Math.Abs(Velocity.X) > vector4.Y)
										{
											if (flag2)
											{
												Collision.stairFall = true;
											}
										}
										else
										{
											float num8 = vector4.Y - (float)Height;
											if (vector2.Y > num8)
											{
												if (flag2)
												{
													Collision.stairFall = true;
												}
												else
												{
													if (TileID.Sets.Platforms[(int)(*Main.tile[i, j].type)])
													{
														Collision.stair = true;
													}
													else
													{
														Collision.stair = false;
													}
													vector2.Y = num8;
													if (vector3.Y > 0f)
													{
														vector3.Y = 0f;
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
			Vector2 velocity = vector2 - Position;
			Vector2 vector5 = Collision.TileCollision(Position, velocity, Width, Height, false, false, 1);
			if (vector5.Y > velocity.Y)
			{
				float num9 = velocity.Y - vector5.Y;
				vector2.Y = Position.Y + vector5.Y;
				if (array[1])
				{
					vector2.X = Position.X - num9;
				}
				if (array[2])
				{
					vector2.X = Position.X + num9;
				}
				vector3.X = 0f;
				vector3.Y = 0f;
				Collision.up = false;
			}
			else if (vector5.Y < velocity.Y)
			{
				float num10 = vector5.Y - velocity.Y;
				vector2.Y = Position.Y + vector5.Y;
				if (array[3])
				{
					vector2.X = Position.X - num10;
				}
				if (array[4])
				{
					vector2.X = Position.X + num10;
				}
				vector3.X = 0f;
				vector3.Y = 0f;
			}
			return new Vector4(vector2, vector3.X, vector3.Y);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00011EA4 File Offset: 0x000100A4
		public unsafe static Vector2 noSlopeCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector2 = Position + Velocity;
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num7 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			float num5 = (float)((value4 + 3) * 16);
			Vector2 vector3 = default(Vector2);
			for (int i = num7; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (!(Main.tile[i, j] == null) && Main.tile[i, j].active() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || (Main.tileSolidTop[(int)(*Main.tile[i, j].type)] && *Main.tile[i, j].frameY == 0)))
					{
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num6 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector3.Y += 8f;
							num6 -= 8;
						}
						if (vector2.X + (float)Width > vector3.X && vector2.X < vector3.X + 16f && vector2.Y + (float)Height > vector3.Y && vector2.Y < vector3.Y + (float)num6)
						{
							if (Position.Y + (float)Height <= vector3.Y)
							{
								Collision.down = true;
								if ((!Main.tileSolidTop[(int)(*Main.tile[i, j].type)] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num5 > vector3.Y)
								{
									num3 = i;
									num4 = j;
									if (num6 < 16)
									{
										num4++;
									}
									if (num3 != num)
									{
										result.Y = vector3.Y - (Position.Y + (float)Height);
										num5 = vector3.Y;
									}
								}
							}
							else if (Position.X + (float)Width <= vector3.X && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								num = i;
								num2 = j;
								if (num2 != num4)
								{
									result.X = vector3.X - (Position.X + (float)Width);
								}
								if (num3 == num)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector3.X + 16f && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								num = i;
								num2 = j;
								if (num2 != num4)
								{
									result.X = vector3.X + 16f - Position.X;
								}
								if (num3 == num)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector3.Y + (float)num6 && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								Collision.up = true;
								num3 = i;
								num4 = j;
								result.Y = vector3.Y + (float)num6 - Position.Y + 0.01f;
								if (num4 == num2)
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

		// Token: 0x0600015B RID: 347 RVA: 0x000122E0 File Offset: 0x000104E0
		public unsafe static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector2 = Position + Velocity;
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num7 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			float num5 = (float)((value4 + 3) * 16);
			Vector2 vector3 = default(Vector2);
			for (int i = num7; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (!(Main.tile[i, j] == null) && Main.tile[i, j].active() && !Main.tile[i, j].inActive() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || (Main.tileSolidTop[(int)(*Main.tile[i, j].type)] && *Main.tile[i, j].frameY == 0)))
					{
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num6 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector3.Y += 8f;
							num6 -= 8;
						}
						if (vector2.X + (float)Width > vector3.X && vector2.X < vector3.X + 16f && vector2.Y + (float)Height > vector3.Y && vector2.Y < vector3.Y + (float)num6)
						{
							bool flag = false;
							bool flag2 = false;
							if (Main.tile[i, j].slope() > 2)
							{
								if (Main.tile[i, j].slope() == 3 && Position.Y + Math.Abs(Velocity.X) >= vector3.Y && Position.X >= vector3.X)
								{
									flag2 = true;
								}
								if (Main.tile[i, j].slope() == 4 && Position.Y + Math.Abs(Velocity.X) >= vector3.Y && Position.X + (float)Width <= vector3.X + 16f)
								{
									flag2 = true;
								}
							}
							else if (Main.tile[i, j].slope() > 0)
							{
								flag = true;
								if (Main.tile[i, j].slope() == 1 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector3.Y + (float)num6 && Position.X >= vector3.X)
								{
									flag2 = true;
								}
								if (Main.tile[i, j].slope() == 2 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector3.Y + (float)num6 && Position.X + (float)Width <= vector3.X + 16f)
								{
									flag2 = true;
								}
							}
							if (!flag2)
							{
								if (Position.Y + (float)Height <= vector3.Y)
								{
									Collision.down = true;
									if ((!Main.tileSolidTop[(int)(*Main.tile[i, j].type)] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num5 > vector3.Y)
									{
										num3 = i;
										num4 = j;
										if (num6 < 16)
										{
											num4++;
										}
										if (num3 != num && !flag)
										{
											result.Y = vector3.Y - (Position.Y + (float)Height) + ((gravDir == -1) ? -0.01f : 0f);
											num5 = vector3.Y;
										}
									}
								}
								else if (Position.X + (float)Width <= vector3.X && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
								{
									if (i >= 1 && Main.tile[i - 1, j] == null)
									{
										Main.tile[i - 1, j] = default(Tile);
									}
									if (i < 1 || (Main.tile[i - 1, j].slope() != 2 && Main.tile[i - 1, j].slope() != 4))
									{
										num = i;
										num2 = j;
										if (num2 != num4)
										{
											result.X = vector3.X - (Position.X + (float)Width);
										}
										if (num3 == num)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.X >= vector3.X + 16f && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
								{
									if (Main.tile[i + 1, j] == null)
									{
										Main.tile[i + 1, j] = default(Tile);
									}
									if (Main.tile[i + 1, j].slope() != 1 && Main.tile[i + 1, j].slope() != 3)
									{
										num = i;
										num2 = j;
										if (num2 != num4)
										{
											result.X = vector3.X + 16f - Position.X;
										}
										if (num3 == num)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.Y >= vector3.Y + (float)num6 && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
								{
									Collision.up = true;
									num3 = i;
									num4 = j;
									result.Y = vector3.Y + (float)num6 - Position.Y + ((gravDir == 1) ? 0.01f : 0f);
									if (num4 == num2)
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

		// Token: 0x0600015C RID: 348 RVA: 0x000129E4 File Offset: 0x00010BE4
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
				Vector2 vector2 = Vector2.UnitX * testMagnitude;
				Vector4 vector3;
				vector3..ctor(position, testMagnitude, 0f);
				if (Collision.SlopeCollision(position, vector2, Width, Height, (float)gravDir, fallThrough) != vector3)
				{
					return false;
				}
				vector2 = -Vector2.UnitX * testMagnitude;
				vector3..ctor(position, 0f - testMagnitude, 0f);
				if (Collision.SlopeCollision(position, vector2, Width, Height, (float)gravDir, fallThrough) != vector3)
				{
					return false;
				}
				vector2 = Vector2.UnitY * testMagnitude;
				vector3..ctor(position, 0f, testMagnitude);
				if (Collision.SlopeCollision(position, vector2, Width, Height, (float)gravDir, fallThrough) != vector3)
				{
					return false;
				}
				vector2 = -Vector2.UnitY * testMagnitude;
				vector3..ctor(position, 0f, 0f - testMagnitude);
				if (Collision.SlopeCollision(position, vector2, Width, Height, (float)gravDir, fallThrough) != vector3)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00012B94 File Offset: 0x00010D94
		public static List<Point> FindCollisionTile(int Direction, Vector2 position, float testMagnitude, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1, bool checkCardinals = true, bool checkSlopes = false)
		{
			List<Point> list = new List<Point>();
			if (Direction > 1)
			{
				if (Direction - 2 <= 1)
				{
					Vector2 vector = (Direction == 2) ? (Vector2.UnitY * testMagnitude) : (-Vector2.UnitY * testMagnitude);
					Vector4 vec;
					vec..ctor(position, vector.X, vector.Y);
					int y = (int)(position.Y + (float)((Direction == 2) ? Height : 0)) / 16;
					float num = Math.Min(16f - position.X % 16f, (float)Width);
					float num2 = num;
					if (checkCardinals && Collision.TileCollision(position - vector, vector, (int)num, Height, fallThrough, fall2, gravDir) != vector)
					{
						list.Add(new Point((int)position.X / 16, y));
					}
					else if (checkSlopes && Collision.SlopeCollision(position, vector, (int)num, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
					{
						list.Add(new Point((int)position.X / 16, y));
					}
					while (num2 + 16f <= (float)(Width - 16))
					{
						if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitX * num2, vector, 16, Height, fallThrough, fall2, gravDir) != vector)
						{
							list.Add(new Point((int)(position.X + num2) / 16, y));
						}
						else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitX * num2, vector, 16, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
						{
							list.Add(new Point((int)(position.X + num2) / 16, y));
						}
						num2 += 16f;
					}
					int width = Width - (int)num2;
					if (checkCardinals && Collision.TileCollision(position - vector + Vector2.UnitX * num2, vector, width, Height, fallThrough, fall2, gravDir) != vector)
					{
						list.Add(new Point((int)(position.X + num2) / 16, y));
					}
					else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitX * num2, vector, width, Height, (float)gravDir, fallThrough).YZW() != vec.YZW())
					{
						list.Add(new Point((int)(position.X + num2) / 16, y));
					}
				}
			}
			else
			{
				Vector2 vector2 = (Direction == 0) ? (Vector2.UnitX * testMagnitude) : (-Vector2.UnitX * testMagnitude);
				Vector4 vec2;
				vec2..ctor(position, vector2.X, vector2.Y);
				int y2 = (int)(position.X + (float)((Direction == 0) ? Width : 0)) / 16;
				float num3 = Math.Min(16f - position.Y % 16f, (float)Height);
				float num4 = num3;
				if (checkCardinals && Collision.TileCollision(position - vector2, vector2, Width, (int)num3, fallThrough, fall2, gravDir) != vector2)
				{
					list.Add(new Point(y2, (int)position.Y / 16));
				}
				else if (checkSlopes && Collision.SlopeCollision(position, vector2, Width, (int)num3, (float)gravDir, fallThrough).XZW() != vec2.XZW())
				{
					list.Add(new Point(y2, (int)position.Y / 16));
				}
				while (num4 + 16f <= (float)(Height - 16))
				{
					if (checkCardinals && Collision.TileCollision(position - vector2 + Vector2.UnitY * num4, vector2, Width, 16, fallThrough, fall2, gravDir) != vector2)
					{
						list.Add(new Point(y2, (int)(position.Y + num4) / 16));
					}
					else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitY * num4, vector2, Width, 16, (float)gravDir, fallThrough).XZW() != vec2.XZW())
					{
						list.Add(new Point(y2, (int)(position.Y + num4) / 16));
					}
					num4 += 16f;
				}
				int height = Height - (int)num4;
				if (checkCardinals && Collision.TileCollision(position - vector2 + Vector2.UnitY * num4, vector2, Width, height, fallThrough, fall2, gravDir) != vector2)
				{
					list.Add(new Point(y2, (int)(position.Y + num4) / 16));
				}
				else if (checkSlopes && Collision.SlopeCollision(position + Vector2.UnitY * num4, vector2, Width, height, (float)gravDir, fallThrough).XZW() != vec2.XZW())
				{
					list.Add(new Point(y2, (int)(position.Y + num4) / 16));
				}
			}
			return list;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00013080 File Offset: 0x00011280
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

		// Token: 0x0600015F RID: 351 RVA: 0x00013164 File Offset: 0x00011364
		public unsafe static bool SolidCollision(Vector2 Position, int Width, int Height)
		{
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num3 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector = default(Vector2);
			for (int i = num3; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (Main.tile[i, j] != null && !Main.tile[i, j].inActive() && Main.tile[i, j].active() && Main.tileSolid[(int)(*Main.tile[i, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
					{
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num2 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector.Y += 8f;
							num2 -= 8;
						}
						if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00013354 File Offset: 0x00011554
		public unsafe static bool SolidCollision(Vector2 Position, int Width, int Height, bool acceptTopSurfaces)
		{
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num3 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector = default(Vector2);
			for (int i = num3; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!(tile == null) && tile.active() && !tile.inActive())
					{
						bool flag = Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)];
						if (acceptTopSurfaces)
						{
							flag |= (Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY == 0);
						}
						if (flag)
						{
							vector.X = (float)(i * 16);
							vector.Y = (float)(j * 16);
							int num2 = 16;
							if (tile.halfBrick())
							{
								vector.Y += 8f;
								num2 -= 8;
							}
							if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00013524 File Offset: 0x00011724
		public unsafe static Vector2 WaterCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, bool lavaWalk = true)
		{
			Vector2 result = Velocity;
			Vector2 vector = Position + Velocity;
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num3 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			Vector2 vector2 = default(Vector2);
			for (int i = num3; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					if (Main.tile[i, j] != null && *Main.tile[i, j].liquid > 0 && *Main.tile[i, j - 1].liquid == 0 && (!Main.tile[i, j].lava() || lavaWalk))
					{
						int num2 = (int)(*Main.tile[i, j].liquid / 32 * 2 + 2);
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16 + 16 - num2);
						if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && vector.Y < vector2.Y + (float)num2 && Position.Y + (float)Height <= vector2.Y && !fallThrough)
						{
							result.Y = vector2.Y - (Position.Y + (float)Height);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0001372C File Offset: 0x0001192C
		public unsafe static Vector2 AnyCollisionWithSpecificTiles(Vector2 Position, Vector2 Velocity, int Width, int Height, bool[] tilesWeCanCollideWithByType, bool evenActuated = false)
		{
			Vector2 result = Velocity;
			Vector2 vector2 = Position + Velocity;
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
			Vector2 vector3 = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!(tile == null) && tile.active() && (evenActuated || !tile.inActive()) && tilesWeCanCollideWithByType[(int)(*tile.type)])
					{
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num9 = 16;
						if (tile.halfBrick())
						{
							vector3.Y += 8f;
							num9 -= 8;
						}
						if (vector2.X + (float)Width > vector3.X && vector2.X < vector3.X + 16f && vector2.Y + (float)Height > vector3.Y && vector2.Y < vector3.Y + (float)num9)
						{
							if (Position.Y + (float)Height <= vector3.Y)
							{
								num7 = i;
								num8 = j;
								if (num7 != num5)
								{
									result.Y = vector3.Y - (Position.Y + (float)Height);
								}
							}
							else if (Position.X + (float)Width <= vector3.X && !Main.tileSolidTop[(int)(*tile.type)])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector3.X - (Position.X + (float)Width);
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector3.X + 16f && !Main.tileSolidTop[(int)(*tile.type)])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector3.X + 16f - Position.X;
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector3.Y + (float)num9 && !Main.tileSolidTop[(int)(*tile.type)])
							{
								num7 = i;
								num8 = j;
								result.Y = vector3.Y + (float)num9 - Position.Y + 0.01f;
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

		// Token: 0x06000163 RID: 355 RVA: 0x00013A50 File Offset: 0x00011C50
		public unsafe static Vector2 AnyCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool evenActuated = false)
		{
			Vector2 result = Velocity;
			Vector2 vector2 = Position + Velocity;
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
			Vector2 vector3 = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (!(Main.tile[i, j] == null) && Main.tile[i, j].active() && (evenActuated || !Main.tile[i, j].inActive()))
					{
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num9 = 16;
						if (Main.tile[i, j].halfBrick())
						{
							vector3.Y += 8f;
							num9 -= 8;
						}
						if (vector2.X + (float)Width > vector3.X && vector2.X < vector3.X + 16f && vector2.Y + (float)Height > vector3.Y && vector2.Y < vector3.Y + (float)num9)
						{
							if (Position.Y + (float)Height <= vector3.Y)
							{
								num7 = i;
								num8 = j;
								if (num7 != num5)
								{
									result.Y = vector3.Y - (Position.Y + (float)Height);
								}
							}
							else if (Position.X + (float)Width <= vector3.X && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector3.X - (Position.X + (float)Width);
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.X >= vector3.X + 16f && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								num5 = i;
								num6 = j;
								if (num6 != num8)
								{
									result.X = vector3.X + 16f - Position.X;
								}
								if (num7 == num5)
								{
									result.Y = Velocity.Y;
								}
							}
							else if (Position.Y >= vector3.Y + (float)num9 && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
							{
								num7 = i;
								num8 = j;
								result.Y = vector3.Y + (float)num9 - Position.Y + 0.01f;
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

		// Token: 0x06000164 RID: 356 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public unsafe static void HitTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
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
			Vector2 vector2 = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && !Main.tile[i, j].inActive() && Main.tile[i, j].active() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || (Main.tileSolidTop[(int)(*Main.tile[i, j].type)] && *Main.tile[i, j].frameY == 0)))
					{
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

		// Token: 0x06000165 RID: 357 RVA: 0x00013FD1 File Offset: 0x000121D1
		public static bool AnyHurtingTiles(Vector2 Position, int Width, int Height)
		{
			return Collision.HurtTiles(Position, Width, Height, null).type >= 0;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00013FE8 File Offset: 0x000121E8
		public unsafe static Collision.HurtTile HurtTiles(Vector2 Position, int Width, int Height, Player player)
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
			Vector2 vector = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!(tile == null) && !tile.inActive() && tile.active())
					{
						vector.X = (float)(i * 16);
						vector.Y = (float)(j * 16);
						int num5 = 16;
						if (tile.halfBrick())
						{
							vector.Y += 8f;
							num5 -= 8;
						}
						int num6 = 0;
						if (TileID.Sets.Suffocate[(int)(*tile.type)])
						{
							num6 = 2;
						}
						if (Position.X + (float)Width - (float)num6 >= vector.X && Position.X + (float)num6 <= vector.X + 16f && Position.Y + (float)Height - (float)num6 >= vector.Y - 0.5f && Position.Y + (float)num6 <= vector.Y + (float)num5 + 0.5f && Collision.CanTileHurt(*tile.type, i, j, player))
						{
							if (tile.slope() > 0)
							{
								if (num6 > 0)
								{
									goto IL_26A;
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
									goto IL_26A;
								}
							}
							return new Collision.HurtTile
							{
								type = (int)(*tile.type),
								x = i,
								y = j
							};
						}
					}
					IL_26A:;
				}
			}
			return new Collision.HurtTile
			{
				type = -1
			};
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00014290 File Offset: 0x00012490
		public static bool CanTileHurt(ushort type, int i, int j, Player player)
		{
			return (type != 230 || Main.getGoodWorld) && (type != 80 || Main.dontStarveWorld) && (TileID.Sets.TouchDamageBleeding[(int)type] || TileID.Sets.Suffocate[(int)type] || TileID.Sets.TouchDamageImmediate[(int)type] > 0 || (TileID.Sets.TouchDamageHot[(int)type] && (player == null || !player.fireWalk)));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000142F4 File Offset: 0x000124F4
		public unsafe static bool SwitchTiles(Vector2 Position, int Width, int Height, Vector2 oldPosition, int objType)
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
			Vector2 vector = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (!(Main.tile[i, j] == null))
					{
						int type = (int)(*Main.tile[i, j].type);
						if (Main.tile[i, j].active() && (type == 135 || type == 210 || type == 443 || type == 442))
						{
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
									switch (*Main.tile[i, j].frameX / 22)
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
										int num5 = (int)(*Main.tile[i, j].frameY / 18);
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

		// Token: 0x06000169 RID: 361 RVA: 0x00014758 File Offset: 0x00012958
		public bool SwitchTilesNew(Vector2 Position, int Width, int Height, Vector2 oldPosition, int objType)
		{
			Point point3 = Position.ToTileCoordinates();
			Point point2 = (Position + new Vector2((float)Width, (float)Height)).ToTileCoordinates();
			int num = Utils.Clamp<int>(point3.X, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(point3.Y, 0, Main.maxTilesY - 1);
			int num3 = Utils.Clamp<int>(point2.X, 0, Main.maxTilesX - 1);
			int num4 = Utils.Clamp<int>(point2.Y, 0, Main.maxTilesY - 1);
			for (int i = num; i <= num3; i++)
			{
				for (int j = num2; j <= num4; j++)
				{
					if (Main.tile[i, j] != null)
					{
						ref ushort type = ref Main.tile[i, j].type;
					}
				}
			}
			return false;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00014820 File Offset: 0x00012A20
		public unsafe static Vector2 StickyTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
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
			Vector2 vector2 = default(Vector2);
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (!(Main.tile[i, j] == null) && Main.tile[i, j].active() && !Main.tile[i, j].inActive())
					{
						if (*Main.tile[i, j].type == 51)
						{
							int num5 = 0;
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							if (Position.X + (float)Width > vector2.X - (float)num5 && Position.X < vector2.X + 16f + (float)num5 && Position.Y + (float)Height > vector2.Y && (double)Position.Y < (double)vector2.Y + 16.01)
							{
								if (*Main.tile[i, j].type == 51 && (double)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7 && Main.rand.Next(30) == 0)
								{
									Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 30, 0f, 0f, 0, default(Color), 1f);
								}
								return new Vector2((float)i, (float)j);
							}
						}
						else if (*Main.tile[i, j].type == 229 && Main.tile[i, j].slope() == 0)
						{
							int num6 = 1;
							vector2.X = (float)(i * 16);
							vector2.Y = (float)(j * 16);
							float num7 = 16.01f;
							if (Main.tile[i, j].halfBrick())
							{
								vector2.Y += 8f;
								num7 -= 8f;
							}
							if (Position.X + (float)Width > vector2.X - (float)num6 && Position.X < vector2.X + 16f + (float)num6 && Position.Y + (float)Height > vector2.Y && Position.Y < vector2.Y + num7)
							{
								if (*Main.tile[i, j].type == 51 && (double)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7 && Main.rand.Next(30) == 0)
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

		// Token: 0x0600016B RID: 363 RVA: 0x00014BED File Offset: 0x00012DED
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

		// Token: 0x0600016C RID: 364 RVA: 0x00014C14 File Offset: 0x00012E14
		public static bool SolidTiles(Vector2 position, int width, int height)
		{
			return Collision.SolidTiles((int)(position.X / 16f), (int)((position.X + (float)width) / 16f), (int)(position.Y / 16f), (int)((position.Y + (float)height) / 16f));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00014C60 File Offset: 0x00012E60
		public unsafe static bool SolidTiles(int startX, int endX, int startY, int endY)
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
					if (Main.tile[i, j].active() && !Main.tile[i, j].inActive() && Main.tileSolid[(int)(*Main.tile[i, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j].type)])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00014D34 File Offset: 0x00012F34
		public static bool SolidTiles(Vector2 position, int width, int height, bool allowTopSurfaces)
		{
			return Collision.SolidTiles((int)(position.X / 16f), (int)((position.X + (float)width) / 16f), (int)(position.Y / 16f), (int)((position.Y + (float)height) / 16f), allowTopSurfaces);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00014D84 File Offset: 0x00012F84
		public unsafe static bool SolidTiles(int startX, int endX, int startY, int endY, bool allowTopSurfaces)
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
						ushort type = *tile.type;
						bool flag = Main.tileSolid[(int)type] && !Main.tileSolidTop[(int)type];
						if (allowTopSurfaces)
						{
							flag |= (Main.tileSolidTop[(int)type] && *tile.frameY == 0);
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

		// Token: 0x06000170 RID: 368 RVA: 0x00014E5C File Offset: 0x0001305C
		public unsafe static void StepDown(ref Vector2 position, ref Vector2 velocity, int width, int height, ref float stepSpeed, ref float gfxOffY, int gravDir = 1, bool waterWalk = false)
		{
			Vector2 vector = position;
			vector.X += velocity.X;
			vector.Y = (float)Math.Floor((double)((vector.Y + (float)height) / 16f)) * 16f - (float)height;
			bool flag = false;
			int num11 = (int)(vector.X / 16f);
			int num2 = (int)((vector.X + (float)width) / 16f);
			int num3 = (int)((vector.Y + (float)height + 4f) / 16f);
			int num4 = height / 16 + ((height % 16 != 0) ? 1 : 0);
			float num5 = (float)((num3 + num4) * 16);
			float num6 = Main.bottomWorld / 16f - 42f;
			for (int i = num11; i <= num2; i++)
			{
				for (int j = num3; j <= num3 + 1; j++)
				{
					if (WorldGen.InWorld(i, j, 1))
					{
						if (Main.tile[i, j] == null)
						{
							Main.tile[i, j] = default(Tile);
						}
						if (Main.tile[i, j - 1] == null)
						{
							Main.tile[i, j - 1] = default(Tile);
						}
						if (waterWalk && *Main.tile[i, j].liquid > 0 && *Main.tile[i, j - 1].liquid == 0)
						{
							int num7 = (int)(*Main.tile[i, j].liquid / 32 * 2 + 2);
							int num8 = j * 16 + 16 - num7;
							if (new Rectangle(i * 16, j * 16 - 17, 16, 16).Intersects(new Rectangle((int)position.X, (int)position.Y, width, height)) && (float)num8 < num5)
							{
								num5 = (float)num8;
							}
						}
						if ((float)j >= num6 || (Main.tile[i, j].nactive() && (Main.tileSolid[(int)(*Main.tile[i, j].type)] || Main.tileSolidTop[(int)(*Main.tile[i, j].type)])))
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

		// Token: 0x06000171 RID: 369 RVA: 0x00015178 File Offset: 0x00013378
		public unsafe static void StepUp(ref Vector2 position, ref Vector2 velocity, int width, int height, ref float stepSpeed, ref float gfxOffY, int gravDir = 1, bool holdsMatching = false, int specialChecksMode = 0)
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
			int num4 = height / 16 + ((height % 16 != 0) ? 1 : 0);
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
				if (!WorldGen.InWorld(num2, num3 - j * gravDir, 0) || Main.tile[num2, num3 - j * gravDir] == null)
				{
					return;
				}
				tile = Main.tile[num2, num3 - j * gravDir];
				flag = (flag && (!tile.nactive() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)]));
			}
			tile = Main.tile[num2 - num, num3 - num4 * gravDir];
			flag2 = (flag2 && (!tile.nactive() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)]));
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			Tile tile2;
			if (gravDir == 1)
			{
				if (Main.tile[num2, num3 - gravDir] == null || Main.tile[num2, num3 - (num4 + 1) * gravDir] == null)
				{
					return;
				}
				tile = Main.tile[num2, num3 - gravDir];
				tile2 = Main.tile[num2, num3 - (num4 + 1) * gravDir];
				flag3 = (flag3 && (!tile.nactive() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)] || (tile.slope() == 1 && position.X + (float)(width / 2) > (float)(num2 * 16)) || (tile.slope() == 2 && position.X + (float)(width / 2) < (float)(num2 * 16 + 16)) || (tile.halfBrick() && (!tile2.nactive() || !Main.tileSolid[(int)(*tile2.type)] || Main.tileSolidTop[(int)(*tile2.type)]))));
				tile = Main.tile[num2, num3];
				tile2 = Main.tile[num2, num3 - 1];
				if (specialChecksMode == 1)
				{
					flag5 = !TileID.Sets.IgnoredByNpcStepUp[(int)(*tile.type)];
				}
				flag4 = (flag4 && ((tile.nactive() && (!tile.topSlope() || (tile.slope() == 1 && position.X + (float)(width / 2) < (float)(num2 * 16)) || (tile.slope() == 2 && position.X + (float)(width / 2) > (float)(num2 * 16 + 16))) && (!tile.topSlope() || position.Y + (float)height > (float)(num3 * 16)) && ((Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)]) || (holdsMatching && ((Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY == 0) || TileID.Sets.Platforms[(int)(*tile.type)]) && (!Main.tileSolid[(int)(*tile2.type)] || !tile2.nactive()) && flag5))) || (tile2.halfBrick() && tile2.nactive())));
				flag4 &= (!Main.tileSolidTop[(int)(*tile.type)] || !Main.tileSolidTop[(int)(*tile2.type)]);
			}
			else
			{
				tile = Main.tile[num2, num3 - gravDir];
				tile2 = Main.tile[num2, num3 - (num4 + 1) * gravDir];
				flag3 = (flag3 && (!tile.nactive() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)] || tile.slope() != 0 || (tile.halfBrick() && (!tile2.nactive() || !Main.tileSolid[(int)(*tile2.type)] || Main.tileSolidTop[(int)(*tile2.type)]))));
				tile = Main.tile[num2, num3];
				tile2 = Main.tile[num2, num3 + 1];
				flag4 = (flag4 && ((tile.nactive() && ((Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)]) || (holdsMatching && Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY == 0 && (!Main.tileSolid[(int)(*tile2.type)] || !tile2.nactive())))) || (tile2.halfBrick() && tile2.nactive())));
			}
			if ((float)(num2 * 16) >= vector.X + (float)width || (float)(num2 * 16 + 16) <= vector.X)
			{
				return;
			}
			if (gravDir == 1)
			{
				if (!flag4 || !flag3 || !flag || !flag2)
				{
					return;
				}
				float num5 = (float)(num3 * 16);
				if (Main.tile[num2, num3 - 1].halfBrick())
				{
					num5 -= 8f;
				}
				else if (Main.tile[num2, num3].halfBrick())
				{
					num5 += 8f;
				}
				if (num5 >= vector.Y + (float)height)
				{
					return;
				}
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
			else
			{
				if (!flag4 || !flag3 || !flag || !flag2 || Main.tile[num2, num3].bottomSlope() || TileID.Sets.Platforms[(int)(*tile2.type)])
				{
					return;
				}
				float num7 = (float)(num3 * 16 + 16);
				if (num7 <= vector.Y)
				{
					return;
				}
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

		// Token: 0x06000172 RID: 370 RVA: 0x000158BB File Offset: 0x00013ABB
		public static bool InTileBounds(int x, int y, int lx, int ly, int hx, int hy)
		{
			return x >= lx && x <= hx && y >= ly && y <= hy;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000158D4 File Offset: 0x00013AD4
		public unsafe static float GetTileRotation(Vector2 position)
		{
			float num = position.Y % 16f;
			int num2 = (int)(position.X / 16f);
			int num3 = (int)(position.Y / 16f);
			Tile tile = Main.tile[num2, num3];
			bool flag = false;
			for (int num4 = 2; num4 >= 0; num4--)
			{
				if (tile.active())
				{
					if (Main.tileSolid[(int)(*tile.type)])
					{
						int num5 = tile.blockType();
						if (!TileID.Sets.Platforms[(int)(*tile.type)])
						{
							switch (num5)
							{
							case 1:
								return 0f;
							case 2:
								return 0.7853982f;
							case 3:
								return -0.7853982f;
							default:
								return 0f;
							}
						}
						else
						{
							int num6 = (int)(*tile.frameX / 18);
							if (((num6 >= 0 && num6 <= 7) || (num6 >= 12 && num6 <= 16)) && (num == 0f || flag))
							{
								return 0f;
							}
							switch (num6)
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
								if (num5 == 2)
								{
									return 0.7853982f;
								}
								if (num5 == 3)
								{
									return -0.7853982f;
								}
								break;
							}
						}
					}
					else if (Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY == 0 && flag)
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

		// Token: 0x06000174 RID: 372 RVA: 0x00015A94 File Offset: 0x00013C94
		public static void GetEntityEdgeTiles(List<Point> p, Entity entity, bool left = true, bool right = true, bool up = true, bool down = true)
		{
			int num = (int)entity.position.X;
			int num2 = (int)entity.position.Y;
			int num7 = num % 16;
			int num8 = num2 % 16;
			int num3 = (int)entity.Right.X;
			int num4 = (int)entity.Bottom.Y;
			if (num % 16 == 0)
			{
				num--;
			}
			if (num2 % 16 == 0)
			{
				num2--;
			}
			if (num3 % 16 == 0)
			{
				num3++;
			}
			if (num4 % 16 == 0)
			{
				num4++;
			}
			int num5 = num3 / 16 - num / 16;
			int num6 = num4 / 16 - num2 / 16;
			num /= 16;
			num2 /= 16;
			for (int i = num; i <= num + num5; i++)
			{
				if (up)
				{
					p.Add(new Point(i, num2));
				}
				if (down)
				{
					p.Add(new Point(i, num2 + num6));
				}
			}
			for (int j = num2; j < num2 + num6; j++)
			{
				if (left)
				{
					p.Add(new Point(num, j));
				}
				if (right)
				{
					p.Add(new Point(num + num5, j));
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00015B9C File Offset: 0x00013D9C
		public unsafe static void StepConveyorBelt(Entity entity, float gravDir)
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
			Vector2 vector;
			vector..ctor(0.0001f);
			Vector2 lineStart = default(Vector2);
			Vector2 lineStart2 = default(Vector2);
			Vector2 lineEnd = default(Vector2);
			Vector2 lineEnd2 = default(Vector2);
			for (int i = 0; i < cacheForConveyorBelts.Count; i++)
			{
				Point point = cacheForConveyorBelts[i];
				if (WorldGen.InWorld(point.X, point.Y, 0) && (player == null || !player.onTrack || point.Y >= num3))
				{
					Tile tile = Main.tile[point.X, point.Y];
					if (!(tile == null) && tile.active() && tile.nactive())
					{
						int num4 = TileID.Sets.ConveyorDirection[(int)(*tile.type)];
						if (num4 != 0)
						{
							lineStart.X = (lineStart2.X = (float)(point.X * 16));
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
							if (!TileID.Sets.Platforms[(int)(*tile.type)] && Collision.CheckAABBvLineCollision2(entity.position - vector, entity.Size + vector * 2f, lineStart, lineEnd))
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
			if (flag && num != 0)
			{
				num = Math.Sign(num);
				num2 = Math.Sign(num2);
				Vector2 velocity = Vector2.Normalize(new Vector2((float)num * gravDir, (float)num2)) * 2.5f;
				Vector2 vector2 = Collision.TileCollision(entity.position, velocity, entity.width, entity.height, false, false, (int)gravDir);
				entity.position += vector2;
				Vector2 velocity2;
				velocity2..ctor(0f, 2.5f * gravDir);
				vector2 = Collision.TileCollision(entity.position, velocity2, entity.width, entity.height, false, false, (int)gravDir);
				entity.position += vector2;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000160A4 File Offset: 0x000142A4
		public static List<Point> GetTilesIn(Vector2 TopLeft, Vector2 BottomRight)
		{
			List<Point> list = new List<Point>();
			Point point3 = TopLeft.ToTileCoordinates();
			Point point2 = BottomRight.ToTileCoordinates();
			int num = Utils.Clamp<int>(point3.X, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(point3.Y, 0, Main.maxTilesY - 1);
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

		// Token: 0x06000177 RID: 375 RVA: 0x00016160 File Offset: 0x00014360
		public static void ExpandVertically(int startX, int startY, out int topY, out int bottomY, int maxExpandUp = 100, int maxExpandDown = 100)
		{
			topY = startY;
			bottomY = startY;
			if (!WorldGen.InWorld(startX, startY, 10))
			{
				return;
			}
			int i = 0;
			while (i < maxExpandUp && topY > 0 && topY >= 10 && !(Main.tile[startX, topY] == null) && !WorldGen.SolidTile3(startX, topY))
			{
				topY--;
				i++;
			}
			int j = 0;
			while (j < maxExpandDown && bottomY < Main.maxTilesY - 10 && bottomY <= Main.maxTilesY - 10 && !(Main.tile[startX, bottomY] == null) && !WorldGen.SolidTile3(startX, bottomY))
			{
				bottomY++;
				j++;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00016208 File Offset: 0x00014408
		public unsafe static Vector2 AdvancedTileCollision(bool[] forcedIgnoredTiles, Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Collision.up = false;
			Collision.down = false;
			Vector2 result = Velocity;
			Vector2 vector2 = Position + Velocity;
			int value5 = (int)(Position.X / 16f) - 1;
			int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
			int value3 = (int)(Position.Y / 16f) - 1;
			int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num7 = Utils.Clamp<int>(value5, 0, Main.maxTilesX - 1);
			value2 = Utils.Clamp<int>(value2, 0, Main.maxTilesX - 1);
			value3 = Utils.Clamp<int>(value3, 0, Main.maxTilesY - 1);
			value4 = Utils.Clamp<int>(value4, 0, Main.maxTilesY - 1);
			float num5 = (float)((value4 + 3) * 16);
			Vector2 vector3 = default(Vector2);
			for (int i = num7; i < value2; i++)
			{
				for (int j = value3; j < value4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!(tile == null) && tile.active() && !tile.inActive() && !forcedIgnoredTiles[(int)(*tile.type)] && (Main.tileSolid[(int)(*tile.type)] || (Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY == 0)))
					{
						vector3.X = (float)(i * 16);
						vector3.Y = (float)(j * 16);
						int num6 = 16;
						if (tile.halfBrick())
						{
							vector3.Y += 8f;
							num6 -= 8;
						}
						if (vector2.X + (float)Width > vector3.X && vector2.X < vector3.X + 16f && vector2.Y + (float)Height > vector3.Y && vector2.Y < vector3.Y + (float)num6)
						{
							bool flag = false;
							bool flag2 = false;
							if (tile.slope() > 2)
							{
								if (tile.slope() == 3 && Position.Y + Math.Abs(Velocity.X) >= vector3.Y && Position.X >= vector3.X)
								{
									flag2 = true;
								}
								if (tile.slope() == 4 && Position.Y + Math.Abs(Velocity.X) >= vector3.Y && Position.X + (float)Width <= vector3.X + 16f)
								{
									flag2 = true;
								}
							}
							else if (tile.slope() > 0)
							{
								flag = true;
								if (tile.slope() == 1 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector3.Y + (float)num6 && Position.X >= vector3.X)
								{
									flag2 = true;
								}
								if (tile.slope() == 2 && Position.Y + (float)Height - Math.Abs(Velocity.X) <= vector3.Y + (float)num6 && Position.X + (float)Width <= vector3.X + 16f)
								{
									flag2 = true;
								}
							}
							if (!flag2)
							{
								if (Position.Y + (float)Height <= vector3.Y)
								{
									Collision.down = true;
									if ((!Main.tileSolidTop[(int)(*tile.type)] || !fallThrough || (Velocity.Y > 1f && !fall2)) && num5 > vector3.Y)
									{
										num3 = i;
										num4 = j;
										if (num6 < 16)
										{
											num4++;
										}
										if (num3 != num && !flag)
										{
											result.Y = vector3.Y - (Position.Y + (float)Height) + ((gravDir == -1) ? -0.01f : 0f);
											num5 = vector3.Y;
										}
									}
								}
								else if (Position.X + (float)Width <= vector3.X && !Main.tileSolidTop[(int)(*tile.type)])
								{
									if (Main.tile[i - 1, j] == null)
									{
										Main.tile[i - 1, j] = default(Tile);
									}
									if (Main.tile[i - 1, j].slope() != 2 && Main.tile[i - 1, j].slope() != 4)
									{
										num = i;
										num2 = j;
										if (num2 != num4)
										{
											result.X = vector3.X - (Position.X + (float)Width);
										}
										if (num3 == num)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.X >= vector3.X + 16f && !Main.tileSolidTop[(int)(*tile.type)])
								{
									if (Main.tile[i + 1, j] == null)
									{
										Main.tile[i + 1, j] = default(Tile);
									}
									if (Main.tile[i + 1, j].slope() != 1 && Main.tile[i + 1, j].slope() != 3)
									{
										num = i;
										num2 = j;
										if (num2 != num4)
										{
											result.X = vector3.X + 16f - Position.X;
										}
										if (num3 == num)
										{
											result.Y = Velocity.Y;
										}
									}
								}
								else if (Position.Y >= vector3.Y + (float)num6 && !Main.tileSolidTop[(int)(*tile.type)])
								{
									Collision.up = true;
									num3 = i;
									num4 = j;
									result.Y = vector3.Y + (float)num6 - Position.Y + ((gravDir == 1) ? 0.01f : 0f);
									if (num4 == num2)
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

		// Token: 0x06000179 RID: 377 RVA: 0x00016818 File Offset: 0x00014A18
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
				Tuple<int, int> col;
				float num6 = Collision.TupleHitLine(num2, num3, num4, num5, 0, 0, new List<Tuple<int, int>>(), out col) ? ((col.Item1 != num4 || col.Item2 != num5) ? (new Vector2((float)Math.Abs(num2 - col.Item1), (float)Math.Abs(num3 - col.Item2)).Length() * 16f) : maxDistance) : (new Vector2((float)Math.Abs(num2 - col.Item1), (float)Math.Abs(num3 - col.Item2)).Length() * 16f);
				samples[i] = num6;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00016953 File Offset: 0x00014B53
		public static void AimingLaserScan(Vector2 startPoint, Vector2 endPoint, float samplingWidth, int samplesToTake, out Vector2 vectorTowardsTarget, out float[] samples)
		{
			samples = new float[samplesToTake];
			vectorTowardsTarget = endPoint - startPoint;
			Collision.LaserScan(startPoint, vectorTowardsTarget.SafeNormalize(Vector2.Zero), samplingWidth, vectorTowardsTarget.Length(), samples);
		}

		// Token: 0x040000BC RID: 188
		public static bool stair;

		// Token: 0x040000BD RID: 189
		public static bool stairFall;

		// Token: 0x040000BE RID: 190
		public static bool honey;

		// Token: 0x040000BF RID: 191
		public static bool shimmer;

		// Token: 0x040000C0 RID: 192
		public static bool sloping;

		// Token: 0x040000C1 RID: 193
		public static bool landMine = false;

		// Token: 0x040000C2 RID: 194
		public static bool up;

		// Token: 0x040000C3 RID: 195
		public static bool down;

		// Token: 0x040000C4 RID: 196
		public static float Epsilon = 2.7182817f;

		// Token: 0x040000C5 RID: 197
		private static List<Point> _cacheForConveyorBelts = new List<Point>();

		// Token: 0x02000788 RID: 1928
		public struct HurtTile
		{
			// Token: 0x0400659B RID: 26011
			public int type;

			// Token: 0x0400659C RID: 26012
			public int x;

			// Token: 0x0400659D RID: 26013
			public int y;
		}
	}
}
