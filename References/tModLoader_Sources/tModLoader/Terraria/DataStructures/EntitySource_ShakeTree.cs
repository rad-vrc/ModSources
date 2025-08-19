using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when items/bomb projectiles/NPCs fall out of trees being cut down
	/// </summary>
	// Token: 0x020006FE RID: 1790
	public class EntitySource_ShakeTree : AEntitySource_Tile
	{
		// Token: 0x06004982 RID: 18818 RVA: 0x0064D9C9 File Offset: 0x0064BBC9
		[NullableContext(2)]
		public EntitySource_ShakeTree(int tileCoordsX, int tileCoordsY, string context = null) : base(tileCoordsX, tileCoordsY, context)
		{
		}
	}
}
