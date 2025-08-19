using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000067 RID: 103
	public static class ModShapes
	{
		// Token: 0x02000528 RID: 1320
		public class All : GenModShape
		{
			// Token: 0x06003081 RID: 12417 RVA: 0x005E2CF4 File Offset: 0x005E0EF4
			public All(ShapeData data) : base(data)
			{
			}

			// Token: 0x06003082 RID: 12418 RVA: 0x005E2D00 File Offset: 0x005E0F00
			public override bool Perform(Point origin, GenAction action)
			{
				foreach (Point16 point in this._data.GetData())
				{
					if (!base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x02000529 RID: 1321
		public class OuterOutline : GenModShape
		{
			// Token: 0x06003083 RID: 12419 RVA: 0x005E2D8C File Offset: 0x005E0F8C
			public OuterOutline(ShapeData data, bool useDiagonals = true, bool useInterior = false) : base(data)
			{
				this._useDiagonals = useDiagonals;
				this._useInterior = useInterior;
			}

			// Token: 0x06003084 RID: 12420 RVA: 0x005E2DA4 File Offset: 0x005E0FA4
			public override bool Perform(Point origin, GenAction action)
			{
				int num = this._useDiagonals ? 16 : 8;
				foreach (Point16 point in this._data.GetData())
				{
					if (this._useInterior && !base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)point.X + ModShapes.OuterOutline.POINT_OFFSETS[i], (int)point.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1]) && !base.UnitApply(action, origin, origin.X + (int)point.X + ModShapes.OuterOutline.POINT_OFFSETS[i], origin.Y + (int)point.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1], new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040057FC RID: 22524
			private static readonly int[] POINT_OFFSETS = new int[]
			{
				1,
				0,
				-1,
				0,
				0,
				1,
				0,
				-1,
				1,
				1,
				1,
				-1,
				-1,
				1,
				-1,
				-1
			};

			// Token: 0x040057FD RID: 22525
			private bool _useDiagonals;

			// Token: 0x040057FE RID: 22526
			private bool _useInterior;
		}

		// Token: 0x0200052A RID: 1322
		public class InnerOutline : GenModShape
		{
			// Token: 0x06003086 RID: 12422 RVA: 0x005E2EF1 File Offset: 0x005E10F1
			public InnerOutline(ShapeData data, bool useDiagonals = true) : base(data)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x06003087 RID: 12423 RVA: 0x005E2F04 File Offset: 0x005E1104
			public override bool Perform(Point origin, GenAction action)
			{
				int num = this._useDiagonals ? 16 : 8;
				foreach (Point16 point in this._data.GetData())
				{
					bool flag = false;
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)point.X + ModShapes.InnerOutline.POINT_OFFSETS[i], (int)point.Y + ModShapes.InnerOutline.POINT_OFFSETS[i + 1]))
						{
							flag = true;
							break;
						}
					}
					if (flag && !base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040057FF RID: 22527
			private static readonly int[] POINT_OFFSETS = new int[]
			{
				1,
				0,
				-1,
				0,
				0,
				1,
				0,
				-1,
				1,
				1,
				1,
				-1,
				-1,
				1,
				-1,
				-1
			};

			// Token: 0x04005800 RID: 22528
			private bool _useDiagonals;
		}
	}
}
