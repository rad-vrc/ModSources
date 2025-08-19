using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200007D RID: 125
	public static class ModShapes
	{
		// Token: 0x02000843 RID: 2115
		public class All : GenModShape
		{
			// Token: 0x060050E6 RID: 20710 RVA: 0x006951E0 File Offset: 0x006933E0
			public All(ShapeData data) : base(data)
			{
			}

			// Token: 0x060050E7 RID: 20711 RVA: 0x006951EC File Offset: 0x006933EC
			public override bool Perform(Point origin, GenAction action)
			{
				foreach (Point16 datum in this._data.GetData())
				{
					if (!base.UnitApply(action, origin, (int)datum.X + origin.X, (int)datum.Y + origin.Y, Array.Empty<object>()) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x02000844 RID: 2116
		public class OuterOutline : GenModShape
		{
			// Token: 0x060050E8 RID: 20712 RVA: 0x00695278 File Offset: 0x00693478
			public OuterOutline(ShapeData data, bool useDiagonals = true, bool useInterior = false) : base(data)
			{
				this._useDiagonals = useDiagonals;
				this._useInterior = useInterior;
			}

			// Token: 0x060050E9 RID: 20713 RVA: 0x00695290 File Offset: 0x00693490
			public override bool Perform(Point origin, GenAction action)
			{
				int num = this._useDiagonals ? 16 : 8;
				foreach (Point16 datum in this._data.GetData())
				{
					if (this._useInterior && !base.UnitApply(action, origin, (int)datum.X + origin.X, (int)datum.Y + origin.Y, Array.Empty<object>()) && this._quitOnFail)
					{
						return false;
					}
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)datum.X + ModShapes.OuterOutline.POINT_OFFSETS[i], (int)datum.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1]) && !base.UnitApply(action, origin, origin.X + (int)datum.X + ModShapes.OuterOutline.POINT_OFFSETS[i], origin.Y + (int)datum.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1], Array.Empty<object>()) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040068B4 RID: 26804
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

			// Token: 0x040068B5 RID: 26805
			private bool _useDiagonals;

			// Token: 0x040068B6 RID: 26806
			private bool _useInterior;
		}

		// Token: 0x02000845 RID: 2117
		public class InnerOutline : GenModShape
		{
			// Token: 0x060050EB RID: 20715 RVA: 0x006953DD File Offset: 0x006935DD
			public InnerOutline(ShapeData data, bool useDiagonals = true) : base(data)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x060050EC RID: 20716 RVA: 0x006953F0 File Offset: 0x006935F0
			public override bool Perform(Point origin, GenAction action)
			{
				int num = this._useDiagonals ? 16 : 8;
				foreach (Point16 datum in this._data.GetData())
				{
					bool flag = false;
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)datum.X + ModShapes.InnerOutline.POINT_OFFSETS[i], (int)datum.Y + ModShapes.InnerOutline.POINT_OFFSETS[i + 1]))
						{
							flag = true;
							break;
						}
					}
					if (flag && !base.UnitApply(action, origin, (int)datum.X + origin.X, (int)datum.Y + origin.Y, Array.Empty<object>()) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040068B7 RID: 26807
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

			// Token: 0x040068B8 RID: 26808
			private bool _useDiagonals;
		}
	}
}
