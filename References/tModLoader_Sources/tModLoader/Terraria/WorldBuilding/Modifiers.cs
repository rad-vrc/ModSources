using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200007C RID: 124
	public static class Modifiers
	{
		// Token: 0x0200082D RID: 2093
		public class ShapeScale : GenAction
		{
			// Token: 0x060050B4 RID: 20660 RVA: 0x006947E2 File Offset: 0x006929E2
			public ShapeScale(int scale)
			{
				this._scale = scale;
			}

			// Token: 0x060050B5 RID: 20661 RVA: 0x006947F4 File Offset: 0x006929F4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = false;
				for (int i = 0; i < this._scale; i++)
				{
					for (int j = 0; j < this._scale; j++)
					{
						flag |= !base.UnitApply(origin, (x - origin.X << 1) + i + origin.X, (y - origin.Y << 1) + j + origin.Y, Array.Empty<object>());
					}
				}
				return !flag;
			}

			// Token: 0x04006890 RID: 26768
			private int _scale;
		}

		// Token: 0x0200082E RID: 2094
		public class Expand : GenAction
		{
			// Token: 0x060050B6 RID: 20662 RVA: 0x00694861 File Offset: 0x00692A61
			public Expand(int expansion)
			{
				this._xExpansion = expansion;
				this._yExpansion = expansion;
			}

			// Token: 0x060050B7 RID: 20663 RVA: 0x00694877 File Offset: 0x00692A77
			public Expand(int xExpansion, int yExpansion)
			{
				this._xExpansion = xExpansion;
				this._yExpansion = yExpansion;
			}

			// Token: 0x060050B8 RID: 20664 RVA: 0x00694890 File Offset: 0x00692A90
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

			// Token: 0x04006891 RID: 26769
			private int _xExpansion;

			// Token: 0x04006892 RID: 26770
			private int _yExpansion;
		}

		// Token: 0x0200082F RID: 2095
		public class RadialDither : GenAction
		{
			// Token: 0x060050B9 RID: 20665 RVA: 0x006948E6 File Offset: 0x00692AE6
			public RadialDither(double innerRadius, double outerRadius)
			{
				this._innerRadius = innerRadius;
				this._outerRadius = outerRadius;
			}

			// Token: 0x060050BA RID: 20666 RVA: 0x006948FC File Offset: 0x00692AFC
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

			// Token: 0x04006893 RID: 26771
			private double _innerRadius;

			// Token: 0x04006894 RID: 26772
			private double _outerRadius;
		}

		// Token: 0x02000830 RID: 2096
		public class Blotches : GenAction
		{
			// Token: 0x060050BB RID: 20667 RVA: 0x00694980 File Offset: 0x00692B80
			public Blotches(int scale = 2, double chance = 0.3)
			{
				this._minX = scale;
				this._minY = scale;
				this._maxX = scale;
				this._maxY = scale;
				this._chance = chance;
			}

			// Token: 0x060050BC RID: 20668 RVA: 0x006949AB File Offset: 0x00692BAB
			public Blotches(int xScale, int yScale, double chance = 0.3)
			{
				this._minX = xScale;
				this._maxX = xScale;
				this._minY = yScale;
				this._maxY = yScale;
				this._chance = chance;
			}

			// Token: 0x060050BD RID: 20669 RVA: 0x006949D6 File Offset: 0x00692BD6
			public Blotches(int leftScale, int upScale, int rightScale, int downScale, double chance = 0.3)
			{
				this._minX = leftScale;
				this._maxX = rightScale;
				this._minY = upScale;
				this._maxY = downScale;
				this._chance = chance;
			}

			// Token: 0x060050BE RID: 20670 RVA: 0x00694A04 File Offset: 0x00692C04
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._random.NextDouble();
				if (GenBase._random.NextDouble() < this._chance)
				{
					bool flag = false;
					int num5 = GenBase._random.Next(1 - this._minX, 1);
					int num2 = GenBase._random.Next(0, this._maxX);
					int num3 = GenBase._random.Next(1 - this._minY, 1);
					int num4 = GenBase._random.Next(0, this._maxY);
					for (int i = num5; i <= num2; i++)
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

			// Token: 0x04006895 RID: 26773
			private int _minX;

			// Token: 0x04006896 RID: 26774
			private int _minY;

			// Token: 0x04006897 RID: 26775
			private int _maxX;

			// Token: 0x04006898 RID: 26776
			private int _maxY;

			// Token: 0x04006899 RID: 26777
			private double _chance;
		}

		// Token: 0x02000831 RID: 2097
		public class InShape : GenAction
		{
			// Token: 0x060050BF RID: 20671 RVA: 0x00694AC4 File Offset: 0x00692CC4
			public InShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x060050C0 RID: 20672 RVA: 0x00694AD3 File Offset: 0x00692CD3
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400689A RID: 26778
			private readonly ShapeData _shapeData;
		}

		// Token: 0x02000832 RID: 2098
		public class NotInShape : GenAction
		{
			// Token: 0x060050C1 RID: 20673 RVA: 0x00694B04 File Offset: 0x00692D04
			public NotInShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x060050C2 RID: 20674 RVA: 0x00694B13 File Offset: 0x00692D13
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400689B RID: 26779
			private readonly ShapeData _shapeData;
		}

		// Token: 0x02000833 RID: 2099
		public class Conditions : GenAction
		{
			// Token: 0x060050C3 RID: 20675 RVA: 0x00694B44 File Offset: 0x00692D44
			public Conditions(params GenCondition[] conditions)
			{
				this._conditions = conditions;
			}

			// Token: 0x060050C4 RID: 20676 RVA: 0x00694B54 File Offset: 0x00692D54
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

			// Token: 0x0400689C RID: 26780
			private readonly GenCondition[] _conditions;
		}

		// Token: 0x02000834 RID: 2100
		public class OnlyWalls : GenAction
		{
			// Token: 0x060050C5 RID: 20677 RVA: 0x00694B9D File Offset: 0x00692D9D
			public OnlyWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x060050C6 RID: 20678 RVA: 0x00694BAC File Offset: 0x00692DAC
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (*GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x0400689D RID: 26781
			private ushort[] _types;
		}

		// Token: 0x02000835 RID: 2101
		public class OnlyTiles : GenAction
		{
			// Token: 0x060050C7 RID: 20679 RVA: 0x00694BFD File Offset: 0x00692DFD
			public OnlyTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x060050C8 RID: 20680 RVA: 0x00694C0C File Offset: 0x00692E0C
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.Fail();
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (*GenBase._tiles[x, y].type == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x0400689E RID: 26782
			private ushort[] _types;
		}

		// Token: 0x02000836 RID: 2102
		public class IsTouching : GenAction
		{
			// Token: 0x060050C9 RID: 20681 RVA: 0x00694C7A File Offset: 0x00692E7A
			public IsTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x060050CA RID: 20682 RVA: 0x00694C90 File Offset: 0x00692E90
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = this._useDiagonals ? 16 : 8;
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.IsTouching.DIRECTIONS[i], y + Modifiers.IsTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (*tile.type == this._tileIds[j])
							{
								return base.UnitApply(origin, x, y, args);
							}
						}
					}
				}
				return base.Fail();
			}

			// Token: 0x0400689F RID: 26783
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

			// Token: 0x040068A0 RID: 26784
			private bool _useDiagonals;

			// Token: 0x040068A1 RID: 26785
			private ushort[] _tileIds;
		}

		// Token: 0x02000837 RID: 2103
		public class NotTouching : GenAction
		{
			// Token: 0x060050CC RID: 20684 RVA: 0x00694D2F File Offset: 0x00692F2F
			public NotTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x060050CD RID: 20685 RVA: 0x00694D48 File Offset: 0x00692F48
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = this._useDiagonals ? 16 : 8;
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.NotTouching.DIRECTIONS[i], y + Modifiers.NotTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (*tile.type == this._tileIds[j])
							{
								return base.Fail();
							}
						}
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040068A2 RID: 26786
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

			// Token: 0x040068A3 RID: 26787
			private bool _useDiagonals;

			// Token: 0x040068A4 RID: 26788
			private ushort[] _tileIds;
		}

		// Token: 0x02000838 RID: 2104
		public class IsTouchingAir : GenAction
		{
			// Token: 0x060050CF RID: 20687 RVA: 0x00694DE7 File Offset: 0x00692FE7
			public IsTouchingAir(bool useDiagonals = false)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x060050D0 RID: 20688 RVA: 0x00694DF8 File Offset: 0x00692FF8
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

			// Token: 0x040068A5 RID: 26789
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

			// Token: 0x040068A6 RID: 26790
			private bool _useDiagonals;
		}

		// Token: 0x02000839 RID: 2105
		public class SkipTiles : GenAction
		{
			// Token: 0x060050D2 RID: 20690 RVA: 0x00694E72 File Offset: 0x00693072
			public SkipTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x060050D3 RID: 20691 RVA: 0x00694E84 File Offset: 0x00693084
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (*GenBase._tiles[x, y].type == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040068A7 RID: 26791
			private ushort[] _types;
		}

		// Token: 0x0200083A RID: 2106
		public class HasLiquid : GenAction
		{
			// Token: 0x060050D4 RID: 20692 RVA: 0x00694EF7 File Offset: 0x006930F7
			public HasLiquid(int liquidLevel = -1, int liquidType = -1)
			{
				this._liquidType = liquidType;
				this._liquidLevel = liquidLevel;
			}

			// Token: 0x060050D5 RID: 20693 RVA: 0x00694F10 File Offset: 0x00693110
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if ((this._liquidType == -1 || this._liquidType == (int)tile.liquidType()) && ((this._liquidLevel == -1 && *tile.liquid != 0) || this._liquidLevel == (int)(*tile.liquid)))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040068A8 RID: 26792
			private int _liquidType;

			// Token: 0x040068A9 RID: 26793
			private int _liquidLevel;
		}

		// Token: 0x0200083B RID: 2107
		public class SkipWalls : GenAction
		{
			// Token: 0x060050D6 RID: 20694 RVA: 0x00694F77 File Offset: 0x00693177
			public SkipWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x060050D7 RID: 20695 RVA: 0x00694F88 File Offset: 0x00693188
			public unsafe override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (*GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040068AA RID: 26794
			private ushort[] _types;
		}

		// Token: 0x0200083C RID: 2108
		public class IsEmpty : GenAction
		{
			// Token: 0x060050D8 RID: 20696 RVA: 0x00694FDC File Offset: 0x006931DC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x0200083D RID: 2109
		public class IsSolid : GenAction
		{
			// Token: 0x060050DA RID: 20698 RVA: 0x0069501C File Offset: 0x0069321C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._tiles[x, y].active() && WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x0200083E RID: 2110
		public class IsNotSolid : GenAction
		{
			// Token: 0x060050DC RID: 20700 RVA: 0x00695064 File Offset: 0x00693264
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active() || !WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}
		}

		// Token: 0x0200083F RID: 2111
		public class RectangleMask : GenAction
		{
			// Token: 0x060050DE RID: 20702 RVA: 0x006950AA File Offset: 0x006932AA
			public RectangleMask(int xMin, int xMax, int yMin, int yMax)
			{
				this._xMin = xMin;
				this._yMin = yMin;
				this._xMax = xMax;
				this._yMax = yMax;
			}

			// Token: 0x060050DF RID: 20703 RVA: 0x006950D0 File Offset: 0x006932D0
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (x >= this._xMin + origin.X && x <= this._xMax + origin.X && y >= this._yMin + origin.Y && y <= this._yMax + origin.Y)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040068AB RID: 26795
			private int _xMin;

			// Token: 0x040068AC RID: 26796
			private int _yMin;

			// Token: 0x040068AD RID: 26797
			private int _xMax;

			// Token: 0x040068AE RID: 26798
			private int _yMax;
		}

		// Token: 0x02000840 RID: 2112
		public class Offset : GenAction
		{
			// Token: 0x060050E0 RID: 20704 RVA: 0x0069512F File Offset: 0x0069332F
			public Offset(int x, int y)
			{
				this._xOffset = x;
				this._yOffset = y;
			}

			// Token: 0x060050E1 RID: 20705 RVA: 0x00695145 File Offset: 0x00693345
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x + this._xOffset, y + this._yOffset, args);
			}

			// Token: 0x040068AF RID: 26799
			private int _xOffset;

			// Token: 0x040068B0 RID: 26800
			private int _yOffset;
		}

		// Token: 0x02000841 RID: 2113
		public class Dither : GenAction
		{
			// Token: 0x060050E2 RID: 20706 RVA: 0x00695160 File Offset: 0x00693360
			public Dither(double failureChance = 0.5)
			{
				this._failureChance = failureChance;
			}

			// Token: 0x060050E3 RID: 20707 RVA: 0x0069516F File Offset: 0x0069336F
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._random.NextDouble() >= this._failureChance)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040068B1 RID: 26801
			private double _failureChance;
		}

		// Token: 0x02000842 RID: 2114
		public class Flip : GenAction
		{
			// Token: 0x060050E4 RID: 20708 RVA: 0x00695195 File Offset: 0x00693395
			public Flip(bool flipX, bool flipY)
			{
				this._flipX = flipX;
				this._flipY = flipY;
			}

			// Token: 0x060050E5 RID: 20709 RVA: 0x006951AB File Offset: 0x006933AB
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

			// Token: 0x040068B2 RID: 26802
			private bool _flipX;

			// Token: 0x040068B3 RID: 26803
			private bool _flipY;
		}
	}
}
