using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000078 RID: 120
	public struct GenShapeActionPair
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0049F941 File Offset: 0x0049DB41
		public GenShapeActionPair(GenShape shape, GenAction action)
		{
			this.Shape = shape;
			this.Action = action;
		}

		// Token: 0x04000FF3 RID: 4083
		public readonly GenShape Shape;

		// Token: 0x04000FF4 RID: 4084
		public readonly GenAction Action;
	}
}
