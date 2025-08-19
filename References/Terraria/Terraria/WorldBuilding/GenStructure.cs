using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006C RID: 108
	public abstract class GenStructure : GenBase
	{
		// Token: 0x0600114C RID: 4428
		public abstract bool Place(Point origin, StructureMap structures);
	}
}
