using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000071 RID: 113
	public struct GenShapeActionPair
	{
		// Token: 0x06001155 RID: 4437 RVA: 0x0048CD96 File Offset: 0x0048AF96
		public GenShapeActionPair(GenShape shape, GenAction action)
		{
			this.Shape = shape;
			this.Action = action;
		}

		// Token: 0x04000FAD RID: 4013
		public readonly GenShape Shape;

		// Token: 0x04000FAE RID: 4014
		public readonly GenAction Action;
	}
}
