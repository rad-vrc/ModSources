using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000079 RID: 121
	public abstract class GenStructure : GenBase
	{
		// Token: 0x060013F5 RID: 5109
		public abstract bool Place(Point origin, StructureMap structures);
	}
}
