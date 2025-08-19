using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000065 RID: 101
	public static class Modifiers
	{
		// Token: 0x02000512 RID: 1298
		public class ShapeScale : GenAction
		{
			// Token: 0x0600304F RID: 12367 RVA: 0x005E2362 File Offset: 0x005E0562
			public ShapeScale(int scale)
			{
				this._scale = scale;
			}

			// Token: 0x06003050 RID: 12368 RVA: 0x005E2374 File Offset: 0x005E0574
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = false;
				for (int i = 0; i < this._scale; i++)
				{
					for (int j = 0; j < this._scale; j++)
					{
						flag |= !base.UnitApply(origin, (x - origin.X << 1) + i + origin.X, (y - origin.Y << 1) + j + origin.Y, new object[0]);
					}
				}
				return !flag;
			}

			// Token: 0x040057D8 RID: 22488
			private int _scale;
		}

		// Token: 0x02000513 RID: 1299
		public class Expand : GenAction
		{
			// Token: 0x06003051 RID: 12369 RVA: 0x005E23E2 File Offset: 0x005E05E2
			public Expand(int expansion)
			{
				this._xExpansion = expansion;
				this._yExpansion = expansion;
			}

			// Token: 0x06003052 RID: 12370 RVA: 0x005E23F8 File Offset: 0x005E05F8
			public Expand(int xExpansion, int yExpansion)
			{
				this._xExpansion = xExpansion;
				this._yExpansion = yExpansion;
			}

			// Token: 0x06003053 RID: 12371 RVA: 0x005E2410 File Offset: 0x005E0610
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = false;
				for (int i = -this._xExpansion; i <= this._xExpansion; i++)
				{
					for (int j = -this._yExpansion; j <= this._yExpansion; j++)
					{
						flag |= !base.UnitApply(origin, x + i, y + j, args);
					}
				}
				return !flag;
			}

			// Token: 0x040057D9 RID: 22489
			private int _xExpansion;

			// Token: 0x040057DA RID: 22490
			private int _yExpansion;
		}

		// Token: 0x02000514 RID: 1300
		public class RadialDither : GenAction
		{
			// Token: 0x06003054 RID: 12372 RVA: 0x005E2466 File Offset: 0x005E0666
			public RadialDither(double innerRadius, double outerRadius)
			{
				this._innerRadius = innerRadius;
				this._outerRadius = outerRadius;
			}

			// Token: 0x06003055 RID: 12373 RVA: 0x005E247C File Offset: 0x005E067C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Vector2D vector2D;
				vector2D..ctor((double)origin.X, (double)origin.Y);
				double num = Vector2D.Distance(new Vector2D((double)x, (double)y), vector2D);
				double num2 = Math.Max(0.0, Math.Min(1.0, (num - this._innerRadius) / (this._outerRadius - this._innerRadius)));
				if (GenBase._random.NextDouble() > num2)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057DB RID: 22491
			private double _innerRadius;

			// Token: 0x040057DC RID: 22492
			private double _outerRadius;
		}

		// Token: 0x02000515 RID: 1301
		public class Blotches : GenAction
		{
			// Token: 0x06003056 RID: 12374 RVA: 0x005E2500 File Offset: 0x005E0700
			public Blotches(int scale = 2, double chance = 0.3)
			{
				this._minX = scale;
				this._minY = scale;
				this._maxX = scale;
				this._maxY = scale;
				this._chance = chance;
			}

			// Token: 0x06003057 RID: 12375 RVA: 0x005E252B File Offset: 0x005E072B
			public Blotches(int xScale, int yScale, double chance = 0.3)
			{
				this._minX = xScale;
				this._maxX = xScale;
				this._minY = yScale;
				this._maxY = yScale;
				this._chance = chance;
			}

			// Token: 0x06003058 RID: 12376 RVA: 0x005E2556 File Offset: 0x005E0756
			public Blotches(int leftScale, int upScale, int rightScale, int downScale, double chance = 0.3)
			{
				this._minX = leftScale;
				this._maxX = rightScale;
				this._minY = upScale;
				this._maxY = downScale;
				this._chance = chance;
			}

			// Token: 0x06003059 RID: 12377 RVA: 0x005E2584 File Offset: 0x005E0784
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._random.NextDouble();
				if (GenBase._random.NextDouble() < this._chance)
				{
					bool flag = false;
					int num = GenBase._random.Next(1 - this._minX, 1);
					int num2 = GenBase._random.Next(0, this._maxX);
					int num3 = GenBase._random.Next(1 - this._minY, 1);
					int num4 = GenBase._random.Next(0, this._maxY);
					for (int i = num; i <= num2; i++)
					{
						for (int j = num3; j <= num4; j++)
						{
							flag |= !base.UnitApply(origin, x + i, y + j, args);
						}
					}
					return !flag;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057DD RID: 22493
			private int _minX;

			// Token: 0x040057DE RID: 22494
			private int _minY;

			// Token: 0x040057DF RID: 22495
			private int _maxX;

			// Token: 0x040057E0 RID: 22496
			private int _maxY;

			// Token: 0x040057E1 RID: 22497
			private double _chance;
		}

		// Token: 0x02000516 RID: 1302
		public class InShape : GenAction
		{
			// Token: 0x0600305A RID: 12378 RVA: 0x005E2644 File Offset: 0x005E0844
			public InShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x0600305B RID: 12379 RVA: 0x005E2653 File Offset: 0x005E0853
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057E2 RID: 22498
			private readonly ShapeData _shapeData;
		}

		// Token: 0x02000517 RID: 1303
		public class NotInShape : GenAction
		{
			// Token: 0x0600305C RID: 12380 RVA: 0x005E2684 File Offset: 0x005E0884
			public NotInShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x0600305D RID: 12381 RVA: 0x005E2693 File Offset: 0x005E0893
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057E3 RID: 22499
			private readonly ShapeData _shapeData;
		}

		// Token: 0x02000518 RID: 1304
		public class Conditions : GenAction
		{
			// Token: 0x0600305E RID: 12382 RVA: 0x005E26C4 File Offset: 0x005E08C4
			public Conditions(params GenCondition[] conditions)
			{
				this._conditions = conditions;
			}

			// Token: 0x0600305F RID: 12383 RVA: 0x005E26D4 File Offset: 0x005E08D4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = true;
				for (int i = 0; i < this._conditions.Length; i++)
				{
					flag &= this._conditions[i].IsValid(x, y);
				}
				if (flag)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057E4 RID: 22500
			private readonly GenCondition[] _conditions;
		}

		// Token: 0x02000519 RID: 1305
		public class OnlyWalls : GenAction
		{
			// Token: 0x06003060 RID: 12384 RVA: 0x005E271D File Offset: 0x005E091D
			public OnlyWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003061 RID: 12385 RVA: 0x005E272C File Offset: 0x005E092C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x040057E5 RID: 22501
			private ushort[] _types;
		}

		// Token: 0x0200051A RID: 1306
		public class OnlyTiles : GenAction
		{
			// Token: 0x06003062 RID: 12386 RVA: 0x005E2779 File Offset: 0x005E0979
			public OnlyTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003063 RID: 12387 RVA: 0x005E2788 File Offset: 0x005E0988
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.Fail();
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].type == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x040057E6 RID: 22502
			private ushort[] _types;
		}

		// Token: 0x0200051B RID: 1307
		public class IsTouching : GenAction
		{
			// Token: 0x06003064 RID: 12388 RVA: 0x005E27EF File Offset: 0x005E09EF
			public IsTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x06003065 RID: 12389 RVA: 0x005E2808 File Offset: 0x005E0A08
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = this._useDiagonals ? 16 : 8;
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.IsTouching.DIRECTIONS[i], y + Modifiers.IsTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (tile.type == this._tileIds[j])
							{
								return base.UnitApply(origin, x, y, args);
							}
						}
					}
				}
				return base.Fail();
			}

			// Token: 0x040057E7 RID: 22503
			private static readonly int[] DIRECTIONS = new int[]
			{
				0,
				-1,
				1,
				0,
				-1,
				0,
				0,
				1,
				-1,
				-1,
				1,
				-1,
				-1,
				1,
				1,
				1
			};

			// Token: 0x040057E8 RID: 22504
			private bool _useDiagonals;

			// Token: 0x040057E9 RID: 22505
			private ushort[] _tileIds;
		}

		// Token: 0x0200051C RID: 1308
		public class NotTouching : GenAction
		{
			// Token: 0x06003067 RID: 12391 RVA: 0x005E28A4 File Offset: 0x005E0AA4
			public NotTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x06003068 RID: 12392 RVA: 0x005E28BC File Offset: 0x005E0ABC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = this._useDiagonals ? 16 : 8;
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.NotTouching.DIRECTIONS[i], y + Modifiers.NotTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (tile.type == this._tileIds[j])
							{
								return base.Fail();
							}
						}
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057EA RID: 22506
			private static readonly int[] DIRECTIONS = new int[]
			{
				0,
				-1,
				1,
				0,
				-1,
				0,
				0,
				1,
				-1,
				-1,
				1,
				-1,
				-1,
				1,
				1,
				1
			};

			// Token: 0x040057EB RID: 22507
			private bool _useDiagonals;

			// Token: 0x040057EC RID: 22508
			private ushort[] _tileIds;
		}

		// Token: 0x0200051D RID: 1309
		public class IsTouchingAir : GenAction
		{
			// Token: 0x0600306A RID: 12394 RVA: 0x005E2958 File Offset: 0x005E0B58
			public IsTouchingAir(bool useDiagonals = false)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x0600306B RID: 12395 RVA: 0x005E2968 File Offset: 0x005E0B68
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = this._useDiagonals ? 16 : 8;
				for (int i = 0; i < num; i += 2)
				{
					if (!GenBase._tiles[x + Modifiers.IsTouchingAir.DIRECTIONS[i], y + Modifiers.IsTouchingAir.DIRECTIONS[i + 1]].active())
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x040057ED RID: 22509
			private static readonly int[] DIRECTIONS = new int[]
			{
				0,
				-1,
				1,
				0,
				-1,
				0,
				0,
				1,
				-1,
				-1,
				1,
				-1,
				-1,
				1,
				1,
				1
			};

			// Token: 0x040057EE RID: 22510
			private bool _useDiagonals;
		}

		// Token: 0x0200051E RID: 1310
		public class SkipTiles : GenAction
		{
			// Token: 0x0600306D RID: 12397 RVA: 0x005E29DF File Offset: 0x005E0BDF
			public SkipTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x0600306E RID: 12398 RVA: 0x005E29F0 File Offset: 0x005E0BF0
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].type == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057EF RID: 22511
			private ushort[] _types;
		}

		// Token: 0x0200051F RID: 1311
		public class HasLiquid : GenAction
		{
			// Token: 0x0600306F RID: 12399 RVA: 0x005E2A5C File Offset: 0x005E0C5C
			public HasLiquid(int liquidLevel = -1, int liquidType = -1)
			{
				this._liquidType = liquidType;
				this._liquidLevel = liquidLevel;
			}

			// Token: 0x06003070 RID: 12400 RVA: 0x005E2A74 File Offset: 0x005E0C74
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if ((this._liquidType == -1 || this._liquidType == (int)tile.liquidType()) && ((this._liquidLevel == -1 && tile.liquid != 0) || this._liquidLevel == (int)tile.liquid))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057F0 RID: 22512
			private int _liquidType;

			// Token: 0x040057F1 RID: 22513
			private int _liquidLevel;
		}

		// Token: 0x02000520 RID: 1312
		public class SkipWalls : GenAction
		{
			// Token: 0x06003071 RID: 12401 RVA: 0x005E2AD6 File Offset: 0x005E0CD6
			public SkipWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003072 RID: 12402 RVA: 0x005E2AE8 File Offset: 0x005E0CE8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057F2 RID: 22514
			private ushort[] _types;
		}

		// Token: 0x02000521 RID: 1313
		public class IsEmpty : GenAction
		{
			// Token: 0x06003073 RID: 12403 RVA: 0x005E2B35 File Offset: 0x005E0D35
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x02000522 RID: 1314
		public class IsSolid : GenAction
		{
			// Token: 0x06003075 RID: 12405 RVA: 0x005E2B5C File Offset: 0x005E0D5C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._tiles[x, y].active() && WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x02000523 RID: 1315
		public class IsNotSolid : GenAction
		{
			// Token: 0x06003077 RID: 12407 RVA: 0x005E2B8C File Offset: 0x005E0D8C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active() || !WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x02000524 RID: 1316
		public class RectangleMask : GenAction
		{
			// Token: 0x06003079 RID: 12409 RVA: 0x005E2BBC File Offset: 0x005E0DBC
			public RectangleMask(int xMin, int xMax, int yMin, int yMax)
			{
				this._xMin = xMin;
				this._yMin = yMin;
				this._xMax = xMax;
				this._yMax = yMax;
			}

			// Token: 0x0600307A RID: 12410 RVA: 0x005E2BE4 File Offset: 0x005E0DE4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (x >= this._xMin + origin.X && x <= this._xMax + origin.X && y >= this._yMin + origin.Y && y <= this._yMax + origin.Y)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057F3 RID: 22515
			private int _xMin;

			// Token: 0x040057F4 RID: 22516
			private int _yMin;

			// Token: 0x040057F5 RID: 22517
			private int _xMax;

			// Token: 0x040057F6 RID: 22518
			private int _yMax;
		}

		// Token: 0x02000525 RID: 1317
		public class Offset : GenAction
		{
			// Token: 0x0600307B RID: 12411 RVA: 0x005E2C43 File Offset: 0x005E0E43
			public Offset(int x, int y)
			{
				this._xOffset = x;
				this._yOffset = y;
			}

			// Token: 0x0600307C RID: 12412 RVA: 0x005E2C59 File Offset: 0x005E0E59
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x + this._xOffset, y + this._yOffset, args);
			}

			// Token: 0x040057F7 RID: 22519
			private int _xOffset;

			// Token: 0x040057F8 RID: 22520
			private int _yOffset;
		}

		// Token: 0x02000526 RID: 1318
		public class Dither : GenAction
		{
			// Token: 0x0600307D RID: 12413 RVA: 0x005E2C74 File Offset: 0x005E0E74
			public Dither(double failureChance = 0.5)
			{
				this._failureChance = failureChance;
			}

			// Token: 0x0600307E RID: 12414 RVA: 0x005E2C83 File Offset: 0x005E0E83
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._random.NextDouble() >= this._failureChance)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040057F9 RID: 22521
			private double _failureChance;
		}

		// Token: 0x02000527 RID: 1319
		public class Flip : GenAction
		{
			// Token: 0x0600307F RID: 12415 RVA: 0x005E2CA9 File Offset: 0x005E0EA9
			public Flip(bool flipX, bool flipY)
			{
				this._flipX = flipX;
				this._flipY = flipY;
			}

			// Token: 0x06003080 RID: 12416 RVA: 0x005E2CBF File Offset: 0x005E0EBF
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._flipX)
				{
					x = origin.X * 2 - x;
				}
				if (this._flipY)
				{
					y = origin.Y * 2 - y;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040057FA RID: 22522
			private bool _flipX;

			// Token: 0x040057FB RID: 22523
			private bool _flipY;
		}
	}
}
