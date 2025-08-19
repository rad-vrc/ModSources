using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Physics
{
	// Token: 0x02000113 RID: 275
	public static class BallCollision
	{
		// Token: 0x06001972 RID: 6514 RVA: 0x004BEFD0 File Offset: 0x004BD1D0
		public unsafe static BallStepResult Step(PhysicsProperties physicsProperties, Entity entity, ref float entityAngularVelocity, IBallContactListener listener)
		{
			Vector2 position = entity.position;
			Vector2 velocity = entity.velocity;
			Vector2 size = entity.Size;
			float num = entityAngularVelocity;
			float num2 = size.X * 0.5f;
			num *= physicsProperties.Drag;
			velocity *= physicsProperties.Drag;
			float num3 = velocity.Length();
			if (num3 > 1000f)
			{
				velocity = 1000f * Vector2.Normalize(velocity);
				num3 = 1000f;
			}
			int num4 = Math.Max(1, (int)Math.Ceiling((double)(num3 / 2f)));
			float num5 = 1f / (float)num4;
			velocity *= num5;
			num *= num5;
			float num6 = physicsProperties.Gravity / (float)(num4 * num4);
			bool flag = false;
			for (int i = 0; i < num4; i++)
			{
				velocity.Y += num6;
				BallPassThroughType type;
				Tile contactTile;
				if (BallCollision.CheckForPassThrough(position + size * 0.5f, out type, out contactTile))
				{
					if (type == BallPassThroughType.Tile && Main.tileSolid[(int)(*contactTile.type)] && !Main.tileSolidTop[(int)(*contactTile.type)])
					{
						velocity *= 0f;
						num *= 0f;
						flag = true;
					}
					else
					{
						BallPassThroughEvent passThrough = new BallPassThroughEvent(num5, contactTile, entity, type);
						listener.OnPassThrough(physicsProperties, ref position, ref velocity, ref num, ref passThrough);
					}
				}
				position += velocity;
				if (!BallCollision.IsBallInWorld(position, size))
				{
					return BallStepResult.OutOfBounds();
				}
				Vector2 collisionPoint;
				if (BallCollision.GetClosestEdgeToCircle(position, size, velocity, out collisionPoint, out contactTile))
				{
					Vector2 vector = Vector2.Normalize(position + size * 0.5f - collisionPoint);
					position = collisionPoint + vector * (num2 + 0.0001f) - size * 0.5f;
					BallCollisionEvent collision = new BallCollisionEvent(num5, vector, collisionPoint, contactTile, entity);
					flag = true;
					velocity = Vector2.Reflect(velocity, collision.Normal);
					listener.OnCollision(physicsProperties, ref position, ref velocity, ref collision);
					num = (collision.Normal.X * velocity.Y - collision.Normal.Y * velocity.X) / num2;
				}
			}
			velocity /= num5;
			num /= num5;
			BallStepResult result = BallStepResult.Moving();
			if (flag && velocity.X > -0.01f && velocity.X < 0.01f && velocity.Y <= 0f && velocity.Y > 0f - physicsProperties.Gravity)
			{
				result = BallStepResult.Resting();
			}
			entity.position = position;
			entity.velocity = velocity;
			entityAngularVelocity = num;
			return result;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x004BF258 File Offset: 0x004BD458
		private unsafe static bool CheckForPassThrough(Vector2 center, out BallPassThroughType type, out Tile contactTile)
		{
			Point tileCoordinates = center.ToTileCoordinates();
			Tile tile = contactTile = Main.tile[tileCoordinates.X, tileCoordinates.Y];
			type = BallPassThroughType.None;
			if (tile == null)
			{
				return false;
			}
			if (tile.nactive())
			{
				type = BallPassThroughType.Tile;
				return BallCollision.IsPositionInsideTile(center, tileCoordinates, tile);
			}
			if (*tile.liquid > 0)
			{
				float num = (float)(tileCoordinates.Y + 1) * 16f - (float)(*tile.liquid) / 255f * 16f;
				byte b = tile.liquidType();
				if (b != 1)
				{
					if (b != 2)
					{
						type = BallPassThroughType.Water;
					}
					else
					{
						type = BallPassThroughType.Honey;
					}
				}
				else
				{
					type = BallPassThroughType.Lava;
				}
				return num < center.Y;
			}
			return false;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x004BF310 File Offset: 0x004BD510
		private static bool IsPositionInsideTile(Vector2 position, Point tileCoordinates, Tile tile)
		{
			if (tile.slope() == 0 && !tile.halfBrick())
			{
				return true;
			}
			Vector2 vector = position / 16f - new Vector2((float)tileCoordinates.X, (float)tileCoordinates.Y);
			switch (tile.slope())
			{
			case 0:
				return vector.Y > 0.5f;
			case 1:
				return vector.Y > vector.X;
			case 2:
				return vector.Y > 1f - vector.X;
			case 3:
				return vector.Y < 1f - vector.X;
			case 4:
				return vector.Y < vector.X;
			default:
				return false;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x004BF3D0 File Offset: 0x004BD5D0
		private static bool IsBallInWorld(Vector2 position, Vector2 size)
		{
			return position.X > 32f && position.Y > 32f && position.X + size.X < (float)Main.maxTilesX * 16f - 32f && position.Y + size.Y < (float)Main.maxTilesY * 16f - 32f;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x004BF43C File Offset: 0x004BD63C
		private static bool GetClosestEdgeToCircle(Vector2 position, Vector2 size, Vector2 velocity, out Vector2 collisionPoint, out Tile collisionTile)
		{
			Rectangle tileBounds = BallCollision.GetTileBounds(position, size);
			Vector2 vector = position + size * 0.5f;
			BallCollision.TileEdges tileEdges = BallCollision.TileEdges.None;
			tileEdges = ((velocity.Y >= 0f) ? (tileEdges | BallCollision.TileEdges.Top) : (tileEdges | BallCollision.TileEdges.Bottom));
			tileEdges = ((velocity.X >= 0f) ? (tileEdges | BallCollision.TileEdges.Left) : (tileEdges | BallCollision.TileEdges.Right));
			tileEdges = ((velocity.Y <= velocity.X) ? (tileEdges | BallCollision.TileEdges.TopRightSlope) : (tileEdges | BallCollision.TileEdges.BottomLeftSlope));
			tileEdges = ((velocity.Y <= 0f - velocity.X) ? (tileEdges | BallCollision.TileEdges.TopLeftSlope) : (tileEdges | BallCollision.TileEdges.BottomRightSlope));
			collisionPoint = Vector2.Zero;
			collisionTile = default(Tile);
			float num = float.MaxValue;
			Vector2 closestPointOut = default(Vector2);
			float distanceSquaredOut = 0f;
			for (int i = tileBounds.Left; i < tileBounds.Right; i++)
			{
				for (int j = tileBounds.Top; j < tileBounds.Bottom; j++)
				{
					if (BallCollision.GetCollisionPointForTile(tileEdges, i, j, vector, ref closestPointOut, ref distanceSquaredOut) && distanceSquaredOut < num && Vector2.Dot(velocity, vector - closestPointOut) <= 0f)
					{
						num = distanceSquaredOut;
						collisionPoint = closestPointOut;
						collisionTile = Main.tile[i, j];
					}
				}
			}
			float num2 = size.X / 2f;
			return num < num2 * num2;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x004BF590 File Offset: 0x004BD790
		private unsafe static bool GetCollisionPointForTile(BallCollision.TileEdges edgesToTest, int x, int y, Vector2 center, ref Vector2 closestPointOut, ref float distanceSquaredOut)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.nactive() || (!Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)]))
			{
				return false;
			}
			if (!Main.tileSolid[(int)(*tile.type)] && Main.tileSolidTop[(int)(*tile.type)] && *tile.frameY != 0)
			{
				return false;
			}
			if (Main.tileSolidTop[(int)(*tile.type)])
			{
				edgesToTest &= (BallCollision.TileEdges.Top | BallCollision.TileEdges.BottomLeftSlope | BallCollision.TileEdges.BottomRightSlope);
			}
			Vector2 tilePosition;
			tilePosition..ctor((float)x * 16f, (float)y * 16f);
			bool flag = false;
			LineSegment edge = default(LineSegment);
			if (BallCollision.GetSlopeEdge(ref edgesToTest, tile, tilePosition, ref edge))
			{
				closestPointOut = BallCollision.ClosestPointOnLineSegment(center, edge);
				distanceSquaredOut = Vector2.DistanceSquared(closestPointOut, center);
				flag = true;
			}
			if (BallCollision.GetTopOrBottomEdge(edgesToTest, x, y, tilePosition, ref edge))
			{
				Vector2 vector = BallCollision.ClosestPointOnLineSegment(center, edge);
				float num = Vector2.DistanceSquared(vector, center);
				if (!flag || num < distanceSquaredOut)
				{
					distanceSquaredOut = num;
					closestPointOut = vector;
				}
				flag = true;
			}
			if (BallCollision.GetLeftOrRightEdge(edgesToTest, x, y, tilePosition, ref edge))
			{
				Vector2 vector2 = BallCollision.ClosestPointOnLineSegment(center, edge);
				float num2 = Vector2.DistanceSquared(vector2, center);
				if (!flag || num2 < distanceSquaredOut)
				{
					distanceSquaredOut = num2;
					closestPointOut = vector2;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x004BF6E8 File Offset: 0x004BD8E8
		private static bool GetSlopeEdge(ref BallCollision.TileEdges edgesToTest, Tile tile, Vector2 tilePosition, ref LineSegment edge)
		{
			switch (tile.slope())
			{
			case 0:
				return false;
			case 1:
				edgesToTest &= (BallCollision.TileEdges.Bottom | BallCollision.TileEdges.Left | BallCollision.TileEdges.BottomLeftSlope);
				if ((edgesToTest & BallCollision.TileEdges.BottomLeftSlope) == BallCollision.TileEdges.None)
				{
					return false;
				}
				edge.Start = tilePosition;
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y + 16f);
				return true;
			case 2:
				edgesToTest &= (BallCollision.TileEdges.Bottom | BallCollision.TileEdges.Right | BallCollision.TileEdges.BottomRightSlope);
				if ((edgesToTest & BallCollision.TileEdges.BottomRightSlope) == BallCollision.TileEdges.None)
				{
					return false;
				}
				edge.Start = new Vector2(tilePosition.X, tilePosition.Y + 16f);
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y);
				return true;
			case 3:
				edgesToTest &= (BallCollision.TileEdges.Top | BallCollision.TileEdges.Left | BallCollision.TileEdges.TopLeftSlope);
				if ((edgesToTest & BallCollision.TileEdges.TopLeftSlope) == BallCollision.TileEdges.None)
				{
					return false;
				}
				edge.Start = new Vector2(tilePosition.X, tilePosition.Y + 16f);
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y);
				return true;
			case 4:
				edgesToTest &= (BallCollision.TileEdges.Top | BallCollision.TileEdges.Right | BallCollision.TileEdges.TopRightSlope);
				if ((edgesToTest & BallCollision.TileEdges.TopRightSlope) == BallCollision.TileEdges.None)
				{
					return false;
				}
				edge.Start = tilePosition;
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y + 16f);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x004BF838 File Offset: 0x004BDA38
		private static bool GetTopOrBottomEdge(BallCollision.TileEdges edgesToTest, int x, int y, Vector2 tilePosition, ref LineSegment edge)
		{
			if ((edgesToTest & BallCollision.TileEdges.Bottom) != BallCollision.TileEdges.None)
			{
				Tile tile = Main.tile[x, y + 1];
				if (BallCollision.IsNeighborSolid(tile) && tile.slope() != 1 && tile.slope() != 2 && !tile.halfBrick())
				{
					return false;
				}
				edge.Start = new Vector2(tilePosition.X, tilePosition.Y + 16f);
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y + 16f);
				return true;
			}
			else
			{
				if ((edgesToTest & BallCollision.TileEdges.Top) == BallCollision.TileEdges.None)
				{
					return false;
				}
				Tile tile2 = Main.tile[x, y - 1];
				if (!Main.tile[x, y].halfBrick() && BallCollision.IsNeighborSolid(tile2) && tile2.slope() != 3 && tile2.slope() != 4)
				{
					return false;
				}
				if (Main.tile[x, y].halfBrick())
				{
					tilePosition.Y += 8f;
				}
				edge.Start = new Vector2(tilePosition.X, tilePosition.Y);
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y);
				return true;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x004BF970 File Offset: 0x004BDB70
		private static bool GetLeftOrRightEdge(BallCollision.TileEdges edgesToTest, int x, int y, Vector2 tilePosition, ref LineSegment edge)
		{
			if ((edgesToTest & BallCollision.TileEdges.Left) != BallCollision.TileEdges.None)
			{
				Tile tile = Main.tile[x, y];
				Tile tile2 = Main.tile[x - 1, y];
				if (BallCollision.IsNeighborSolid(tile2) && tile2.slope() != 1 && tile2.slope() != 3 && (!tile2.halfBrick() || tile.halfBrick()))
				{
					return false;
				}
				edge.Start = new Vector2(tilePosition.X, tilePosition.Y);
				edge.End = new Vector2(tilePosition.X, tilePosition.Y + 16f);
				if (tile.halfBrick())
				{
					edge.Start.Y = edge.Start.Y + 8f;
				}
				return true;
			}
			else
			{
				if ((edgesToTest & BallCollision.TileEdges.Right) == BallCollision.TileEdges.None)
				{
					return false;
				}
				Tile tile3 = Main.tile[x, y];
				Tile tile4 = Main.tile[x + 1, y];
				if (BallCollision.IsNeighborSolid(tile4) && tile4.slope() != 2 && tile4.slope() != 4 && (!tile4.halfBrick() || tile3.halfBrick()))
				{
					return false;
				}
				edge.Start = new Vector2(tilePosition.X + 16f, tilePosition.Y);
				edge.End = new Vector2(tilePosition.X + 16f, tilePosition.Y + 16f);
				if (tile3.halfBrick())
				{
					edge.Start.Y = edge.Start.Y + 8f;
				}
				return true;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x004BFAE0 File Offset: 0x004BDCE0
		private static Rectangle GetTileBounds(Vector2 position, Vector2 size)
		{
			int num = (int)Math.Floor((double)(position.X / 16f));
			int num2 = (int)Math.Floor((double)(position.Y / 16f));
			int num3 = (int)Math.Floor((double)((position.X + size.X) / 16f));
			int num4 = (int)Math.Floor((double)((position.Y + size.Y) / 16f));
			return new Rectangle(num, num2, num3 - num + 1, num4 - num2 + 1);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x004BFB5C File Offset: 0x004BDD5C
		private unsafe static bool IsNeighborSolid(Tile tile)
		{
			return tile != null && tile.nactive() && Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)];
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x004BFB94 File Offset: 0x004BDD94
		private static Vector2 ClosestPointOnLineSegment(Vector2 point, LineSegment lineSegment)
		{
			Vector2 vector2 = point - lineSegment.Start;
			Vector2 vector = lineSegment.End - lineSegment.Start;
			float num = vector.LengthSquared();
			float num2 = Vector2.Dot(vector2, vector) / num;
			if (num2 < 0f)
			{
				return lineSegment.Start;
			}
			if (num2 > 1f)
			{
				return lineSegment.End;
			}
			return lineSegment.Start + vector * num2;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x004BFC00 File Offset: 0x004BDE00
		[Conditional("DEBUG")]
		private static void DrawEdge(LineSegment edge)
		{
		}

		// Token: 0x0200089C RID: 2204
		[Flags]
		private enum TileEdges : uint
		{
			// Token: 0x04006A21 RID: 27169
			None = 0U,
			// Token: 0x04006A22 RID: 27170
			Top = 1U,
			// Token: 0x04006A23 RID: 27171
			Bottom = 2U,
			// Token: 0x04006A24 RID: 27172
			Left = 4U,
			// Token: 0x04006A25 RID: 27173
			Right = 8U,
			// Token: 0x04006A26 RID: 27174
			TopLeftSlope = 16U,
			// Token: 0x04006A27 RID: 27175
			TopRightSlope = 32U,
			// Token: 0x04006A28 RID: 27176
			BottomLeftSlope = 64U,
			// Token: 0x04006A29 RID: 27177
			BottomRightSlope = 128U
		}
	}
}
