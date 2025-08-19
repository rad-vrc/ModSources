using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Intended for mods to use when a tile spawns an entity due to periodic/random updating, rather than in response to a specific trigger.
	/// </summary>
	// Token: 0x02000704 RID: 1796
	public class EntitySource_TileUpdate : AEntitySource_Tile
	{
		// Token: 0x0600498D RID: 18829 RVA: 0x0064DA4E File Offset: 0x0064BC4E
		[NullableContext(2)]
		public EntitySource_TileUpdate(int tileCoordsX, int tileCoordsY, string context = null) : base(tileCoordsX, tileCoordsY, context)
		{
		}
	}
}
