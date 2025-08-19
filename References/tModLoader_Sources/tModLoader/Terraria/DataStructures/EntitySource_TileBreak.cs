using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when a tile or wall is broken
	/// </summary>
	// Token: 0x02000701 RID: 1793
	public class EntitySource_TileBreak : AEntitySource_Tile
	{
		// Token: 0x06004987 RID: 18823 RVA: 0x0064DA02 File Offset: 0x0064BC02
		[NullableContext(2)]
		public EntitySource_TileBreak(int tileCoordsX, int tileCoordsY, string context = null) : base(tileCoordsX, tileCoordsY, context)
		{
		}
	}
}
