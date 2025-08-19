using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Used when attempting to add an item to a chest, but the chest is full so it spills into the world. <br />
	/// Only vanilla use is the gas trap.
	/// </summary>
	// Token: 0x020006FA RID: 1786
	[NullableContext(1)]
	[Nullable(0)]
	public class EntitySource_OverfullChest : AEntitySource_Tile
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x0064D94B File Offset: 0x0064BB4B
		public Chest Chest { get; }

		// Token: 0x06004979 RID: 18809 RVA: 0x0064D953 File Offset: 0x0064BB53
		public EntitySource_OverfullChest(int tileCoordsX, int tileCoordsY, Chest chest, [Nullable(2)] string context) : base(tileCoordsX, tileCoordsY, context)
		{
			this.Chest = chest;
		}
	}
}
